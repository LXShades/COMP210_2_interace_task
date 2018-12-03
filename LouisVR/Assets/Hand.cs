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

        // Turn off the cube if VR is enabled
        if (UnityEngine.XR.XRSettings.enabled)
        {
            Transform cube = transform.Find("Cube");

            if (cube)
            {
                cube.gameObject.SetActive(false);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        // Update the previous position
        previousPosition = transform.position;

        if (!UnityEngine.XR.XRSettings.enabled)
        {
            // Position the hand from camera
            Ray cameraRay = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(cameraRay, out hit, defaultArmLength, ~(1 << LayerMask.NameToLayer("Hand")), QueryTriggerInteraction.Ignore))
            {
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
	}
}
