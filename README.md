# RecurringDates

.NET library for calculating when a given event occurs, based on a programmatic recurring rule.
 
The rule can range from very simple (e.g. every day) to arbitrarily complex (e.g. The 3rd Monday and also Thursday & Friday after the 3rd Monday of January/April/July/October/December).

See the unit tests project for plenty of examples.

The main selling points of this library :
- the building blocks are very simple rules
- these rules can be composed to allow any degree of flexibility
- you can get automatic human-readable descriptions for any defined rule (currently in english only)

The rules work at day/week/month level. There is (currently) no support for rule-based time-of-day (like "every three hours").

##Licensing

This library is licensed under a BSD license and therefore explicitely allows commercial use.  
See LICENSE.txt for more information.
