using UnityEngine;
using System.Collections;

public partial class GameController : MonoBehaviour
{
	public void CreateSplotch(Vector3 a_SpawnPos, bool a_IsMachine = true)
	{
		GameObject newSplotchGO = (GameObject)Instantiate(splotchTemplate, a_SpawnPos, this.transform.rotation);
		Splotch newSplotch = newSplotchGO.GetComponent<Splotch>();
		newSplotch.Init(a_IsMachine);
		if(player.isMachine == a_IsMachine)
		{
			friendlySplotches.Add(newSplotch);
		}
		else
		{
			enemySplotches.Add(newSplotch);
		}
		updateWinPercentNext = true;
	}
}
