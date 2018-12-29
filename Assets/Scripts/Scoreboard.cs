using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
	public static Scoreboard Instance;
	
	public Transform scoreboardPlayerItemParent;
	
	private Player[] players;
	private ScoreboardPlayerItem[] items;
	
	public ScoreboardPlayerItem eliminationItem;
	
	public float refreshDelay;
	private float refreshCountdown;

	void Awake()
	{
		Instance = this;
	}
	
	void Start()
	{
		StartCoroutine(LateStart());
	}

	void Update()
	{
		refreshCountdown += Time.deltaTime;
		if (refreshCountdown >= refreshDelay)
		{
			refreshCountdown = 0;
			SortPlayers();
			RefreshScoreboard();
		}
	}
	
	IEnumerator LateStart()
	{
		yield return new WaitForSeconds(0.01f);
		
		SetupArrays();
		RefreshScoreboard();
	}

	void SetupArrays()
	{
		items = new ScoreboardPlayerItem[scoreboardPlayerItemParent.childCount];
		for (int i = 0; i < items.Length; i++)
		{
			items[i] = scoreboardPlayerItemParent.GetChild(i).GetComponent<ScoreboardPlayerItem>();
		}
		
		players = GameManager.Instance.GetPlayerArray();
	}
	
	void RefreshScoreboard()
	{
		Player[] temp = getTop3Players();
		
		for (int i = 0; i < temp.Length; i++)
		{
			items[i].SetPlayer(temp[i]);
		}

		Player tempElimination = getEliminationPlayer();
		eliminationItem.SetPlayer(tempElimination);
	}

	void SortPlayers()
	{
		Player temp;
		
		for (int i = 0; i < players.Length; i++)
		{
			for (int j = 0; j < players.Length - 1; j++)
			{
				if (players[j].goals < players[j + 1].goals)
				{
					temp = players[j + 1];
					players[j + 1] = players[j];
					players[j] = temp;
				}
			}
		}
	}
	
	// returns 3 players with the most goal in descending order
	Player[] getTop3Players()
	{
		Player[] result = new Player[3];

		for (int i = 0; i < result.Length; i++)
		{
			if (!players[i].eliminated)
			{
				result[i] = players[i];
			}
		}

		return result;
	}
	
	
	// returns the player with the lowest goal count
	public Player getEliminationPlayer()
	{
		Player temp = null;
		
		for (int i = players.Length - 1; i > 0; i--)
		{
			if (!players[i].eliminated)
			{
				temp = players[i];
				break;
			}
		}

		return temp;
	}
}
