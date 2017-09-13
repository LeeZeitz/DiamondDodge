using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;

public class GameStatsController : MonoBehaviour {


	public static GameStatsController data;
	private string fileName = "/playerData";

	public float numberOfObstacles;
	public float record;
	public float numberOfMoves;
	public float timePlayed;
	public float totalNumberOfObstacles;
	public float totalNumberOfMoves;
	public float totalTimePlayed;

	public float archivedRecord;
	public float archivedTotalNumberOfObstacles;
	public float archivedTotalNumberOfMoves;
	public float archivedTotalTimePlayed;

	public float startTime;

	public Color32[] colors;
	public int[] colorCounter;
	public Color32 killColor;


	void Awake () 
	{
		if (data == null) {
			DontDestroyOnLoad (gameObject);
			data = this;
		}
		else if (data != this) 
		{
			Destroy (gameObject);
		}

		startTime = 0;
		colorCounter = new int[17];
	}


	public void Save()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + fileName);
		PlayerData archive = new PlayerData ();

		// Increment value in colorCounter corresponding to the round's kill color
		for (int i = 0; i < colors.Length; i++)
		{
			if (colors[i].ToString() == killColor.ToString())
			{
				colorCounter[i]++;
				break;
			}
		}

		timePlayed = Time.time - startTime;

		totalNumberOfObstacles = archivedTotalNumberOfObstacles + numberOfObstacles;
		totalNumberOfMoves = archivedTotalNumberOfMoves + numberOfMoves;
		totalTimePlayed = archivedTotalTimePlayed + timePlayed;

		archive.totalNumberOfObstacles = totalNumberOfObstacles;
		archive.totalNumberOfMoves = totalNumberOfMoves;
		archive.totalTimePlayed = totalTimePlayed;
		archive.colorCounter = colorCounter;

		if (archivedRecord > numberOfObstacles) {
			archive.record = archivedRecord;
			record = archivedRecord;
		}
		else {
			archive.record = numberOfObstacles;
			record = numberOfObstacles;
		}

		bf.Serialize (file, archive);
		file.Close ();
	}


	public void Load()
	{
		if (File.Exists (Application.persistentDataPath + fileName)) 
		{
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + fileName, FileMode.Open);
			try
			{
				PlayerData archive = (PlayerData)bf.Deserialize (file);
				archivedRecord = archive.record;
				archivedTotalNumberOfObstacles = archive.totalNumberOfObstacles;
				archivedTotalNumberOfMoves = archive.totalNumberOfMoves;
				archivedTotalTimePlayed = archive.totalTimePlayed;
				colorCounter = archive.colorCounter;
			}

			catch(SerializationException e)
			{
				Debug.Log (e);
				archivedRecord = numberOfObstacles;
				archivedTotalNumberOfObstacles = numberOfObstacles;
				archivedTotalNumberOfMoves = numberOfMoves;
				totalTimePlayed = 0;
				colorCounter = new int[colorCounter.Length];
			}
			file.Close ();
		}
	}
}


[Serializable]
class PlayerData
{
	public float record;
	public float totalNumberOfObstacles;
	public float totalNumberOfMoves;
	public float totalTimePlayed;
	public int[] colorCounter;
}