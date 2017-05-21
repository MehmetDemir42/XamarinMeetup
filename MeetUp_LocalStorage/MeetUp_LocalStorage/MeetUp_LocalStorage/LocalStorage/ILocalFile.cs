using System.IO;
using System.Threading.Tasks;

namespace MeetUp_LocalStorage.LocalStorage
{
    public interface ILocalFile
    {
        Task CreateFolder(string folderName);

        Task<bool> FolderExists(string folderName);

        Task<string> SaveFileFromStringContent(string folderName, string fileName, string fileContent);

        Task<bool> FileExists(string folderName, string fileName);

        Task<string> ReadFileAsString(string folderName, string fileName);

        Task<string> SaveFileFromStream(string folderName, string fileName, Stream fileStream);
    }
}