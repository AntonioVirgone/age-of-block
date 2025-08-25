using UnityEngine;
using UnityEngine.UI;

public class PanelMarketController : MonoBehaviour {
	[Header("Riferimenti")] [SerializeField]
	private CanvasGroup canvasGroup;

	[SerializeField] private Button closeButton;

	void Start() {
		// Nascondi all'avvio
		//HidePanel();

		// Setup bottone chiusura
		if (closeButton != null)
			closeButton.onClick.AddListener(HidePanel);
	}

	public void ShowPanel() {
		canvasGroup.alpha = 1;
		canvasGroup.interactable = true;
		canvasGroup.blocksRaycasts = true;

		// Pausa eventuali elementi di gioco
		Time.timeScale = 0;
	}

	public void HidePanel() {
		canvasGroup.alpha = 0;
		canvasGroup.interactable = false;
		canvasGroup.blocksRaycasts = false;

		// Riprendi il gioco
		Time.timeScale = 1;
	}

	// Per attivare/disattivare da altri script
	public void TogglePanel() {
		if (canvasGroup.alpha > 0) {
			HidePanel();
		} else {
			ShowPanel();
		}
	}
}