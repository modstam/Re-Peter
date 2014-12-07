using UnityEngine;
using System.Collections.Generic;

public class TimeLine : MonoBehaviour {

	public Vector3 startPosition;
	public Vector3 endPosition;
	public Material material;
	private LineRenderWrapper timeLine;
	private List<LineRenderWrapper> events = new List<LineRenderWrapper>();
	private float hScale = 3.0f;
	private float wScale = 1.0f;
	public bool isRunning = false;
	// Use this for initialization
	void Start () {
		timeLine = new LineRenderWrapper();
		timeLine.line = gameObject.AddComponent<LineRenderer>();

		if(!timeLine.line || !material){
			Debug.Log("TimeLine: Line couldn't be instantiated");
			return;
		}

		timeLine.line.material = material;
		timeLine.line.material.color = Color.black;
		timeLine.line.SetColors(Color.black, Color.black);
		timeLine.line.SetWidth(0.02F*wScale, 0.02F*wScale);
		timeLine.line.SetVertexCount(2);
		timeLine.line.useWorldSpace = false;

		Mesh mesh = GetComponent<MeshFilter>().mesh;
		LineRenderWrapper line = new LineRenderWrapper();
		line.gameObject = new GameObject("GreenLine");
		line.gameObject.transform.parent = this.transform;
		line.line = line.gameObject.AddComponent<LineRenderer>();
		line.line.material = material;
		line.line.material.color = Color.green;
		line.line.SetWidth(0.1F*wScale, 0.1F*wScale);
		line.line.SetVertexCount(2);
		line.line.useWorldSpace = false;
		line.start = new Vector3(-2.5f , transform.position.y+(mesh.bounds.max.y/hScale+0.25f), transform.position.z-0.65f);
		line.end = new Vector3(-2.5f, transform.position.y-(mesh.bounds.max.y/hScale+0.25f), transform.position.z-0.65f);
		line.line.SetColors(Color.green, Color.green);
		line.line.SetPosition(0,line.start);
		line.line.SetPosition(1,line.end);

	}

	public void init(){
		Start();
	}
	
	// Update is called once per frame
	void Update () {
	
		updateEvents();
		drawEvents();
		drawTimeLine();

	}
	
	public void placeEvent(string e, float time){
		isRunning = true;
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		LineRenderWrapper line = new LineRenderWrapper();
		line.gameObject = new GameObject("Line");
		line.gameObject.transform.parent = this.transform;
		line.line = line.gameObject.AddComponent<LineRenderer>();
		if(line.line && line.gameObject){
			line.line.material = material;
			line.line.material.color = Color.red;
			line.line.SetWidth(0.1F*wScale, 0.1F*wScale);
			line.line.SetVertexCount(2);
			line.line.useWorldSpace = false;
			line.start = new Vector3(-2.5f + time , transform.position.y+(mesh.bounds.max.y/hScale), -0.6f);
			line.end = new Vector3(-2.5f + time, transform.position.y-(mesh.bounds.max.y/hScale), -0.6f);
			line.line.SetColors(Color.black, Color.black);
			events.Add (line);
		}
	}

	void updateEvents(){
		Mesh mesh = GetComponent<MeshFilter>().mesh;


		foreach(LineRenderWrapper line in events){
			line.start.x -= Time.deltaTime;
			line.end.x -= Time.deltaTime;

//			line.start.x = mesh.bounds.min.y;
//			line.end.x = mesh.bounds.min.y;

			line.start.y = transform.position.y+(mesh.bounds.max.y/hScale);
			line.end.y = transform.position.y-(mesh.bounds.max.y/hScale);
		}
	}

	void drawEvents(){
		List<LineRenderWrapper> linesToRemove = new List<LineRenderWrapper>();
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		foreach(LineRenderWrapper line in events){
			line.line.SetPosition(0,line.start);
			line.line.SetPosition(1,line.end);

			if(line.start.x > 2.5f){
				line.gameObject.SetActive(false);
			}
			else{
				line.gameObject.SetActive(true);
			}

			if(line.start.x < -2.5f){
				linesToRemove.Add (line);
			}
		}
		foreach(LineRenderWrapper line in linesToRemove){
			events.Remove (line);
			Destroy(line.gameObject);
		}
	}

	void drawTimeLine(){


//		startPosition = new Vector3(renderer.bounds.min.x, transform.position.y, transform.position.z-0.5f);
//		endPosition = new Vector3(renderer.bounds.max.x, transform.position.y, transform.position.z-0.5f);

		Mesh mesh = GetComponent<MeshFilter>().mesh;
		startPosition = new Vector3(mesh.bounds.min.x, 0, -0.5f);
		endPosition = new Vector3(mesh.bounds.max.x, 0, -0.5f);

		timeLine.line.SetPosition(0,startPosition);
		timeLine.line.SetPosition(1,endPosition);
	}

	public void reset(){
		isRunning = false;
		List<LineRenderWrapper> linesToRemove = new List<LineRenderWrapper>();
		foreach(LineRenderWrapper line in events){
			linesToRemove.Add (line);
		}
		foreach(LineRenderWrapper line in linesToRemove){
			events.Remove (line);
			Destroy(line.gameObject);
		}
		events.Clear ();
	}
}
