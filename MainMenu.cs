using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;

public class MainMenu : MonoBehaviour {

	public Button newGame;
	public Button stats;
	public Button sound;
	public Sprite audioOnSprite;
	public Sprite audioOffSprite;
	public AudioSource playSFX;
	public AudioSource statsSFX;
	public AudioSource infoSFX;

	private bool startButtonPressed;
	private bool statsButtonPressed;


	void Start () 
	{
		newGame.GetComponent<Button> ().onClick.AddListener (StartNewGame);
		stats.GetComponent<Button> ().onClick.AddListener (GoToStatsMenu);
		sound.GetComponent<Button> ().onClick.AddListener (ToggleAudio);
		startButtonPressed = false;
	}


	void Update()
	{
		if (startButtonPressed && !playSFX.isPlaying)
		{
			startButtonPressed = false;
			SceneManager.LoadScene("gameplay");
		}

		if (statsButtonPressed && !statsSFX.isPlaying)
		{
			statsButtonPressed = false;
			SceneManager.LoadScene ("StatsMenu");
		}

	}


	void StartNewGame()
	{
		if (!startButtonPressed)
		{
			GameStatsController.data.startTime = Time.time;

			playSFX.Play();
			startButtonPressed = true;
		}
	}


	void GoToStatsMenu() {

		if (!statsButtonPressed)
		{
			statsSFX.Play();
			statsButtonPressed = true;
		}
	}


	void ToggleAudio () 
	{
		if (sound.image.sprite == audioOnSprite) {
			sound.image.sprite = audioOffSprite;
		}
		else {
			sound.image.sprite = audioOnSprite;
		}
	}

}
