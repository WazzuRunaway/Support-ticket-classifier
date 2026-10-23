using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Support_Ticket_Classifier
{
    public enum Destination
    {
        None = -1,
        General = 0,
        Billing = 1,
        Technical = 2,
        Account = 3,
        Security = 4
    }
}
