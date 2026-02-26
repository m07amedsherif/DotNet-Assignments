using System.ComponentModel;
using System.Diagnostics;
using System.Numerics;
using System.Security.AccessControl;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleApp2
{
    internal class Program
    {
        public enum TicketType
        {
            Standard,
            VIP,
            IMAX
        }

        public struct Seat
        {
            public char Row { get; set; }
            public int Number { get; set; }

            public Seat(char row, int number)
            {
                Row = row;
                Number = number;
            }

            public string seatLocation()
            {
                return $"{Row}{Number}";
            }
        }

        public class Ticket
        {
            public string MovieName { get; set; }
            public TicketType Type { get; set; }
            public Seat Seat { get; set; }

            private double Price;

            // Full constructor
            public Ticket(string movieName, TicketType type, Seat seat, double price)
            {
                MovieName = movieName;
                Type = type;
                Seat = seat;
                Price = price;
            }

            // Constructor with movie name only (default values)
            public Ticket(string movieName)
                : this(movieName, TicketType.Standard, new Seat('A', 1), 50)
            {
            }

            // 1️ Calculate total after tax
            public double CalcTotal(double taxPercent)
            {
                return Price + (Price * taxPercent / 100);
            }

            // 2️ Apply discount
            public void ApplyDiscount(ref double discountAmount)
            {
                if (discountAmount > 0 && discountAmount <= Price)
                {
                    Price -= discountAmount;
                    discountAmount = 0; // consumed
                }
            }

            // 3️ Print ticket
            public void PrintTicket()
            {
                Console.WriteLine($"Movie: {MovieName}");
                Console.WriteLine($"Type: {Type}");
                Console.WriteLine($"Seat: {Seat.seatLocation()}");
                Console.WriteLine($"Base Price: {Price}");
            }
        }
        static void Main(string[] args)
        {
            
                Console.Write("Enter Movie Name: ");
                string movie = Console.ReadLine();

                Console.Write("Enter Ticket Type (0 = Standard , 1 = VIP , 2 = IMAX ): ");
                TicketType type = (TicketType)int.Parse(Console.ReadLine());

                Console.Write("Enter Seat Row (A, B, C...): ");
                char row = char.Parse(Console.ReadLine());

                Console.Write("Enter Seat Number: ");
                int number = int.Parse(Console.ReadLine());

                Console.Write("Enter Price: ");
                double price = double.Parse(Console.ReadLine());

                Seat seat = new Seat(row, number);
                Ticket ticket = new Ticket(movie, type, seat, price);

                Console.Write("Enter Discount Amount: ");
                double discount = double.Parse(Console.ReadLine());

                Console.Write("Enter Tax Percentage: ");
                double tax = double.Parse(Console.ReadLine());

                // ===== Ticket Info =====
                Console.WriteLine();
                Console.WriteLine("===== Ticket Info =====");
                ticket.PrintTicket();
                Console.WriteLine($"Total ({tax}% tax) : {ticket.CalcTotal(tax):F2}");

                // ===== After Discount =====
                Console.WriteLine();
                Console.WriteLine("===== After Discount =====");

                double discountBefore = discount;

                Console.WriteLine($"Discount Before : {discountBefore:F2}");

                ticket.ApplyDiscount(ref discount);

                Console.WriteLine($"Discount After  : {discount:F2}");

                ticket.PrintTicket();
                Console.WriteLine($"Total ({tax}% tax) : {ticket.CalcTotal(tax):F2}");
            
        }

    }
}
