using System;
using System.Collections.Generic;

namespace MovieManager.ContextModel
{
	public abstract class Context<TContextType, TIdType> : IContext<TContextType, TIdType>
	{
		public virtual TContextType Save(TContextType context)
		{
			throw new NotImplementedException();
		}

		public virtual void Update(TContextType context)
		{
			throw new NotImplementedException();
		}

		public virtual void Delete(TContextType context)
		{
			throw new NotImplementedException();
		}

		public void DeleteAllOf(TIdType foreignKey)
		{
			throw new NotImplementedException();
		}

		public virtual IEnumerable<TContextType> GetAll()
		{
			throw new NotImplementedException();
		}

		public virtual IEnumerable<TContextType> GetAllOf(TIdType foreignKey)
		{
			throw new NotImplementedException();
		}

		public virtual TContextType GetSpecific(TIdType id)
		{
			throw new NotImplementedException();
		}
	}
}