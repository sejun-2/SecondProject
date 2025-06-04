using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject resultPanel;

    void Awake()
    {
        Instance = this;
        resultPanel.SetActive(true);
    }

    public void EndGame()
    {
        // ���� ����
        Time.timeScale = 0f;
        // ��� ȭ�� ǥ��
        resultPanel.SetActive(false);
    }
}
