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

	private Player player;
	private GameObject head;

	private bool isGrippingGround = false;

	private float lastCollidedGroundTime;

    private Human lastCollidedHuman;
    private float lastCollidedHumanTime;

    private Human carryingHuman;
    private Vector3 carryingHumanPosition;

	private Vector3 previousPosition;

	struct PastPosition
	{
		public float time;
		public Vector3 position;
	}

	private PastPosition[] pastPositions = new PastPosition[100];
	int currentPastPosition = 0;

	// Use this for initialization
	void Start()
	{
		player = GameObject.Find("Player").GetComponent<Player>();
		head = GameObject.Find("[CameraRig]/Camera");
	}

	// Update is called once per frame
	void Update()
	{
		// Update drag movement
		UpdateDragMovement();

		// Update the previous position
		previousPosition = transform.position;

        // Update grip
        bool isGripDown = Input.GetMouseButtonDown(0), isGripUp = Input.GetMouseButtonUp(0);

        if (UnityEngine.XR.XRSettings.enabled)
        {
            isGripDown = isGrabbing.GetStateDown(handType);
            isGripUp = isGrabbing.GetStateUp(handType);
        }

        //isGripping = Input.GetButton("Fire1");
        if (isGripDown)
        {
            // Grab the ground
            if (Time.time - lastCollidedGroundTime <= 0.1)
            {
                Debug.Log("Grabbed");
                isGrippingGround = true;
            }

            // Grab humans
            if (Time.time - lastCollidedHumanTime <= 0.1f)
            {
                carryingHuman = lastCollidedHuman;
                carryingHuman.transform.SetParent(transform, true);
                carryingHumanPosition = carryingHuman.transform.localPosition;
            }
        }
        else if (isGripUp)
        {
            Debug.Log("NoGrab");
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

            // Convert them to zombies if close enough to mouth
            if (Vector3.Distance(carryingHuman.transform.position, Camera.main.transform.position) <= 2.0f)
            {
                carryingHuman.isZombie = true;
            }
        }
    }

	private bool wasGrippingGround = false;
	void UpdateDragMovement()
	{
		// Update position history
		pastPositions[currentPastPosition].position = transform.position;
		pastPositions[currentPastPosition].time = Time.time;
		currentPastPosition++;

		if (currentPastPosition >= pastPositions.Length)
		{
			currentPastPosition = 0;
		}

		if (isGrippingGround)
		{
			Vector3 movementVector = previousPosition - transform.position;

			// Don't move vertically
			movementVector.y = 0.0f;

			// Pull the camera along
			player.transform.position += movementVector;

			// Turn the camera
			float angleDifference = Mathf.Atan2(transform.position.y - head.transform.position.y, transform.position.x - head.transform.position.x);
			angleDifference -= Mathf.Atan2(previousPosition.y - head.transform.position.y, previousPosition.x - head.transform.position.x);

			/*Vector3 eulerAngles = player.transform.rotation.eulerAngles;
			eulerAngles.y -= angleDifference * Mathf.Rad2Deg;
			player.transform.rotation = Quaternion.Euler(eulerAngles);*/

			// Cancel momentum
			player.velocity = Vector3.zero;
		}
		else if (wasGrippingGround)
		{
			// Assume skateboard movement
			if (player.hasSkateboard)
			{
				// Get the average motion vector of the hand
				Vector3 averageMotion = new Vector3(0.0f, 0.0f, 0.0f);
				float timeSpan = 0.1f;
				int numSamples = 0;

				for (int i = 0; i < pastPositions.Length; i++)
				{
					if (Time.time - pastPositions[i].time <= timeSpan)
					{
						averageMotion += (pastPositions[i].position - pastPositions[(i - 1 + pastPositions.Length) % pastPositions.Length].position);
						numSamples++;
					}
				}

				// Add it to the player's velocity
				averageMotion.y = 0.0f; // you raise me up...
				player.velocity -= averageMotion / timeSpan;

				Debug.Log("numsamples: " + numSamples + "motion: " + averageMotion);
			}
		}

		wasGrippingGround = isGrippingGround;
	}

	void OnTriggerStay(Collider other)
	{
        var human = other.gameObject.GetComponent<Human>();

        // Check if we picked up a human
        if (human)
        {
            lastCollidedHuman = human;
            lastCollidedHumanTime = Time.time;
        }
        else
        {
            lastCollidedGroundTime = Time.time;
        }

        // Check if we clicked the win box
        if (other.gameObject.GetComponent<WinCube>())
        {
            bool isGripDown = Input.GetMouseButtonDown(0), isGripUp = Input.GetMouseButtonUp(0);

            if (UnityEngine.XR.XRSettings.enabled)
            {
                isGripDown = isGrabbing.GetStateDown(handType);
                isGripUp = isGrabbing.GetStateUp(handType);
            }

            if (isGripDown)
            {
                // Restart the game
                GameMode.RestartGame();
            }
        }
	}
}
