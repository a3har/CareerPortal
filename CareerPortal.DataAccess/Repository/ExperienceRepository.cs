using CareerPortal.DataAccess.Data;
using CareerPortal.DataAccess.Repository.IRepository;
using CareerPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CareerPortal.DataAccess.Repository
{
    public class ExperienceRepository : Repository<Experience>, IExperienceRepository
    {
        private readonly ApplicationDbContext _db;
        public ExperienceRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Experience user)
        {
            var ObjFromDb = _db.Experiences.FirstOrDefault(s => s.Id == user.Id);
            if (ObjFromDb != null)
            {
                ObjFromDb.Company = user.Company;
                ObjFromDb.Position = user.Position;
                ObjFromDb.Description = user.Description;
                ObjFromDb.StartDate = user.StartDate;
                ObjFromDb.EndDate = user.EndDate;
            }
        }
    }
}
