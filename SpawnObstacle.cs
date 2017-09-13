using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnObstacle : MonoBehaviour {

	public Transform[] spawnPoints;
	public GameObject prefab;
	public Color32[] colors;

	public Text currentScore;


	private float timer;
	private float timeSinceStart;

	public float timeFactor;
	public Vector2 vel;


	void Awake() 
	{
		timeFactor = 1.0F;
		timeSinceStart = 0.0F;
		timer = 0.0F;
	}


	void Start () 
	{
		
		vel = new Vector2 (0.0F, -2F);
		timeFactor = 1.0F;
		timeSinceStart = 0.0F;
		timer = 0.0F;
		GameStatsController.data.Load ();
		GameStatsController.data.numberOfObstacles = 0;
		GameStatsController.data.timePlayed = 0;
		GameStatsController.data.colors = colors;
	}

		
	void Update () 
	{

		timeSinceStart += Time.deltaTime;
		GameStatsController.data.timePlayed += timeSinceStart;

		if (timeSinceStart < 10 && timer > 0.75) {
			CreateObstacle ();
			timer = 0.0F;
		}
		else {
			timer += Time.deltaTime;
		}
	}

		
	void CreateObstacle()
	{
		GameObject obj = Instantiate (prefab, spawnPoints[Random.Range(0, 4)]);
		obj.GetComponent<Renderer> ().material.color = colors[Random.Range(0, 16)];
		obj.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0.0F, vel [1] - timeFactor);
	}


	void OnTriggerEnter2D(Collider2D other) 
	{
		GameStatsController.data.numberOfObstacles++;
		GameStatsController.data.totalNumberOfObstacles++;
		currentScore.text = "" + GameStatsController.data.numberOfObstacles;

		if (timeSinceStart > 10) {
			timeFactor = Mathf.Pow (timeSinceStart, 1.01F) * 0.1F;
			CreateObstacle ();
		}
		Destroy (other.gameObject);
	}

}
