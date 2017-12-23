using UnityEngine;
using System.Collections;

public class Metronome : MonoBehaviour {

	public AudioSource metronome;
	public static float offset;
	public static float halfOffset;
	public static float quarterOffset;
	public static float eighthOffset;
	public static float sixteenthOffset;
	
	public static float metronomePos;
	public static float metronomeLength;
	
	void Awake(){
		metronomeLength = metronome.audio.clip.length;
	}
	
	void Start(){
		Application.targetFrameRate = 60;
		//QualitySettings.vSyncCount = 1;
		
		RunMetronome();	
	}

	void Update(){
		metronomePos = metronome.audio.time;
//		offset = metronomeLength - metronomePos;
//		halfOffset = (metronomeLength / 2) - metronomePos;
//		quarterOffset = (metronomeLength / 4) - metronomePos;
//		eighthOffset = (metronomeLength / 8) - metronomePos;
		sixteenthOffset = (metronomeLength / 32) - metronomePos;
	}

	void FixedUpdate(){
		metronomePos = metronome.audio.time;
//		offset = metronomeLength - metronomePos;
//		halfOffset = (metronomeLength / 2) - metronomePos;
//		quarterOffset = (metronomeLength / 4) - metronomePos;
//		eighthOffset = (metronomeLength / 8) - metronomePos;
		sixteenthOffset = (metronomeLength / 32) - metronomePos;
	}

	void LateUpdate(){
		metronomePos = metronome.audio.time;
//		offset = metronomeLength - metronomePos;
//		halfOffset = (metronomeLength / 2) - metronomePos;
//		quarterOffset = (metronomeLength / 4) - metronomePos;
//		eighthOffset = (metronomeLength / 8) - metronomePos;
		sixteenthOffset = (metronomeLength / 32) - metronomePos;
	}
	
	void RunMetronome(){
		metronome.Play();
		metronome.loop = true;
	}
}
