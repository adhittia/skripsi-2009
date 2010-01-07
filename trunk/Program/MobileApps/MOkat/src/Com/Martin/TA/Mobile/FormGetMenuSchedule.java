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

/**
 * @author UserXP
 */
public class FormGetMenuSchedule extends Form implements CommandListener, Runnable {
    /**
     * constructor
     */
    public FormGetMenuSchedule(MOkat midlet, Display display){
        super("Form Menu Schedule List");
    }
   
    /**
     * Called when action should be handled
     */
    public void commandAction(Command command, Displayable displayable) {
    }

    public void run() {
        throw new UnsupportedOperationException("Not supported yet.");
    }

}