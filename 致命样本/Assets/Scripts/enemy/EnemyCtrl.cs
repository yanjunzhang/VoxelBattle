using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : MonoBehaviour {
    public float HP = 100;
    [HideInInspector]
    public SwordMove _sword;
    public BulletMove _bullet;
    Vector3 _dir;//子弹射入方向
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        BulletMove _bullet = other.transform.GetComponent<BulletMove>();
        if (_bullet!=null&& other.gameObject.tag=="Player")
        {
            HP -= _bullet._damage;
            StartCoroutine(HitBack(_bullet._hitback));
            _dir = other.transform.forward;
            
            //Destroy(other.gameObject);
            
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
            
            
        }
        if (_sword!=null && other.gameObject.tag == "Player")
        {
            HP -= _sword._damage;
            _dir = new Vector3((transform.position - other.transform.position).x, 0, (transform.position - other.transform.position).z).normalized;
            StartCoroutine(HitBack(_sword._hitback));
            _sword.GetComponent<BoxCollider>().isTrigger = true;
            
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
}
