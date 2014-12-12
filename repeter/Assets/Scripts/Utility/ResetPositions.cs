using UnityEngine;
using System.Collections;

public class ResetPositions : MonoBehaviour {


	// Use this for initialization
	void Start () {
		foreach (Transform child in transform) {
			//child.position;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ResetAllPositions() {
	}
}
