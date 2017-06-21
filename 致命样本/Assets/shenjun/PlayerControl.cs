﻿/*
 * Author : shenjun
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    //武器切换 动作相关
    EquipmengtCtrl _weaponCtrl;

    //操控相关逻辑
    public Transform camTrans;
	public float moveSpeed=1;
    LayerMask layer;
    Camera cam;
    Ray ray;
    RaycastHit hit;

	[HideInInspector]


    private void Awake()
    {
        _weaponCtrl = GetComponent<EquipmengtCtrl>();
        cam = Camera.main;
        //layer = LayerMask.GetMask("Terrain");
    }

    void Start () {
		
	}

    //    private void OnGUI()
    //    {
    //        GUILayout.Label("MousePos : " + Input.mousePosition);
    //        GUILayout.Label("HitPos : " + hit.point);
    //    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _weaponCtrl.AttackAniHit(false);//单击释放
            if (Input.GetMouseButton(0))
                _weaponCtrl.AttackAniHit(true);
        }
            
        
        if (Input.GetMouseButtonUp(0))
            _weaponCtrl._animator.SetBool("IsContinue", false);
    }
    void FixedUpdate () {

        ray = cam.ScreenPointToRay(Input.mousePosition);
        
        if(Physics.Raycast(ray, out hit, 1000))
        {

            Debug.DrawLine(ray.origin, hit.point, Color.red);


            Vector3 target = hit.point;
            Vector3 dir = target - transform.position;
            dir.y = 0;
            transform.rotation = Quaternion.LookRotation(dir);
            Debug.DrawLine(transform.position, target, Color.red);
        }

        Move();

    }

    void Move()
    {
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

		if (x == 0 && z == 0) {
            _weaponCtrl.PlayerIdle();
			return;
		}
        _weaponCtrl.PlayerMove();
        Transform child = camTrans.GetComponent<CameraControl>().child;

        Vector3 forward = child.rotation * Vector3.forward;
        forward.y = 0;
        forward *= z;

        Vector3 right = child.rotation * Vector3.right;
        right.y = 0;
        right *= x;

		transform.position += forward*moveSpeed;
		transform.position += right*moveSpeed;

    }
	
}
