using UnityEngine;
using System.Collections;

public class SceneFadeInOut : MonoBehaviour {

	public float fadeSpeed = 1.5f;
	private bool sceneStarting = false;
	private bool isFading = false;


	void Awake(){
		guiTexture.pixelInset = new Rect (0f,0f, Screen.width, Screen.height);
		guiTexture.enabled = false;
	}

	void Update(){
		if(!isFading){
			if(Input.GetKeyDown(KeyCode.E)){
				isFading = true;
			}
		}
		else{
			if(!sceneStarting){
				EndScene();	
			}
			else{
				StartScene();
			}
		}

	}

	void FadeToClear(){
		guiTexture.color = Color.Lerp (guiTexture.color, Color.clear, fadeSpeed * Time.deltaTime);
	}

	void FadeToBlack(){
		guiTexture.color = Color.Lerp (guiTexture.color, Color.black, fadeSpeed * Time.deltaTime);
	}

	void StartScene(){
		FadeToClear();
		isFading = true;

		if(guiTexture.color.a <= 0.05f){
			guiTexture.color = Color.clear;
			guiTexture.enabled = false;
			sceneStarting = false;
			isFading = false;
			//guiTexture.color.a = 1.0f;
		}
	}

	public void EndScene(){

		guiTexture.enabled = true;
		guiTexture.color = Color.black;
		FadeToBlack ();

		if(guiTexture.color.a >= 0.95f){
			sceneStarting = true;

		}

	}
}
