using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
	private MovementMotor motor;
	public bool isRealPlayer;
	public string playerName;
	public int speedIncreaseEvent;
	
	[Space]
	public int goals;
	public int speedIncreaseGoals;
	public bool eliminated;

	[Space] 
	public ParticleSystem levelUpEffect;
	
	void Start()
	{
		StartCoroutine(LateStart());
		motor = GetComponent<MovementMotor>();
		speedIncreaseEvent = GameManager.Instance.speedIncreaseEvent;
	}
	
	IEnumerator LateStart()
	{
		yield return new WaitForSeconds(0.01f);
		
		if (isRealPlayer)
		{
			playerName = PlayerPrefs.GetString("playerName");
		}
		else
		{
			playerName = BotInformation.Instance.GetName();
		}
		
		goals = 0;
	}

	public void IncreaseGoals()
	{
		if (isRealPlayer)
		{
			GameUI.Instance.ShowGoalMessage();
		}
		
		goals++;

		speedIncreaseGoals++;
		
		if (speedIncreaseGoals == speedIncreaseEvent)
		{
			if (isRealPlayer)
			{
				GameUI.Instance.ShowSpeedUpMessage();
			}
			
			motor.IncreaseSpeed();
			levelUpEffect.Play();
			speedIncreaseGoals = 0;
		}
	}

	public void DisablePlayer()
	{
		if (isRealPlayer)
		{
			GameManager.Instance.EndGame(false);
		}
		else
		{
			GameUI.Instance.ShowEliminationMessage(playerName);
			
			gameObject.SetActive(false);
		}
		eliminated = true;
	}
}
