using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour {
    Vector3 runDirection;

    [SerializeField] float maxRunSpeed = 40.0f;
    [SerializeField] float minRunSpeed = 0.0f;
    [SerializeField] float maxRange = 30.0f; // range after which the human stops running to let the player catch up

    Player player;

    public bool isZombie
    {
        set
        {
            // Make me green
            if (value == true)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    var renderer = transform.GetChild(i).GetComponent<MeshRenderer>();
                    if (renderer)
                    {
                        renderer.material.color = Color.green;
                    }
                }
            }

            if (value != _isZombie)
            {
                if (value == true)
                {
                    GameMode.numZombies++;
                    GameMode.numHumans--;
                }
                else
                {
                    GameMode.numHumans++;
                    GameMode.numZombies--;
                }
            }

            _isZombie = value;
        }
        get
        {
            return _isZombie;
        }
    }
    private bool _isZombie;

    // Use this for initialization
    void Start () {
        float runAngle = Random.Range(-40.0f, 40.0f) * Mathf.Deg2Rad;

        // Initialise the run direction
        runDirection = new Vector3(Mathf.Sin(runAngle), 0.0f, Mathf.Cos(runAngle));
        player = GameObject.FindWithTag("Player").GetComponent<Player>();

        // Increment game mode's number of humans
        GameMode.numHumans++;
	}
	
	// Update is called once per frame
	void Update () {
        // Run away from the player
        float runSpeed = Mathf.Lerp(maxRunSpeed, minRunSpeed, Vector3.Distance(transform.position, player.transform.position) / maxRange);

        Debug.Log("Run speed: " + runSpeed);

        if (Mathf.Sign(transform.position.z - player.transform.position.z) != Mathf.Sign(runDirection.z))
        {
            runDirection.z = -runDirection.z;
        }

        // If we're zombiefied, run towards the nearest human
        if (isZombie)
        {
            GameObject closestHuman = null;
            float closestHumanDistance = 0.0f;
            foreach (var human in GameObject.FindGameObjectsWithTag("Human"))
            {
                var humanComponent = human.GetComponent<Human>();
                if (humanComponent && (!humanComponent.isZombie) && (Vector3.Distance(human.transform.position, transform.position) < closestHumanDistance || !closestHuman))
                {
                    closestHuman = human;
                    closestHumanDistance = Vector3.Distance(human.transform.position, transform.position);
                }
            }

            if (closestHuman != null)
            {
                // Chase the human at a reasonable speed
                runDirection = (closestHuman.transform.position - transform.position).normalized;
                //runSpeed = what could the speed be?
            }
        }

        // Move by the velocity
		if (runSpeed >= 0.0f)
        {
            transform.position += runDirection * (Time.deltaTime * runSpeed);
            //GetComponent<Rigidbody>().velocity = runDirection * runSpeed;
            transform.LookAt(transform.position + runDirection);
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        // Convert other zombies to zombies if we're a zombie
        var human = collision.collider.gameObject.GetComponent<Human>();
        if (isZombie && human)
        {
            human.isZombie = true;
        }
        // Bounce off the sides of the road
        runDirection.x = -runDirection.x;
    }
}
