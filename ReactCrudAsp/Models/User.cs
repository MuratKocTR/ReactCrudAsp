using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ReactCrudAsp.Models
{
    public class User
    {
        [Key]
        public int id { get; set; }

        
        public string email { get; set; }

        public string password { get; set; }
        public string name { get; set; } =string.Empty;
        public string surname { get; set; } = string.Empty;

        public int phonenumber { get; set; }


    }
}
