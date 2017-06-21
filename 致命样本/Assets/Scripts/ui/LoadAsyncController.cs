using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAsyncController : MonoBehaviour {
    public UISlider _UISlider;
    AsyncOperation _async;
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
        //_async.allowSceneActivation = false;
        while (_async.progress<0.9f)
        {
            _UISlider.value = _async.progress;

        }

        _UISlider.value = Mathf.Lerp(_UISlider.value, 1, 1f);
        yield return _async;
        //_async.allowSceneActivation = true;
    }
}
