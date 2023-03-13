using Poc.Ecare.Infrastructure.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poc.Ecare.Infrastructure.Models.Seeds
{
    public class AccessTempSeed
    {
        public static IList<AccessTemp> GetAccessTempsMockUp()
        {
            return new List<AccessTemp>()
        {
            new AccessTemp()
            {
                IdAccessTemp = 1,
                CodeClientBscs = "CODE001",
                NumLigneBscs = 1,
                NumGsm = "0661123456"
            },
            new AccessTemp()
            {
                IdAccessTemp = 2,
                CodeClientBscs = "CODE002",
                NumLigneBscs = 2,
                NumGsm = "0661123456"
            },
            new AccessTemp()
            {
                IdAccessTemp = 3,
                CodeClientBscs = "CODE003",
                NumLigneBscs = 3,
                NumGsm = "0661123456"
            },
            new AccessTemp()
            {
                IdAccessTemp = 4,
                CodeClientBscs = "CODE004",
                NumLigneBscs = 4,
                NumGsm = "0661123456"
            },
            new AccessTemp()
            {
                IdAccessTemp = 5,
                CodeClientBscs = "CODE005",
                NumLigneBscs = 5,
                NumGsm = "0661123456"
            },
            new AccessTemp()
            {
                IdAccessTemp = 6,
                CodeClientBscs = "CODE006",
                NumLigneBscs = 6,
                NumGsm = "0661123456"
            }
        };
        }
    }
}
