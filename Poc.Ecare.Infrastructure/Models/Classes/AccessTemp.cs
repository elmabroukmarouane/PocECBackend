using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Poc.Ecare.Infrastructure.Models.Classes
{
    [Table("ECA_ACCES_TEMP")]
    public class AccessTemp : Entity
    {
        [Key]
        [Column("ID_ACCES_TEMP")]
        [Required]
        public int IdAccessTemp { get; set; }
        [Column("CODE_CLIENT_BSCS")]
        [Required]
        public string CodeClientBscs { get; set; }
        [Column("NUM_LIGNE_BSCS")]
        [Required]
        public int NumLigneBscs { get; set; }
        [Column("NUM_GSM")]
        [Required]
        public string NumGsm { get; set;}
        [Column("TYPE_CLIENT")]
        public int Typeclient { get; set; }
        [Column("DATE_CREATION")]
        public DateTime? DateCreation { get; set; }
        [Column("DATE_MAJ")]
        public DateTime? DateMaj { get; set; }
        [Column("CODE_SECRET")]
        public byte[] CodeSecret { get; set; }
        [Column("DEJA_CONNECTE")]
        public int DejaConnecte { get; set; }
        [Column("DATE_VALIDITE_CF")]
        public DateTime DateValiditeCf { get; set; }
        [Column("TYPE_DEMANDE_CF")]
        public int TypeDemandeCf { get; set; }
        [Column("EST_SUPPRIME")]
        public int EstSupprime { get; set; }
        [Column("ID_APPLICATIF")]
        public int IdApplicatif { get; set; }
        [Column("CODE_VALIDATION")]
        public string CodeValidation { get; set; }
        [Column("TOKEN_URL_COURTE")]
        public string TokenUrlCourte { get; set; }
    }
}
