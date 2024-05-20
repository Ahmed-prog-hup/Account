using System;
using System.Collections.Generic;

namespace Account.Models;

public partial class Account
{
    public string AccNumber { get; set; } = null!;

    public string? AccParent { get; set; }

    public decimal? Balance { get; set; }

    public virtual Account? AccParentNavigation { get; set; }

    public virtual ICollection<Account> InverseAccParentNavigation { get; set; } = new List<Account>();
}
