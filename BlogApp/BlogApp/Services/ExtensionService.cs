using System;
using System.Text;

namespace BlogApp.Services
{

    public static class ExtensionService
    {
    static List<string> extensionList = new List<string>();

        
        public static string GenerateRandomString(int length)
        {
            StringBuilder result = new StringBuilder(length);
            bool IsExtensionAlreadyUsed = true;

            while (IsExtensionAlreadyUsed)
            {
                const string chars = "0123456789ABCDEFGHIJKLMNOPRSTUVWXYZ";

                Random random = new Random();

                for (int i = 0; i < length; i++)
                {
                result.Append(chars[random.Next(chars.Length)]);
                }

                if(!extensionList.Any(x => x == result.ToString()))
                {
                extensionList.Add(result.ToString());
                IsExtensionAlreadyUsed = false;
                }
            }

            return result.ToString();
        }
    }
}