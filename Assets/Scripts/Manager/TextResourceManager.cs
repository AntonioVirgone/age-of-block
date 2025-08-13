using TMPro;
using UnityEngine;

public class TextResourceManager : MonoBehaviour {
	// Istanza privata con inizializzazione lazy
	private static TextResourceManager _instance;
	private static readonly object _lock = new object();
	private static bool _isApplicationQuitting = false;

	// Aggiungi questo metodo
	public static bool IsReady => _instance != null &&
	                              _instance._stoneText != null &&
	                              _instance._goldText != null;

	// Riferimenti UI (serializzati per l'Inspector)
	[Header("Resources")] [SerializeField] private TMP_Text _stoneText;
	[SerializeField] private TMP_Text _goldText;

	[Header("Revenues")] [SerializeField] private TMP_Text _revenueText;

	// Proprietà pubblica con controllo null e creazione automatica
	public static TextResourceManager Instance {
		get {
			if (_isApplicationQuitting) {
				Debug.LogWarning("Instance già distrutta durante l'uscita dall'applicazione");
				return null;
			}

			lock (_lock) {
				if (!_instance) {
					_instance = FindObjectOfType<TextResourceManager>();

					if (!_instance) {
						GameObject singletonObject = new GameObject(nameof(TextResourceManager));
						_instance = singletonObject.AddComponent<TextResourceManager>();
						Debug.LogWarning("TextResourceManager creato automaticamente nella scena");
					}

					DontDestroyOnLoad(_instance.gameObject);
				}

				return _instance;
			}
		}
	}

	private void Awake() {
		lock (_lock) {
			if (_instance != null && _instance != this) {
				Debug.LogWarning("Duplicato TextResourceManager distrutto", gameObject);
				Destroy(gameObject);
				return;
			}

			_instance = this;
			DontDestroyOnLoad(gameObject);

			// Inizializzazione aggiuntiva
			ValidateTextReferences();
		}
	}

	private void OnApplicationQuit() {
		_isApplicationQuitting = true;
	}

	private void ValidateTextReferences() {
		if (_stoneText == null) Debug.LogError("Stone Text non assegnato!", this);
		if (_goldText == null) Debug.LogError("Gold Text non assegnato!", this);
		if (_revenueText == null) Debug.LogError("Revenue Text non assegnato!", this);
	}

	// Metodi pubblici per aggiornare i testi
	public void UpdateStone(int value) {
		if (_stoneText)
			_stoneText.text = $"Stone: {value}";
	}

	public void UpdateGold(int value) {
		if (_goldText)
			_goldText.text = $"Gold: {value}";
	}

	public void UpdateRevenue(int value) {
		if (_revenueText != null)
			_revenueText.text = $"Revenue: {value}";
	}
}