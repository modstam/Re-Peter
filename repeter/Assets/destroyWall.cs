using UnityEngine;
using System.Collections;

public class destroyWall : MonoBehaviour {

	string message = "Activate by pressing E";
	bool showMessage = false;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnMouseExit() {
		showMessage = false;
	}

	void OnMouseEnter() {
		if(FirstPersonCharacter.seeVirtual)
			showMessage = true;
	}


	void OnMouseOver() {
		if (FirstPersonCharacter.seeVirtual) {
			if (Input.GetKey (KeyCode.E)) {
				GameObject[] gos;
				gos = GameObject.FindGameObjectsWithTag ("Button1");
				for (int i = 0; i < gos.Length; i++) {
					Destroy (gos [i]);
				}
			}
		} else {
			showMessage = false;
		}
	}

	void OnGUI()
	{
		if ( showMessage )
		{
			string style = "<color=white><size=20>"+ message + "</size></color>";
			GUI.Label(new Rect(Screen.width * 0.5f - 100f, Screen.height * 0.5f + 100f, 200f, 40f),style);
		}
	}

}



