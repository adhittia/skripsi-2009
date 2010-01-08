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
public class FormUpdateMenu extends Form implements CommandListener, Runnable {
    private Display display;
    private MKAdmin midlet;
    private Thread thread;
    private String textsms = "";
    private String nodest = "081510649790";
    private final Command cmdKembali = new Command("Back", Command.BACK, 1);
    private final Command cmdKirim = new Command("Send Request", Command.SCREEN, 2);
    
    private TextField menuId = new TextField("Menu ID", "", 11, TextField.NUMERIC);
    private TextField nameMenu = new TextField("Menu Name", "", 20, TextField.ANY);
    private TextField price = new TextField("Price", "", 11, TextField.NUMERIC);

    private String[] listType = {"MAIN", "ADDITIONAL"};
    private ChoiceGroup typeMenu = new ChoiceGroup("Type", Choice.POPUP, listType, null);

    private String[] listCategory = {"FOOD", "DRINKS", "SNACKS", "DESSERT"};
    private ChoiceGroup typeCategory = new ChoiceGroup("Category", Choice.POPUP, listCategory, null);

    public FormUpdateMenu(MKAdmin midlet, Display display){
        super("Form Update Menu");

        this.midlet = midlet;
        this.display = display;

        StringItem info = new StringItem("", "", StringItem.LAYOUT_LEFT);
        info.setText("Update Menu.");

        this.append(info);
        this.append(nameMenu);
        this.append(typeMenu);
        this.append(typeCategory);
        this.append(price);
        this.addCommand(cmdKembali);
        this.addCommand(cmdKirim);
        this.setCommandListener(this);
        this.display.setCurrent(this);
    }

    public void commandAction(Command command, Displayable d) {
        if (command == cmdKembali) {
            this.display.setCurrent(this.midlet.list);
        } else if (command == cmdKirim) {
            this.textsms = "MENU;CHG;"+  this.typeMenu.getString(this.typeMenu.getSelectedIndex()) + ";" + this.typeCategory.getString(this.typeCategory.getSelectedIndex()) + ";" + this.price.getString() + ";" + this.menuId.getString();
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
