using PCLStorage;
using System.IO;
using System.Threading.Tasks;

namespace MeetUp_LocalStorage.LocalStorage
{
    public class LocalFile : ILocalFile
    {
        public async Task CreateFolder(string folderName)
        {
            // PCL IFolder oluşturuluyor. Spesifik cihazının local storage alanında.
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            // verilen isimde klasör oluşturuluyor. Eğer klasör var ise bu klasör açılıyor.Üzerine yazma varsa hata dön gibi creationcollisionoptionlar mevcut...
            await rootFolder.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);
        }

        public async Task<bool> FolderExists(string folderName)
        {
            // PCL ile LocalStorage root a gidiyoruz ve check exitst kontrolü yapıyoruz. Dönen ExistenceCheckResult Folder , File veya Notfound şeklinde bunu senaryomuza göre kullanıyoruz.
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            var requestedFolder = await rootFolder.CheckExistsAsync(folderName);
            if (requestedFolder == ExistenceCheckResult.FolderExists)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<string> SaveFileFromStringContent(string folderName, string fileName, string fileContent)
        {
            //GetFileFromFolder yardımıyla klasörün içerisine dosyamızı oluşturuyoruz. içeriğini ise writealltextasync methodu ile doldurup dosyanın pathini geri dönüyoruz.
            var requestedFile = await GetFileFromFolder(folderName, fileName);
            if (requestedFile != null)
            {
                await requestedFile.WriteAllTextAsync(fileContent);
                return requestedFile.Path;
            }
            else
            {
                return string.Empty;
            }
        }

        public async Task<bool> FileExists(string folderName, string fileName)
        {
            // PCL ile LocalStorage root a gidiyoruz ve check exitst kontrolü yapıyoruz. Dönen ExistenceCheckResult Folder , File veya Notfound şeklinde bunu senaryomuza göre kullanıyoruz.
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            if (await FolderExists(folderName))
            {
                var requestedFolder = await rootFolder.GetFolderAsync(folderName);
                if (requestedFolder == null)
                {
                    return false;
                }
                ExistenceCheckResult requestedFileResult = await requestedFolder.CheckExistsAsync(fileName);
                if (requestedFileResult == ExistenceCheckResult.FileExists)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public async Task<string> ReadFileAsString(string folderName, string fileName)
        {
            //GetFileFromFolder yardımıyla klasörün içerisine dosyamızı getiriyoruz. içeriğini ise readalltextasync methodu ile okuyup dosyanın contentini geri dönüyoruz.
            var requestedFile = await GetFileFromFolder(folderName, fileName);
            if (requestedFile != null)
            {
                var content = await requestedFile.ReadAllTextAsync();
                return content;
            }
            else
            {
                return string.Empty;
            }
        }

        public async Task<string> SaveFileFromStream(string folderName, string fileName, Stream stream)
        {
            //Belirlediğiniz klasörün altına belirlediğimiz isim ile elimizdeki stream i dosya olarak kaydediyoruz. bunu yaparken stream i file open asnyc methodu ile kullanıyoruz.
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder folder = await rootFolder.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);

            IFile file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);

            using (Stream fileStream = await file.OpenAsync(FileAccess.ReadAndWrite))
            {
                stream.CopyTo(fileStream);
            }
            return file.Path;
        }

        #region Helper

        public async Task<IFile> GetFileFromFolder(string folderName, string fileName)
        {
            // Bu method ile klasörden dosya çağırıyoruz. CreationCollisionOption durumlarından OpenIfexits seçili eğer dosya var ise dosyayı dönüyor.
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            var exits = await FolderExists(folderName);
            if (exits)
            {
                IFolder requestedFolder = await rootFolder.GetFolderAsync(folderName);
                IFile requestedFile = await requestedFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
                return requestedFile;
            }
            else
            {
                return null;
            }
        }

        #endregion Helper
    }
}