using UnityEngine;
using System.Collections;

public class CharacterArmMovementControllerScript : MonoBehaviour {
	

	public bool isReadyToFire; // This is a flag used by an attack.
	
	CharacterMovementControllerScript characterMovementControllerInstance;
	GameObject arm;

	// Theses are correction coordinates for 
	float xCorrect;//correcting the position of the arm
	float yCorrect;// after rotation.


	Vector3 armPos; //position vector for the arm.


	// Use this for initialization
	void Start () {
	
		isReadyToFire=false;
		characterMovementControllerInstance = GetComponent<CharacterMovementControllerScript>();
		arm=GameObject.FindGameObjectWithTag("Arm");

	}



	void FixedUpdate () {

		float inputValue = Input.GetAxis("Fire1"); // the axis which is responsible for arm extension.
		CalibrateCorrectionCoordinates(); // check for left,right facing.

		if(inputValue>0 && !isReadyToFire){// if the arm isn't extended.
				
				arm.transform.Rotate(new Vector3(0,0,90),Space.World); // extend it.
				Vector3 currentPos = arm.transform.position;
				currentPos.y+=yCorrect;//correct the position.
				currentPos.x+=xCorrect;
				arm.transform.position=currentPos;				
				armPos = currentPos;
				isReadyToFire=true;// set the attack flag to true.
		}

		else if(inputValue<=0 && isReadyToFire){

			arm.transform.Rotate(new Vector3(0,0,-90),Space.World); // rotate it back (i.e. arms by the side).
			Vector3 currentPos = arm.transform.position;
			currentPos.y-=yCorrect;//correct the position.
			currentPos.x-=xCorrect;
			arm.transform.position=currentPos;
			isReadyToFire=false;//set the attack flag to false.
		}

	}




	// This function shifts the position of 
	// the arm before and after rotation.
	// There are a few subtle shifts when the character is moving vs. stationary.
	// Again, the values here are determined via hit and trial.
	private void CalibrateCorrectionCoordinates(){

		if(characterMovementControllerInstance.facingRight)
			xCorrect=.3f;
		else 
			xCorrect=-.3f; // x axis negates.
		if(!isReadyToFire){
		if(characterMovementControllerInstance.isCharacterMoveable)
			yCorrect=1.2f;
		else
			yCorrect=1;
		}

	}





}
