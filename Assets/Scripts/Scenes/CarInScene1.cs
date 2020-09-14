using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInScene1 : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField]
    AudioClip audioStartEngine;
    [SerializeField]
    AudioClip audioIdle;
    [SerializeField]
    AudioClip audioRun;
    

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            throw new NullReferenceException();
    }

    void Update()
    {
        
    }

    public void PlayStartEngine()
    {
        audioSource.PlayOneShot(audioStartEngine);
    }

    public void PlayIdleEngine()
    {
        audioSource.clip = audioIdle;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayRun()
    {
        audioSource.PlayOneShot(audioRun);
    }

}
