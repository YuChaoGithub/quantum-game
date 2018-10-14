using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class DataSaverAndLoader : MonoBehaviour {
	public GameObject[] completeIndicators;

	void Awake()
	{
		Load ();
	}

	void Load()
	{
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			LevelCompleteData data = (LevelCompleteData)bf.Deserialize (file);
			file.Close ();

			for (int i = 0; i < completeIndicators.Length; i++) {
				completeIndicators [i].SetActive (data.completedLevels [i]);
			}
		} else {
			Save ();
		}
	}

	void Save()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/playerInfo.dat");

		LevelCompleteData data = new LevelCompleteData ();
		data.completedLevels = new bool[completeIndicators.Length];
		for (int i = 0; i < data.completedLevels.Length; i++) {
			data.completedLevels [i] = false;
		}

		bf.Serialize (file, data);
		file.Close ();
	}
}
	