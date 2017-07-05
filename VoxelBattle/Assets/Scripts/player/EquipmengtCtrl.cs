using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmengtCtrl : MonoBehaviour {
    public UIManager_Game _manager;
    public Transform _weaponPoint;

    [HideInInspector]
    Transform[] _weapons = new Transform[3];
    public Animator _animator;
    public int i = 1;//1近战 2枪械 3魔法
    // Use this for initialization
    void Start () {
        _animator = GetComponent<Animator>();
        for (int i = 0; i < _weaponPoint.childCount; i++)
        {
            _weapons[i] = _weaponPoint.GetChild(i);
        }
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //更换UI图片
            _manager.ChangeWeapon(i % 3);
            //武器模型切换
            ChangeWeapon(i % 3);
            i++;
        }
        //每帧检测武器状态，及时更换动作
        DetecWeaponState();
    }
    void ChangeWeapon(int j)
    {
        for (int i = 0; i < _weapons.Length; i++)
        {
            if (i==j)
            {
                _weapons[i].gameObject.SetActive(true);
            }else
                _weapons[i].gameObject.SetActive(false);
            
        }
    }
    void DetecWeaponState()
    {
        if (_animator.GetBool("isMove"))
        {
            if (_animator.GetBool("IsContinue"))
            {
                return;
            }
            if (i % 3 == 1 || i % 3 == 2)
            {
                _animator.SetBool("SwordMove", true);
                _animator.SetBool("MagicMove", false);
            }
            else if (i % 3 == 0)
            {
                _animator.SetBool("SwordMove", false);
                _animator.SetBool("MagicMove", true);
            }
        }
        else
        {
            if (i % 3 == 1 || i % 3 == 2)
            {
                _animator.SetBool("SwordIdle", true);
                _animator.SetBool("MagicIdle", false);
            }
            else if (i % 3 == 0)
            {
                _animator.SetBool("SwordIdle", false);
                _animator.SetBool("MagicIdle", true);
            }
        }

    }
    //普通攻击(false单击,true按住）
    public void AttackAniHit(bool isContinue)
    {
        if (i%3==1)//近战
        {
            _animator.SetTrigger("SwordAttack");
        }
        else if (i%3==2)//枪械
        {
            _animator.SetTrigger("GunAttack");
        }
        else if (i%3==0)//法杖
        {
            _animator.SetTrigger("MagicAttack");
        }
        if (isContinue)
            _animator.SetBool("IsContinue", true);
        else
            _animator.SetBool("IsContinue", false);
    }
    public void PlayerIdle()
    {
        _animator.SetBool("MagicMove", false);
        _animator.SetBool("SwordMove", false);
        _animator.SetBool("isMove", false);
    }
    public void PlayerMove()
    {
        _animator.SetBool("isMove", true);
        if (i % 3 == 1 || i % 3 == 2)
        {
            if (_animator.GetBool("IsContinue"))
            {
                _animator.SetBool("SwordMove", false);
                _animator.SetBool("SwordIdle", false);
                
                return;
            }
                
            _animator.SetBool("SwordMove", true);
            _animator.SetBool("SwordIdle", false);
        }
        else if (i % 3 == 0)
        {
            if (_animator.GetBool("IsContinue"))
            {
                _animator.SetBool("MagicMove", false);
                _animator.SetBool("MagicIdle", false);
                return;
            }
            _animator.SetBool("MagicMove", true);
            _animator.SetBool("MagicIdle", false);
        }
        
    }
}
