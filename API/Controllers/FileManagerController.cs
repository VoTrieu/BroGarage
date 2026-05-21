using BroGarage.API.Attributes;
using BroGarage.API.Data;
using BroGarage.API.Shared.Models;
using BroGarage.API.Shared.ResponseModels.FileManager;
using BroGarage.API.Shared.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace BroGarage.API.Controllers;

[Route("file-manager")]
[Auth]
public class FileManagerController : BaseController
{
    private readonly IConfiguration configuration;

    public FileManagerController(DatabaseContext db, IConfiguration configuration) : base(db)
    {
        this.configuration = configuration;
    }

    [HttpPost("upload-image")]
    public async Task<ActionResult<ResponseModel<FileManagerResModel>>> UploadImage(IFormFile file)
    {
        ResponseModel<FileManagerResModel> response = new();

        DateOnly now = DateOnly.FromDateTime(DateTime.Now);

        int year = now.Year;
        int month = now.Month;
        int day = now.Day;

        try
        {
            file = Request.Form.Files[0];
        }
        catch
        {
            response.Message = "Vui lòng đính kèm ảnh";
            return Ok(response);
        }

        string fileName = file.FileName;
        var stream = file.OpenReadStream();
        var fileSize = stream.Length;
        var fileExtension = Path.GetExtension(fileName);
        var file_extensions = configuration.GetSection("ImgExtensions").Get<List<string>>() ?? new();
        if (!file_extensions.Any(n => n.Equals(fileExtension)))
        {
            response.Message = "Định dạng ảnh không cho phép";
            return Ok(response);
        }
        string appDataPath = Directory.GetCurrentDirectory();
        string resourceFolderPath = Path.Combine("Resources", year.ToString(), month.ToString(), day.ToString());
        var uploadPath = Path.Combine(appDataPath, resourceFolderPath);
        if (!Directory.Exists(uploadPath))
        {
            Directory.CreateDirectory(uploadPath);
        }
        string random_name = StringUtility.Random(30);
        var image = await Image.LoadAsync(stream);
        int width = image.Width;
        int height = image.Height;
        int image_max_size = configuration.GetValue<int>("ImgMaxSize");
        int max_input_size = width > height ? width : height;
        image_max_size = image_max_size > max_input_size ? max_input_size : image_max_size;
        Size download_size = new(0, 0);
        if (width > height)
        {
            download_size.Width = image_max_size;
        }
        else
        {
            download_size.Height = image_max_size;
        }
        string new_download_file = $"{random_name}.jpg";
        string local_file_path = Path.Combine(uploadPath, new_download_file);

        image.Mutate(n => n.Resize(download_size));
        await image.SaveAsJpegAsync(local_file_path);

        var fileInfo = new FileInfo(local_file_path);

        //string strDate = now.ToString("yyyy/MM/dd");

        string strDate = $"{year}/{month}/{day}";

        string download_path = $"/resources/{strDate}/{new_download_file}";

        string fullUrl = $"{Request.Scheme}://{Request.Host}{download_path}";

        string thumbnailUrl = $"{Request.Scheme}://{Request.Host}/file-manager/thumbnail?url={download_path}&size=200";

        response.Result = new FileManagerResModel()
        {
            Url = download_path,
            FullUrl = fullUrl,
            ThumbnailUrl = thumbnailUrl
        };

        response.IsSuccess = true;

        return Ok(response);
    }

    [HttpPost("upload-file")]
    public async Task<ActionResult<ResponseModel<FileManagerResModel>>> UploadFile(IFormFile file)
    {
        ResponseModel response = new();

        DateOnly now = DateOnly.FromDateTime(DateTime.Now);

        int year = now.Year;
        int month = now.Month;
        int day = now.Day;

        try
        {
            file = Request.Form.Files[0];
        }
        catch
        {
            response.Message = "Vui lòng đính kèm file";
            return Ok(response);
        }

        string fileName = file.FileName;
        var stream = file.OpenReadStream();
        var fileSize = stream.Length;
        var fileExtension = Path.GetExtension(fileName);
        var file_extensions = configuration.GetSection("FileExtensions").Get<List<string>>() ?? new();
        if (!file_extensions.Any(n => n.Equals(fileExtension)))
        {
            response.Message = "Định dạng file không cho phép";
            return Ok(response);
        }
        string appDataPath = Directory.GetCurrentDirectory();
        string resourceFolderPath = Path.Combine("Resources", year.ToString(), month.ToString(), day.ToString());
        var uploadPath = Path.Combine(appDataPath, resourceFolderPath);
        if (!Directory.Exists(uploadPath))
        {
            Directory.CreateDirectory(uploadPath);
        }
        string random_name = StringUtility.Random(30);
        string new_download_file = $"{random_name}{fileExtension}";
        string local_file_path = Path.Combine(uploadPath, new_download_file);

        using Stream fileStream = new FileStream(local_file_path, FileMode.Create);
        await file.CopyToAsync(fileStream);

        string strDate = $"{year}/{month}/{day}";

        string download_path = $"/resources/{strDate}/{new_download_file}";

        string fullUrl = $"{Request.Scheme}://{Request.Host}{download_path}";

        response.Result = new FileManagerResModel()
        {
            Url = download_path,
            FullUrl = fullUrl
        };
        response.IsSuccess = true;

        return Ok(response);
    }

    [HttpGet("thumbnail")]
    [AllowAnonymous]
    public async Task<FileResult> Thumbnail(string url, int size = 200)
    {
        if (!url.StartsWith("/"))
        {
            url = "/" + url;
        }

        string appFolderPath = Directory.GetCurrentDirectory();

        string resourceFilePath = appFolderPath + url;

        byte[]? bytes = null;

        Image? originalImage = null;

        bool hasImg = true;

        if (string.IsNullOrEmpty(url))
        {
            hasImg = false;
        }
        else if (!System.IO.File.Exists(resourceFilePath))
        {
            hasImg = false;
        }

        if (hasImg)
        {
            originalImage = await Image.LoadAsync(resourceFilePath);
        }
        else
        {
            string noImg = $"{appFolderPath}\\Images\\default-image.png";
            originalImage = await Image.LoadAsync(noImg);
        }

        int width = originalImage.Width;
        int height = originalImage.Height;

        var pictureThumbnailSize = GetThumbnailSize(width, height, size);

        originalImage.Mutate(n => n.Resize(pictureThumbnailSize));

        using MemoryStream? memoryStream = new();

        await originalImage.SaveAsJpegAsync(memoryStream);

        bytes = memoryStream.ToArray();

        return File(bytes, "image/jpeg");
    }

    private static Size GetThumbnailSize(int width, int height, int thumbSize)
    {
        int thumbnailWidth = thumbSize;
        int thumbnailHeight = thumbSize;
        if (width > height)
        {
            decimal per = Math.Round((decimal)width / height, 1);
            thumbnailWidth = Convert.ToInt32(thumbSize * per);
        }
        else
        {
            decimal per = Math.Round((decimal)height / width, 1);
            thumbnailHeight = Convert.ToInt32(thumbSize * per);
        }
        Size thumbnailSize = new(thumbnailWidth, thumbnailHeight);
        return thumbnailSize;
    }
}
