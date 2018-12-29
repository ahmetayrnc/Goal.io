using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour {

	public TMP_InputField nameInput;
	public TextMeshProUGUI best;
	
	[Space]
	public string MainSceneName = "Main";
	
	void Start()
	{
		nameInput.text = PlayerPrefs.GetString("playerName", "Player");

		if (!PlayerPrefs.HasKey("best"))
		{
			PlayerPrefs.SetInt("best", 0);
		}
		
		best.text = "Best " + PlayerPrefs.GetInt("best");
	}
	
	public void StartGame()
	{
		SceneManager.LoadScene(MainSceneName);
	}
	
	public void SetName( string _playername )
	{
		PlayerPrefs.SetString("playerName", _playername);
		PlayerPrefs.Save();
	}
}
