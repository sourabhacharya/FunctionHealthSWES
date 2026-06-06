# Incident Summary (Fill in)

**Title:**  Multiple issues reported by users while using Task Tracker application 
**Date:**  2026-06-04
**Severity:**  P0

## Impact
- Who/what was impacted? Web application - Task Tracker application used by clients: User-001, User-002, User-003, User-004, User-005.
- Symptoms observed by customers/internal users:
Web application is impacted by Error 500; slow loading on task creation; duplicate task creation.

## Detection
- How did we learn about this? (customer reports, monitoring, etc.)
Issue Confirmed: Task creation intermittently returns 500
Evidence from logs:
System.FormatException: String '' was not recognized as a valid DateTime.

From: TaskEndpoints.cs:line 41
CreateTask request UserId=user-002 Title= X-Client-Timestamp present=False length=0


## Timeline (UTC)
- 10:30 — Error 500 observed on task creation
- 11:00 - Fixed Error 500
- 11:30 — Slow loading on task creation
- 12:00 - Fixed slow list loading
- 12:30 — Found issue of duplicate task creation
- 13:00 - Fixed the issue of duplicate task creation  

## Root cause
- What happened and why?
Server and Performance Issues is intermittent because:
- Some clients send X-Client-Timestamp
- Some don’t
- Missing header = empty string = application crash
So behavior depends on client/user → Application sometimes fails.


## Mitigation / resolution
- What did we change to stop the bleeding?
Fixed the timestamp and DB-filtering issues with dotnet code changes in TaskEndPoints.cs;
Fixed the REFRESH logic in JS code in main.js
  
- What was the final fix?
| Symptom               | Root cause           | Fix                       |
| --------------------- | -------------------- | ------------------------- |
| 500 errors            | DateTime.Parse("")   | TryParse / validation     |
| slow list             | loading full table   | DB-level filtering        |
| duplicates / ordering | in-memory operations | ORDER BY + pagination     |

## Verification
- How did we verify the fix worked?
Validated the server error fix;
Application performance logs for lower time, i.e. less tha 30 ms values;
No duplicates created after clicking Refresh/Add buttons.

## Follow-ups / action items
- Updated TICKET.MD for documenting fix and next steps to feature enhancement
- Updated RUNBOOK.MD for following best practices during application crash scenarios.
