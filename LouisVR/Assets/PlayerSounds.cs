using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioClip))]
public class PlayerSounds : MonoBehaviour {
    AudioSource audio;

    public float minSoundDelay = 1.0f;
    public float maxSoundDelay = 3.0f;
    private float nextSoundDelay = 0.0f;
    private float lastSoundPlayedTime = 0.0f;

    public AudioClip[] groanSounds;
    public AudioClip[] brainsSounds;

	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		// Continuously play zombie sounds
        if (!audio.isPlaying && Time.time >= lastSoundPlayedTime + nextSoundDelay)
        {
            // Are we playing a groan or braiiiins sound?
            if (Random.value >= 0.6f)
            {
                audio.clip = brainsSounds[Random.Range(0, brainsSounds.Length)];
            }
            else
            {
                audio.clip = groanSounds[Random.Range(0, groanSounds.Length)];
            }

            // Play at a slightly random pitch and volume
            audio.volume = Random.Range(0.7f, 1.0f);
            audio.pitch = Random.Range(0.97f, 1.03f);
            audio.Play();

            // Delay the next sound
            nextSoundDelay = Random.Range(minSoundDelay, maxSoundDelay);
            lastSoundPlayedTime = Time.time + audio.clip.length;
        }
	}

    void PlayHumanGrabSound()
    {
        audio.Stop();
    }
}
