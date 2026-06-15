namespace FolderDetector.Models
{
    public class FileModel
    {
        private int _hashCode;

        public FileModel(string path)
        {
            Path = path;
            Name = System.IO.Path.GetFileName(path);
            Version = 0;
            IsNew = true;
            CreateHashCode();
        }

        public bool CouldBeAnalysed { get; private set; }

        public string Name { get; private set; }

        public int Version { get; private set; }

        public string Path { get; private set; }

        public bool IsNew { get; set; }

        public bool CreateHashCode()
        {
            var fileContent = File.ReadAllText(Path);

            if (fileContent == null)
            {
                CouldBeAnalysed = false;
                return false;
            }

            var changeDetected = false;
            CouldBeAnalysed = true;

            var newHashCode = fileContent.GetHashCode();

            if (newHashCode != _hashCode)
            {
                Version++;
                changeDetected = true;
            }

            _hashCode = newHashCode;
            return changeDetected;
        }

    }
}
