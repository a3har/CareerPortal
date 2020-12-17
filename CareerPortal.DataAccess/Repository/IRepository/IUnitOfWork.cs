using System;
using System.Collections.Generic;
using System.Text;

namespace CareerPortal.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository User { get; }
        public void Save();
    }
}
