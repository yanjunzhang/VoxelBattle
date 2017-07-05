using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    private static PlayerManager _instance;
    public static PlayerManager Instance { get { return _instance; } }
    //道具栏label
    public UILabel[] _itemLabel;
    public int[] _itemNumber;
    public UIManager_Game _managerGame;
    public EquipmengtCtrl _equipCtrl;
    
    public int _playerWeaponNum;
    public float _HP;
    public float _currHP;
    public float _health;
    public float _currHealth;
    public float _exp;

    // Use this for initialization
    private void Awake()
    {
        _instance = this;
		_itemNumber = new int[2];
    }
    void Start () {
		LoadPlayer ();
    }
	
	// Update is called once per frame
	void Update () {
        _playerWeaponNum = _equipCtrl.i % 3;
        

        //item
        for (int i = 0; i < _itemLabel.Length; i++)
        {
            _itemLabel[i].text = _itemNumber[i].ToString();
        }
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			if (_itemNumber[0]>0) {
				_currHP += 20;
				_itemNumber [0]--;
				if (_currHP>_HP) {
					_currHP = _HP;
				}
			}
		}else if (Input.GetKeyDown (KeyCode.Alpha2)) {
			if (_itemNumber[1]>0) {
				_currHealth += 20;
				_itemNumber [1]--;
				if (_currHealth>_health) {
					_currHealth = _health;
				}
			}
		}

	}
	public void LevelUP(){
		_HP += 10;
		_currHP += 10;
		_health += 10;
		_currHealth += 10;
	}

	public void LoadPlayer()
	{
		string path = LoadInformation._path;
		_HP = ES2.Load<float>(path + "?tag=HP");
		_currHP = ES2.Load<float>(path + "?tag=currHP");
		_health= ES2.Load<float>(path + "?tag=health");
		_currHealth= ES2.Load<float>(path + "?tag=currHealth");

		_exp= ES2.Load<float>(path + "?tag=exp");
	}

}
