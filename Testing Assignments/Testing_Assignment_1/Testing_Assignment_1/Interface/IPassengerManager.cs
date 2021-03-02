using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing_Assignment_1.Database;

namespace Testing_Assignment_1.Interface
{
    public interface IPassengerManager
    {
        Passenger GetPassenger(int id);
        int CreatePassenger(Passenger passenger);
        int UpdatePassenger(int id, Passenger passenger);
        int DeletePassenger(int id);
    
    }
}
