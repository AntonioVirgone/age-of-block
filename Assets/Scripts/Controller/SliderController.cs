using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
	[Header("Riferimenti")]
	[SerializeField] private Slider slider;
	[SerializeField] private TMP_Text valueText; // Opzionale: per visualizzare il valore

	[Header("Impostazioni")]
	[SerializeField] private float minValue = 0f;
	[SerializeField] private float maxValue = 100f;
	[SerializeField] private float currentValue = 50f;

	void Start()
	{
		// Inizializza lo slider
		slider.minValue = minValue;
		slider.maxValue = maxValue;
		slider.value = currentValue;

		// Aggiorna il testo (se presente)
		if (valueText != null)
			valueText.text = $"Stone: {currentValue.ToString("F1")}";

		// Aggiungi listener per i cambiamenti
		slider.onValueChanged.AddListener(OnSliderChanged);
	}

	// Chiamato quando lo slider viene mosso
	private void OnSliderChanged(float newValue)
	{
		currentValue = newValue;
        
		// Aggiorna il testo
		if (valueText != null)
			valueText.text = $"Stone: {currentValue.ToString("F1")}";

		// Esegui calcoli basati sul valore
		PerformCalculations(currentValue);
	}

	// Esempio di calcoli basati sul valore
	private void PerformCalculations(float value)
	{
		float calculatedResult = value * 2.5f;
		Debug.Log($"Valore: {value} | Calcolo: {calculatedResult}");

		// Aggiungi qui la tua logica di calcolo
	}

	// Metodo per aggiornare lo slider da codice
	public void SetSliderValue(float newValue)
	{
		currentValue = Mathf.Clamp(newValue, minValue, maxValue);
		slider.value = currentValue;
	}
}