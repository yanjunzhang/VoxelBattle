using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : MonoBehaviour {
    public float HP = 100;
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
            Debug.Log("triggerhit");
            //Destroy(other.gameObject);
            
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        BulletMove _bullet = other.transform.GetComponent<BulletMove>();
        if (_bullet != null && other.gameObject.tag == "Player")
        {
            HP -= _bullet._damage;
            StartCoroutine(HitBack(_bullet._hitback));
            _dir = other.transform.forward;
            Destroy(other.gameObject);
            
            Debug.Log("collisionhit");
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
