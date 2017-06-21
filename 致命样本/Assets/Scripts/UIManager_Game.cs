using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager_Game : MonoBehaviour {
    public UILabel _playerName;
    public UILabel _playerHP;
    public UILabel _playerHealth;
    public UILabel _playerLevel;
    public UISlider _HPSlider;
    public UISlider _healthSlider;

    [HideInInspector]
    public float _HP;
    public float _currHP;
    public float _health;
    public float _currHealth;
    public float _exp;

    //武器图片
    public GameObject weaponManager;

    UIPlayTween[] _weapons;
    string path;
    // Use this for initialization
    private void Awake()
    {
        path = LoadInformation._path;
        _HP = ES2.Load<float>(path + "?tag=HP");
        _currHP = ES2.Load<float>(path + "?tag=currHP");
        _health= ES2.Load<float>(path + "?tag=health");
        _currHealth= ES2.Load<float>(path + "?tag=currHealth");
        _weapons = weaponManager.GetComponentsInChildren<UIPlayTween>();
        _exp= ES2.Load<float>(path + "?tag=exp");
    }
    void Start () {
        //path = Application.dataPath + "/User.txt";
        
        LoadPlayer();
        
    }
	
	// Update is called once per frame
	void Update () {
        
    }
    public void LoadPlayer()
    {
        _playerName.text= ES2.Load<string>(path + "?tag=name");
        _HPSlider.value = _currHP / _HP;
        _playerHP.text = _currHP + "/" + _HP;
        _healthSlider.value = _currHealth / _health;
        _playerHealth.text = _currHealth + "/" + _health;

    }
    public void ChangeWeapon(int j)
    {
        for (int i = 0; i < _weapons.Length; i++)
        {
            if (i==j)
            {
                _weapons[i].Play(true);
            }else
                _weapons[i].Play(false);
        }
       
    }
    
}
