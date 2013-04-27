using UnityEngine;
using System.Collections;

public class Camera_Tracking : MonoBehaviour
{
	public GameObject trackedObject;
	public Camera trackingCamera;
	public float minDist = 0;
	
	public float maxFlySpeed = 15;
	public float tLeftFrozen = 0;
	
	void Start()
	{
		trackedObject = GameObject.FindGameObjectWithTag("Player");
		trackingCamera = this.GetComponent<Camera>();
	}
	
	void Update ()
	{
		if(trackingCamera && trackedObject)
		{
			if(tLeftFrozen < 0)
			{
				tLeftFrozen -= Time.deltaTime;
			}
			else
			{
				Vector3 diff = trackedObject.transform.position - trackingCamera.transform.position;
				diff.z = 0;
				float sqrDist = diff.sqrMagnitude;
				if(sqrDist > minDist * minDist)
				{
					//Debug.Log("cam move");
					float sqrMoveDist = sqrDist - minDist * minDist;
					sqrMoveDist = sqrMoveDist > maxFlySpeed ? maxFlySpeed : sqrMoveDist;
					diff.Normalize();
					
					diff *= sqrMoveDist * Time.deltaTime;
					
					trackingCamera.transform.position += diff;
				}
			}
		}
	}
}
