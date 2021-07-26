using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Project.Data.Models
{
    using static DataConstants;

    public class User : IdentityUser
    {
        [MaxLength(NameMaxLength)]
        public string FullName { get; set; }
    }
}
