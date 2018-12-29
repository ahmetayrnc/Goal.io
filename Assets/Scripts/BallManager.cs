using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
	public static BallManager Instance;
	
	public Transform ballParent;
	
	private Ball[] balls;

	public int activeBallAmount;
	public int targetBallAmount;

	void Awake()
	{
		Instance = this;
	}
	
	// Use this for initialization
	void Start () 
	{
		SetupBallArray();
		activeBallAmount = balls.Length;
		targetBallAmount = balls.Length;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SetupBallArray()
	{
		balls = new Ball[ballParent.childCount];
		for (int i = 0; i < balls.Length; i++)
		{
			balls[i] = ballParent.GetChild(i).GetComponent<Ball>();
		}
	}

	public void RemoveBall(Transform _ball)
	{
		for (int i = 0; i < balls.Length; i++)
		{
			if (balls[i] == null)
			{
				continue;
			}
			
			if (balls[i].transform == _ball)
			{
				balls[i].gameObject.SetActive(false);
				balls[i] = null;
				activeBallAmount--;
				break;
			}
		}
	}

	public bool HasTooMuchBalls()
	{
		return activeBallAmount > targetBallAmount;
	}
}
