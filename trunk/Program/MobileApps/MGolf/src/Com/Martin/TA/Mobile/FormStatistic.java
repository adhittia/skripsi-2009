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
 * @author Martin
 */
public class FormStatistic extends Form implements CommandListener, Runnable{
    private Display display;
    private MGolf midlet;
    private Thread thread;
    
    private String textsms = "";
    private String nodest = "085883768065";

    private TextField username = new TextField("username", "ketik username", 15, TextField.ANY);
    private final Command cmdKembali = new Command("Kembali", Command.BACK,1);
    private final Command cmdKirim = new Command("Kirim SMS", Command.SCREEN, 2);

    public FormStatistic(MGolf midlet, Display display){
        super("Info Course");
        this.midlet = midlet;
        this.display = display;

        StringItem info = new StringItem("", "", StringItem.LAYOUT_LEFT);
        info.setText("Dapatkan informasi statistic anggota IJGC.");

        this.append(info);
        this.append(username);
        this.addCommand(cmdKembali);
        this.addCommand(cmdKirim);
        this.setCommandListener(this);
        this.display.setCurrent(this);
    }

    public void commandAction(Command c, Displayable d) {
        if(c == cmdKembali){
            this.display.setCurrent(this.midlet.list);
        }else if(c == cmdKirim){
            this.textsms = "info;mystat;" + this.username.getString();

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
