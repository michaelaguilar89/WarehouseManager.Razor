using Microsoft.AspNetCore.DataProtection;
using System.Security.Cryptography;
using System.Text;

namespace WareHouseManager.Razor.Service
{
    public class EncryptedSevice
    {
        private readonly IDataProtector _dataProtector;
        private readonly IConfiguration _configuration;
        public EncryptedSevice(
            IDataProtectionProvider dataProtectionProvider,
                            IConfiguration configuration)
        {
           
            _dataProtector = dataProtectionProvider.CreateProtector("ndvnvmvmpwvoer,ber5gb1e61bt98n19w1n9w1nwr1n9"); ;
        }

        public string Protect(string data)
        {
            Console.WriteLine($"Protected Data: {data}");
            string protectedData = _dataProtector.Protect(data);
            return protectedData;

        }

        public string UnProtect(string protectedData)
        {
            Console.WriteLine($"Unprotected Data: {protectedData}");
            string unprotectedData = _dataProtector.Unprotect(protectedData);


            return unprotectedData;

        }
        public string HashString(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
