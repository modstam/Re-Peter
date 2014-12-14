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

	public void postTimeLine(int start, int end, float startTime){
		StateRecorder stateRecorder = GameObject.Find("First Person Character").GetComponent<StateRecorder>();
		states = stateRecorder.getStates();
		foreach(GameObject gameobj in timeLines){
			TimeLine timeLine = gameobj.GetComponent<TimeLine>();
			if(!timeLine.isRunning){
				for(int i = start; i < end; i++){
					State state = states[i];
					if(state.jump){
						timeLine.placeEvent("Jump", Color.red, state.stateTime- startTime);
					}
					if(state.hitTrigger){
						timeLine.placeEvent("Trigger", Color.yellow, state.stateTime- startTime);
					}
				}
				return;
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
