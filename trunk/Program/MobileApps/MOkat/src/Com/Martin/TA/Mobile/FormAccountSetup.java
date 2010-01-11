/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package Com.Martin.TA.Mobile;

import javax.microedition.lcdui.Alert;
import javax.microedition.lcdui.Command;
import javax.microedition.lcdui.CommandListener;
import javax.microedition.lcdui.Display;
import javax.microedition.lcdui.Displayable;
import javax.microedition.lcdui.Form;
import javax.microedition.lcdui.StringItem;
import javax.microedition.lcdui.TextField;
import javax.microedition.rms.RecordStoreException;

/**
 *
 * @author joshua
 */
public class FormAccountSetup extends Form implements CommandListener, Runnable {

    private Display display;
    private MOkat midlet;
    private Alert alert;
    private Thread thread;
    private final Command cmdKembali = new Command("Back", Command.BACK, 1);
    private final Command cmdKirim = new Command("Save Account", Command.SCREEN, 2);
    private TextField custId = new TextField("Customer ID", "", 11, TextField.NUMERIC);

    public FormAccountSetup(MOkat midlet, Display display) {
        super("Form Setup Account");
        this.midlet = midlet;
        this.display = display;
        this.alert = new Alert(null);
        this.alert.setTimeout(Alert.FOREVER);

        StringItem info = new StringItem("", "", StringItem.LAYOUT_LEFT);
        info.setText("Setup Account");

        AppRecord apr = new AppRecord();
        String customer = apr.ReadCustomerId();
        apr.close();
        this.custId.setString(customer);

        this.append(info);
        this.append(custId);
        this.addCommand(cmdKembali);
        this.addCommand(cmdKirim);
        this.setCommandListener(this);
        this.display.setCurrent(this);
    }

    public void commandAction(Command command, Displayable d) {
        if (command == cmdKembali) {
            this.display.setCurrent(this.midlet.list);
        } else if (command == cmdKirim) {
            thread = new Thread(this);
            thread.start();
        }
    }

    public void run() {
        String customer = this.custId.getString();
        AppRecord apr = new AppRecord();
        boolean hasil = apr.SaveCustomerID(customer);
        apr.close();
        if (hasil) {
            this.alert.setString("Data berhasil disimpan");
            this.display.setCurrent(alert, this);
        } else {
            this.alert.setString("Data gagal disimpan");
            this.display.setCurrent(alert, this);
        }
    }
}
