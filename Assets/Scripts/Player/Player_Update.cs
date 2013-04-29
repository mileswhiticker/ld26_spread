using UnityEngine;
using System.Collections;

public partial class Player : MonoBehaviour
{
	float tLeftAttracting = 0;
	float energyPerAttract = 1;
	void FixedUpdate()
	{
		Movement();
	}
	
	void Update()
	{
		
		if(tLeftCantPickup > 0)
		{
			tLeftCantPickup -= Time.deltaTime;
		}
		
		if(tLeftToggleDrops > 0)
		{
			tLeftToggleDrops -= Time.deltaTime;
		}
		
		if(playerControlled)
		{
			if(gameController && Input.GetButton("Jump"))
			{
				if(tLeftAttracting <= 0)
				{
					if(energy > energyPerAttract)
					{
						energy -= energyPerAttract;
						gameController.PlayerAttract();
						tLeftAttracting = 5.0f;
					}
				}
				else
				{
					tLeftAttracting -= Time.deltaTime;
				}
			}
			
			//
			RegularEnergyRelease(Time.deltaTime);
		}
	}
}
