using UnityEngine;
using System.Collections;

public class DamageControllerScript : MonoBehaviour,IDamageable {


	// damage begins with a collision.
	public void OnCollisionEnter2D(Collision2D other){

		GameObject collisionGameObject = other.gameObject; // get the gameObject that collided.
		if(collisionGameObject.tag.Equals("Attack")){
			Debug.Log("Attacked !");
		}
	}


	#region IDamageable implementation

	public void TakeDamage ()
	{
		throw new System.NotImplementedException ();
	}

	public void Destroy ()
	{
		throw new System.NotImplementedException ();
	}

	#endregion



}
