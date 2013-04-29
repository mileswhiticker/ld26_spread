using UnityEngine;
using System.Collections;

public partial class GameController : MonoBehaviour
{
	void GameOver(bool a_Won)
	{
		won = a_Won;
		curLevel = 0;
		player.playerControlled = false;
		ShowMenu();
		scoreMoveDirection = 1;
		
		Vector3 newScale = cancel.transform.localScale;
		newScale.y = 0;
		cancel.transform.localScale = newScale;
		
		string bgTexName = "red_half_trans";
		if(won)
		{
			bgTexName = "green_half_trans";
			audio.PlayOneShot(win);
		}
		else
		{
			audio.PlayOneShot(fail);
		}
		//teamTexture = (Texture2D)Resources.Load(teamTexName + Mathf.Ceil(Random.value * 3.0f));
		scoreBackground.renderer.material.mainTexture = (Texture2D)Resources.Load(bgTexName);
	}
}
