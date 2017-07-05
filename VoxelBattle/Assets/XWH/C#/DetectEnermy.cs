using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnermy : MonoBehaviour {

	//判断是否发现敌人
	public bool  IsFindEnermy = false;

	//设定敌人世界坐标与相应的屏幕坐标
	public Vector3 Target;
	private Vector2 ScreenPosition;

	//设定射程
	public float MaxDistanse = 450f;

	//调用敌人在碰撞器中的临时位置
	public Transform TempTarget;
	//调用锁定目标位置（十字架）
	public Transform Lock;
	//设定十字架初始位置
	public Vector3 oripos;

	//设定敌人在碰撞器外位置
	private float distanceOut = 0;

	void Start()
	{
		//获得十字架初始位置
		 oripos = Lock.position;
	}

	void Update () 
	{  
		//如果敌人与玩家的距离大于最大射程，则未发现敌人
		distanceOut = Vector3 .Distance ( this.transform.position ,TempTarget.position );
		if(distanceOut >= MaxDistanse)
		{
			Debug.Log ("2222222222222222222222222 " + IsFindEnermy);
			IsFindEnermy = false;
		}
	}  

	//当敌人处于触发器内时
	void OnTriggerStay(Collider  collision)
	{
		//获得敌人世界、屏幕坐标
		Target = collision.gameObject.transform.position;
		ScreenPosition = Camera.main.WorldToScreenPoint (Target);

		//用十字架锁定敌人；如果敌人在屏幕外，十字架归位
		if (ScreenPosition.x > 0 && ScreenPosition.x < 1600 && ScreenPosition.y > 0 && ScreenPosition.y < 900) {
			Lock.position = ScreenPosition;
		} else {
			Lock.position = oripos;
		}

		//设定敌人与玩家的距离
		float distanceIn = Vector3 .Distance ( this.transform .position ,Target );

		//如果敌人在射程内则发现敌人
		if (collision.gameObject.tag == "Enermy" && distanceIn < MaxDistanse  )
		{
			IsFindEnermy = true;
			Debug.Log ("in");
		}
	}

	//当敌人离开触发器时
	void OnTriggerExit(Collider  collision)
	{
		//使用临时目标（用以确定离开触发器后的敌人与玩家的距离）；十字架归位
		if (collision.gameObject.tag == "Enermy"  )
		{
			TempTarget = collision.gameObject.transform;
			IsFindEnermy = false;
			Lock.position = oripos;
			Debug.Log ("out");
		}
	}
}
