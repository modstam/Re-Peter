using UnityEngine;
using System.Collections;

public class TransformData{
	float posX;
	float posY;
	float posZ;

	float rotX;
	float rotY;
	float rotZ;


	public void Clone(Transform tf){

		posX = tf.position.x;
		posY = tf.position.y;
		posZ = tf.position.z;

		Vector3 eulerAngles = tf.eulerAngles;
		rotX = eulerAngles.x;
		rotY = eulerAngles.y;
		rotZ = eulerAngles.z;
	}


	public Vector3 getPosition(){
		return new Vector3(posX,posY,posZ);
	}


	public Vector3 getRotation(){

		return new Vector3(rotX,rotY,rotY);
	}
	
}
