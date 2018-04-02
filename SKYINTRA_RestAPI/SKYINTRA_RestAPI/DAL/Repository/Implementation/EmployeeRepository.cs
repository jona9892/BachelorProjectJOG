using DomainModel;
using MySql.Data.MySqlClient;
using SKYINTRA_RestAPI.DAL.Context;
using SKYINTRA_RestAPI.DAL.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKYINTRA_RestAPI.DAL.Repository.Implementation
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private IPWMetazoContext ctx;

        public EmployeeRepository(IPWMetazoContext context)
        {
            ctx = context;
        }


        public Employee GetEmployee(string name)
        {
            MySqlConnection conn = ctx.GetConnection();
            Employee employee = new Employee();

            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT e.name, e.birthday, e.email, e.sex, e.jobtitle " +
                                                    "FROM employee e " + 
                                                    "INNER JOIN user u ON e.userid = u.objectid " +
                                                    "WHERE u.name = @Name", conn);
            cmd.Parameters.AddWithValue("@Name", name);

            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    employee.name = reader["name"].ToString();
                    string birthday = reader["birthday"].ToString();
                    employee.birthday = Convert.ToDateTime(birthday);
                    employee.email = reader["email"].ToString();
                    employee.sex = reader["sex"].ToString();
                    employee.jobtitle = reader["jobtitle"].ToString();
                }
            }
            conn.Close();

            return employee;
        }

        public IEnumerable<Employee> ReadBirthdayWeek()
        {
            List<Employee> list = new List<Employee>();

            using (MySqlConnection conn = ctx.GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT name, birthday, jobtitle FROM employee " + 
                                                     "WHERE DATE_ADD(birthday, INTERVAL YEAR(CURDATE()) - YEAR(birthday) " +
                                                     "+ IF(DAYOFYEAR(CURDATE()) > DAYOFYEAR(birthday), 1, 0) YEAR) " +
                                                       "BETWEEN CURDATE() AND DATE_ADD(CURDATE(), INTERVAL 7 DAY) ", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Employee()
                        {
                            name = reader["name"].ToString(),
                            birthday = Convert.ToDateTime(reader["birthday"].ToString()),
                            jobtitle = reader["jobtitle"].ToString()
                        });
                    }
                }
            }
            return list;
        }

        public IEnumerable<Employee> ReadEmployementWeek()
        {
            List<Employee> list = new List<Employee>();

            using (MySqlConnection conn = ctx.GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT name, employment, jobtitle FROM employee " +
                                                     "WHERE DATE_ADD(employment, INTERVAL YEAR(CURDATE()) - YEAR(employment) " +
                                                     "+ IF(DAYOFYEAR(CURDATE()) > DAYOFYEAR(employment), 1, 0) YEAR) " +
                                                       "BETWEEN CURDATE() AND DATE_ADD(CURDATE(), INTERVAL 7 DAY) ", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Employee()
                        {
                            name = reader["name"].ToString(),
                            employment = Convert.ToDateTime(reader["employment"].ToString()),
                            jobtitle = reader["jobtitle"].ToString()
                        });
                    }
                }
            }
            return list;
        }
    }
}
