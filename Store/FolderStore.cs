using System.Collections.Concurrent;
using FolderDetector.Models;

namespace FolderDetector.Store
{
    public class FolderStore : IFolderStore
    {
        private readonly ConcurrentDictionary<string, FolderModel> _map = new();
        public FolderModel Get(string key)
        {
            return _map.TryGetValue(key, out var folder) ? folder : null;
        }

        public void Set(string key, FolderModel folder)
        {
            _map[key] = folder;
        }

        public bool TryGet(string key, out FolderModel folder)
        {
            return _map.TryGetValue(key, out folder);
        }

        public IEnumerable<FolderModel> GetAll()
        {
            return _map.Values.ToList();
        }
    }
}
