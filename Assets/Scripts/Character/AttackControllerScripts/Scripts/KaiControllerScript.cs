using UnityEngine;
using System.Collections;

public class KaiControllerScript : MonoBehaviour,IAttackController{


	float kiEnergy; // maximum energy of the attack. Used to control the size of Kai ball.
	CharacterArmMovementControllerScript characterArmMovementControllerInstance;
	CharacterMovementControllerScript characterMovementControllerInstance;
	GameObject character; // reference to character
	GameObject ki; // reference to kai ball.
	GameObject kiPoint; // reference to a fixed point at which the kai ball will be generated.

	bool isAttackCharging; // flag to indicate whether the kai ball is growing.
	Vector3 minimumKaiSize; // smalles possible Kai ball.
	Vector3 initialKaiPosition; // initial position when Kai ball is hidden.

	float scaleFactor; // How much we want the Kai ball's size to increase per update.
	float xTranslate; // translation coordinates (remember world flip).
	float yTranslate; 
	float kiPointTranslate; 

	bool isFired; // Tells us whether the ki blast has been fired.
		// If so, we shouldn't be able to control it anymore.s


	// Use this for initialization

	void Start () {
		character = GameObject.FindGameObjectWithTag("Character");
		characterArmMovementControllerInstance = 
			character.GetComponent<CharacterArmMovementControllerScript>();
		characterMovementControllerInstance =
			character.GetComponent<CharacterMovementControllerScript>();
		isFired=false;
		isAttackCharging=false;
		ki = GameObject.FindGameObjectWithTag("Ki");
		kiPoint = GameObject.FindGameObjectWithTag("KiPoint");
		kiEnergy=100; // This is a nice hit and trial value.
		minimumKaiSize = transform.localScale;
		initialKaiPosition = transform.position;
	}
	

	void FixedUpdate () {
	
		if(!isFired){

			if(!characterArmMovementControllerInstance.isReadyToFire){
				CancelAttack(); // if not ready to fire, the kai ball should not be visible.s
				return;
			}
					
			float inputValue = Input.GetAxis("Fire1"); //see if the arm is extended. 
			Debug.Log("isFired is false");
			if(inputValue>0){
				CalibrateParameters(); // Depending on whether the user is facing right/left.
				chargeAttack(); // charge increases the size of the Kai ball if fire key is held.
			}

			//finally check if the command to fire has been clicked.
			inputValue = Input.GetAxis("Fire3");
			if(inputValue>0){
				isAttackCharging=false;
				Attack();
				isFired=true;
			}
		}
		else{
			Debug.Log("isFired is true");
		}

	}


	private void chargeAttack(){
		gameObject.renderer.enabled=true;
		if(!isAttackCharging){ // first time the Kai ball is created.
			Vector3 kiPointPosition  = kiPoint.transform.position; 
			Debug.Log("PosKai : " + kiPointPosition);
			kiPointPosition.x+=kiPointTranslate;
			transform.position=kiPointPosition;
			isAttackCharging=true; // set flag to denote charging status ( increase in ball size).
		}

		if(kiEnergy>0){ // as the ball grows, energy is transferred into the ball.
			transform.localScale+=new Vector3(scaleFactor,scaleFactor,scaleFactor); // increase ball size by scalefactor.
			transform.position+=new Vector3(xTranslate,yTranslate,.01f); // correct the bigger ball's position.
			kiEnergy-=5; // decrement the energy remaining.
		}
	}




	private void CalibrateParameters(){ 
		scaleFactor = .02f; // These remain fixed regardless
		yTranslate = .01f; // of left/right facing.
		if(characterMovementControllerInstance.facingRight){
			xTranslate  = yTranslate; // x gets affected.
			kiPointTranslate = 0.5f;
		}
		else{
			xTranslate = -yTranslate; // absolute values are equal due to uniform scaling.
			kiPointTranslate = -0.5f;
		}
		xTranslate=0.0f;// after correcting the pivot on Kai.
		yTranslate=0.0f;// I don't think translation is needed.

	}



	#region IAttackController implementation
	public void Attack ()
	{
		isAttackCharging =false;
		rigidbody2D.velocity = new Vector2(1,0);
		//kai fired..
		isFired=true;

	}
	public void CancelAttack ()
	{
		gameObject.renderer.enabled=false;
		kiEnergy=100;
		transform.localScale = minimumKaiSize;
		transform.position = initialKaiPosition;
		isAttackCharging=false;
		//kiPoint.transform.position = initialKaiPointPosition;
	}
	public void HandleDamage ()
	{

	}
	#endregion


	//check for collision.
	public void OnCollisionEnter2D(Collision2D other){
	
		if(!other.gameObject.tag.Equals("Character")) // this is to ensure that the 
			Debug.Log("Colliding....");				 //collision is not with the player himself.



	}




}
