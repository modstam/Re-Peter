using UnityEngine;
using System.Collections;

public class GhostMovement : MonoBehaviour {


	/** Character movement traits */
	public float speed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	
	private Vector3 moveDirection = Vector3.zero;
	

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

		controller.Move(mDirection*10*Time.deltaTime);
		
		
	}
}
