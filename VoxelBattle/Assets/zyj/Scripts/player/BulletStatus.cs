using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStatus : MonoBehaviour {
	public float time=5f;
	// Use this for initialization
	void Start () {
		Invoke("DestroySelf", time);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
