using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceSound : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip buttons;

    public void PlayAudioButton(){
        audioSource.PlayOneShot(buttons);
    }
    
}
