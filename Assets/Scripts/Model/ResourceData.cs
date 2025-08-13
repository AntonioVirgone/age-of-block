using System;

[Serializable]
public class ResourceData {
	public TileResourceEnum resourceType;
	public string resourceName;
	public int resourceAmount { get; set; }
}