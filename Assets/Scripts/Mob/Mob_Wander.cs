using UnityEngine;
using System.Collections;

public partial class Mob : MonoBehaviour
{
	//todo: make this a static or global
	public float maxSpeed = 4;
	public Vector3 wanderTarget;
	float tLeftRechooseDest = 45;
	void MoveToWanderTarget()
	{
		//failsafe, in case any get bugged
		tLeftRechooseDest -= Time.deltaTime;
		if(tLeftRechooseDest <= 0)
		{
			tLeftRechooseDest = 45;
			wanderTarget = gameController.GetRandomMapPoint();
		}
		
		//forward movement
		float sqrDist = (wanderTarget - this.transform.position).sqrMagnitude;
		Vector3 targetDir = (wanderTarget - this.transform.position).normalized;
		if(sqrDist < 1)
		{
			wanderTarget = gameController.GetRandomMapPoint();
		}
		else if(this.rigidbody.velocity.sqrMagnitude < maxSpeed * maxSpeed)
		{
			//this.rigidbody.AddForce(this.transform.rotation * Vector3.up * Time.deltaTime * 50);
			this.rigidbody.AddForce(targetDir * Time.deltaTime * 50);
		}
		
		if(this.rigidbody.velocity.sqrMagnitude > 0)
			this.transform.rotation = Quaternion.LookRotation(targetDir) * Quaternion.LookRotation(Vector3.down);
	}
}
