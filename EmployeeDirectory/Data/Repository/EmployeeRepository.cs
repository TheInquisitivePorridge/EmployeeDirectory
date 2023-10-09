using EmployeeDirectory.Data.Interface;
using EmployeeDirectory.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeDirectory.Data.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmpDirectoryContext _context;
        public EmployeeRepository(EmpDirectoryContext _context) => this._context = _context;
        public IEnumerable<Employee> GetAllEmployees() => _context.Employee.Include(x => x.Section);
        public Employee GetEmployee(int id) => _context.Employee.Include(x => x.Section).FirstOrDefault(x => x.Id == id);
        public void UpdateEmployee(Employee employee)
        {
            _context.Attach(employee);
            _context.Entry(employee).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public void DeleteEmployee(Employee employee)
        {
            _context.Attach(employee);
            _context.Entry(employee).State = EntityState.Deleted;
            _context.SaveChanges();
        }
        public void CreateEmployee(Employee employee)
        {
            _context.Attach(employee);
            _context.Entry(employee).State = EntityState.Added;
            _context.SaveChanges();
        }

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