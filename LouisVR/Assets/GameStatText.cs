using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatText : MonoBehaviour {
    Player player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.up = Vector3.up;
        transform.forward = -(player.transform.position - transform.position);

        GetComponent<TextMesh>().text = "Humans left: " + GameMode.numHumans;
	}
}
