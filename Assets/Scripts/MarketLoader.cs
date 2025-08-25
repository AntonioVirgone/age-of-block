using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MarketLoader : MonoBehaviour {
	public void OpenMarket() {
		StartCoroutine(LoadMarketAsync());
	}

	IEnumerator LoadMarketAsync() {
		// Carica la seconda scena in modo additivo
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MarketScene", LoadSceneMode.Additive);

		while (!asyncLoad.isDone) {
			// Puoi mostrare una barra di progresso qui se vuoi
			yield return null;
		}

		// Facoltativo: imposta la nuova scena come attiva
		Scene marketScene = SceneManager.GetSceneByName("MarketScene");
		if (marketScene.IsValid()) {
			SceneManager.SetActiveScene(marketScene);
		}
	}
}