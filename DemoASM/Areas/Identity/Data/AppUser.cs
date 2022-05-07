using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DemoASM.Areas.Identity.Data;

// Add profile data for application users by adding properties to the AppUser class
public class AppUser : IdentityUser
{
    public string? FullName { get; set; }
    public string? Address { get; set; }
    public DateTime? DoB { get; set; }
    public string? Gender { get; set;}
}