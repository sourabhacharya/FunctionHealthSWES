TicketID#1

**Title:** Error 500, Slow List, Refresh/Add button click generates duplicates
**Priority:** P0 
**Owner:** Sourabh Acharya 

Environment: Prod

## Description
Issue#1: Server - HTTP Error 500 (Internal Server Error)

Issue#2: Performance - Slow List

Issue#3: Application - Refresh/Add button click is generating duplicates


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
