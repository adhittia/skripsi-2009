
CREATE TABLE Customer_Order
(
	Menu_Schedule_Customer_S  INTEGER NULL,
	COM_Selected  CHAR(18) NULL,
	COM_Order_Status  CHAR(18) NULL,
	COM_Order_Date  CHAR(18) NULL,
	Menu_Schedule_Id  CHAR(18) NOT NULL,
	Customer_ID  VARCHAR(20) NULL,
	Id_Input  INTEGER NOT NULL
)
;



ALTER TABLE Customer_Order
	ADD  PRIMARY KEY (Menu_Schedule_Customer_S)
;



CREATE TABLE Customer_Order_Detail
(
	Additional_Order_S  INTEGER NULL,
	Menu_ID  VARCHAR(20) NULL,
	Price  CHAR(18) NULL,
	Menu_Schedule_Customer_S  INTEGER NOT NULL,
	Id_Input  INTEGER NOT NULL
)
;



ALTER TABLE Customer_Order_Detail
	ADD  PRIMARY KEY (Additional_Order_S)
;



CREATE TABLE Customer_Profile
(
	Customer_ID  VARCHAR(20) NULL,
	CP_Name  VARCHAR(20) NULL,
	CP_Delivery_Address  VARCHAR(20) NULL,
	CP_Bill_Address  CHAR(18) NULL,
	CP_Mobile_Number  CHAR(18) NULL,
	CP_Email  CHAR(18) NULL
)
;



ALTER TABLE Customer_Profile
	ADD  PRIMARY KEY (Customer_ID)
;



CREATE TABLE Daftar_Register
(
	Reg_Type  VARCHAR(20) NULL,
	Reg_Name  VARCHAR(20) NULL,
	Nama_Class  VARCHAR(20) NULL,
	Nama_Assembly  VARCHAR(20) NULL
)
;



ALTER TABLE Daftar_Register
	ADD  PRIMARY KEY (Reg_Type,Reg_Name)
;



CREATE TABLE Jadwal_Broadcast
(
	Id_Jadwal  INTEGER NULL,
	Pengulangan_Max  INTEGER NULL,
	Pengulangan_Hitung  CHAR(18) NULL,
	Pengulangan_Jeda_Hari  CHAR(18) NULL,
	Waktu_Eksekusi_Berikut  DATE NULL,
	Waktu_Eksekusi_Terakhir  DATE NULL,
	Status  VARCHAR(20) NULL,
	Reg_Name  VARCHAR(20) NOT NULL,
	Reg_Type  VARCHAR(20) NOT NULL
)
;



ALTER TABLE Jadwal_Broadcast
	ADD  PRIMARY KEY (Id_Jadwal)
;



CREATE TABLE Menu
(
	Menu_ID  VARCHAR(20) NULL,
	M_Name  VARCHAR(20) NULL,
	M_Description  CHAR(18) NULL,
	M_Price  CHAR(18) NULL,
	M_Type  CHAR(18) NULL,
	M_Category  CHAR(18) NULL
)
;



ALTER TABLE Menu
	ADD  PRIMARY KEY (Menu_ID)
;



CREATE TABLE Menu_Schedule
(
	Menu_Schedule_Id  CHAR(18) NULL,
	MS_Date  CHAR(18) NULL,
	MS_Menu_A  CHAR(18) NULL,
	MS_Menu_B  CHAR(18) NULL,
	MS_Menu_C  CHAR(18) NULL
)
;



ALTER TABLE Menu_Schedule
	ADD  PRIMARY KEY (Menu_Schedule_Id)
;



CREATE TABLE Sms_Input
(
	Id_Input  INTEGER NULL,
	Tanggal_Terima  DATE NULL,
	No_Pengirim  VARCHAR(20) NULL,
	Pesan_Teks  VARCHAR(20) NULL,
	Status  VARCHAR(20) NULL
)
;



ALTER TABLE Sms_Input
	ADD  PRIMARY KEY (Id_Input)
;



CREATE TABLE Sms_Output
(
	Id_Output  INTEGER NULL,
	Waktu_Diproses  DATE NULL,
	Waktu_Kirim  DATE NULL,
	No_Tujuan  VARCHAR(20) NULL,
	Pesan_Teks  CHAR(18) NULL,
	Status  VARCHAR(20) NULL,
	Reg_Name  VARCHAR(20) NOT NULL,
	Reg_Type  VARCHAR(20) NOT NULL,
	Id_Input  INTEGER NOT NULL
)
;



ALTER TABLE Sms_Output
	ADD  PRIMARY KEY (Id_Output)
;



ALTER TABLE Customer_Order
	ADD FOREIGN KEY R_1 (Menu_Schedule_Id) REFERENCES Menu_Schedule(Menu_Schedule_Id)
;


ALTER TABLE Customer_Order
	ADD FOREIGN KEY R_5 (Customer_ID) REFERENCES Customer_Profile(Customer_ID)
;


ALTER TABLE Customer_Order
	ADD FOREIGN KEY R_11 (Id_Input) REFERENCES Sms_Input(Id_Input)
;



ALTER TABLE Customer_Order_Detail
	ADD FOREIGN KEY R_3 (Menu_ID) REFERENCES Menu(Menu_ID)
;


ALTER TABLE Customer_Order_Detail
	ADD FOREIGN KEY R_6 (Menu_Schedule_Customer_S) REFERENCES Customer_Order(Menu_Schedule_Customer_S)
;


ALTER TABLE Customer_Order_Detail
	ADD FOREIGN KEY R_12 (Id_Input) REFERENCES Sms_Input(Id_Input)
;



ALTER TABLE Jadwal_Broadcast
	ADD FOREIGN KEY R_7 (Reg_Type,Reg_Name) REFERENCES Daftar_Register(Reg_Type,Reg_Name)
;



ALTER TABLE Sms_Output
	ADD FOREIGN KEY R_8 (Reg_Type,Reg_Name) REFERENCES Daftar_Register(Reg_Type,Reg_Name)
;


ALTER TABLE Sms_Output
	ADD FOREIGN KEY R_9 (Id_Input) REFERENCES Sms_Input(Id_Input)
;


