using System.Diagnostics;
using System.Text;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            #region Q1

            // (a) because using the string will make us create 5000 new string and assign them to the result variable
            // and that well be overhead on the program

            // ----------- String Version -----------
            Stopwatch sw1 = Stopwatch.StartNew();
            string result1 = "";

            for (int i = 1; i <= 5000; i++)
            {
                result1 += i + ",";
            }

            sw1.Stop();
            Console.WriteLine($"String Time: {sw1.ElapsedMilliseconds} ms");

            // ----------- StringBuilder Version -----------
            Stopwatch sw2 = Stopwatch.StartNew();
            StringBuilder sb = new StringBuilder();

            for (int i = 1; i <= 5000; i++)
            {
                sb.Append(i).Append(",");
            }

            string result2 = sb.ToString();
            sw2.Stop();
            Console.WriteLine($"StringBuilder Time: {sw2.ElapsedMilliseconds} ms");

            
            // (c) StringBuilder is faster than the normal string

            #endregion
            


            #region Q2

            Console.WriteLine("\n--- Ticket Pricing System ---");

            Console.Write("Enter Age: ");
            int age = int.Parse(Console.ReadLine());

            Console.Write("Enter Day: ");
            string day = Console.ReadLine().ToLower();

            Console.Write("Student ID? (yes/no): ");
            string studentInput = Console.ReadLine().ToLower();
            bool isStudent = studentInput == "yes" ? true : false;

            double price = 0;
            string breakdown = "";

            // Base price
            if (age < 5 )
            {
                if(day == "fri" || day == "sat")
                {
                    price = 10;
                    breakdown = "Base: 0 LE + 10 LE weekend";
                }
                else
                {
                    price = 0;
                    breakdown = "Base: 0 LE";
                }
            }
            else if (age <= 12)
            {
                price = 30;
                breakdown = "Base: 30 LE";
            }
            else if (age <= 59)
            {
                price = 50;
                breakdown = "Base: 50 LE";
            }
            else
            {
                price = 25;
                breakdown = "Base: 25 LE";
            }

            // Student discount
            if (isStudent)
            {
                price *= 0.8;
                breakdown += " - 20% (Student)";
            }

            Console.WriteLine($"Final Price: {price} LE");
            Console.WriteLine($"Breakdown: {breakdown}");

            #endregion
            


            #region Q3

            string fileExtension = ".pdf";
            string fileType;

            //(A) Traditional switch statement

            switch (fileExtension)
            {
                case ".pdf":
                    fileType = "PDF Document";
                    break;

                case ".doc":
                case ".docx":
                    fileType = "Word Document";
                    break;

                case ".xls":
                case ".xlsx":
                    fileType = "Excel Spreadsheet";
                    break;

                case ".jpg":
                case ".png":
                case ".gif":
                    fileType = "Image File";
                    break;

                default:
                    fileType = "Unknown File Type";
                    break;
            }

            //(b) Switch expression

            fileType = fileExtension switch
            {
                ".pdf" => "PDF Document",

                ".doc" or ".docx" => "Word Document",

                ".xls" or ".xlsx" => "Excel Spreadsheet",

                ".jpg" or ".png" or ".gif" => "Image File",

                _ => "Unknown File Type"
            };

       
            #endregion
            


            #region Question 04 - Ternary Operator

            int temperature = 35;

            string weatherAdvice =
                temperature < 0 ? "Freezing! Stay indoors." :
                temperature < 15 ? "Cold. Wear a jacket." :
                temperature < 25 ? "Pleasant weather." :
                temperature < 35 ? "Warm. Stay hydrated." :
                                   "Hot! Avoid sun exposure.";

            Console.WriteLine(weatherAdvice);

            // No less readable.
            // Use ternary for simple conditions.
            // Use if-else for complex or multiple branches.

            #endregion
            

            
            #region Q5

            int attempts = 0;
            bool valid = false;

            do
            {
                Console.Write("\nEnter Password: ");
                string password = Console.ReadLine();

                attempts++;

                bool hasUpper = false;
                bool hasDigit = false;
                bool hasSpace = false;

                foreach (char c in password)
                {
                    if (char.IsUpper(c)) hasUpper = true;
                    if (char.IsDigit(c)) hasDigit = true;
                    if (char.IsWhiteSpace(c)) hasSpace = true;
                }

                if (password.Length < 8)
                    Console.WriteLine("Must be at least 8 characters");

                if (!hasUpper)
                    Console.WriteLine("Must contain uppercase letter");

                if (!hasDigit)
                    Console.WriteLine("Must contain digit");

                if (hasSpace)
                    Console.WriteLine("No spaces allowed");

                valid = password.Length >= 8 && hasUpper && hasDigit && !hasSpace;

                if (valid)
                {
                    Console.WriteLine("Password accepted!");
                    break;
                }

            } while (attempts < 5);

            if (!valid)
                Console.WriteLine("Account locked");

            #endregion
            */
            
            #region Q6

            int[] scores = { 95, 87, 45, 72, 38, 100, 56, 91, 62, 49 };

            // (a) Failing scores < 50
            Console.Write("\nFailing Scores:");
            foreach (int s in scores)
            {
                if (s < 50)
                    Console.Write($"{s} ");
            }
            Console.WriteLine();

            // (b) First score > 90
            Console.Write("First score > 90: ");
            foreach (int s in scores)
            {
                if (s > 90)
                {
                    Console.WriteLine(s);
                    break;
                }
            }

            // (c) Average excluding < 40
            int sum = 0;
            int count = 0;

            foreach (int s in scores)
            {
                if (s >= 40)
                {
                    sum += s;
                    count++;
                }
            }

            Console.WriteLine($"Average: {(double)sum / count}");

            // (d) Grade distribution
            int A = 0, B = 0, C = 0, D = 0, F = 0;

            foreach (int s in scores)
            {
                if (s >= 90) A++;
                else if (s >= 80) B++;
                else if (s >= 70) C++;
                else if (s >= 60) D++;
                else F++;
            }

            Console.WriteLine($"A:{A} B:{B} C:{C} D:{D} F:{F}");

            #endregion
            
        }
    }
}
