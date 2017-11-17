-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server Version:               10.2.10-MariaDB - mariadb.org binary distribution
-- Server Betriebssystem:        Win64
-- HeidiSQL Version:             9.4.0.5125
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Exportiere Datenbank Struktur für praktikum
CREATE DATABASE IF NOT EXISTS `praktikum` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `praktikum`;

-- Exportiere Struktur von Tabelle praktikum.bestellung
CREATE TABLE IF NOT EXISTS `bestellung` (
  `bestellung_id` mediumint(9) NOT NULL AUTO_INCREMENT,
  `zeitpunkt` datetime NOT NULL,
  `anzahl` smallint(6) NOT NULL,
  `FeNutzer_id` mediumint(9) NOT NULL,
  PRIMARY KEY (`bestellung_id`),
  KEY `nutzerid_fuer_bestellung` (`FeNutzer_id`),
  CONSTRAINT `nutzerid_fuer_bestellung` FOREIGN KEY (`FeNutzer_id`) REFERENCES `fenutzer` (`Nr`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Daten Export vom Benutzer nicht ausgewählt
-- Exportiere Struktur von Tabelle praktikum.bild
CREATE TABLE IF NOT EXISTS `bild` (
  `id` mediumint(9) NOT NULL AUTO_INCREMENT,
  `alt_text` varchar(80) NOT NULL DEFAULT 'kein Alternativtext vorhanden',
  `titel` varchar(100) NOT NULL DEFAULT 'kein Titel',
  `Bildunterschrift` varchar(100) DEFAULT NULL,
  `Binaerdaten` blob NOT NULL,
  `kategorie_id` mediumint(9) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `kategorie_fuer_bild` (`kategorie_id`),
  CONSTRAINT `kategorie_fuer_bild` FOREIGN KEY (`kategorie_id`) REFERENCES `kategorie` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Daten Export vom Benutzer nicht ausgewählt
-- Exportiere Struktur von Tabelle praktikum.fenutzer
CREATE TABLE IF NOT EXISTS `fenutzer` (
  `Nr` mediumint(9) NOT NULL AUTO_INCREMENT,
  `aktiv` tinyint(1) NOT NULL,
  `anlegedatum` date NOT NULL,
  `loginname` varchar(30) NOT NULL,
  `vorname` varchar(40) NOT NULL,
  `nachname` varchar(50) NOT NULL,
  `letzter_login` datetime DEFAULT NULL,
  `email` varchar(50) NOT NULL,
  `hashwert` varchar(24) NOT NULL,
  `salt` varchar(32) NOT NULL,
  `stretch` varchar(50) NOT NULL,
  `algorithmus` enum('sha1','sha256') NOT NULL,
  PRIMARY KEY (`Nr`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Daten Export vom Benutzer nicht ausgewählt
-- Exportiere Struktur von Tabelle praktikum.gast
CREATE TABLE IF NOT EXISTS `gast` (
  `gast_id` mediumint(9) NOT NULL AUTO_INCREMENT,
  `ablaufdatum` date NOT NULL,
  `gastgrund` varchar(60) NOT NULL,
  `FeNutzer_id` mediumint(9) NOT NULL,
  PRIMARY KEY (`gast_id`),
  KEY `nutzerid_fuer_gast` (`FeNutzer_id`),
  CONSTRAINT `nutzerid_fuer_gast` FOREIGN KEY (`FeNutzer_id`) REFERENCES `fenutzer` (`Nr`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Daten Export vom Benutzer nicht ausgewählt
-- Exportiere Struktur von Tabelle praktikum.kategorie
CREATE TABLE IF NOT EXISTS `kategorie` (
  `id` mediumint(9) NOT NULL AUTO_INCREMENT,
  `bezeichnung` varchar(50) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Daten Export vom Benutzer nicht ausgewählt
-- Exportiere Struktur von Tabelle praktikum.preis
CREATE TABLE IF NOT EXISTS `preis` (
  `id` mediumint(9) NOT NULL AUTO_INCREMENT,
  `gastbetrag` decimal(4,2) NOT NULL,
  `studentenbetrag` decimal(4,2) NOT NULL,
  `mitarbeiterbetrag` decimal(4,2) NOT NULL,
  PRIMARY KEY (`id`),
  CONSTRAINT `produktid_fuer_preis` FOREIGN KEY (`id`) REFERENCES `kategorie` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Daten Export vom Benutzer nicht ausgewählt
-- Exportiere Struktur von Tabelle praktikum.produkt
CREATE TABLE IF NOT EXISTS `produkt` (
  `id` mediumint(9) NOT NULL AUTO_INCREMENT,
  `beschreibung` varchar(80) NOT NULL DEFAULT 'kein Alternativtext vorhanden',
  `vegetarisch` tinyint(1) DEFAULT 0,
  `vegan` tinyint(1) DEFAULT 0,
  `bild_id` mediumint(9) NOT NULL,
  `kategorie_id` mediumint(9) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `kategorie_fuer_produkt` (`kategorie_id`),
  KEY `bild_fuer_produkt` (`bild_id`),
  CONSTRAINT `bild_fuer_produkt` FOREIGN KEY (`bild_id`) REFERENCES `bild` (`id`) ON DELETE CASCADE,
  CONSTRAINT `kategorie_fuer_produkt` FOREIGN KEY (`kategorie_id`) REFERENCES `kategorie` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Daten Export vom Benutzer nicht ausgewählt
-- Exportiere Struktur von Tabelle praktikum.produkt_to_zutat
CREATE TABLE IF NOT EXISTS `produkt_to_zutat` (
  `id_produkt` mediumint(9) NOT NULL,
  `id_zutat` mediumint(9) NOT NULL,
  KEY `produktid_fuer_link` (`id_produkt`),
  KEY `zutatid_fuer_link` (`id_zutat`),
  CONSTRAINT `produktid_fuer_link` FOREIGN KEY (`id_produkt`) REFERENCES `produkt` (`id`) ON DELETE CASCADE,
  CONSTRAINT `zutatid_fuer_link` FOREIGN KEY (`id_zutat`) REFERENCES `zutat` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Daten Export vom Benutzer nicht ausgewählt
-- Exportiere Struktur von Tabelle praktikum.zutat
CREATE TABLE IF NOT EXISTS `zutat` (
  `id` mediumint(9) NOT NULL AUTO_INCREMENT,
  `name` varchar(60) NOT NULL DEFAULT 'unbekannt',
  `beschreibung` varchar(120) DEFAULT NULL,
  `vegan` tinyint(1) NOT NULL DEFAULT 0,
  `vegetarisch` tinyint(1) NOT NULL DEFAULT 0,
  `bio` tinyint(1) NOT NULL DEFAULT 0,
  `glutenfrei` tinyint(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Daten Export vom Benutzer nicht ausgewählt
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
