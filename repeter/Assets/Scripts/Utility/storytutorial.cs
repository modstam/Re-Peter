using UnityEngine;
using System.Collections;

public class storytutorial : MonoBehaviour {
	TextMesh tm;
	// Use this for initialization
	void Start () {
		tm = gameObject.GetComponentInChildren<TextMesh>();
		StartCoroutine(intro());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator intro(){
		yield return new WaitForSeconds(3);
		string message = "Don't be scared, I will get you out of here";
		tm.text = message;
		yield return new WaitForSeconds(5);
		message = "The emergency power will soon get online";
		tm.text = message;
		yield return new WaitForSeconds(4);
		tm.text = "";
		yield return new WaitForSeconds(4);
		message = "Nothing is true, what you see is not real.";
		tm.text = message;
		yield return new WaitForSeconds(3);
		message = "You are dangerous to them, you can crack their prison";
		tm.text = message;
		yield return new WaitForSeconds(4);
		message = "Behind you I've created a stone that will hack the door";
		tm.text = message;

		//TODO Create loop till player made it
		yield return new WaitForSeconds(4);
		message = "Create a spawning point by pressing 'Q'";
		tm.text = message;
		yield return new WaitForSeconds(5);
		message = "Go to the stone and press 'E' to relive your destiny";
		tm.text = message;
		//TODO include yield 10 seconds

		yield return new WaitForSeconds(5);
		message = "You can see abnormalties in the world by holding down 'shift'";
		tm.text = message;
		yield return new WaitForSeconds(4);
		
		message = "I can't stay here! Escape and i will find you. Good luck!";
		tm.text = message;
		yield return new WaitForSeconds(4);
		tm.text = "";
	}
}
