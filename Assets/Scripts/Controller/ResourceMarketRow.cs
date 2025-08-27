using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceMarketRow : MonoBehaviour {
	[Header("Component")] 
	[SerializeField]
	private TMP_Text resourceType;    // Es. "STONE", "GOLD", ecc.
	[SerializeField]
	private Slider slider;
	[SerializeField]
	private Button sellButton;
	
	[Header("Settings")]
	[SerializeField]
	private TileResourceEnum resourceName;
	[SerializeField]
	private float minValue = 0f;
	[SerializeField] private float maxValue = 100f;
	[SerializeField] private float currentValue = 50f;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start() {
		var resourceData = LocalResourceManager.Instance.LoadData(resourceName);
		maxValue = resourceData?.resourceAmount ?? 0;
		currentValue = resourceData?.resourceAmount ?? 0;

		// Inizializza lo slider
		slider.minValue = minValue;
		slider.maxValue = maxValue;
		slider.value = currentValue;

		// Aggiorna il testo (se presente)
		if (resourceType != null)
			resourceType.text = $"{resourceName.ToString()}: {currentValue:F1}";

		// Aggiungi listener per i cambiamenti
		slider.onValueChanged.AddListener(OnSliderChanged);
	}

	// Chiamato quando lo slider viene mosso
	private void OnSliderChanged(float newValue) {
		currentValue = newValue;

		// Aggiorna il testo
		if (resourceType != null)
			resourceType.text = $"{resourceName.ToString()}: {currentValue:F1}";

		// Esegui calcoli basati sul valore
		PerformCalculations(currentValue);
	}

	// Esempio di calcoli basati sul valore
	private void PerformCalculations(float value) {
		float calculatedResult = value * 2.5f;
		Debug.Log($"Valore: {value} | Calcolo: {calculatedResult}");

		// Aggiungi qui la tua logica di calcolo
	}

	// Metodo per aggiornare lo slider da codice
	public void SetSliderValue(float newValue) {
		currentValue = Mathf.Clamp(newValue, minValue, maxValue);
		slider.value = currentValue;
	}
}