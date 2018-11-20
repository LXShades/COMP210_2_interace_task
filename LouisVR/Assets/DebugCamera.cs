using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCamera : MonoBehaviour {
    private Vector3 lastMousePosition;

    [SerializeField] public int sensitivity = 2000; // pixels per rotation

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // Reset the delta mouse position if we've just pressed the mouse button
        if (Input.GetMouseButtonDown(1))
        {
            lastMousePosition = Input.mousePosition;
        }

        // Turn the camera
		if (Input.GetMouseButton(1))
        {
            transform.rotation = Quaternion.AngleAxis(360.0f * (Input.mousePosition.x - lastMousePosition.x) / sensitivity, Vector3.up) * transform.rotation;
            transform.rotation = Quaternion.AngleAxis(360.0f * (lastMousePosition.y - Input.mousePosition.y) / sensitivity, transform.right) * transform.rotation;
        }

        lastMousePosition = Input.mousePosition;
	}
}
