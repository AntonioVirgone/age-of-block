using UnityEngine;

public class SelectionManager : MonoBehaviour {
	// Istanza del singleton
	private static SelectionManager _instance;
	public static SelectionManager Instance => _instance;

	// Variabili di stato
	private TileResourceEnum _selectedValue;

	private void Awake() {
		// Implementazione del pattern singleton
		if (_instance != null && _instance != this) {
			Destroy(gameObject);
		} else {
			_instance = this;
			_selectedValue = TileResourceEnum.UNKNOWN;
			DontDestroyOnLoad(gameObject);
		}
	}

	// Metodo per impostare il valore
	public void SetSelectedValue(TileResourceEnum value) {
		_selectedValue = value;
		Debug.Log($"Valore impostato: {value}");
	}

	public TileResourceEnum GetSelectedValue() {
		return _selectedValue;
	}

	/*
	// Metodo per verificare e processare la selezione
	public void ProcessSelection(int newValue) {
		if (_selectedValue == -1) {
			// Prima selezione
			SetSelectedValue(newValue);
		} else {
			// Seconda selezione - logica di confronto
			CompareSelections(_selectedValue, newValue);
			_selectedValue = -1; // Resetta dopo il confronto
		}
	}
	*/

	/*
	private void CompareSelections(int first, int second) {
		Debug.Log($"Confronto tra {first} e {second}");

		// Aggiungi qui la tua logica di confronto
		if (first == second) {
			Debug.Log("Selezioni uguali! Azione speciale");
			// Esempio: cambia colore, distruggi le tile, ecc.
		} else {
			Debug.Log("Selezioni diverse");
		}
	}
*/
}