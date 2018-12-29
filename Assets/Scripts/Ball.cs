using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
	private Player lastHitPlayer;
	private BallSpawner ballSpawner;
	public bool inGoalPost;
	public bool isSpawning;

	private TrailRenderer trailRenderer;
	
	void Start()
	{
		inGoalPost = false;
		lastHitPlayer = null;
		ballSpawner = BallSpawner.Instance;
		trailRenderer = GetComponent<TrailRenderer>();
	}
	
	void FixedUpdate() 
	{
		GetComponent<Rigidbody>().AddForce(Physics.gravity * 2f, ForceMode.Acceleration);
	}


	public Player GetLastHitPlayer()
	{
		return lastHitPlayer;
	}

	public void setLastHitPlayer(Player player)
	{
		lastHitPlayer = player;
	}

	public void ResetBall()
	{
		trailRenderer.Clear();
		lastHitPlayer = null;
		inGoalPost = false;
		isSpawning = false;
		gameObject.layer = LayerMask.NameToLayer("Ball");
	}

	public void RespawnBall(float delay)
	{
		ballSpawner.RespawnBall(transform, delay);
	}

	public bool IsInGoalPost()
	{
		return inGoalPost;
	}

	public bool IsSpawning()
	{
		return isSpawning;
	}
	
}
