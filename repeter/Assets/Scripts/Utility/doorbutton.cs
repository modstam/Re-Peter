using UnityEngine;
using System.Collections;

public class doorbutton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	void OnTriggerEnter(Collider other) {
		//Destroy(other.gameObject);
		//Debug.Log("Collided with button");
		Debug.Log(gameObject.name + " stepped on the button");
		foreach (Transform child in transform)
			child.gameObject.SetActive(false);
	}


	void OnTriggerExit(Collider other) {
		Debug.Log("leaving button");
		gameObject.SetActiveRecursively(true);
		gameObject.SetActive(true);
	}
}
