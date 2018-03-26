using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
	
	public Transform rightShoulder;
	public Transform leftShoulder;
	public Transform CameraParent;
	public float speed = 5.0f;
	public float speedMove_Forward = 5.0f;
	public float speedMove_Right = 1.2f;
	public float speedMove_Left = 1.0f;
	public float speedMove_Back = 1.0f;
	public float gravity = 20.0f;
	private Vector3 moveDirection = Vector3.zero;


	// Use this for initialization
	void Start () {


		
	}

	// Update is called once per frame
	void Update () {

		CharacterController controller = GetComponent<CharacterController> ();

		if (PlayerShoot.Fire == true){
			speed = 0.0f;
		}




		else {
			speed = speedMove_Forward;
		}

		if (controller.isGrounded) {
			moveDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
			moveDirection = transform.TransformDirection (moveDirection);

			if (Input.GetAxis ("Horizontal") > 0 
				&& PlayerShoot.Fire == false) {
				speed = speedMove_Right;
			}

			if (Input.GetAxis ("Horizontal") < 0 
				&& PlayerShoot.Fire == false) {
				speed = speedMove_Left;
			}

			if (Input.GetAxis ("Vertical") < 0 
				&& PlayerShoot.Fire == false) {
				speed = speedMove_Back;
			}

			moveDirection *= speed;


		}




		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move (moveDirection * Time.deltaTime);
		
	}

	void LateUpdate(){

		if (Input.GetButton ("Fire1")) {
			rightShoulder.rotation = Quaternion.Euler (rightShoulder.eulerAngles.x
			- CameraParent.localEulerAngles.x, rightShoulder.eulerAngles.y, 
				rightShoulder.eulerAngles.z);

			leftShoulder.rotation = Quaternion.Euler (leftShoulder.eulerAngles.x
				+ CameraParent.localEulerAngles.x, leftShoulder.eulerAngles.y, 
				leftShoulder.eulerAngles.z);


		}
		
	}
}
