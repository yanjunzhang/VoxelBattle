﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStatus : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("DestroySelf", 5f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
