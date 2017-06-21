using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    private static PlayerManager _instance;
    public static PlayerManager Instance { get { return _instance; } }

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
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        _playerWeaponNum = _equipCtrl.i % 3;
        _HP = _managerGame._HP;
        _currHP = _managerGame._currHP;
        _health = _managerGame._health;
        _currHealth = _managerGame._currHealth;
        _exp = _managerGame._exp;
	}
}
