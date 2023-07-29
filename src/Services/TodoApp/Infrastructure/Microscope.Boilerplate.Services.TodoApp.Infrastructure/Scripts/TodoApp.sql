CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM pg_namespace WHERE nspname = 'boilerplate_todoapp') THEN
        CREATE SCHEMA boilerplate_todoapp;
    END IF;
END $EF$;

CREATE TABLE boilerplate_todoapp."TodoLists" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "TenantId" text NULL,
    "CreatedAt" timestamp without time zone NOT NULL,
    "CreatedBy" uuid NOT NULL,
    "CreatorMail" text NOT NULL,
    "UpdatedAt" timestamp without time zone NOT NULL,
    "UpdatedBy" uuid NOT NULL,
    CONSTRAINT "PK_TodoLists" PRIMARY KEY ("Id")
);

CREATE TABLE boilerplate_todoapp."Tag" (
    "Id" uuid NOT NULL,
    "Label" text NOT NULL,
    "Color" text NOT NULL,
    "TodoListId" uuid NOT NULL,
    CONSTRAINT "PK_Tag" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Tag_TodoLists_TodoListId" FOREIGN KEY ("TodoListId") REFERENCES boilerplate_todoapp."TodoLists" ("Id") ON DELETE CASCADE
);

CREATE TABLE boilerplate_todoapp."TodoItem" (
    "Id" uuid NOT NULL,
    "Label" text NOT NULL,
    "IsCompleted" boolean NOT NULL,
    "TodoListId" uuid NOT NULL,
    "TenantId" text NULL,
    CONSTRAINT "PK_TodoItem" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_TodoItem_TodoLists_TodoListId" FOREIGN KEY ("TodoListId") REFERENCES boilerplate_todoapp."TodoLists" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_Tag_TodoListId" ON boilerplate_todoapp."Tag" ("TodoListId");

CREATE INDEX "IX_TodoItem_TodoListId" ON boilerplate_todoapp."TodoItem" ("TodoListId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230726131829_InitialCreate', '7.0.9');

COMMIT;


