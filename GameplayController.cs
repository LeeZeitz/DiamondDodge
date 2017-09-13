using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;
using GoogleMobileAds.Api;


public class GameplayController : MonoBehaviour {

	public Button pauseButton;
	public Button resumeButton;
	public Button quitButton;
	public Button soundButton;

	public Sprite pauseSprite;
	public Sprite playSprite;
	public Sprite soundOnSprite;
	public Sprite soundOffSprite;

	public SpriteRenderer count1;
	public SpriteRenderer count2;
	public SpriteRenderer count3;

	public GameObject gameplayCanvas;
	public GameObject pauseCanvas;
	public GameObject player;

	public AudioSource resumeSFX;
	public AudioSource quitSFX;

	private bool show1;
	private bool show2;
	private bool show3;

	private float showTimer;


	void Awake()
	{
		show1 = false;
		show2 = false;
		show3 = false;
		pauseCanvas.SetActive(false);
		gameplayCanvas.SetActive(true);
	}


	void Start () 
	{
		pauseButton.GetComponent<Button> ().onClick.AddListener (TogglePause);
		soundButton.GetComponent<Button> ().onClick.AddListener (ToggleAudio);
		resumeButton.GetComponent<Button> ().onClick.AddListener (TogglePause);
		quitButton.GetComponent<Button> ().onClick.AddListener (GoToMainMenu);
		player.GetComponent<PlayerController>().enabled = true;

		showTimer = 0;
		Time.timeScale = 1;
	}


	void Update ()
	{
		if (Input.GetKey (KeyCode.Escape))  
		{
			if (pauseButton.image.sprite == pauseSprite) 
			{
				PauseGame ();
			} 
			else
			{
				ResumeGame ();
			}
		}

		if (Input.GetKey (KeyCode.Home)) 
		{
			PauseGame ();
		}

		if (show3)
		{
			if (showTimer >= 1)
			{
				show3 = false;
				show2 = true;
				count3.enabled = false;
				count2.enabled = true;
				showTimer = 0;
			}
			else
			{
				showTimer += Time.unscaledDeltaTime;
			}
		}

		if (show2)
		{
			if (showTimer >= 1)
			{
				show2 = false;
				show1 = true;
				count2.enabled = false;
				count1.enabled = true;
				showTimer = 0;
			}
			else
			{
				showTimer += Time.unscaledDeltaTime;
			}
		}

		if (show1)
		{
			if (showTimer >= 1)
			{
				show1 = false;
				count1.enabled = false;
				showTimer = 0;
				player.GetComponent<PlayerController>().enabled = true;
				Time.timeScale = 1;
			}
			else
			{
				showTimer += Time.unscaledDeltaTime;
			}
		}

	}


	void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus)
		{
			PauseGame();
		}
	}

		
	void TogglePause() 
	{
		if (pauseButton.image.sprite == pauseSprite) {
			PauseGame ();
		}
		else {
			ResumeGame ();
		}
	}


	void PauseGame ()
	{
		AdController.ad.RequestBannerAd(1);
		pauseButton.image.sprite = playSprite;
		Time.timeScale = 0;

		player.GetComponent<PlayerController>().enabled = false;
		gameplayCanvas.SetActive(false);
		pauseCanvas.SetActive(true);
	}


	void ResumeGame()
	{
		pauseButton.image.sprite = pauseSprite;
		pauseCanvas.SetActive(false);
		gameplayCanvas.SetActive(true);

		show3 = true;
		count3.enabled = true;

		AdController.ad.HideBannerAd();
	}


	void ToggleAudio () 
	{
		if (soundButton.image.sprite == soundOnSprite) {
			soundButton.image.sprite = soundOffSprite;
		}
		else {
			soundButton.image.sprite = soundOnSprite;
		}
	}


	void GoToMainMenu()
	{
		AdController.ad.HideBannerAd();
		SceneManager.LoadScene ("MainMenu");
	}

}
