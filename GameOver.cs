using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	public Button newGame;
	public Button mainMenu;
	public Text score;
	public Text record;

	public AudioSource replaySFX;
	public AudioSource mainMenuSFX;


	void Awake() 
	{
		newGame.GetComponent<Button> ().onClick.AddListener (StartNewGame);
		mainMenu.GetComponent<Button> ().onClick.AddListener (GoToMainMenu);

	}


	void Start () 
	{
		AdController.ad.RequestBannerAd(0);
		GameStatsController.data.Load ();
		if (GameStatsController.data.numberOfObstacles > GameStatsController.data.record) {
			Debug.Log (GameStatsController.data.record);
			Debug.Log ("New Record!");
		}
		score.text = "Score: " + GameStatsController.data.numberOfObstacles;
		record.text = "Record: " + GameStatsController.data.record;
	}


	void Update ()
	{
		if (Input.GetKey (KeyCode.Escape)) 
		{
			GoToMainMenu();
		}
	}
		

	void StartNewGame()
	{
		GameStatsController.data.startTime = Time.time;
		SceneManager.LoadScene ("Gameplay");
	}


	void GoToMainMenu() {
		SceneManager.LoadScene ("MainMenu");
	}


	void OnDestroy() {
		AdController.ad.HideBannerAd();
	}
}
