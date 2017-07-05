using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseHud : MonoBehaviour {
	public Transform m_target;
	public GameObject m_hudTextPrefabs;
//	public GameObject m_hudHPPrefabs;
	HUDText m_hudText=null;
    UIFollowTarget follow;
    // Use this for initialization
    void Start () {
		m_target = transform;
		//添加hudtext到hudroot结点下
		GameObject child = NGUITools.AddChild (HUDRoot.go, m_hudTextPrefabs);
//		GameObject childHP = NGUITools.AddChild (HUDRoot.go, m_hudHPPrefabs);

		//获取hud text
		m_hudText = child.GetComponent<HUDText> ();
		//添加uifollow脚本
//		childHP.AddComponent<UIFollowTarget> ().target = m_target;
		if (this.gameObject.tag=="Enemy") {
			m_target = this.transform.GetChild (0);
		}
		follow = child.AddComponent<UIFollowTarget> ();
		follow.target = m_target;
		follow.gameCamera = Camera.main;
		follow.uiCamera = GameObject.Find ("Camera").GetComponent<Camera> ();
		follow.disableIfInvisible = false;


	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void BeHit(float damage){
		m_hudText.Add ("-"+damage, Color.red, 0.1f);
	}
    private void OnDestroy()
    {
		if (follow!=null)
        {
            Destroy(follow.gameObject);
        }
        
    }

}
