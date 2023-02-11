using AutoMapper;
using System;
using System.IO;
using System.Text;
using ULearn.Common.Extensions;
using ULearn.Core.Managers;
using ULearn.DbModel.Models;

namespace ULearn.Core.Manager
{
    public class HelperManager : IHelperManager
    {
        private readonly IMapper _mapper;
        private ulearndbContext _context;

        public HelperManager(ulearndbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public string SaveImage(string base64img, string baseFolder)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(baseFolder))
                {
                    throw new ServiceValidationException("Invalid folder name for upload images");
                }

                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), baseFolder);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var base64Array = base64img.Split(";base64,");
                if (base64Array.Length < 1)
                {
                    return "";
                }

                base64img = base64Array[1];
                var fileName = $"{Guid.NewGuid()}{".png"}".Replace("-", "", StringComparison.InvariantCultureIgnoreCase);
                if (!string.IsNullOrWhiteSpace(folderPath))
                {
                    var url = $@"{baseFolder}\{fileName}";
                    fileName = @$"{folderPath}\{fileName}";
                    File.WriteAllBytes(fileName, Convert.FromBase64String(base64img));
                    return url;
                }

                return "";
            }
            catch (Exception ex)
            {
                throw new ServiceValidationException(ex.Message);
            }
        }

        public string Base64ToString(string base64String)
        {
            byte[] data = Convert.FromBase64String(base64String);
            return Encoding.ASCII.GetString(data);
        }
    }
}
