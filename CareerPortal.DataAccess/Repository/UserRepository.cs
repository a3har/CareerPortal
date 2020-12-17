using CareerPortal.DataAccess.Data;
using CareerPortal.DataAccess.Repository.IRepository;
using CareerPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CareerPortal.DataAccess.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _db;
        public UserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(User user)
        {
            var ObjFromDb = _db.Users.FirstOrDefault(s => s.Id == user.Id);
            if (ObjFromDb != null)
            {
                ObjFromDb.Name = user.Name;
                ObjFromDb.Address = user.Address;
                ObjFromDb.DateOfBirth = user.DateOfBirth;
                ObjFromDb.PhoneNumber = user.PhoneNumber;
            }
        }
    }
}
