using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.IService
{
    public interface IBaseCrudService<TEntity>
    {
        void CommitAsync();
        void Commit();
    }
}
