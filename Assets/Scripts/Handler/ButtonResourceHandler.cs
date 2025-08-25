using Manager;
using UnityEngine;

namespace Handler {
	public class ButtonResourceHandler : MonoBehaviour {
		public void HandleResourceButtonClick(GameObject clickedButton) {
			Debug.Log($"Resource button clicked: {clickedButton.tag}");

			var resourceType = clickedButton.tag switch {
				"BtnStone" => TileResourceEnum.STONE,
				"BtnGold" => TileResourceEnum.GOLD,
				"BtnMarket" => TileResourceEnum.MARKET,
				_ => TileResourceEnum.UNKNOWN
			};

			SelectionManager.Instance.SetSelectedValue(resourceType);
		}
	}
}