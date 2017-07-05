using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyUIPlaySound : UIPlaySound {
    string path;
    public float _normalVolume;
    public float _backgroundVolume;
	// Use this for initialization
	void Start () {
        path = LoadInformation._path;
        
    }
	
	// Update is called once per frame
	void Update () {
        //音效
        _normalVolume = ES2.Load<float>(path + "?tag=normalVolume");
        _backgroundVolume = ES2.Load<float>(path + "?tag=backgroundVolume");
        volume = _normalVolume;
        if (this.tag=="Fx")
        {
            volume = _backgroundVolume;
        }
    }
    public void  PlayNormal()
    {
        NGUITools.PlaySound(audioClip, _normalVolume, pitch);
    }
    public void PlayGround()
    {
        NGUITools.PlaySound(audioClip, _backgroundVolume, pitch);
    }
    
}
