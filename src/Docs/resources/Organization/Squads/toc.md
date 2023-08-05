# Product Engineering Organization

## Organization diagram

```mermaid
graph TD;
    CPTO-->ENGINEERING_MANAGER;
    CPTO-->GROUP_PM;
    
    subgraph SquadB2C
        PM1
        UX1
        TL1
        ENGINEER1
        ENGINEER2
        ENGINEER3
    end
    
    subgraph SquadB2B
        PM2
        UX2
        TL2
        ENGINEER4
        ENGINEER5
    end
    
    subgraph Architecture
        Principal
        DevOPS
    end
    
    CPTO-->UX1;
    CPTO-->UX2;
    CPTO-->Principal;
    CPTO-->DevOPS;
    GROUP_PM-->PM1;
    GROUP_PM-->PM2;
    ENGINEERING_MANAGER-->TL1;
    ENGINEERING_MANAGER-->TL2;
    ENGINEERING_MANAGER-->ENGINEER1;
    ENGINEERING_MANAGER-->ENGINEER2;
    ENGINEERING_MANAGER-->ENGINEER3;
    ENGINEERING_MANAGER-->ENGINEER4;
    ENGINEERING_MANAGER-->ENGINEER5;
```

## Squads

* [Squad B2C](/Organization/Squads/squad-B2C)
* [Squad B2B](/Organization/Squads/squad-B2B)
* [Architecture](/Organization/Squads/squad-architecture)
* [Management](/Organization/Squads/squad-management)