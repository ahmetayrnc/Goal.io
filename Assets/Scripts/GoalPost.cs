using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GoalPost : MonoBehaviour
{
	public Transform front;
	public Transform goalPost;
	public Material closedMaterial;
	private Renderer goalPostRenderer;

	void Start()
	{
		goalPostRenderer = goalPost.GetComponent<Renderer>();
	}
	
	public void CloseTheDoor()
	{
		front.DOScaleX(1.05f, 1f).SetEase(Ease.OutSine).OnComplete(() => { goalPostRenderer.material = closedMaterial; });
	}
}
