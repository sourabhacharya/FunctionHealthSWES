# Runbook — SupportEngineerChallenge

> Update this file as part of the exercise.

## Service overview
- **Service:** SupportEngineerChallenge.Api
- **Purpose:** Minimal task tracker (create + list tasks)
- **Data store:** SQLite (`app.db` in the API working directory)

## Common commands

**Run locally**
```bash
cd src/SupportEngineerChallenge.Api
dotnet run
```

**Run tests**
```bash
dotnet test
```

## Key endpoints
- `GET /api/tasks?userId={id}&limit={n}`
- `POST /api/tasks`

## Using log artifacts

- **Create-task 500:** Inspect `artifacts/sample_api_log.txt` (or production logs). Look for the `CreateTask request` line — `X-Client-Timestamp present=False` or `length=0` indicates missing/invalid header. The stack trace shows `FormatException` at `DateTime.Parse`.
- **Slow list:** Look for `ListTasks completed` lines with high `elapsedMs` (e.g. `artifacts/sample_slow_list_log.txt`). Correlate `userId` and `limit` with slow requests.

## Troubleshooting checklist (starter)

### “Create task fails with 500”
- Check API logs in console.
- Verify request payload and headers.
- Look for unhandled exceptions in `POST /api/tasks`.

Screenshot - Error 500 observed on the UI:
<img width="1600" height="925" alt="image" src="https://github.com/user-attachments/assets/18fef525-2cc5-41b8-bcc4-22d2782e473d" />

Screenshot of FormatException error in logs:
<img width="1312" height="572" alt="image" src="https://github.com/user-attachments/assets/5a9822ce-00ff-4dd0-94b5-efae9bdcb26d" />


### “Tasks list is slow”
- Confirm dataset size (seed can be large).
- Inspect how the list endpoint fetches and filters data.
- Review query patterns and database usage.

Screenshot of logs with time values in ms showing slow response (before code fix):

<img width="696" height="172" alt="image" src="https://github.com/user-attachments/assets/1e7fa812-2021-4c06-ae89-2934e7593c80" />


Screenshot of logs with time values in ms showing fast responses (after code fix):

<img width="715" height="210" alt="image" src="https://github.com/user-attachments/assets/2a2af256-0012-49f5-a502-af1841d7fe3f" />
<img width="682" height="184" alt="image" src="https://github.com/user-attachments/assets/ba96b031-1703-455a-a74f-625fc949bd23" />


### “Duplicates / wrong order after refresh”
- Compare Swagger API response vs UI rendering.
- Check the UI state update logic during refresh.
- Verify how the list is merged and ordered.

Screenshot of duplicate records after task creation:
<img width="1600" height="930" alt="image" src="https://github.com/user-attachments/assets/d7748e67-6054-40b7-bf0a-15f0c641647a" />


## Verification steps (starter)
- Create tasks from UI and via Swagger.
- Refresh tasks repeatedly; confirm no duplicates and ordering is correct.
- Validate list endpoint returns only requested user's tasks.

<img width="1523" height="1012" alt="image" src="https://github.com/user-attachments/assets/977c59d4-8b7d-41bf-b416-36c36c768d04" />


## Rollback / mitigation ideas (starter)
- Roll back to last known good version.
- Temporarily disable problematic client behavior (feature flag / UI change).
Allow only authorized admin users to access the web application by using SSO login features.
- Add guardrails (e.g. input validation, error handling) to prevent unhandled exceptions.
- Input validation:
  1. Check the total length of field, any invalid special characters and/or bad data and accordingly clean up the data entry before insertion.
  2. To check proper data entry, set a maximum limit on the number of tasks a user can add to the table in a day or an hour.

