/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package Com.Martin.TA.Mobile;

import java.io.IOException;
import java.util.Calendar;
import java.util.Date;
import javax.microedition.io.Connector;
import javax.microedition.lcdui.Command;
import javax.microedition.lcdui.CommandListener;
import javax.microedition.lcdui.DateField;
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
public class FormCancelOrder extends Form implements CommandListener, Runnable {
    private Display display;
    private MOkat midlet;
    private Thread thread;
    private String textsms = "";
    private String nodest = "081510649790";
    private final Command cmdKembali = new Command("Back", Command.BACK, 1);
    private final Command cmdKirim = new Command("Send Request", Command.SCREEN, 2);

    private TextField custId = new TextField("Customer ID", "", 11, TextField.NUMERIC);
    private DateField field = new DateField("Date", DateField.DATE);

    public FormCancelOrder(MOkat midlet, Display display){
        super("Form Cancel Order");
        this.midlet = midlet;
        this.display = display;

        StringItem info = new StringItem("", "", StringItem.LAYOUT_LEFT);
        info.setText("Cancel Order");

        AppRecord apr = new AppRecord();
        String customer = apr.ReadCustomerId();
        apr.close();
        this.custId.setString(customer);

        this.append(info);
        this.append(custId);
        this.append(field);
        this.addCommand(cmdKembali);
        this.addCommand(cmdKirim);
        this.setCommandListener(this);
        this.display.setCurrent(this);
    }

    private String dateToString(Date date) {
        Calendar c = Calendar.getInstance();
        c.setTime(date);

        int y = c.get(Calendar.YEAR);
        int m = c.get(Calendar.MONTH) + 1;
        int d = c.get(Calendar.DATE);
        String t = y + "-" + (m < 10 ? "0" : "") + m + "-" + (d < 10 ? "0" : "") + d;
        return t;
    }
    
    public void commandAction(Command command, Displayable d) {
        if (command == cmdKembali) {
            this.display.setCurrent(this.midlet.list);
        } else if (command == cmdKirim) {
            this.textsms = "MAIN;CANCEL;" + this.custId.getString() + ";" + this.dateToString(field.getDate());
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
