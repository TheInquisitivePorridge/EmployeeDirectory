using EmployeeDirectory.Data.Interface;
using EmployeeDirectory.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeDirectory.Data.Repository
{
    public class SectionRepository : ISectionRepository
    {
        private readonly EmpDirectoryContext _context;
        public SectionRepository(EmpDirectoryContext _context) => this._context = _context;
        public IEnumerable<Section> GetAll() => _context.Section;
        public Section GetById(int id) => _context.Section.First(x => x.SectionId == id);
        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
