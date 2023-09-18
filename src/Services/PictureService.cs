using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Unidecode.NET;

namespace StreetBook.Services;

public interface IPictureService
{
    Task<byte[]?> GetPicture(IHostEnvironment hostEnvironment, string fileName);
}

public class PictureService : IPictureService
{
    public async Task<byte[]?> GetPicture(IHostEnvironment hostEnvironment, string fileName)
    {
        var picturePath = GetPicturePath(hostEnvironment, fileName);

        if (!Path.Exists(picturePath))
        {
            picturePath = $"{hostEnvironment.ContentRootPath}/wwwroot/img/defaultAccount.jpg";
        }

        return await File.ReadAllBytesAsync(picturePath);
    }

    private static string GetPicturePath(IHostEnvironment hostEnvironment, string fileName)
    {
        var basePath = hostEnvironment.IsProduction() ? "/data/Data" : $"{hostEnvironment.ContentRootPath}/Data";
        var picturePath = Path.Combine(basePath, $"{fileName.Unidecode()}.jpg");
        return picturePath;
    }
}
