using UnityEngine;
using System.Collections;

public class CharacterMovementControllerScript : MonoBehaviour {
	
	public bool facingRight=true; // a flag that tells us whether the character
	//is facing right
	
	private float characterSpeed=5f;
	private float verticalCharacterSpeed=10.0f ;// This is the speed of our
	//character. We assume zero acceleration for now.

	public bool isCharacterMoveable;
	public bool isCharacterJumping=false;
	

	void Start(){
		Debug.developerConsoleVisible=true;

	}


	// Update is called once per frame
	// But this is a variant called FixedUpdate -- @tejas
	void FixedUpdate () {

		float horizontalInputValue=Input.GetAxis("Horizontal");
		float verticalInputValue=Input.GetAxis("Vertical");

		//@ tejas
		// This gets the input. By default
		// value returned is +1 for right key.
		// -1 for left key. See the input manager for more details.

		if(isCharacterMoveable){
		rigidbody2D.velocity=new Vector2(horizontalInputValue*characterSpeed,rigidbody2D.velocity.y);
		}

		else{
			float xVelocity = Mathf.Max(0,rigidbody2D.velocity.x-1);
			rigidbody2D.velocity=new Vector2(xVelocity,rigidbody2D.velocity.y);
		}

		if(verticalInputValue>0 && !isCharacterJumping){
			isCharacterJumping=true;
			float yVelocity = Mathf.Min (8,(verticalCharacterSpeed*verticalInputValue ));
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x,yVelocity);
		}

		//@ tejas
		// check whether the player has changed
		//direction. We should flip the world if so.
		CheckAndFlip(horizontalInputValue);



	}
	
	
	
	/// <summary>
	/// Checks and flips the world if required.
	/// </summary>
	/// <param name="inputValue">Input value.</param>
	void CheckAndFlip(float inputValue){
		
		bool isFlipRequired=false;
		if((facingRight && inputValue<0) ||
		   (!facingRight && inputValue>0))
			isFlipRequired=true;
		
		// perform the flip if required.
		
		if(isFlipRequired){
			
			Vector3 currentLocalScale = transform.localScale;
			currentLocalScale.x*=-1;
			transform.localScale=currentLocalScale;
			facingRight=!facingRight;


		}
	}
}
