using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Support_Ticket_Classifier
{
    public class Classifier
    {
        public event Action<Ticket?, (Priority, Destination, string)> onTicketClassified;
        public (Priority priority, Destination destination, string description) Classify(Ticket? ticket)
        {
            (Priority, Destination, string) result = ticket switch
            {
                //Case 1:
                null         => (Priority.Invalid, Destination.None, "The ticket is null!"),
                { Id: <= 0 } => (Priority.Invalid, Destination.None, "Invalid Id!"),

                { Message: var mes }
                    when string.IsNullOrWhiteSpace(mes) =>
                        (Priority.Invalid, Destination.None, "Invalid Message!"),

                { Customer: not null, Customer: CustomerInfo customer }
                    when string.IsNullOrWhiteSpace(customer.Name) =>
                        (Priority.Invalid, Destination.None, "Invalid customer name!"),

                { Customer: not null, Customer: CustomerInfo customer }
                    when !Enum.IsDefined(customer.Subscription) =>
                        (Priority.Invalid, Destination.None, "Invalid customer subscription!"),

                { Category: TicketCategory cat }
                    when !Enum.IsDefined(cat) =>
                        (Priority.Invalid, Destination.None, "Invalid ticket category!"),

                //case 2:
                { IsSecurityRelated: true } =>
                    (Priority.Critical, Destination.Security, "Security ticket!"),

                //case 3:
                { AffectedUsers: >= 100, Category: TicketCategory.Technical } =>
                    (Priority.Critical, Destination.Technical, "Technical ticket affecting over a 100 users."),

                //case 4:
                { Category: TicketCategory.Technical, Customer: not null, Customer.Subscription: SubscriptionType.Premium } =>
                    (Priority.High, Destination.Technical, "Premium customer with a technical issue"),

                //case 5:
                { Category: TicketCategory.Billing, HoursWaiting: >= 24 } =>
                    (Priority.High, Destination.Billing, "Billing issue waiting over 24 hours"),

                //case 6:
                { Category: TicketCategory category, HoursWaiting: uint hours, Customer: not null, Customer.Subscription: SubscriptionType subscription }
                    when IsSubscriptionTimeExceeded(subscription, hours) =>
                        (Priority.High, (Destination)(int)category, $"Waiting time for {subscription.ToString()} customer exceeded!"),

                //case 7:
                { Customer: null, Category: TicketCategory.Account } =>
                    (Priority.Normal, Destination.Account, "No customer information"),

                //case 8:
                { Category: TicketCategory.General, Customer.Subscription: SubscriptionType.Free } =>
                    (Priority.Low, Destination.General, "General query from free user"),

                //Case 9:
                { Category: TicketCategory category } =>
                    (Priority.Normal, (Destination)(int)category, "Ticket is valid and common.")
            };

            onTicketClassified?.Invoke(ticket ?? null, result);

            return result;
        }

        private bool IsSubscriptionTimeExceeded(SubscriptionType subscription, uint time)
        {
            if (subscription == SubscriptionType.Free)
                return time >= 72U;
            if (subscription == SubscriptionType.Standard)
                return time >= 48U;
            if (subscription == SubscriptionType.Premium)
                return time >= 12U;

            //TODO: Replace with actual exception
            throw new NotImplementedException();
        }
    }
}
