using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PhotoRater.Models;

// Add profile data for application users by adding properties to the User class
public class User : IdentityUser
{
    public const int KarmaQuant = 5;
    public const int MaxKarma = 100;
    public const int MinKarma = 0;
    
    public int Karma { get; set; } = 0;
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public List<PhotoOnRate> PhotosOnRate { get; set; } = [];
}

