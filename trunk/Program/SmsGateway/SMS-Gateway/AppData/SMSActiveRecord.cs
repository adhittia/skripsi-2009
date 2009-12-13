﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4016
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SMS_Gateway.AppData {
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using Castle.ActiveRecord;
    
    
    [ActiveRecord("customer_order", Schema="catering")]
    public partial class CustomerOrder : ActiveRecordBase<CustomerOrder> {
        
        private int _comSelected;
        
        private string _comOrderStatus;
        
        private string _comOrderDate;
        
        private int _menuScheduleCustomerS;
        
        [Property("COM_Selected", ColumnType="Int32")]
        public virtual int ComSelected {
            get {
                return this._comSelected;
            }
            set {
                this._comSelected = value;
            }
        }
        
        [Property("COM_Order_Status", ColumnType="String")]
        public virtual string ComOrderStatus {
            get {
                return this._comOrderStatus;
            }
            set {
                this._comOrderStatus = value;
            }
        }
        
        [Property("COM_Order_Date", ColumnType="String")]
        public virtual string ComOrderDate {
            get {
                return this._comOrderDate;
            }
            set {
                this._comOrderDate = value;
            }
        }
        
        [PrimaryKey(PrimaryKeyType.Native, "Menu_Schedule_Customer_S", ColumnType="Int32")]
        public virtual int MenuScheduleCustomerS {
            get {
                return this._menuScheduleCustomerS;
            }
            set {
                this._menuScheduleCustomerS = value;
            }
        }
    }
    
    [ActiveRecord("sms_output", Schema="catering")]
    public partial class SmsOutput : ActiveRecordBase<SmsOutput> {
        
        private System.DateTime _waktuDiproses;
        
        private System.DateTime _waktuKirim;
        
        private string _noTujuan;
        
        private string _pesanTeks;
        
        private string _status;
        
        private string _idOutput;
        
        [Property("Waktu_Diproses", ColumnType="DateTime")]
        public virtual System.DateTime WaktuDiproses {
            get {
                return this._waktuDiproses;
            }
            set {
                this._waktuDiproses = value;
            }
        }
        
        [Property("Waktu_Kirim", ColumnType="DateTime")]
        public virtual System.DateTime WaktuKirim {
            get {
                return this._waktuKirim;
            }
            set {
                this._waktuKirim = value;
            }
        }
        
        [Property("No_Tujuan", ColumnType="String")]
        public virtual string NoTujuan {
            get {
                return this._noTujuan;
            }
            set {
                this._noTujuan = value;
            }
        }
        
        [Property("Pesan_Teks", ColumnType="String")]
        public virtual string PesanTeks {
            get {
                return this._pesanTeks;
            }
            set {
                this._pesanTeks = value;
            }
        }
        
        [Property("Status", ColumnType="String")]
        public virtual string Status {
            get {
                return this._status;
            }
            set {
                this._status = value;
            }
        }
        
        [PrimaryKey(PrimaryKeyType.Native, "Id_Output", ColumnType="String")]
        public virtual string IdOutput {
            get {
                return this._idOutput;
            }
            set {
                this._idOutput = value;
            }
        }
    }
    
    [ActiveRecord("customer_order_detail", Schema="catering")]
    public partial class CustomerOrderDetail : ActiveRecordBase<CustomerOrderDetail> {
        
        private string _price;
        
        private int _additionalOrderS;
        
        [Property("Price", ColumnType="String")]
        public virtual string Price {
            get {
                return this._price;
            }
            set {
                this._price = value;
            }
        }
        
        [PrimaryKey(PrimaryKeyType.Native, "Additional_Order_S", ColumnType="Int32")]
        public virtual int AdditionalOrderS {
            get {
                return this._additionalOrderS;
            }
            set {
                this._additionalOrderS = value;
            }
        }
    }
    
    [ActiveRecord("customer_profile", Schema="catering")]
    public partial class CustomerProfile : ActiveRecordBase<CustomerProfile> {
        
        private string _cpName;
        
        private string _cpDeliveryAddress;
        
        private string _cpBillAddress;
        
        private string _cpMobileNumber;
        
        private string _cpEmail;
        
        private int _customerId;
        
        [Property("CP_Name", ColumnType="String")]
        public virtual string CpName {
            get {
                return this._cpName;
            }
            set {
                this._cpName = value;
            }
        }
        
        [Property("CP_Delivery_Address", ColumnType="String")]
        public virtual string CpDeliveryAddress {
            get {
                return this._cpDeliveryAddress;
            }
            set {
                this._cpDeliveryAddress = value;
            }
        }
        
        [Property("CP_Bill_Address", ColumnType="String")]
        public virtual string CpBillAddress {
            get {
                return this._cpBillAddress;
            }
            set {
                this._cpBillAddress = value;
            }
        }
        
        [Property("CP_Mobile_Number", ColumnType="String")]
        public virtual string CpMobileNumber {
            get {
                return this._cpMobileNumber;
            }
            set {
                this._cpMobileNumber = value;
            }
        }
        
        [Property("CP_Email", ColumnType="String")]
        public virtual string CpEmail {
            get {
                return this._cpEmail;
            }
            set {
                this._cpEmail = value;
            }
        }
        
        [PrimaryKey(PrimaryKeyType.Native, "Customer_ID", ColumnType="Int32")]
        public virtual int CustomerId {
            get {
                return this._customerId;
            }
            set {
                this._customerId = value;
            }
        }
    }
    
    [ActiveRecord("daftar_register", Schema="catering")]
    public partial class DaftarRegister : ActiveRecordBase<DaftarRegister> {
        
        private string _namaClass;
        
        private string _namaAssembly;
        
        private DaftarRegisterCompositeKey _daftarRegisterCompositeKey;
        
        [Property("Nama_Class", ColumnType="String")]
        public virtual string NamaClass {
            get {
                return this._namaClass;
            }
            set {
                this._namaClass = value;
            }
        }
        
        [Property("Nama_Assembly", ColumnType="String")]
        public virtual string NamaAssembly {
            get {
                return this._namaAssembly;
            }
            set {
                this._namaAssembly = value;
            }
        }
        
        [CompositeKey()]
        public virtual DaftarRegisterCompositeKey DaftarRegisterCompositeKey {
            get {
                return this._daftarRegisterCompositeKey;
            }
            set {
                this._daftarRegisterCompositeKey = value;
            }
        }
    }
    
    [Serializable()]
    public partial class DaftarRegisterCompositeKey {
        
        private string _regType;
        
        private string _regName;
        
        [KeyProperty(Column="Reg_Type", ColumnType="String", NotNull=true)]
        public virtual string RegType {
            get {
                return this._regType;
            }
            set {
                this._regType = value;
            }
        }
        
        [KeyProperty(Column="Reg_Name", ColumnType="String", NotNull=true)]
        public virtual string RegName {
            get {
                return this._regName;
            }
            set {
                this._regName = value;
            }
        }
        
        public override string ToString() {
            return String.Join(":", new string[] {
                        this._regType.ToString(),
                        this._regName.ToString()});
        }
        
        public override bool Equals(object obj) {
            if ((obj == this)) {
                return true;
            }
            if (((obj == null) 
                        || (obj.GetType() != this.GetType()))) {
                return false;
            }
            DaftarRegisterCompositeKey test = ((DaftarRegisterCompositeKey)(obj));
            return (((_regType == test._regType) 
                        || ((_regType != null) 
                        && _regType.Equals(test._regType))) 
                        && ((_regName == test._regName) 
                        || ((_regName != null) 
                        && _regName.Equals(test._regName))));
        }
        
        public override int GetHashCode() {
            return XorHelper(_regType.GetHashCode(), _regName.GetHashCode());
        }
        
        private int XorHelper(int left, int right) {
            return left ^ right;
        }
    }
    
    [ActiveRecord("jadwal_broadcast", Schema="catering")]
    public partial class JadwalBroadcast : ActiveRecordBase<JadwalBroadcast> {
        
        private int _pengulanganMax;
        
        private int _pengulanganHitung;
        
        private int _pengulanganJedaHari;
        
        private string _waktuEksekusiBerikut;
        
        private string _waktuEksekusiTerakhir;
        
        private string _status;
        
        private int _idJadwal;
        
        [Property("Pengulangan_Max", ColumnType="Int32")]
        public virtual int PengulanganMax {
            get {
                return this._pengulanganMax;
            }
            set {
                this._pengulanganMax = value;
            }
        }
        
        [Property("Pengulangan_Hitung", ColumnType="Int32")]
        public virtual int PengulanganHitung {
            get {
                return this._pengulanganHitung;
            }
            set {
                this._pengulanganHitung = value;
            }
        }
        
        [Property("Pengulangan_Jeda_Hari", ColumnType="Int32")]
        public virtual int PengulanganJedaHari {
            get {
                return this._pengulanganJedaHari;
            }
            set {
                this._pengulanganJedaHari = value;
            }
        }
        
        [Property("Waktu_Eksekusi_Berikut", ColumnType="String")]
        public virtual string WaktuEksekusiBerikut {
            get {
                return this._waktuEksekusiBerikut;
            }
            set {
                this._waktuEksekusiBerikut = value;
            }
        }
        
        [Property("Waktu_Eksekusi_Terakhir", ColumnType="String")]
        public virtual string WaktuEksekusiTerakhir {
            get {
                return this._waktuEksekusiTerakhir;
            }
            set {
                this._waktuEksekusiTerakhir = value;
            }
        }
        
        [Property("Status", ColumnType="String")]
        public virtual string Status {
            get {
                return this._status;
            }
            set {
                this._status = value;
            }
        }
        
        [PrimaryKey(PrimaryKeyType.Native, "Id_Jadwal", ColumnType="Int32")]
        public virtual int IdJadwal {
            get {
                return this._idJadwal;
            }
            set {
                this._idJadwal = value;
            }
        }
    }
    
    [ActiveRecord("menu", Schema="catering")]
    public partial class Menu : ActiveRecordBase<Menu> {
        
        private string _mName;
        
        private string _mDescription;
        
        private decimal _mPrice;
        
        private string _mType;
        
        private string _mCategory;
        
        private int _menuId;
        
        [Property("M_Name", ColumnType="String")]
        public virtual string MName {
            get {
                return this._mName;
            }
            set {
                this._mName = value;
            }
        }
        
        [Property("M_Description", ColumnType="String")]
        public virtual string MDescription {
            get {
                return this._mDescription;
            }
            set {
                this._mDescription = value;
            }
        }
        
        [Property("M_Price", ColumnType="Decimal")]
        public virtual decimal MPrice {
            get {
                return this._mPrice;
            }
            set {
                this._mPrice = value;
            }
        }
        
        [Property("M_Type", ColumnType="String")]
        public virtual string MType {
            get {
                return this._mType;
            }
            set {
                this._mType = value;
            }
        }
        
        [Property("M_Category", ColumnType="String")]
        public virtual string MCategory {
            get {
                return this._mCategory;
            }
            set {
                this._mCategory = value;
            }
        }
        
        [PrimaryKey(PrimaryKeyType.Native, "Menu_ID", ColumnType="Int32")]
        public virtual int MenuId {
            get {
                return this._menuId;
            }
            set {
                this._menuId = value;
            }
        }
    }
    
    [ActiveRecord("menu_schedule", Schema="catering")]
    public partial class MenuSchedule : ActiveRecordBase<MenuSchedule> {
        
        private string _msDate;
        
        private int _msMenuA;
        
        private int _msMenuB;
        
        private int _msMenuC;
        
        private int _menuScheduleId;
        
        [Property("MS_Date", ColumnType="String")]
        public virtual string MsDate {
            get {
                return this._msDate;
            }
            set {
                this._msDate = value;
            }
        }
        
        [Property("MS_Menu_A", ColumnType="Int32")]
        public virtual int MsMenuA {
            get {
                return this._msMenuA;
            }
            set {
                this._msMenuA = value;
            }
        }
        
        [Property("MS_Menu_B", ColumnType="Int32")]
        public virtual int MsMenuB {
            get {
                return this._msMenuB;
            }
            set {
                this._msMenuB = value;
            }
        }
        
        [Property("MS_Menu_C", ColumnType="Int32")]
        public virtual int MsMenuC {
            get {
                return this._msMenuC;
            }
            set {
                this._msMenuC = value;
            }
        }
        
        [PrimaryKey(PrimaryKeyType.Native, "Menu_Schedule_Id", ColumnType="Int32")]
        public virtual int MenuScheduleId {
            get {
                return this._menuScheduleId;
            }
            set {
                this._menuScheduleId = value;
            }
        }
    }
    
    [ActiveRecord("sms_input", Schema="catering")]
    public partial class SmsInput : ActiveRecordBase<SmsInput> {
        
        private System.DateTime _tanggalTerima;
        
        private string _noPengirim;
        
        private string _pesanTeks;
        
        private string _status;
        
        private string _idInput;
        
        [Property("Tanggal_Terima", ColumnType="DateTime")]
        public virtual System.DateTime TanggalTerima {
            get {
                return this._tanggalTerima;
            }
            set {
                this._tanggalTerima = value;
            }
        }
        
        [Property("No_Pengirim", ColumnType="String")]
        public virtual string NoPengirim {
            get {
                return this._noPengirim;
            }
            set {
                this._noPengirim = value;
            }
        }
        
        [Property("Pesan_Teks", ColumnType="String")]
        public virtual string PesanTeks {
            get {
                return this._pesanTeks;
            }
            set {
                this._pesanTeks = value;
            }
        }
        
        [Property("Status", ColumnType="String")]
        public virtual string Status {
            get {
                return this._status;
            }
            set {
                this._status = value;
            }
        }
        
        [PrimaryKey(PrimaryKeyType.Native, "Id_Input", ColumnType="String")]
        public virtual string IdInput {
            get {
                return this._idInput;
            }
            set {
                this._idInput = value;
            }
        }
    }
}
