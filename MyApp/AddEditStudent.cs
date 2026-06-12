using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyApp
{
    public partial class AddEditStudent : Form
    {
        //public delegate void MysimpleDelegate();
        //public event MysimpleDelegate StudentAdded;
        private int _studentId;
        private Student _student;

        private FileHelper<List<Student>> _fileHelper = new FileHelper<List<Student>>
           (Program.FilePath);

        public AddEditStudent(int id = 0)
        {
            InitializeComponent();
            _studentId = id;
            GetStudentData();
        }

        //private void OnStudentAdded()
        //{
        //    StudentAdded?.Invoke();
        //}

        private void GetStudentData()
        {
            if (_studentId != 0)
            {
                Text = "Edytowanie danych Ucznia";

                var students = _fileHelper.DeserializeFromFile();
                _student = students.FirstOrDefault(x => x.Id == _studentId);

                if (_student == null)
                {
                    throw new Exception("Brak użytkownika o podanym Id");
                }
                FillTextBoxes();
            }
            tbFirstName.Select();
        }
        private void FillTextBoxes()
        {
            tbId.Text = _student.Id.ToString();
            tbFirstName.Text = _student.FirstName;
            tbLastName.Text = _student.LastName;
            tbPolish.Text = _student.Polish;
            tbEnglish.Text = _student.English;
            tbMath.Text = _student.Math;

        }
        
      

        private async void btnConfirm_Click(object sender, EventArgs e)
        {
            var students = _fileHelper.DeserializeFromFile();

            if (_studentId != 0)
                students.RemoveAll(x => x.Id == _studentId);
            
            else
                AssignIdToNewStudent(students);


            AddNewStudentToList(students);

    
            _fileHelper.serializeToFile(students);
            //OnStudentAdded();
           await LongProcessAsync();
            Close();

        }

        private async Task LongProcessAsync()
        {
            
           await Task.Run(() =>
            {
                Thread.Sleep(3000);
            });
        }

        private void AddNewStudentToList(List<Student> students)
        {
            var student = new Student
            {
                Id = _studentId,
                FirstName = tbFirstName.Text,
                LastName = tbLastName.Text,
                Polish = tbPolish.Text,
                English = tbEnglish.Text,
                Math = tbMath.Text,

            };
            students.Add(student);
        }
        private void AssignIdToNewStudent(List<Student> students)
        {
            var studentWithHighestId = students.OrderByDescending(x => x.Id).FirstOrDefault();



            _studentId = studentWithHighestId == null ? 1 : studentWithHighestId.Id + 1;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close(); 
        }
    }


}
