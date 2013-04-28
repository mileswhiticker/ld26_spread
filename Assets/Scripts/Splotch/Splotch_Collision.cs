using UnityEngine;
using System.Collections;

public partial class Splotch : MonoBehaviour
{
	void OnTriggerEnter(Collider collider)
	{
		//work out what we've hit
		GameObject gameObject = collider.gameObject;
		if(gameObject && !ignoreMe)
		{
			Splotch splotch = gameObject.GetComponent<Splotch>();
			if(splotch && !splotch.ignoreMe)
			{
				//friend or foe?
				if(splotch.isMachine == isMachine)
				{
					overlappingSplotchGameObjects.Add(gameObject);
					if(splotchLevel + splotch.splotchLevel < 50 && tLeftUntilMerge <= 0 && splotch.tLeftUntilMerge <= 0)
					{
						//assimilate them into our mass
						int levelsGained = splotch.splotchLevel;
						splotch.AddLevels(-levelsGained);
						AddLevels(levelsGained);
						tLeftUntilMerge = 10.0f;
						gameController.DestroyGameObject(gameObject);
						//overlappingSplotchGameObjects.Remove(gameObject);
					}
					else
					{
						//overlappingSplotchGameObjects.Add(gameObject);
					}
				}
				else
				{
					//fight!
					int enemyLevels = splotch.splotchLevel;
					int myLevels = splotchLevel;
					if(enemyLevels > 0)
						AddLevels(-enemyLevels);
					
					if(myLevels > 0)
						splotch.AddLevels(-myLevels);
				}
			}
		}
	}
	
	void OnTriggerStay(Collider collider)
	{
		//drift away from them
		Vector3 newForce = (this.transform.position - collider.gameObject.transform.position).normalized * 5;
		this.rigidbody.AddForce(newForce);
		//overlappingSplotchGameObjects.Remove(collider.gameObject);
	}
}
