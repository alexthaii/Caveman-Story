using UnityEngine;
using System.Collections;

public class DrawRay : MonoBehaviour {

	public static float calculateXPosition(Ray ray, RaycastHit hit, float skin, float dir, float delta){
		float distance = Vector3.Distance(ray.origin, hit.point); 
		if(distance < skin){
			delta = distance * dir + skin;	
		}
		else{
			delta = 0;	
		}
		return delta;
	}
	
	public static float calculateYPosition(Ray ray, RaycastHit hit, float skin, float dir, float delta){
		float distance = Vector3.Distance(ray.origin, hit.point);
			if(distance > skin){
				delta = distance * dir + skin;	
			}
			else{
				delta = 0;	
			}
		return delta;
	}
}
