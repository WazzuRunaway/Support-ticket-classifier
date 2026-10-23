using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Support_Ticket_Classifier
{
    public readonly record struct CustomerInfo (string Name, SubscriptionType Subscription)
    {
    }
}
