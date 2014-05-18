using PManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PManager.Domain.Concrete
{
    public class UnitOfWork : IDisposable
    {
        /*
         *  The DbContext class we shall be using
         */
        private EFDbContext _context = new EFDbContext();
        private bool disposed = false;

        /*
         *  The Reposotories to use will all be saved here
         */

        private GenericRepository<Project> _projectRepository;
        //private GenericRepository<ProjectRole> _projectRoleRepository;
        private GenericRepository<ProjectTask> _projectTaskRepository;
        //private GenericRepository<User> _userRepository;


        public GenericRepository<Project> ProjectRepository
        {
            get
            {
                if (_projectRepository == null)
                {
                    _projectRepository = new GenericRepository<Project>(_context);
                }
                return _projectRepository;
            }
        }
        //public GenericRepository<ProjectRole> ProjectRoleRepository
        //{
        //    get
        //    {
        //        if (_projectRoleRepository == null)
        //        {
        //            _projectRoleRepository = new GenericRepository<ProjectRole>(_context);
        //        }
        //        return _projectRoleRepository;
        //    }
        //}
        public GenericRepository<ProjectTask> ProjectTaskRepository
        {
            get
            {
                if (_projectTaskRepository == null)
                {
                    _projectTaskRepository = new GenericRepository<ProjectTask>(_context);
                }
                return _projectTaskRepository;
            }
        }
        //public GenericRepository<User> UserRepository
        //{
        //    get
        //    {
        //        if (_userRepository == null)
        //        {
        //            _userRepository = new GenericRepository<User>(_context);

        //        }
        //        return _userRepository;
        //    }
        //}

        public void Save()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
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
