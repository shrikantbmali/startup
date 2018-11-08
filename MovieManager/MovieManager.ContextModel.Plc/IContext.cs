using System.Collections.Generic;

namespace MovieManager.ContextModel
{
    public interface IContext<TContextType, in TIdType>
    {
        TContextType Save(TContextType context);

        void Update(TContextType context);

        void Delete(TContextType context);

        void DeleteAllOf(TIdType foreignKey);

        IEnumerable<TContextType> GetAll();

        IEnumerable<TContextType> GetAllOf(TIdType foreignKey);

        TContextType GetSpecific(TIdType id);
    }
}