using UnityEngine;
using System.Collections;

public class moveSoundScript : MonoBehaviour {
		
		// audio clip references
		[SerializeField] AudioClip[] footstepSounds;		// an array of footstep sounds that will be randomly selected from.
		[SerializeField] AudioClip jumpSound;				// the sound played when character leaves the ground.
		[SerializeField] AudioClip landSound;				// the sound played when character touches back on ground.
		
		// private vars:
		FirstPersonCharacter character;						// a reference to the First Person Character component (on the parent gameobject)
		Vector3 originalLocalPos;							// the original local position of this gameobject at Start
		
		float nextStepTime = 0.5f;									// the time at which the next footstep sound is due to occur
		float headBobCycle = 0;								// the current position through the headbob cycle
		float headBobFade = 0;								// the current amount to which the head bob position is being applied or not (it is faded out when the character is not moving)
		
		
		// Fields for simple spring calculation:
		float springPos = 0;
		float springVelocity = 0;
		float springElastic = 1.1f;		
		float springDampen = 0.8f;
		float springVelocityThreshold = 0.05f;
		float springPositionThreshold = 0.05f;
		
		
		Vector3 prevPosition;								// the position from last frame
		Vector3 prevVelocity = Vector3.zero;				// the velocity from last frame
		bool prevGrounded = true;							// whether the character was grounded last frame
		

		
		// Use this for initialization
		void Start () {
			character = GetComponent<FirstPersonCharacter>();
			if (audio == null)
			{
				// we automatically add an audiosource, if one has not been manually added.
				// (if you want to control the rolloff or other audio settings, add an audiosource manually)
				gameObject.AddComponent<AudioSource>();
			}
			prevPosition = rigidbody.position;
		}			
		
		// Update is called once per frame
		void FixedUpdate () {
			
			// we use the actual distance moved as the velocity since last frame, rather than reading
			//the rigidbody's velocity, because this prevents the 'running against a wall' effect.
			Vector3 velocity = (rigidbody.position - prevPosition) / Time.deltaTime;
			Vector3 velocityChange = velocity - prevVelocity;
			prevPosition = rigidbody.position;
			prevVelocity = velocity;
			
			// vertical head position "spring simulation" for jumping/landing impacts
			springVelocity -= velocityChange.y;							// input to spring from change in character Y velocity
			springVelocity -= springPos*springElastic;					// elastic spring force towards zero position
			springVelocity *= springDampen;								// damping towards zero velocity
			springPos += springVelocity * Time.deltaTime;				// output to head Y position
			springPos = Mathf.Clamp( springPos, -.3f, .3f );			// clamp spring distance
			
			// snap spring values to zero if almost stopped:
			if (Mathf.Abs(springVelocity) < springVelocityThreshold && Mathf.Abs (springPos) < springPositionThreshold)
			{
				springVelocity = 0;
				springPos = 0;
			}
			
			// head bob cycle is based on "flat" velocity (i.e. excluding Y)
			float flatVelocity = new Vector3(velocity.x,0,velocity.z).magnitude;
			
			// actual bobbing and swaying values calculated using Sine wave
			float bobFactor = Mathf.Sin(headBobCycle*Mathf.PI*2); 
			float bobSwayFactor = Mathf.Sin(headBobCycle*Mathf.PI*2 + Mathf.PI*.5f); // sway is offset along the sin curve by a quarter-turn in radians
			bobFactor = 1-(bobFactor*.5f+1); // bob value is brought into 0-1 range and inverted
			bobFactor *= bobFactor;	// bob value is biased towards 0

			headBobCycle += ((flatVelocity) * (Time.deltaTime))/4;

			// fade head bob effect to zero if not moving
			if (new Vector3(velocity.x,0,velocity.z).magnitude < 0.1f)
			{
				headBobFade = Mathf.Lerp(headBobFade,0,Time.deltaTime);
			} else {
				headBobFade = Mathf.Lerp(headBobFade,1,Time.deltaTime);
			}
			
			
			// Play audio clips based on leaving ground/landing and head bob cycle
			if (character.grounded )
			{
				if (!prevGrounded)
				{
					audio.clip = landSound;
					audio.Play();
					nextStepTime = headBobCycle + .5f;
					
				} else {
					
					if ( headBobCycle > nextStepTime)
					{
						// time for next footstep sound:
						
						nextStepTime = headBobCycle + .5f;
						
						// pick & play a random footstep sound from the array,
						// excluding sound at index 0
						int n = Random.Range(1,footstepSounds.Length);
						audio.clip = footstepSounds[n];
						audio.Play();
						
						// move picked sound to index 0 so it's not picked next time
						footstepSounds[n] = footstepSounds[0];
						footstepSounds[0] = audio.clip;
						
					}
				}
				prevGrounded = true;
				
			} else {
				
				if (prevGrounded)
				{
					audio.clip = jumpSound;
					audio.Play();
				}
				prevGrounded = false;
			}
		}
	}
