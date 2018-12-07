using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	[SerializeField] public float neutralHeadHeight = 0.3f; // The central height of your head above the road
	[SerializeField] public float minHeadHeight = 0.1f; // The minimum height of your head above the road
	[SerializeField] public float maxHeadHeight = 0.6f;

    [SerializeField] public GameObject winObjectType;

    [SerializeField] public bool canTurn = false;
	[SerializeField] public bool hasSkateboard = true;
	[SerializeField] public float skateboardFriction = 1.0f;

	public Vector3 velocity;

	public Transform head;

    private GameObject winObject;

	private Vector3 initialPosition;

    private Rigidbody rigidbody;

    // Use this for initialization
    void Start () {
		head = transform.Find("[CameraRig]").Find("Camera");
		initialPosition = transform.position;
        rigidbody = GetComponent<Rigidbody>();
        lastPosition = transform.position;
    }

    // Update is called once per frame
    Vector3 lastPosition = Vector3.zero;

	void Update ()
    {
        // stupid dumb lazy collision detection
        Vector3 newPosition = transform.position;
        transform.position = lastPosition;

        SetPosition(newPosition);

        lastPosition = transform.position;

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

		if (headPosition.y <= minHeadHeight + initialPosition.y)
		{
			localPosition.y -= headPosition.y - (minHeadHeight + initialPosition.y);
		}

		if (headPosition.y >= maxHeadHeight + initialPosition.y)
		{
			localPosition.y -= headPosition.y - (maxHeadHeight + initialPosition.y);
		}

		transform.position = localPosition;

		// Move by velocity
		transform.position += velocity * Time.deltaTime;

		// Friction
		if (velocity.magnitude > skateboardFriction * Time.deltaTime)
		{
			velocity -= velocity.normalized * (skateboardFriction * Time.deltaTime);
		}
		else
		{
			velocity = Vector3.zero;
		}

        // If we won, spawn the win cube in front of the player
        if (GameMode.numHumans == 0 && !winObject)
        {
            winObject = Instantiate(winObjectType);
            winObject.transform.position = headPosition + head.forward * 1.0f;
        }
    }

    // Sets the player position with collision detection
    public void SetPosition(Vector3 position)
    {
        // Do collision detection
        RaycastHit hit;

        if (rigidbody.SweepTest((position - transform.position).normalized, out hit, Vector3.Distance(transform.position, position)))
        {
            transform.position += (position - transform.position).normalized * (hit.distance * 0.95f);
        }
        else
        {
            transform.position = position;
        }
    }
}
