﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class VRHand : MonoBehaviour
{
	[SerializeField] public SteamVR_Input_Sources handType;

	[SteamVR_DefaultAction("GrabPinch", "default")]
	public SteamVR_Action_Boolean isGrabbing;

    [SteamVR_DefaultAction("Haptic", "default")]
    public SteamVR_Action_Vibration vibration;

    public AudioClip[] slapSounds;
    public float slapSoundVolume = 1.0f;

    public PlayerSounds playerSounds;

	private Player player;
	private GameObject head;
    private AudioSource audio;

    private bool isGripDown = false, isGripUp = false;
	private bool isGrippingGround = false;
	private bool isGripHeld = false;

	private float lastCollidedGroundTime;

    private Human lastCollidedHuman;
    private float lastCollidedHumanTime;

    private Human carryingHuman;
    private Vector3 carryingHumanPosition;

	private GameObject zombieHandModel;
	private GameObject zombieHandModelClasped;

	private Vector3 previousPosition;

	struct PastPosition
	{
		public float time;
		public Vector3 position;
	}

	private PastPosition[] pastPositions = new PastPosition[100];
    private PastPosition[] pastHeadPositions = new PastPosition[100];
    int currentPastPosition = 0;

	// Use this for initialization
	void Start()
	{
		// Initialise components and children
		player = GameObject.Find("Player").GetComponent<Player>();
		head = GameObject.Find("[CameraRig]/Camera");
        audio = GetComponent<AudioSource>();
		zombieHandModel = transform.Find("ZombieHand").gameObject;
        zombieHandModelClasped = transform.Find("ZombieHandClasped").gameObject;
    }

	// Update is called once per frame
	void Update()
	{
		// Update drag movement
		UpdateDragMovement();

		// Update hand clasp visuals
		UpdateClaspVisuals();

		// Update the previous position
		previousPosition = transform.position;

        // Update grip
        if (UnityEngine.XR.XRSettings.enabled)
        {
            isGripDown = isGrabbing.GetStateDown(handType);
            isGripUp = isGrabbing.GetStateUp(handType);

			isGripHeld = (isGripDown || isGripHeld) && !isGripUp;
		}
        else
        {
            isGripDown = Input.GetMouseButtonDown(0);
            isGripUp = Input.GetMouseButtonUp(0);

			isGripHeld = (isGripDown || isGripHeld) && !isGripUp;
        }
        
        if (isGripDown)
        {
            // Grab the ground
            if (Time.time - lastCollidedGroundTime <= 0.1)
            {
                isGrippingGround = true;
            }
            else if (Time.time - lastCollidedHumanTime <= 0.1f)
            {
                // or grab a human
                carryingHuman = lastCollidedHuman;
                carryingHuman.transform.SetParent(transform, true);
                carryingHumanPosition = carryingHuman.transform.localPosition;

                // Mmm tasty
                PlayGrabSound();
            }
        }
        else if (isGripUp)
        {
            // Ungrab the ground
            isGrippingGround = false;

            // Let go of humans
            if (carryingHuman)
            {
                carryingHuman.transform.SetParent(null, true);
                carryingHuman = null;
            }
        }

        // Move grabbed humans
        if (carryingHuman)
        {
            carryingHuman.transform.localPosition = carryingHumanPosition;
        }

        // Update past positions
        // Update position history
        pastPositions[currentPastPosition].position = transform.position;
        pastPositions[currentPastPosition].time = Time.time;
        pastHeadPositions[currentPastPosition].position = player.transform.position;
        pastHeadPositions[currentPastPosition].time = Time.time;
        currentPastPosition++;

        if (currentPastPosition >= pastPositions.Length)
        {
            currentPastPosition = 0;
        }
    }

	private bool wasGrippingGround = false;
	void UpdateDragMovement()
	{
		if (isGrippingGround)
		{
            Vector3 movementVector = previousPosition - transform.position;

            // Don't move vertically
            movementVector.y = 0.0f;

            // Cancel momentum
            player.velocity = Vector3.zero;

            // Use rotation-distance movement or absolute movement
            if (!player.canTurn)
            {
                player.SetPosition(player.transform.position + movementVector);
            }
            else
            {
                Vector3 previousPositionMinusY = new Vector3(previousPosition.x, 0.0f, previousPosition.z);
                Vector3 positionMinusY = new Vector3(transform.position.x, 0.0f, transform.position.z);
                Vector3 headPositionMinusY = new Vector3(player.head.position.x, 0.0f, player.head.position.z);
				Vector3 headPosition = player.head.transform.position;
				Vector3 headForwardMinusY = new Vector3(player.head.forward.x, 0.0f, player.head.forward.z);
				Vector3 torsoPosition = new Vector3(player.torsoPosition.x, 0.0f, player.torsoPosition.z);
				Vector3 torsoForward = (headPositionMinusY - torsoPosition).normalized;

				// Rotate
                player.transform.rotation *= Quaternion.FromToRotation(positionMinusY - torsoPosition, previousPositionMinusY - torsoPosition);

				// Rotate around the head rather than the centre
				player.transform.position -= player.head.transform.position - headPosition;

				// Move
				player.SetPosition(player.transform.position - torsoForward * Vector3.Dot(torsoForward, positionMinusY - previousPositionMinusY));
			}
		}
		else if (wasGrippingGround)
		{
			// Assume skateboard movement
			if (player.hasSkateboard)
			{
				// Get the average motion vector of the hand
				Vector3 averageMotion = new Vector3(0.0f, 0.0f, 0.0f);
				float timeSpan = 0.2f;
				int numSamples = 0;

				for (int i = 0; i < pastPositions.Length; i++)
				{
					if (Time.time - pastHeadPositions[i].time <= timeSpan)
					{
						averageMotion += (pastHeadPositions[i].position - pastHeadPositions[(i - 1 + pastHeadPositions.Length) % pastHeadPositions.Length].position);
						numSamples++;
					}
				}

				// Add it to the player's velocity
				averageMotion.y = 0.0f; // you raise me up...
				player.velocity += averageMotion / timeSpan;
			}
        }

        wasGrippingGround = isGrippingGround;
	}

    void PlaySlapSound()
    {
        audio.clip = slapSounds[Random.Range(0, slapSounds.Length)];
        audio.pitch = Random.Range(0.95f, 1.05f);
        audio.volume = slapSoundVolume;
        audio.Play();
    }

    void PlayGrabSound()
    {
        // Find the player to say zombie stuff
        if (playerSounds)
        {
            playerSounds.PlayHumanGrabSound();
        }
    }

	private void UpdateClaspVisuals()
	{
		if (zombieHandModel && zombieHandModelClasped)
		{
            if (UnityEngine.XR.XRSettings.enabled || GetComponent<MouseHand>())
            {
                zombieHandModel.SetActive(!isGripHeld);
                zombieHandModelClasped.SetActive(isGripHeld);
            }
            else
            {
                // Disable the hand if VR isn't working and this isn't a mouse-controlled hand
                transform.Find("ZombieHand").gameObject.SetActive(false);
                transform.Find("ZombieHandClasped").gameObject.SetActive(false);
            }
        }
	}

    void OnTriggerStay(Collider other)
	{
        var human = other.gameObject.GetComponent<Human>();

        // Check if we picked up a human
        if (human)
		{
			// Feedback to the user
			if (Time.time - lastCollidedHumanTime >= 0.05f)
			{
                if (UnityEngine.XR.XRSettings.enabled)
                {
                    vibration.Execute(0.0f, 0.08f, 80, 0.6f, handType);
                }
			}

			lastCollidedHuman = human;
            lastCollidedHumanTime = Time.time;
        }
        else if (!other.isTrigger) // don't collide with other triggers such as hands etc
		{
			// Feedback to the user
			if (Time.time - lastCollidedGroundTime >= 0.05f)
            {
                if (UnityEngine.XR.XRSettings.enabled)
                {
                    vibration.Execute(0.0f, 0.08f, 80, 0.6f, handType);
                }

                // Play slap sound
                PlaySlapSound();
			}

			lastCollidedGroundTime = Time.time;
        }

        // Check if we clicked the win box
        if (other.gameObject.GetComponent<WinCube>() && isGripDown)
        {
            // Restart the game
            GameMode.RestartGame();
        }

        // Check if we picked up the skateboard
        if (other.gameObject.CompareTag("Skateboard") && isGripDown)
        {
            // Give it to the player
            player.hasSkateboard = true;

            Destroy(other.gameObject);
        }
	}
}
