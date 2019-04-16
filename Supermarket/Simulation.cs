

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using PriorityQueue;

namespace Supermarket
{
    /// <summary>
    /// Simulation Class
    /// </summary>
    class Simulation
    {

        private Random r = new Random();
        private PriorityQueue<Events> PQ = new PriorityQueue<Events>();

        private List<Queue<Customer>> CollectionLines = new List<Queue<Customer>>();


        private TimeSpan shortest, longest, totalTime;

        public int customers { get; set; }

        public int registers { get; set; }

        private DateTime timeWeOpen = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day,
            8, 0, 0);

        private DateTime timeWeClose = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day,
            8, 0, 0);

        int hoursAdded = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="Simulation"/> class.
        /// </summary>
        public Simulation()
        {

        }


        /// <summary>
        /// Sets the closing time.
        /// </summary>
        /// <param name="amountToAdd">The amount to add to opening time</param>
        public void SetClosingTime(int amountToAdd)
        {
            timeWeClose = timeWeOpen.AddHours(amountToAdd);
            hoursAdded = amountToAdd;
        }

        /// <summary>
        /// Adds to the amount of registers
        /// </summary>
        /// <param name="amount">int amount of registers</param>
        public void SetTotalRegisters(int amount)
        {
            registers = amount;
        }

        /// <summary>
        /// Negs the exp.
        /// </summary>
        /// <param name="ExpectedValue">The expected value.</param>
        /// <returns></returns>
        private double NegExp(double ExpectedValue)
        {
            return -ExpectedValue * Math.Log(r.NextDouble(), Math.E);
        }

        /// <summary>
        /// Starts the simulation
        /// </summary>
        public void Run()
        {
            Console.Clear();

            // Make sure customers property has data
            if (customers <= 0)
            {
                Console.WriteLine("Simulation error: Customers must be greater than 0 to run simulation.");

                return;
            }

            if (registers <= 0)
            {
                Console.WriteLine("Simulation error: Registers must be greater than 0 to run simulation.");

                return;
            }

            if (timeWeOpen == timeWeClose)
            {
                Console.WriteLine("Simulation error: Close time must be not be less-than-equal to opening time.");

                return;
            }



            GenerateCustomers();

            CreateRegisters();

            DoSimulation();

        }

        /// <summary>
        /// Finds the smallest Line (queue)
        /// </summary>
        /// <returns></returns>
        public int FindSmallestLine()
        {
            Queue<Customer> smallest = CollectionLines[0];
            int smallestIndex = 0;
            for (var i = 0; i < CollectionLines.Count; i++)
                if (CollectionLines[i].Count < smallest.Count)
                    smallestIndex = i;


            return smallestIndex;
        }

        /// <summary>
        /// Finds the longest line (Queue)
        /// </summary>
        /// <returns>int longest line (index)</returns>
        public int FindLongestLine()
        {
            int largestIndex = 0;
            Queue<Customer> smallest = CollectionLines[0];
            for (var i = 0; i < CollectionLines.Count; i++)
                if (CollectionLines[i].Count > smallest.Count)
                {
                    smallest = CollectionLines[i];
                    largestIndex = CollectionLines[i].Count;

                }


            return largestIndex;

        }

        #region CreateRegisters

        /// <summary>
        /// Creates the registers collections
        /// </summary>
        public void CreateRegisters()
        {

            CollectionLines.Clear();

            for (int i = 1; i <= registers; i++)
            {

                CollectionLines.Add(new Queue<Customer>());

            }


        }

        #endregion

        #region Generate Customers

        /// <summary>
        /// Generates the patrons for the simulation based off customers property
        /// </summary>
        public void GenerateCustomers()
        {
            TimeSpan arrival;
            TimeSpan served;

            for (int patron = 1; patron <= customers; patron++)
            {

                arrival = new TimeSpan(0, r.Next(hoursAdded * 60), 0);
                served = new TimeSpan(0, (int)(1.0 + NegExp(4)), 0);


                Customer customer = new Customer(patron, arrival, served);


                PQ.Enqueue(new Events(EVENTTYPE.ARRIVAL, timeWeOpen.Add(arrival), customer));

            }

        }

        #endregion

        #region Simulation Process
        /// <summary>
        /// Runs the simulation process
        /// </summary>
        private void DoSimulation()
        {
            int longestLine = 0;
            TimeSpan avgCheckout = new TimeSpan(0, 0, 0);


            int lineCount = 1;
            int arrivals = 0;
            int depart = 0;

            Console.WriteLine("\n");

            while (PQ.Count > 0)
            {
                Console.Clear();

                // I was unable to get the text to align in columns properly

                // Draw registers
                for (int i = 1; i <= registers; i++)
                {

                    Customer[] array = CollectionLines[i-1].ToArray();

                    Console.Write("Register #" + i +": ");
                    foreach (var item in array)
                    {
                        var buildCustomer = " [C" + item.id + "] ";
                        Console.Write(buildCustomer.PadLeft(3));
                    }

                    Console.Write("\n");
                }

                if (PQ.Peek().Type == EVENTTYPE.ARRIVAL)
                {

                    TimeSpan served = new TimeSpan(0, (int)(1.0 + NegExp(4)), 0);

                    avgCheckout += served;

                    // Enqueue a new event type
                    PQ.Enqueue(new Events(EVENTTYPE.LEAVE, PQ.Peek().Time.Add(served), PQ.Peek().customer));

                    // Find smallest queue to insert into
                    int lowest = FindSmallestLine();

                    CollectionLines[lowest].Enqueue(PQ.Peek().customer);

                    //Console.WriteLine(lineCount + ": Customer arrived at the register. In line in register # {0}", lowest+1);

                    // Get longest line 
                    if(CollectionLines[lowest].Count > longestLine){
                        longestLine = CollectionLines[lowest].Count;
                    }

                    arrivals++;
                }

                if(PQ.Peek().Type == EVENTTYPE.LEAVE)
                {


                    // Dequeue leaving customer

                    for (var i = 0; i < CollectionLines.Count; i++)
                    {
                        if (CollectionLines[i].Contains(PQ.Peek().customer))
                        {
                            CollectionLines[i].Dequeue();

                            

                        }
                    }

                    depart++;
                }

                // Dequeue here because the customer is either arrivaling or leaving always
                PQ.Dequeue();

                // increment event counter
                lineCount++;

                // Display stats
                Console.Write("\n\n-------------------- Statistics ------------------\n");
                Console.Write("\rEvents processed {0}", lineCount - 1);
                Console.Write("\tArrivals: {0}", arrivals);
                Console.Write("\t Departures: {0}", depart);
                Console.Write("\t Longest Line: {0}", longestLine);
                Console.Write("\t Total Registers: {0}", CollectionLines.Count);

                Thread.Sleep(130);

            }

            // Display useful stats to prove the data that was showed //

            // Clear console 
            Console.Clear();

            // Display useful stats
            int seconds = (int)(avgCheckout.TotalSeconds / customers);
            TimeSpan avgSeconds = new TimeSpan(0, 0, seconds);

            Console.WriteLine("Simulation over!\n");

            Console.WriteLine("\n\n\n");

            Console.Write("-------------------- Statistics ------------------");
            Console.Write("\rEvents processed {0}", lineCount-1);
            Console.Write("\tArrivals: {0}", arrivals);
            Console.Write("\t Departures: {0}", depart);
            Console.Write("\t Longest Line: {0}", longestLine);
            Console.Write("\t Total Registers: {0}", CollectionLines.Count);

            Console.Write("\nAverage checkout tme: {0}", avgSeconds);
            Console.Write("\t Hours of Operations: " + timeWeOpen.ToShortTimeString() + "-" + timeWeClose.ToShortTimeString());


            Console.WriteLine("\n\nPress any key to return to main menu....");


        }
        #endregion
    }
}
