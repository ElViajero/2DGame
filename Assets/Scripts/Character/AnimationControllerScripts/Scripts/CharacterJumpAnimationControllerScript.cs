using UnityEngine;
using System.Collections;

public class CharacterJumpAnimationControllerScript : MonoBehaviour, IAnimationController {

	Animator animatorInstance;
	CharacterMovementControllerScript characterMovementControllerScriptInstance;
	bool isJumpComplete;

	public Transform groundCheckTransform;
	public LayerMask whatIsGround;
	int count=50;

	// Use this for initialization
	void Start () {
		CharacterAnimationController.RegisterAnimationController(this);
		animatorInstance  = GetComponent<Animator>();
		characterMovementControllerScriptInstance = GetComponent<CharacterMovementControllerScript>();	
		isJumpComplete=true;
	}
	
	
	public void HandleAnimation ()
	{	

		float inputValue = Input.GetAxis("Vertical");
		if(inputValue>0 && isJumpComplete){
			count=100;
			isJumpComplete=false;
			animatorInstance.SetTrigger("jump");
			animatorInstance.SetBool("isJumpComplete",false);
			animatorInstance.SetBool("isGrounded",false);
			//characterMovementControllerScriptInstance.isCharacterJumping=true;
		}

		if(!isJumpComplete){// guard condition in case the jump state is active for too long.
			count--;//this counter is used everywhere. I know it's not really a great choice.
			//but animations are tricky and it's more of a trial and error thing than anything else.
			if(count<=0)
				CompleteJump();
		}

		//Debug.Log ("values are :"+ inputValue+","+isJumpComplete );

			
	}

	//check for collision.
	public void OnCollisionEnter2D(Collision2D other){
		CompleteJump();

	}

	private void CheckIfGrounded(){

		bool grounded =Physics2D.OverlapCircle(groundCheckTransform.position,0.2f,whatIsGround);
		if(grounded){
			Debug.Log("inside checkGround true");
			isJumpComplete=true;
			animatorInstance.SetBool("isGrounded",true);
		}

	}



	// This function completes the jump.
	private void CompleteJump(){
		if(!isJumpComplete){
			isJumpComplete=true;
			animatorInstance.SetBool("isJumpComplete",true);
			animatorInstance.SetBool("isGrounded",true);
			characterMovementControllerScriptInstance.isCharacterJumping=false;
			rigidbody2D.velocity = new Vector2(0,0);
		}
	}

}
