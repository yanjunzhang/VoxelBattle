using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnermyState
{
	Walk,
	Idle,
	Attack,
	Die,
	Dead
}

public class EnermyPathFinding : MonoBehaviour
{

	public Transform Player;
	NavMeshAgent enermyNav;

	private float BulletHitTime;

	private float distance;

	public EnermyState enermyState {
		get{ return localEnermyState; }
		set {
//			if (value != localEnermyState) 
//			{
			//	currentTime = passedTime ;
//			}
			localEnermyState = value;
		}
	}

	private EnermyState localEnermyState;

	public float MovementTimer = 0f;
	public float DieTimer = 0f;

	// Use this for initialization
	void Start ()
	{
		enermyNav = GetComponent<NavMeshAgent> ();
		StartCoroutine (EnermyMove ());
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Debug.Log("MovementTimer:"+MovementTimer);
		distance = Vector3.Distance (Player.position, this.transform.position);
		//Debug.Log ("distance" + distance);
	}

	IEnumerator EnermyMove ()
	{
		while (localEnermyState != EnermyState.Dead) {

			//Walk
			if (localEnermyState == EnermyState.Walk && distance > 12) {
				enermyNav.SetDestination (Player.position);
				Debug.Log ("Walk");
			}

			//Idle
			if (localEnermyState == EnermyState.Idle) {
				//enermyAnimation.MovingState = EnermyMovingState.Idle;
				enermyNav.SetDestination (this.transform.position);
				Debug.Log ("Idle");
			}

			//Attack
			if (localEnermyState == EnermyState.Attack) {
				//enermyAnimation.MovingState = EnermyMovingState.Idle;
				Debug.Log ("Attack");
			}

			//Die
			if (localEnermyState == EnermyState.Die) {
				//enermyAnimation.MovingState = EnermyMovingState.Idle;
				enermyNav.velocity = new Vector3 (0, 0, 0);
				Debug.Log ("Die");
			}

			yield return TimeCount (); 
		}
		if (localEnermyState == EnermyState.Dead) {
			//enermyAnimation.MovingState = EnermyMovingState.Idle;
			Debug.Log ("Dead");
			Destroy (this.gameObject);
			yield return 0;
		}
	}

	IEnumerator TimeCount ()
	{  
		if (localEnermyState != EnermyState.Dead) {
			MovementTimer += Time.deltaTime;
		}

		//Walk
		if (MovementTimer <= 8f) {
			localEnermyState = EnermyState.Walk;
		}

		//Idle
		else if (MovementTimer > 8f && MovementTimer <= 14f) {
			localEnermyState = EnermyState.Idle;
		}

		//Reset
		else if (MovementTimer > 14f) {
			MovementTimer = 0;
		} 

		//Attack
		if (distance <= 12f) {
			localEnermyState = EnermyState.Attack;
		}

		//Die
		if (BulletHitTime >= 5) {
			localEnermyState = EnermyState.Die;
			DieTimer += Time.deltaTime;
		}
		if (localEnermyState == EnermyState.Die && DieTimer >= 3f) {
			localEnermyState = EnermyState.Dead;

			yield return 0; 
		}
	}

	void OnTriggerEnter (Collider   collision)
	{
		if (collision.gameObject.tag == "PlayerBullet") {
//	        AudioSource.PlayClipAtPoint (PlayerBeHacked,collision.gameObject.transform.localPosition );  
//			transform.GetChild (0).transform.position  = collision.transform.position ;
//			transform.GetChild (0).gameObject.SetActive (true);
			BulletHitTime++;
			Debug.Log ("BeHit");
		}
	}
}
