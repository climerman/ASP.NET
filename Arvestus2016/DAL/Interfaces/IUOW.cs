using DAL.Interfaces.Contacts;
using Domain.Contacts;

namespace DAL.Interfaces
{
    public interface IUOW
    {
        //save pending changes to the data store
        void Commit();
        void RefreshAllEntities();

        //UOW Methods, that dont fit into specific repo

        //get repository for type
        T GetRepository<T>() where T : class;

        // standard autocreated repos, since we do not have any special methods in interfaces
        IEFRepository<ContactType> ContactTypes { get; }


        // Custom repos
        IContactRepository Contacts { get; }
        IPersonRepository Persons { get; }
    }
}