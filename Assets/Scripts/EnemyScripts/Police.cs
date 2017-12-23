using UnityEngine;
using System.Collections;

public class Police : MonoBehaviour {

	public GameObject bullet;
	public int force;
	private tk2dSpriteAnimator anim;
	private bool currentlyShooting = false;

	void Start(){
		anim = GetComponent<tk2dSpriteAnimator>();
		Shoot();
	}

	void Update(){
		if(anim.CurrentFrame == 2 && !currentlyShooting){
			currentlyShooting = true;
			FireBullet();
		}
		if(anim.CurrentFrame != 2){
			currentlyShooting = false;
		}
	}

	void Shoot(){
		if(!anim.IsPlaying("Shoot")){
			anim.Play("Shoot");
		}
	}

	void FireBullet(){
		GameObject bulletInstance = Instantiate(bullet, new Vector3(transform.position.x - 0.7f, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
		bulletInstance.transform.localEulerAngles = new Vector3(0, 0, 90);
		bulletInstance.rigidbody.AddForce(-Vector3.right * force);
	}
}
