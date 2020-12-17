using CareerPortal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerPortal.DataAccess.Repository.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        void Update(User user);
    }
}
