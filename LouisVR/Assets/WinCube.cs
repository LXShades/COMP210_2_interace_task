using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCube : MonoBehaviour {
	float baseHeight = 0.0f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        if (player)
        {
            transform.forward = player.head.position - transform.position;
        }

		if (baseHeight < player.head.position.y + 0.25f)
		{
			baseHeight = player.head.position.y + 0.25f;
		}

        transform.position = new Vector3(transform.position.x, baseHeight + 0.25f + Mathf.Sin(Time.time * 2.0f) * 0.25f, transform.position.z);
	}
}
