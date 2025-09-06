CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;
DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM pg_namespace WHERE nspname = 'efcore') THEN
        CREATE SCHEMA efcore;
    END IF;
END $EF$;

CREATE TABLE efcore."TodoLists" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "CreatedAt" timestamp without time zone NOT NULL,
    "CreatedBy" uuid NOT NULL,
    "CreatorMail" text,
    "UpdatedAt" timestamp without time zone NOT NULL,
    "UpdatedBy" uuid NOT NULL,
    "TenantId" text,
    CONSTRAINT "PK_TodoLists" PRIMARY KEY ("Id")
);

CREATE TABLE efcore."Tag" (
    "Id" uuid NOT NULL,
    "Label" text NOT NULL,
    "Color" text NOT NULL,
    "TodoListId" uuid NOT NULL,
    CONSTRAINT "PK_Tag" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Tag_TodoLists_TodoListId" FOREIGN KEY ("TodoListId") REFERENCES efcore."TodoLists" ("Id") ON DELETE CASCADE
);

CREATE TABLE efcore."TodoItem" (
    "Id" uuid NOT NULL,
    "Label" text NOT NULL,
    "IsCompleted" boolean NOT NULL,
    "TodoListId" uuid NOT NULL,
    CONSTRAINT "PK_TodoItem" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_TodoItem_TodoLists_TodoListId" FOREIGN KEY ("TodoListId") REFERENCES efcore."TodoLists" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_Tag_TodoListId" ON efcore."Tag" ("TodoListId");

CREATE INDEX "IX_TodoItem_TodoListId" ON efcore."TodoItem" ("TodoListId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250807233214_InitialCreate', '9.0.8');

COMMIT;
