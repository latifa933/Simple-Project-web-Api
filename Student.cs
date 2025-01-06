using StudentApiDataAccessLayer;

namespace StudentApiBisnessLayer
{
    public class Student
    { public enum enmode { addnew =0,update =1};
      public enmode mode = enmode.addnew;
      
      public StudentDTO STDO
        {
            get { return (new StudentDTO(this.ID, this.Name, this.Age, this.Grade)); }
        }
      public Student(StudentDTO SDTO,enmode Cmode = enmode.addnew)
        {
            this.ID = STDO.Id;
            this.Name = SDTO.Name;
            this.Age = SDTO.Age;
            this.Grade = SDTO.Grade;
            mode = Cmode;
        }
     
      public int ID { get; set; }
      public string Name { get; set; }
      public int Age { get; set; }
      public int Grade { get; set; }
      
      public static List<StudentDTO> GetAllStudents()
        {
            return StudentData.GetAllStudents();
        }
      public static List<StudentDTO> GetPassedStudents()
        {
            return StudentData.GetPassedStudents();
        }
      public static double GetAverageGrade()
        {
            return StudentData.GetAverageGrade();
        }
      public static bool DeleteStudent(int id)
        {
            return StudentData.DeleteStudent(id);
        }
      public static Student Find(int ID)
        {
            StudentDTO SDTO = StudentData.GetStudentByID(ID);
            if (SDTO != null)
            {
                return new Student(SDTO, enmode.update);
            }
            else
                return null;
          
        }
      private bool _AddNewStudent()
        {
            this.ID = StudentData.AddStudent(STDO);
            return this.ID != -1;
        }
      private bool _UpdateStudent()
        {
            return StudentData.UpdateStudent(STDO);
        }
      public bool Save()
        {  switch(mode)
            {
                case enmode.addnew:
                    if (_AddNewStudent())
                    {
                        mode = enmode.update;
                        return true;
                    }
                    else
                        return false;
                case enmode.update:
                    return _UpdateStudent();

            }
            return false;

        }

    }
}
