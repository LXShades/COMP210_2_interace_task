using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {
    private Camera camera;

    [SerializeField] public float defaultArmLength = 3.0f;

    private bool isGripping = false;

    private Vector3 previousPosition;

    private Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
        camera = Camera.main;
        rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        // Update drag movement
        UpdateDragMovement();

        // Update the previous position
        previousPosition = transform.position;

        // Position the hand from camera
        Ray cameraRay = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(cameraRay, out hit, defaultArmLength, ~(1<<LayerMask.NameToLayer("Hand"))))
        {
            Debug.Log("Collided with " + hit.collider.gameObject);
            transform.position = hit.point;
        }
        else
        {
            transform.position = cameraRay.origin + cameraRay.direction * defaultArmLength;
        }

        // Change the distance
        defaultArmLength += Input.mouseScrollDelta.y;

        // Drag
        isGripping = Input.GetButton("Fire1");
	}

    void UpdateDragMovement()
    {
        /*if (isGripping)
        {
            // Pull the camera along
            camera.transform.position -= transform.position - previousPosition;
        }*/
    }
}
