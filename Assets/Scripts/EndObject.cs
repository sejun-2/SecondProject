using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndObject : MonoBehaviour
{
    public Timer timer;

    // �÷��̾ EndObject�� ������ �� ����
    void OnTriggerEnter2D(Collider2D other)
    {
        // �÷��̾� �±׸� ����� ����
        if (other.CompareTag("Player"))
        {
            Debug.Log("�÷��̾ EndObject�� ���Խ��ϴ�!");
            timer.StopTimer();
            // Ÿ�̸Ӹ� ���߰� ��� ȭ���� ǥ���ϴ� ������ �߰��մϴ�.
            //GameManager.Instance.EndGame();
        }
    }
}
