using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//枚举敌人状态
public enum EnermyState
{
	Walk,
	Idle,
	Attack,
	Die,
	Dead
}

public class EnermyMove : MonoBehaviour
{

	public Animation ani;
	//调用玩家位置作为行进目标
	public Transform Target;
	//声明与玩家距离
	private float distance;

	//调用场景管理
	public EnermySceneManager enermySceneManager;

	//声明寻路组件
	NavMeshAgent enermyNav;

	//声明子弹射中次数
	private float BulletHitTime;

	//声明敌人属性
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

	//声明行动时间
	public float MovementTimer = 0f;

	//声明死亡时间
	private float DieTimer = 0f;

	public float TimeForWalk=8f;

	public float TimeFromWalkToIdle=11f;

	public float DetectRange = 15f;

	public float AttackRange = 2.5f;

	public float DieTime = 5f;
	public float dead = 3f;

	public EnemyCtrl enemyCtrl;

	public AudioClip SlimeDieAudio;
	public AudioClip TurtleDieAudio;

	public AudioClip CurrentEnemyDieAudio;

	NavMeshPath navPath;

	void Awake()
	{
		enermyNav = GetComponent<NavMeshAgent> ();
		ani =GetComponent<Animation> ();
		enemyCtrl = GetComponent<EnemyCtrl> ();



	}
	// Use this for initialization
	void Start ()
	{
		localEnermyState = EnermyState.Idle;

		Target = GameObject .Find ("Player").transform;

		distance = Vector3.Distance (Target.position, transform.position);


		//		Debug.Log ("Dis : " + distance);

		enermySceneManager = GameObject .Find ("EnermySceneManager").transform.GetComponent <EnermySceneManager>();

		if(enermySceneManager.turns==0)
		{
			CurrentEnemyDieAudio = SlimeDieAudio;

		}
		if(enermySceneManager.turns==1)
		{
			CurrentEnemyDieAudio = TurtleDieAudio;

		}

		StartCoroutine (EnermyMovment ());


	}

	// Update is called once per frame
	void Update ()
	{
		//Debug.Log("MovementTimer:"+MovementTimer);
		distance = Vector3.Distance (Target.position, transform.position);
		//Debug.Log ("distance" + distance);
	}

	void OnGUI()
	{
		GUILayout.Label ("EnemyState : " + localEnermyState);
		GUILayout.Label ("Dis : " + distance);
	}

	IEnumerator EnermyMovment ()
	{
		//循环条件：敌人未死亡
		while (localEnermyState != EnermyState.Dead) {

			//Walk
			if (localEnermyState == EnermyState.Walk ) {
				//enermyAnimation.MovingState = EnermyMovingState.Idle;
				if(enermyNav)
				enermyNav.SetDestination (Target.position);

				//ani.Play ("run");
//				Debug.Log ("Walk");
			}

			//Idle
			if (localEnermyState == EnermyState.Idle) {
				//enermyAnimation.MovingState = EnermyMovingState.Idle;
				if(enermyNav)
				enermyNav.SetDestination (transform.position);
//				Debug.Log ("Idle");
				//ani.Play ("idle");
			}

			//Attack
			if (localEnermyState == EnermyState.Attack) {
				//enermyAnimation.MovingState = EnermyMovingState.Idle;
//				Debug.Log ("Attack");
				//ani.Play ("attack2");
			}

			//Die
			if (localEnermyState == EnermyState.Die) {
				//enermyAnimation.MovingState = EnermyMovingState.Idle;
				AudioSource.PlayClipAtPoint (CurrentEnemyDieAudio, transform.position);
				if(enermyNav)
				enermyNav.velocity = new Vector3 (0, 0, 0);
//				Debug.Log ("Die");
				//ani.Play ("die");
			}

			yield return TimeCount (); 
		}
		if (localEnermyState == EnermyState.Dead) {
			//enermyAnimation.MovingState = EnermyMovingState.Idle;
			Debug.Log ("Dead");
			enermySceneManager.EnermyList.Remove (this.gameObject);
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
		if (MovementTimer <= TimeForWalk) {
			localEnermyState = EnermyState.Walk;

		}

		//Idle
		else if (MovementTimer > TimeForWalk && MovementTimer <= TimeFromWalkToIdle && distance > AttackRange) {
			localEnermyState = EnermyState.Idle;

		}

		//Reset
		else if (MovementTimer > TimeFromWalkToIdle) {
			MovementTimer = 0;
		} 

		//Attack
		if (distance <= AttackRange) {
			localEnermyState = EnermyState.Attack;

		}

		//Die
		if (enemyCtrl.HP<=0) {
			localEnermyState = EnermyState.Die;
			DieTimer += Time.deltaTime;

		}
		if (localEnermyState == EnermyState.Die && DieTimer >= dead) {
			localEnermyState = EnermyState.Dead;

			yield return 0; 
		}
	}

	//检测子弹碰撞
//	void OnTriggerEnter (Collider   collision)
//	{
//		if (collision.gameObject.tag == "PlayerColdSteel") {
//			//	        AudioSource.PlayClipAtPoint (PlayerBeHacked,collision.gameObject.transform.localPosition );  
//			//			transform.GetChild (0).transform.position  = collision.transform.position ;
//			//			transform.GetChild (0).gameObject.SetActive (true);
//			BulletHitTime++;
//			Debug.Log ("BeHitByColdSteel");
//
//			if (collision.gameObject.tag == "PlayerBullet") {
//				//	        AudioSource.PlayClipAtPoint (PlayerBeHacked,collision.gameObject.transform.localPosition );  
//				//			transform.GetChild (0).transform.position  = collision.transform.position ;
//				//			transform.GetChild (0).gameObject.SetActive (true);
//				BulletHitTime++;
//				Debug.Log ("BeHitByBullet");
//			}
//
//		}
//	}
}
