using UnityEngine;
using System.Collections;

public class CharacterWalkAnimationControllerScript : MonoBehaviour,IAnimationController {


	CharacterMovementControllerScript characterMovementControllerScriptInstance;
	Animator animatorInstance;
	bool isWalking; // these flags correspond to bool flags on the animator.
	bool isReadyToWalk;
	int count;


	// Use this for initialization
	void Start () {
		CharacterAnimationController.RegisterAnimationController(this);
		animatorInstance  = GetComponent<Animator>();
		characterMovementControllerScriptInstance = GetComponent<CharacterMovementControllerScript>();
	}

	// This is what gets called from our 
	// generic CharacterAnimationController.
	public void HandleAnimation ()
	{
		float inputValue = Input.GetAxis("Horizontal");

		//check if input is correct.
		if(inputValue==1 || inputValue==-1){
			if(!isReadyToWalk){ // if he isn't ready, put him into the ready state.
				isReadyToWalk=true;
				animatorInstance.SetTrigger("getReadyToWalk");
				animatorInstance.SetBool("isReadyToWalk",true);
				characterMovementControllerScriptInstance.isCharacterMoveable=false;
			}
			else{// he's ready to walk, start walking.
				isWalking=true;
				animatorInstance.SetBool("isWalking",true);
				count=10;
				characterMovementControllerScriptInstance.isCharacterMoveable=true;
			}

		}
		else{// The input has nothing to do with walking, keep
			// him in ready state for a while and then go back to idle.
			characterMovementControllerScriptInstance.isCharacterMoveable=false;
			isWalking=false;
			animatorInstance.SetBool("isWalking",false);
			count--;
			if(count<=0){//go back to idle when this reaches zero.
				isReadyToWalk=false;
				animatorInstance.SetBool("isReadyToWalk",false);
				isWalking=false; // safe to just reset all flags.
			}
				
		}

	}


}
