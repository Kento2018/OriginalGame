using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour {


	public float walkSpeed = 2f;
	public float runSpeed = 6f;
	public float gravity = -12f;
	private bool zoomView = false;
	private bool armed = false;


	private float turnSmoothTime = 0.2f;
	float turnSmoothVelocity;

	public float speedSmoothTime = 0.1f;
	float speedSmoothVelocity;
	float currentSpeed;
	float velocityY;

	public float aimMoveForwardSpeed = 2f;
	public float straffeSpeed = 1.5f;

	Animator animator;
	Transform cameraT;
	CharacterController controller;

	Vector3 currentRotation;
	float yaw;
	Vector3 rotationSmoothVelocity;

	// angle from character forward to cam forward
	//float angle;


	public float lookIKWeight;
	public float bodyWeight;
	public float headWeight;
	public float eyeWeight;
	public float clampWeight;
	public float lookAtDistance = 5f;
	public Vector3 lookOffset;

	public Transform rifle;
	public Transform rifleBackpackHolder;
	public GameObject backpackOBJ;
	SkinnedMeshRenderer backpackSMR;
	public Transform rifleBodyHolder;
	public Transform gunJoint;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		cameraT = Camera.main.transform;
		controller = GetComponent<CharacterController> ();
		backpackSMR = backpackOBJ.GetComponent<SkinnedMeshRenderer> ();
		RiflePlacement ();

	}
	
	// Update is called once per frame
	void Update (){


		Vector2 input = new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		Vector2 inputDir = input.normalized;	

		if (Input.GetAxis ("Horizontal") > 0) {
			animator.SetInteger (("Horizontal"), 1);
		}
		



		bool running = Input.GetKey (KeyCode.LeftShift);

		if (Input.GetMouseButtonDown(1)) {
			zoomView = true;
			animator.SetBool ("bZoomIn", true);
		} 
		if (Input.GetMouseButtonUp(1)){
			zoomView = false;
			animator.SetBool ("bZoomIn", false);
		}

		if(armed && zoomView && Input.GetKey("mouse 0")){
			animator.SetLayerWeight (animator.GetLayerIndex("shoot"), 1);
		}

		if(Input.GetMouseButtonUp(0)){
			animator.SetLayerWeight (animator.GetLayerIndex("shoot"), 0);
		}

		if (zoomView == false) {
			NormalMove (inputDir, running);
			float animationSpeedPecent = ((running) ? currentSpeed/runSpeed : currentSpeed/walkSpeed * 0.5f);
			animator.SetFloat("speedPercent", animationSpeedPecent, speedSmoothTime, Time.deltaTime);
		}

		if (zoomView == true) {
			ZoomViewMove ();

			float zoomForwardSpeedPercent = Input.GetAxis("Vertical");
			animator.SetFloat("zoomWalkForward", zoomForwardSpeedPercent);

			float zoomStraffeSpeedPercent = Input.GetAxis("Horizontal");
			animator.SetFloat("zoomStraffe", zoomStraffeSpeedPercent);

			if (armed) {
				lookOffset = new Vector3 (0, -0.34f, 0);
			}
			
			if (!armed) {
				lookOffset = new Vector3 (0, 0, 0);
			}
		}

		//angle = Vector3.Angle (transform.forward, cameraT.forward);


		if (Input.GetKeyDown (KeyCode.F)) {
			armed = !armed;
			animator.SetBool ("bArmed", armed);
			if (armed) {
				rifle.SetParent ( gunJoint,false);
			}
			if (!armed) {
				RiflePlacement ();
				//rifle.SetParent (rifleBackpackHolder,false);
			}
		}
					

	}


	public void RiflePlacement(){
		if (backpackSMR.enabled && !armed) {
			rifle.SetParent (rifleBackpackHolder,false);
		}

		if (!backpackSMR.enabled && !armed) {
			rifle.SetParent (rifleBodyHolder,false);
		}
	}

	void OnAnimatorIK()
	{		
		if (zoomView == true) {
			animator.SetLookAtWeight (lookIKWeight, bodyWeight, headWeight, eyeWeight, clampWeight);
			animator.SetLookAtPosition (cameraT.position + (cameraT.forward +lookOffset) * lookAtDistance);
		}
	}


	void NormalMove( Vector2 inputDir, bool running)
	{

		if (inputDir != Vector2.zero) {
			float targetRotation = Mathf.Atan2 (inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
			transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle (transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
		}

		//
		
		float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
		currentSpeed = Mathf.SmoothDamp (currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

		velocityY += Time.deltaTime * gravity;

		Vector3 velocity =  transform.forward * currentSpeed + Vector3.up * velocityY;

		controller.Move (velocity * Time.deltaTime);
		currentSpeed = new Vector2 (controller.velocity.x, controller.velocity.z).magnitude;


		if(controller.isGrounded){
			velocityY = 0;
		}


	}

	void ZoomViewMove(){

		transform.eulerAngles = new Vector3 (0, cameraT.eulerAngles.y);
	
		velocityY += Time.deltaTime * gravity;

		float moveForward = Input.GetAxis ("Vertical") * aimMoveForwardSpeed ;
		float straffe = Input.GetAxis ("Horizontal") * straffeSpeed ; 

		Vector3 h = transform.TransformDirection (Vector3.forward) ;
		Vector3 v = transform.TransformDirection (Vector3.right);

		controller.Move ((straffe * v + moveForward * h + Vector3.up * velocityY) * Time.deltaTime);

		if(controller.isGrounded){
			velocityY = 0;
		}


	}
		
}
