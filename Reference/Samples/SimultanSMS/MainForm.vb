Imports ATSMS
Imports ATSMS.SMS
Imports ATSMS.Common
Imports System.Data.Odbc
Public Class MainForm
    Dim conn As New System.Data.Odbc.OdbcConnection("Dsn=sms_center")
    Dim phone_connected As Boolean
    Private WithEvents oGsmModem As New GSMModem

    Private Sub MainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckForIllegalCrossThreadCalls = False
        cboComPort.SelectedIndex = 3
        oGsmModem.AutoDeleteNewMessage = True
    End Sub

    
    Private Sub btnPhone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConnect.Click

        If cboComPort.Text = String.Empty Then
            MsgBox("COM Port must be selected", MsgBoxStyle.Information)
            Return
        End If

        oGsmModem.Port = cboComPort.Text

        If cboBaudRate.Text <> String.Empty Then
            oGsmModem.BaudRate = Convert.ToInt32(cboBaudRate.Text)
        End If

        If cboDataBit.Text <> String.Empty Then
            oGsmModem.DataBits = Convert.ToInt32(cboDataBit.Text)
        End If

        If cboStopBit.Text <> String.Empty Then
            Select Case cboStopBit.Text
                Case "1"
                    oGsmModem.StopBits = Common.EnumStopBits.One
                Case "1.5"
                    oGsmModem.StopBits = Common.EnumStopBits.OnePointFive
                Case "2"
                    oGsmModem.StopBits = Common.EnumStopBits.Two
            End Select
        End If

        If cboFlowControl.Text <> String.Empty Then
            Select Case cboFlowControl.Text
                Case "None"
                    oGsmModem.FlowControl = Common.EnumFlowControl.None
                Case "Hardware"
                    oGsmModem.FlowControl = Common.EnumFlowControl.RTS_CTS
                Case "Xon/Xoff"
                    oGsmModem.FlowControl = Common.EnumFlowControl.Xon_Xoff
            End Select
        End If

        Try
            oGsmModem.Connect()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            Return
        End Try

        Try
            oGsmModem.NewMessageIndication = True
        Catch ex As Exception

        End Try

        btnSendMsg.Enabled = True
        btnSendClass2Msg.Enabled = True
        btnCheckPhone.Enabled = True
        btnDisconnect.Enabled = True

        MsgBox("Connected to phone successfully !", MsgBoxStyle.Information)
        phone_connected = True
    End Sub


    Private Sub btnDisconnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDisconnect.Click
        Try
            oGsmModem.Disconnect()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try

        btnSendMsg.Enabled = False
        btnSendClass2Msg.Enabled = False
        btnCheckPhone.Enabled = False
        btnDisconnect.Enabled = False
        phone_connected = False
        btnConnect.Enabled = True

    End Sub

   
    Private Sub btnSendMsg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendMsg.Click
        If txtPhoneNumber.Text.Trim = String.Empty Then
            MsgBox("Phone number must not be empty", MsgBoxStyle.Critical)
            Return
        End If

        If txtMsg.Text.Trim = String.Empty Then
            MsgBox("Phone number must not be empty", MsgBoxStyle.Critical)
            Return
        End If

        Try
            Dim msg As String = txtMsg.Text.Trim
            Dim msgNo As String
            If StringUtils.IsUnicode(msg) Then
                msgNo = oGsmModem.SendSMS(txtPhoneNumber.Text, msg, Common.EnumEncoding.Unicode_16Bit)
            Else
                msgNo = oGsmModem.SendSMS(txtPhoneNumber.Text, msg, Common.EnumEncoding.GSM_Default_7Bit)
            End If
            'MsgBox("Message is sent. Reference no is " & msgNo, MsgBoxStyle.Information)
            Dim cmd As New System.Data.Odbc.OdbcCommand("insert into sent values ('" & get_current_time() & "','server','" & txtPhoneNumber.Text & "','" & msg & "','" & msgNo & "')", conn)
            conn.Open()
            cmd.ExecuteReader()
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message & ". Make sure your SIM memory is not full.", MsgBoxStyle.Critical)
        End Try

        'Try
        '    Dim storages() As Storage = oGsmModem.GetStorageSetting
        '    Dim i As Integer
        '    txtStorage.Text = String.Empty
        '    For i = 0 To storages.Length - 1
        '        Dim storage As Storage = storages(i)
        '        txtStorage.Text += storage.Name & "(" & storage.Used & "/" & storage.Total & "), "
        '    Next
        'Catch ex As Exception
        '    txtStorage.Text = "Not supported"
        'End Try
    End Sub

    Private Sub btnSendClass2Msg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendClass2Msg.Click
        If txtPhoneNumber.Text.Trim = String.Empty Then
            MsgBox("Phone number must not be empty", MsgBoxStyle.Critical)
            Return
        End If

        If txtMsg.Text.Trim = String.Empty Then
            MsgBox("Phone number must not be empty", MsgBoxStyle.Critical)
            Return
        End If

        Try
            Dim msg As String = txtMsg.Text.Trim
            Dim msgNo As String
            If StringUtils.IsUnicode(msg) Then
                msgNo = oGsmModem.SendSMS(txtPhoneNumber.Text, msg, Common.EnumEncoding.Unicode_16Bit)
            Else
                msgNo = oGsmModem.SendSMS(txtPhoneNumber.Text, msg, Common.EnumEncoding.Class2_7_Bit)
            End If
            MsgBox("Message is sent. Reference no is " & msgNo, MsgBoxStyle.Information)
            Dim cmd As New System.Data.Odbc.OdbcCommand("insert into sent values ('" & get_current_time() & "','" & txtPhoneNumber.Text & "','" & msg & "','" & msgNo & "')", conn)
            conn.Open()
            cmd.ExecuteReader()
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message & ". Make sure your SIM memory is not full.", MsgBoxStyle.Critical)
        End Try

        'Try
        '    Dim storages() As Storage = oGsmModem.GetStorageSetting
        '    Dim i As Integer
        '    txtStorage.Text = String.Empty
        '    For i = 0 To storages.Length - 1
        '        Dim storage As Storage = storages(i)
        '        txtStorage.Text += storage.Name & "(" & storage.Used & "/" & storage.Total & "), "
        '    Next
        'Catch ex As Exception
        '    txtStorage.Text = "Not supported"
        'End Try
    End Sub


    Private Sub oGsmModem_NewMessageReceived(ByVal e As ATSMS.NewMessageReceivedEventArgs) Handles oGsmModem.NewMessageReceived
        txtMsg.Text = "Message from " & e.MSISDN & ". Message - " & e.TextMessage & ControlChars.CrLf
        Dim dbstore_nemwsg As New System.Data.Odbc.OdbcCommand("insert into received values ('" & get_current_time() & "','" & e.MSISDN & "','" & e.TextMessage & ControlChars.CrLf & "')", conn)
        Dim dbstore_autoreply As New System.Data.Odbc.OdbcCommand("insert into outbox values ('" & get_current_time() & "','server','" & e.MSISDN & "','Terima Kasih Atas SMS yang Anda Kirimkan.')", conn)
        conn.Open()
        dbstore_nemwsg.ExecuteReader()
        dbstore_autoreply.ExecuteReader()
        conn.Close()
    End Sub

    Private Sub btnCheckPhone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckPhone.Click
        MsgBox("Going to analyze your phone. It may take a while", MsgBoxStyle.Information)
        oGsmModem.CheckATCommands()
        If oGsmModem.ATCommandHandler.Is_SMS_Received_Supported Then
            MsgBox("Your phone is able to receive SMS. Message indication command is " & oGsmModem.ATCommandHandler.MsgIndication, MsgBoxStyle.Information)
            oGsmModem.NewMessageIndication = True
        Else
            MsgBox("Sorry. Your phone cannot receive SMS", MsgBoxStyle.Information)
        End If
    End Sub

    Public Function get_current_time() As String
        Dim currtime As String = Now.Year.ToString & "-" & Now.Month.ToString & "-" & Now.Day.ToString & " " & Now.Hour.ToString & ":" & Now.Minute.ToString & ":" & Now.Second.ToString
        get_current_time = currtime
    End Function

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If phone_connected Then
            Timer1.Enabled = False
            conn.Open()
            Dim cmdqueue As New OdbcCommand("select * from queue", conn)
            Dim drQueue As OdbcDataReader = cmdqueue.ExecuteReader
            If drQueue.HasRows Then
            Else
                Dim cmd As New OdbcCommand("select * from outbox", conn)
                Dim dr As OdbcDataReader = cmd.ExecuteReader
                If dr.HasRows Then
                    While dr.Read
                        Dim cmd3 As New System.Data.Odbc.OdbcCommand("insert into queue values ('" & get_current_time() & "','" & dr(1) & "','" & dr(2) & "','" & dr(3) & "')", conn)
                        cmd3.ExecuteReader()
                    End While
                    Dim cmd5 As New System.Data.Odbc.OdbcCommand("delete from outbox", conn)
                    cmd5.ExecuteReader()
                    send_queue()
                End If
            End If
            conn.Close()
            Timer1.Enabled = True
        End If
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Timer2.Enabled = False
        conn.Open()
        Dim cmdSchedule As New OdbcCommand("select * from schedule where done='0'", conn)
        Dim drSchedule As OdbcDataReader = cmdSchedule.ExecuteReader
        Dim hari_ini As Date = Now
        If drSchedule.HasRows Then
            While drSchedule.Read()
                If drSchedule("tahun") = hari_ini.Year.ToString Or drSchedule("tahun") = "*" Then
                    If drSchedule("bulan") = hari_ini.Month.ToString("00") Or drSchedule("bulan") = "*" Then
                        If drSchedule("hari") = hari_ini.Day.ToString("00") Or drSchedule("hari") = "*" Then
                            If drSchedule("jam") = hari_ini.Hour.ToString("00") Or drSchedule("jam") = "*" Then
                                If drSchedule("menit") = hari_ini.Minute.ToString("00") Or drSchedule("menit") = "*" Then
                                    txtMsg.Text += " " + hari_ini.ToString
                                    Dim copy2outbox As New System.Data.Odbc.OdbcCommand("insert into outbox values ('" & get_current_time() & "','" & drSchedule("sender").ToString & "','" & drSchedule("phone_number").ToString & "','" & drSchedule("message").ToString & "')", conn)
                                    copy2outbox.ExecuteReader()
                                End If
                            End If
                        End If
                    End If
                End If
            End While
        End If
        conn.Close()
        'Dim outbox As Integer
        'Try
        '    Dim storages() As Storage = oGsmModem.GetStorageSetting
        '    Dim i As Integer
        '    txtStorage.Text = String.Empty
        '    For i = 0 To storages.Length - 1
        '        Dim storage As Storage = storages(i)
        '        outbox = storage.Used
        '        txtStorage.Text = storage.Name & "(" & outbox & "/" & storage.Total & ")"
        '    Next
        'Catch ex As Exception
        '    txtStorage.Text = "Not supported"
        'End Try
        'If oGsmModem.IsConnected And outbox > 1 Then
        '    Dim msgStore As MessageStore = oGsmModem.MessageStore
        '    oGsmModem.MessageMemory = EnumMessageMemory.SM
        '    msgStore.Refresh()
        '    Dim j As Integer
        '    j = msgStore.Count
        '    For j = 0 To msgStore.Count - 1
        '        Dim sms As SMSMessage = msgStore.Message(j)
        '        sms.Delete()
        '    Next
        'End If
        Timer2.Enabled = True
    End Sub
    Public Sub send_queue()
        Dim cmd As New OdbcCommand("select * from queue", conn)
        Dim dr As OdbcDataReader = cmd.ExecuteReader
        If dr.HasRows Then
            While dr.Read()
                Dim msg As String = dr(3).Trim
                Dim msgNo As String = "SIM full"
                Try
                    If StringUtils.IsUnicode(msg) Then
                        msgNo = oGsmModem.SendSMS(dr(2), msg, Common.EnumEncoding.Unicode_16Bit)
                    Else
                        msgNo = oGsmModem.SendSMS(dr(2), msg, Common.EnumEncoding.GSM_Default_7Bit)
                    End If
                    Dim cmd2 As New System.Data.Odbc.OdbcCommand("insert into sent values ('" & get_current_time() & "','" & dr(1) & "','" & dr(2) & "','" & msg & "','" & msgNo & "')", conn)
                    cmd2.ExecuteReader()
                Catch ex As Exception
                    txtMsg.Text = ex.Message & ". Make sure your SIM memory is not full."
                    Dim cmd3 As New System.Data.Odbc.OdbcCommand("insert into unsent values ('" & get_current_time() & "','" & dr(1) & "','" & dr(2) & "','" & msg & "','" & msgNo & "')", conn)
                    cmd3.ExecuteReader()
                End Try
            End While
        End If
        If oGsmModem.IsConnected Then
            Dim msgStore As MessageStore = oGsmModem.MessageStore
            oGsmModem.MessageMemory = EnumMessageMemory.SM
            msgStore.Refresh()
            Dim j As Integer
            j = msgStore.Count
            For j = 0 To msgStore.Count - 1
                Dim sms As SMSMessage = msgStore.Message(j)
                sms.Delete()
            Next
        End If
        Dim cmd4 As New System.Data.Odbc.OdbcCommand("delete from queue", conn)
        cmd4.ExecuteReader()
    End Sub
End Class
