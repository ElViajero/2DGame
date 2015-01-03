using UnityEngine;
using System.Collections;

public interface IAttackController{

	void Attack();
	void CancelAttack();
	void HandleDamage();

}
