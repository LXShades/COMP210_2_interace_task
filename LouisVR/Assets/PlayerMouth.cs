using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouth : MonoBehaviour {
    public PlayerSounds playerSounds;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider collider)
    {
        // Check if we can bite this
        Human human = collider.gameObject.GetComponent<Human>();
        if (human)
        {
            // Are we holding it? (rough test)
            if (human.transform.parent != null && !human.isZombie)
            {
                // Tag, you're a zombie!
                human.isZombie = true;

                // Play the sound if we have a PlayerSounds reference
                if (playerSounds)
                {
                    playerSounds.PlayBiteSound();
                }
            }
        }
    }
}
