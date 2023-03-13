using Poc.Ecare.Infrastructure.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poc.Ecare.Server.DtoModel.Models
{
    public class AccessTempViewModel : Entity
    {
        public int IdAccessTemp { get; set; }
        public string CodeClientBscs { get; set; }
        public int NumLigneBscs { get; set; }
        public string NumGsm { get; set; }
        public int Typeclient { get; set; }
        public DateTime? DateCreation { get; set; }
        public DateTime? DateMaj { get; set; }
        public int DejaConnecte { get; set; }
        public DateTime DateValiditeCf { get; set; }
        public int TypeDemandeCf { get; set; }
        public int EstSupprime { get; set; }
        public int IdApplicatif { get; set; }
    }
}
