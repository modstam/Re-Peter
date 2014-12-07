using UnityEngine;
using System.Collections.Generic;

public class timeLineStarter : MonoBehaviour {


	private List<State> states; 
	private List<GameObject> timeLines;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void postTimeLine(int start, int end){
		StateRecorder stateRecorder = GameObject.FindWithTag("First Person Character").GetComponent<StateRecorder>();
		states = stateRecorder.getStates();
	}

	public void reset(){

	}
}
