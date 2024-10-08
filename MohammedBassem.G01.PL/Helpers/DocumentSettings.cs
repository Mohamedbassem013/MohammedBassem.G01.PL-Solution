namespace MohammedBassem.G01.PL.Helpers
{
    public static class DocumentSettings
    {
        // 1.Upload --> لايى فايل مش صوره بس Upload دول شويه خطوات ثابته عشان اعمل
        public static string UploadFile(IFormFile file , string FolderName) // Images
        {
            // 1. Get Location Folder Path --> Upload هنا بحدد المكان بتاع الفولدر اللي هعمل فيه

            // string FolderPath = $"C:\\Users\\ABO Ahmed\\source\\repos\\MohamedBassem.G01 Solution\\Company.G01 Solution\\Company.G01.PL\\wwwroot\\Files\\{FolderName}";

            //string FolderPath =  Directory.GetCurrentDirectory() + @"wwwroot\Files"+ FolderName; // Static بتجيب مسار البروجيكت كامل بدل ما احط المسار

            // طريقه تانيه  

            string FolderPath = Path.Combine(Directory.GetCurrentDirectory() , @"wwwroot\Files" , FolderName);

            // 2. Get FileName Make It Unique --> Upload بحدد اسم الفايل اللي هعمله 

            //string FileName = $"{Guid.NewGuid()}{file.FileName}";
            string FileName = $"{Guid.NewGuid()}{file.FileName}";

            // 3. Get File Path --> FolderPath + FileName --> هنا بحدد الفايل باص 

            string FilePath = Path.Combine(FolderPath, FileName);

            // 4. Save File as Stream : Data Per Time

            using var FileStream = new FileStream(FilePath, FileMode.Create);

            file.CopyTo(FileStream); //Server علي ال Save عشان اعمله FileStream هنا باخد نسخه من ال

            return FileName;


        }

        // 2.Delete
        public static void DeleteFile(string fileName , string FolderName)
        {
            string FilePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files", FolderName , fileName);

            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }
        }
    }
}
