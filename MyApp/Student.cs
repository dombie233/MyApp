using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp
{

    //public class Math
    //{
    //    public const double PI = 3.14159; 
    //}

    public class Address
    {
        public string City { get; set; }
        public string Street { get; set; }

    }
    public class Student
    {
     
        //public string PublicField;
        //private readonly string _privateField;
        //public Student()
        //{
        //    _privateField = "1";
        //}

        //private string _property;
        //public string Property {
        //    get
        //    {
        //        if (_property == "1")
        //            throw new Exception("Zła wartość");

        //        return _property;
        //    }

        //   set
        //    {
        //        System.Windows.Forms.MessageBox.Show("Przypisywanie wartości");
        //        _property = value;
        //    }
        //}
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Polish { get; set; }
        public string English { get; set; }
        public string Math { get; set; }

      
    }

    
}
