using UnityEngine;
using System.Collections;

public class PlayerPhysics : MonoBehaviour {

	public LayerMask collisionMask;
	
	[HideInInspector]
	public bool grounded;

	private static float lastXPos;

	private BoxCollider boxColl;
	private Vector3 initSize;
	private Vector3 initCenter;
	
	private float skin = .005f;

	private Ray rayX;
	private Ray rayY;
	private Ray rayZLeft;
	private Ray rayZCenter;
	private Ray rayZRight;
	private float rayXDist = 0.5f;
	private float rayYDist = 0.5f;
	private float rayZDist = 1f;
	private RaycastHit hitX;
	private RaycastHit hitY;
	private RaycastHit hitZ;
	
	
	void Start() {
		boxColl = GetComponent<BoxCollider>();
		initSize = boxColl.size;
		initCenter = boxColl.center;
	}

	public void Move(Vector3 moveAmount){
		Vector3 p = transform.position;
		
		float deltaX = moveAmount.x;
		float deltaY = 	moveAmount.y;
		float deltaZ = moveAmount.z;
		float directionX = Mathf.Sign(lastXPos);
		float directionY = Mathf.Sign(deltaY);
		float directionZ = Mathf.Sign(deltaZ);

		if(deltaX != 0){
			lastXPos = deltaX;
		}

		for(int i = 0; i < 3; i++){
			float colliderCenterX = (p.x + initCenter.x - initSize.x/2) + initSize.x/2 * i;
			float colliderCenterY = (p.y + initCenter.y - initSize.y/2) + initSize.y/2 * i;
			float colliderCenterZ = (p.z + initCenter.z - initSize.z/2);// + initSize.z/2 * i;
			float colliderBottom = p.y + initCenter.y + initSize.y/2 * directionY; 

			rayX = new Ray(new Vector3( (p.x + initCenter.x) , colliderCenterY, colliderCenterZ), new Vector3(directionX, 0, 0));	
			if(Physics.Raycast(rayX, out hitX, rayXDist)){
				deltaX = DrawRay.calculateXPosition(rayX, hitX, skin, directionX, deltaX);
			}
			
			rayY = new Ray(new Vector3(colliderCenterX, colliderBottom, colliderCenterZ), new Vector3(0, directionY, 0));
			if(Physics.Raycast(rayY, out hitY, rayYDist, collisionMask)){
				deltaY = DrawRay.calculateYPosition(rayY, hitY, skin, directionY, Mathf.Abs(deltaY));
				grounded = true;
			}

			if(directionZ == 1){
				colliderCenterZ += initSize.z;
			}
			rayZLeft = new Ray(new Vector3( (p.x + initCenter.x - initSize.x/2), colliderCenterY, colliderCenterZ), new Vector3(0, 0, directionZ));	
			if(Physics.Raycast(rayZLeft, rayZDist, collisionMask)){
				deltaZ = 0;
			}
			rayZCenter = new Ray(new Vector3( (p.x + initCenter.x), colliderCenterY, colliderCenterZ), new Vector3(0, 0, directionZ));	
			if(Physics.Raycast(rayZCenter, rayZDist, collisionMask)){
				deltaZ = 0;
			}
			rayZRight = new Ray(new Vector3( (p.x + initCenter.x + initSize.x/2), colliderCenterY, colliderCenterZ), new Vector3(0, 0, directionZ));	
			if(Physics.Raycast(rayZRight, rayZDist, collisionMask)){
				deltaZ = 0;
			}

			Debug.DrawRay(rayZLeft.origin, rayZLeft.direction * rayZDist);
			Debug.DrawRay(rayZCenter.origin, rayZCenter.direction * rayZDist);
			Debug.DrawRay(rayZRight.origin, rayZRight.direction * rayZDist);
		}

		Vector3 finalTransform = new Vector3(deltaX, deltaY, deltaZ);
		transform.Translate(finalTransform);
	}
}
