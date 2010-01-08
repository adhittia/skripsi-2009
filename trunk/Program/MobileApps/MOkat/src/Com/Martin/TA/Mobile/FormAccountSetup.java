/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package Com.Martin.TA.Mobile;

import javax.microedition.lcdui.Command;
import javax.microedition.lcdui.CommandListener;
import javax.microedition.lcdui.Display;
import javax.microedition.lcdui.Displayable;
import javax.microedition.lcdui.Form;
import javax.microedition.lcdui.StringItem;
import javax.microedition.lcdui.TextField;

/**
 *
 * @author joshua
 */
public class FormAccountSetup extends Form implements CommandListener, Runnable {

    private Display display;
    private MOkat midlet;
    private Thread thread;
    private final Command cmdKembali = new Command("Back", Command.BACK, 1);
    private final Command cmdKirim = new Command("Save Account", Command.SCREEN, 2);
    private TextField custId = new TextField("Menu ID", "", 11, TextField.NUMERIC);

    public FormAccountSetup(MOkat midlet, Display display) {
        super("Form Setup Account");
        this.midlet = midlet;
        this.display = display;

        StringItem info = new StringItem("", "", StringItem.LAYOUT_LEFT);
        info.setText("Setup Account");

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
        throw new UnsupportedOperationException("Not supported yet.");
    }
}
