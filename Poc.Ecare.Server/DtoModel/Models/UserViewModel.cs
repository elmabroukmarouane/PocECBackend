using Poc.Ecare.Infrastructure.Models.Classes;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Poc.Ecare.Server.DtoModel.Models
{
    public class UserViewModel : Entity
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserSessionId { get; set; }
        public string Token { get; set; }
    }
}
