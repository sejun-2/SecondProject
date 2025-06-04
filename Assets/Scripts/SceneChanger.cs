using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // <씬 전환>
    // 프로젝트에 포함된 다른 씬을 로딩하고 기존의 씬의 내용을 삭제함
    public void ChageScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


    // <씬 추가>
    // 프로젝트에 포함된 다른 씬을 로딩하고 기존의 씬의 내용을 유지함
    public void AddScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }


    //// <비동기 씬 로드>
    //// 씬 로딩을 백그라운드로 진행하여 게임 중 멈춤이 없도록하는 비동기 방법
    //public void ChangeSceneASync()
    //{
    //    AsyncOperation operation = SceneManager.LoadSceneAsync("StageScene2", LoadSceneMode.Additive);

    //    operation.allowSceneActivation = true;      // 씬 로딩 완료시 바로 씬 전환을 진행하는지 여부
    //    bool isLoaded = operation.isDone;           // 씬 로딩의 완료여부 확인
    //    float progress = operation.progress;        // 씬 로딩의 진행률 확인
    //    operation.completed += (oper) => { };       // 씬 로딩의 완료시 진행할 이벤트 추가
    //}


    //// <씬 언로드>
    //// 로딩되어 있는 씬을 언로드 시키는 방법
    //public void UnloadScene()
    //{
    //    Scene scene = SceneManager.GetSceneByName("StageScene2");
    //    if (scene.isLoaded == true) // 씬이 Loaded 상태일때
    //    {
    //        SceneManager.UnloadSceneAsync(scene);   // 씬 언로드 -> 닫아라. Unload 의 경우에는 반드시 Async 사용
    //    }
    //}


    //private void OnTriggerEnter(Collider other) // 씬 추가
    //{
    //    //AddScene("StageScene2");

    //    if (other.gameObject.tag != "Player")
    //        return;

    //    SceneManager.LoadSceneAsync("StageScene2", LoadSceneMode.Additive);     // 비동기 씬 로딩
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag != "Player")
    //        return;

    //    SceneManager.UnloadSceneAsync("StageScene2");
    //}



}
