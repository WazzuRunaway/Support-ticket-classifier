# Support Ticket Classifier



A C# console application that classifies support tickets by priority and destination using records and pattern matching.



Built as a practical assignment while studying modern C#.



## Features



* Validates tickets, customer information, and enum values.
* Applies ordered rules for security issues, technical incidents, billing delays, and subscription-based escalation.
* Returns a priority, destination, and explanation of the matched rule.
* Demonstrates boundary cases, overlapping rules, and invalid input.



## Classification Rules



The first matching rule wins.



| Condition | Priority | Destination |
|---|---|---|
| Invalid ticket or customer data | Invalid | None |
| Security-related issue | Critical | Security |
| Technical issue affecting at least 100 users | Critical | Technical |
| Premium customer with a technical issue | High | Technical |
| Billing issue waiting at least 24 hours | High | Billing |
| Subscription escalation threshold reached | High | According to category |
| Anonymous account issue | Normal | Account |
| General enquiry from a Free customer | Low | General |
| Any other valid ticket | Normal | According to category |



Escalation thresholds are 72 hours for Free, 48 hours for Standard, and 12 hours for Premium customers.



## Run



Requires the .NET 8 SDK or a newer SDK capable of targeting .NET 8.



From the project directory:



```bash

dotnet run

```



Alternatively, open the project in Visual Studio and run it.



The application processes a fixed set of demonstration tickets and prints their classification results.



## Implementation Notes



* Tickets and customers are represented by readonly record structs.
* Classification uses a switch expression, property patterns, relational patterns, and guards.
* `uint` prevents negative waiting times and affected-user counts.
* Category and destination enums intentionally share numeric values to support direct conversion.
* Console output is triggered through a classification event.



The examples in `Program.cs` are manual demonstrations; the project does not include an automated test suite.

