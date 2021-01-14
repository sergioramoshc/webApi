using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Security
{
    public class RefreshTokenModel
    {
        public string RefreshToken { get; set; }
        public int UserID { get; set; }
    }
}
