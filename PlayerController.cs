using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {


	const float location1 = -1.8F;
	const float location2 = -0.8F;
	const float location3 = 0.8F;
	const float location4 = 1.8F;


	public float vertPos = -4.2F;
	public int location;

	private Vector2 touchPosition;

	private bool sameTouch;

	void Start ()
	{
		location = 2;
		sameTouch = false;
		GameStatsController.data.numberOfMoves = 0;
	}


	void Update () 
	{
		if (Input.touchCount > 0) {
			
			if (Input.GetTouch (0).phase == TouchPhase.Began) {
				touchPosition = Input.GetTouch (0).position;
				sameTouch = false;
			} else if (!sameTouch && Vector2.SqrMagnitude (touchPosition - Input.GetTouch (0).position) > 0.3) {
				if (touchPosition [0] > Input.GetTouch (0).position [0]) {
					MovePlayerLeft ();
				} else {
					MovePlayerRight ();
				}
				sameTouch = true;
			} 
		}
		else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			MovePlayerLeft ();
		}
		else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			MovePlayerRight ();
		}
	}


	void MovePlayerLeft() 
	{
		switch (location) {
		case 2:
			transform.position = new Vector2 (location1, vertPos);
			location = 1;
			break;
		case 3:
			transform.position = new Vector2 (location2, vertPos);
			location = 2;
			break;
		case 4:
			transform.position = new Vector2 (location3, vertPos);
			location = 3;
			break;
		}
		GameStatsController.data.numberOfMoves++;
	}


	void MovePlayerRight() 
	{
		switch (location) {
		case 1:
			transform.position = new Vector2 (location2, vertPos);
			location = 2;
			break;
		case 2:
			transform.position = new Vector2 (location3, vertPos);
			location = 3;
			break;
		case 3:
			transform.position = new Vector2 (location4, vertPos);
			location = 4;
			break;
		}
		GameStatsController.data.numberOfMoves++;
	}

		
	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.gameObject.name == "Obstacle(Clone)") 
		{
			GameStatsController.data.killColor = other.gameObject.GetComponent<Renderer> ().material.color;
			GameStatsController.data.Save ();
			SceneManager.LoadScene ("GameOver");
		}
	}

}
