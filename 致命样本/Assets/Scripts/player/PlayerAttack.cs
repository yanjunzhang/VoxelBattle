using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    public GameObject _bullet;
    public GameObject _emptyBullet;

    public GameObject _swordPoint;
    public GameObject _gunPoint;
    public Transform _bulletPosition;
    public Transform _emptyBulletPos;
    public GameObject _magicPoint;
    [HideInInspector]
	public SwordMove _sword;
    string _gunName;
    float _coldTime;
    float _playerHitBack;//后坐力

    //玩家信息
    //武器状态 0-法杖 1-剑 2-枪
    public PlayerManager _playerInfo;
    //武器信息
    public List<PackageItems> _weaponInfo;
    // Use this for initialization
    void Start () {
        _playerInfo = PlayerManager.Instance;
        _weaponInfo = XML.Instance.equipsLoaded;
        RefreshWeaponName();
    }
	
	// Update is called once per frame
	void Update () {
        
        
    }
	void RefreshWeaponName(){
        List<PackageItems> _weapons = XML.Instance.equipsLoaded;
        //刷新剑的信息
        _sword = _swordPoint.GetComponentInChildren<SwordMove> ();
        if (_sword._swordName == "") {
			string _swordName = _swordPoint.transform.GetChild(0).name;
			
			for (int i = 0; i < _weapons.Count; i++)
			{
				if (_swordName == _weapons[i].name)
				{
					_coldTime = _weapons[i].coldTime;

					break;
				}
			}
			_sword._swordName = _swordName;
		}
        //刷新枪的名字，生成子弹的时候读取枪的name
        _gunName = _gunPoint.transform.GetChild(0).name;
        for (int i = 0; i < _weapons.Count; i++)
        {
            if (_gunName == _weapons[i].name)
            {
                _coldTime = _weapons[i].coldTime;
                _playerHitBack = _weapons[i].playerHitBack;
                break;
            }
        }

    }
    //剑 开始挥动为碰撞体，结束挥动为触发器，写在动作里
	void StopAttackWithSword(){
//		_sword = _swordPoint.GetComponentInChildren<swordMove> ();
		_sword.StopAct ();

	}
	void StartAttackWithSword(){
//		_sword = _swordPoint.GetComponentInChildren<swordMove> ();
		_sword.StartAct ();

	}
    void AttackWithGun()
    {
        //生成子弹
        GameObject.Instantiate(_bullet, _bulletPosition.position, transform.rotation).AddComponent<BulletMove>()._gunName = _gunName;
        //生成弹壳
        Instantiate(_emptyBullet, _emptyBulletPos.position, transform.rotation);
        StartCoroutine(HitBack(_playerHitBack));
    }
    IEnumerator HitBack(float range)
    {
        for (int i = 0; i < 5; i++)
        {
            transform.position = Vector3.Slerp(transform.position, transform.position - transform.forward * range, 0.2f);
            yield return null;
        }
        
    }
}
