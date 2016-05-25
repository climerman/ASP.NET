using System;
using System.Data.Entity;
using DAL.Interfaces;
using DAL.Interfaces.Contacts;
using Domain.Contacts;
using NLog;

namespace DAL
{
    public class UOW : IUOW, IDisposable
    {
        private readonly ILogger _logger;
        private readonly string _instanceId = Guid.NewGuid().ToString();

        private IDbContext DbContext { get; set; }
        protected IEFRepositoryProvider RepositoryProvider { get; set; }

        public UOW(IEFRepositoryProvider repositoryProvider, IDbContext dbContext, ILogger logger)
        {
            _logger = logger;
            _logger.Debug("InstanceId: " + _instanceId);

            DbContext = dbContext;
            repositoryProvider.DbContext = dbContext;
            RepositoryProvider = repositoryProvider;
        }


        #region Save & Refresh

        public void Commit()
        {
            ((DbContext)DbContext).SaveChanges();
        }

        public void RefreshAllEntities()
        {
            foreach (var entity in ((DbContext)DbContext).ChangeTracker.Entries())
            {
                entity.Reload();
            }
        }

        #endregion


        #region Get Repositories

        // calling standard EF repo provider
        private IEFRepository<T> GetStandardRepo<T>() where T : class
        {
            return RepositoryProvider.GetRepositoryForEntityType<T>();
        }

        // calling custom repo provier
        private T GetRepo<T>() where T : class
        {
            return RepositoryProvider.GetRepository<T>();
        }

        public T GetRepository<T>() where T : class
        {
            var res = GetRepo<T>() ?? GetStandardRepo<T>() as T;
            if (res == null)
            {
                throw new NotImplementedException("No repository for type, " + typeof(T).FullName);
            }
            return res;
        }

        #endregion

        #region Standard Repositories

        public IEFRepository<ContactType> ContactTypes => GetStandardRepo<ContactType>();

        #endregion

        #region Custom repositories

        public IContactRepository Contacts => GetRepo<IContactRepository>();
        public IPersonRepository Persons => GetRepo<IPersonRepository>();

        #endregion


        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _logger.Debug("InstanceId: " + _instanceId + " Disposing:" + disposing);
        }

        #endregion
    }
}