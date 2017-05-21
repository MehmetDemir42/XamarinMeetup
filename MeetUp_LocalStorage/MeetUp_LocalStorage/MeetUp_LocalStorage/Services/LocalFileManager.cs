using MeetUp_LocalStorage.LocalStorage;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MeetUp_LocalStorage.Services
{
    public class LocalFileManager
    {
        public async Task<string> SaveFileFromString(string text, string extantion, string folderName, string fileName)
        {
            //String content i .txt extantion ile kaydediyorum.
            var lf = new LocalFile();
            var folderExists = await lf.FolderExists(folderName);
            if (!folderExists)
            {
                await lf.CreateFolder(folderName);
            }
            var retVal = await lf.SaveFileFromStringContent(folderName, fileName + "." + extantion, text);
            return retVal;
        }

        public async Task<string> GetSavedFile(string folderName, string fileName, string extantion)
        {
            var lf = new LocalFile();
            if (!await lf.FolderExists(folderName)) return string.Empty;
            //extantion .txt
            var retVal = await lf.ReadFileAsString(folderName, fileName + extantion);
            if (string.IsNullOrEmpty(retVal)) return string.Empty;
            return retVal;
        }

        // URL den gelen linki kaydetmek için Microsoft.Net.Http gerekli download için.
        public async Task<string> SaveFileFromUrl(string url, string folderName)
        {
            //Örnegin URL miz https://www.xamarin.com/content/images/pages/branding/assets/xamagon.png olsun burdan logo png dosyasını aynı isimle kaydetmek için split işlemlerimizi yapıp dosya adını oluşturuyoruz uzantısıyla beraber
            //sonra ise httpclient yardımıyla streami elde ediyoruz. sonrasinda save stream to file methodumuzu kullanıyoruz.

            if (string.IsNullOrWhiteSpace(url) || string.IsNullOrWhiteSpace(folderName))
                return "";

            var fileName = url.Split('/').Last();

            HttpClient webClient = new HttpClient();
            var fileStream = await webClient.GetStreamAsync(url);

            var lf = new LocalFile();
            return await lf.SaveFileFromStream(folderName, fileName, fileStream);
        }
    }
}