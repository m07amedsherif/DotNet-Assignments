using System.Diagnostics;
using System.Text;

namespace ConsoleApp2
{
    internal class Program
    {
        class PersonClass
        {
            public string Name;
        }

        struct PersonStruct
        {
            public string Name;
        }

        class Student
        {
            public string Name;      // Accessible everywhere
            private double GPA;      // Accessible only inside this class

            public void SetGPA(double gpa)
            {
                GPA = gpa;
            }

            public void PrintGPA()
            {
                Console.WriteLine(GPA);
            }
        }

        static void Main(string[] args)
        {
            #region part1Q1
            // CLASS (Reference Type)
            // STRUCT (Value Type)

            PersonClass p1 = new PersonClass();
            p1.Name = "Mohamed";
            PersonClass p2 = p1;
            p2.Name = "Ali";

            Console.WriteLine(p1.Name); // name changed


            PersonStruct s1 = new PersonStruct();
            s1.Name = "Mohamed";
            PersonStruct s2 = s1;
            s2.Name = "Ali";
            Console.WriteLine(s1.Name); // name did not change
            #endregion


            #region Part1Q2
            //public -> Accessible from anywhere
            //private -> Accessible only inside the same class

            Student s = new Student();
            s.Name = "Mohamed";     // Allowed
            // s.GPA = 3.5;         Error (private)

            s.SetGPA(3.5);
            s.PrintGPA();
            #endregion

            #region Part1Q3
            /*
                Open Visual Studio
                Create New Project
                Class Library (.NET)
                Name it (e.g., MyLibrary)
                Click Create
             */
            #endregion

            #region Part1Q4
            /*
                A Class Library is a separate project that contains reusable classes, but has no Main method and cannot run on its own. 
                It compiles into a .dll file (Dynamic Link Library).
            why we use it ?
                Reusability — write once, use in many projects
                Organization — Separate concerns into different assemblies
                Teamwork — different developers work on different libraries
                Maintenance — fix a bug once, all projects benefit
             */
            #endregion
        }

    }
}