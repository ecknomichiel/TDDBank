using System;
using System.Collections.Generic;
using TDDBanking.Models;

namespace TDDBanking.DataAccess
{
    public interface IBankData
    {
        ICollection<Account> GetAllAccounts();
    }
}
