using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMove : MonoBehaviour {

    public string _swordName;
    float _range;
    public float _damage;
    public float _hitback;
    public float _healthUse;
    public string _bulletEffectName;
    public string _hitEffectName;
    float _playerHitBack;
    float _bulletSpeed;
    public float _explodeRadius;
    public float _explodeDamage;
    int _canCrossWall;
    MyUIPlaySound _sound;
	GameObject _startEffect;
	GameObject _endEffect;
    BoxCollider _boxCollider;
    // Use this for initialization
    void Start () {
        //_boxCollider=this.gameObject.GetComponent
		_swordName = this.gameObject.name;
        _sound = GetComponent<MyUIPlaySound>();
    }
	
	// Update is called once per frame
	void Update () {
		if (_damage==0) {
			Detect ();
//			Debug.Log (this.gameObject.name);
		}
	}
	void Detect(){
		List<PackageItems> _weapons = XML.Instance.equipsLoaded;
		for (int i = 0; i < _weapons.Count; i++)
		{
			if (_swordName==_weapons[i].name)
			{
				_range = _weapons[i].range;
				_damage = _weapons[i].damage;
				_hitback = _weapons[i].hitback;
				_healthUse = _weapons[i].healthUse;
				_bulletEffectName = _weapons[i].bulletEffectName;
				_hitEffectName = _weapons[i].hitEffectName;
				_playerHitBack = _weapons[i].playerHitBack;
				_bulletSpeed = _weapons[i].bulletSpeed;
				_explodeRadius = _weapons[i].exploadRadius;
				_explodeDamage = _weapons[i].explodeDamage;
				_canCrossWall = _weapons[i].canCrossWall;
				break;
			}
		}

		GameObject[] effects = XML.Instance._effects;
		if (effects!=null) {
			foreach (var item in effects) {
				if (item.name==_bulletEffectName) {
					_startEffect = item;
				}else if (item.name==_hitEffectName) {
					_endEffect = item;
				}
				
			}
		}

		if (_startEffect) {
			Instantiate (_startEffect, transform.position,transform.rotation);
		}
	}
	void OnDestroy(){
		if (_endEffect) {
			Instantiate (_endEffect, transform.position,transform.rotation);
		}

	}
	public void StartAct(){
		this.gameObject.GetComponent<BoxCollider> ().isTrigger = false;
        PlaySound();
	}
	public void StopAct(){
		this.gameObject.GetComponent<BoxCollider> ().isTrigger = true;
	}
    public void PlaySound()
    {
        _sound.PlayNormal();
    }
}
