using UnityEngine;

public class CameraCheck : MonoBehaviour
{
	public GameObject riftCameraPrefab;
	
	void Start()
	{
		riftCameraPrefab.SetActive(OVRManager.display.isPresent);			
	}
}