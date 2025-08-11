using UnityEngine;

public class TileController : MonoBehaviour {
	public int tileNumber;
	private SpriteRenderer spriteRenderer;
	private Color originalColor = new Color(1, 1, 1, 1);
	private bool isClickable = true;

	void Start() {
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void OnMouseDown() {
		Debug.Log($"Casella cliccata: {tileNumber}. SelectionManager {SelectionManager.Instance.GetSelectedValue()}");

		if (!isClickable) return;
		
		if (SelectionManager.Instance.GetSelectedValue() is TileResourceEnum.STONE) {
			StartCoroutine(ChangeColor(new Color(183f / 255f, 183f / 255f, 183f / 255f), 2f)); // Grigio chiaro iniziale
		} else if (SelectionManager.Instance.GetSelectedValue() is TileResourceEnum.GOLD) {
			StartCoroutine(ChangeColor(new Color(200f / 255f, 168f / 255f, 0f / 255f), 2f)); // Grigio chiaro iniziale
		}
		
		// Creiamo un esempio di dati
		ResourceData dati = new ResourceData();
		dati.resourceName = "Stone";
		dati.resourceAmount = 100;
		
		ResourceManager.Instance.SaveData(dati);

		Debug.Log(ResourceManager.Instance.LoadData().resourceName);
	}

	private System.Collections.IEnumerator ChangeColor(Color newColor, float timer) {
		// Cambia colore
		spriteRenderer.color = newColor;
		
		// Disattiva il click sul Tile
		isClickable = false;

		// Attendi X secondi
		yield return new WaitForSeconds(timer);
		
		// Ripristina colore
		spriteRenderer.color = originalColor;

		// Riattiva il click sul Tile
		isClickable = true;
	}
}