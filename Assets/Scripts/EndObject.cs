using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndObject : MonoBehaviour
{
    public Timer timer;
    public float delayBeforeLoad = 5f; // 5�� ���

    // �÷��̾ EndObject�� ������ �� ����
    void OnTriggerEnter2D(Collider2D other)
    {
        // �÷��̾� �±׸� ����� ����
        if (other.CompareTag("Player"))
        {
            Debug.Log("�÷��̾ EndObject�� ���Խ��ϴ�!");
            timer.StopTimer();

            // Ÿ�̸Ӹ� ���߰� ��� ȭ���� ǥ���ϴ� ������ �߰��մϴ�.
            StartCoroutine(LoadTitleSceneAfterDelay());
        }
    }

    IEnumerator LoadTitleSceneAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeLoad);
        SceneManager.LoadScene("TitleScene"); // �� �̸��� ������Ʈ�� �°� ����
    }
}
