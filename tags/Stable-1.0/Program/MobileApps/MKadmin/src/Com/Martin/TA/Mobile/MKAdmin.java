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
 * @author joshua
 */
public class MKAdmin extends MIDlet implements CommandListener {
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
        this.list = new List("Menu Catering Admin", Choice.IMPLICIT);
        this.list.append("Add Menu", img);
        this.list.append("Update Menu", img);
        this.list.append("Set Menu Schedule", img);
        this.list.append("Send News", img);
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
                    FormAddMenu formAddMenu = new FormAddMenu(this, this.display);
                    this.display.setCurrent(formAddMenu);
                    break;
                case 1:
                    FormUpdateMenu formUpdateMenu = new FormUpdateMenu(this, this.display);
                    this.display.setCurrent(formUpdateMenu);
                    break;
                case 2:
                    FormSetSchedule formSetSchedule = new FormSetSchedule(this, this.display);
                    this.display.setCurrent(formSetSchedule);
                    break;
                case 3:
                    FormSendNews formSendNews = new FormSendNews(this, this.display);
                    this.display.setCurrent(formSendNews);
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
