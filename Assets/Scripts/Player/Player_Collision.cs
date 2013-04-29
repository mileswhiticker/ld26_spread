using UnityEngine;
using System.Collections;

public partial class Player : MonoBehaviour
{
	void OnTriggerEnter(Collider collider)
	{
		//work out what we've hit
		GameObject gameObject = collider.gameObject;
		if(gameObject)
		{
			Sparkly sparkly = gameObject.GetComponent<Sparkly>();
			if(sparkly && tLeftCantPickup <= 0)
			{
				//YUM
				energy += 5;
				gameController.DestroyGameObject(gameObject);
				gameController.IncrementSparklyCounter();
			}
		}
	}
	
	void OnCollisionEnter(Collision collision)
	{
		//work out what we've hit
		GameObject gameObject = collision.gameObject;
		if(gameObject)
		{
			Mob mob = gameObject.GetComponent<Mob>();
			if(mob)
			{
				//check the level
				if(mob.mobLevel < playerLevel)
				{
					mob.DownGrade();
					gameController.audio.PlayOneShot(gameController.blip);
					if(mob.isMachine == isMachine)
					{
						gameController.IncrementFriendlyBump(mob.mobLevel);
					}
					else
					{
						gameController.IncrementEnemyBump(mob.mobLevel);
					}
					//tLeftCantPickup = 0.5f;
				}
				/*
				//friend or foe?
				if(mob.isMachine == isMachine)
				{
					//see if we can pick it up
					if(mob.mobLevel < playerLevel5)
					{
						//nom
						contents[mob.mobLevel] += 1;
						energy += 5 + mob.mobLevel * 5;
						
						gameController.DestroyGameObject(gameObject);
					}
					else
					{
						//just pass through it
					}
				}
				else
				{
					//check the level
					if(mob.mobLevel < playerLevel)
					{
						mob.DownGrade();
						tLeftCantPickup = 3.0f;
					}
				}
				*/
			}
		}
	}
}
