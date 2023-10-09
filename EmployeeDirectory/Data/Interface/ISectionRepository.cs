using EmployeeDirectory.Data.Models;
using System;
using System.Collections.Generic;

namespace EmployeeDirectory.Data.Interface
{
    public interface ISectionRepository : IDisposable
    {
        IEnumerable<Section> GetAll();
        Section GetById(int id);
    }
}
