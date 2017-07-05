using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAni : MonoBehaviour {
	
	GameObject _item;
	int _type;

	[HideInInspector]
	public Light _light;
	public float speed= 0.1f;
	public float distance = 0.04f;
    public float rotateSpeed = 1f;
	float i;
	// Use this for initialization
	void Start () {
		
		//读取武器type
		string _itemName = transform.GetChild (transform.childCount - 1).name;
        if (_itemName == "medical")
            _type = 0;
        else if (_itemName == "healthMedical")
            _type = 1;
	}

	// Update is called once per frame
	void Update () {
		i++;
		transform.position = new Vector3 (transform.position.x, transform.position.y + Mathf.Cos (i*0.05f)*distance, transform.position.z);
        transform.Rotate(Vector3.up, rotateSpeed);
    }

	void OnTriggerEnter(Collider col){
		if (col.tag=="PlayerBody") {
            PlayerManager.Instance._itemNumber[_type]++;
            //Destroy(this.gameObject);
		}



	}


}
