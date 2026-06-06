TicketID#1

**Title:** Error 500, Slow List, Refresh/Add button click generates duplicates
**Priority:** P0 
**Owner:** Sourabh Acharya 

Environment: Prod

## Description
Issue#1: Server - HTTP Error 500 (Internal Server Error)
Some users intermittently reported Error 500 while creating a task using the Task Tracker web application.


Issue#2: Performance - Task List loads very slowly when there are more than 200 records.
Some users reported slow loading of their webpage, that took more than 5 seconds to load 200+ records, while creating a task using the Task Tracker web application.


Issue#3: Application - Refresh/Add button click is generating duplicates.
All users reported issue of duplicate records while creating a task using the Task Tracker web application.


## Acceptance criteria
- Application should load webpage successfully with no "INTERNAL SERVER ERROR 500"
- Application should load webpage in a timely manner i.e. within a certain maximum limit, for example - 100 ms.
- Application should be robust and stable after each "Refresh" button click OR each "Add" button click. CTA buttons should result in any duplicate record(s).
- Ordering of the record(s) in the Task Tracker application should either be in ascending or descending order.

## Notes / context
- Link to relevant code: https://github.com/sourabhacharya/FunctionHealthSWES
- Alerting suggestions:
  1. Create a program to warn user about duplicate(s) after each entry.
  2. Create a real-time system level alert notification via email for any Error 500 observed in logs.
