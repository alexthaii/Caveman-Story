using UnityEngine;
using System.Collections;

public class ClimbingArea : MonoBehaviour {

	public GameObject climbZonePrefab;
	
	void Start () {
		Vector3 leftPos = new Vector3(transform.position.x - renderer.bounds.size.x/2, renderer.bounds.center.y, transform.position.z);
		Vector3 rightPos = new Vector3(transform.position.x + renderer.bounds.size.x/2, renderer.bounds.center.y, transform.position.z);
		Vector3 newScale = new Vector3(1f, renderer.bounds.size.y, renderer.bounds.size.z);
		CreateClimbZone("LeftClimbZone", leftPos, newScale);
		CreateClimbZone("RightClimbZone", rightPos, newScale);
	}
	
	void CreateClimbZone(string zoneName, Vector3 newPos, Vector3 newScale){
		GameObject climbZoneObj = Instantiate(climbZonePrefab, 
		                                      new Vector3(0, 0, 0), 
		                                      Quaternion.identity) as GameObject;
		climbZoneObj.transform.parent = transform;
		climbZoneObj.transform.name = zoneName;
		climbZoneObj.transform.tag = zoneName;
		climbZoneObj.transform.position = newPos;
		climbZoneObj.transform.localScale = newScale;
	}
}
