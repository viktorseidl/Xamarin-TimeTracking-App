-- phpMyAdmin SQL Dump
-- version 5.0.4
-- https://www.phpmyadmin.net/
--
-- Host: mysql05.manitu.net
-- Erstellungszeit: 21. Mrz 2023 um 21:11
-- Server-Version: 10.6.8-MariaDB-log
-- PHP-Version: 7.4.33

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Datenbank: `.........`
--

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `timetouchbuchungen`
--

CREATE TABLE `timetouchbuchungen` (
  `ID` int(11) NOT NULL,
  `MID` int(11) NOT NULL,
  `Personalnr` varchar(5) NOT NULL,
  `Monat` int(11) NOT NULL,
  `Jahr` int(11) NOT NULL,
  `Datum` varchar(10) NOT NULL,
  `Uhrzeit` varchar(10) NOT NULL,
  `Buchung` varchar(30) NOT NULL,
  `ImportDatum` varchar(25) NOT NULL,
  `User` varchar(5) NOT NULL,
  `Vorgang` int(11) NOT NULL,
  `extDate` varchar(25) NOT NULL,
  `extDateTime` varchar(25) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;


--
-- Indizes der exportierten Tabellen
--

--
-- Indizes für die Tabelle `timetouchbuchungen`
--
ALTER TABLE `timetouchbuchungen`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `MID` (`MID`),
  ADD KEY `Personalnr` (`Personalnr`),
  ADD KEY `Monat` (`Monat`),
  ADD KEY `Jahr` (`Jahr`),
  ADD KEY `Datum` (`Datum`),
  ADD KEY `Uhrzeit` (`Uhrzeit`),
  ADD KEY `extDate` (`extDate`);

--
-- AUTO_INCREMENT für exportierte Tabellen
--

--
-- AUTO_INCREMENT für Tabelle `timetouchbuchungen`
--
ALTER TABLE `timetouchbuchungen`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=223;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
