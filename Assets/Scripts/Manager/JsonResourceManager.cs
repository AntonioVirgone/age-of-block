using System.Collections.Generic;
using System.IO;using Manager;
using UnityEngine;

public class JsonResourceManager : MonoBehaviour, IResourceManager {
	public static JsonResourceManager Instance { get; set; }
	private string filePath;

	private void Awake() {
		// Singleton base
		if (Instance != null && Instance != this) {
			Destroy(gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);

		filePath = Path.Combine(Application.persistentDataPath, "players.json");
	}

	public void SaveData(ResourceData newResource) {
		// Percorso del file nella cartella persistente del gioco
		filePath = Path.Combine(Application.persistentDataPath, "dati.json");

		// Salviamo
		var json = JsonUtility.ToJson(newResource, true); // true = formato leggibile
		File.WriteAllText(filePath, json);
		Debug.Log("Dati salvati in: " + filePath);
	}

	public ResourceData LoadData(TileResourceEnum key) {
		if (File.Exists(filePath)) {
			string json = File.ReadAllText(filePath);
			ResourceData dati = JsonUtility.FromJson<ResourceData>(json);
			return dati;
		} else {
			Debug.LogWarning("File JSON non trovato!");
			return null;
		}
	}

	public List<ResourceData> LoadAllData() {
		return new List<ResourceData>();
	}
}