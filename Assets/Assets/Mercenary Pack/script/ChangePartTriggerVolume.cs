using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePartTriggerVolume : MonoBehaviour {

	public string partName;
	bool playerInsideColider;

	ChangeBodyPart changePartScript;

	 
													//pantPart;   	partIndex 1
													//backpackPart;	partIndex 2
	public int partIndexNumber;						//headPart;		partIndex 3
													//clothesPart;	partIndex 4

	public bool bVisible;

	// Use this for initialization
	void Start () {
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		changePartScript = player.GetComponent<ChangeBodyPart> ();
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.E) && playerInsideColider) {
			if (changePartScript != null) {
				changePartScript.ChangePart (partIndexNumber, partName, bVisible);
			}
		}

	}



	void OnTriggerEnter(Collider other){
	
		if (other.gameObject.tag == "Player")
			playerInsideColider = true;
	}

	void OnTriggerExit (Collider other){
		if (other.gameObject.tag == "Player")
			playerInsideColider = false;
	}

}
