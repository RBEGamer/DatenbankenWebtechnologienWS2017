-- SQL stub, nutzen Sie dieses Snippet als Ausgangspunkt und erweitern und ändern Sie es so,
-- dass es die Aufgaben aus Paket 2 löst



-- --------------------------------------------------------
-- CREATE statements
-- Schemaelemente definieren
CREATE DATABASE IF NOT EXISTS `dbwt_praktikum`;
USE `dbwt_praktikum`;
-- --------------------------------------------------------



-- --------------------------------------------------------
-- DROP statements
-- zuvor angelegte Tabellen löschen, niemals die ganze Datenbank löschen, nur Tabellen
-- mit DROP werden sowohl die Schemadefinitionen der Tabelle als auch die in ihr gespeicherten Daten
DROP TABLE IF EXISTS `Kategorie`;
DROP TABLE IF EXISTS `ProduktZutat`;
DROP TABLE IF EXISTS `Preis`;
DROP TABLE IF EXISTS `Bestellung`;
DROP TABLE IF EXISTS `Bewertung`;
DROP TABLE IF EXISTS `Bild`;
DROP TABLE IF EXISTS `Gast`;
DROP TABLE IF EXISTS `Mitarbeiter`;
DROP TABLE IF EXISTS `Student`;
DROP TABLE IF EXISTS `FE-Nutzer`;


DROP TABLE IF EXISTS `Produkt`;

DROP TABLE IF EXISTS `Zutat`;
-- --------------------------------------------------------


-- Tabelle Bestellung
-- Relation: keine
CREATE TABLE IF NOT EXISTS `Bestellung` (
  `Id` int AUTO_INCREMENT NOT NULL,
  `Zeitpunkt` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `FK_Produkt` mediumint NOT NULL,
  `FK_FE-Nutzer` int NOT NULL,
  PRIMARY KEY (`Id`)
);


-- Tabelle Bewertung
-- Relation: keine
CREATE TABLE IF NOT EXISTS `Bewertung` (
  `Id` mediumint AUTO_INCREMENT NOT NULL,
  `FK_FE-Nutzer` int NOT NULL,
  `Zeit` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `Note` tinyint NOT NULL,
  `Bemerkung` varchar(160) NOT NULL DEFAULT '-- keine weiteren Angaben --',
  `Views` int NOT NULL DEFAULT '0',
  `FK_Produkt` MEDIUMINT DEFAULT NULL,
  PRIMARY KEY (`Id`)
);


-- Tabelle Bild
-- Relation: keine
CREATE TABLE IF NOT EXISTS `Bild` (
  `Id` mediumint AUTO_INCREMENT NOT NULL,
  `Alt-Text` varchar(100) NOT NULL DEFAULT '-- no alt text --',
  `Binärdaten` blob NOT NULL,
  `Titel` varchar(64) NOT NULL,
  `BildunterSchrift` varchar(80) NOT NULL,
  PRIMARY KEY (`Id`)
);


-- Tabelle FE-Nutzer
-- Relation: keine
CREATE TABLE IF NOT EXISTS `FE-Nutzer` (
  `Nr` int AUTO_INCREMENT NOT NULL,
  `Aktiv` BOOLEAN NOT NULL,
  `Vorname` varchar(64) NOT NULL,
  `Nachname` varchar(64) NOT NULL,
  `Loginname` varchar(64) NOT NULL,
  `Email` varchar(64) NOT NULL,
  `Hash` varchar(24) NOT NULL,
  `Salt` varchar(32) NOT NULL,
  `Algorythmus` enum('sha1','sha256') NOT NULL,
  `Strech` int unsigned NOT NULL,
  `LetzterLogin` timestamp NULL DEFAULT NULL,
  `Anlegedatum` date NOT NULL,
  `Benutzerrolle` ENUM('Student','Mitarbeiter','Gast','') NOT NULL, -- ADDED FOR ID
  PRIMARY KEY (`Nr`)
);


-- Tabelle Gast
-- Relation: keine
CREATE TABLE IF NOT EXISTS `Gast` (
  `Id` int AUTO_INCREMENT NOT NULL,
  `Grund` varchar(100) NOT NULL DEFAULT 'Tagesgast',
  `Ablauf` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `FK_Fe-Nutzer` int NOT NULL UNIQUE,
  CONSTRAINT `GastNutzer` FOREIGN KEY (`FK_Fe-Nutzer`) REFERENCES `FE-Nutzer` (`Nr`) ON DELETE CASCADE,
  PRIMARY KEY (`Id`)
);


-- Tabelle Kategorie
-- Relation: keine
CREATE TABLE IF NOT EXISTS `Kategorie` (
  `Id` mediumint AUTO_INCREMENT NOT NULL,
  `Bezeichnung` varchar(100) DEFAULT NULL,
  `Oberkategorie` MEDIUMINT DEFAULT NULL,
  PRIMARY KEY (`Id`),
  CONSTRAINT `OberKat` FOREIGN KEY (`Oberkategorie`) REFERENCES `Kategorie` (`Id`)
);


-- Tabelle Mitarbeiter
-- Relation: keine
CREATE TABLE IF NOT EXISTS `Mitarbeiter` (
  `Id` int AUTO_INCREMENT NOT NULL,
  `Telefonnummer` varchar(16) NOT NULL,
  `MA-Nummer` int NOT NULL,
  `Büro` varchar(80) NOT NULL DEFAULT 'kein Büro',
  `FK_Fe-Nutzer` int NOT NULL UNIQUE,
  CONSTRAINT `MitarbeiterNutzer` FOREIGN KEY (`FK_Fe-Nutzer`) REFERENCES `FE-Nutzer` (`Nr`) ON DELETE CASCADE,
  PRIMARY KEY (`Id`)
);


-- Tabelle Produkt
-- Relation: keine
CREATE TABLE IF NOT EXISTS `Produkt` (
  `Id` mediumint AUTO_INCREMENT NOT NULL,
  `Beschreibung` varchar(100) NOT NULL,
  `FK_Kategorie` mediumint NOT NULL,
  `Gastbetrag` float NOT NULL,
  `Studentbetrag` float NOT NULL,
  `Mitarbeiterbetrag` float NOT NULL,
  `FK_Bild` mediumint NOT NULL,
  `FK_Preis` mediumint NOT NULL,
  PRIMARY KEY (`Id`)
);


-- Tabelle Student
-- Relation: keine
CREATE TABLE IF NOT EXISTS `Student` (
  `Id` mediumint AUTO_INCREMENT NOT NULL,
  `Matrikelnummer` int NOT NULL,
  `Studiengang` varchar(32) NOT NULL,
  `FK_Fe-Nutzer` int NOT NULL UNIQUE,
  CONSTRAINT `StudentNutzer` FOREIGN KEY (`FK_Fe-Nutzer`) REFERENCES `FE-Nutzer` (`Nr`) ON DELETE CASCADE,
  PRIMARY KEY (`Id`)
);


-- Tabelle Zutat
-- Relation: keine
CREATE TABLE IF NOT EXISTS `Zutat` (
  `Id` mediumint AUTO_INCREMENT NOT NULL,
  `Name` varchar(32) NOT NULL,
  `Beschreibung` varchar(100) DEFAULT '-- Beschreibung der Zutat --',
  `Vegan` BOOLEAN DEFAULT '0',
  `Vegetarisch` BOOLEAN DEFAULT '0',
  `Bio` BOOLEAN DEFAULT '0',
  `Glutenfrei` BOOLEAN DEFAULT '0',
  PRIMARY KEY (`Id`)
);


-- Tabelle Bewertung
-- Relation: keine
CREATE TABLE IF NOT EXISTS `ProduktZutat` (
  `FK_Produkt` mediumint NOT NULL,
  `FK_Zutat` mediumint NOT NULL,
  CONSTRAINT `zut_id` FOREIGN KEY (`FK_Zutat`) REFERENCES `Zutat` (`Id`),
  CONSTRAINT `prod_id` FOREIGN KEY (`FK_Produkt`) REFERENCES `Produkt` (`Id`),
  PRIMARY KEY (`FK_Produkt`,`FK_Zutat`)
);


-- Tabelle Preis
-- Relation: keine
CREATE TABLE IF NOT EXISTS `Preis` (
  `Id` mediumint NOT NULL AUTO_INCREMENT,
  `Gastbetrag` decimal(4,2) NOT NULL,
  `Studentenbetrag` decimal(4,2) NOT NULL,
  `Mitarbeiterbetrag` decimal(4,2) NOT NULL,
  `FK_Produkt` mediumint NOT NULL,
  PRIMARY KEY (`Id`),
  CONSTRAINT `produktid_fuer_preis` FOREIGN KEY (`id`) REFERENCES `Produkt` (`Id`) ON DELETE CASCADE
);
--
--
-- INSERT statements
-- wenn alle Tabellen vollständig definiert sind, fügen Sie Beispieldaten in die Benutzertabellen ein (Aufgabe 2.4)
-- hier ein Insert-Beispiel für die im Stub definierte Tabelle Kategorie (wenn Sie das Schema ändern, kann es auch dazu kommen, dass Sie Änderungen in den INSERT Statements vornehmen müssen)


-- da ID automatisch vergeben wird, und die Oberkategorie per Default NULL ist, muss pro Eintrag nur das Attribut "Bezeichnung" angegeben werden
-- ausserdem können Sie in einem INSERT Statement mehrere Entitäten zum speichern angeben

INSERT INTO `Bild` (`Id`, `Alt-Text`, `Binärdaten`, `Titel`, `BildunterSchrift`) VALUES
(2, '-- no alt text --', 0x89504e470d0a1a0a0000000d4948445200000010000000100804000000b5fa37ea0000000467414d410000b18f0bfc6105000000206348524d00007a26000080840000fa00000080e8000075300000ea6000003a98000017709cba513c00000002624b47440000aa8d2332000000097048597300000dd700000dd70142289b780000000774494d4507e10b110d1e012527ff76000000ce4944415428cfa5d1a18a02011485e16f0c26838251c11718c420ecce5b984d767d19c132cd30c16e12b3bb60144ca261b0ca6a51109c0db2e2b0983ce9c23df0df7b0eef2a784c651f3e4558f8f2ede7d95635759339999b3bc9dc4c55ffd691d4415fa8000a427d07a90806ae961aca7a622b2bb19eb286a5ab013343452d5b999d446227b3d5523434bb433aced6c2c74da1b5b3ce9d077589b68b89546ae2a22d51cf3fdc74b43732b277d4fc9f482856110854c44fb89c6a36366aaf73edca64baaf0d256363a5b70bcce9176c4a3cd024502c970000002574455874646174653a63726561746500323031372d31312d31375431333a33303a30312b30313a303070966ecf0000002574455874646174653a6d6f6469667900323031372d31312d31375431333a33303a30312b30313a303001cbd6730000001974455874536f667477617265007777772e696e6b73636170652e6f72679bee3c1a0000000049454e44ae426082, 'test icon', 'icon'),
(3, '-- no alt text --', 0x89504e470d0a1a0a0000000d4948445200000010000000100804000000b5fa37ea0000000467414d410000b18f0bfc6105000000206348524d00007a26000080840000fa00000080e8000075300000ea6000003a98000017709cba513c00000002624b47440000aa8d2332000000097048597300000dd700000dd70142289b780000000774494d4507e10b110d1e012527ff76000000ce4944415428cfa5d1a18a02011485e16f0c26838251c11718c420ecce5b984d767d19c132cd30c16e12b3bb60144ca261b0ca6a51109c0db2e2b0983ce9c23df0df7b0eef2a784c651f3e4558f8f2ede7d95635759339999b3bc9dc4c55ffd691d4415fa8000a427d07a90806ae961aca7a622b2bb19eb286a5ab013343452d5b999d446227b3d5523434bb433aced6c2c74da1b5b3ce9d077589b68b89546ae2a22d51cf3fdc74b43732b277d4fc9f482856110854c44fb89c6a36366aaf73edca64baaf0d256363a5b70bcce9176c4a3cd024502c970000002574455874646174653a63726561746500323031372d31312d31375431333a33303a30312b30313a303070966ecf0000002574455874646174653a6d6f6469667900323031372d31312d31375431333a33303a30312b30313a303001cbd6730000001974455874536f667477617265007777772e696e6b73636170652e6f72679bee3c1a0000000049454e44ae426082, 'test icon', 'icon');


INSERT INTO `Kategorie` (`Id`, `Bezeichnung`, `Oberkategorie`) VALUES
(2, 'GOOD LIFE FOOD', NULL);








INSERT INTO `FE-Nutzer` (`Nr`, `Aktiv`, `Vorname`, `Nachname`, `Loginname`, `Email`, `Hash`, `Salt`, `Algorythmus`, `Strech`, `LetzterLogin`, `Anlegedatum`,`Benutzerrolle`) VALUES
(1, 1, 'Marcel', 'Ochsendorf', 'marcelochsendorf', 'marcel.ochsendorf@gmail.com', '123332123456789098765432', '68687', 'sha1', 6, '2017-11-16 00:00:00', '2017-11-05', 'Mitarbeiter');

INSERT INTO `FE-Nutzer` (`Nr`, `Aktiv`, `Vorname`, `Nachname`, `Loginname`, `Email`, `Hash`, `Salt`, `Algorythmus`, `Strech`, `LetzterLogin`, `Anlegedatum`,`Benutzerrolle`) VALUES
(2, 1, 'Marcel', 'Ochsendorf', 'marcelochsendorf', 'marcel.ochsendorf@gmail.com', '123332123456789098765432', '68687', 'sha1', 6, '2017-11-16 00:00:00', '2017-11-05', 'Gast');

INSERT INTO `Gast` (`Id`, `Grund`, `Ablauf`, `FK_Fe-Nutzer`) VALUES (NULL, 'Tagesgast', CURRENT_TIMESTAMP, '2');

INSERT INTO `Mitarbeiter` (`Id`, `Telefonnummer`, `MA-Nummer`, `Büro`, `FK_Fe-Nutzer`) VALUES (NULL, '123123', '12321', 'kein Büro', '1');


INSERT INTO `Zutat` (`Id`, `Name`, `Beschreibung`, `Vegan`, `Vegetarisch`, `Bio`, `Glutenfrei`) VALUES(1, 'Pfeffer', 'scharf', 1, 1, 0, 1), (2, 'Wurst', 'Salami', 0, 0, 1, 0);


INSERT INTO `Produkt` (`Id`, `Beschreibung`, `FK_Kategorie`, `Gastbetrag`, `Studentbetrag`, `Mitarbeiterbetrag`, `FK_Bild`,`FK_Preis`) VALUES
(1, 'Salami Salat', 2, 3, 1, 9, 2,1);


-- das erstelle produkt SamaiSalat enhält beide zutaten
INSERT INTO `ProduktZutat` (`FK_Produkt`, `FK_Zutat`) VALUES (1, 1), (1, 2);


INSERT INTO `Bestellung` (`Id`, `Zeitpunkt`, `FK_Produkt`, `FK_FE-Nutzer`) VALUES
(1, '2017-11-17 12:50:44', 1, 1);


INSERT INTO `Bewertung` (`Id`, `FK_FE-Nutzer`, `Zeit`, `Note`, `Bemerkung`, `Views`, `FK_Produkt`) VALUES
(1, 1, '2017-11-17 13:01:10', 3, 'joar war ganz gut', 0, 1);


INSERT INTO `Preis` (`Id`, `Gastbetrag`, `Studentenbetrag`, `Mitarbeiterbetrag`, `FK_Produkt`) VALUES ('1', '1.99', '2.99', '3.99', '1');












-- --------------------------------------------------------
