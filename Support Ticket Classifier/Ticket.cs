using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Support_Ticket_Classifier
{
    public readonly record struct Ticket
    {
        private static int counter = 1;
        public int Id { get; init; }
        public TicketCategory Category {get; init;}
        public CustomerInfo? Customer {get; init;}
        public string Message {get; init;}
        public uint HoursWaiting { get; init;}
        public uint AffectedUsers { get; init;}
        public bool IsSecurityRelated { get; init;}

        public Ticket(uint hoursWaiting, uint affectedUsers, bool isSecurityRelated = false, TicketCategory category = TicketCategory.General, CustomerInfo? customer = null, string message = "")
        {
            Id = counter++;
            Category = category;
            Customer = customer;
            Message = message;
            HoursWaiting = hoursWaiting;
            AffectedUsers = affectedUsers;
            IsSecurityRelated = isSecurityRelated;
        }

        public void Deconstruct(out int id, out TicketCategory category, out CustomerInfo? customer,
            out string message, out uint hoursWaiting, out uint affectedUsers, out bool isSecurityRelated) =>

        (id, category, customer, message, hoursWaiting, affectedUsers, isSecurityRelated) =
            (Id, Category, Customer, Message, HoursWaiting, AffectedUsers, IsSecurityRelated);
    }
}
