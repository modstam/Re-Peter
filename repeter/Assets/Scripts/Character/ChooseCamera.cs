using UnityEngine;

public class VRCamera : MonoBehaviour
{
	public Transform riftCameraPrefab;
	
	void Awake()
	{
		// Instantiate OVRCameraRig to detect Oculus Rift
		Transform riftCamera = (Transform) Instantiate(riftCameraPrefab, transform.position, transform.rotation);
		riftCamera.parent = transform;
		
		if (OVRManager.display.isPresent) {
			// Disable normal camera if Rift is connected
			transform.Find("Main Camera").gameObject.SetActive(false);
		} else {
			// Destroy OVRCamraRift if Rift is not connected
			DestroyImmediate(riftCamera.gameObject);
		}
	}
}