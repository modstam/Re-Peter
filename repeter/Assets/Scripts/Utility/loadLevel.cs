using UnityEngine;
using System.Collections;

public class loadLevel : MonoBehaviour {


	public float fadeSpeed = 1.5f;
	public int levelnum;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other) {
		Debug.Log("completed level");
		Application.LoadLevel(levelnum); //TODO change this
	}

}
