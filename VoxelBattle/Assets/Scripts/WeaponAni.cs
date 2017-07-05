using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAni : MonoBehaviour {
	
	public List<PackageItems> _weapons;
	public GameObject[] _weaponPrefabs;
	public float timeInterval=5f;
	GameObject _targetWeapon;
	GameObject _weapon;
	public int _type;
	public EllipsoidParticleEmitter _effect;
	GameObject _currEff;
	[HideInInspector]

	public float speed=0.1f;
	public float distance = 0.04f;
	float i;
	// Use this for initialization
	void Start () {
		_effect = GetComponentInChildren <EllipsoidParticleEmitter> ();
		_weapons = XML.Instance.equipsLoaded;
		//读取武器type
		string _weaponName = transform.GetChild (transform.childCount - 1).name;
		_weapon = transform.GetChild (transform.childCount - 1).gameObject;
		for (int i = 0; i < _weapons.Count; i++) {
			if (_weaponName == _weapons [i].name) {
				_type = _weapons [i].type;
				break;
			}	
		}
		StartCoroutine (ChangeWeapon ());
	}

	// Update is called once per frame
	void Update () {
		i++;
		transform.position = new Vector3 (transform.position.x, transform.position.y + Mathf.Cos (i*0.05f)*distance, transform.position.z);
	}

	void OnTriggerEnter(Collider col){
		if (col.tag=="PlayerBody") {
			if (_effect!=null) {
				_effect.enabled = true;
			}

		}



	}
	void OnTriggerExit(Collider col){
		if (col.tag=="PlayerBody") {
			if (_effect!=null) {
				_effect.enabled = false;
			}
		}
	}
	void OnTriggerStay(Collider col){
		if (Input.GetKeyDown(KeyCode.F)) {

//			GameObject _newWeapon = Instantiate (col.GetComponent<PlayerAttack> ()._weapons [_type].transform.GetChild(0).gameObject, _weapon.transform.position, _weapon.transform.rotation)as GameObject;
//			_newWeapon.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
//			_newWeapon.transform.SetParent (this.transform);
//			_weapon.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
//			col.GetComponent<PlayerAttack> ().ChangeWeapon (_type, _weapon);
//			Destroy (_weapon);
//			_weapon = _newWeapon;
//			_targetWeapon = null;
			//交换武器位置
			try {
				Transform target =col.GetComponent<PlayerAttack> ()._weapons [_type].transform;
				_targetWeapon = target.GetChild (0).gameObject;
				_targetWeapon.transform.position = _weapon.transform.position;
				_targetWeapon.transform.rotation=_weapon.transform.rotation;
				_targetWeapon.transform.SetParent (this.transform);
				_weapon.transform.position = target.position;
				_weapon.transform.rotation = target.rotation;
				_weapon.transform.SetParent (target);
				_weapon.transform.localScale = new Vector3 (1f, 1f, 1f);
				_weapon.transform.SetAsFirstSibling();
				_weapon = this.transform.GetChild (transform.childCount - 1).gameObject;
				_weapon.GetComponent<BoxCollider>().isTrigger = true;
				_weapon.transform.localScale = new Vector3 (1f, 1f, 1f);
			} catch (System.Exception ex) {
				
			}
			//Debug.Log ("trigger");
            //col.GetComponent<PlayerAttack>().RefreshWeaponName();

		}

	}
	IEnumerator ChangeWeapon(){
		while (true) {
			yield return new WaitForSeconds (timeInterval);
			int i = Random.Range (0, _weaponPrefabs.Length);
			Destroy (transform.GetChild (transform.childCount-1).gameObject);
			GameObject newGO = GameObject.Instantiate (_weaponPrefabs [i], transform);
			newGO.name = _weaponPrefabs [i].name;
			newGO.transform.localPosition = Vector3.zero;
			newGO.transform.localRotation = Quaternion.identity;
			_weapon = newGO;
		}
	}
}
