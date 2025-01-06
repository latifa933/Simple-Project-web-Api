using System;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Data.SqlClient;
namespace StudentApiDataAccessLayer
{
    public class StudentDTO
    {
        public StudentDTO(int id, string name, int age, int grade)
        {
            this.Id = id;
            this.Name = name;
            this.Age = age;
            this.Grade = grade;

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Grade { get; set; }


    }
    public class StudentData
    {
        static string _connectionString = "Server=localhost;Database=StudentsDB;Integrated Security=True;User Id=sa;Password=sa123456;Encrypt=False;TrustServerCertificate=True";
        public static List<StudentDTO> GetAllStudents()
        {
            var studentList = new List<StudentDTO>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetAllStudents", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            studentList.Add(new StudentDTO
                                (reader.GetInt32(reader.GetOrdinal("Id")),
                                reader.GetString(reader.GetOrdinal("Name")),
                                reader.GetInt32(reader.GetOrdinal("Age")),
                                reader.GetInt32(reader.GetOrdinal("Grade"))

                                ));
                        }
                    }

                }
                return studentList;
            }

        }

        public static List<StudentDTO> GetPassedStudents()
        {
            var passedStudents = new List<StudentDTO>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetPassedStudents", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            passedStudents.Add(new StudentDTO
                               (reader.GetInt32(reader.GetOrdinal("Id")),
                               reader.GetString(reader.GetOrdinal("Name")),
                               reader.GetInt32(reader.GetOrdinal("Age")),
                               reader.GetInt32(reader.GetOrdinal("Grade"))
                               ));
                        }
                    }
                }

                return passedStudents;
            }

        }

        public static double GetAverageGrade()
        {
            double averageGrade;
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                using(SqlCommand cmd = new SqlCommand("SP_GetAverageGrade", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        averageGrade = Convert.ToDouble(result);
                    }
                    else
                        averageGrade = 0;

                }

            }
            return averageGrade;

        }

        public static StudentDTO GetStudentByID(int studentID)
        {  using(var connexion = new SqlConnection(_connectionString))
            { using(var cmd = new SqlCommand("SP_GetStudentById",connexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StudentId", studentID);
                    connexion.Open();
                    using(var reader = cmd.ExecuteReader())
                    { if (reader.Read())
                        {
                            return new StudentDTO(
                                 reader.GetInt32(reader.GetOrdinal("Id")),
                                 reader.GetString(reader.GetOrdinal("Name")),
                                 reader.GetInt32(reader.GetOrdinal("Age")),
                                 reader.GetInt32(reader.GetOrdinal("Grade"))

                                );
                        }
                        else
                            return null;




                    }

                }



            }

        }

        public static int AddStudent(StudentDTO StudentDTO)
        {
            using (var connexion = new SqlConnection(_connectionString))
            { using(var command = new SqlCommand("SP_AddStudent",connexion))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name",StudentDTO.Name);
                    command.Parameters.AddWithValue("@Age",StudentDTO.Age);
                    command.Parameters.AddWithValue("@Grade",StudentDTO.Grade);
                    var outputIDParameter = new SqlParameter("@NewStudentId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputIDParameter);
                    connexion.Open();
                    command.ExecuteNonQuery();
                    return (int)outputIDParameter.Value;

                }

            }

        }

        public static bool UpdateStudent(StudentDTO StudentDTO)
        { using (SqlConnection connexion = new SqlConnection(_connectionString))
            { using (SqlCommand command = new SqlCommand("SP_UpdateStudent", connexion))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@StudentId", StudentDTO.Id);
                    command.Parameters.AddWithValue("@Name", StudentDTO.Name);
                    command.Parameters.AddWithValue("@Age", StudentDTO.Age);
                    command.Parameters.AddWithValue("@Grade", StudentDTO.Grade);
                    connexion.Open();
                    command.ExecuteNonQuery();
                    return true;

                }
            }
        }
        public static bool DeleteStudent(int studentID)
        { using (SqlConnection connexion = new SqlConnection(_connectionString))
            { using(SqlCommand command = new SqlCommand("SP_DeleteStudent", connexion))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@StudentId", studentID);
                    connexion.Open();
                    int rowsAffect = (int)command.ExecuteScalar();
                    return rowsAffect == 1;




                }







            }




                }







    }
}
