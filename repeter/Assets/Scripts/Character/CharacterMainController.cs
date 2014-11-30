﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/**
 * This class should hold the main character logic
 * Movement specific logic should be implemented in the CharacterMovement class
 */

public class CharacterMainController : MonoBehaviour {
	public GameObject ghostPrefab;

	TransformData spawnTransformData = new TransformData();
	public int numberOfGhosts = 0;
	public int nextSpawnState = 0;	
	public float nextSpawnTime = 0;
	public List<GhostState> ghosts = new List<GhostState>();
	

	bool initComplete = false;
	// Use this for initialization
	void Start () {
		setSpawnPoint ();
	}
	
	/**
	 * Main character update loop
	 */
	void Update () {


		if(Input.GetKeyDown(KeyCode.E)){
			this.transform.rotation = Quaternion.Euler(spawnTransformData.getRotation());
			this.transform.position = spawnTransformData.getPosition();
			spawnGhost();
				
		}

		if(Input.GetKeyDown(KeyCode.Q)){
			setSpawnPoint();
		}
	
	}

	public void setSpawnPoint(){
		this.spawnTransformData.Clone(this.transform);
		this.nextSpawnState = 0;
		numberOfGhosts = 0;
		ghosts.Clear ();
		GetComponent<StateRecorder> ().getStates ().Clear ();
	}

	public void spawnGhost(){
		if(ghostPrefab){
			GameObject ghost;

			bool jump = Input.GetButton("Jump");

			ghosts.Add(new GhostState(nextSpawnState,
			                       	  GetComponent<StateRecorder>().getStates().Count,
			                          GetComponent<StateRecorder>().getStates()[nextSpawnState].stateTime));

		
			foreach(GhostState state in ghosts){
				ghost = Instantiate(ghostPrefab, spawnTransformData.getPosition(), Quaternion.Euler (spawnTransformData.getRotation())) as GameObject; 
					if(ghost){
						GhostMainController gc = ghost.GetComponent<GhostMainController>();
						if(gc){ 
							gc.Initialize(GetComponent<StateRecorder>().getStates()
								          ,state.spawnState,
								           state.endState, 
								           state.startTime);

							nextSpawnState = GetComponent<StateRecorder>().getStates().Count;	
							Debug.Log("Successfully spawned ghost");
						}else Debug.Log("Coulnd't initialize ghost");
					}
			}
		
		}
		else Debug.Log("Ghost-prefa\tb not set in CharacterController");
	}


	public class GhostState{
		public int spawnState;
		public int endState;
		public float startTime;

		public GhostState(int spawnState, int endState, float startTime){
			this.spawnState = spawnState;
			this.endState = endState;
			this.startTime = startTime;
		}
			
	}


	
}
