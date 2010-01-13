/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package Com.Martin.TA.Mobile;

import java.io.IOException;
import javax.microedition.io.Connector;
import javax.microedition.lcdui.Choice;
import javax.microedition.lcdui.ChoiceGroup;
import javax.microedition.lcdui.Command;
import javax.microedition.lcdui.CommandListener;
import javax.microedition.lcdui.Display;
import javax.microedition.lcdui.Displayable;
import javax.microedition.lcdui.Form;
import javax.microedition.lcdui.StringItem;
import javax.microedition.lcdui.TextField;
import javax.wireless.messaging.MessageConnection;
import javax.wireless.messaging.TextMessage;

/**
 *
 * @author joshua
 */
public class FormCustomerProfile extends Form implements CommandListener, Runnable {
    private Display display;
    private MOkat midlet;
    private Thread thread;
    private String textsms = "";
    private String nodest = "081510649790";
    private final Command cmdKembali = new Command("Back", Command.BACK, 1);
    private final Command cmdKirim = new Command("Send Request", Command.SCREEN, 2);

    private TextField custId = new TextField("Customer ID", "", 11, TextField.NUMERIC);
    private TextField value = new TextField("Value", "", 80, TextField.ANY);
    private String[] listCategory = {"Customer Name", "Delivery Address", "Billing Address", "Mobile No", "E-Mail"};
    private ChoiceGroup typeCategory = new ChoiceGroup("Field", Choice.POPUP, listCategory, null);
    
    public FormCustomerProfile(MOkat midlet, Display display){
        super("Form Update Profile");
        this.midlet = midlet;
        this.display = display;

        StringItem info = new StringItem("", "", StringItem.LAYOUT_LEFT);
        info.setText("Update Customer Profile");

        AppRecord apr = new AppRecord();
        String customer = apr.ReadCustomerId();
        apr.close();
        this.custId.setString(customer);

        this.append(info);
        this.append(custId);
        this.append(typeCategory);
        this.append(value);
        this.addCommand(cmdKembali);
        this.addCommand(cmdKirim);
        this.setCommandListener(this);
        this.display.setCurrent(this);
    }

    public void commandAction(Command command, Displayable d) {
        if (command == cmdKembali) {
            this.display.setCurrent(this.midlet.list);
        } else if (command == cmdKirim) {
            switch(typeCategory.getSelectedIndex()){
                case 0:
                    this.textsms = "CUST;PROFILE;"+ this.custId.getString()  + ";CP_NAME;" + this.value.getString();
                    break;
                case 1:
                    this.textsms = "CUST;PROFILE;"+ this.custId.getString()  + ";CP_DELIVERY_ADDRESS;" + this.value.getString();
                    break;
                case 2:
                    this.textsms = "CUST;PROFILE;"+ this.custId.getString()  + ";CP_BILL_ADDRESS;" + this.value.getString();
                    break;
                case 3:
                    this.textsms = "CUST;PROFILE;"+ this.custId.getString()  + ";CP_MOBILE_NUMBER;" + this.value.getString();
                    break;
                case 4:
                    this.textsms = "CUST;PROFILE;"+ this.custId.getString()  + ";CP_EMAIL;" + this.value.getString();
                    break;
            }
            thread = new Thread(this);
            thread.start();
        }
    }

    public void run() {
        MessageConnection conn = null;
        try {
            conn = (MessageConnection) Connector.open("sms://" + this.nodest);
            TextMessage pesan = (TextMessage) conn.newMessage(MessageConnection.TEXT_MESSAGE);

            pesan.setAddress("sms://" + this.nodest);
            pesan.setPayloadText(this.textsms);
            conn.send(pesan);
        } catch (IOException ex) {
            ex.printStackTrace();
        }

        if (conn != null) {
            try {
                conn.close();
            } catch (IOException ex) {
                ex.printStackTrace();
            }
        }
    }

}
