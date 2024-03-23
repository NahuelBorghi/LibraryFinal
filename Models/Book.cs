using System;
using System.Collections.Generic;

namespace LibraryFinal.Models;

public partial class Book
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string Author { get; set; } = null!;

    public virtual ICollection<Ownership> Ownerships { get; set; } = new List<Ownership>();
}
