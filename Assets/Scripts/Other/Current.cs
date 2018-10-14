using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Current : MonoBehaviour 
{
	public GameObject winScene;
	public int level;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player" || col.gameObject.tag == "Float Player")
		{
			SaveData ();
			winScene.SetActive(true);
			Time.timeScale = 0f;
		}
	}

	void SaveData()
	{
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			LevelCompleteData data = (LevelCompleteData)bf.Deserialize (file);
			file.Close ();

			data.completedLevels [level] = true;

			file = File.Create (Application.persistentDataPath + "/playerInfo.dat");
			bf.Serialize (file, data);
			file.Close ();
		}
	}
}
