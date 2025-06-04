using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // �ʿ�� audioSource.Play(); �� ���
    }

    // ���ϴ� ������ ���
    public void PlaySound()
    {
        audioSource.Play();
    }

}
