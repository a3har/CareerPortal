using CareerPortal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerPortal.DataAccess.Repository.IRepository
{
    public interface IEducationRepository : IRepository<Education>
    {
        void Update(Education education);
    }
}
