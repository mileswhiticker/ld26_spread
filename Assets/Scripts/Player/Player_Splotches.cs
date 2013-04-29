using UnityEngine;
using System.Collections;

public partial class Player : MonoBehaviour
{
	public float tLeftReleaseSplotch = 0.5f;
	public float dropIntervals = 0.5f;
	public float energyPerSplotch = 0.3f;
	public bool releasingSplotches = false;
	
	void RegularEnergyRelease(float a_DeltaT)
	{
		//just release it if we have the energy, don't worry about holding down the key
		if(energy > energyPerSplotch)
		{
			tLeftReleaseSplotch -= a_DeltaT;
			if(tLeftReleaseSplotch < 0)	
			{
				//tell the GC to drop a splotch
				Vector3 spawnPos = this.transform.position;
				spawnPos.z = 0;
				gameController.CreateSplotch(spawnPos, playerLevel, isMachine);
				
				//play sound
				gameController.audio.PlayOneShot(gameController.dropSplotch);
				
				//bookkeeping
				energy -= energyPerSplotch;
				tLeftReleaseSplotch = dropIntervals;
			}
			
			/*if(tLeftToggleDrops <= 0 && Input.GetButton("Sprint"))
			{
				releasingSplotches = !releasingSplotches;
				tLeftToggleDrops = 0.5f;
			}*/
			if(releasingSplotches)
			{
				//
			}
			if(energy < energyPerSplotch)
			{
				releasingSplotches = false;
			}
		}
	}
}
