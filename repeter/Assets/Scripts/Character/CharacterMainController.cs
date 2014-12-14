using UnityEngine;
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
	public int limitOfGhosts;
	public int nextSpawnState = 0;	
	public float nextSpawnTime = 0;
	public List<GhostState> ghosts = new List<GhostState>();
	public List<GhostMainController> gcs = new List<GhostMainController>();
	private bool cantSpawn = false;
	public bool hitTriggerThisFrame = false;
	public Color triggerColor = Color.black;
	public bool exitTriggerThisFrame = false;
	

	bool initComplete = false;



	// Use this for initialization
	void Start () {
		setSpawnPoint ();
	}
	
	/**
	 * Main character update loop
	 */
	void Update () {


		if(Input.GetButtonDown("SpawnGhost")){
			Debug.Log(numberOfGhosts + " " + limitOfGhosts);
			if(numberOfGhosts < limitOfGhosts){
				this.transform.rotation = Quaternion.Euler(spawnTransformData.getRotation());
				this.transform.position = spawnTransformData.getPosition();
				spawnGhost();
			} else {
				cantSpawnOn();
				Invoke("cantSpawnOff", 3);
			}
		}

		if(Input.GetButtonDown("ResetGhost")){
			setSpawnPoint();
		}
	
	}

	public void setSpawnPoint(){
		this.spawnTransformData.Clone(this.transform);
		this.nextSpawnState = 0;
		numberOfGhosts = 0;
		clearGhosts ();
		ghosts.Clear ();
		GetComponent<StateRecorder> ().getStates ().Clear ();
	}

	void clearGhosts ()
	{
		foreach(GhostMainController gc in gcs){
			gc.kill();
		}
		gcs.Clear();
	}

	public void spawnGhost(){
		if(ghostPrefab){
			GameObject ghost;
		
			numberOfGhosts++;
			ghosts.Add(new GhostState(nextSpawnState,
			                       	  GetComponent<StateRecorder>().getStates().Count,
			                          GetComponent<StateRecorder>().getStates()[nextSpawnState].stateTime));
			clearGhosts();
			resetTimeLines();
			foreach(GhostState state in ghosts){
				ghost = Instantiate(ghostPrefab, spawnTransformData.getPosition(), Quaternion.Euler (spawnTransformData.getRotation())) as GameObject; 
					if(ghost){
						GhostMainController gc = ghost.GetComponent<GhostMainController>();
						gcs.Add(gc);
						if(gc){ 
							gc.Initialize(GetComponent<StateRecorder>().getStates()
								          ,state.spawnState,
								           state.endState, 
								           state.startTime);

							nextSpawnState = GetComponent<StateRecorder>().getStates().Count;	
							Debug.Log("Successfully spawned ghost");
							
						startTimeLine(state.spawnState, state.endState, state.startTime);

						}else Debug.Log("Coulnd't initialize ghost");

					}
			}
			UpdateCollisions();
		
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

	void OnGUI()
	{
		if ( cantSpawn )
		{
			var centeredStyle = GUI.skin.GetStyle("Label");
			centeredStyle.alignment = TextAnchor.UpperCenter;
			string style = "<color=white><size=20>Maximum number of Ghosts. Press Q to reset.</size></color>";
			GUI.Label(new Rect(Screen.width * 0.05f, Screen.height * 0.8f, Screen.width * 0.9f, Screen.height * 0.9f),style,centeredStyle);

		}
	}

	public void cantSpawnOn() {
		cantSpawn = true;
	}
	public void cantSpawnOff() {
		cantSpawn = false;
	}

	public void startTimeLine(int start, int end, float startTime){
		GameObject timelines = GameObject.Find("TimeLines");
		if(timelines){
			timeLineStarter tlstart = timelines.GetComponent<timeLineStarter>();
			tlstart.postTimeLine(start,end, startTime);
		}
		else{
			Debug.Log ("No 'TimeLines'-object found in scene");
		}
	}

	public void resetTimeLines(){
		GameObject timelines = GameObject.Find("TimeLines");
		if(timelines){
			timeLineStarter tlstart = timelines.GetComponent<timeLineStarter>();
			tlstart.reset ();
		}
		else{
			Debug.Log ("No 'TimeLines'-object found in scene");
		}
	}

	void UpdateCollisions(){
		GameObject[] ghosts = GameObject.FindGameObjectsWithTag("Ghost");

		foreach(GameObject ghost in ghosts){
			Collider capsule = GetComponent<CapsuleCollider>();
			Collider box = GetComponent<BoxCollider>();

			Physics.IgnoreCollision(ghost.collider, capsule, true);
			Physics.IgnoreCollision(ghost.collider, box, true);
		}
	}


	void OnCollisionEnter(Collision c){
		if(c.gameObject.tag.Equals("TriggerBlue")){
			Debug.Log ("Hit trigger");
			hitTriggerThisFrame = true;
			triggerColor = Color.blue;
		}
		if(c.gameObject.tag.Equals("TriggerGreen")){
			Debug.Log ("Hit trigger");
			hitTriggerThisFrame = true;
			triggerColor = Color.green;
		}
	}

	void OnCollisionExit(Collision c){
		if(c.gameObject.tag.Equals("TriggerBlue") || c.gameObject.tag.Equals("TriggerGreen") ){
			Debug.Log ("Exit trigger");
			exitTriggerThisFrame = true;
			hitTriggerThisFrame = false;
		}
	}
	
}
