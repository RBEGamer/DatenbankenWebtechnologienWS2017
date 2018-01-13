-- phpMyAdmin SQL Dump
-- version 4.2.12deb2+deb8u2
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Erstellungszeit: 13. Jan 2018 um 12:43
-- Server Version: 5.5.58-0+deb8u1
-- PHP-Version: 5.6.30-0+deb8u1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Datenbank: `dbwt_praktikum`
--

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `Bestellung`
--

CREATE TABLE IF NOT EXISTS `Bestellung` (
`Id` int(11) NOT NULL,
  `Zeitpunkt` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `FK_Produkt` mediumint(9) NOT NULL,
  `FK_FE-Nutzer` int(11) NOT NULL
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

--
-- Daten für Tabelle `Bestellung`
--

INSERT INTO `Bestellung` (`Id`, `Zeitpunkt`, `FK_Produkt`, `FK_FE-Nutzer`) VALUES
(1, '2017-11-17 12:50:44', 1, 1);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `Bewertung`
--

CREATE TABLE IF NOT EXISTS `Bewertung` (
`Id` mediumint(9) NOT NULL,
  `FK_FE-Nutzer` int(11) NOT NULL,
  `Zeit` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `Note` tinyint(4) NOT NULL,
  `Bemerkung` varchar(160) NOT NULL DEFAULT '-- keine weiteren Angaben --',
  `Views` int(11) NOT NULL DEFAULT '0',
  `FK_Produkt` mediumint(9) DEFAULT NULL
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

--
-- Daten für Tabelle `Bewertung`
--

INSERT INTO `Bewertung` (`Id`, `FK_FE-Nutzer`, `Zeit`, `Note`, `Bemerkung`, `Views`, `FK_Produkt`) VALUES
(1, 1, '2017-11-17 13:01:10', 3, 'joar war ganz gut', 0, 1);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `Bild`
--

CREATE TABLE IF NOT EXISTS `Bild` (
`Id` mediumint(9) NOT NULL,
  `Alt-Text` varchar(100) NOT NULL DEFAULT '-- no alt text --',
  `Binärdaten` blob NOT NULL,
  `Titel` varchar(64) NOT NULL,
  `BildunterSchrift` varchar(80) NOT NULL,
  `IconBildPfad` varchar(32) NOT NULL,
  `IconBildPfadGray` text NOT NULL,
  `DetailsBildPfad` varchar(32) NOT NULL
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

--
-- Daten für Tabelle `Bild`
--

INSERT INTO `Bild` (`Id`, `Alt-Text`, `Binärdaten`, `Titel`, `BildunterSchrift`, `IconBildPfad`, `IconBildPfadGray`, `DetailsBildPfad`) VALUES
(2, '-- no alt text --', 0x89504e470d0a1a0a0000000d4948445200000010000000100804000000b5fa37ea0000000467414d410000b18f0bfc6105000000206348524d00007a26000080840000fa00000080e8000075300000ea6000003a98000017709cba513c00000002624b47440000aa8d2332000000097048597300000dd700000dd70142289b780000000774494d4507e10b110d1e012527ff76000000ce4944415428cfa5d1a18a02011485e16f0c26838251c11718c420ecce5b984d767d19c132cd30c16e12b3bb60144ca261b0ca6a51109c0db2e2b0983ce9c23df0df7b0eef2a784c651f3e4558f8f2ede7d95635759339999b3bc9dc4c55ffd691d4415fa8000a427d07a90806ae961aca7a622b2bb19eb286a5ab013343452d5b999d446227b3d5523434bb433aced6c2c74da1b5b3ce9d077589b68b89546ae2a22d51cf3fdc74b43732b277d4fc9f482856110854c44fb89c6a36366aaf73edca64baaf0d256363a5b70bcce9176c4a3cd024502c970000002574455874646174653a63726561746500323031372d31312d31375431333a33303a30312b30313a303070966ecf0000002574455874646174653a6d6f6469667900323031372d31312d31375431333a33303a30312b30313a303001cbd6730000001974455874536f667477617265007777772e696e6b73636170652e6f72679bee3c1a0000000049454e44ae426082, 'test icon', 'icon', 'default.png', 'default_gray.png', 'defaultbig.png'),
(3, '-- no alt text --', 0x89504e470d0a1a0a0000000d4948445200000010000000100804000000b5fa37ea0000000467414d410000b18f0bfc6105000000206348524d00007a26000080840000fa00000080e8000075300000ea6000003a98000017709cba513c00000002624b47440000aa8d2332000000097048597300000dd700000dd70142289b780000000774494d4507e10b110d1e012527ff76000000ce4944415428cfa5d1a18a02011485e16f0c26838251c11718c420ecce5b984d767d19c132cd30c16e12b3bb60144ca261b0ca6a51109c0db2e2b0983ce9c23df0df7b0eef2a784c651f3e4558f8f2ede7d95635759339999b3bc9dc4c55ffd691d4415fa8000a427d07a90806ae961aca7a622b2bb19eb286a5ab013343452d5b999d446227b3d5523434bb433aced6c2c74da1b5b3ce9d077589b68b89546ae2a22d51cf3fdc74b43732b277d4fc9f482856110854c44fb89c6a36366aaf73edca64baaf0d256363a5b70bcce9176c4a3cd024502c970000002574455874646174653a63726561746500323031372d31312d31375431333a33303a30312b30313a303070966ecf0000002574455874646174653a6d6f6469667900323031372d31312d31375431333a33303a30312b30313a303001cbd6730000001974455874536f667477617265007777772e696e6b73636170652e6f72679bee3c1a0000000049454e44ae426082, 'test icon', 'icon', 'default.png', 'default.png', 'defaultbig.png');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `FE-Nutzer`
--

CREATE TABLE IF NOT EXISTS `FE-Nutzer` (
`Nr` int(11) NOT NULL,
  `Aktiv` tinyint(1) NOT NULL,
  `Vorname` varchar(64) NOT NULL,
  `Nachname` varchar(64) NOT NULL,
  `Loginname` varchar(64) NOT NULL,
  `Email` varchar(64) NOT NULL,
  `Hash` varchar(24) NOT NULL,
  `Salt` varchar(32) NOT NULL,
  `Algorythmus` enum('sha1','sha256') NOT NULL,
  `Strech` int(10) unsigned NOT NULL,
  `LetzterLogin` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `Anlegedatum` date NOT NULL,
  `Benutzerrolle` enum('Student','Mitarbeiter','Gast','') NOT NULL,
  `verified` tinyint(1) NOT NULL DEFAULT '0',
  `admin` tinyint(1) NOT NULL DEFAULT '0'
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

--
-- Daten für Tabelle `FE-Nutzer`
--

INSERT INTO `FE-Nutzer` (`Nr`, `Aktiv`, `Vorname`, `Nachname`, `Loginname`, `Email`, `Hash`, `Salt`, `Algorythmus`, `Strech`, `LetzterLogin`, `Anlegedatum`, `Benutzerrolle`, `verified`, `admin`) VALUES
(1, 1, 'Marcel', 'Ochsendorf', 'dbwt', 'marcel.ochsendorf@gmail.com', 'sRsl6XgRWEZA92KXsycuxQXO', 'QpJ/ciH4wC/0/Xdc7UH2mJfsa8idmdhq', 'sha1', 18, '2018-01-11 01:57:27', '2017-11-05', 'Student', 1, 1);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `Gast`
--

CREATE TABLE IF NOT EXISTS `Gast` (
`Id` int(11) NOT NULL,
  `Grund` varchar(100) NOT NULL DEFAULT 'Tagesgast',
  `Ablauf` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `FK_Fe-Nutzer` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `Kategorie`
--

CREATE TABLE IF NOT EXISTS `Kategorie` (
`Id` mediumint(9) NOT NULL,
  `Bezeichnung` varchar(100) DEFAULT NULL,
  `Oberkategorie` mediumint(9) DEFAULT NULL
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=latin1;

--
-- Daten für Tabelle `Kategorie`
--

INSERT INTO `Kategorie` (`Id`, `Bezeichnung`, `Oberkategorie`) VALUES
(2, 'Hauptkategorie', NULL),
(4, 'Unterkategorie 1', 2),
(5, 'Unterkategorie 2', 2),
(6, 'Unterkategorie 3', 2),
(7, 'TEST', NULL),
(8, 'hallo', 7);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `Mitarbeiter`
--

CREATE TABLE IF NOT EXISTS `Mitarbeiter` (
`Id` int(11) NOT NULL,
  `Telefonnummer` varchar(16) NOT NULL,
  `MA-Nummer` int(11) NOT NULL,
  `Büro` varchar(80) NOT NULL DEFAULT 'kein Büro',
  `FK_Fe-Nutzer` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `Preis`
--

CREATE TABLE IF NOT EXISTS `Preis` (
`Id` mediumint(9) NOT NULL,
  `Gastbetrag` decimal(4,2) NOT NULL,
  `Studentenbetrag` decimal(4,2) NOT NULL,
  `Mitarbeiterbetrag` decimal(4,2) NOT NULL,
  `FK_Produkt` mediumint(9) NOT NULL
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

--
-- Daten für Tabelle `Preis`
--

INSERT INTO `Preis` (`Id`, `Gastbetrag`, `Studentenbetrag`, `Mitarbeiterbetrag`, `FK_Produkt`) VALUES
(1, 4.99, 2.99, 3.99, 1);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `Produkt`
--

CREATE TABLE IF NOT EXISTS `Produkt` (
`Id` mediumint(9) NOT NULL,
  `Beschreibung` varchar(100) NOT NULL,
  `FK_Kategorie` mediumint(9) NOT NULL,
  `FK_Bild` mediumint(9) NOT NULL,
  `FK_Preis` mediumint(9) NOT NULL,
  `Ausverkauft` tinyint(1) NOT NULL DEFAULT '0',
  `Titel` text NOT NULL
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=latin1;

--
-- Daten für Tabelle `Produkt`
--

INSERT INTO `Produkt` (`Id`, `Beschreibung`, `FK_Kategorie`, `FK_Bild`, `FK_Preis`, `Ausverkauft`, `Titel`) VALUES
(1, 'Salami Salat ist toll', 4, 2, 1, 1, 'Salami Salat'),
(2, 'prod1 desc', 5, 2, 1, 0, 'P1'),
(3, 'prod 2 desc', 6, 2, 1, 0, 'P2'),
(4, 'prod3 desc', 4, 2, 1, 0, 'P3'),
(5, 'prod 4 desc', 5, 2, 1, 0, 'P4'),
(6, 'tolles produkt', 6, 2, 1, 0, 'Fleischwurst'),
(7, 'toller salat gesund und munter', 4, 2, 1, 0, 'Feischsalat'),
(8, 'Knofi', 5, 2, 1, 1, 'extra knbolauch');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `ProduktZutat`
--

CREATE TABLE IF NOT EXISTS `ProduktZutat` (
  `FK_Produkt` mediumint(9) NOT NULL,
  `FK_Zutat` mediumint(9) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Daten für Tabelle `ProduktZutat`
--

INSERT INTO `ProduktZutat` (`FK_Produkt`, `FK_Zutat`) VALUES
(1, 1),
(1, 2);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `Student`
--

CREATE TABLE IF NOT EXISTS `Student` (
`Id` mediumint(9) NOT NULL,
  `Matrikelnummer` int(11) NOT NULL,
  `Studiengang` varchar(32) NOT NULL,
  `FK_Fe-Nutzer` int(11) NOT NULL
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

--
-- Daten für Tabelle `Student`
--

INSERT INTO `Student` (`Id`, `Matrikelnummer`, `Studiengang`, `FK_Fe-Nutzer`) VALUES
(1, 3120232, 'inf', 1);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `Zutat`
--

CREATE TABLE IF NOT EXISTS `Zutat` (
`Id` mediumint(9) NOT NULL,
  `Name` varchar(32) NOT NULL,
  `Beschreibung` varchar(100) DEFAULT '-- Beschreibung der Zutat --',
  `Vegan` tinyint(1) DEFAULT '0',
  `Vegetarisch` tinyint(1) DEFAULT '0',
  `Bio` tinyint(1) DEFAULT '0',
  `Glutenfrei` tinyint(1) DEFAULT '0'
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

--
-- Daten für Tabelle `Zutat`
--

INSERT INTO `Zutat` (`Id`, `Name`, `Beschreibung`, `Vegan`, `Vegetarisch`, `Bio`, `Glutenfrei`) VALUES
(1, 'Pfeffer', 'scharf', 1, 1, 0, 1),
(2, 'Wurst', 'Salami', 0, 0, 1, 0);

--
-- Indizes der exportierten Tabellen
--

--
-- Indizes für die Tabelle `Bestellung`
--
ALTER TABLE `Bestellung`
 ADD PRIMARY KEY (`Id`);

--
-- Indizes für die Tabelle `Bewertung`
--
ALTER TABLE `Bewertung`
 ADD PRIMARY KEY (`Id`);

--
-- Indizes für die Tabelle `Bild`
--
ALTER TABLE `Bild`
 ADD PRIMARY KEY (`Id`);

--
-- Indizes für die Tabelle `FE-Nutzer`
--
ALTER TABLE `FE-Nutzer`
 ADD PRIMARY KEY (`Nr`);

--
-- Indizes für die Tabelle `Gast`
--
ALTER TABLE `Gast`
 ADD PRIMARY KEY (`Id`), ADD UNIQUE KEY `FK_Fe-Nutzer` (`FK_Fe-Nutzer`);

--
-- Indizes für die Tabelle `Kategorie`
--
ALTER TABLE `Kategorie`
 ADD PRIMARY KEY (`Id`), ADD KEY `OberKat` (`Oberkategorie`);

--
-- Indizes für die Tabelle `Mitarbeiter`
--
ALTER TABLE `Mitarbeiter`
 ADD PRIMARY KEY (`Id`), ADD UNIQUE KEY `FK_Fe-Nutzer` (`FK_Fe-Nutzer`);

--
-- Indizes für die Tabelle `Preis`
--
ALTER TABLE `Preis`
 ADD PRIMARY KEY (`Id`);

--
-- Indizes für die Tabelle `Produkt`
--
ALTER TABLE `Produkt`
 ADD PRIMARY KEY (`Id`);

--
-- Indizes für die Tabelle `ProduktZutat`
--
ALTER TABLE `ProduktZutat`
 ADD PRIMARY KEY (`FK_Produkt`,`FK_Zutat`), ADD KEY `zut_id` (`FK_Zutat`);

--
-- Indizes für die Tabelle `Student`
--
ALTER TABLE `Student`
 ADD PRIMARY KEY (`Id`), ADD UNIQUE KEY `FK_Fe-Nutzer` (`FK_Fe-Nutzer`);

--
-- Indizes für die Tabelle `Zutat`
--
ALTER TABLE `Zutat`
 ADD PRIMARY KEY (`Id`);

--
-- AUTO_INCREMENT für exportierte Tabellen
--

--
-- AUTO_INCREMENT für Tabelle `Bestellung`
--
ALTER TABLE `Bestellung`
MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=2;
--
-- AUTO_INCREMENT für Tabelle `Bewertung`
--
ALTER TABLE `Bewertung`
MODIFY `Id` mediumint(9) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=2;
--
-- AUTO_INCREMENT für Tabelle `Bild`
--
ALTER TABLE `Bild`
MODIFY `Id` mediumint(9) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=4;
--
-- AUTO_INCREMENT für Tabelle `FE-Nutzer`
--
ALTER TABLE `FE-Nutzer`
MODIFY `Nr` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=2;
--
-- AUTO_INCREMENT für Tabelle `Gast`
--
ALTER TABLE `Gast`
MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT für Tabelle `Kategorie`
--
ALTER TABLE `Kategorie`
MODIFY `Id` mediumint(9) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=9;
--
-- AUTO_INCREMENT für Tabelle `Mitarbeiter`
--
ALTER TABLE `Mitarbeiter`
MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT für Tabelle `Preis`
--
ALTER TABLE `Preis`
MODIFY `Id` mediumint(9) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=2;
--
-- AUTO_INCREMENT für Tabelle `Produkt`
--
ALTER TABLE `Produkt`
MODIFY `Id` mediumint(9) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=9;
--
-- AUTO_INCREMENT für Tabelle `Student`
--
ALTER TABLE `Student`
MODIFY `Id` mediumint(9) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=2;
--
-- AUTO_INCREMENT für Tabelle `Zutat`
--
ALTER TABLE `Zutat`
MODIFY `Id` mediumint(9) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=3;
--
-- Constraints der exportierten Tabellen
--

--
-- Constraints der Tabelle `Gast`
--
ALTER TABLE `Gast`
ADD CONSTRAINT `GastNutzer` FOREIGN KEY (`FK_Fe-Nutzer`) REFERENCES `FE-Nutzer` (`Nr`) ON DELETE CASCADE;

--
-- Constraints der Tabelle `Kategorie`
--
ALTER TABLE `Kategorie`
ADD CONSTRAINT `OberKat` FOREIGN KEY (`Oberkategorie`) REFERENCES `Kategorie` (`Id`);

--
-- Constraints der Tabelle `Mitarbeiter`
--
ALTER TABLE `Mitarbeiter`
ADD CONSTRAINT `MitarbeiterNutzer` FOREIGN KEY (`FK_Fe-Nutzer`) REFERENCES `FE-Nutzer` (`Nr`) ON DELETE CASCADE;

--
-- Constraints der Tabelle `Preis`
--
ALTER TABLE `Preis`
ADD CONSTRAINT `produktid_fuer_preis` FOREIGN KEY (`Id`) REFERENCES `Produkt` (`Id`) ON DELETE CASCADE;

--
-- Constraints der Tabelle `ProduktZutat`
--
ALTER TABLE `ProduktZutat`
ADD CONSTRAINT `prod_id` FOREIGN KEY (`FK_Produkt`) REFERENCES `Produkt` (`Id`),
ADD CONSTRAINT `zut_id` FOREIGN KEY (`FK_Zutat`) REFERENCES `Zutat` (`Id`);

--
-- Constraints der Tabelle `Student`
--
ALTER TABLE `Student`
ADD CONSTRAINT `StudentNutzer` FOREIGN KEY (`FK_Fe-Nutzer`) REFERENCES `FE-Nutzer` (`Nr`) ON DELETE CASCADE;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
