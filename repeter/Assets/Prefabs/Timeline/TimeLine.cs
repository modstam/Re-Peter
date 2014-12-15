using UnityEngine;
using System.Collections.Generic;

public class TimeLine : MonoBehaviour {

	public Vector3 startPosition;
	public Vector3 endPosition;
	public float anchorLeft = 0f;
	public Material material;
	private LineRenderWrapper timeLine;
	private LineRenderWrapper greenLine;
	private List<LineRenderWrapper> events = new List<LineRenderWrapper>();
	public float hScale = 3.0f;
	public bool isRunning = false;
	public float angleRatio;
	public float yAngle;
	// Use this for initialization
	void Start () {
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		timeLine = new LineRenderWrapper();
		timeLine.line = gameObject.AddComponent<LineRenderer>();
		if(!timeLine.line || !material){
			Debug.Log("TimeLine: Line couldn't be instantiated");
			return;
		}
		angleRatio = (1f-(transform.rotation.eulerAngles.y)/90f);
		//anchorLeft = (mesh.bounds.min.x*transform.localScale.x)*angleRatio ;
		yAngle = transform.rotation.eulerAngles.y;
		//anchorLeft = renderer.bounds.min.x;
		timeLine.line.material = material;
		timeLine.line.material.color = Color.black;
		timeLine.line.SetColors(Color.black, Color.black);
		timeLine.line.SetWidth(0.04F, 0.04F);
		timeLine.line.SetVertexCount(2);
		timeLine.line.useWorldSpace = false;

		startPosition = new Vector3(mesh.bounds.min.x, 0, -0.5f);
		endPosition = new Vector3(mesh.bounds.max.x, 0, -0.5f);
		
		timeLine.line.SetPosition(0,startPosition);
		timeLine.line.SetPosition(1,endPosition);


		greenLine = new LineRenderWrapper();
		greenLine.gameObject = new GameObject("GreenLine");
		greenLine.gameObject.transform.parent = this.transform;
		greenLine.gameObject.transform.position = transform.position;
		greenLine.gameObject.transform.rotation = transform.rotation;
		greenLine.line = greenLine.gameObject.AddComponent<LineRenderer>();
		greenLine.line.material = material;
		greenLine.line.material.color = Color.green;
		greenLine.line.SetWidth(0.1F, 0.1F);
		greenLine.line.SetVertexCount(2);
		greenLine.line.useWorldSpace = false;
		greenLine.start = new Vector3(anchorLeft, (mesh.bounds.max.y/hScale+0.50f), -0.65f);
		greenLine.end = new Vector3(anchorLeft, -(mesh.bounds.max.y/hScale+0.50f), -0.65f);
		//line.start = new Vector3(0,1,-0.65f);
		//line.end = new Vector3(0,-1,-0.65f);
		greenLine.line.SetColors(Color.green, Color.green);
		greenLine.line.SetPosition(0,greenLine.start);
		greenLine.line.SetPosition(1,greenLine.end);

	}

	public void init(){
		Start();
	}
	
	// Update is called once per frame
	void Update () {
	
		updateEvents();
		drawEvents();

	}
	
	public void placeEvent(string e, Color color, float time){
		isRunning = true;
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		LineRenderWrapper line = new LineRenderWrapper();
		line.gameObject = new GameObject("Line");
		line.gameObject.transform.parent = this.transform;
		line.gameObject.transform.position = transform.position;
		line.gameObject.transform.rotation = transform.rotation;
		line.line = line.gameObject.AddComponent<LineRenderer>();
		if(line.line && line.gameObject){
			line.line.material = material;
			line.line.material.color = color;
			line.line.SetWidth(0.15F, 0.15F);
			if(e == "OnTrigger"){
				line.line.SetWidth(0.1F, 0.1F);
			}

			line.line.SetVertexCount(2);
			line.line.useWorldSpace = false;
			line.start = new Vector3(anchorLeft + time , +((mesh.bounds.max.y/hScale)), -0.6f);
			line.end = new Vector3(anchorLeft + time, -((mesh.bounds.max.y/hScale)), -0.6f);
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

			line.start.y = +(mesh.bounds.max.y/hScale +0.4f);
			line.end.y = -(mesh.bounds.max.y/hScale + 0.4f);
		}
	}

	void drawEvents(){
		List<LineRenderWrapper> linesToRemove = new List<LineRenderWrapper>();
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		foreach(LineRenderWrapper line in events){
			line.line.SetPosition(0,line.start);
			line.line.SetPosition(1,line.end);

			if(line.start.x > -anchorLeft){
				line.gameObject.SetActive(false);
			}
			else{
				line.gameObject.SetActive(true);
			}

			if(line.start.x < anchorLeft){
				linesToRemove.Add (line);
			}
		}
		foreach(LineRenderWrapper line in linesToRemove){
			events.Remove (line);
			Destroy(line.gameObject);
		}
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
		events = new List<LineRenderWrapper>();
	}
}
