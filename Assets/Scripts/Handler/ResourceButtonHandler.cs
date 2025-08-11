using UnityEngine;

namespace Handler {
	public class ResourceButtonHandler : MonoBehaviour {
		public void HandleResourceButtonClick(GameObject clickedButton)
		{
			Debug.Log($"Resource button clicked: {clickedButton.tag}");
        
			var resourceType = clickedButton.tag switch
			{
				"BtnStone" => TileResourceEnum.STONE,
				"BtnGold" => TileResourceEnum.GOLD,
				_ => TileResourceEnum.UNKNOWN
			};
        
			SelectionManager.Instance.SetSelectedValue(resourceType);
		}
	}
}