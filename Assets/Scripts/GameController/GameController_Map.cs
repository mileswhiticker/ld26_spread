using UnityEngine;
using System.Collections;

public partial class GameController : MonoBehaviour
{
	public Vector3 GetRandomMapPoint()
	{
		int ind = (int)Mathf.Floor(Random.value * 7.9999f);
		return wanderTargets[ind];
		
		//float val1 = 5 * Mathf.Floor(Random.value * 7);
		//float val1 = Random.value > 0.5f ? -15 : 15;
		//float val2 = 5 * Mathf.Floor(Random.value * 7);
		//float val2 = Random.value > 0.5f ? -15 : 15;
		
		//Debug.Log("New wander target: " + wanderTarget);
		
		//lock rotation for now
		/*Vector3 curDir = this.transform.rotation * Vector3.up;
		Vector3 targetDir = this.transform.position - wanderTarget;
		Quaternion newOrientation = this.transform.rotation;
		newOrientation.SetFromToRotation(curDir, targetDir);
		this.transform.rotation = newOrientation;*/
	}
}
