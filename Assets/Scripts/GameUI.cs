using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameUI : MonoBehaviour
{
	public static GameUI Instance;
	
	public TextMeshProUGUI realPlayerGoals;
	public Player realPlayer;
	public TextMeshProUGUI countdown;
	public Image timerBar;

	public GameObject messages;
	public TextMeshProUGUI eliminationMessage;
	public TextMeshProUGUI speedUpMessage;
	public TextMeshProUGUI goalMessage;
	public TextMeshProUGUI goMessage;
	public GameObject tutorial;

	private GameManager gameManager;
	
	void Awake()
	{
		Instance = this;
	}
	
	void Start()
	{
		gameManager = GameManager.Instance;

		ShowTutorial();
		SetupMessages();
	}
	
	void Update()
	{
		timerBar.fillAmount = ( (gameManager.remainingPlayerCount - 2) * gameManager.roundDuration + gameManager.countdown) / gameManager.totalTime;
		
		realPlayerGoals.text = realPlayer.goals + "";
		countdown.text = string.Format("{0:D2}:{1:D2}", (int)(GameManager.Instance.countdown / 60),
			(int)(GameManager.Instance.countdown % 60));
	}

	void SetupMessages()
	{
		messages.SetActive(true);
		eliminationMessage.alpha = 0;
		speedUpMessage.alpha = 0;
		goalMessage.alpha = 0;
		goMessage.alpha = 0;
	}
	
	public void ShowEliminationMessage(string playerName)
	{
		MessageAnimation(eliminationMessage, playerName + " Eliminated", 1.5f);
	}

	public void ShowSpeedUpMessage()
	{
		//MessageAnimation(speedUpMessage, "Speed Up!");
	}

	public void ShowGoalMessage()
	{
		goalMessage.DOKill();
		MessageAnimation(goalMessage, "Goal!", 0.75f);
	}

	public void ShowGoMessage()
	{
		MessageAnimation(goMessage, "Go!", 0.25f);
	}
	
	public void ShowReadyMessage()
	{
		MessageAnimation(goMessage, "Ready!", 0.25f);
	}

	public void ShowTutorial()
	{
		tutorial.SetActive(true);
	}

	public void HideTutorial()
	{
		tutorial.SetActive(false);
	}

	void MessageAnimation(TextMeshProUGUI message, string messageText, float textDuration)
	{
		message.text = messageText;

		message.alpha = 0;
		message.transform.localScale = new Vector3(2,2,1);
		
		message.DOFade(1, 0.5f).SetEase(Ease.InQuart);
		message.transform.DOScaleX(1, 0.5f).SetEase(Ease.InQuart);
		message.transform.DOScaleY(1, 0.5f).SetEase(Ease.InQuart).OnComplete(() =>
		{
			message.DOFade(1, textDuration).OnComplete(() =>
			{
				message.DOFade(0, 0.5f).SetEase(Ease.OutSine);
			});
		});
	}
}
