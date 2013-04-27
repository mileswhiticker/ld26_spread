using UnityEngine;
using System.Collections;

public partial class GameController : MonoBehaviour
{
	public void SpawnNewSparkly()
	{
		Vector3 spawnPos = new Vector3(-15 + Random.value * 30, -15 + Random.value * 30, sparklyTemplate.transform.position.z);
		GameObject newSparklyGO = (GameObject)Instantiate(sparklyTemplate, spawnPos, Quaternion.identity);
		Sparkly newSparkly = newSparklyGO.GetComponent<Sparkly>();
		sparklies.Add(newSparkly);
	}
	
	void NomSparkly(Sparkly a_SparklyToNom)
	{
		sparklies.Remove(a_SparklyToNom);
	}
}
