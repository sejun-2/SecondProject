using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // 필요시 audioSource.Play(); 로 재생
    }

    // 원하는 시점에 재생
    public void PlaySound()
    {
        audioSource.Play();
    }

}
