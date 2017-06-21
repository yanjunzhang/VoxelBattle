using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordMove : MonoBehaviour {

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
	GameObject _startEffect;
	GameObject _endEffect;
    // Use this for initialization
    void Start () {
		
        
        
	}
	
	// Update is called once per frame
	void Update () {
		if (_damage==0) {
			Detect ();
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
				if (_startEffect!=""&&_endEffect!="") {
					break;
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
		gameObject.GetComponent<BoxCollider> ().isTrigger = false;
		Debug.Log ("start");
	}
	public void Stop(){
		gameObject.GetComponent<BoxCollider> ().isTrigger = true;
		Debug.Log ("stop");
	}
}
