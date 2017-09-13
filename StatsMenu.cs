using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class StatsMenu : MonoBehaviour {

	public Button mainMenu;
	public Text totalScore;
	public Text record;
	public Text totalMoves;
	public Text totalTime;

	public Transform spawnPoint;
	public GameObject prefab;

	public AudioSource backSFX;

	void Awake()
	{
		GameStatsController.data.Load();
	}

	void Start () 

	{
		if (GameStatsController.data.record == 0 && GameStatsController.data.totalNumberOfMoves == 0 && GameStatsController.data.record == 0 && (int)GameStatsController.data.totalTimePlayed == 0)
		{
			totalScore.text = "Total Score: " + GameStatsController.data.archivedTotalNumberOfObstacles;
			record.text = "Record: " + GameStatsController.data.archivedRecord;
			totalMoves.text = "Total Moves Made: " + GameStatsController.data.archivedTotalNumberOfMoves;
			totalTime.text = "Total Time Played: " + (int)GameStatsController.data.archivedTotalTimePlayed;
		}

		else
		{
			totalScore.text = "Total Score: " + GameStatsController.data.totalNumberOfObstacles;
			record.text = "Record: " + GameStatsController.data.record;
			totalMoves.text = "Total Moves Made: " + GameStatsController.data.totalNumberOfMoves;
			totalTime.text = "Total Time Played: " + (int)GameStatsController.data.totalTimePlayed;
		}

		GameObject obj = Instantiate (prefab, spawnPoint);
		obj.GetComponent<Renderer> ().material.color = GameStatsController.data.colors[GameStatsController.data.colorCounter.ToList().IndexOf(GameStatsController.data.colorCounter.Max())];

		mainMenu.GetComponent<Button> ().onClick.AddListener (GoToMainMenu);
	}


	void Update ()
	{
		if (Input.GetKey (KeyCode.Escape)) 
		{
			GoToMainMenu();
		}
	}


	void GoToMainMenu()
	{ 
		SceneManager.LoadScene ("MainMenu");
	}

}
