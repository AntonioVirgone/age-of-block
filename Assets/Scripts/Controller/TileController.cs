using System;
using System.Collections;
using Manager;
using UnityEngine;

namespace Controller {
	[RequireComponent(typeof(SpriteRenderer))]
	public class TileController : MonoBehaviour {
		[Header("Tile Settings")] [SerializeField]
		public int tileNumber;

		[SerializeField] private float colorChangeDuration = 2f;

		[Header("Tile Colors")] [SerializeField]
		private Color stoneColor = new Color(183f / 255f, 183f / 255f, 183f / 255f);
		[SerializeField] private Color goldColor = new Color(200f / 255f, 168f / 255f, 0f / 255f);
		[SerializeField] private Color woodColor = new Color(200f / 255f, 168f / 255f, 0f / 255f);
		[SerializeField] private Color grainColor = new Color(200f / 255f, 168f / 255f, 0f / 255f);
		[SerializeField] private Color defaultColor = Color.white;

		private SpriteRenderer _spriteRenderer;
		private bool _isClickable = true;
		private Coroutine _colorChangeCoroutine;

		private void Awake() {
			_spriteRenderer = GetComponent<SpriteRenderer>();
			_spriteRenderer.color = defaultColor;
		}

		private void OnMouseDown() {
			if (!_isClickable || !SelectionManager.Instance)
				return;

			HandleTileClick();
		}

		private void HandleTileClick() {
			var selectedResource = SelectionManager.Instance.GetSelectedValue();
			Debug.Log($"Tile {tileNumber} clicked. Selected resource: {selectedResource}");

			if (_colorChangeCoroutine != null) {
				StopCoroutine(_colorChangeCoroutine);
			}

			switch (selectedResource) {
				case TileResourceEnum.STONE:
					_colorChangeCoroutine = StartCoroutine(ChangeColorCoroutine(
						stoneColor,
						() => UpdateResource(TileResourceEnum.STONE, 100)));
					break;
				case TileResourceEnum.GOLD:
					_colorChangeCoroutine = StartCoroutine(ChangeColorCoroutine(
						goldColor,
						() => UpdateResource(TileResourceEnum.GOLD, 80)));
					break;
				case TileResourceEnum.WOOD:
					_colorChangeCoroutine = StartCoroutine(ChangeColorCoroutine(
						woodColor,
						() => UpdateResource(TileResourceEnum.WOOD, 120)));
					break;
				case TileResourceEnum.GRAIN:
					_colorChangeCoroutine = StartCoroutine(ChangeColorCoroutine(
						grainColor,
						() => UpdateResource(TileResourceEnum.GRAIN, 30)));
					break;
				case TileResourceEnum.UNKNOWN:
				default:
					Debug.LogWarning($"Unhandled resource type: {selectedResource}");
					break;
			}
		}

		private IEnumerator ChangeColorCoroutine(Color targetColor, Action onComplete) {
			// Disabilita interazione
			_isClickable = false;

			// Cambia colore
			_spriteRenderer.color = targetColor;

			// Attendi
			yield return new WaitForSeconds(colorChangeDuration);

			// Ripristina colore
			_spriteRenderer.color = defaultColor;

			// Riabilita interazione
			_isClickable = true;

			// Callback
			onComplete?.Invoke();

			_colorChangeCoroutine = null;
		}

		private void UpdateResource(TileResourceEnum resourceType, int amount) {
			if (!JsonResourceManager.Instance || !TextResourceManager.Instance) {
				Debug.LogError("Managers not initialized!");
				return;
			}

			var resourceData = new ResourceData {
				resourceType = resourceType,
				resourceName = resourceType.ToString()
			};

			resourceData.resourceAmount += amount;

			var resourceSaved = LocalResourceManager.Instance.SaveData(resourceData);
			
			TextResourceManager.Instance.UpdateResource(resourceSaved);
		}

		private void OnDisable() {
			if (_colorChangeCoroutine != null) {
				StopCoroutine(_colorChangeCoroutine);
				_colorChangeCoroutine = null;
			}

			// Ripristina stato iniziale
			_isClickable = true;
			_spriteRenderer.color = defaultColor;
		}
	}
}