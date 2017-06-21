using UnityEngine;
using System.Collections;

public class View : MonoBehaviour
{
	public Transform m_target;

	public float m_viewAngle = 90f;
	public int m_vertexNum = 5;

	public float m_detectDistance = 15f;
	public float m_loseDistance = 20f;

	private float m_viewDistance;

	private Vector3[] m_vertexBuffer;
	private Vector3[] m_vertexBuffer2;

	private Vector3[] m_normalBuffer;
	private int[] m_trangleIndexBuffer;

	private int m_trangleNum;

	private Mesh m_mesh;
	private Material m_material;

	public bool m_foundTarget = false;

	public delegate void ViewDelegate (Transform target);

	public ViewDelegate findTargetDelegate;
	public ViewDelegate loseTargetDelegate;
	// Use this for initialization
	void Start ()
	{
		initData ();
		drawView ();
	}

	void initData ()
	{
		m_mesh = GetComponent<MeshFilter> ().mesh;
		m_material = GetComponent<MeshRenderer> ().material;

	
		if (m_vertexNum < 3)
			m_vertexNum = 3;

		m_trangleNum = m_vertexNum - 2;

		m_vertexBuffer = new Vector3[m_vertexNum];
		m_vertexBuffer2 = new Vector3[m_vertexNum];
		m_normalBuffer = new Vector3[m_vertexNum];
		m_trangleIndexBuffer = new int[m_trangleNum * 3];

		for (int i = 0; i < m_vertexNum; ++i)
			m_normalBuffer [i] = Vector3.up;
        
		for (int i = 0; i < m_trangleNum; ++i) {
			int j = i * 3;
			m_trangleIndexBuffer [j] = 0;
			m_trangleIndexBuffer [j + 1] = i + 1;
			m_trangleIndexBuffer [j + 2] = i + 2;
		}



		m_mesh.vertices = m_vertexBuffer;
		m_mesh.normals = m_normalBuffer;
		m_mesh.triangles = m_trangleIndexBuffer;
		;
		m_mesh.bounds = new Bounds (Vector3.zero, new Vector3 (m_loseDistance, m_loseDistance, 1));


		m_viewDistance = m_detectDistance;

		m_vertexBuffer [0] = Vector3.zero;

	}


	// Update is called once per frame
	void LateUpdate ()
	{
		if (Input.GetKeyDown (KeyCode.Space)) {
			initData ();
			drawView ();
		}

		detect ();
		updateView ();
	}

	bool hasObstacle (Vector3 dic, float distance, out RaycastHit hit, string layer)
	{
		return Physics.Raycast (transform.position, dic, out hit, distance, 1 << LayerMask.NameToLayer (layer));
	}

	public bool hasObstacle (Vector3 dic, float distance, string layer)
	{
		return Physics.Raycast (transform.position, dic, distance, 1 << LayerMask.NameToLayer (layer));
	}

	public bool hasObstacle (Vector3 dic, float distance, out Vector3 edgePoint, string layer)
	{
		RaycastHit hit;
		if (Physics.Raycast (transform.position, dic, out hit, distance, 1 << LayerMask.NameToLayer (layer))) {
			Transform col = hit.collider.transform;
			Vector3 hVector = Vector3.Cross (hit.normal, Vector3.up);
			hVector = hit.collider.transform.InverseTransformDirection (hVector);
			hVector = hVector.normalized;
			hVector = new Vector3 (col.localScale.x * hVector.x, 0, col.localScale.z * hVector.z);
			Vector3 point1 = transform.position + hVector;
			Vector3 point2 = transform.position - hVector;
			edgePoint = point1;

			edgePoint = Vector3.Cross ((transform.position - hit.collider.transform.position), Vector3.up).normalized;
			return true;
		}
		edgePoint = Vector3.zero;
		return false;
	}

	void updateView ()
	{
		RaycastHit hit;
	
		for (int i = 1; i < m_vertexNum; ++i) { 
//			if (m_vertexBuffer [i] == null) {
//				Debug.Log("m_vertexBuffer is null");
//			}
			if (hasObstacle (transform.TransformDirection (m_vertexBuffer [i]), m_viewDistance, out hit, "Wall"))
				m_vertexBuffer2 [i] = transform.InverseTransformPoint (hit.point);
			else
				m_vertexBuffer2 [i] = m_vertexBuffer [i];

		}
		m_mesh.vertices = m_vertexBuffer2;
	}

	void drawView ()
	{
		Quaternion startRot = Quaternion.Euler (0, -m_viewAngle / 2, 0);
		Quaternion dRot = Quaternion.Euler (0, m_viewAngle / m_trangleNum, 0);
		Vector3 rotVector = startRot * (Vector3.forward * m_viewDistance);

		for (int i = 1; i < m_vertexNum; ++i) {
			m_vertexBuffer [i] = rotVector;
			rotVector = dRot * rotVector;
		}
		m_mesh.vertices = m_vertexBuffer;
	}

	void detect ()
	{
		if (!m_foundTarget) {
			if (Vector3.Distance (transform.position, m_target.position) < m_detectDistance) {
				Vector3 dic = (m_target.position - transform.position);
				float cos = Vector3.Dot (transform.forward, dic.normalized);
				float angle = Mathf.Acos (cos) * Mathf.Rad2Deg;
				if (angle <= m_viewAngle / 2) {
					if (!hasObstacle (dic, dic.magnitude, "Wall")) {
						m_material.color = new Color (1f, 0, 0, 0.2f);
						m_foundTarget = true;
						m_viewDistance = m_loseDistance;
						drawView ();
						if (findTargetDelegate != null)
							findTargetDelegate (m_target);
					}
				}
			}
		} else {
			if (Vector3.Distance (transform.position, m_target.position) > m_loseDistance) {
				m_material.color = new Color (0, 1f, 0, 0.2f);
				m_foundTarget = false;
				m_viewDistance = m_detectDistance;
				drawView ();
				if (loseTargetDelegate != null)
					loseTargetDelegate (m_target);
			}
		}



	}
}
