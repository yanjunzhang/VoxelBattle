using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissileMove : MonoBehaviour {

	public float PlayerMissileSpeed = 350f;
	public float  RotateSpeed = 10f;
	public float PlayerMissileDestroyTime = 0.4f;
	public float TimeCount = 2f;
	public float DestroyTime = 10;

	public Vector3 PlayerMissileTarget;

	public bool IsFired = false;
	private bool IsTimeStart = false;

	public DetectEnermy detectEnermy;

	// Use this for initialization
	void Start () {

		transform.GetComponentInChildren <PlayerMissileMove > ().detectEnermy = GameObject.Find ("Player").transform.GetComponentInChildren<DetectEnermy>();
		PlayerMissileTarget= detectEnermy.Target ;
		transform.LookAt(PlayerMissileTarget);
		Destroy (gameObject, PlayerMissileDestroyTime);
	}

	// Update is called once per frame
	void Update () {
		
		//Debug.Log ("PlayerMissileTarget:" + PlayerMissileTarget);
		Attack ();
		if (IsTimeStart==true) {
			TimeCount += Time.deltaTime;
		}
		if(IsFired==true)
		{
			Destroy (gameObject, DestroyTime);
		}
	}

	void Attack()
	{
		PlayerMissileTarget= detectEnermy.Target ;
		if (detectEnermy.IsFindEnermy == true ) {
			
			IsTimeStart = true;

			transform.Translate (Vector3.forward * PlayerMissileSpeed * Time.deltaTime);

			if (TimeCount > 0.5f) {
				Quaternion TargetRotation = Quaternion.LookRotation (PlayerMissileTarget - transform.position, Vector3.up);
				transform.rotation = Quaternion.Slerp (transform.rotation, TargetRotation, Time.deltaTime * RotateSpeed);
			}
			IsFired = true;
		}
	}
}
