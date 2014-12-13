using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GhostMainController : MonoBehaviour {

	public List<State> states;
	public GameObject animatedModel;
	public Animator anim;
	public float renderDistance = 3.0f;
	public int stop;
	public int nextElem;
	public float currentTime = 0.0f;
	public float locationPrecision = 0.0f;
	public State nextState;
	public float nextTimeGoal = 0.0f;
	public bool init = false;


	// Use this for initialization
	void Start () {
		if(animatedModel){
			anim = animatedModel.GetComponent<Animator>();
		}
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

		//Handle rendering
		renderSwitch ();
				
		if(init){

			//Handle animation
			updateAnimations();

			if(nextElem != stop){
				if(currentTime >= nextState.stateTime){
					this.transform.position = nextState.getPosition();
					//this.transform.rotation = nextState.getRotation();
					this.transform.localEulerAngles = new Vector3(0.0f,nextState.getRotationEuler().y, 0.0f);
					//Debug.Log(nextState.getRotation().eulerAngles.x + ", \t" + nextState.getRotation().eulerAngles.y + ",\t" + nextState.getRotation().eulerAngles.z);

					this.nextState = states[nextElem];
					this.nextTimeGoal = nextState.stateTime;
					nextElem++;
					
					//Debug.Log ("Location reached!");
				}else{

					this.transform.position = new Vector3(Mathf.Lerp (nextState.getPosition().x, this.transform.position.x, Time.deltaTime ),
							                                      Mathf.Lerp (nextState.getPosition().y, this.transform.position.y, Time.deltaTime),
							                                      Mathf.Lerp (nextState.getPosition().z, this.transform.position.z, Time.deltaTime));
														
					//this.transform.rotation= Quaternion.Lerp( this.transform.rotation ,nextState.getRotation(), Time.deltaTime );
				}

			}
			else{

				Debug.Log ("End of queue");
				Destroy(gameObject);
			}

		currentTime += Time.deltaTime;
		}
	}

	void updateAnimations(){
		if(anim){
			Vector3 velocity = new Vector3(nextState.velocity.x, 0.0f , nextState.velocity.z);
			//Debug.Log(velocity.magnitude);
			//velocity.Normalize();
			//Debug.Log(velocity.magnitude*Time.deltaTime);
			anim.SetFloat ("Speed", velocity.magnitude);
			anim.SetBool ("Jump", nextState.jump);
			anim.SetBool ("isGrounded", nextState.grounded);
		}
	}

	
	void OnCollisionEnter(Collision other){
		Debug.Log("Entered collision with  " + other.gameObject.name);
		if((other.gameObject.name == "First Person Character") || (other.gameObject.name == "Ghost")){
			Physics.IgnoreCollision(gameObject.collider, other.gameObject.collider, true);
		}
		
	}


	private void renderSwitch(){
		Transform camera = GameObject.Find ("First Person Camera").transform;
		if(camera){
			float distance = Vector3.Distance(camera.position, this.transform.position);


			if(distance < renderDistance){

				Renderer[] renderers = GetComponentsInChildren<Renderer>();									
				foreach(Renderer renderer in renderers){
					Material[] materials = renderer.materials;
					foreach(Material material in materials){
						Color color = material.color;
						color.a = 1.0f- (1.0f/distance);
						material.color = color;
						//renderer.enabled = false;
					}	
				}

			}
			else if(distance > renderDistance && distance < renderDistance + 2){
				Renderer[] renderers = GetComponentsInChildren<Renderer>();		
				foreach(Renderer renderer in renderers){
					Material[] materials = renderer.materials;
					foreach(Material material in materials){
						Color color = material.color;
						color.a = 0.45f;
						material.color = color;
						//renderer.enabled = true;
					}	
				}	
			}
		}
	}


	
}
