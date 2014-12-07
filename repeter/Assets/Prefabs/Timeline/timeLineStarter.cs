using UnityEngine;
using System.Collections.Generic;

public class timeLineStarter : MonoBehaviour {


	private List<State> states; 
	public GameObject[] timeLines;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void postTimeLine(int start, int end){
		StateRecorder stateRecorder = GameObject.FindWithTag("First Person Character").GetComponent<StateRecorder>();
		states = stateRecorder.getStates();
		foreach(GameObject gameobj in timeLines){
			TimeLine timeLine = gameobj.GetComponent<TimeLine>();
			if(!timeLine.isRunning){
				foreach(State state in states){
					if(state.jump){
						timeLine.placeEvent("Jump",state.stateTime);
					}
				}
				break;
			}
		}
	}

	public void reset(){
		foreach(GameObject gameobj in timeLines){
			TimeLine timeLine = gameobj.GetComponent<TimeLine>();
			timeLine.reset();
		}
	}
}
