-- MySQL dump 10.13  Distrib 8.0.40, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: sampledb
-- ------------------------------------------------------
-- Server version	8.0.40

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
-- Table structure for table `customer_tbl`
--

DROP TABLE IF EXISTS `customer_tbl`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `customer_tbl` (
  `customerid` int NOT NULL AUTO_INCREMENT,
  `customerTitleName` varchar(45) DEFAULT NULL,
  `customerFName` varchar(45) DEFAULT NULL,
  `customerLNAME` varchar(45) DEFAULT NULL,
  `customerAddress` varchar(50) DEFAULT NULL,
  `customerProvince` varchar(50) DEFAULT NULL,
  `districtId` int DEFAULT NULL,
  `subdistrictId` int DEFAULT NULL,
  `customerPostalCode` int DEFAULT NULL,
  `customerPhone` int DEFAULT NULL,
  `customerImage` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`customerid`),
  KEY `fk_customerDistrict` (`districtId`),
  KEY `fk_customerSubDistrict` (`subdistrictId`),
  CONSTRAINT `fk_customerDistrict` FOREIGN KEY (`districtId`) REFERENCES `district_tbl` (`districtId`),
  CONSTRAINT `fk_customerSubDistrict` FOREIGN KEY (`subdistrictId`) REFERENCES `subdistrict_tbl` (`subdistrictId`)
) ENGINE=InnoDB AUTO_INCREMENT=34 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `customer_tbl`
--

LOCK TABLES `customer_tbl` WRITE;
/*!40000 ALTER TABLE `customer_tbl` DISABLE KEYS */;
INSERT INTO `customer_tbl` VALUES (32,'Mr','mooting','moosawan','1999','Bangkok',1,1,19108,6123456,'ba74e89d-3a1e-41a8-9ba1-1b6bd2e21716.jpg'),(33,'Mr','moodeng','moosawan','1999','Bangkok',1,1,19108,6123456,'83235d70-2b3e-409b-a0b1-fdb2a5b902f7.jpg');
/*!40000 ALTER TABLE `customer_tbl` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-01-13  3:08:53
