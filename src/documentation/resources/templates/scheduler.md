# Scheduler

> Scheduler service using TickerQ

**Dashboard UI :**
![](/images/Scheduler.png)

**Aspire :**
![](/images/Scheduler-aspire.png)

### Features

**UI**
* ✅ Dashboard UI
    
**Scheduling**
* ✅ Cron job
* ✅ Time job

**Authentication**
* ✅ Basic auth
* ✅ ApiKey auth
* ⚠️ IAM (keycloak) - with issues

**IAC**
* ✅ Docker
* ✅ Aspire

**Data**
* ✅ Postgres
* 🚫 Open telemetry


### Create new project
```console
dotnet new mcsp_scheduler -n Acme
```

### Template options

* -A, --Aspire
    * Whether to include aspire or not.
    * Type : bool
    * Default : false
