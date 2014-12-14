using UnityEngine;
using System.Collections;

public class win : MonoBehaviour {


	//use sound effects
	public AudioClip[] audioClip;
	
	void PlaySound(int clip)
	{
		audio.clip = audioClip [clip];
		audio.Play ();
	}
	


	void OnTriggerEnter(Collider other) {
		
		if (other.transform.tag == "Player") {
			PlaySound(0);
		}
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
