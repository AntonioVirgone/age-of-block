using System.Collections.Generic;

namespace Manager {
	public interface IResourceManager {
		public void SaveData(ResourceData newResource);
		public ResourceData LoadData(TileResourceEnum key);
		public List<ResourceData> LoadAllData();
	}
}