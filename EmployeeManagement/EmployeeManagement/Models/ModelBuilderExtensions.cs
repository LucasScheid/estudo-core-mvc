﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public static class ModelBuilderExtensions
    {

        public static void Seed(this ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>().HasData(

                 new Employee() { Id = 1, Name = "Mary", Department = Dept.HR, Email = "mary@test.com.br" },
                 new Employee() { Id = 2, Name = "John", Department = Dept.IT, Email = "john@test.com.br" },
                 new Employee() { Id = 3, Name = "Sam", Department = Dept.IT, Email = "sam@test.com.br" }

                );

        }

    }
}
