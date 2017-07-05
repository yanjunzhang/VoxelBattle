using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAsyncController : MonoBehaviour {
    public UISlider _UISlider;
    AsyncOperation _async;
	float displayProgress=0f;
	float toProgress;
	// Use this for initialization
	void Start () {
        StartCoroutine(LoadAsync(LoadInformation._sceneName));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator LoadAsync(string sceneName)
    {
        _async = SceneManager.LoadSceneAsync(LoadInformation._sceneName);
        _async.allowSceneActivation = false;
        while (_async.progress<0.9f)
        {
			toProgress = _async.progress;
			while (displayProgress<=toProgress) {
				displayProgress += 0.01f;
				_UISlider.value = displayProgress;
				yield return new WaitForEndOfFrame ();
			}

        }
		toProgress = 1f;
		while (displayProgress<=toProgress) {
			displayProgress += 0.01f;
			_UISlider.value = displayProgress;
			yield return new WaitForEndOfFrame ();
		}

        _async.allowSceneActivation = true;
    }
}
