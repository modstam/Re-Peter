using UnityEngine;
using System.Collections;

public class loadLevel1 : MonoBehaviour {


	public float fadeSpeed = 1.5f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other) {
		Debug.Log("completed level");
		Application.LoadLevel(0); //TODO change this
	}

}
