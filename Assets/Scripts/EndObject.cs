using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndObject : MonoBehaviour
{
    public Timer timer;

    // 플레이어가 EndObject에 들어왔을 때 실행
    void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어 태그를 사용해 구분
        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어가 EndObject에 들어왔습니다!");
            timer.StopTimer();
            // 타이머를 멈추고 결과 화면을 표시하는 로직을 추가합니다.
            //GameManager.Instance.EndGame();
        }
    }
}
