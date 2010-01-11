/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package Com.Martin.TA.Mobile;

import javax.microedition.rms.InvalidRecordIDException;
import javax.microedition.rms.RecordEnumeration;
import javax.microedition.rms.RecordStore;
import javax.microedition.rms.RecordStoreException;
import javax.microedition.rms.RecordStoreFullException;
import javax.microedition.rms.RecordStoreNotOpenException;

/**
 *
 * @author joshua
 */
public class AppRecord {

    private RecordStore rs = null;

    public AppRecord() {
        try {
            this.rs = RecordStore.openRecordStore("mokat", true);
        } catch (RecordStoreException ex) {
            ex.printStackTrace();
        }
    }
    // Close the record store

    public void close() {
        if (this.rs != null) {
            try {
                this.rs.closeRecordStore();
            } catch (RecordStoreNotOpenException ex) {
                ex.printStackTrace();
            } catch (RecordStoreException ex) {
                ex.printStackTrace();
            }
        }
    }

    public boolean SaveCustomerID(String customerId) {
        if (this.rs != null) {
            byte[] custByte = customerId.getBytes();
            try {
                if (this.rs.getNumRecords() == 0) {
                    this.rs.addRecord(custByte, 0, custByte.length);
                } else {
                    RecordEnumeration re = this.rs.enumerateRecords(null, null, false);
                    int recId = -1;
                    if (re.hasNextElement()) {
                        recId = re.nextRecordId();
                    }
                    if (recId != -1) {
                        rs.setRecord(recId, custByte, 0, custByte.length);
                    }
                }
            } catch (RecordStoreNotOpenException ex) {
                ex.printStackTrace();
                return false;
            } catch (InvalidRecordIDException ex) {
                ex.printStackTrace();
                return false;
            } catch (RecordStoreException ex) {
                ex.printStackTrace();
                return false;
            }
            return true;
        }
        return false;
    }

    public String ReadCustomerId() {
        if (this.rs != null) {
            byte[] custByte = null;
            try {
                RecordEnumeration re = this.rs.enumerateRecords(null, null, false);
                if (re.hasNextElement()) {
                    int recId = re.nextRecordId();
                    custByte = this.rs.getRecord(recId);
                }
            } catch (RecordStoreNotOpenException ex) {
                ex.printStackTrace();
                return "";
            } catch (InvalidRecordIDException ex) {
                ex.printStackTrace();
                return "";
            } catch (RecordStoreException ex) {
                ex.printStackTrace();
                return "";
            }
            if (custByte != null) {
                String result = new String(custByte, 0, custByte.length);
                return result;
            }
        }
        return "";
    }
}
