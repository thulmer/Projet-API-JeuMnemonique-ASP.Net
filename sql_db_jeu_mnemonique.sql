CREATE DATABASE  IF NOT EXISTS `jeumnemonique` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `jeumnemonique`;
-- MySQL dump 10.13  Distrib 8.0.30, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: jeumnemonique
-- ------------------------------------------------------
-- Server version	8.0.30

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `groupe`
--

DROP TABLE IF EXISTS `groupe`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `groupe` (
  `id` int NOT NULL AUTO_INCREMENT,
  `nomGroupe` varchar(255) DEFAULT 'Nouveau groupe',
  `proprietaireGroupe` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `fk_proprietaire_groupe_idx` (`proprietaireGroupe`),
  CONSTRAINT `fk_proprietaire_groupe` FOREIGN KEY (`proprietaireGroupe`) REFERENCES `joueur` (`courriel`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `groupe`
--

LOCK TABLES `groupe` WRITE;
/*!40000 ALTER TABLE `groupe` DISABLE KEYS */;
INSERT INTO `groupe` VALUES (1,'Groupe info','toto@lol.com'),(2,'Groupe langues','bobmax@lol.com'),(3,'Groupe fun','michel@lol.com'),(4,'Révisions','michel@lol.com'),(13,'Native 3','toto@lol.com');
/*!40000 ALTER TABLE `groupe` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `joueur`
--

DROP TABLE IF EXISTS `joueur`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `joueur` (
  `identifiant` varchar(16) NOT NULL,
  `courriel` varchar(255) NOT NULL,
  `motDePasse` varchar(500) NOT NULL,
  PRIMARY KEY (`courriel`),
  UNIQUE KEY `courriel_UNIQUE` (`courriel`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `joueur`
--

LOCK TABLES `joueur` WRITE;
/*!40000 ALTER TABLE `joueur` DISABLE KEYS */;
INSERT INTO `joueur` VALUES ('Bob','bobmax@lol.com','o7OjlHlCFMifa8M6e5TOZA==:dmbL7VMVIFwJYKYfVHlRRh5H8O+uJW7cbXwLylVY1Uc='),('michel','michel@lol.com','TxQOf9Y6MfLqZdC7M/FR5Q==:6zSPZnwPxO67wwzW25pmbl1xBqzRdIpMkvfC3IDsVcc='),('Toto','toto@lol.com','tyhBqf8Xn57lWEqvnqz6wA==:d4+qqvP/w1eUot7NPpDq+L3uPAz3p8RMIkZuxdbUDvw=');
/*!40000 ALTER TABLE `joueur` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `liste_groupe`
--

DROP TABLE IF EXISTS `liste_groupe`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `liste_groupe` (
  `groupe` int NOT NULL,
  `listePartagee` int NOT NULL,
  PRIMARY KEY (`groupe`,`listePartagee`),
  KEY `fk_liste_partagee_idx` (`listePartagee`),
  CONSTRAINT `fk_groupe_partage` FOREIGN KEY (`groupe`) REFERENCES `groupe` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_liste_partagee` FOREIGN KEY (`listePartagee`) REFERENCES `listedequestions` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `liste_groupe`
--

LOCK TABLES `liste_groupe` WRITE;
/*!40000 ALTER TABLE `liste_groupe` DISABLE KEYS */;
INSERT INTO `liste_groupe` VALUES (1,3),(2,3),(2,4),(2,5);
/*!40000 ALTER TABLE `liste_groupe` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `listedequestions`
--

DROP TABLE IF EXISTS `listedequestions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `listedequestions` (
  `id` int NOT NULL AUTO_INCREMENT,
  `nomListe` varchar(100) DEFAULT 'Nouvelle liste',
  `proprietaire` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_propietaire_idx` (`proprietaire`),
  CONSTRAINT `fk_propietaire` FOREIGN KEY (`proprietaire`) REFERENCES `joueur` (`courriel`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `listedequestions`
--

LOCK TABLES `listedequestions` WRITE;
/*!40000 ALTER TABLE `listedequestions` DISABLE KEYS */;
INSERT INTO `listedequestions` VALUES (1,'Liste des capitales','michel@lol.com'),(2,'Liste informatique','michel@lol.com'),(3,'Liste calcul','toto@lol.com'),(4,'Liste espagnol','bobmax@lol.com'),(5,'Liste allemand','bobmax@lol.com');
/*!40000 ALTER TABLE `listedequestions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `membregroupe`
--

DROP TABLE IF EXISTS `membregroupe`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `membregroupe` (
  `groupe` int NOT NULL,
  `membre` varchar(255) NOT NULL,
  PRIMARY KEY (`groupe`,`membre`),
  KEY `fk_membre_membreGroupe_idx` (`membre`),
  CONSTRAINT `fk_groupe_membreGroupe` FOREIGN KEY (`groupe`) REFERENCES `groupe` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_membre_membreGroupe` FOREIGN KEY (`membre`) REFERENCES `joueur` (`courriel`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `membregroupe`
--

LOCK TABLES `membregroupe` WRITE;
/*!40000 ALTER TABLE `membregroupe` DISABLE KEYS */;
INSERT INTO `membregroupe` VALUES (3,'bobmax@lol.com'),(1,'michel@lol.com'),(2,'toto@lol.com'),(3,'toto@lol.com');
/*!40000 ALTER TABLE `membregroupe` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `questionreponse`
--

DROP TABLE IF EXISTS `questionreponse`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `questionreponse` (
  `id` int NOT NULL AUTO_INCREMENT,
  `question` varchar(500) DEFAULT 'Question',
  `reponse` varchar(500) DEFAULT 'Réponse',
  `liste` int NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `fk_liste_idx` (`liste`),
  CONSTRAINT `fk_liste` FOREIGN KEY (`liste`) REFERENCES `listedequestions` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `questionreponse`
--

LOCK TABLES `questionreponse` WRITE;
/*!40000 ALTER TABLE `questionreponse` DISABLE KEYS */;
INSERT INTO `questionreponse` VALUES (1,'Capitale des USA','Washington',1),(2,'Capitale de la France','Paris',1),(3,'Capitale de l\'Allemagne','Berlin',1),(4,'Que veut dire USB ?','Universal Serial Bus',2),(5,'C\'est quoi Kotlin?','Un langage de programmation',2),(6,'Que veut dire FTP','File Tranfer Protocol',2),(7,'2+2 =  ?','4',3),(8,'6*5 = ?','30',3),(9,'4/2 = ?','2',3),(10,'Comment dit-on bonjour?','Hola',4),(11,'Comment dit-on merci?','Gracias',4),(12,'Comment dit-on bonjour?','Guten tag',5),(13,'Comment dit-on merci?','Danke',5);
/*!40000 ALTER TABLE `questionreponse` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2022-12-06 16:30:10
