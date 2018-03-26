using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotPlayer : MonoBehaviour {

	public int damage = 200;

	// Use this for initialization
	void Start () {

		Destroy (gameObject, 2.0f);
		
	}
	
	// Update is called once per frame
	void Update () {

		transform.position += transform.forward * Time.deltaTime * 100;
		damage--;
		if(damage <= 1){
			damage = 1;
		}
		
	}

	private void OnCollisionEnter(Collision collider){
		if (collider.gameObject.name == "Terrain") {
			Destroy (gameObject);
		}

		if(collider.gameObject.tag == "Enemy"){
			Destroy(gameObject);
		}


	}






}
