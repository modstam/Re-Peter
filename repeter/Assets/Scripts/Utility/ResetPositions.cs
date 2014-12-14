using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/*
 * Drag and drop platforms that should reset on "death"
 * in to the transforms array
 */
public class ResetPositions : MonoBehaviour {	

	Vector3[] posList;
	public Transform[] transforms;

	// Use this for initialization
	void Start () {
		posList = new Vector3[transforms.Length];
		for(int i = 0; i < transforms.Length; i++) {
			posList[i] = transforms[i].position;
		}
		
	}

	public void Update(){
		// This sould be called from another script if you want to call Reset remotley
		//((ResetPositions) GameObject.Find("Dynamic").GetComponent(typeof(ResetPositions))).Reset ();


		if (Input.GetButtonDown ("SpawnGhost")) {
			Debug.Log("posList: " + posList.Length);
			Reset();
			}
		}
	
	public void Reset(){
		for(int i = 0; i < transforms.Length; i++) {
			transforms[i].position = posList[i];
		}
	}	
}