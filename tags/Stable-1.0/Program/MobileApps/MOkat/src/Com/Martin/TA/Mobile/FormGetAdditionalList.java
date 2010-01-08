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
import javax.wireless.messaging.MessageConnection;
import javax.wireless.messaging.TextMessage;

/**
 *
 * @author UserXP
 */
public class FormGetAdditionalList extends Form implements CommandListener, Runnable {
    private Display display;
    private MOkat midlet;
    private Thread thread;
    private String textsms = "";
    private String nodest = "081510649790";
    private final Command cmdKembali = new Command("Back", Command.BACK, 1);
    private final Command cmdKirim = new Command("Send Request", Command.SCREEN, 2);

    private String[] listCategory = {"FOOD", "DRINKS", "SNACKS", "DESSERT"};
    private ChoiceGroup typeCategory = new ChoiceGroup("Category", Choice.EXCLUSIVE, listCategory, null);
    
    public FormGetAdditionalList(MOkat midlet, Display display){
        super("Form Additional Menu List");

        this.midlet = midlet;
        this.display = display;

        StringItem info = new StringItem("", "", StringItem.LAYOUT_LEFT);
        info.setText("Get Additional Menu List");

        this.append(info);
        this.append(typeCategory);
        this.addCommand(cmdKembali);
        this.addCommand(cmdKirim);
        this.setCommandListener(this);
        this.display.setCurrent(this);
    }

    public void commandAction(Command command, Displayable d) {
        if (command == cmdKembali) {
            this.display.setCurrent(this.midlet.list);
        } else if (command == cmdKirim) {
            this.textsms = "ADDT;GET;" + this.typeCategory.getString(this.typeCategory.getSelectedIndex());
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
