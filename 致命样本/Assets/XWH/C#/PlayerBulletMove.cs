using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletMove : MonoBehaviour {
    
	public float PlayerBulletSpeed = 350f;
	private Transform PlayerBulletTarget;
	public float PlayerBulletDestroyTime = 0.4f;

    [HideInInspector]
    public string Weapon;//需要传入武器名称 读取XML 来读取参数
    // Use this for initialization
    void Start () {
		PlayerBulletTarget = GameObject.Find ("Aim").transform ;
		transform.LookAt(PlayerBulletTarget);
		Destroy (gameObject, PlayerBulletDestroyTime);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.forward * PlayerBulletSpeed * Time.deltaTime);
	}
}
