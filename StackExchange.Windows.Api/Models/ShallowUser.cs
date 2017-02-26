using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace StackExchange.Windows.Api.Models
{
    public class ShallowUser
    {
        public string DisplayName { get; set; }
        
        public string Link { get; set; }
        
        public int UserId { get; set; }
        
        public int? Reputation { get; set; }
    }
}
