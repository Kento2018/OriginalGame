using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

	public GameObject target;
	NavMeshAgent agent;
	public int armorPoint;
	public int armorPointMax = 1000;
	int damage = 100;



	// Use this for initialization
	void Start () {

		agent = GetComponent<NavMeshAgent> ();
		armorPoint = armorPointMax;
		
	}
	
	// Update is called once per frame
	void Update () {

		agent.destination = target.transform.position;

	}

	private void OnCollisionEnter(Collision collider){
		if (collider.gameObject.tag == "Shot" && armorPoint > 0) {

			damage = collider.gameObject.GetComponent<ShotPlayer> ().damage;
			armorPoint -= damage;
			Debug.Log (armorPoint);

			if (armorPoint <= 0) {
				agent.Stop ();
			

				AnimatorStateInfo stateInfo = gameObject.GetComponent
					<Animator> ().GetCurrentAnimatorStateInfo (0);
				if (stateInfo.fullPathHash ==
				   Animator.StringToHash ("Base Layer.Killed")) {
					gameObject.GetComponent<Animator> ().Play (stateInfo.fullPathHash, 0, 0.0f);
				} else {
					gameObject.GetComponent<Animator> ().SetTrigger ("Dead");

				}


			}

		}

}
}
