using EmployeeDirectory.Data.Models;
using System;
using System.Collections.Generic;

namespace EmployeeDirectory.Data.Interface
{
    public interface IEmployeeRepository : IDisposable
    {
        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployee(int id);
        void UpdateEmployee(Employee employee);
        void CreateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
    }
}
