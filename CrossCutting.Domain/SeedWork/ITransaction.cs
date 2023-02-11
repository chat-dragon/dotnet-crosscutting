using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Domain.SeedWork
{
    public interface ITransaction : IDisposable
    {
        Task Commit(CancellationToken cancellation = default);
    }
}
