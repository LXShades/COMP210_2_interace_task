using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCube : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var player = GameObject.FindGameObjectWithTag("Player");

        if (player)
        {
            transform.forward = player.transform.position - transform.position;
        }

        transform.position = new Vector3(transform.position.x, 2.0f + Mathf.Sin(Time.time), transform.position.z);
	}
}
