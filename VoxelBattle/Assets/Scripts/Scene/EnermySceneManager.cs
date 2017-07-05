using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermySceneManager : MonoBehaviour
{
	//调用敌人预制体
	public GameObject Enermy1_Slime;
	public GameObject Enermy2_Turtle;

	private GameObject CurrentEnermy;

	//调用生成地点
	public Transform BornPosition1;
	public Transform BornPosition2;
	public Transform BornPosition3;
	public Transform BornPosition4;
	public Transform BornPosition5;
	public Transform BornPosition6;
	public Transform BornPosition7;
	public Transform BornPosition8;

	private Transform BornPosition;
	//声明地点随机变量
	private int PositionNum = 0;

	//声明准备时间
	public float ReadyTime = 0;

	//声明准备计时器
	private float ReadyTimer = 10f;
	private bool StartReadyTimeCount= false;

	//声明当前波总时间
	public float CurrentRoundTime =20f;

	//声明当前波计时器
	public float CurrentRoundTimer = 0;
	private bool StartRoundTimeCount = false;

	//声明敌人数量
	private int EnemyNums = 0;
	//限定当前波最大敌人总数量
	public const int EnemyMaxNum = 40;

	//声明波数
	public int turns = 0;
	//限定总波数
	public const int MaxTurn = 5;

	//声明敌人链表，用来记录敌人数量
	public ArrayList EnermyList = new ArrayList ();
	//限定当前最大敌人存量
	public int EnemyCurrentMaxNum = 8;

	// Use this for initialization
	void Start ()
	{
		//开始协程
		StartCoroutine (SumRounds ());
	}

	// Update is called once per frame
	void Update ()
	{
		//计时当前波
		if (StartReadyTimeCount == true) {
			ReadyTimer -= Time.deltaTime;
		}
		//计时准备
		if (StartRoundTimeCount == true) {
			CurrentRoundTimer += Time.deltaTime;
		}
	}

	//	void OnGUI ()
	//	{
	//		GUI.color = Color.red;
	//		GUILayout.Label ("Timer : " + Timer);
	//	}

	public void CreatEnermy(string name)
	{


	}

	IEnumerator SumRounds ()
	{
		//经过等待后开始运行
		yield return new WaitForSeconds (ReadyTime);
		StartRoundTimeCount = true;
		//如果波数小于最大波数，开启当前波生成怪物的协程
		while (turns < MaxTurn/*波数*/) {

			if(turns==0)
			{
				CurrentEnermy = Enermy1_Slime;

			}
			if(turns==1)
			{
				CurrentEnermy = Enermy2_Turtle;

			}

			if(StartRoundTimeCount==true)
			{
			yield return StartCoroutine (Rounds ());
			}

			if(StartRoundTimeCount==false)
			{
			turns++;
				StartReadyTimeCount = true;
			}
		}

	}


	IEnumerator Rounds ()
	{
		//Debug.Log ("Turn: " + turns);
		//循环条件：敌人数量小于当前波最大敌人总数量 并且 当前敌人数量小于当前波最大敌人存量
		while (EnemyNums < EnemyMaxNum && EnermyList.Count <= EnemyCurrentMaxNum && CurrentRoundTimer < CurrentRoundTime) {

			//随机生成地点
			PositionNum = Random.Range (1, 9);
			if (PositionNum == 1) {
				BornPosition = BornPosition1;
			}
			if (PositionNum == 2) {
				BornPosition = BornPosition2;
			}
			if (PositionNum == 3) {
				BornPosition = BornPosition3;
			}
			if (PositionNum == 4) {
				BornPosition = BornPosition4;
			}
			if (PositionNum == 5) {
				BornPosition = BornPosition5;
			}
			if (PositionNum == 6) {
				BornPosition = BornPosition6;
			}
			if (PositionNum == 7) {
				BornPosition = BornPosition7;
			}
			if (PositionNum == 8) {
				BornPosition = BornPosition8;
			}

			//实例化敌人
			GameObject enermy = Instantiate 
				(
					CurrentEnermy
				)as GameObject;

			//把每一个生成的敌人放到链表中进行记录
			EnermyList.Add (enermy);
			//设置敌人位置
			enermy.transform.position = BornPosition.position;
			EnemyNums++;
		}

		if (CurrentRoundTimer >= CurrentRoundTime&&StartReadyTimeCount==false) {
			StartReadyTimeCount = true;
			StartRoundTimeCount = false;
			//Debug.Log ("Next Ready");
		}

		if (ReadyTimer >= ReadyTime&&StartRoundTimeCount==false) {
			StartReadyTimeCount = false;
			StartRoundTimeCount = true;
			//Debug.Log ("Next Round");
		}
		yield return null;

	}


}
