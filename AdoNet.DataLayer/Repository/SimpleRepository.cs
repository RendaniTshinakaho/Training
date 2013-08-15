using System;
using System.Globalization;
using System.Linq;
using DomainClasses;

namespace Repository
{
    public class SimpleRepository
    {
        public void getStudent()
        {
            var context = new StudentDatabaseTestEntities();
            var cust = from c in context.Students select c;
            foreach (var student in cust)
            {
                Console.WriteLine("{0}", student.Name);
                Console.WriteLine("{0}",student.StudentId.ToString(CultureInfo.InvariantCulture));
                Console.WriteLine("{0}",student.Subjects);
                Console.ReadLine();
            }
     }
    }
}
