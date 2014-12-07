using UnityEngine;
using System.Collections.Generic;

public class TimeLine : MonoBehaviour {

	public Vector3 startPosition;
	public Vector3 endPosition;
	public Material material;
	private LineRenderer timeLine;
	private List<LineRenderer> events;
	// Use this for initialization
	void Start () {
		timeLine = gameObject.AddComponent<LineRenderer>();
		events = new List<LineRenderer>();
		if(!timeLine || !material){
			Debug.Log("TimeLine: Line couldn't be instantiated");
			return;
		}

		timeLine.material = material;
		timeLine.SetColors(Color.black, Color.black);
		timeLine.SetWidth(0.2F, 0.2F);
		timeLine.SetVertexCount(2);


	}
	
	// Update is called once per frame
	void Update () {
		startPosition = new Vector3(renderer.bounds.min.x, transform.position.y, transform.position.z-0.5f);
		endPosition = new Vector3(renderer.bounds.max.x, transform.position.y, transform.position.z-0.5f);

		timeLine.SetPosition(0,startPosition);
		timeLine.SetPosition(1,endPosition);
	}

	void placeEvent(String event){

	}

	void drawEvents(){

	}
}
