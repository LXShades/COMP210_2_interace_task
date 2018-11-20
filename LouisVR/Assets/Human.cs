using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour {
    Vector3 runDirection;

    [SerializeField] float maxRunSpeed = 40.0f;
    [SerializeField] float minRunSpeed = 0.0f;
    [SerializeField] float maxRange = 30.0f; // range after which the human stops running to let the player catch up

    Player player;

	// Use this for initialization
	void Start () {
        float runAngle = Random.Range(-40.0f, 40.0f) * Mathf.Deg2Rad;

        runDirection = new Vector3(Mathf.Cos(runAngle), 0.0f, Mathf.Sin(runAngle));
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
        // Run away from the player
        float runSpeed = Mathf.Lerp(maxRunSpeed, minRunSpeed, Vector3.Distance(transform.position, player.transform.position) / maxRange);

        if (Mathf.Sign(transform.position.z - player.transform.position.z) != Mathf.Sign(runDirection.z))
        {
            runDirection.z = -runDirection.z;
        }

        // Move by the velocity
		if (runSpeed >= 0.0f)
        {
            transform.position += runDirection * (Time.deltaTime * runSpeed);
            //GetComponent<Rigidbody>().velocity = runDirection * runSpeed;
            transform.LookAt(runDirection);
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        // Bounce off the sides of the road
        runDirection.x = -runDirection.x;
    }
}
