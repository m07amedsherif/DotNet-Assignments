using System;

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

            public override string ToString()
            {
                return $"{Row}{Number}";
            }
        }

        #region Interfaces

        public interface IPrintable
        {
            void Print();
        }

        public interface IBookable
        {
            bool Book();
            bool Cancel();
            bool IsBooked { get; }
        }

        #endregion


        #region Ticket Base Class

        public class Ticket : IPrintable, IBookable, ICloneable
        {
            private static int ticketCounter = 0;
            private string movieName;
            private double price;

            public int TicketId { get; private set; }

            public bool IsBooked { get; private set; }

            public string MovieName
            {
                get => movieName;
                set
                {
                    if (!string.IsNullOrWhiteSpace(value))
                        movieName = value;
                }
            }

            public TicketType Type { get; set; }
            public Seat Seat { get; set; }

            public double Price
            {
                get => price;
                set
                {
                    if (value > 0)
                        price = value;
                }
            }

            public Ticket(string movieName, TicketType type, Seat seat, double price)
            {
                ticketCounter++;
                TicketId = ticketCounter;

                MovieName = movieName;
                Type = type;
                Seat = seat;
                Price = price;
            }

            public virtual void Print()
            {
                double afterTax = Price * 1.14;

                Console.WriteLine($"[Ticket #{TicketId}] {MovieName} | {Type} | Price: {Price} | After Tax: {afterTax:F1} | Booked: {(IsBooked ? "Yes" : "No")}");
            }

            public bool Book()
            {
                if (IsBooked)
                    return false;

                IsBooked = true;
                return true;
            }

            public bool Cancel()
            {
                if (!IsBooked)
                    return false;

                IsBooked = false;
                return true;
            }

            public virtual object Clone()
            {
                Ticket clone = (Ticket)this.MemberwiseClone();
                ticketCounter++;
                clone.TicketId = ticketCounter;
                clone.IsBooked = false;
                return clone;
            }

            public void SetPrice(decimal price)
            {
                Price = (double)price;
            }

            public void SetPrice(decimal basePrice, decimal multiplier)
            {
                Price = (double)(basePrice * multiplier);
            }
        }

        #endregion


        #region Child Classes

        public class StandardTicket : Ticket
        {
            public string SeatNumber { get; set; }

            public StandardTicket(string movie, Seat seat, double price, string seatNumber)
                : base(movie, TicketType.Standard, seat, price)
            {
                SeatNumber = seatNumber;
            }

            public override void Print()
            {
                double afterTax = Price * 1.14;

                Console.WriteLine($"[Ticket #{TicketId}] {MovieName} | Standard | Seat: {SeatNumber} | Price: {Price} | After Tax: {afterTax:F1} | Booked: {(IsBooked ? "Yes" : "No")}");
            }
        }

        public class VIPTicket : Ticket
        {
            public bool LoungeAccess { get; set; }
            public decimal ServiceFee { get; set; } = 50;

            public VIPTicket(string movie, Seat seat, double price, bool loungeAccess)
                : base(movie, TicketType.VIP, seat, price)
            {
                LoungeAccess = loungeAccess;
            }

            public override void Print()
            {
                double afterTax = Price * 1.14;

                Console.WriteLine($"[Ticket #{TicketId}] {MovieName} | VIP | Lounge: {(LoungeAccess ? "Yes" : "No")} | Fee: {ServiceFee} | Price: {Price} | After Tax: {afterTax:F1} | Booked: {(IsBooked ? "Yes" : "No")}");
            }
        }

        public class IMAXTicket : Ticket
        {
            public bool Is3D { get; set; }

            public IMAXTicket(string movie, Seat seat, double price, bool is3D)
                : base(movie, TicketType.IMAX, seat, is3D ? price + 30 : price)
            {
                Is3D = is3D;
            }

            public override void Print()
            {
                double afterTax = Price * 1.14;

                Console.WriteLine($"[Ticket #{TicketId}] {MovieName} | IMAX | 3D: {(Is3D ? "Yes" : "No")} | Price: {Price} | After Tax: {afterTax:F1} | Booked: {(IsBooked ? "Yes" : "No")}");
            }
        }

        #endregion


        #region Projector Class

        public class Projector
        {
            public void Start()
            {
                Console.WriteLine("Projector started.");
            }

            public void Stop()
            {
                Console.WriteLine("Projector stopped.");
            }
        }

        #endregion


        #region Cinema Class

        public class Cinema
        {
            public string CinemaName { get; set; }

            private Ticket[] tickets = new Ticket[20];
            private Projector projector = new Projector();

            public Cinema(string name)
            {
                CinemaName = name;
            }

            public bool AddTicket(Ticket t)
            {
                for (int i = 0; i < tickets.Length; i++)
                {
                    if (tickets[i] == null)
                    {
                        tickets[i] = t;
                        return true;
                    }
                }
                return false;
            }

            public void PrintAllTickets()
            {
                Console.WriteLine("\n--- All Tickets ---");

                foreach (var ticket in tickets)
                {
                    if (ticket != null)
                        ticket.Print();
                }
            }

            public void OpenCinema()
            {
                Console.WriteLine("=== Cinema Opened ===");
                projector.Start();
            }

            public void CloseCinema()
            {
                Console.WriteLine("\n=== Cinema Closed ===");
                projector.Stop();
            }
        }

        #endregion


        #region Helper

        public static class BookingHelper
        {
            public static void PrintAll(IPrintable[] items)
            {
                foreach (var item in items)
                    item.Print();
            }
        }

        #endregion


        static void Main(string[] args)
        {
            Cinema cinema = new Cinema("Cairo Cinema");

            cinema.OpenCinema();

            StandardTicket t1 = new StandardTicket("Inception", new Seat('A', 5), 80, "A5");
            VIPTicket t2 = new VIPTicket("Avengers", new Seat('B', 3), 200, true);
            IMAXTicket t3 = new IMAXTicket("Dune", new Seat('C', 2), 100, true);

            t1.Book();
            t2.Book();
            t3.Book();

            cinema.AddTicket(t1);
            cinema.AddTicket(t2);
            cinema.AddTicket(t3);

            cinema.PrintAllTickets();

            Console.WriteLine("\n--- Clone Test ---");

            VIPTicket clone = (VIPTicket)t2.Clone();
            clone.MovieName = "Interstellar";

            Console.Write("Original : ");
            t2.Print();

            Console.Write("Clone    : ");
            clone.Print();

            Console.WriteLine("\n--- After Cancellation ---");

            t1.Cancel();
            t1.Print();

            Console.WriteLine("\n--- BookingHelper.PrintAll ---");

            IPrintable[] arr = { t1, t2, t3 };

            BookingHelper.PrintAll(arr);

            cinema.CloseCinema();
        }
    }
}