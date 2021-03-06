
CREATE TABLE
IF NOT EXISTS "__EFMigrationsHistory"
(
    "MigrationId" character varying
(150) NOT NULL,
    "ProductVersion" character varying
(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY
("MigrationId")
);

START TRANSACTION;

CREATE TABLE "Escola"
(
    "Id" uuid NOT NULL,
    "Nome" character varying(500) NOT NULL,
    "NumeroInep" integer NOT NULL,
    CONSTRAINT "PK_Escola" PRIMARY KEY ("Id")
);

CREATE TABLE "Turma"
(
    "Id" uuid NOT NULL,
    "EscolaId" uuid NOT NULL,
    "Ano" integer NOT NULL,
    "Curso" character varying(255) NULL,
    "Serie" character varying(255) NULL,
    "Nome" character varying(255) NULL,
    CONSTRAINT "PK_Turma" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Turma_Escola_Id" FOREIGN KEY ("Id") REFERENCES "Escola" ("Id") ON DELETE CASCADE
);

INSERT INTO "__EFMigrationsHistory"
    ("MigrationId", "ProductVersion")
VALUES
    ('20201024023658_inicial', '5.0.0-rc.2.20475.6');

COMMIT;

START TRANSACTION;

ALTER TABLE "Turma" DROP CONSTRAINT "FK_Turma_Escola_Id";

CREATE INDEX "IX_Turma_EscolaId" ON "Turma" ("EscolaId");

ALTER TABLE "Turma" ADD CONSTRAINT "FK_Turma_Escola_EscolaId" FOREIGN KEY ("EscolaId") REFERENCES "Escola" ("Id") ON DELETE CASCADE;

INSERT INTO "__EFMigrationsHistory"
    ("MigrationId", "ProductVersion")
VALUES
    ('20201024164744_corrigeFkTurmaEscola', '5.0.0-rc.2.20475.6');

COMMIT;

START TRANSACTION;

ALTER TABLE "Turma" ALTER COLUMN "Serie"
SET
NOT NULL;
ALTER TABLE "Turma" ALTER COLUMN "Serie"
SET
DEFAULT '';

ALTER TABLE "Turma" ALTER COLUMN "Nome"
SET
NOT NULL;
ALTER TABLE "Turma" ALTER COLUMN "Nome"
SET
DEFAULT '';

ALTER TABLE "Turma" ALTER COLUMN "Curso"
SET
NOT NULL;
ALTER TABLE "Turma" ALTER COLUMN "Curso"
SET
DEFAULT '';

INSERT INTO "__EFMigrationsHistory"
    ("MigrationId", "ProductVersion")
VALUES
    ('20201024171626_camposObrigatoriosTurma', '5.0.0-rc.2.20475.6');

COMMIT;


