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
 *
 * @author joshua
 */
public class FormCancelOrder extends Form implements CommandListener, Runnable {
    
    public FormCancelOrder(MOkat midlet, Display display){
        super("Form Cancel Order");
    }
    
    public void commandAction(Command c, Displayable d) {
        throw new UnsupportedOperationException("Not supported yet.");
    }

    public void run() {
        throw new UnsupportedOperationException("Not supported yet.");
    }

}
