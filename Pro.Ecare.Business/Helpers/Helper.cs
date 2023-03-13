using System.Collections.Generic;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Poc.Ecare.Business.Helpers
{
    public class Helper
    {
        public static IList<int> SplitStringToListInt(string stringToSplit)
        {
            var ids = stringToSplit.Split(",", StringSplitOptions.RemoveEmptyEntries);
            var idsList = new List<int>();
            foreach (var id in ids)
            {
                bool isParsableId = int.TryParse(id, out int idInt);
                if (isParsableId)
                {
                    idsList.Add(idInt);
                }
            }
            return idsList;
        }

#pragma warning disable CS8632 // L'annotation pour les types référence Nullable doit être utilisée uniquement dans le code au sein d'un contexte d'annotations '#nullable'.
        public static string? CreateHashPassword(string? password)
#pragma warning restore CS8632 // L'annotation pour les types référence Nullable doit être utilisée uniquement dans le code au sein d'un contexte d'annotations '#nullable'.
        {
            if (string.IsNullOrEmpty(password)) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            using (var sha512Hash = SHA512.Create())
            {
                var bytes = sha512Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                var builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
