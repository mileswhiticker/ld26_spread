using UnityEngine;
using System.Collections;

public partial class Player : MonoBehaviour
{
	public float energy = 10;
	public float tLeftReleaseSplotch = 0.5f;
	public float dropIntervals = 0.3f;
	public float energyPerSplotch = 0.3f;
	
	void RegularEnergyRelease(float a_DeltaT)
	{
		//if we're holding down shift and going over a certain speed, we can drop splotches
		if(energy > energyPerSplotch && Input.GetButton("Sprint") && this.gameObject.rigidbody.velocity.sqrMagnitude > 2)
		{
			tLeftReleaseSplotch -= a_DeltaT;
		
			if(tLeftReleaseSplotch < 0)
			{
				//tell the GC to drop a splotch
				Vector3 spawnPos = this.transform.position;
				spawnPos.z = 0;
				gameController.CreateSplotch(spawnPos);
				
				//bookkeeping
				energy -= energyPerSplotch;
				tLeftReleaseSplotch = dropIntervals;
			}
		}
	}
}
