

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Supermarket
{
    /// <summary>
    /// Driver Class for Simulation
    /// </summary>
    class SimulationDriver
    {

        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Title = "Supermarket Simulation";
            Menu menu = new Menu("Supermarket Simulation");
            menu = menu + "Set the number of customers" + "Set the number of hours of operation"
                + "Set the number of registers" + "Set the expected checkout duration"
                + "Run the simulation"
                + "Exit program";


            // Start a new simulation class
            Simulation simulation = new Simulation();

            Choices choice = (Choices)menu.GetChoice();
            while (choice != Choices.EXIT)
            {
                switch (choice)
                {
                    case Choices.NUM_CUSTOMERS:
                        Console.Write("How many customers will be served in a day?: ");

                        var totalCustomers = Console.ReadLine();

                        simulation.customers = Convert.ToInt32(totalCustomers);

                        break;

                    case Choices.NUM_HOURS:
                        Console.Write("How many hours will the business be open?: ");

                        simulation.SetClosingTime(Convert.ToInt32(Console.ReadLine()));

                        break;

                    case Choices.NUM_REGISTERS:
                        Console.WriteLine("How many lines are to be simulated?:");

                        simulation.SetTotalRegisters(Convert.ToInt32(Console.ReadLine()));

                        break;
                    case Choices.NUM_CHECKOUT:

                        break;
                    case Choices.RUN:
                        Console.WriteLine("Running simulation...");

                        simulation.Run();

                        Console.ReadKey();
                        break;
                }  // end of switch

                choice = (Choices)menu.GetChoice();
            } 

        }
    }
}
