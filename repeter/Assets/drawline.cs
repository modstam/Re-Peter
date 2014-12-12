using UnityEngine;
using System.Collections;

public class drawline : MonoBehaviour {
	public Material mat;
	private Vector3 startVertex;
	private Vector3 mousePos;
	private Vector3 lastVec;
	private int last = 0;
	void Update() {

		mousePos = Input.mousePosition;
		if (last > 25){
			startVertex = new Vector3(lastVec.x, lastVec.y, lastVec.z);
			
		}
		lastVec = new Vector3(mousePos.x,mousePos.y, 0);
	}
	void OnPostRender() {
		if (!mat) {
			Debug.LogError("Please Assign a material on the inspector");
			return;
		}
		GL.PushMatrix();
		mat.SetPass(0);
		GL.LoadOrtho();
		GL.Begin(GL.LINES);
		GL.Color(Color.red);
		GL.Vertex(startVertex);
		//GL.Vertex(new Vector3(0,0, 0));
		GL.Vertex(new Vector3(mousePos.x / Screen.width, mousePos.y / Screen.height, 0));

		GL.End();
		GL.PopMatrix();
	}
	void Example() {
		startVertex = new Vector3(0, 0, 0);
	}
}