CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260130100119_InitialMigration') THEN
    CREATE TABLE "Email_Type" (
        "ID" integer GENERATED ALWAYS AS IDENTITY,
        "Name" text NOT NULL,
        "Active" boolean NOT NULL DEFAULT TRUE,
        CONSTRAINT "Email_Type_pkey" PRIMARY KEY ("ID")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260130100119_InitialMigration') THEN
    CREATE TABLE "Lead_Source" (
        "ID" integer GENERATED ALWAYS AS IDENTITY,
        "Name" text NOT NULL,
        "Active" boolean NOT NULL DEFAULT TRUE,
        CONSTRAINT "Lead_Source_pkey" PRIMARY KEY ("ID")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260130100119_InitialMigration') THEN
    CREATE TABLE "Lead_Status" (
        "ID" integer GENERATED ALWAYS AS IDENTITY,
        "Name" text NOT NULL,
        "Active" boolean DEFAULT TRUE,
        CONSTRAINT "Lead_Status_pkey" PRIMARY KEY ("ID")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260130100119_InitialMigration') THEN
    CREATE TABLE "Email_Template" (
        "ID" integer GENERATED ALWAYS AS IDENTITY,
        "Name" text NOT NULL,
        "EmailTypeID" integer NOT NULL,
        "Subject" text NOT NULL,
        "Body" text NOT NULL,
        "Active" boolean NOT NULL DEFAULT TRUE,
        "SouceID" integer,
        CONSTRAINT "Email_Template_pkey" PRIMARY KEY ("ID"),
        CONSTRAINT "Email_Type_Email_Template" FOREIGN KEY ("EmailTypeID") REFERENCES "Email_Type" ("ID"),
        CONSTRAINT "LeadSource_EmailTemplate" FOREIGN KEY ("SouceID") REFERENCES "Lead_Source" ("ID")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260130100119_InitialMigration') THEN
    CREATE TABLE "Lead" (
        "ID" integer GENERATED ALWAYS AS IDENTITY,
        "CompanyName" text NOT NULL,
        "ContactName" text,
        "Website" text,
        "City" text NOT NULL,
        "SourceID" integer,
        "StatusID" integer,
        "AddedOn" date,
        "EmailID" text NOT NULL,
        CONSTRAINT "Primary_Key_Lead" PRIMARY KEY ("ID"),
        CONSTRAINT "Lead_source" FOREIGN KEY ("SourceID") REFERENCES "Lead_Source" ("ID"),
        CONSTRAINT "Lead_status" FOREIGN KEY ("StatusID") REFERENCES "Lead_Status" ("ID")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260130100119_InitialMigration') THEN
    CREATE TABLE "LeadEmailLog" (
        "ID" integer GENERATED ALWAYS AS IDENTITY,
        "LeadID" integer NOT NULL,
        "EmailTemplateID" integer NOT NULL,
        "NextEmailDate" date,
        "MailDate" date NOT NULL,
        CONSTRAINT "LeadEmailLog_pkey" PRIMARY KEY ("ID"),
        CONSTRAINT "Lead_EmailLog" FOREIGN KEY ("LeadID") REFERENCES "Lead" ("ID"),
        CONSTRAINT "Lead_TemplateID" FOREIGN KEY ("EmailTemplateID") REFERENCES "Email_Template" ("ID")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260130100119_InitialMigration') THEN
    CREATE INDEX "IX_Email_Template_EmailTypeID" ON "Email_Template" ("EmailTypeID");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260130100119_InitialMigration') THEN
    CREATE INDEX "IX_Email_Template_SouceID" ON "Email_Template" ("SouceID");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260130100119_InitialMigration') THEN
    CREATE INDEX "IX_Lead_SourceID" ON "Lead" ("SourceID");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260130100119_InitialMigration') THEN
    CREATE INDEX "IX_Lead_StatusID" ON "Lead" ("StatusID");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260130100119_InitialMigration') THEN
    CREATE INDEX "IX_LeadEmailLog_EmailTemplateID" ON "LeadEmailLog" ("EmailTemplateID");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260130100119_InitialMigration') THEN
    CREATE INDEX "IX_LeadEmailLog_LeadID" ON "LeadEmailLog" ("LeadID");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260130100119_InitialMigration') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260130100119_InitialMigration', '8.0.2');
    END IF;
END $EF$;
COMMIT;

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260130132825_SeedData') THEN
    INSERT INTO "Email_Type" ("ID", "Active", "Name")
    OVERRIDING SYSTEM VALUE
    VALUES (1, TRUE, 'Initial');
    INSERT INTO "Email_Type" ("ID", "Active", "Name")
    OVERRIDING SYSTEM VALUE
    VALUES (2, TRUE, 'Followup');
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260130132825_SeedData') THEN
    INSERT INTO "Lead_Source" ("ID", "Active", "Name")
    OVERRIDING SYSTEM VALUE
    VALUES (1, TRUE, 'Google Map');
    INSERT INTO "Lead_Source" ("ID", "Active", "Name")
    OVERRIDING SYSTEM VALUE
    VALUES (2, TRUE, 'HotFrog');
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260130132825_SeedData') THEN
    INSERT INTO "Lead_Status" ("ID", "Active", "Name")
    OVERRIDING SYSTEM VALUE
    VALUES (6, TRUE, 'New');
    INSERT INTO "Lead_Status" ("ID", "Active", "Name")
    OVERRIDING SYSTEM VALUE
    VALUES (7, TRUE, 'InitialSent');
    INSERT INTO "Lead_Status" ("ID", "Active", "Name")
    OVERRIDING SYSTEM VALUE
    VALUES (8, TRUE, 'FollowUpSent');
    INSERT INTO "Lead_Status" ("ID", "Active", "Name")
    OVERRIDING SYSTEM VALUE
    VALUES (9, TRUE, 'Replied');
    INSERT INTO "Lead_Status" ("ID", "Active", "Name")
    OVERRIDING SYSTEM VALUE
    VALUES (10, TRUE, 'Closed');
    INSERT INTO "Lead_Status" ("ID", "Active", "Name")
    OVERRIDING SYSTEM VALUE
    VALUES (11, TRUE, 'DoNotContact');
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260130132825_SeedData') THEN
    PERFORM setval(
        pg_get_serial_sequence('"Email_Type"', 'ID'),
        GREATEST(
            (SELECT MAX("ID") FROM "Email_Type") + 1,
            nextval(pg_get_serial_sequence('"Email_Type"', 'ID'))),
        false);
    PERFORM setval(
        pg_get_serial_sequence('"Lead_Source"', 'ID'),
        GREATEST(
            (SELECT MAX("ID") FROM "Lead_Source") + 1,
            nextval(pg_get_serial_sequence('"Lead_Source"', 'ID'))),
        false);
    PERFORM setval(
        pg_get_serial_sequence('"Lead_Status"', 'ID'),
        GREATEST(
            (SELECT MAX("ID") FROM "Lead_Status") + 1,
            nextval(pg_get_serial_sequence('"Lead_Status"', 'ID'))),
        false);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260130132825_SeedData') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260130132825_SeedData', '8.0.2');
    END IF;
END $EF$;
COMMIT;

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260216104253_addedEmailTemplates') THEN
    INSERT INTO "Email_Template" ("ID", "Active", "Body", "EmailTypeID", "Name", "SouceID", "Subject")
    OVERRIDING SYSTEM VALUE
    VALUES (1, TRUE, 'Hi@ContactName,<br/></br>I came across @FirmName while reviewing accounting firms in @City and wanted to reach out directly.I work with professional services firms to improve internal systems and streamline day-to - day operations.If improving internal efficiency or reducing manual effort is something you’re considering this year, would you be open to a short conversation to see if there’s a fit?<br/></br>Best regards,<br/>Krunal Pagdar<br/>Full-Stack Developer (.NET | Azure)', 1, 'GoogleMap - Initial', 1, 'Quick question about your internal workflows');
    INSERT INTO "Email_Template" ("ID", "Active", "Body", "EmailTypeID", "Name", "SouceID", "Subject")
    OVERRIDING SYSTEM VALUE
    VALUES (2, TRUE, 'Hi@ContactName,<br/><br/>I came across @FirmName while reviewing accounting firms listed on Hotfrog and wanted to reach out directly.I work with professional services firms to improve internal systems and streamline day-to - day operations.<br/><br/>If improving internal efficiency is something you’re exploring this year, I’d be happy to have a quick conversation.<br/><br/>Best regards,<br/>Krunal Pagdar<br/>Full-Stack Developer (.NET | Azure)', 1, 'HotFrog - Initial', 2, 'Quick question after finding @FirmName on Hotfrog');
    INSERT INTO "Email_Template" ("ID", "Active", "Body", "EmailTypeID", "Name", "SouceID", "Subject")
    OVERRIDING SYSTEM VALUE
    VALUES (3, TRUE, 'Hi@ContactName,<br/><br/>Just following up on my earlier note.< br />< br />If improving internal workflows or operational efficiency is on your roadmap, I’d be happy to explore whether there’s a good fit.<br/><br/>Best regards,<br/>Krunal Pagdar<br/>Full-Stack Developer (.NET | Azure)', 2, 'FollowUp', NULL, 'Following up');
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260216104253_addedEmailTemplates') THEN
    PERFORM setval(
        pg_get_serial_sequence('"Email_Template"', 'ID'),
        GREATEST(
            (SELECT MAX("ID") FROM "Email_Template") + 1,
            nextval(pg_get_serial_sequence('"Email_Template"', 'ID'))),
        false);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260216104253_addedEmailTemplates') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260216104253_addedEmailTemplates', '8.0.2');
    END IF;
END $EF$;
COMMIT;

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260217093242_UpdatedEmailTemplates') THEN
    UPDATE "Email_Template" SET "Body" = '<p>Hi@ContactName,</p>

    <p>I came across @FirmName while reviewing accounting firms in @City and wanted to reach out directly. I work with professional services firms to improve internal systems and streamline day-to-day operations.</p>

    <p>If improving internal efficiency or reducing manual effort is something you’re considering this year, would you be open to a short conversation to see if there’s a fit?</p>

    <p style="margin-top:25px;">
    Best regards,<br/>
    Krunal Pagdar<br/>
    Full-Stack Developer (.NET | Azure)
    </p>'
    WHERE "ID" = 1;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260217093242_UpdatedEmailTemplates') THEN
    UPDATE "Email_Template" SET "Body" = '<p>Hi@ContactName,</p>

    <p>I came across @FirmName while reviewing accounting firms listed on Hotfrog and wanted to reach out directly. I work with professional services firms to improve internal systems and streamline day-to-day operations.</p>

    <p>If improving internal efficiency is something you’re exploring this year, I’d be happy to have a quick conversation.</p>

    <p style="margin-top:25px;">
    Best regards,<br/>
    Krunal Pagdar<br/>
    Full-Stack Developer (.NET | Azure)
    </p>'
    WHERE "ID" = 2;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260217093242_UpdatedEmailTemplates') THEN
    UPDATE "Email_Template" SET "Body" = '<p>Hi@ContactName,</p>

    <p>Just following up on my earlier note.</p>

    <p>If improving internal workflows or operational efficiency is on your roadmap, I’d be happy to explore whether there’s a good fit.</p>

    <p style="margin-top:25px;">
    Best regards,<br/>
    Krunal Pagdar<br/>
    Full-Stack Developer (.NET | Azure)
    </p>'
    WHERE "ID" = 3;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260217093242_UpdatedEmailTemplates') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260217093242_UpdatedEmailTemplates', '8.0.2');
    END IF;
END $EF$;
COMMIT;

