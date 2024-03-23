using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryFinal.Models;

public partial class User
{
    public Guid Id { get; set; }

    [Display(Name = "Nombre de usuario")]
    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;
    [Display(Name = "Rol")]
    public string Role { get; set; } = null!;

    public string Token { get; set; } = null!;

    public virtual ICollection<Ownership> Ownerships { get; set; } = new List<Ownership>();
}
