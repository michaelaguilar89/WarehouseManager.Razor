-- Database: WareHouseDB

-- DROP DATABASE IF EXISTS "WareHouseDB";

CREATE DATABASE "WareHouseDB"
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'Spanish_Spain.1252'
    LC_CTYPE = 'Spanish_Spain.1252'
    LOCALE_PROVIDER = 'libc'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;

-- Table: public.AspNetUsers

-- DROP TABLE IF EXISTS public."AspNetUsers";

CREATE TABLE IF NOT EXISTS public."AspNetUsers"
(
    "Id" text COLLATE pg_catalog."default" NOT NULL,
    "CreationTime" timestamp with time zone NOT NULL,
    "LastLoginTime" timestamp with time zone NOT NULL,
    "IsActived" boolean NOT NULL,
    "UserName" character varying(256) COLLATE pg_catalog."default",
    "NormalizedUserName" character varying(256) COLLATE pg_catalog."default",
    "Email" character varying(256) COLLATE pg_catalog."default",
    "NormalizedEmail" character varying(256) COLLATE pg_catalog."default",
    "EmailConfirmed" boolean NOT NULL,
    "PasswordHash" text COLLATE pg_catalog."default",
    "SecurityStamp" text COLLATE pg_catalog."default",
    "ConcurrencyStamp" text COLLATE pg_catalog."default",
    "PhoneNumber" text COLLATE pg_catalog."default",
    "PhoneNumberConfirmed" boolean NOT NULL,
    "TwoFactorEnabled" boolean NOT NULL,
    "LockoutEnd" timestamp with time zone,
    "LockoutEnabled" boolean NOT NULL,
    "AccessFailedCount" integer NOT NULL,
    CONSTRAINT "PK_AspNetUsers" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."AspNetUsers"
    OWNER to postgres;
-- Index: EmailIndex

-- DROP INDEX IF EXISTS public."EmailIndex";

CREATE INDEX IF NOT EXISTS "EmailIndex"
    ON public."AspNetUsers" USING btree
    ("NormalizedEmail" COLLATE pg_catalog."default" ASC NULLS LAST)
    TABLESPACE pg_default;
-- Index: UserNameIndex

-- DROP INDEX IF EXISTS public."UserNameIndex";

CREATE UNIQUE INDEX IF NOT EXISTS "UserNameIndex"
    ON public."AspNetUsers" USING btree
    ("NormalizedUserName" COLLATE pg_catalog."default" ASC NULLS LAST)
    TABLESPACE pg_default;


-- Table: public.AspNetUserTokens

-- DROP TABLE IF EXISTS public."AspNetUserTokens";

CREATE TABLE IF NOT EXISTS public."AspNetUserTokens"
(
    "UserId" text COLLATE pg_catalog."default" NOT NULL,
    "LoginProvider" text COLLATE pg_catalog."default" NOT NULL,
    "Name" text COLLATE pg_catalog."default" NOT NULL,
    "Value" text COLLATE pg_catalog."default",
    CONSTRAINT "PK_AspNetUserTokens" PRIMARY KEY ("UserId", "LoginProvider", "Name"),
    CONSTRAINT "FK_AspNetUserTokens_AspNetUsers_UserId" FOREIGN KEY ("UserId")
        REFERENCES public."AspNetUsers" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."AspNetUserTokens"
    OWNER to postgres;
-- Table: public.AspNetUserRoles

-- DROP TABLE IF EXISTS public."AspNetUserRoles";

CREATE TABLE IF NOT EXISTS public."AspNetUserRoles"
(
    "UserId" text COLLATE pg_catalog."default" NOT NULL,
    "RoleId" text COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "PK_AspNetUserRoles" PRIMARY KEY ("UserId", "RoleId"),
    CONSTRAINT "FK_AspNetUserRoles_AspNetRoles_RoleId" FOREIGN KEY ("RoleId")
        REFERENCES public."AspNetRoles" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE,
    CONSTRAINT "FK_AspNetUserRoles_AspNetUsers_UserId" FOREIGN KEY ("UserId")
        REFERENCES public."AspNetUsers" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."AspNetUserRoles"
    OWNER to postgres;
-- Index: IX_AspNetUserRoles_RoleId

-- DROP INDEX IF EXISTS public."IX_AspNetUserRoles_RoleId";

CREATE INDEX IF NOT EXISTS "IX_AspNetUserRoles_RoleId"
    ON public."AspNetUserRoles" USING btree
    ("RoleId" COLLATE pg_catalog."default" ASC NULLS LAST)
    TABLESPACE pg_default;

-- Table: public.AspNetUserLogins

-- DROP TABLE IF EXISTS public."AspNetUserLogins";

CREATE TABLE IF NOT EXISTS public."AspNetUserLogins"
(
    "LoginProvider" text COLLATE pg_catalog."default" NOT NULL,
    "ProviderKey" text COLLATE pg_catalog."default" NOT NULL,
    "ProviderDisplayName" text COLLATE pg_catalog."default",
    "UserId" text COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "PK_AspNetUserLogins" PRIMARY KEY ("LoginProvider", "ProviderKey"),
    CONSTRAINT "FK_AspNetUserLogins_AspNetUsers_UserId" FOREIGN KEY ("UserId")
        REFERENCES public."AspNetUsers" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."AspNetUserLogins"
    OWNER to postgres;
-- Index: IX_AspNetUserLogins_UserId

-- DROP INDEX IF EXISTS public."IX_AspNetUserLogins_UserId";

CREATE INDEX IF NOT EXISTS "IX_AspNetUserLogins_UserId"
    ON public."AspNetUserLogins" USING btree
    ("UserId" COLLATE pg_catalog."default" ASC NULLS LAST)
    TABLESPACE pg_default;

-- Table: public.AspNetUserClaims

-- DROP TABLE IF EXISTS public."AspNetUserClaims";

CREATE TABLE IF NOT EXISTS public."AspNetUserClaims"
(
    "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "UserId" text COLLATE pg_catalog."default" NOT NULL,
    "ClaimType" text COLLATE pg_catalog."default",
    "ClaimValue" text COLLATE pg_catalog."default",
    CONSTRAINT "PK_AspNetUserClaims" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_AspNetUserClaims_AspNetUsers_UserId" FOREIGN KEY ("UserId")
        REFERENCES public."AspNetUsers" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."AspNetUserClaims"
    OWNER to postgres;
-- Index: IX_AspNetUserClaims_UserId

-- DROP INDEX IF EXISTS public."IX_AspNetUserClaims_UserId";

CREATE INDEX IF NOT EXISTS "IX_AspNetUserClaims_UserId"
    ON public."AspNetUserClaims" USING btree
    ("UserId" COLLATE pg_catalog."default" ASC NULLS LAST)
    TABLESPACE pg_default;

-- Table: public.AspNetRoles

-- DROP TABLE IF EXISTS public."AspNetRoles";

CREATE TABLE IF NOT EXISTS public."AspNetRoles"
(
    "Id" text COLLATE pg_catalog."default" NOT NULL,
    "Name" character varying(256) COLLATE pg_catalog."default",
    "NormalizedName" character varying(256) COLLATE pg_catalog."default",
    "ConcurrencyStamp" text COLLATE pg_catalog."default",
    CONSTRAINT "PK_AspNetRoles" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."AspNetRoles"
    OWNER to postgres;
-- Index: RoleNameIndex

-- DROP INDEX IF EXISTS public."RoleNameIndex";

CREATE UNIQUE INDEX IF NOT EXISTS "RoleNameIndex"
    ON public."AspNetRoles" USING btree
    ("NormalizedName" COLLATE pg_catalog."default" ASC NULLS LAST)
    TABLESPACE pg_default;

-- Table: public.AspNetRoleClaims

-- DROP TABLE IF EXISTS public."AspNetRoleClaims";

CREATE TABLE IF NOT EXISTS public."AspNetRoleClaims"
(
    "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "RoleId" text COLLATE pg_catalog."default" NOT NULL,
    "ClaimType" text COLLATE pg_catalog."default",
    "ClaimValue" text COLLATE pg_catalog."default",
    CONSTRAINT "PK_AspNetRoleClaims" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_AspNetRoleClaims_AspNetRoles_RoleId" FOREIGN KEY ("RoleId")
        REFERENCES public."AspNetRoles" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."AspNetRoleClaims"
    OWNER to postgres;
-- Index: IX_AspNetRoleClaims_RoleId

-- DROP INDEX IF EXISTS public."IX_AspNetRoleClaims_RoleId";

CREATE INDEX IF NOT EXISTS "IX_AspNetRoleClaims_RoleId"
    ON public."AspNetRoleClaims" USING btree
    ("RoleId" COLLATE pg_catalog."default" ASC NULLS LAST)
    TABLESPACE pg_default;

-- Table: public.Productions

-- DROP TABLE IF EXISTS public."Productions";

CREATE TABLE IF NOT EXISTS public."Productions"
(
    "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "ProductName" text COLLATE pg_catalog."default" NOT NULL,
    "ProductNameHash" text COLLATE pg_catalog."default" NOT NULL,
    "Batch" text COLLATE pg_catalog."default" NOT NULL,
    "BatchHash" text COLLATE pg_catalog."default" NOT NULL,
    "StoreId" integer NOT NULL,
    "Quantity" numeric(10,2) NOT NULL,
    "Tank" text COLLATE pg_catalog."default" NOT NULL,
    "FinalLevel" text COLLATE pg_catalog."default" NOT NULL,
    "CreationTime" timestamp with time zone NOT NULL,
    "ModificacionTime" timestamp with time zone,
    "UserIdCreation" text COLLATE pg_catalog."default" NOT NULL,
    "UserIdModification" text COLLATE pg_catalog."default",
    "Comments" text COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "PK_Productions" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Productions_AspNetUsers_UserIdCreation" FOREIGN KEY ("UserIdCreation")
        REFERENCES public."AspNetUsers" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE,
    CONSTRAINT "FK_Productions_AspNetUsers_UserIdModification" FOREIGN KEY ("UserIdModification")
        REFERENCES public."AspNetUsers" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "FK_Productions_Stores_StoreId" FOREIGN KEY ("StoreId")
        REFERENCES public."Stores" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Productions"
    OWNER to postgres;
-- Index: IX_Productions_StoreId

-- DROP INDEX IF EXISTS public."IX_Productions_StoreId";

CREATE INDEX IF NOT EXISTS "IX_Productions_StoreId"
    ON public."Productions" USING btree
    ("StoreId" ASC NULLS LAST)
    TABLESPACE pg_default;
-- Index: IX_Productions_UserIdCreation

-- DROP INDEX IF EXISTS public."IX_Productions_UserIdCreation";

CREATE INDEX IF NOT EXISTS "IX_Productions_UserIdCreation"
    ON public."Productions" USING btree
    ("UserIdCreation" COLLATE pg_catalog."default" ASC NULLS LAST)
    TABLESPACE pg_default;
-- Index: IX_Productions_UserIdModification

-- DROP INDEX IF EXISTS public."IX_Productions_UserIdModification";

CREATE INDEX IF NOT EXISTS "IX_Productions_UserIdModification"
    ON public."Productions" USING btree
    ("UserIdModification" COLLATE pg_catalog."default" ASC NULLS LAST)
    TABLESPACE pg_default;


-- Table: public.Stores

-- DROP TABLE IF EXISTS public."Stores";

CREATE TABLE IF NOT EXISTS public."Stores"
(
    "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "ProductName" text COLLATE pg_catalog."default" NOT NULL,
    "ProductNameHash" text COLLATE pg_catalog."default" NOT NULL,
    "Batch" text COLLATE pg_catalog."default" NOT NULL,
    "BatchHash" text COLLATE pg_catalog."default" NOT NULL,
    "TotalQuantity" numeric(10,2) NOT NULL,
    "ActualQuantity" numeric(10,2) NOT NULL,
    "CreationTime" timestamp with time zone NOT NULL,
    "Description" text COLLATE pg_catalog."default" NOT NULL,
    "Comments" text COLLATE pg_catalog."default" NOT NULL,
    "ModificationAt" timestamp with time zone,
    "UserIdCreation" text COLLATE pg_catalog."default" NOT NULL,
    "UserIdModification" text COLLATE pg_catalog."default",
    CONSTRAINT "PK_Stores" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Stores_AspNetUsers_UserIdCreation" FOREIGN KEY ("UserIdCreation")
        REFERENCES public."AspNetUsers" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE,
    CONSTRAINT "FK_Stores_AspNetUsers_UserIdModification" FOREIGN KEY ("UserIdModification")
        REFERENCES public."AspNetUsers" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Stores"
    OWNER to postgres;
-- Index: IX_Stores_UserIdCreation

-- DROP INDEX IF EXISTS public."IX_Stores_UserIdCreation";

CREATE INDEX IF NOT EXISTS "IX_Stores_UserIdCreation"
    ON public."Stores" USING btree
    ("UserIdCreation" COLLATE pg_catalog."default" ASC NULLS LAST)
    TABLESPACE pg_default;
-- Index: IX_Stores_UserIdModification

-- DROP INDEX IF EXISTS public."IX_Stores_UserIdModification";

CREATE INDEX IF NOT EXISTS "IX_Stores_UserIdModification"
    ON public."Stores" USING btree
    ("UserIdModification" COLLATE pg_catalog."default" ASC NULLS LAST)
    TABLESPACE pg_default;
