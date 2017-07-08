using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {
	AudioSource _audio;
	public float range =8;
	float searchRange=15f;
	NavMeshAgent agent;
	Animator ani;
	Transform player;
	EnemyCtrl enemy;
	bool findPlayer;
	// Use this for initialization
	void Start () {
		_audio = GetComponent<AudioSource> ();
		agent = GetComponent <NavMeshAgent> ();
		ani = GetComponent <Animator> ();
		enemy = GetComponent <EnemyCtrl> ();
		player = GameObject.FindWithTag ("PlayerBody").transform;
		InvokeRepeating ("EnemyMove",0.5f,10f);
		RandomEnemy ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (transform.position,player.position)<searchRange) {
			StopAllCoroutines ();
			ani.SetBool ("isWalk",true);
			agent.SetDestination (player.position);
			if (!findPlayer) {
				InvokeRepeating ("PlayAudio",1f,2f);
			}
			findPlayer = true;
			CancelInvoke ("EnemyMove");
		}
		if (findPlayer) {
			agent.SetDestination (player.position);
		}
	}
	void PlayAudio(){
		//_audio.Play ();
		//AudioSource.PlayClipAtPoint (_audio,transform.position);
	}
	void EnemyMove(){
		if (Vector3.Distance (transform.position,player.position)>=searchRange) {
			StopAllCoroutines ();
			StartCoroutine (Partal ());
		}

	}
	IEnumerator FollowPlayer(){
		ani.SetBool ("isWalk",true);
		while (true) {
			agent.SetDestination (player.position);
			yield return 0;
		}
	}
	IEnumerator Partal(){
		ani.SetBool ("isWalk",true);
		while (true) {
			Vector3 tmpPos = transform.position + new Vector3 (Random.Range (-range,range),0, Random.Range (-range,range));
			agent.SetDestination (tmpPos);
			Debug.DrawLine (transform.position,tmpPos);
			if (!agent.hasPath) {
				break;
			}
			PlayAudio ();
			yield return new WaitForSeconds (3f);
		}
		ani.SetBool ("isWalk",false);
	}
	void RandomEnemy(){
		float i = Random.value;
		enemy.HP += enemy.HP * i;
		enemy.damage += enemy.damage * i;
		enemy.exp += enemy.exp * i;
		agent.speed += agent.speed * i;
		searchRange += searchRange * i;
	}
}
