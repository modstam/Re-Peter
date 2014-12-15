using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

	/** Camera movement traits */
	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;
	
	public float minimumX = -360F;
	public float maximumX = 360F;
	
	public float minimumY = -60F;
	public float maximumY = 60F;

	float rotationY = 0F;


	/** Character movement traits */
	public float speed = 6.0F;
	public float jumpSpeed = 10.0F;
	public float gravity = 20.0F;

	private Vector3 moveDirection = Vector3.zero;



	void Update() {

		UpdateCamera();
		UpdateMovement();

	}

	void UpdateCamera(){
		if (axes == RotationAxes.MouseXAndY)
		{
			float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
			
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			
			transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
		}
		else if (axes == RotationAxes.MouseX)
		{
			transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
		}
		else
		{
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			
			transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
		}
	}

	void UpdateMovement(){

		CharacterController controller = GetComponent<CharacterController>();
		if (controller.isGrounded) {
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;
			if (Input.GetButton("Jump"))
				moveDirection.y = jumpSpeed;
			
		}
		moveDirection.y -= gravity * Time.deltaTime;
		//controller.Move(moveDirection * Time.deltaTime);
		//Debug.Log (moveDirection);
		MoveTowards(moveDirection, false);

	}
	/**
	 * takes a Vector3 holding a direction and moves towards it
	 */
	public void MoveTowards(Vector3 direction, bool jump){
		Vector3 mDirection = direction;
		CharacterController controller = GetComponent<CharacterController>();
		if (controller.isGrounded) {
			mDirection *= speed;
			if (jump)
				mDirection.y = jumpSpeed;
			
		}
		mDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);


	}
}