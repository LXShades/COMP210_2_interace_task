﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class VRHand : MonoBehaviour {
	[SteamVR_DefaultAction("GrabPinch", "default")]
	public SteamVR_Action_Boolean isGrabbing;

	private GameObject camera;

	private bool isGripping = false;

	private float lastCollisionTime;

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
		//isGripping = Input.GetButton("Fire1");
		if (isGrabbing.GetStateDown(SteamVR_Input_Sources.Any))
		{
			if (Time.time - lastCollisionTime <= 0.1)
			{
				Debug.Log("Grabbed");
				isGripping = true;
			}
		}
		else if (isGrabbing.GetStateUp(SteamVR_Input_Sources.Any))
		{
			Debug.Log("NoGrab");
			isGripping = false;
		}
	}

	void UpdateDragMovement()
	{
		if (isGripping)
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
		Debug.Log("Colliding");
		lastCollisionTime = Time.time;
	}
}