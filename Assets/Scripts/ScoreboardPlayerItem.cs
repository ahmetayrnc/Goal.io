using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreboardPlayerItem : MonoBehaviour
{
	public TextMeshProUGUI playerName;
	public TextMeshProUGUI goals;
	private Player player;

	void Update()
	{
		if (player == null)
		{
			playerName.text = "";
			goals.text = "";
			return;
		}
		
		playerName.text = player.playerName + "";
		goals.text = player.goals + "";
	}

	public void SetPlayer(Player _player)
	{
		player = _player;
	}
}
