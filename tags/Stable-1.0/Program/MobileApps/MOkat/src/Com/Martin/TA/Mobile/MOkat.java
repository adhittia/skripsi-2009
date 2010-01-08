/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package Com.Martin.TA.Mobile;

import javax.microedition.lcdui.Choice;
import javax.microedition.lcdui.Command;
import javax.microedition.lcdui.CommandListener;
import javax.microedition.lcdui.Display;
import javax.microedition.lcdui.Displayable;
import javax.microedition.lcdui.Image;
import javax.microedition.lcdui.List;
import javax.microedition.midlet.*;

/**
 * @author UserXP
 */
public class MOkat extends MIDlet implements CommandListener {
    public List list;

    private Display display;
    private Command cmdPilih;
    private Command cmdKembali;
    
    public void startApp() {
        this.initialize();

        Image img;
        try{
            img = Image.createImage(getClass().getResourceAsStream("Resources/file.png"));
        }catch(Exception ex){
            img = null;
        }
        this.list = new List("Menu Mobile Catering", Choice.IMPLICIT);
        this.list.append("Get Menu Schedule", img);
        this.list.append("Get Additional Menu", img);
        this.list.append("Change Schedule Menu", img);
        this.list.append("Order Additional Menu", img);
        this.list.append("Cancel Order", img);
        this.list.append("Get Billing Information", img);
        this.list.append("Update Profile", img);
        this.list.append("Setup Account", img);
        this.list.addCommand(cmdPilih);
        this.list.addCommand(cmdKembali);
        this.list.setCommandListener(this);
        this.display.setCurrent(list);
    }

    public void pauseApp() {
    }

    public void destroyApp(boolean unconditional) {
        notifyDestroyed();
    }

    public void commandAction(Command c, Displayable d) {
        if(c == cmdKembali){
            destroyApp(false);
        }else{
            switch(this.list.getSelectedIndex()){
                case 0:
                    FormGetMenuSchedule frmMenuSch = new FormGetMenuSchedule(this, this.display);
                    this.display.setCurrent(frmMenuSch);
                    break;
                case 1:
                    FormGetAdditionalList frmAddtMenu = new FormGetAdditionalList(this, this.display);
                    this.display.setCurrent(frmAddtMenu);
                    break;
                case 2:
                    FormChangeMenu frmChangeMenu = new FormChangeMenu(this, this.display);
                    this.display.setCurrent(frmChangeMenu);
                    break;
                case 3:
                    FormOrderAdditional frmOrderAddt = new FormOrderAdditional(this, this.display);
                    this.display.setCurrent(frmOrderAddt);
                    break;
                case 4:
                    FormCancelOrder formCancelOrder = new FormCancelOrder(this, this.display);
                    this.display.setCurrent(formCancelOrder);
                    break;
                case 5:
                    FormBillingInformation formBillingInformation = new FormBillingInformation(this, this.display);
                    this.display.setCurrent(formBillingInformation);
                    break;
                case 6:
                    FormCustomerProfile formCustomerProfile = new FormCustomerProfile(this, this.display);
                    this.display.setCurrent(formCustomerProfile);
                    break;
                case 7:
                    FormAccountSetup formAccountSetup = new FormAccountSetup(this, this.display);
                    this.display.setCurrent(formAccountSetup);
                    break;
            }
        }
    }
    
    private void initialize(){
        this.display = Display.getDisplay(this);
        this.cmdPilih = new Command("Pilih", Command.SCREEN, 2);
        this.cmdKembali = new Command("Keluar", Command.EXIT, 1);
    }
}
