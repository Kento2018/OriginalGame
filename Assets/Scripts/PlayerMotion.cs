using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotion : MonoBehaviour {

	private Animator animator;
	private bool lookback;
	private float lookbackdone = 0f;

	// Use this for initialization
	void Start () {

		animator = GetComponent<Animator> ();
		lookback = false;



	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetAxis ("Horizontal") > 0) {
			animator.SetInteger ("Horizontal", 1);
		} else if (Input.GetAxis ("Horizontal") < 0) {
			animator.SetInteger ("Horizontal", -1);
		} else {
			animator.SetInteger ("Horizontal", 0);
		}

		if (Input.GetAxis ("Vertical") > 0) {
			animator.SetInteger ("Vertical", 1);
		} else if (Input.GetAxis ("Vertical") < 0) {
			animator.SetInteger ("Vertical", -1);
		} else {
			animator.SetInteger ("Vertical", 0);	
		}

		if (Input.GetAxis ("Vertical") < 0) {
			if (Input.GetButton ("Vertical3")) {
				lookback = true;
			}
		}

	
		if (lookback == true) {
			
			transform.rotation = Quaternion.Slerp (transform.rotation,
				Quaternion.Euler (0, transform.localEulerAngles.y+21.51f, 0), Time.deltaTime * 4.0f);

			lookbackdone += Time.deltaTime;
			
		}

		if (lookbackdone > 1.0f) {
			lookback = false;
			lookbackdone = 0;
		}
	}
}