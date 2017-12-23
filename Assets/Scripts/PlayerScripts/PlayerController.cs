using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public AudioSource punchFX;
	public float jumpHeight;
	public float gravity;
	public float speed;
	public int damageFreq;

	private PlayerPhysics playerPhysics;

	private float acceleration = 5.0f;
	private Vector3 currentSpeed;
	private Vector3 targetSpeed;

	private bool climbEnabled;
	private Vector3 amountToMove;
	private bool facingRight = true;

	private tk2dSpriteAnimator anim;
	private Renderer playerRenderer;
	private bool canBeHurt = true;

	void Start(){
		anim = GetComponent<tk2dSpriteAnimator>();
		playerPhysics = GetComponent<PlayerPhysics>();
		playerRenderer = GetComponent<Renderer>();
	}
	
	void Update(){
		float moveX = Input.GetAxis("Horizontal");
		float moveY = Input.GetAxis("Vertical");
		Vector3 moveDirection;
		if(climbEnabled){
			moveDirection = new Vector3(moveX, moveY, 0);
		}else{
			moveDirection = new Vector3(moveX, 0, moveY);
		}

		if(moveX == 0 && moveY == 0){
			anim.Stop();
		}else{
			anim.Play();
		}

		CheckForFlip(moveX);
		CheckForJump();
		CalculateAmountToMove(moveDirection);

		playerPhysics.Move(amountToMove * Time.deltaTime);
	}

	void CheckForFlip(float dirX){
		if(dirX > 0 && !facingRight){
			Flip();
		}else if(dirX < 0 && facingRight){
			Flip();
		}
	}
	
	void Flip(){
		facingRight = !facingRight;
		Vector3 currentDir = transform.localScale;
		currentDir.x *= -1;
		transform.localScale = currentDir;
	}

	void CheckForJump(){
		if(playerPhysics.grounded && Input.GetButtonDown("Jump")){
			Jump();
		}
	}

	void Jump(){
		amountToMove.y = jumpHeight;
		playerPhysics.grounded = false;
	}
	
	void CalculateAmountToMove(Vector3 dir){
		targetSpeed = dir * speed;
		currentSpeed.x = Mathf.MoveTowards(currentSpeed.x, targetSpeed.x, acceleration);
		currentSpeed.y = Mathf.MoveTowards(currentSpeed.y, targetSpeed.y, acceleration);
		currentSpeed.z = Mathf.MoveTowards(currentSpeed.z, targetSpeed.z, acceleration);

		amountToMove.x = currentSpeed.x;
		if(dir.y == 0 && !climbEnabled){
			amountToMove.y -= gravity * Time.deltaTime;
			amountToMove.z = currentSpeed.z;
		}else{
			amountToMove.y = currentSpeed.y;
			amountToMove.z = 0;
		}

	}
	void OnCollisionEnter(Collision collision){
		if(collision.transform.tag == "Bullet" && canBeHurt){
			canBeHurt = false;
			StartCoroutine("Flicker");
			SoundManager.SwapForModernSound();
		}
	}

	void OnTriggerStay(Collider collider){
		if(collider.tag == "Building" && Input.GetButtonDown("Fire1")){
			string hitFrom = "";
			if(collider.transform.tag == "LeftClimbZone"){
				hitFrom = "left";
			}else if(collider.transform.tag == "RightClimbZone"){
				hitFrom = "right";
			}
			BuildingManager buildingManager = collider.transform.parent.GetComponent<BuildingManager>();
			buildingManager.BuildingPieceHit(collider.gameObject, hitFrom);
			punchFX.Play();
		}

		if(collider.tag == "LeftClimbZone" && Input.GetKey(KeyCode.LeftShift)){
			climbEnabled = true;
			if(!facingRight){
				Flip();
			}
		}

		if(collider.tag == "RightClimbZone" && Input.GetKey(KeyCode.LeftShift)){
			climbEnabled = true;
			if(facingRight){
				Flip();	
			}
		}

		if(Input.GetKeyUp(KeyCode.LeftShift)){
			climbEnabled = false;
		}
	}

	void OnTriggerExit(Collider collider){
		if(collider.tag == "LeftClimbZone" || collider.tag == "RightClimbZone"){
			climbEnabled = false;
		}
	}	

	IEnumerator Flicker(){
		float startTime = Time.time;
		while((Time.time - startTime) < damageFreq){
			playerRenderer.enabled = false;
			yield return new WaitForSeconds(0.1f);
			playerRenderer.enabled = true;
			yield return new WaitForSeconds(0.1f);
		}
		canBeHurt = true;
	}
}
