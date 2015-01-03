using UnityEngine;
using System.Collections.Generic;
using System;


/// <summary>
/// This class controls the animations
/// that are played for the character
/// by setting parameters on the animation 
/// controller instance.
/// </summary>
public class CharacterAnimationController : MonoBehaviour {
	
	Animator animatorInstance; // a reference to the animator
	private static IList<IAnimationController> animationControllerList;

	
	public static void RegisterAnimationController(IAnimationController controller){
		if(animationControllerList==null)
			animationControllerList=new List<IAnimationController>();
		animationControllerList.Add(controller);
	}





	public void FixedUpdate()
	{
		// call the handle animation
		// method on each AnimationController that has registered.
		foreach (IAnimationController controller in animationControllerList ){
			controller.HandleAnimation();
		}
	}

}
