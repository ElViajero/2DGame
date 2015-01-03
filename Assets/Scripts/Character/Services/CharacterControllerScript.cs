using UnityEngine;
using System.Collections;


public class CharacterControllerScript : MonoBehaviour {


	private int count;
	private bool facingRight=true; // a flag that tells us whether the character
								//is facing right

	private float characterSpeed=10f; // This is the speed of our
									//character. We assume zero acceleration for now.


	// Use this for initialization
	void Start () {


	} 
	
	// Update is called once per frame
	// But this is a variant called FixedUpdate -- @tejas
	void FixedUpdate () {

		//@ tejas
		// This gets the input. By default
		// value returned is +1 for right key.
		// -1 for left key. See the input manager for more details.
		float inputValue=Input.GetAxis("Horizontal");
		rigidbody2D.velocity=new Vector2(inputValue*characterSpeed,rigidbody2D.velocity.y);

		//@ tejas
		// check whether the player has changed
		//direction. We should flip the world if so.
		CheckAndFlip(inputValue);

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
