namespace FolderDetector.Models
{
    public class FolderModel
    {
        public FolderModel(string path)
        {
            Path = path;
            Name = new DirectoryInfo(path).Name;
            LoadFiles();
            DeletedFiles = new List<FileModel>();
        }

        public string Path { get; set; }

        public string Name { get; set; }

        public bool ChangeDetected { get; set; }

        public IDictionary<string, FileModel> Files { get; set; }

        public IEnumerable<FileModel> DeletedFiles { get; set; }

        public void LoadFiles()
        {
            Files = Directory.GetFiles(Path).Select(file => new FileModel(file)).ToDictionary(file => file.Name);
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
                    DeletedFiles = DeletedFiles.Append(file.Value);
                }
            }

            RemoveDeletedFiles();

            var currentFiles = Directory.GetFiles(Path).Select(file => new FileModel(file)).ToList();

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