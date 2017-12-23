using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public AudioSource rawTrack1;
	public AudioSource rawTrack2;
	public AudioSource rawTrack3;
	public AudioSource rawTrack4;
	public AudioSource rawTrack5;
	
	public AudioSource modernTrack1;
	public AudioSource modernTrack2;
	public AudioSource modernTrack3;
	public AudioSource modernTrack4;
	public AudioSource modernTrack5;

	private static Queue rawQueue;
	private static Queue modernQueue;

	void Awake(){
		modernQueue = new Queue();
		rawQueue = new Queue();
		EnqueueStartingSounds();
	}
	
	void Start(){
		StartSound(modernTrack4);
		StartSound(modernTrack1);
		StartSound(modernTrack3);
		StartSound(modernTrack2);
		StartSound(modernTrack5);
	}
	
	void EnqueueStartingSounds(){
		modernQueue.Enqueue(modernTrack1);
		modernQueue.Enqueue(modernTrack2);
		modernQueue.Enqueue(modernTrack3);
		modernQueue.Enqueue(modernTrack4);
		modernQueue.Enqueue(modernTrack5);

		rawQueue.Enqueue(rawTrack1);
		rawQueue.Enqueue(rawTrack2);
		rawQueue.Enqueue(rawTrack3);
		rawQueue.Enqueue(rawTrack4);
		rawQueue.Enqueue(rawTrack5);
	}

	public static void SwapForRawSound(){
		if(!CheckIfSetIsPlaying("raw")){
			AudioSource soundToRemove = null;
			AudioSource soundToAdd = null;
			
			soundToRemove = modernQueue.Dequeue() as AudioSource;
			soundToAdd = rawQueue.Dequeue() as AudioSource;
			
			StopSound(soundToRemove);
			StartSound(soundToAdd);
			
			//Requeue dequeued sounds for next time
			modernQueue.Enqueue(soundToRemove);
			rawQueue.Enqueue(soundToAdd);
		}
	}

	public static void SwapForModernSound(){
		if(!CheckIfSetIsPlaying("modern")){
			AudioSource soundToRemove = null;
			AudioSource soundToAdd = null;
			
			soundToRemove = rawQueue.Dequeue() as AudioSource;
			soundToAdd = modernQueue.Dequeue() as AudioSource;

			StopSound(soundToRemove);
			StartSound(soundToAdd);
			
			//Requeue dequeued sounds for next time
			rawQueue.Enqueue(soundToRemove);
			modernQueue.Enqueue(soundToAdd);
		}
	}

	private static bool CheckIfSetIsPlaying(string set){
		if(set == "raw"){
			AudioSource[] tracks = new AudioSource[rawQueue.Count];
			for(int i = 0; i < rawQueue.Count; i++){
				tracks[i] = rawQueue.Dequeue() as AudioSource;
				rawQueue.Enqueue(tracks[i]);
			}
			for(int i = 0; i < tracks.Length; i++){
				if(!tracks[i].isPlaying){
					return false;
				}
			}
			return true;
		}else{
			AudioSource[] tracks = new AudioSource[modernQueue.Count];
			for(int i = 0; i < modernQueue.Count; i++){
				tracks[i] = modernQueue.Dequeue() as AudioSource;
				modernQueue.Enqueue(tracks[i]);
			}
			for(int i = 0; i < tracks.Length; i++){
				if(!tracks[i].isPlaying){
					return false;
				}
			}
			return true;
		}
	}
	
	static void StopSound(AudioSource soundToStop){
		soundToStop.Stop();
	}
	
	static void StartSound(AudioSource soundToStart){
		soundToStart.time = 0;
		soundToStart.time = Metronome.metronomePos;
		soundToStart.Play();
		soundToStart.loop = true;
	}
}
