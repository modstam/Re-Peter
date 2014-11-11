using UnityEngine;
using System.Collections;

public class TransformData{
	float posX;
	float posY;
	float posZ;

	float rotW;
	float rotX;
	float rotY;
	float rotZ;


	public void Clone(Transform tf){

		posX = tf.position.x;
		posY = tf.position.y;
		posZ = tf.position.z;

		rotW = tf.rotation.w;
		rotX = tf.rotation.x;
		rotY = tf.rotation.y;
		rotZ = tf.rotation.z;
	}


	public Vector3 getPosition(){
		return new Vector3(posX,posY,posZ);
	}


	public Quaternion getRotation(){

		return new Quaternion(rotW,rotX,rotY,rotY);
	}
	
}
