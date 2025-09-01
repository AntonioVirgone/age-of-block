using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceMarketRow : MonoBehaviour {
	[Header("Component")] [SerializeField] private TMP_Text resourceType; // Es. "STONE", "GOLD", ecc.
	[SerializeField] private Slider slider;
	[SerializeField] private Button sellButton;

	[Header("Settings")] [SerializeField] private TileResourceEnum resourceName;
	[SerializeField] private float minValue = 0f;
	[SerializeField] private float maxValue = 100f;
	[SerializeField] private float currentValue = 0f;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	private void Start() {
		var resourceData = LocalResourceManager.Instance.LoadData(resourceName);
		maxValue = resourceData?.resourceAmount ?? 0;
		currentValue = maxValue / 2;
		
		// Inizializza lo slider
		slider.minValue = minValue;
		slider.maxValue = maxValue;
		slider.value = currentValue;

		// Aggiorna il testo (se presente)
		if (resourceType != null)
			resourceType.text = $"{resourceName.ToString()}: {currentValue:F1}/{maxValue:F1}";

		// Aggiungi listener per i cambiamenti
		slider.onValueChanged.AddListener(OnSliderChanged);

		sellButton.onClick.AddListener(OnSellClicked);
	}

	// Chiamato quando lo slider viene mosso
	private void OnSliderChanged(float newValue) {
		currentValue = newValue;

		// Aggiorna il testo
		if (resourceType != null)
			resourceType.text = $"{resourceName.ToString()}: {currentValue:F1}/{maxValue:F1}";
	}

	// Metodo per aggiornare lo slider da codice
	private void SetSliderValue(float newValue) {
		slider.value = maxValue / 2;
		slider.minValue = minValue;
		slider.maxValue = maxValue;
	}

	// Chiamato quando si preme il button
	private void OnSellClicked() {
		var amountToSell = slider.value;
		maxValue -= amountToSell;
		
		SetSliderValue(amountToSell);
		var resourceData = new ResourceData {
			resourceAmount = maxValue,
			resourceName = resourceName.ToString(),
			resourceType = resourceName
		};

		LocalResourceManager.Instance.SaveData(resourceData);
		
		TextResourceManager.Instance.UpdateResource(resourceData);
		TextResourceManager.Instance.UpdateRevenue(amountToSell);
	}
}