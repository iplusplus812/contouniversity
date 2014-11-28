using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContosoUniversity.Models;

namespace ContosoUniversity.DAL
{
    public class UnitOfWork:IDisposable
    {
        private SchoolContext context = new SchoolContext();
        private ISchoolRepository<Department> departmentRepository;
        private ISchoolRepository<Course> courseRepository;

        public ISchoolRepository<Department> DepartmentRepository
        {
            get
            {
                if (this.departmentRepository == null)
                {
                    this.departmentRepository = new SchoolRepository<Department>(this.context);
                }
                return this.departmentRepository;
            }
        } 

        public ISchoolRepository<Course> CourseRepository
        {
            get
            {
                if (this.courseRepository == null)
                {
                    this.courseRepository = new SchoolRepository<Course>(this.context);
                }
                return this.courseRepository;
            }
        }

        public int ExecuteSqlCommand(String sql, params System.Object[] parameters)
        {
            return this.context.Database.ExecuteSqlCommand(sql, parameters);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
