using System.Collections.Generic;
using System.IO;
using Manager;
using Mono.Cecil;
using UnityEngine;

public class LocalResourceManager : MonoBehaviour {
	public static LocalResourceManager Instance { get; set; }

	private List<ResourceData> resourceDataList;

	private void Awake() {
		// Singleton base
		if (Instance != null && Instance != this) {
			Destroy(gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);
	}

	public ResourceData SaveData(ResourceData newResource) {
		resourceDataList ??= new List<ResourceData>();
		var itemToUpdate = resourceDataList.Find(item => item.resourceType == newResource.resourceType);
		if (itemToUpdate != null) {
			itemToUpdate.resourceAmount += newResource.resourceAmount; // Modifica diretta (gli oggetti sono reference)
		} else {
			Debug.LogWarning($"Item con ID {newResource.resourceType} non trovato!");
			resourceDataList.Add(newResource);
		}
		
		return itemToUpdate;
	}

	public ResourceData LoadData(TileResourceEnum key) {
		return resourceDataList.Find(item => item.resourceType.Equals(key));
	}

	public List<ResourceData> LoadAllData() {
		return resourceDataList;
	}
}