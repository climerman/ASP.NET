using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Interfaces.Contacts;
using DAL.Repositories;
using DAL.Repositories.Contacts;
//using DAL.Interfaces.Contacts;
//using DAL.Interfaces.Identity;
//using DAL.Interfaces.Orders;
//using DAL.Interfaces.Storage;
//using DAL.Repositories;
//using DAL.Repositories.Contacts;
//using DAL.Repositories.Identity;
//using DAL.Repositories.Orders;
//using DAL.Repositories.Storage;
using NLog;

namespace DAL.Helpers
{
    public class EFRepositoryFactories : IDisposable
    {
        private readonly NLog.ILogger _logger;
        private readonly string _instanceId = Guid.NewGuid().ToString();

        private readonly IDictionary<Type, Func<IDbContext, object>> _repositoryFactories;

        public EFRepositoryFactories(ILogger logger)
        {
            _logger = logger;
            _logger.Debug("InstanceId: " + _instanceId);

            _repositoryFactories = GetCustomFactories();
        }

        //this ctor is for testing only, you can give here an arbitrary list of repos
        public EFRepositoryFactories(IDictionary<Type, Func<IDbContext, object>> factories, ILogger logger)
        {
            _logger = logger;
            _repositoryFactories = factories;

            _logger.Debug("InstanceId: " + _instanceId);
        }

        //special repos with custom interfaces are registered here
        private static IDictionary<Type, Func<IDbContext, object>> GetCustomFactories()
        {
            return new Dictionary<Type, Func<IDbContext, object>>
            {
                {typeof (IPersonRepository), dbContext => new PersonRepository(dbContext)},
                {typeof (IContactRepository), dbContext => new ContactRepository(dbContext)}
                //{typeof (IOrderRepository), dbContext => new OrderRepository(dbContext)},
                //{typeof (IProductRepository), dbContext => new ProductRepository(dbContext)},
                //{typeof (IStoredProductRepository), dbContext => new StoredProductRepository(dbContext)},
                //{typeof (IOrderedProductRepository), dbContext => new OrderedProductRepository(dbContext)},
                //{typeof (IUserRepository), dbContext => new UserRepository(dbContext)},
                //{typeof (IUserRoleRepository), dbContext => new UserRoleRepository(dbContext)},
                //{typeof (IUserClaimRepository), dbContext => new UserClaimRepository(dbContext)},
                //{typeof (IUserLoginRepository), dbContext => new UserLoginRepository(dbContext)},
                //{typeof (IRoleRepository), dbContext => new RoleRepository(dbContext)},
                //{typeof (IPersonTypeRepository), dbContext => new PersonTypeRepository(dbContext)},
                //{typeof (IContactTypeRepository), dbContext => new ContactTypeRepository(dbContext)},
                //{typeof (IOrderTypeRepository), dbContext => new OrderTypeRepository(dbContext)},
                //{typeof (IProductCategoryRepository), dbContext => new ProductCategoryRepository(dbContext)},
            };
        }

        public Func<IDbContext, object> GetRepositoryFactory<T>()
        {
            Func<IDbContext, object> factory;
            _repositoryFactories.TryGetValue(typeof(T), out factory);
            return factory;
        }

        public Func<IDbContext, object> GetRepositoryFactoryForEntityType<T>() where T : class
        {
            // if we already have this repository in list, return it
            // if not, create new instance of EFRepository
            return GetRepositoryFactory<T>() ?? DefaultEntityRepositoryFactory<T>();
        }

        protected virtual Func<IDbContext, object> DefaultEntityRepositoryFactory<T>() where T : class
        {
            // create new instance of EFRepository<T>
            return dbContext => new EFRepository<T>(dbContext);
        }

        public void Dispose()
        {
            _logger.Debug("InstanceId: " + _instanceId);
        }
    }
}