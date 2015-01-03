using UnityEngine;
using System.Collections;

public class CameraMovementControllerScript : MonoBehaviour {

	GameObject characterInstance;

	// Use this for initialization
	void Start () {
		characterInstance  = GameObject.FindGameObjectWithTag("Character");
	}
	
	// Update is called once per frame
	void Update () {
	
		Vector3 characterPosition = characterInstance.transform.localPosition;
		Vector3 currentcameraPosition = gameObject.transform.position;



		currentcameraPosition.x=characterInstance.transform.localPosition.x;
		gameObject.transform.position= currentcameraPosition ;


	}
}
