using UnityEngine;
using System.Collections;

public class collidercontroller : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter(Collider other){


		Debug.Log ("entered trigger area");
	}
}
