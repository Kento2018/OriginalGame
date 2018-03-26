using System.Collections;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {

	public float mouseSensitivity = 2f;
	public Transform target;
	public Transform zoomInTarget;
	public float dstFromTarget = 2f;
	public Vector2 pitchMinMax = new Vector2 (-30, 70);
	private bool zoomView = false;
	public float rotationSmoothTime = 0.12f;
	Vector3 rotationSmoothVelocity;
	Vector3 currentRotation;

	public float camSmoothTime = 5f;

	//private float turnSmoothTime = 0.2f;
	//float turnSmoothVelocity;

	float yaw;
	float pitch;
	Camera cam;


	//public Transform playerTransform;

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		cam = GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void LateUpdate () {

	
		if (Input.GetMouseButtonDown (1)) {
			zoomView = true;
		} 
		if (Input.GetMouseButtonUp (1)){
			zoomView = false;
		}

		if (Input.GetKeyDown ("escape")) {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}


		yaw += Input.GetAxis ("Mouse X") *mouseSensitivity;
		pitch -= Input.GetAxis ("Mouse Y") *mouseSensitivity;
		pitch = Mathf.Clamp (pitch, pitchMinMax.x, pitchMinMax.y);
		currentRotation = Vector3.SmoothDamp (currentRotation, new Vector3 (pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
		transform.eulerAngles = currentRotation;

		if (zoomView == false) {
			transform.position = target.position - transform.forward * dstFromTarget;
			cam.fieldOfView = Mathf.Lerp (cam.fieldOfView, 60, Time.deltaTime*5);

		}


		if (zoomView == true) {
			transform.position =  zoomInTarget.position - transform.forward * dstFromTarget* 0.6f;
			cam.fieldOfView = Mathf.Lerp (cam.fieldOfView, 40, Time.deltaTime*5);

		}



	}
}
