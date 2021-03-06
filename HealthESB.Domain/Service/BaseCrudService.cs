using HealthESB.Domain.IRepository;
using HealthESB.Domain.IService;
using System;


namespace HealthESB.Domain.Service
{

    public class BaseCrudService<TEntity> : IBaseCrudService<TEntity> where TEntity : class
    {
        private readonly IGenericRepository<TEntity> _repository;
        public BaseCrudService(IGenericRepository<TEntity> repository)
        {
            _repository = repository;
        }
        public void Commit()
        {
            _repository.Commit();
        }
        public void CommitAsync()
        {
            try
            {
                _repository.CommitAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}