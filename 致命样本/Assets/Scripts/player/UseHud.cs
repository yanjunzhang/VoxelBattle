using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseHud : MonoBehaviour {
	public Transform m_target;
	public GameObject m_hudTextPrefabs;
	public GameObject m_hudHPPrefabs;
	HUDText m_hudText=null;

	// Use this for initialization
	void Start () {
		//添加hudtext到hudroot结点下
		GameObject child = NGUITools.AddChild (HUDRoot.go, m_hudTextPrefabs);
		GameObject childHP = NGUITools.AddChild (HUDRoot.go, m_hudHPPrefabs);

		//获取hud text
		m_hudText = child.GetComponent<HUDText> ();
		//添加uifollow脚本
		childHP.AddComponent<UIFollowTarget> ().target = m_target;
        child.AddComponent<UIFollowTarget>().target = m_target;
        
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			m_hudText.Add ("+100", Color.red, 0.1f);
           
		}
	}

}
