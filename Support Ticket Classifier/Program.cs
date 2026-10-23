namespace Support_Ticket_Classifier
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Classifier classifier = new Classifier();

            classifier.onTicketClassified += (_, __) => DisplayTicket(in _, in __);
            
            //Test 1
            Ticket? t = null;
            classifier.Classify(t);
            
            //Test 2
            t = new Ticket(message: "", hoursWaiting: 12, affectedUsers: 2, isSecurityRelated: false);
            classifier.Classify(t);
            
            //Test 3 is impossible to create since of uint

            //Test 4
            t = new Ticket(12, 12, message: "anonymous sec issue", isSecurityRelated: true);
            classifier.Classify(t);
            
            //Test 5
            t = new Ticket(message: "", hoursWaiting: 12, affectedUsers: 2, isSecurityRelated: true);
            classifier.Classify(t);
            
            //Test 6
            t = new Ticket(12, 150, category: TicketCategory.Technical, 
                customer: new CustomerInfo("Marat", SubscriptionType.Premium), message: "Test");
            classifier.Classify(t);
            
            //Test 7
            t = new Ticket(12,1, category: TicketCategory.Technical, 
                customer: new CustomerInfo("Marat", SubscriptionType.Premium), message: "Test");
            classifier.Classify(t);
            
            //Test 8
            t = new Ticket(23, 10, message: "Test", category: TicketCategory.Billing);
            classifier.Classify(t);
            t = new Ticket(24, 10, message: "Test", category: TicketCategory.Billing);
            classifier.Classify(t);

            //Test 9
            t = new Ticket(47, 10, message: "Test", category: TicketCategory.General, 
                customer: new CustomerInfo("Semen", SubscriptionType.Standard));
            classifier.Classify(t);
            t = new Ticket(48, 10, message: "Test", category: TicketCategory.General, 
                customer: new CustomerInfo("Semen", SubscriptionType.Standard));
            classifier.Classify(t);

            //Test 10
            t = new Ticket(72, 10, message: "Test", category: TicketCategory.General, 
                customer: new CustomerInfo("Vano", SubscriptionType.Free));
            classifier.Classify(t);

            //Test 11
            t = new Ticket(1, 1, message: "Test", category: TicketCategory.Account);
            classifier.Classify(t);

            //Test 12
            t = new Ticket(1, 1, message: "Ordinary", category: TicketCategory.General);
            classifier.Classify(t);

            //Test 13 - whitespace message
            t = new Ticket(1, 1, message: " \t\n ");
            classifier.Classify(t);

            //Test 14 - invalid Id
            t = new Ticket(1, 1, message: "Test") { Id = 0 };
            classifier.Classify(t);

            //Test 15 - blank customer name
            t = new Ticket(1, 1, message: "Test",
                customer: new CustomerInfo("   ", SubscriptionType.Standard));
            classifier.Classify(t);

            //Test 16 - invalid category
            t = new Ticket(1, 1, message: "Test",
                category: (TicketCategory)999);
            classifier.Classify(t);

            //Test 17 - invalid subscription
            t = new Ticket(1, 1, message: "Test",
                customer: new CustomerInfo("Marat", (SubscriptionType)999));
            classifier.Classify(t);

            //Test 18 - invalid category and security issue
            t = new Ticket(1, 1, message: "Test", isSecurityRelated: true,
                category: (TicketCategory)999);
            classifier.Classify(t);

            //Test 19 - invalid subscription and security issue
            t = new Ticket(1, 1, message: "Test", isSecurityRelated: true,
                customer: new CustomerInfo("Marat", (SubscriptionType)999));
            classifier.Classify(t);

            //Test 20 - general query from free customer
            t = new Ticket(1, 1, message: "Test",
                customer: new CustomerInfo("Marat", SubscriptionType.Free));
            classifier.Classify(t);
        }

        static void DisplayTicket(in Ticket? ticket, in (Priority priority, Destination destination, string description) log)
        {
            Console.WriteLine($"Ticket id: {ticket?.Id ?? -1} \nPriority: {log.priority} \nDestination: {log.destination}\n{log.description}\n");
        }
    }
}
