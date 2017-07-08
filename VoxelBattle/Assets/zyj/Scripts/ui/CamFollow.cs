using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour {
	Transform target;
	// Use this for initialization
	void Start () {
		target = GameObject.FindGameObjectWithTag ("PlayerBody").transform;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (target.position.x, transform.position.y, target.position.z);
	}
}
