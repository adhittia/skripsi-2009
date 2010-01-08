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
 * @author Martin
 */
public class MGolf extends MIDlet implements CommandListener{
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
        this.list = new List("Menu M-Golf", Choice.IMPLICIT);
        this.list.append("Info Course", img);
        this.list.append("Info Course Top Score", img);
        this.list.append("Info Top Score", img);
        this.list.append("Info Statistic", img);
        this.list.append("Create Game", img);
        this.list.append("Add Score", img);
        this.list.append("Add Stroke", img);
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
                    FormCourse frmCourse = new FormCourse(this, this.display);
                    this.display.setCurrent(frmCourse);
                    break;
                case 1:
                    FormTrCourse frmTrCourse = new FormTrCourse(this, this.display);
                    this.display.setCurrent(frmTrCourse);
                    break;
                case 2:
                    FormTopScore frmTopScore = new FormTopScore(this, this.display);
                    this.display.setCurrent(frmTopScore);
                    break;
                case 3:
                    FormStatistic frmStatistic = new FormStatistic(this, this.display);
                    this.display.setCurrent(frmStatistic);
                    break;
                case 4:
                    FormCreateGame frmCreateGame = new FormCreateGame(this, this.display);
                    this.display.setCurrent(frmCreateGame);
                    break;
                case 5:
                    FormAddScore frmAddScore = new FormAddScore(this, this.display);
                    this.display.setCurrent(frmAddScore);
                    break;
                case 6:
                    FormAddStroke frmAddStroke = new FormAddStroke(this, this.display);
                    this.display.setCurrent(frmAddStroke);
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
