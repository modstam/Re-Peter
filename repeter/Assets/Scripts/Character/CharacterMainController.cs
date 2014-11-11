using UnityEngine;
using System.Collections;


/**
 * This class should hold the main character logic
 * Movement specific logic should be implemented in the CharacterMovement class
 */

public class CharacterMainController : MonoBehaviour {
	public GameObject ghostPrefab;

	TransformData spawnTransformData = new TransformData();
	bool initComplete = false;
	// Use this for initialization
	void Start () {
		this.spawnTransformData.Clone(this.transform);
	}
	
	/**
	 * Main character update loop
	 */
	void Update () {

		if(Input.GetKeyDown(KeyCode.E)){
			spawnGhost();
				
		}
	
	}

	public void spawnGhost(){
		if(ghostPrefab){
			GameObject ghost;
			ghost = Instantiate(ghostPrefab, spawnTransformData.getPosition(), spawnTransformData.getRotation()) as GameObject; 
			if(ghost){
				GhostMainController gc = ghost.GetComponent<GhostMainController>();
				if(gc) gc.Initialize(GetComponent<StateRecorder>().getStates());
				else Debug.Log("Coulnd't initialize ghost");
				Debug.Log("Successfully spawned ghost");
			}	
		}
		else Debug.Log("Ghost-prefab not set in CharacterController");

	}

	/*
	 * Because gameobjects are passed by reference, we need to clone the transform manually 
	 */
	void cloneTransform(){

	}
}
