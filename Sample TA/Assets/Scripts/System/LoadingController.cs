using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingController : MonoBehaviour {

    public static LoadingController 
        loadingController;

    public GameObject 
        loadingBarHolder;

    public Slider 
        loadingBarProgress;

    private void Awake() {
        if (loadingController == null) {
            loadingController = this;
        } else if (loadingController != this) {
            Destroy(gameObject);
        }
    }

    void Start() {

    }

    void Update() {

    }

    public void ButtonLoadSceneFunction(int sceneIndex) {
        StartCoroutine(LoadSceneProgress(sceneIndex));
    }

    IEnumerator LoadSceneProgress(int sceneIndex) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingBarHolder.SetActive(true);

        while (!operation.isDone) {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            loadingBarProgress.value = progress;

            yield return null;
        }
    }
}
