using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRentaCarSln.DataAccess.Abstractions.Base;
using TestRentaCarSln.DataAccess.Context;

namespace TestRentaCarSln.DataAccess.Implementations.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;
        private readonly Dictionary<Type, IRepositoryBase> _repositories;

        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _repositories = new Dictionary<Type, IRepositoryBase>();
        }

        public void Dispose()
        {
            _appDbContext.Dispose();
        }

        public async Task<int> SaveAsync()
        {
            return await _appDbContext.SaveChangesAsync();
        }

        public TRepository GetRepository<TRepository>()
            where TRepository : class, IRepositoryBase

        {
            Type repositoryImplementationType = GetConcreteRepositoryInfo<TRepository>();

            if (_repositories.TryGetValue(repositoryImplementationType, out IRepositoryBase existingRepository))
            {
                return (TRepository)existingRepository;
            }

            IRepositoryBase? repositoryInstance = (IRepositoryBase)Activator.CreateInstance(repositoryImplementationType, _appDbContext);

            if (repositoryInstance is TRepository repository)
            {
                _repositories.Add(repositoryImplementationType, repository);
                return repository;
            }

            throw new InvalidOperationException($"Could not create repository of type {repositoryImplementationType}");


        }

        private Type GetConcreteRepositoryInfo<TRepository>() where TRepository : class, IRepositoryBase
        {
            string interfaceName = typeof(TRepository).Name;
            string className = interfaceName.StartsWith("I") ? interfaceName.Substring(1) : interfaceName;


            string interfaceNamespace = typeof(TRepository).Namespace;
            string implementationFullName = $"{interfaceNamespace}.{className}";

            Type repositoryType = Type.GetType(implementationFullName);

            if (repositoryType == null)
            {
                repositoryType = AppDomain.CurrentDomain.GetAssemblies()
                                                        .SelectMany(a => a.GetTypes())
                                                        .FirstOrDefault(t => t.Name == className && typeof(TRepository).IsAssignableFrom(t));


            }

            if (repositoryType == null)
            {
                throw new InvalidOperationException();
            }

            return repositoryType;

        }
    }
}

   

