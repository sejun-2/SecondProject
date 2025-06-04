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
        // 게임 멈춤
        Time.timeScale = 0f;
        // 결과 화면 표시
        resultPanel.SetActive(false);
    }
}
