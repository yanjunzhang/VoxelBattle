using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//枚举玩家攻击状态：近战武器；枪械；法杖。
public enum PlayerWeaponState
{
	Gun1, 
	RPG,
	Staff,
}

public class AttackWithGuns : MonoBehaviour {

	//调用导弹移动脚本
	public PlayerMissileMove playerMissileMove;

	//调用侦测敌人脚本
	public DetectEnermy detectEnermy;

//	public static Dictionary<int, string> GetTypeDict()
//	{
//		Dictionary<int, string> dict = new Dictionary<int, string>();
//		Type t = typeof(ArticleType);
//		Array arrays = PlayerWeaponState.GetValues(t);
//		for (int i = 0; i < playerWeaponState.Length; i++)
//		{
//			ArticleType tmp = (ArticleType)arrays.GetValue(i);
//			string Description = Utility.GetEnumDescription(tmp);
//			dict.Add((int)tmp, Description);
//		}
//		return dict;
//	}

	//调用玩家子弹预制体
	public GameObject PlayerBullet;
	//调用子弹实例化位置
	public Transform Gun1;

	public GameObject PlayerMissile;
	public Transform RPG;

	//判断是否正在创建导弹
	bool IsBuildingMissile =false;

	//设定导弹冷却时间
	public float TimeCount = 0;

	//用于切换武器
	public int Turn=5;

	//实例化枚举，并用属性保护
	private PlayerWeaponState localWeaponState;
	public PlayerWeaponState WeaponState
	{
		get{ return localWeaponState;}
		set
		{
			localWeaponState = value;
		}
	}

	void Start () {

	}

	void Update () {

		SwitchWeapon ();
		MatchState ();
		PlayerShoot ();
		PlayerFire ();

		//生成导弹CD计时器
		TimeCount += Time.deltaTime;
		if (TimeCount >= 2) {
			IsBuildingMissile = false;
		}
	}

	//按QE键切换武器
	void SwitchWeapon()
	{
		if (Input.GetKeyUp (KeyCode.Q ) ) 
		{
			Turn--;
		}

		if (Input.GetKeyUp (KeyCode.E ) ) 
		{
			Turn++;
		}
	}

	//根据武器确定玩家攻击状态
	void MatchState()
	{
		if(Turn%3==2)
		{
			localWeaponState = PlayerWeaponState.Gun1 ;
		}
		else if(Turn%3==1)
		{
			localWeaponState = PlayerWeaponState.Staff  ;
		}
		else if(Turn%3==0)
		{
			localWeaponState = PlayerWeaponState.RPG ;
		}
		else if(Turn<3)
		{
			Turn = 5;
		}
	}

	//玩家装备近战武器攻击（此处用枪械代替）
	void PlayerShoot()
	{
		if (Input.GetMouseButton (0) && localWeaponState == PlayerWeaponState.Gun1) 
		{
			GameObject CreatBullet= Instantiate 
				(
					PlayerBullet
				)	as GameObject ;

			CreatBullet.transform.position = Gun1.transform.position;
		}
	}

	//玩家装备枪械攻击（此处用导弹加上跟踪系统）
	void PlayerFire()
	{
		if (Input.GetMouseButton (0) && localWeaponState == PlayerWeaponState.RPG 
			&& playerMissileMove.IsFired==false && detectEnermy.IsFindEnermy ==true 
			&& IsBuildingMissile == false  ) 
		{
			GameObject CreatMissile= Instantiate 
				(
					PlayerMissile
				)	as GameObject ;

			CreatMissile.transform.position = RPG.transform.position;
			IsBuildingMissile = true;
			TimeCount = 0;
		}
	}
}
