using System;

[Serializable]
public class ResourceData {
	public TileResourceEnum resourceType;
	public string resourceName;
	public float resourceAmount { get; set; }
}