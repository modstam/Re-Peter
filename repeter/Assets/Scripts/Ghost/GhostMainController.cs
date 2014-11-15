using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GhostMainController : MonoBehaviour {

	public List<State> states;
	public int stop;
	public int nextElem;
	public float currentTime = 0.0f;
	public float locationPrecision = 0.0f;
	public State nextState;
	public float nextTimeGoal = 0.0f;
	public bool init = false;


	// Use this for initialization
	void Start () {
	}

	public void Initialize(List<State> states, int startState, int endState, float startTime){
		this.states = states;
		this.nextState = states[0];
		this.stop = endState;
		this.nextTimeGoal = nextState.stateTime;
		this.currentTime = startTime;
		nextElem = startState;
		Debug.Log("Ghost was initialized");
		init = true;
	}
	
	// Update is called once per frame
	void Update () {

		if(init && nextElem < stop+1){
		Vector3 diff = nextState.getPosition()-this.transform.position;
		diff.x = Mathf.Abs (diff.x);
		diff.y = Mathf.Abs (diff.y);
		diff.z = Mathf.Abs (diff.z);

		if(!(diff.x < locationPrecision && diff.y < locationPrecision && diff.z < locationPrecision)){
			//if(currentTime >= nextState.stateTime){
				//GhostMovement movementComponent = GetComponent<GhostMovement>();
				
			//		if(movementComponent){ //sanity check
			//		Vector3 direction = nextState.getPosition() - this.transform.position;
					//direction = transform.TransformDirection(direction);
					//movementComponent.MoveTowards(direction, nextState.jump);
					//this.transform.position = new Vector3(Mathf.Lerp (nextState.getPosition().x, this.transform.position.x, 0.5f),
					//	                                  Mathf.Lerp (nextState.getPosition().y, this.transform.position.y, 0.5f),
					//	                                      Mathf.Lerp (nextState.getPosition().z, this.transform.position.z, 0.5f));
			//	}else Debug.Log ("Ghost doesnt have a movement component");
				
			//}//else Debug.Log("Something went horribly wrong");


				this.transform.position = new Vector3(Mathf.Lerp (nextState.getPosition().x, this.transform.position.x, Time.deltaTime ),
				                                      Mathf.Lerp (nextState.getPosition().y, this.transform.position.y, Time.deltaTime),
				                                      Mathf.Lerp (nextState.getPosition().z, this.transform.position.z, Time.deltaTime));

				this.transform.rotation= Quaternion.Lerp( this.transform.rotation ,nextState.getRotation(), Time.deltaTime );
		                                    
				//Time.deltaTime
		}
		else{

			if(nextElem != stop){
				if(currentTime >= nextState.stateTime){
					this.transform.position = nextState.getPosition();
					//this.transform.rotation = nextState.getRotation();

					this.nextState = states[nextElem];
					this.nextTimeGoal = nextState.stateTime;
					nextElem++;
					
					//Debug.Log ("Location reached!");
				}
					//else Debug.Log("Something went horribly wrong2");
			}
			else{

				Debug.Log ("End of queue");
				Destroy(gameObject);
			}
		}


		currentTime += Time.deltaTime;
		}
	}


	
	void OnCollisionEnter(Collision other){
		Debug.Log("Entered collision with  " + other.gameObject.name);
		if((other.gameObject.name == "First Person Character") || (other.gameObject.name == "Ghost")){
			Physics.IgnoreCollision(gameObject.collider, other.gameObject.collider, true);
		}
		
	}


	
}
