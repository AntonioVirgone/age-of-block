using UnityEngine;

namespace Manager {
	public class GlobalManager:MonoBehaviour {
		[Header("Settings")]
		[SerializeField] private KeyCode marketKey = KeyCode.M;
		
		private void Update() {
			if (Input.GetKeyDown(marketKey)) {
				// Per mostrare
				FindFirstObjectByType<PanelMarketController>().ShowPanel();
			}
		}
	}
}