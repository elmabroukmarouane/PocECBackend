using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Poc.Ecare.Infrastructure.Models.Classes
{
    [Table("T_USERS")]
    public class User : Entity
    {
        [Key]
        [Column("user_id")]
        [Required]
        public int UserId { get; set; }
        [Column("first_name")]
        public string FirstName { get; set; }
        [Column("last_name")]
        public string LastName { get; set; }
        [Column("email")]
        [Required]
        public string Email { get; set; }
        [Column("password")]
        [Required]
        public string Password { get; set; }
        [Column("is_connected")]
        public int IsConnected { get; set; }
    }
}
