using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{

    public AudioClip soundClip;
    AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        audio.PlayOneShot(soundClip, 0.7F);
    }

}
