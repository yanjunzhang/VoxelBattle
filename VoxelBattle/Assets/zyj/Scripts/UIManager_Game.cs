using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager_Game : MonoBehaviour {
    public UILabel _playerName;
    public UILabel _playerHP;
    public UILabel _playerHealth;
	public UILabel _playerExp;
    public UISlider _HPSlider;
    public UISlider _healthSlider;
	public UISlider _expSlider;
	public GameObject menu;
	public TweenColor _tweenColor;
	public UISlider _backgroundVolume;
	public AudioClip _gameOverAudio;
    [HideInInspector]
	AudioSource _audio;
    public float _HP;
    public float _currHP;
    public float _health;
    public float _currHealth;
    public float _exp;

    //武器图片
    public GameObject weaponManager;
	public GameObject[] weapons;
	public UISprite[] _weaponSprite;
    UIPlayTween[] _weapons;
    string path;
	int level;
	int tmpLevel;
    // Use this for initialization
    private void Awake()
    {
		

		path = LoadInformation._path;
		/*
        _HP = ES2.Load<float>(path + "?tag=HP");
        _currHP = ES2.Load<float>(path + "?tag=currHP");
        _health= ES2.Load<float>(path + "?tag=health");
        _currHealth= ES2.Load<float>(path + "?tag=currHealth");*/
        _weapons = weaponManager.GetComponentsInChildren<UIPlayTween>();
        //_exp= ES2.Load<float>(path + "?tag=exp");
    }
    void Start () {
        //path = Application.dataPath + "/User.txt";
		_playerName.text= ES2.Load<string>(path + "?tag=name");
		level = (int)(PlayerManager.Instance._exp / 100);
		tmpLevel = level;
		_backgroundVolume.value = ES2.Load<float>(path + "?tag=backgroundVolume");
		_audio = GetComponent<AudioSource> ();
		_audio.volume = _backgroundVolume.value;
    }
	
	// Update is called once per frame
	void Update () {
		InitWeaponSprite ();
		_HP = PlayerManager.Instance._HP;
		_currHP = PlayerManager.Instance._currHP;
		_health = PlayerManager.Instance._health;
		_currHealth = PlayerManager.Instance._currHealth;
		_exp = PlayerManager.Instance._exp;
		LoadPlayer ();
		if (!menu.activeInHierarchy) {
			if (Input.GetKeyDown (KeyCode.Escape)) {
				Time.timeScale = 0;
				menu.SetActive (true);
			}
		}

	}

    
		
	void InitWeaponSprite(){
		for (int i = 0; i < _weaponSprite.Length; i++) {
            _weaponSprite[i].spriteName = weapons[i].transform.GetChild(0).name;
            //weapons[i].transform.childCount - 1
        }
    }
	public void BackToGame(){
		Time.timeScale = 1;
		menu.SetActive (false);
	}
	public void QuitGame(){
		Application.Quit ();
	}
	public void GameOver(){
		Time.timeScale = 0;
		_audio.clip = _gameOverAudio;
		_audio.Play ();
		//AudioSource.PlayClipAtPoint (_gameOverAudio, transform.position);
		_tweenColor.PlayForward ();
	} 
    public void LoadPlayer()
    {
        
        _HPSlider.value = _currHP / _HP;
		_playerHP.text = (int)_currHP + "/" + (int)_HP;
        _healthSlider.value = _currHealth / _health;
		_playerHealth.text = (int)_currHealth + "/" + (int)_health;
		_playerExp.text = ((int)(_exp / 100)+1).ToString ();
		_expSlider.value = _exp % 100 / 100;
		level = (int)(_exp / 100);
		if (tmpLevel!=level) {
			PlayerManager.Instance.LevelUP ();
			tmpLevel = level;
		}

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
	public void LoadMain(){
		Time.timeScale = 1;
		SceneManager.LoadScene ("MainLoad");
	}
	public void SetVolume()
	{
		ES2.Save(_backgroundVolume.value, path + "?tag=backgroundVolume");
		_audio.volume = _backgroundVolume.value;
	}
    
}
