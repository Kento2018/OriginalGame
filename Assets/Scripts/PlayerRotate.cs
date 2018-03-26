using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour {

	public Transform CameraParent_Limit;
	public float maxangle = 25.0f;
	public float minangle = -20.0f;
	Vector3 rotEuler;





	// Use this for initialization
	void Start () {

		rotEuler = new Vector3 (CameraParent_Limit.localEulerAngles.x, 
			CameraParent_Limit.localEulerAngles.y, 0.0f);
		
	} 

	// Update is called once per frame
	void Update () {
		transform.Rotate (0, Input.GetAxis ("Horizontal2") * 3.0f, 0);





		GameObject CameraParent = Camera.main.transform.parent.gameObject;
		CameraParent.transform.Rotate (Input.GetAxis ("Vertical2") * 3.0f, 0, 0);

		rotEuler = new Vector3 (Mathf.Clamp (rotEuler.x + Input.GetAxis ("Vertical2") * 3.0f, 
			minangle, maxangle), rotEuler.y, 0.0f);
		CameraParent_Limit.localEulerAngles = rotEuler;




}
}