using UnityEngine;
using System.Collections;

public partial class Mob : MonoBehaviour
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
				gameController.DestroyGameObject(gameObject);
				Vector3 spawnPos = this.transform.position;
				spawnPos.z = 0;
				gameController.CreateSplotch(spawnPos, mobLevel, isMachine);
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
				//friend or foe?
				if(mob.isMachine != isMachine && destroyingEnemies)
				{
					//me smash
					Destroy(mob.gameObject);
				}
			}
		}
	}
}
