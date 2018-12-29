using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BotInformation : MonoBehaviour
{
	public static BotInformation Instance;
	public Transform ballParent;
	private Ball[] balls;

	private String[] names;
	private int nameIndex;

	void Awake()
	{
		Instance = this;
	}
	
	void Start()
	{
		balls = new Ball[ballParent.childCount];
		for (int i = 0; i < balls.Length; i++)
		{
			balls[i] = ballParent.GetChild(i).GetComponent<Ball>();
		}
		
		CreateNameArray();
		nameIndex = names.Length - 1;
	}

	public Transform GetRandomBallOutside()
	{
		if (balls == null)
		{
			return null;
		}
		
		int randomIndex = Random.Range(0, balls.Length);

		while(balls[randomIndex].inGoalPost || !balls[randomIndex].gameObject.activeSelf)
		{
			randomIndex = Random.Range(0, balls.Length);
		}

		return balls[randomIndex].transform;
	}
	
	public Transform GetClosestBallOutside(Vector3 position)
	{
		if (balls == null)
		{
			return null;
		}
		
		Ball closestBall = balls[0];
		Vector3 distanceVector = balls[0].transform.position - position;
		float distance = Vector3.SqrMagnitude(distanceVector);

		for (int i = 0; i < balls.Length; i++)
		{
			if (balls[i].inGoalPost || !balls[i].gameObject.activeSelf)
			{
				continue;
			}
			
			Vector3 tempDistanceVector = balls[i].transform.position - position;
			float tempDistance = Vector3.SqrMagnitude(tempDistanceVector);
			
			//Debug.Log("tempDistance, " + i + ": " + tempDistance);
			if (tempDistance < distance)
			{
				closestBall = balls[i];
			}
		}

		return closestBall.transform;
	}

	public String GetName()
	{
		if (nameIndex < 0)
		{
			return "null";
		}
		
		String botName = names[nameIndex];
		nameIndex--;
		return botName;
	}
	
	private void Shuffle<T> (IList<T> array)
	{
		int n = array.Count;
		var rng = new System.Random();
		while (n > 1) 
		{
			int k = rng.Next(n--);
			T temp = array[n];
			array[n] = array[k];
			array[k] = temp;
		}
	}

	private void CreateNameArray()
	{
		names = new string[]
		{
			"Lolumut","Izzy23","Chilly Pubble","CHEEKY MONKEY","VIKING","PITBULL","MERLIN","FLASH","anowhu","eshes","ceactave",
			"zofisenout","tetinsald","tenowiet","illai","lathe","orate","sansuttir","ratear","rirsulico","suesciess","daterarse",
			"zurithin","tousatic","h3ym4n","c00lbr0s","aqw6","omgtownismine","brad","samvan","someone","everyBodyInMyA33",
			"kisesed","Rosalee","Foster","Rosemarie","Rosie","Hayley","Smith","Kira","Ashley","Barrett","Brown","Rayne",
			"DiamandaJackson","Olympe12","Eglantine_poq","Shadow_rise","_chancellor","Sadie","Jennie","Mandi","Rosie",
			"Amy Potter","I’LL FIND YOU","COME AND FIND ME","EAT ME","DON’T EAT ME", "Snower", "Shodaw_Destroyer99", "CAN YOU EAT ME?",
			"HEY_MAN", "hi_there", "filozofHasan", "Sen Kimsin Ya!!", "EYYY!", "Binary_IH_IH_IH"
		};
		
		Shuffle(names);
	}
}
