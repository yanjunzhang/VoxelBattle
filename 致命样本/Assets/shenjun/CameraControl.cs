/*
 * Author : shenjun
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Controller
{
	mouse,
	key
};
public class CameraControl : MonoBehaviour {
    


    public Transform player;
//	[SerializeField]
	public Controller _controller=Controller.key;

    [HideInInspector]
    public Transform child;
	public Quaternion targetQuaternion;

    private void Awake()
    {
        child = transform.GetChild(0);
        child.LookAt(this.transform);
    }

    float mouseX = 0;
    public float rotSpeed = 1000;
	
	void Update () {

        transform.position = player.position;
		if (_controller==Controller.mouse) {
			if (Input.GetMouseButton(1))
			{
				mouseX = Input.GetAxis("Mouse X");
				transform.Rotate(0, mouseX * rotSpeed * Time.deltaTime, 0);
			}
		}else if (_controller==Controller.key) {
			if (Input.GetKeyDown(KeyCode.Q)) {
				targetQuaternion = Quaternion.Euler (transform.rotation.eulerAngles + new Vector3 (0, -45, 0));
			}else if (Input.GetKeyDown(KeyCode.E)) {
				targetQuaternion = Quaternion.Euler (transform.rotation.eulerAngles + new Vector3 (0, 45, 0));
			}
			transform.rotation = Quaternion.Slerp (transform.rotation,targetQuaternion,0.1f);
		}

        
	}
}
