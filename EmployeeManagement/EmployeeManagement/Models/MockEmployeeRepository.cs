using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {

        private List<Employee> _employeeList;

        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>()
            {
                new Employee(){ Id = 1, Name="Mary", Department=Dept.HR, Email= "mary@test.com.br"},
                new Employee(){ Id = 2, Name="John", Department=Dept.IT, Email= "john@test.com.br"},
                new Employee(){ Id = 3, Name="Sam", Department=Dept.IT, Email= "sam@test.com.br"},
            };
        }

        public Employee Add(Employee employee)
        {
            employee.Id = _employeeList.Max(e => e.Id) + 1;
            _employeeList.Add(employee);

           return employee;
        }

        public Employee Delete(int id)
        {
            Employee e = GetEmployee(id);

            if (e != null)
                _employeeList.Remove(e);

            return e;

        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _employeeList;
        }

        public Employee GetEmployee(int id)
        {
            return _employeeList.FirstOrDefault(e=> e.Id == id);
        }

        public Employee Update(Employee changeEmployee)
        {
            Employee e = GetEmployee(changeEmployee.Id);

            if (e != null) { 
                _employeeList.Remove(e);
                _employeeList.Add(e);
            }

            return e;
        }
    }
}
