using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioClip))]
public class PlayerSounds : MonoBehaviour {
    AudioSource audio;
    AudioSource secondaryAudio;

    public float minSoundDelay = 1.0f;
    public float maxSoundDelay = 3.0f;
    private float nextSoundDelay = 0.0f;
    private float lastSoundPlayedTime = 0.0f;
    int numBreaths = 0;

    public AudioClip[] groanSounds;
    public AudioClip[] brainsSounds;
    public AudioClip[] breathSounds;
    public AudioClip[] pickupSounds;
    public AudioClip[] nomSounds;

    // Use this for initialization
    void Start () {
        audio = GetComponents<AudioSource>()[0];
        secondaryAudio = GetComponents<AudioSource>()[1];
    }
	
	// Update is called once per frame
	void Update () {
		// Continuously play zombie sounds
        if (!audio.isPlaying && Time.time >= lastSoundPlayedTime + nextSoundDelay)
        {
            // Are we playing a breath, groan or braiiiins sound?
            if (numBreaths <= 0)
            {
                if (Random.value >= 0.6f)
                {
                    audio.clip = brainsSounds[Random.Range(0, brainsSounds.Length)];
                }
                else
                {
                    audio.clip = groanSounds[Random.Range(0, groanSounds.Length)];
                }

                audio.volume = Random.Range(0.7f, 1.0f);
                numBreaths = Random.Range(0, 2);
            }
            else
            {
                // Breathe
                audio.clip = breathSounds[Random.Range(0, breathSounds.Length)];
                numBreaths--;

                audio.volume = Random.Range(0.2f, 0.4f);
            }

            // Play at a slightly random pitch and volume
            audio.pitch = Random.Range(0.97f, 1.03f);
            audio.Play();

            // Delay the next sound
            //nextSoundDelay = Random.Range(minSoundDelay, maxSoundDelay);
            lastSoundPlayedTime = Time.time + audio.clip.length;
        }
	}

    public void PlayHumanGrabSound()
    {
        // Play the sound on the secondary audio source
        secondaryAudio.Stop();

        secondaryAudio.clip = pickupSounds[Random.Range(0, pickupSounds.Length)];
        secondaryAudio.volume = Random.Range(0.7f, 1.0f);
        secondaryAudio.pitch = Random.Range(0.97f, 1.03f);
        secondaryAudio.Play();

        lastSoundPlayedTime = Mathf.Max(lastSoundPlayedTime, Time.time + secondaryAudio.clip.length);
    }

    public void PlayBiteSound()
    {
        secondaryAudio.Stop();

        secondaryAudio.clip = nomSounds[Random.Range(0, nomSounds.Length)];
        secondaryAudio.volume = Random.Range(0.7f, 1.0f);
        secondaryAudio.pitch = Random.Range(0.97f, 1.03f);
        secondaryAudio.Play();

        lastSoundPlayedTime = Mathf.Max(lastSoundPlayedTime, Time.time + secondaryAudio.clip.length);
    }
}
