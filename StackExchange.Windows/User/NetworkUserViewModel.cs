using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using StackExchange.Windows.Api.Models;

namespace StackExchange.Windows.User
{
    public class NetworkUserViewModel
    {
        public NetworkUserViewModel(NetworkUser account)
        {
            Reputation = account.Reputation;
            SiteName = account.SiteName;
        }

        public int Reputation { get; }
        public string SiteName { get; }
    }
}
