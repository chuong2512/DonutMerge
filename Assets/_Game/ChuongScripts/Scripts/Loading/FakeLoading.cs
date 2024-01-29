using System.Collections;
using ChuongCustom;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FakeLoading : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _loadingTMP;
    
    public void Start()
    {
        StartCoroutine(StartLoad());
    }

    IEnumerator StartLoad()
    {
        StartCoroutine(FadeLoadingScreen(1));
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneLoader.Instance.sceneName);
        while (!operation.isDone)
        {
            yield return null;
        }
    }

    IEnumerator FadeLoadingScreen(float duration)
    {
        float time = 0;

        var dot = 0;
        var dotString = ".";

        while (time < duration)
        {
            dot++;
            switch (dot % 3)
            {
                case 0:
                    dotString = ".";
                    break;
                case 1:
                    dotString = "..";
                    break;
                case 2:
                    dotString = "...";
                    break;
            }

            time += Time.deltaTime;
            _loadingTMP.SetText($"Loading{dotString}");
            yield return new WaitForSeconds(0.5f);
        }
    }
}