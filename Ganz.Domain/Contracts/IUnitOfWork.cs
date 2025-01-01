using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ganz.Domain.Contracts
{
    public interface IUnitOfWork: IDisposable
    {
        Task<int> SaveChangesAsync();
        void Rollback();
    }
}
