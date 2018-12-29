using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScoreboard : MonoBehaviour {

	public Transform scoreboardPlayerItemParent;
	
	private Player[] players;
	private GameOverScoreboardItem[] items;
	
	void SetupArrays()
	{
		items = new GameOverScoreboardItem[scoreboardPlayerItemParent.childCount];
		for (int i = 0; i < items.Length; i++)
		{
			items[i] = scoreboardPlayerItemParent.GetChild(i).GetComponent<GameOverScoreboardItem>();
		}
		
		players = GameManager.Instance.GetPlayerArray();
	}
	
	void SortPlayers()
	{	
		for (int i = 0; i < players.Length; i++)
		{
			for (int j = 0; j < players.Length - 1; j++)
			{
				if (players[j].goals < players[j + 1].goals)
				{
					var temp = players[j + 1];
					players[j + 1] = players[j];
					players[j] = temp;
				}
			}
		}
	}
	
	void RefreshScoreboard()
	{
		for (int i = 0; i < items.Length; i++)
		{
			var tempItem = items[i];
			var tempPlayer = players[i];
			tempItem.SetPlayer(tempPlayer);
			items[i].SetPlayer(players[i]);
		}
	}

	public void ShowScoreboard()
	{
		SetupArrays();
		SortPlayers();
		RefreshScoreboard();
	}
}
