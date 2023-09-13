CREATE TABLE Benutzer (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    LoginID VARCHAR UNIQUE,
    Passwort VARCHAR,
    IstAdmin BOOLEAN
);

CREATE TABLE Schueler (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name VARCHAR,
    BenutzerId INTEGER,
    CONSTRAINT fk_Schueler_Benutzer FOREIGN KEY (BenutzerId) REFERENCES Benutzer(Id)
);

CREATE TABLE Lehrer (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    BenutzerId INTEGER,
    CONSTRAINT fk_Lehrer_Benutzer FOREIGN KEY (BenutzerId) REFERENCES Benutzer(Id)
);

CREATE TABLE Klasse (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name VARCHAR
);

CREATE TABLE Einschreibung (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    SchuelerId INTEGER,
    KlasseId INTEGER,
    CONSTRAINT uq_Einschreibung_Klasse_Schueler UNIQUE (SchuelerId, KlasseId),
    CONSTRAINT fk_Einschreibung_Schueler FOREIGN KEY (SchuelerId) REFERENCES Schueler(Id),
    CONSTRAINT fk_Einschreibung_Klasse FOREIGN KEY (KlasseId) REFERENCES Klasse(Id)
);

CREATE TABLE Kurs (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    LehrerId INTEGER,
    KlasseId INTEGER,
    Name VARCHAR,
    Schuljahr VARCHAR,
    CONSTRAINT fk_Kurs_Lehrer FOREIGN KEY (LehrerId) REFERENCES Lehrer(Id),
    CONSTRAINT fk_Kurs_Klasse FOREIGN KEY (KlasseId) REFERENCES Klasse(Id)
);

CREATE TABLE NotenTyp (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name VARCHAR
);

CREATE TABLE Note (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    SchuelerId INTEGER,
    KursId INTEGER,
    NotenTypId INTEGER,
    CONSTRAINT fk_Note_Schueler FOREIGN KEY (SchuelerId) REFERENCES Schueler(Id),
    CONSTRAINT fk_Note_Kurs FOREIGN KEY (KursId) REFERENCES Kurs(Id),
    CONSTRAINT fk_Note_NotenTyp FOREIGN KEY (NotenTypId) REFERENCES NotenTyp(Id)
);