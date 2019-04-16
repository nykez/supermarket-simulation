



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket
{
    /// <summary>
    /// Customer class used in Events and Simulation
    /// </summary>
    class Customer
    {

        public int id { get; set; }

        /// <summary>
        /// Gets or sets the arrival.
        /// </summary>
        /// <value>
        /// The arrival.
        /// </value>
        public TimeSpan arrival { get; set; }

        /// <summary>
        /// Gets or sets the served time.
        /// </summary>
        /// <value>
        /// The served time.
        /// </value>
        public TimeSpan servedTime { get; set; }

        /// <summary>
        /// Creates a nw empty customer object
        /// </summary>
        public Customer()
        {

        }

        /// <summary>
        /// Creates a new customer object
        /// </summary>
        /// <param name="ourID"></param>
        /// <param name="timeArrival"></param>
        /// <param name="timeToBeServed"></param>
        public Customer(int ourID, TimeSpan timeArrival, TimeSpan timeToBeServed)
        {
            id = ourID;

            arrival = timeArrival;

            servedTime = timeToBeServed;
        }


    }
}
