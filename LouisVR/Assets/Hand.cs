using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {
    private Camera camera;

    private bool isGripping = false;

    private Vector3 previousPosition;

	// Use this for initialization
	void Start () {
        camera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        // Update drag movement
        UpdateDragMovement();

        // Update the previous position
        previousPosition = transform.position;

        // Position the hand from camera
        Ray cameraRay = camera.ScreenPointToRay(Input.mousePosition);
        float maxArmLength = 2.0f;
        RaycastHit hit;

        if (Physics.Raycast(cameraRay, out hit, maxArmLength, ~(1<<LayerMask.NameToLayer("Hand"))))
        {
            Debug.Log("Collided with " + hit.collider.gameObject);
            transform.position = hit.point;
        }
        else
        {
            transform.position = cameraRay.origin + cameraRay.direction * maxArmLength;
        }

        // Drag
        isGripping = Input.GetButton("Fire1");
	}

    void UpdateDragMovement()
    {
        if (isGripping)
        {
            // Pull the camera along
            camera.transform.position -= transform.position - previousPosition;
        }
    }
}
