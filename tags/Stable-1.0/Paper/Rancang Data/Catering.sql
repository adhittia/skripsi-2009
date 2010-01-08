-- MySQL Administrator dump 1.4
--
-- ------------------------------------------------------
-- Server version	5.0.15-nt


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


--
-- Create schema catering
--

CREATE DATABASE IF NOT EXISTS catering;
USE catering;

--
-- Definition of table `customer_order`
--

DROP TABLE IF EXISTS `customer_order`;
CREATE TABLE `customer_order` (
  `Menu_Schedule_Customer_S` int(11) NOT NULL auto_increment,
  `COM_Selected` int(11) default NULL,
  `COM_Order_Status` varchar(20) default NULL,
  `COM_Order_Date` date default NULL,
  `Menu_Schedule_Id` int(11) NOT NULL,
  `Customer_ID` int(11) default NULL,
  `Id_Input` varchar(17) NOT NULL,
  PRIMARY KEY  (`Menu_Schedule_Customer_S`),
  KEY `R_1` (`Menu_Schedule_Id`),
  KEY `R_5` (`Customer_ID`),
  KEY `R_11` (`Id_Input`),
  CONSTRAINT `customer_order_ibfk_1` FOREIGN KEY (`Menu_Schedule_Id`) REFERENCES `menu_schedule` (`Menu_Schedule_Id`),
  CONSTRAINT `customer_order_ibfk_2` FOREIGN KEY (`Customer_ID`) REFERENCES `customer_profile` (`Customer_ID`),
  CONSTRAINT `customer_order_ibfk_3` FOREIGN KEY (`Id_Input`) REFERENCES `sms_input` (`Id_Input`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `customer_order`
--

/*!40000 ALTER TABLE `customer_order` DISABLE KEYS */;
/*!40000 ALTER TABLE `customer_order` ENABLE KEYS */;


--
-- Definition of table `customer_order_detail`
--

DROP TABLE IF EXISTS `customer_order_detail`;
CREATE TABLE `customer_order_detail` (
  `Additional_Order_S` int(11) NOT NULL auto_increment,
  `Menu_ID` int(11) default NULL,
  `Price` varchar(20) default NULL,
  `Menu_Schedule_Customer_S` int(11) NOT NULL,
  `Id_Input` varchar(17) NOT NULL,
  PRIMARY KEY  (`Additional_Order_S`),
  KEY `R_3` (`Menu_ID`),
  KEY `R_6` (`Menu_Schedule_Customer_S`),
  KEY `R_12` (`Id_Input`),
  CONSTRAINT `customer_order_detail_ibfk_1` FOREIGN KEY (`Menu_ID`) REFERENCES `menu` (`Menu_ID`),
  CONSTRAINT `customer_order_detail_ibfk_2` FOREIGN KEY (`Menu_Schedule_Customer_S`) REFERENCES `customer_order` (`Menu_Schedule_Customer_S`),
  CONSTRAINT `customer_order_detail_ibfk_3` FOREIGN KEY (`Id_Input`) REFERENCES `sms_input` (`Id_Input`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `customer_order_detail`
--

/*!40000 ALTER TABLE `customer_order_detail` DISABLE KEYS */;
/*!40000 ALTER TABLE `customer_order_detail` ENABLE KEYS */;


--
-- Definition of table `customer_profile`
--

DROP TABLE IF EXISTS `customer_profile`;
CREATE TABLE `customer_profile` (
  `Customer_ID` int(11) NOT NULL auto_increment,
  `CP_Name` varchar(20) default NULL,
  `CP_Delivery_Address` varchar(80) default NULL,
  `CP_Bill_Address` varchar(80) default NULL,
  `CP_Mobile_Number` varchar(15) default NULL,
  `CP_Email` varchar(50) default NULL,
  PRIMARY KEY  (`Customer_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `customer_profile`
--

/*!40000 ALTER TABLE `customer_profile` DISABLE KEYS */;
/*!40000 ALTER TABLE `customer_profile` ENABLE KEYS */;


--
-- Definition of table `daftar_register`
--

DROP TABLE IF EXISTS `daftar_register`;
CREATE TABLE `daftar_register` (
  `Reg_Type` varchar(5) NOT NULL default '',
  `Reg_Name` varchar(15) NOT NULL default '',
  `Nama_Class` varchar(255) default NULL,
  `Nama_Assembly` varchar(255) default NULL,
  PRIMARY KEY  (`Reg_Type`,`Reg_Name`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `daftar_register`
--

/*!40000 ALTER TABLE `daftar_register` DISABLE KEYS */;
/*!40000 ALTER TABLE `daftar_register` ENABLE KEYS */;


--
-- Definition of table `jadwal_broadcast`
--

DROP TABLE IF EXISTS `jadwal_broadcast`;
CREATE TABLE `jadwal_broadcast` (
  `Id_Jadwal` int(11) NOT NULL auto_increment,
  `Pengulangan_Max` int(11) default NULL,
  `Pengulangan_Hitung` int(11) default NULL,
  `Pengulangan_Jeda_Hari` int(11) default NULL,
  `Waktu_Eksekusi_Berikut` date default NULL,
  `Waktu_Eksekusi_Terakhir` date default NULL,
  `Status` varchar(10) default NULL,
  `Reg_Name` varchar(15) NOT NULL,
  `Reg_Type` varchar(5) NOT NULL,
  PRIMARY KEY  (`Id_Jadwal`),
  KEY `R_7` (`Reg_Type`,`Reg_Name`),
  CONSTRAINT `jadwal_broadcast_ibfk_1` FOREIGN KEY (`Reg_Type`, `Reg_Name`) REFERENCES `daftar_register` (`Reg_Type`, `Reg_Name`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `jadwal_broadcast`
--

/*!40000 ALTER TABLE `jadwal_broadcast` DISABLE KEYS */;
/*!40000 ALTER TABLE `jadwal_broadcast` ENABLE KEYS */;


--
-- Definition of table `menu`
--

DROP TABLE IF EXISTS `menu`;
CREATE TABLE `menu` (
  `Menu_ID` int(11) NOT NULL auto_increment,
  `M_Name` varchar(20) default NULL,
  `M_Description` varchar(50) default NULL,
  `M_Price` decimal(8,2) default NULL,
  `M_Type` varchar(20) default NULL,
  `M_Category` varchar(20) default NULL,
  PRIMARY KEY  (`Menu_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `menu`
--

/*!40000 ALTER TABLE `menu` DISABLE KEYS */;
/*!40000 ALTER TABLE `menu` ENABLE KEYS */;


--
-- Definition of table `menu_schedule`
--

DROP TABLE IF EXISTS `menu_schedule`;
CREATE TABLE `menu_schedule` (
  `Menu_Schedule_Id` int(11) NOT NULL auto_increment,
  `MS_Date` date default NULL,
  `MS_Menu_A` int(11) default NULL,
  `MS_Menu_B` int(11) default NULL,
  `MS_Menu_C` int(11) default NULL,
  PRIMARY KEY  (`Menu_Schedule_Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `menu_schedule`
--

/*!40000 ALTER TABLE `menu_schedule` DISABLE KEYS */;
/*!40000 ALTER TABLE `menu_schedule` ENABLE KEYS */;


--
-- Definition of table `sms_input`
--

DROP TABLE IF EXISTS `sms_input`;
CREATE TABLE `sms_input` (
  `Id_Input` varchar(17) NOT NULL default '',
  `Tanggal_Terima` datetime default NULL,
  `No_Pengirim` varchar(15) default NULL,
  `Pesan_Teks` varchar(160) default NULL,
  `Status` varchar(5) default NULL,
  PRIMARY KEY  (`Id_Input`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `sms_input`
--

/*!40000 ALTER TABLE `sms_input` DISABLE KEYS */;
/*!40000 ALTER TABLE `sms_input` ENABLE KEYS */;


--
-- Definition of table `sms_output`
--

DROP TABLE IF EXISTS `sms_output`;
CREATE TABLE `sms_output` (
  `Id_Output` varchar(17) NOT NULL default '',
  `Waktu_Diproses` datetime default NULL,
  `Waktu_Kirim` datetime default NULL,
  `No_Tujuan` varchar(15) default NULL,
  `Pesan_Teks` varchar(160) default NULL,
  `Status` varchar(5) default NULL,
  `Reg_Name` varchar(15) NOT NULL,
  `Reg_Type` varchar(5) NOT NULL,
  `Id_Input` varchar(17) NOT NULL,
  PRIMARY KEY  (`Id_Output`),
  KEY `R_8` (`Reg_Type`,`Reg_Name`),
  KEY `R_9` (`Id_Input`),
  CONSTRAINT `sms_output_ibfk_2` FOREIGN KEY (`Id_Input`) REFERENCES `sms_input` (`Id_Input`),
  CONSTRAINT `sms_output_ibfk_1` FOREIGN KEY (`Reg_Type`, `Reg_Name`) REFERENCES `daftar_register` (`Reg_Type`, `Reg_Name`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `sms_output`
--

/*!40000 ALTER TABLE `sms_output` DISABLE KEYS */;
/*!40000 ALTER TABLE `sms_output` ENABLE KEYS */;




/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
