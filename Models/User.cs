using System;
using System.Collections.Generic;

namespace LibraryFinal.Models;

public partial class User
{
    public Guid Id { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string Token { get; set; } = null!;

    public virtual ICollection<Ownership> Ownerships { get; set; } = new List<Ownership>();
}
