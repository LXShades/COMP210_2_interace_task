using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class VRHand : MonoBehaviour
{
	[SerializeField] public SteamVR_Input_Sources handType;

	[SteamVR_DefaultAction("GrabPinch", "default")]
	public SteamVR_Action_Boolean isGrabbing;

	private GameObject camera;

	private bool isGrippingGround = false;

	private float lastCollidedGroundTime;

    private Human lastCollidedHuman;
    private float lastCollidedHumanTime;

    private Human carryingHuman;
    private Vector3 carryingHumanPosition;

	private Vector3 previousPosition;

	// Use this for initialization
	void Start()
	{
		camera = GameObject.Find("[CameraRig]");
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

	void UpdateDragMovement()
	{
		if (isGrippingGround)
		{
			Vector3 movementVector = previousPosition - transform.position;

			// Don't move vertically
			movementVector.y = 0.0f;

			// Pull the camera along
			camera.transform.position += movementVector;
		}
	}

	void OnTriggerStay(Collider other)
	{
        var human = other.gameObject.GetComponent<Human>();

        if (human)
        {
            lastCollidedHuman = human;
            lastCollidedHumanTime = Time.time;
        }
        else
        {
            lastCollidedGroundTime = Time.time;
        }
	}
}
