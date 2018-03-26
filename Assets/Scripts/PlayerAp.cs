using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAp : MonoBehaviour {
    int armorPoint;
    int armorPointMax = 5000;
	int damage = 100;
	public Image gaugeImage;
	public Color myYellow;
	public Color myRed;

	// Use this for initialization
	void Start () {

		armorPoint = armorPointMax;
		
	}
	
	// Update is called once per frame
	void Update () {

		float percentageArmorPoint = (float)armorPoint / armorPointMax;

		if (percentageArmorPoint > 0.5f) {
			gaugeImage.color = new Color (0.25f, 0.7f, 0.6f);
		} else if (percentageArmorPoint > 0.2f) {
			gaugeImage.color = myYellow;
		} else {
			gaugeImage.color = myRed;
		}

		gaugeImage.transform.localScale = new Vector3(percentageArmorPoint, 1, 1);
		//Debug.Log (percentageArmorPoint);
		//Debug.Log (armorPoint);
		
	}

	private void OnCollisionEnter(Collision collider){
		if (collider.gameObject.tag == "Enemy") {
			armorPoint -= damage;
			armorPoint = Mathf.Clamp (armorPoint, 0, armorPointMax);
		}
	}




}