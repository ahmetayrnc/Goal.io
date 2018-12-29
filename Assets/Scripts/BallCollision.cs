using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
	private Rigidbody rb;
	
	private Ball ball;
	public float forcePower;
	public float upForce;
	
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		ball = GetComponent<Ball>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.CompareTag("GoalPost"))
		{
			ball.inGoalPost = true;
			ball.RespawnBall(3f);
			ball.gameObject.layer = LayerMask.NameToLayer("Default");

			if (ball.GetLastHitPlayer() != null)
			{
				ball.GetLastHitPlayer().IncreaseGoals();
			}
		}
	}

	void OnCollisionEnter(Collision other)
	{
		if (ball.IsInGoalPost())
		{
			return;
		}

		if (other.transform.CompareTag("Outside"))
		{
			ball.RespawnBall(3f);
			return;
		}
		
		if (other.transform.CompareTag("Player"))
		{
			ball.setLastHitPlayer(other.gameObject.GetComponent<Player>());
			Shoot(other);
		}		
	}

	void OnCollisionStay(Collision other)
	{
		if (ball.IsInGoalPost())
		{
			return;
		}
		
		if (other.transform.CompareTag("Player"))
		{
			Shoot(other);
		}	
	}

	void Shoot(Collision other)
	{
		ContactPoint contact = other.contacts[0];
		Vector3 force = (transform.position - contact.point).normalized;
		force.y = upForce;
		rb.AddForce(force * forcePower * 0.2f, ForceMode.Impulse);
	}
}
