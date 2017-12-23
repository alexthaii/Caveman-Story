using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	void Start(){
		Destroy(gameObject, 2);
	}

	void OnCollisionEnter(Collision collision){
		Destroy(gameObject);
	}
}
