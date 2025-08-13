using UnityEngine;

namespace Manager {
	public class SelectionManager : MonoBehaviour {
		public static SelectionManager Instance { get; private set; }

		// Variabili di stato
		private TileResourceEnum _selectedValue;

		private void Awake() {
			// Implementazione del pattern singleton
			if (Instance != null && Instance != this) {
				Destroy(gameObject);
			} else {
				Instance = this;
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
	}
}