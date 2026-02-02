using System;
using System.IO;
using System.Threading.Tasks;

namespace OrganizationAssets_and_FinanceManagement.Repositories
{
    public class DocumentUploadClass
    {
        public async Task<string> SaveBase64DocumentAsync(
            string fileName,
            string base64Data,
            string docType)
        {
            // Remove base64 header if exists (data:image/png;base64,...)
            if (base64Data.Contains(","))
                base64Data = base64Data.Substring(base64Data.IndexOf(",") + 1);

            byte[] fileBytes = Convert.FromBase64String(base64Data);

            // Rule:
            // PDF → .pdf
            // Everything else → .png
            string extension = docType?.ToLower() == "pdf" ? ".pdf" : ".png";

            // Ensure filename has correct extension
            fileName = Path.GetFileNameWithoutExtension(fileName) + extension;

            string folderPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "Documents"
            );

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string fullPath = Path.Combine(folderPath, fileName);

            await File.WriteAllBytesAsync(fullPath, fileBytes);

            // Return relative path to store in DB
            return $"/Documents/{fileName}";
        }
    }
}
