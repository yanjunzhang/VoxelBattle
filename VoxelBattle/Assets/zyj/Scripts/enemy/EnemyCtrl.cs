using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : MonoBehaviour {
    public float HP = 100;
	public float damage=5;
	public float exp =30;
    [HideInInspector]
    public SwordMove _sword;
    public BulletMove _bullet;
	public MagicMove _magic;

	public AudioClip SwardHit;

    Vector3 _dir;//子弹射入方向
	UseHud _hud;
	// Use this for initialization
	void Start () {
		_hud = this.GetComponent<UseHud> ();
	}

	// Update is called once per frame
	void Update () {
		if (HP<=0) {
			Destroy (this.gameObject,0.5f);
		}
	}
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("111111111");
		_magic = other.transform.GetComponent<MagicMove>();
        if (other.gameObject.tag=="Effect")
        {
			
            HP -= _magic._damage;
            StartCoroutine(HitBack(_magic._hitback));
            _dir = other.transform.forward;
            _hud.BeHit(_magic._damage);
            
        }
    }

    private void OnCollisionEnter(Collision other)
    {

        _sword = other.transform.GetComponent<SwordMove>();
        _bullet = other.transform.GetComponent<BulletMove>();
        if (_bullet != null && other.gameObject.tag == "Player")
        {
            HP -= _bullet._damage;           
            _dir = other.transform.forward;
            StartCoroutine(HitBack(_bullet._hitback));
            Destroy(other.gameObject);
			_hud.BeHit (_bullet._damage);
            
        }
        if (_sword!=null && other.gameObject.tag == "Player")
        {
            HP -= _sword._damage;
            _dir = new Vector3((transform.position - other.transform.position).x, 0, (transform.position - other.transform.position).z).normalized;
            StartCoroutine(HitBack(_sword._hitback));
            _sword.GetComponent<BoxCollider>().isTrigger = true;
			_hud.BeHit (_sword._damage);
			if (SwardHit!=null) {
				AudioSource.PlayClipAtPoint (SwardHit, transform.position);
			}

        }

		if (other.gameObject.tag=="PlayerBody") {
			PlayerManager.Instance._currHP -= damage;
		}

    }


    IEnumerator HitBack(float hitback)
    {
        for (int i = 0; i < 5; i++)
        {
            this.transform.position=Vector3.Slerp(this.transform.position, this.transform.position +_dir * hitback, 0.2f);
            yield return 0;
        }
    }
	void OnDestroy(){
		PlayerManager.Instance._exp += exp;
	}

}
