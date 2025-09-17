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

**IAM**
* ✅ Basic Auth
* 🚫 IAM (keycloak)

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

* -D, --Docker
    * Whether to include docker compose or not.
    * Type : bool
    * Default : false

* -A, --Aspire
    * Whether to include aspire or not.
    * Type : bool
    * Default : false
