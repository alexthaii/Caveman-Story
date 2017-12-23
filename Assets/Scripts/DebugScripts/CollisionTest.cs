using UnityEngine;
using System.Collections;

public class CollisionTest : MonoBehaviour {

	void OnCollisionEnter(Collision collision){
		if(collision.transform.tag == "Player"){
			Debug.Log("OnCollisionEnter: " + collision.transform.name);
		}
	}
	void OnCollisionStay(Collision collision){
		if(collision.transform.tag == "Player"){
			Debug.Log("OnCollisionStay: " + collision.transform.name);
		}
	}
	void OnCollisionLeave(Collision collision){
		if(collision.transform.tag == "Player"){
			Debug.Log("OnCollisionLeave: " + collision.transform.name);
		}
	}

	void OnTriggerEnter(Collider collider){
		if(collider.transform.tag == "Player"){
			Debug.Log("OnTriggerEnter: " + collider.transform.name);
		}
	}
	void OnTriggerStay(Collider collider){
		if(collider.transform.tag == "Player"){
			Debug.Log("OnTriggerStay: " + collider.transform.name);
		}
	}
	void OnTriggerLeave(Collider collider){
		if(collider.transform.tag == "Player"){
			Debug.Log("OnTriggerLeave: " + collider.transform.name);
		}
	}
}
