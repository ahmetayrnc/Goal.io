using System.Collections.Generic;
using UnityEngine;

public class GoalPostController : MonoBehaviour
{
	public static GoalPostController Instance;
	
	public GoalPost[] goalPosts;
	public Transform goalPostParent;

	private int closedGoalPostIndex;

	void Awake()
	{
		Instance = this;
	}
	
	void Start()
	{
		closedGoalPostIndex = goalPostParent.childCount - 1;
		goalPosts = new GoalPost[goalPostParent.childCount];
		for (int i  = 0; i < goalPosts.Length; i++)
		{
			goalPosts[i] = goalPostParent.GetChild(i).GetComponent<GoalPost>();
		}
		
		Shuffle(goalPosts);
	}
	
	public void DisableGoalPost()
	{
		if (closedGoalPostIndex < 0)
		{
			return;
		}
		
		goalPosts[closedGoalPostIndex].CloseTheDoor();
		closedGoalPostIndex--;
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
}
