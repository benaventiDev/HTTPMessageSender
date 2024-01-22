using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HTTPMessageSender.http
{
    public class TokenGenerator
    {
        public static string GetRandomToken()
        {
            /*Random random = new Random();
            string randomString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            string randomSubstring = randomString.Substring(randomString.Length - 8);
            return randomSubstring;*/


            Random random = new Random();
            string randomString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            string randomSubstring = randomString.Substring(randomString.Length - 8);

            // Remove any non-alphanumeric characters
            Regex regex = new Regex("[^a-zA-Z0-9]");
            randomSubstring = regex.Replace(randomSubstring, "");

            return randomSubstring;
        }

    }
}
