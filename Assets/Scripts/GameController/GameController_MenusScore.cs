using UnityEngine;
using System.Collections;

public partial class GameController : MonoBehaviour
{
	void HandleMenus(float a_DeltaT)
	{
		//start button
		Vector3 screenPos = Camera.main.WorldToScreenPoint(this.teamMarker.transform.position);
		if(GUI.Button(new Rect(
			screenPos.x - 125,
			screenPos.y - 125,
			250,
			250),
			"",
			GUIStyle.none
			))
		{
			BeginLevel(1);
		}
		
		//tree team select
		screenPos = Camera.main.WorldToScreenPoint(tree.transform.position);
		if(GUI.Button(new Rect(
			screenPos.x - 25,
			screenPos.y - 25,
			50,
			50),
			"",
			GUIStyle.none
			//tree.renderer.material.mainTexture
			))
		{
			tree.renderer.material.SetColor("_Color",Color.green);
			machine.renderer.material.SetColor("_Color",Color.red);
			teamMarker.renderer.material.mainTexture = (Texture2D)Resources.Load("grass");
			teamMarker.renderer.material.mainTexture.filterMode = FilterMode.Point;
			playerIsMachineNextGame = false;
			//Debug.Log("tree clicked");
		}
		
		//machine team select
		screenPos = Camera.main.WorldToScreenPoint(machine.transform.position);
		if(GUI.Button(new Rect(
			screenPos.x - 25,
			screenPos.y - 25,
			50,
			50),
			"",
			GUIStyle.none
			//machine.renderer.material.mainTexture
			))
		{
			tree.renderer.material.SetColor("_Color",Color.red);
			machine.renderer.material.SetColor("_Color",Color.green);
			teamMarker.renderer.material.mainTexture = (Texture2D)Resources.Load("oil");
			teamMarker.renderer.material.mainTexture.filterMode = FilterMode.Point;
			playerIsMachineNextGame = true;
			//Debug.Log("machine clicked");
		}
		
		//quit (door)
		screenPos = Camera.main.WorldToScreenPoint(door.transform.position);
		if(GUI.Button(new Rect(
			screenPos.x - 25,
			Screen.height - screenPos.y - 25,
			50,
			50),
			"",
			GUIStyle.none
			))
		{
			Application.Quit();
		}
		
		//unpause (cancel)
		screenPos = Camera.main.WorldToScreenPoint(cancel.transform.position);
		if(GUI.Button(new Rect(
			screenPos.x - 25,
			Screen.height - screenPos.y - 25,
			50,
			50),
			"",
			GUIStyle.none
			))
		{
			if(Time.timeScale == 0)
			{
				Time.timeScale = 1;
				handleMenu = false;
				ShowGui();
				
				//reset the tree/machine colours
				tree.renderer.material.SetColor("_Color",Color.white);
				machine.renderer.material.SetColor("_Color",Color.white);
			}
		}
		
		//teamMarker.renderer.material.mainTexture = (Texture2D)Resources.Load(teamTexName + Mathf.Ceil(Random.value * 3.0f));
		//teamMarker.renderer.material.mainTexture.filterMode = FilterMode.Point;
		
		//rotate the team marker, todo: replace this with a button
		teamMarker.transform.Rotate(new Vector3(0, 0, 2 * a_DeltaT));
		
		//interactive menu: move forward and list lazily sidewards
		if(!player.playerControlled)
		{
			/*if(player.rigidbody.velocity.sqrMagnitude < 4)
			{
				//move to center of screen
				player.rigidbody.AddForce(-player.transform.position.normalized);
			}*/
			Vector3 newForce = player.transform.rotation * Vector3.down;
			player.rigidbody.AddForce(newForce * 10 * a_DeltaT);
			//
			player.transform.Rotate(new Vector3(0,0, -10) * a_DeltaT);
		}
	}
	
	void MoveScoreBackground(float a_Progress)
	{
		//Debug.Log("showing score");
		//GUI.Box(new Rect(20, -Screen.height + (a_Progress * (10 + Screen.height)), Screen.width - 40, Screen.height - 40), won ? "YOU WON" : "YOU LOST");
	}
}
