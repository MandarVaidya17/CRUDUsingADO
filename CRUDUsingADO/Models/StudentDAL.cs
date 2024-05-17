using System.Data.SqlClient;

namespace CRUDUsingADO.Models
{
    public class StudentDAL
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        private readonly IConfiguration configuration;

        public StudentDAL(IConfiguration configuration)
        {
            this.configuration = configuration;
            string connstr = this.configuration.GetConnectionString("DefaultConnection");
            con=new SqlConnection(connstr);
        }

        public List<Student> GetStudents()
        {
            List<Student> studentlist = new List<Student>();
            string qry = "select * from student";
            cmd=new SqlCommand(qry, con);
            con.Open();
            dr= cmd.ExecuteReader();
            if(dr.HasRows)
            {
                while(dr.Read())
                {
                    Student student = new Student();
                    student.Id = Convert.ToInt32(dr["id"]);
                    student.Name = dr["name"].ToString();
                    student.Age = Convert.ToInt32(dr["age"]);
                    student.Address = dr["address"].ToString();
                    studentlist.Add(student);   
                    
                }
            }
            con.Close();
            return studentlist;
        }

        public int AddStudent(Student stud)
        {
            int result = 0;
            string qry = "insert into student values(@id,@name,@age,@address)";
            cmd=new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", stud.Id);
            cmd.Parameters.AddWithValue("@name", stud.Name);
            cmd.Parameters.AddWithValue("@age", stud.Age);
            cmd.Parameters.AddWithValue("@address", stud.Address);
            con.Open();
            result=cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        public int EditStudent(Student stud)
        {
            int result = 0;
            string qry = "update student set name=@name,age=@age,address=@address where id=@id";
            cmd = new SqlCommand(qry, con);
            
            cmd.Parameters.AddWithValue("@name", stud.Name);
            cmd.Parameters.AddWithValue("@age", stud.Age);
            cmd.Parameters.AddWithValue("@address", stud.Address);
            cmd.Parameters.AddWithValue("@id", stud.Id);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        public int DeleteStudent(int id)
        {
            int result = 0;
            string qry = "delete from student where id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }

        public Student GetStudentByID(int id)
        {
            Student student = new Student();
            string qry = "select * from student where id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            dr= cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    student.Id = Convert.ToInt32(dr["id"]);
                    student.Name = dr["name"].ToString();
                    student.Age = Convert.ToInt32(dr["age"]);
                    student.Address = dr["address"].ToString();
                }
            }
            con.Close();
            return student;
        }

    }
}
