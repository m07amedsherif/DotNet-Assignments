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

        #region Ticket Base Class
        public class Ticket
        {
            private static int ticketCounter = 0;
            private string movieName;
            private double price;

            public int TicketId { get; private set; }

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

            public virtual void PrintTicket()
            {
                double afterTax = Price * 1.14;

                Console.WriteLine($"Ticket #{TicketId} | {MovieName} | Price: {Price} EGP | After Tax: {afterTax:F2} EGP");
            }

            public void SetPrice(decimal price)
            {
                Price = (double)price;
                Console.WriteLine($"Setting price directly: {Price}");
            }

            public void SetPrice(decimal basePrice, decimal multiplier)
            {
                Price = (double)(basePrice * multiplier);
                Console.WriteLine($"Setting price with multiplier: {basePrice} x {multiplier} = {Price}");
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

            public override void PrintTicket()
            {
                base.PrintTicket();
                Console.WriteLine($"Seat: {SeatNumber}");
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

            public override void PrintTicket()
            {
                base.PrintTicket();
                Console.WriteLine($"Lounge: {(LoungeAccess ? "Yes" : "No")} | Service Fee: {ServiceFee} EGP");
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

            public override void PrintTicket()
            {
                base.PrintTicket();
                Console.WriteLine($"IMAX 3D: {(Is3D ? "Yes" : "No")}");
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
                Console.WriteLine("\n========== All Tickets ==========");

                foreach (var ticket in tickets)
                {
                    if (ticket != null)
                        ticket.PrintTicket();
                }
            }

            public void OpenCinema()
            {
                Console.WriteLine("========= Cinema Opened =========");
                projector.Start();
            }

            public void CloseCinema()
            {
                Console.WriteLine("\n========= Cinema Closed =========");
                projector.Stop();
            }
        }
        #endregion

        public static void ProcessTicket(Ticket t)
        {
            Console.WriteLine("\n========== Process Single Ticket ==========");
            t.PrintTicket();
        }

        static void Main(string[] args)
        {
            // Create cinema
            Cinema cinema = new Cinema("Cairo Cinema");

            cinema.OpenCinema();

            Console.WriteLine("\n========== SetPrice Test ==========");

            // Create tickets
            StandardTicket t1 = new StandardTicket("Inception", new Seat('A', 5), 150, "A5");
            VIPTicket t2 = new VIPTicket("Avengers", new Seat('B', 3), 200, true);
            IMAXTicket t3 = new IMAXTicket("Dune", new Seat('C', 2), 180, false);

            // Test both SetPrice methods
            t1.SetPrice(150);
            t1.SetPrice(100, 1.5m);

            // Add tickets to cinema
            cinema.AddTicket(t1);
            cinema.AddTicket(t2);
            cinema.AddTicket(t3);

            // Print all tickets
            cinema.PrintAllTickets();

            ProcessTicket(t2);

            cinema.CloseCinema();
        }
    }
}