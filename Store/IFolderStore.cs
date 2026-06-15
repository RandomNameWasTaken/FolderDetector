using FolderDetector.Models;

namespace FolderDetector.Store
{
    public interface IFolderStore {
        FolderModel? Get(string key);
        void Set(string key, FolderModel folder);
        bool TryGet(string key, out FolderModel folder);
        IEnumerable<FolderModel> GetAll();
    }
}