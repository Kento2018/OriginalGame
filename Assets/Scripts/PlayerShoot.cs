using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour {

	public GameObject shot;
	public GameObject muzzle;
	float shotInterval = 0;
	float shotIntervalMax = 1.0f;
	float shotLightInterval = 0;
	float shotLightIntervalMax = 0.1f;
	float fireReset = 0.0f;
	AudioSource audioSource;
	private Animator animator;
	public Light shotLight;
	public Image aimImage;
	Vector3 aimImageSize;
	public static bool Fire;
	public float aimImageMaxsize = 1.8f;
	public float aimImageMinsize = 1.0f;


	// Use this for initialization
	void Start () {

		audioSource = gameObject.GetComponent<AudioSource> ();
		animator = GetComponent<Animator> ();
		shotLight.enabled = false;
		Fire = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		shotInterval += Time.deltaTime;
		shotLightInterval += Time.deltaTime;
		fireReset += Time.deltaTime;
			
		if (Input.GetButton ("Fire1")) {
			animator.SetBool ("Fire1", true);
			Fire = true;
			fireReset = 0.0f;

			if (shotInterval > shotIntervalMax) {
				Instantiate (shot, muzzle.transform.position, Camera.main.transform.rotation);
				shotInterval = 0;

				shotLight.enabled = true;
				shotLightInterval = 0;


				audioSource.PlayOneShot (audioSource.clip);

				//AnimatorStateInfo stateInfo = aimImage.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0);
				//if (stateInfo.fullPathHash ==
				    //Animator.StringToHash ("Base Layer.scale@AimImage")) {
					//aimImage.GetComponent<Animator> ().Play (stateInfo.fullPathHash, 0, 0.0f);
				//} else {
					//aimImage.GetComponent<Animator> ().SetTrigger ("Fire");
				//}
			}
		} else {
			if (fireReset > 0.5f) {
				animator.SetBool ("Fire1", false);
				Fire = false;
			}
		}


		if (Input.GetButton ("Fire1")) {
			
			aimImage.transform.localScale += new Vector3 
				(Time.deltaTime * 2, Time.deltaTime * 2, Time.deltaTime * 2);
			if(aimImage.transform.localScale.x > aimImageMaxsize &&
				aimImage.transform.localScale.y > aimImageMaxsize &&
				aimImage.transform.localScale.z > aimImageMaxsize){
				aimImage.transform.localScale = new Vector3 
					(aimImageMaxsize, aimImageMaxsize, aimImageMaxsize);
			}

		} else {
			
			aimImage.transform.localScale -= new Vector3
				(Time.deltaTime * 6, Time.deltaTime * 6, Time.deltaTime * 6);
			if(aimImage.transform.localScale.x < aimImageMinsize &&
				aimImage.transform.localScale.y < aimImageMinsize &&
				aimImage.transform.localScale.z < aimImageMinsize){
				aimImage.transform.localScale = new Vector3
					(aimImageMinsize, aimImageMinsize, aimImageMinsize);
			}



		}

		if (shotLightInterval > shotLightIntervalMax) {
			shotLight.enabled = false;
			shotLightInterval = 0;
		}
	}
}