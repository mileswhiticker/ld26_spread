using UnityEngine;
using System.Collections;

public partial class Player : MonoBehaviour
{
	void OnCollisionEnter(Collision collision)
	{
		//work out what we've hit
		GameObject gameObject = collision.gameObject;
		if(gameObject)
		{
			Mob mob = gameObject.GetComponent<Mob>();
			if(mob)
			{
				//friend or foe?
				if(mob.isMachine == isMachine)
				{
					//see if we can pick it up
					if(mob.mobLevel < playerLevel && mob.mobLevel < 5)
					{
						//nom
						contents[mob.mobLevel] += 1;
						energy += 5 + mob.mobLevel * 2;
						
						gameController.DestroyGameObject(gameObject);
					}
					else
					{
						//just pass through it
					}
				}
			}
			else
			{
				Sparkly sparkly = gameObject.GetComponent<Sparkly>();
				if(sparkly)
				{
					//YUM
					energy += 10;
					gameController.DestroyGameObject(gameObject);
				}
			}
		}
	}
}
