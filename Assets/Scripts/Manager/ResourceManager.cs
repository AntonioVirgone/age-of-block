using System.IO;
using UnityEngine;

public class ResourceManager : MonoBehaviour {
	public static ResourceManager Instance { get; set; }
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

	public void SaveData(ResourceData dati) {
		// Percorso del file nella cartella persistente del gioco
		filePath = Path.Combine(Application.persistentDataPath, "dati.json");

		// Salviamo
		var json = JsonUtility.ToJson(dati, true); // true = formato leggibile
		File.WriteAllText(filePath, json);
		Debug.Log("Dati salvati in: " + filePath);
	}

	public ResourceData LoadData() {
		if (File.Exists(filePath)) {
			string json = File.ReadAllText(filePath);
			ResourceData dati = JsonUtility.FromJson<ResourceData>(json);
			return dati;
		} else {
			Debug.LogWarning("File JSON non trovato!");
			return null;
		}
	}
}