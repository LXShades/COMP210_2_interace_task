using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	[SerializeField] public float neutralHeadHeight = 0.3f; // The central height of your head above the road
	[SerializeField] public float minHeadHeight = 0.1f; // The minimum height of your head above the road
	[SerializeField] public float maxHeadHeight = 0.6f;

    [SerializeField] public GameObject winObjectType;

	public Transform head;

    private GameObject winObject;

	// Use this for initialization
	void Start () {
		head = transform.Find("[CameraRig]").Find("Camera");
	}
	
	// Update is called once per frame
	void Update () {
        if (!UnityEngine.XR.XRSettings.enabled)
        {
            // Move with WASD
            transform.position += Camera.main.transform.right * (Input.GetAxis("Horizontal") * 6.0f * Time.deltaTime);
            transform.position += Camera.main.transform.forward * (Input.GetAxis("Vertical") * 6.0f * Time.deltaTime);

            // Restart the game on keypress
            if (Input.GetKeyDown(KeyCode.R))
            {
                GameMode.RestartGame();
            }
        }

		// Adjust head height based on the user's max and min height
		Vector3 headPosition = head.position;
		Vector3 localPosition = transform.position;

		if (headPosition.y <= minHeadHeight)
		{
			localPosition.y -= headPosition.y - minHeadHeight;
		}

		if (headPosition.y >= maxHeadHeight)
		{
			localPosition.y -= headPosition.y - maxHeadHeight;
		}

		transform.position = localPosition;

        // If we won, show the win cube
        if (GameMode.numHumans == 0 && !winObject)
        {
            winObject = Instantiate(winObjectType);
            winObject.transform.position = headPosition + head.forward * 3.0f;
        }
    }
}
