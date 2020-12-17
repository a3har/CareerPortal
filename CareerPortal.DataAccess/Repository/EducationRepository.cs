using CareerPortal.DataAccess.Data;
using CareerPortal.DataAccess.Repository.IRepository;
using CareerPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CareerPortal.DataAccess.Repository
{
    public class EducationRepository : Repository<Education>, IEducationRepository
    {
        private readonly ApplicationDbContext _db;
        public EducationRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Education user)
        {
            var ObjFromDb = _db.Educations.FirstOrDefault(s => s.Id == user.Id);
            if (ObjFromDb != null)
            {
                ObjFromDb.Institution = user.Institution;
                ObjFromDb.Degree = user.Degree;
                ObjFromDb.Score = user.Score;
                ObjFromDb.EnrollYear = user.EnrollYear;
                ObjFromDb.PassYear = user.PassYear;

            }
        }
    }
}
