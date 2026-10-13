
namespace SinsEntityConverter
{
    public enum OperationCode
    {
        Pending,
        InvalidPath,
        FileNotFound,
        InvalidContent,
        InvalidExtension,
        ConversionFailed,
        ConversionTimeout,
        Success
    }

    public enum RenderCode
    {
        LoadingStart,
        LoadSuccess,
        LoadFailed,
        LoadingComplete,
        ConversionStarted,
        ConversionSuccess,
        ConversionFailed,
        Cleanup,
        ConversionComplete
    }

    public class SinsFile
    {
        public string? Path;
        public string? FileName;
        public string? Ext;
        public string? conversion;
        public OperationCode OperationCode;
    }

    public class DataRepresentations
    {
        public SinsFile? curFile;
        public List<SinsFile>? curFiles;
        public List<SinsFile>? loadedFiles;
    }
}

