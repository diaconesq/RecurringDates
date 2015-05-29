[![Build status](https://ci.appveyor.com/api/projects/status/g4svu8vmli7tfqp0?svg=true)](https://ci.appveyor.com/project/cristian-diaconescu/recurringdates) 
[![diaconesq MyGet Build Status](https://www.myget.org/BuildSource/Badge/diaconesq?identifier=767fe9e9-01f3-4d50-92dc-f27300486d6d)](https://www.myget.org/) 
[![NuGet Status](http://img.shields.io/nuget/v/RecurringDates.svg?style=flat)](https://www.nuget.org/packages/RecurringDates/)

# RecurringDates

.NET library for calculating when a given event occurs, based on a programmatic recurring rule.
 
The rule can range from very simple (e.g. every day) to arbitrarily complex (e.g. The 3rd Monday and also Thursday & Friday after the 3rd Monday of January/April/July/October/December).

See the unit tests project for plenty of examples.

## The main selling points
### Very simple building blocks

    var everyMonday = new DayOfWeekRule(DayOfWeek.Monday);
    var date = new DateTime(2015, 3, 9);
    everyMonday.IsMatch(date).Should().BeTrue();
    
    var lastDayOfTheMonth = new NthInMonthRule { Nth = -1, ReferencedRule = new EveryDayRule()};
    var date = new DateTime(2015, 2, 28);
    lastDayOfTheMonth.IsMatch(date).Should().BeTrue();

### Composable rules
There are building blocks for the usual logical operations: *And*, *Or*, *Not*, and for set operations: Union ( = Or), Intersect ( = And), Difference

These rules can be composed to allow any degree of flexibility.

Get the 17th of the month if it's not a Sunday
    
    var the17th = new NthInMonthRule { Nth = 17, ReferencedRule = new EveryDayRule()};
    var sundays = new DayOfWeekRule(DayOfWeek.Sunday);

Start composing:

    var notSundays = new NotRule{referencedRule = sundays};

Now let's put them together:

    var rule = new SetIntersectionRule { Rules = new[] {the17th, notSundays} };
    
    var date = new DateTime(2015, 4, 17); //it's a Friday
    rule.IsMatch(date).Should().BeTrue();

The same can be achieved using set difference: "all 17th's" *except* "all sundays":

    var sameRule = new SetDifferenceRule { IncludeRule = the17th, ExcludeRule = sundays};
    sameRule.IsMatch(date).Should().BeTrue();

### Fluent syntax
 
 This is optional, but produces very readable rules.
 
 Here are the same examples from above, written in fluent syntax:
    
    var everyMonday = DayOfWeek.Monday.EveryWeek();
    var lastMondayOfTheMonth = everyMonday.TheLastOccurenceInTheMonth();
    

The "17th and not a Sunday" example from above, rewritten in fluent syntax:

    var sundays = DayOfWeek.Sunday.EveryWeek();
    var notSundays = sundays.Not(); 
    var the17th = new EveryDayRule().NthInMonth(17);
    
    var rule = the17th.IfAlso(notSundays);
    var sameRule = the17th.Except(sundays);

Another example: get the first and last Sunday of the month:
    
    var sundays = DayOfWeek.Sunday.EveryWeek();
    var firstOrLast = sundays.The1stOccurenceInTheMonth()
                  .Or(sundays.TheLastOccurenceInTheMonth());


### Automatic human-readable descriptions for any defined rule

    var anyThirdMonday = DayOfWeek.Monday.EveryWeek().The3rdOccurenceInTheMonth();
    var thirdMonday = anyThirdMonday.InMonths(Month.Jan, Month.Apr, Month.Jul, Month.Oct, Month.Dec);
    
    Console.WriteLine(thirdMonday.GetDescription());
    
    => 3rd Monday of the month in Jan, Apr, Jul, Oct, Dec

### Serializable built-in rules 

    var rule = DayOfWeek.Monday.EveryWeek().The2ndOccurenceInTheMonth();
    string serializedString = new RuleSerializer().Serialize(rule);
    
    //persist it e.g. in a database; come back later and retrieve it:
    IRule deserializedRule = new RuleSerializer().Deserialize(serializedString);

There are also helper methods for serializing your own custom rules. You just need to specify the assemblies where the rules live, or the actual types of the custom rules.

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

##Start a discussion

I'm eager to hear about any bugs or suggestions: https://github.com/diaconesq/RecurringDates/issues

