


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket
{
    enum EVENTTYPE {ARRIVAL, CHECKOUT, LEAVE};
    /// <summary>
    /// Events class for customer
    /// </summary>
    class Events: IComparable
    {
        public EVENTTYPE Type { get; set; }

        public DateTime Time { get; set; }

        public Customer customer { get; set; }

        /// <summary>
        /// Creates a new Event object
        /// </summary>
        /// <param name="type"></param>
        /// <param name="time"></param>
        /// <param name="ourcustomer"></param>
        public Events(EVENTTYPE type, DateTime time, Customer ourcustomer)
        {
            Type = type;
            Time = time;
            customer = ourcustomer;
        }

        /// <summary>
        /// Compares events (time)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns
        public int CompareTo(Object obj)
        {

            if (!(obj is Events))
            {
                throw new ArgumentException("The argument is not a Event object.");
            }

            Events e = (Events)obj;
            return (e.Time.CompareTo(Time));

        }


    }
}
