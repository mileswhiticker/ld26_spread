using UnityEngine;
using System.Collections;

public partial class Mob : MonoBehaviour
{
	//todo: make this a static or global
	public float maxSpeed = 5;
	public Vector3 wanderTarget;
	float tLeftRechooseDest = 45;
	GameController gameController;
	
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
		if(sqrDist < 1)
		{
			wanderTarget = gameController.GetRandomMapPoint();
		}
		else if(this.rigidbody.velocity.sqrMagnitude < maxSpeed * maxSpeed)
		{
			Vector3 targetDir = wanderTarget - this.transform.position;
			this.rigidbody.AddForce(targetDir.normalized);
			//Debug.Log("rotating");
			
			//this.transform.rotation = newOrientation;
		}
		/*float vertAxis = Input.GetAxis ("Vertical");
		if(vertAxis != 0 && this.rigidbody.velocity.sqrMagnitude < maxSpeed * maxSpeed)
		{
			Vector3 newForce = this.transform.rotation * Vector3.down;
			this.rigidbody.AddForce(newForce);
		}
		
		//steering
		this.transform.Rotate(new Vector3(0,0, -3 * Input.GetAxis ("Horizontal")));*/
	}
}
