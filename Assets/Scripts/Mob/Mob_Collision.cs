using UnityEngine;
using System.Collections;

public partial class Mob : MonoBehaviour
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
				if(mob.isMachine != isMachine && destroyingEnemies)
				{
					//me smash
					Destroy(mob.gameObject);
				}
			}
			else
			{
				Sparkly sparkly = gameObject.GetComponent<Sparkly>();
				if(sparkly)
				{
					//YUM
					gameController.DestroyGameObject(gameObject);
				}
			}
		}
	}
}
