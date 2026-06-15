namespace FolderDetector.Models
{
    public class FolderModel
    {
        public FolderModel(string path)
        {
            Path = path;
            Name = new DirectoryInfo(path).Name;
            Files = new Dictionary<string, FileModel>();
            DeletedFiles = new List<FileModel>();
            Files = LoadFiles().ToDictionary(file => file.Name);
        }

        public string Path { get; set; }

        public string Name { get; set; }

        public bool ChangeDetected { get; set; }

        public IDictionary<string, FileModel> Files { get; set; }

        public List<FileModel> DeletedFiles { get; set; }

        public  IEnumerable<FileModel> LoadFiles()
        {
            try
            {
                return Directory.GetFiles(Path).Select(file => new FileModel(file)).ToList();
            }
            catch
            {
                // If directory access fails, leave Files empty
                return new List<FileModel>();
            }
        }

        public void AnalyseFiles()
        {
            ChangeDetected = false;
            SetIsNewForOldFiles();


            foreach (var file in Files)
            {
                if (File.Exists(file.Value.Path))
                {
                    ChangeDetected = file.Value.CreateHashCode() || ChangeDetected;
                }
                else
                {
                    DeletedFiles.Add(file.Value);
                }
            }

            RemoveDeletedFiles();

            var currentFiles = LoadFiles();

            foreach (var file in currentFiles)
            {
                if (!Files.ContainsKey(file.Name))
                {
                    Files[file.Name] = file;
                }
            }
        }
        

        private void SetIsNewForOldFiles()
        {
            foreach (var item in Files)
            {
                item.Value.IsNew = false;
            }
        }

        private void RemoveDeletedFiles()
        {
            if (DeletedFiles == null || DeletedFiles.Count == 0)
                return;

            foreach (var deletedFile in DeletedFiles)
            {
                if (Files.ContainsKey(deletedFile.Name))
                {
                    Files.Remove(deletedFile.Name);
                }
            }
        }
    }
}