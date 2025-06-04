using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // <�� ��ȯ>
    // ������Ʈ�� ���Ե� �ٸ� ���� �ε��ϰ� ������ ���� ������ ������
    public void ChageScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


    // <�� �߰�>
    // ������Ʈ�� ���Ե� �ٸ� ���� �ε��ϰ� ������ ���� ������ ������
    public void AddScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }


    //// <�񵿱� �� �ε�>
    //// �� �ε��� ��׶���� �����Ͽ� ���� �� ������ �������ϴ� �񵿱� ���
    //public void ChangeSceneASync()
    //{
    //    AsyncOperation operation = SceneManager.LoadSceneAsync("StageScene2", LoadSceneMode.Additive);

    //    operation.allowSceneActivation = true;      // �� �ε� �Ϸ�� �ٷ� �� ��ȯ�� �����ϴ��� ����
    //    bool isLoaded = operation.isDone;           // �� �ε��� �ϷῩ�� Ȯ��
    //    float progress = operation.progress;        // �� �ε��� ����� Ȯ��
    //    operation.completed += (oper) => { };       // �� �ε��� �Ϸ�� ������ �̺�Ʈ �߰�
    //}


    //// <�� ��ε�>
    //// �ε��Ǿ� �ִ� ���� ��ε� ��Ű�� ���
    //public void UnloadScene()
    //{
    //    Scene scene = SceneManager.GetSceneByName("StageScene2");
    //    if (scene.isLoaded == true) // ���� Loaded �����϶�
    //    {
    //        SceneManager.UnloadSceneAsync(scene);   // �� ��ε� -> �ݾƶ�. Unload �� ��쿡�� �ݵ�� Async ���
    //    }
    //}


    //private void OnTriggerEnter(Collider other) // �� �߰�
    //{
    //    //AddScene("StageScene2");

    //    if (other.gameObject.tag != "Player")
    //        return;

    //    SceneManager.LoadSceneAsync("StageScene2", LoadSceneMode.Additive);     // �񵿱� �� �ε�
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag != "Player")
    //        return;

    //    SceneManager.UnloadSceneAsync("StageScene2");
    //}



}
