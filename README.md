[![Build status](https://ci.appveyor.com/api/projects/status/g4svu8vmli7tfqp0?svg=true)](https://ci.appveyor.com/project/cristian-diaconescu/recurringdates) 
[![diaconesq MyGet Build Status](https://www.myget.org/BuildSource/Badge/diaconesq?identifier=767fe9e9-01f3-4d50-92dc-f27300486d6d)](https://www.myget.org/)

# RecurringDates

.NET library for calculating when a given event occurs, based on a programmatic recurring rule.
 
The rule can range from very simple (e.g. every day) to arbitrarily complex (e.g. The 3rd Monday and also Thursday & Friday after the 3rd Monday of January/April/July/October/December).

See the unit tests project for plenty of examples.

The main selling points of this library :
- the building blocks are very simple rules
- these rules can be composed to allow any degree of flexibility
- you can get automatic human-readable descriptions for any defined rule (currently in english only)

The rules work at day/week/month level. There is (currently) no support for rule-based time-of-day (like "every three hours").

##Installing
Get it from NuGet:  [![NuGet Status](http://img.shields.io/nuget/v/RecurringDates.svg?style=flat)](https://www.nuget.org/packages/RecurringDates/)


Or, in Visual Studio, in the NuGget package manager GUI, search for package '`RecurringDates`'.  

Or install directly From the NuGet Package Manager console:

```
PM> Install-Package RecurringDates
```
##Licensing

This library is licensed under a BSD license and therefore explicitely allows commercial use.  
See LICENSE.txt for more information.
