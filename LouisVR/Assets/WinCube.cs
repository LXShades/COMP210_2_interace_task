using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCube : MonoBehaviour {

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

        transform.position = new Vector3(transform.position.x, player.transform.position.y + 0.25f + Mathf.Sin(Time.time * 2.0f) * 0.25f, transform.position.z);
	}
}
