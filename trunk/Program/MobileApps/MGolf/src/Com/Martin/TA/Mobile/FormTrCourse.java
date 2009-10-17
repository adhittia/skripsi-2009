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
 * @author Martin
 */
public class FormTrCourse extends Form implements CommandListener, Runnable{
    private Display display;
    private MGolf midlet;
    private Thread thread;

    private String textsms = "";
    private String nodest = "085883768065";
    private String[] listcourse = {"Mentari Golf", "Imperial Club - Karawaci", "Mountain View GC", "Sawangan Golf", "Jakarta GC"};
    private ChoiceGroup course = new ChoiceGroup("Course List", Choice.POPUP, listcourse, null);

    private final Command cmdKembali = new Command("Kembali", Command.BACK,1);
    private final Command cmdKirim = new Command("Kirim SMS", Command.SCREEN, 2);

    public FormTrCourse(MGolf midlet, Display display){
        super("Info Course");
        this.midlet = midlet;
        this.display = display;

        StringItem info = new StringItem("", "", StringItem.LAYOUT_LEFT);
        info.setText("Dapatkan informasi pemain top score pada golf course yang tersedia.");

        this.append(info);
        this.append(course);
        this.addCommand(cmdKembali);
        this.addCommand(cmdKirim);
        this.setCommandListener(this);
        this.display.setCurrent(this);
    }

    public void commandAction(Command c, Displayable d) {
        if(c == cmdKembali){
            this.display.setCurrent(this.midlet.list);
        }else if(c == cmdKirim){
            switch(course.getSelectedIndex()){
                case 0:
                    this.textsms = "info;trcourse;6";
                    break;
                case 1:
                    this.textsms = "info;trcourse;7";
                    break;
                case 2:
                    this.textsms = "info;trcourse;8";
                    break;
                case 3:
                    this.textsms = "info;trcourse;9";
                    break;
                case 4:
                    this.textsms = "info;trcourse;10";
                    break;
            }
            thread = new Thread(this);
            thread.start();
        }
    }

    public void run() {
        MessageConnection conn = null;
        try{
            conn = (MessageConnection) Connector.open("sms://" + this.nodest);
            TextMessage pesan = (TextMessage) conn.newMessage(MessageConnection.TEXT_MESSAGE);

            pesan.setAddress("sms://"+this.nodest);
            pesan.setPayloadText(this.textsms);
            conn.send(pesan);
        }catch(IOException ex){
            ex.printStackTrace();
        }

        if(conn != null){
            try{
                conn.close();
            }catch(IOException ex){
                ex.printStackTrace();
            }
        }
    }

}
