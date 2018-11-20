using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // Move with WASD
        transform.position += transform.right * (Input.GetAxis("Horizontal") * 6.0f * Time.deltaTime);
        transform.position += transform.forward * (Input.GetAxis("Vertical") * 6.0f * Time.deltaTime);
    }
}
