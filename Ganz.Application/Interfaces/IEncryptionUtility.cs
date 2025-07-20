using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ganz.Application.Interfaces
{
    public interface IEncryptionUtility
    {
        string GetSHA256(string password, string salt);
        string GetNewSalt();
        string GetNewRefreshToken();
        string GetNewToken(Guid userId);
    }
}
