using UnityEngine;
using System.Collections;

public partial class Player : MonoBehaviour
{
	void Movement()
	{
		//forward movement
		float vertAxis = Input.GetAxis ("Vertical");
		if(vertAxis != 0 && Mathf.Abs(this.rigidbody.velocity.sqrMagnitude) < maxSpeed * maxSpeed)
		{
			Vector3 newForce = this.transform.rotation * Vector3.down * vertAxis * playerLevel;
			this.rigidbody.AddForce(newForce);
		}
		
		//steering
		this.transform.Rotate(new Vector3(0,0, -3 * Input.GetAxis ("Horizontal")));
	}
}
