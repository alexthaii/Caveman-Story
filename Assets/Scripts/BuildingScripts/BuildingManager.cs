using UnityEngine;
using System.Collections;

public class BuildingManager : MonoBehaviour {

	public GameObject bamText;
	public AudioSource earthquake;
	public ParticleSystem buildingHitPS;
	public ParticleSystem buildingDestroyedPS;

	private GameManager gameManager;

	private int timesHit;
	private Vector3 originalWorld;

	void Start(){
		gameManager = GameManager.GetGameManager();
		originalWorld = transform.position;
	}

	void Update(){
		if(timesHit >= 5){
			StartCoroutine("BuildingDestroyedShake");
			gameManager.GameOver();
			timesHit = 0;
		}
	}

	public void BuildingPieceHit(GameObject buildingPiece, string hitFrom){
		Vector3 piecePos = buildingPiece.transform.position;
		GameObject bamTextInstance = Instantiate(bamText, new Vector3(piecePos.x + Random.Range(0, 1), piecePos.y + Random.Range(0, 1), 0), Quaternion.identity) as GameObject;
		Destroy(bamTextInstance, 1f);
		ParticleSystem psInstance = Instantiate(buildingHitPS, piecePos, Quaternion.identity) as ParticleSystem;
		psInstance.transform.parent = buildingPiece.transform;
		psInstance.Play();
		SoundManager.SwapForRawSound();
		timesHit++;
		StartCoroutine(BuildingHitShake(hitFrom));
	}

	IEnumerator BuildingHitShake(string shakeDirection) {
		float duration = 0.5f;
		float magnitude = 0.75f;
		float elapsed = 0.0f;
		Vector3 originalCamPos = Camera.main.transform.position;
		
		while (elapsed < duration) {
			transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y - 10, transform.position.z), duration/2 * Time.deltaTime);
			elapsed += Time.deltaTime;          
			
			float percentComplete = elapsed / duration;         
			float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

			float x = 0;
			if(shakeDirection.Equals("left")){
				x = Random.value;
			}else{
				x = Random.value - 1.0f;
			}
			x *= magnitude * damper;
			
			Camera.main.transform.position = new Vector3(originalCamPos.x + x, originalCamPos.y, originalCamPos.z);
			
			yield return null;
		}
		Camera.main.transform.position = originalCamPos;
	}

	IEnumerator BuildingDestroyedShake() {
		ParticleSystem destroyedPS = Instantiate(buildingDestroyedPS, transform.position, Quaternion.identity) as ParticleSystem;
		destroyedPS.Play();
		earthquake.Play();

		float duration = 5f;
		float magnitude = 0.05f;
		float elapsed = 0.0f;
		Vector3 originalCamPos = Camera.main.transform.position;
		Vector3 originalCamRot = Camera.main.transform.eulerAngles;

		GameObject cave = GameObject.Find("Cave");
		while (elapsed < duration) {
			transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y - 10, transform.position.z), duration/2 * Time.deltaTime);
			cave.transform.position = Vector3.MoveTowards(cave.transform.position, originalWorld, duration/2 * Time.deltaTime);
			elapsed += Time.deltaTime;          
			
			float percentComplete = elapsed / duration;         
			float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);
			
			// map noise to [-1, 1]
			float x = Random.value * 2.0f - 1.0f;
			float y = Random.value * 2.0f - 1.0f;
			float z = Random.value * 2.0f - 1.0f;
			x *= magnitude * damper;
			y *= magnitude * damper;
			z *= magnitude * damper;
			
			Camera.main.transform.position = new Vector3(originalCamPos.x + x, originalCamPos.y + y, originalCamPos.z);
			Camera.main.transform.eulerAngles = new Vector3(originalCamRot.x, originalCamRot.y + y * 2, originalCamRot.z);
			
			yield return null;
		}
		earthquake.Stop();
		Camera.main.transform.position = originalCamPos;
		Camera.main.transform.eulerAngles = originalCamRot;
	}
}
