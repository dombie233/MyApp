using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


namespace MyApp
{
    public partial class Main : Form
    {
        private delegate void DisplayMessage(string message);

        private FileHelper<List<Student>> _fileHelper = new FileHelper<List<Student>>
            (Program.FilePath);
        public Main()
        {
            InitializeComponent();
            RefreshDiary();
            SetColumnsHeader();
            //var list = new List<int> { 2, 244, 22, 5, 58 };
            //var list2 = list.Where(x => x > 10).OrderByDescending(x => x).ToList();

            //foreach ( var x in list2)
            //{
            //    MessageBox.Show($"{x}");
            //}
            //var students = new List<Student>();

            //var students1 = students.Select(x => x.Id);

            //var allPositives = list.All(x => x > 0);

            //var anyNumberBiggerThan100 = list.Any(x => x > 100);
            //MessageBox.Show(anyNumberBiggerThan100.ToString());

            //var contain10 = list.Contains(10);
            //var sum = list.Sum();
            //var count = list.Count();
            //var avg = list.Average();
            //var max = list.Max(); 

            //var firstElement = list.First();

            //var student1 = new Student(); 
            //var person = new Person();
            //student1.Id = 1;
            //student1.FirstName = "Test";

            //person.Id = 2;
            //person = student1;
            //MessageBox.Show(person.Id.ToString());

            //var list = new List<Person>()
            //{
            //    new Student {FirstName = "StudentImie", LastName="StudentNazwisko", Math="4"},
            //};

            //foreach (var item in list)
            //{
            //    MessageBox.Show(item.GetInfo());

            //}

            //var student = new Student();
            //student.PublicField = "1";
            //student._privateField  "2"; //Nie działa :( 
            //student.ProtectedField = "3";

            //try
            //{
            //    var student = new Student();
            //    student.Property = "2";
            //    MessageBox.Show(student.Property);
            //    MessageBox.Show(Math.PI.ToString());

            //}catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

            //var student = new Student();
            //student.Address.City = "1";
            //var messages = new DisplayMessage(DisplayMessage1);
            //messages += DisplayMessage2;
            //messages("test");




        }
        public void DisplayMessage1(string message)
        {
            MessageBox.Show($"Metoda 1 - {message}");
        }
        public void DisplayMessage2(string message)
        {
            MessageBox.Show($"Metoda 22 - {message}");
        }
        private void RefreshDiary()
        {
            var students = _fileHelper.DeserializeFromFile();
            dgvDiary.DataSource = students;
        }

        private void SetColumnsHeader()
        {
            dgvDiary.Columns[0].HeaderText = "Numer";
            dgvDiary.Columns[1].HeaderText = "Imie";
            dgvDiary.Columns[2].HeaderText = "Nazwisko";
            dgvDiary.Columns[3].HeaderText = "Polski";
            dgvDiary.Columns[4].HeaderText = "Angielski";
            dgvDiary.Columns[5].HeaderText = "Matematyka";

        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            var addEditStudent = new AddEditStudent();
            addEditStudent.FormClosing += AddEditStudent_FormClosing;
            //addEditStudent.StudentAdded += AddEditStudent_StudentAdded;
            addEditStudent.ShowDialog();
            //addEditStudent.StudentAdded -= AddEditStudent_StudentAdded;
        }

        private void AddEditStudent_FormClosing(object sender, FormClosingEventArgs e)
        {
            RefreshDiary();
        }

        //private void AddEditStudent_StudentAdded()
        //{
        //    RefreshDiary();
        //}

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvDiary.SelectedRows.Count == 0)
            {
                MessageBox.Show("Prosze zaznacz ucznia którego chcesz edytować");
                return;
            }
            var addEditStudent = new AddEditStudent(Convert.ToInt32(dgvDiary.SelectedRows[0].Cells[0].Value));
            addEditStudent.FormClosing += AddEditStudent_FormClosing;

            addEditStudent.ShowDialog();


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDiary.SelectedRows.Count == 0)
            {
                MessageBox.Show("Prosze zaznacz ucznia którego chcesz usunąc");
                return;
            }

            var selectedStudent = dgvDiary.SelectedRows[0];
            var firstName = selectedStudent.Cells[1].Value?.ToString();
            var lastName = selectedStudent.Cells[2].Value?.ToString();

            var confirmDelete = MessageBox.Show(
             $"Czy na pewno chcesz usunąć ucznia {(firstName + " " + lastName).Trim()}?",
                "Usuwanie ucznia",
             MessageBoxButtons.OKCancel
 );

            if (confirmDelete == DialogResult.OK)
            {
                DeleteStudent(Convert.ToInt32(selectedStudent.Cells[0].Value));
                RefreshDiary();
            }


        }
        private void DeleteStudent(int id)
        {
            var students = _fileHelper.DeserializeFromFile();
            students.RemoveAll(x => x.Id == id);
            _fileHelper.serializeToFile(students);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDiary();
        }

        
    }
}
