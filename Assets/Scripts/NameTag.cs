using TMPro;
using UnityEngine;

public class NameTag : MonoBehaviour
{
	public Player player;
	public TextMeshProUGUI playerName;
	public GameObject eliminationSkull;
	
	public float refreshDelay;
	private float refreshCountdown;
	
	void Update()
	{
		if (player == null)
		{
			return;
		}
		
		if (player.eliminated)
		{
			gameObject.SetActive(false);
		}
		
		transform.position = player.transform.position;
		playerName.text = player.playerName;
		
		refreshCountdown += Time.deltaTime;
		if (refreshCountdown >= refreshDelay)
		{
			if (Scoreboard.Instance.getEliminationPlayer() == player)
			{
				ShowEliminationSkull();
			}
			else
			{
				HideEliminationSkull();
			}
		}
	}

	public void SetPlayer(Player _player)
	{
		player = _player;
		playerName.text = player.playerName;
	}

	public void ShowEliminationSkull()
	{
		eliminationSkull.SetActive(true);
	}

	public void HideEliminationSkull()
	{
		eliminationSkull.SetActive(false);
	}
}
