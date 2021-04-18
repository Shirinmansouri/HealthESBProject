using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.IService
{
    public interface ICacheService
    {
        bool TryGet<T, TItem>(string key,  TItem item,out T value) ;
        T Set<T>(T item, string key);
        void Remove<T>(string key,T value);

    }
}
