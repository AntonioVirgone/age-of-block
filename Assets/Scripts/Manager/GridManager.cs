using Controller;
using UnityEngine;

public class GridManager : MonoBehaviour {
	[SerializeField] private GameObject tilePrefab;
	public int gridSize = 4;
	public float tileSize = 1f; // Dimensione di ogni tile
	public float spacing = 0.1f; // Spazio tra i tile

	private void Start() {
		GenerateCenteredGrid();
	}
	
	private void GenerateCenteredGrid() {
		if (tilePrefab == null) {
			Debug.LogError("Tile Prefab non assegnato!");
			return;
		}

		// Calcola la dimensione totale della griglia (compresi spazi)
		var totalWidth = (gridSize * tileSize) + ((gridSize - 1) * spacing);
		var totalHeight = (gridSize * tileSize) + ((gridSize - 1) * spacing);

		// Punto di partenza per centrare la griglia
		var startPos = new Vector2(
			-totalWidth / 2 + tileSize / 2,
			totalHeight / 2 - tileSize / 2
		);

		for (var y = 0; y < gridSize; y++) {
			for (var x = 0; x < gridSize; x++) {
				// Calcola la posizione centrata
				var tilePosition = new Vector2(
					startPos.x + x * (tileSize + spacing),
					startPos.y - y * (tileSize + spacing)
				);

				var tile = Instantiate(tilePrefab, tilePosition, Quaternion.identity, transform);

				var tileController = tile.GetComponent<TileController>();
				if (tileController == null) continue;
				var tileNumber = (y * gridSize) + x + 1;
				tileController.tileNumber = tileNumber;
				tile.name = $"Tile_{tileNumber}";
			}
		}
	}
}