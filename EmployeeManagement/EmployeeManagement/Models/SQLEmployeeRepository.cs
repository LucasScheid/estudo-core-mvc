using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {

        private readonly AppDBContext _context;
        private readonly ILogger<SQLEmployeeRepository> logger;

        public SQLEmployeeRepository(AppDBContext context, ILogger<SQLEmployeeRepository> logger)
        {
            _context = context;
            this.logger = logger;
        }

        public Employee Add(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();

            return employee;
        }

        public Employee Delete(int id)
        {
            Employee e = _context.Employees.Find(id);

            if (e != null)
            {
                _context.Employees.Remove(e);
                _context.SaveChanges();
            }

            return e;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _context.Employees;
        }

        public Employee GetEmployee(int id)
        {
            return _context.Employees.Find(id);
        }

        public Employee Update(Employee changeEmployee)
        {
            var employee = _context.Employees.Attach(changeEmployee);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return changeEmployee;
        }

    }
}

