using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
	public GameOverScoreboard scoreboard;
	
	public TextMeshProUGUI gameOverText;
	
	[Space]
	public string mainMenuSceneName = "MainMenu";

	void Start()
	{
	}
	
	public void MainMenu()
	{
		SceneManager.LoadScene(mainMenuSceneName);
	}

	public void SetGameOverText(string gameOverString)
	{
		gameOverText.text = gameOverString;
	}

	public void ShowScoreboard(bool realPlayerWon)
	{
		if (realPlayerWon)
		{
			scoreboard.gameObject.SetActive(true);
			scoreboard.ShowScoreboard();
		}
		else
		{
			scoreboard.gameObject.SetActive(false);
		}
	}
}
