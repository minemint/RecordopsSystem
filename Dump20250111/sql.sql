-- Drop tables if they already exist to ensure a clean slate
DROP TABLE IF EXISTS `customer_tbl`;
DROP TABLE IF EXISTS `district_tbl`;
DROP TABLE IF EXISTS `subdistrict_tbl`;

-- Create the `district_tbl`
CREATE TABLE `district_tbl` (
  `districtId` int NOT NULL AUTO_INCREMENT,
  `districtName` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`districtId`)
);

-- Create the `subdistrict_tbl`
CREATE TABLE `subdistrict_tbl` (
  `subdistrictId` int NOT NULL AUTO_INCREMENT,
  `subdistrictName` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`subdistrictId`)
);

-- Create the `customer_tbl`
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
);

-- Insert data into `district_tbl`
INSERT INTO `district_tbl` (`districtId`, `districtName`) VALUES
(1, 'Thon Buri'),
(2, 'Bang Rak');

-- Insert data into `subdistrict_tbl`
INSERT INTO `subdistrict_tbl` (`subdistrictId`, `subdistrictName`) VALUES
(1, 'Anusawari'),
(2, 'Silom');

-- Insert data into `customer_tbl`
INSERT INTO `customer_tbl` (`customerid`, `customerTitleName`, `customerFName`, `customerLNAME`, `customerAddress`, `customerProvince`, `districtId`, `subdistrictId`, `customerPostalCode`, `customerPhone`, `customerImage`) VALUES
(32, 'Mr', 'mooting', 'moosawan', '1999', 'Bangkok', 1, 1, 19108, 6123456, 'ba74e89d-3a1e-41a8-9ba1-1b6bd2e21716.jpg'),
(33, 'Mr', 'moodeng', 'moosawan', '1999', 'Bangkok', 1, 1, 19108, 6123456, '83235d70-2b3e-409b-a0b1-fdb2a5b902f7.jpg');
