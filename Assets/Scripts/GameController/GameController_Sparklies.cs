using UnityEngine;
using System.Collections;

public partial class GameController : MonoBehaviour
{
	public void SpawnNewSparkly()
	{
		CreateSparkly(new Vector3(-15 + Random.value * 30, -15 + Random.value * 30, sparklyTemplate.transform.position.z));
	}
	
	public Sparkly CreateSparkly(Vector3 a_SpawnPos)
	{
		GameObject newSparklyGO = (GameObject)Instantiate(sparklyTemplate, a_SpawnPos, Quaternion.identity);
		Sparkly newSparkly = newSparklyGO.GetComponent<Sparkly>();
		sparklies.Add(newSparkly);
		return newSparkly;
	}
	
	void NomSparkly(Sparkly a_SparklyToNom)
	{
		sparklies.Remove(a_SparklyToNom);
	}
}
