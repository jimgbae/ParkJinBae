using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesLoader : MonoBehaviour
{
    //Stage이름을 저장할 변수
    public string StageName;

    //CanvasGroup 컴포넌트를 저장할 변수
    public CanvasGroup fadeCG;
    //Fade IN 처리시간
    [Range(0.5f,2.0f)]
    public float fadeDuration = 1.0f;
    //호출 씬과 씬로드 방식을 저장할 딕셔너리
    public Dictionary<string, LoadSceneMode> loadScenes = new Dictionary<string, LoadSceneMode>();

    void InitSceneInfo()
    {
        int Number = UIManager.instance.StageNumber;
        switch (Number)
        {
            case 9:
                StageName = "TestMap";
                break;
            case 1:
                StageName = "Level1";
                break;
            case 2:
                StageName = "Level2";
                break;
            case 3:
                StageName = "Level3";
                break;
        }
        loadScenes.Add(StageName, LoadSceneMode.Additive);
        loadScenes.Add("Play", LoadSceneMode.Additive);
    }

    //코루틴
    IEnumerator Start()
    {
        InitSceneInfo();

        //처음 알파값 설정
        fadeCG.alpha = 1.0f;

        //여러 개의 씬을 코루틴으로 호출
        foreach(var _loadScene in loadScenes)
        {
            yield return StartCoroutine(LoadScene(_loadScene.Key, _loadScene.Value));
        }

        //Fade In 함수 호출
        StartCoroutine(Fade(0.0f));
    }

    IEnumerator LoadScene(string sceneName, LoadSceneMode mode)
    {
        yield return SceneManager.LoadSceneAsync(sceneName, mode);

        Scene loadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(loadedScene);
    }

    IEnumerator Fade(float finalAlpha)
    {
        //라이트맵이 깨지는것을 방지하기 위해 스테이지 씬 활성화
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(StageName));
        fadeCG.blocksRaycasts = true;

        //절대값 함수로 백분율 계산
        float fadeSpeed = Mathf.Abs(fadeCG.alpha - finalAlpha) / fadeDuration;

        //알파값 조정
        while(!Mathf.Approximately(fadeCG.alpha, finalAlpha))
        {
            fadeCG.alpha = Mathf.MoveTowards(fadeCG.alpha, finalAlpha, fadeSpeed * Time.deltaTime);
            yield return null;
        }

        fadeCG.blocksRaycasts = false;

        SceneManager.UnloadSceneAsync("SceneLoader");
    }
}
