using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
    //玩家初始信息
    public float _exp = 0;
    public float _HP = 100;
    public float _health = 100;

    public UISlider _normalVolume;
    public UISlider _backgroundVolume;
    public GameObject continueLabel;
    public UILabel playerName;


    string path;
    
	// Use this for initialization
	void Start () {
        //path = Application.dataPath + "/User.txt";
        path = LoadInformation._path;

        _normalVolume.value= ES2.Load<float>(path + "?tag=normalVolume");
        _backgroundVolume.value= ES2.Load<float>(path + "?tag=backgroundVolume");
    }
	
	// Update is called once per frame
	void Update () {
        try
        {
            if (ES2.Load<string>(path + "?tag=name") == null)
            {
                continueLabel.SetActive(false);
            }
            else
                continueLabel.SetActive(true);
        }
        catch 
        {

            return;
        }
    }
    public void SavePlayerNmae()
    {
        if (playerName.text!="")
        {
            ES2.Save(playerName.text, path + "?tag=name");
        }
        
    }
    public void LoadNewGame()
    {
        //新建游戏，玩家初始信息
        LoadInformation._sceneName = "Map_Army";
        ES2.Save("Map_Army", path + "?tag=round");
        ES2.Save(_exp, path + "?tag=exp");
        ES2.Save(_HP, path + "?tag=HP");
        ES2.Save(_HP, path + "?tag=currHP");
        ES2.Save(_health, path + "?tag=health");
        ES2.Save(_health, path + "?tag=currHealth");
        SceneManager.LoadScene("LoadScene");
    }
    public void LoadGame()
    {
        
        LoadInformation._sceneName = ES2.Load<string>(path + "?tag=round");
        SceneManager.LoadScene("LoadScene");
    }
    public void SetVolume()
    {
        ES2.Save(_normalVolume.value,path + "?tag=normalVolume");
        ES2.Save(_backgroundVolume.value, path + "?tag=backgroundVolume");
        
    }
}
