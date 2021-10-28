using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using UtilizandoADOProjetoMVC.Models;

namespace UtilizandoADOProjetoMVC.Repository
{
    public class EmployeeRepository : IRepository<Employee>
    {
        private readonly SqlConnection connection;

        public EmployeeRepository(IConfiguration config)
        {
            string cs = config.GetConnectionString("Default");
            this.connection = new SqlConnection(cs);
        }

        public void Delete(Employee model)
        {
            using (connection)
            {
                var cmd = new SqlCommand("spDeleteEmployee", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                connection.Open();
                cmd.Parameters.AddWithValue("@Id", model.Id);
                cmd.ExecuteNonQuery();
            }
        }

        public void Inserir(Employee model)
        {
            using (connection)
            {
                var cmd = new SqlCommand("spAddNew", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                connection.Open();
                cmd.Parameters.AddWithValue("@Name", model.Name);
                cmd.Parameters.AddWithValue("@Office", model.Office);
                cmd.Parameters.AddWithValue("Age", model.Age);
                cmd.Parameters.AddWithValue("@Position", model.Position);
                cmd.Parameters.AddWithValue("@Salary", model.Salary);
                cmd.ExecuteNonQuery();
            }
        }

        public Employee RetornarPorId(int? Id)
        {
            var employee = new Employee();

            using (connection)
            {
                var cmd = new SqlCommand("spGetEmployee", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", Id);
                connection.Open();

                SqlDataReader sdr = cmd.ExecuteReader();

                if (!sdr.HasRows)
                {
                    return null;
                }

                while (sdr.Read())
                {
                    employee.Id = Convert.ToInt32(sdr["Id"]);
                    employee.Name = Convert.ToString(sdr["Name"]);
                    employee.Age = Convert.ToInt32(sdr["Age"]);
                    employee.Office = Convert.ToString(sdr["Office"]);
                    employee.Position = Convert.ToString(sdr["Position"]);
                    employee.Salary = Convert.ToDecimal(sdr["Salary"]);
                }
            }

            return employee;
        }

        public IEnumerable<Employee> RetornarTodos()
        {
            var employees = new List<Employee>();

            using (connection)
            {
                var cmd = new SqlCommand("spGetEmployee", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                connection.Open();

                var sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    var employee = new Employee()
                    {
                        Id = Convert.ToInt32(sdr["Id"]),
                        Name = Convert.ToString(sdr["Name"]),
                        Age = Convert.ToInt32(sdr["Age"]),
                        Office = Convert.ToString(sdr["Office"]),
                        Position = Convert.ToString(sdr["Position"]),
                        Salary = Convert.ToDecimal(sdr["Salary"])
                    };

                    employees.Add(employee);
                }
                
            }

            return employees;
        }

        public void Update(Employee model)
        {
            using (connection)
            {
                var cmd = new SqlCommand("spUdateEmployee", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", model.Id);
                cmd.Parameters.AddWithValue("@Name", model.Name);
                cmd.Parameters.AddWithValue("@Position", model.Position);
                cmd.Parameters.AddWithValue("@Age", model.Age);
                cmd.Parameters.AddWithValue("@Salary", model.Salary);
                cmd.Parameters.AddWithValue("@Office", model.Office);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
