<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.Label1 = New System.Windows.Forms.Label
        Me.cboComPort = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.cboBaudRate = New System.Windows.Forms.ComboBox
        Me.cboDataBit = New System.Windows.Forms.ComboBox
        Me.cboStopBit = New System.Windows.Forms.ComboBox
        Me.cboFlowControl = New System.Windows.Forms.ComboBox
        Me.btnConnect = New System.Windows.Forms.Button
        Me.btnDisconnect = New System.Windows.Forms.Button
        Me.Label27 = New System.Windows.Forms.Label
        Me.txtPhoneNumber = New System.Windows.Forms.TextBox
        Me.Label26 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btnSendClass2Msg = New System.Windows.Forms.Button
        Me.btnCheckPhone = New System.Windows.Forms.Button
        Me.txtStorage = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.btnSendMsg = New System.Windows.Forms.Button
        Me.txtMsg = New System.Windows.Forms.TextBox
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(21, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "COM Port: *"
        '
        'cboComPort
        '
        Me.cboComPort.AutoCompleteCustomSource.AddRange(New String() {"COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9", "COM10", "COM11", "COM12", "COM13", "COM14", "COM15"})
        Me.cboComPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboComPort.FormattingEnabled = True
        Me.cboComPort.Items.AddRange(New Object() {"COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9", "COM10", "COM11", "COM12", "COM13", "COM14", "COM15"})
        Me.cboComPort.Location = New System.Drawing.Point(101, 11)
        Me.cboComPort.Name = "cboComPort"
        Me.cboComPort.Size = New System.Drawing.Size(121, 21)
        Me.cboComPort.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(21, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Baud Rate:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(21, 69)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Data Bit:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(21, 96)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(47, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Stop Bit:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(21, 125)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(68, 13)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Flow Control:"
        '
        'cboBaudRate
        '
        Me.cboBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBaudRate.FormattingEnabled = True
        Me.cboBaudRate.Items.AddRange(New Object() {"1200", "2400", "4800", "9600", "19200", "38400", "57600", "115200"})
        Me.cboBaudRate.Location = New System.Drawing.Point(101, 42)
        Me.cboBaudRate.Name = "cboBaudRate"
        Me.cboBaudRate.Size = New System.Drawing.Size(121, 21)
        Me.cboBaudRate.TabIndex = 6
        '
        'cboDataBit
        '
        Me.cboDataBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDataBit.FormattingEnabled = True
        Me.cboDataBit.Items.AddRange(New Object() {"4", "5", "6", "7", "8"})
        Me.cboDataBit.Location = New System.Drawing.Point(101, 69)
        Me.cboDataBit.Name = "cboDataBit"
        Me.cboDataBit.Size = New System.Drawing.Size(121, 21)
        Me.cboDataBit.TabIndex = 7
        '
        'cboStopBit
        '
        Me.cboStopBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStopBit.FormattingEnabled = True
        Me.cboStopBit.Items.AddRange(New Object() {"1", "1.5", "2"})
        Me.cboStopBit.Location = New System.Drawing.Point(101, 96)
        Me.cboStopBit.Name = "cboStopBit"
        Me.cboStopBit.Size = New System.Drawing.Size(121, 21)
        Me.cboStopBit.TabIndex = 8
        '
        'cboFlowControl
        '
        Me.cboFlowControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFlowControl.FormattingEnabled = True
        Me.cboFlowControl.Items.AddRange(New Object() {"None", "Hardware", "Xon/Xoff"})
        Me.cboFlowControl.Location = New System.Drawing.Point(101, 123)
        Me.cboFlowControl.Name = "cboFlowControl"
        Me.cboFlowControl.Size = New System.Drawing.Size(121, 21)
        Me.cboFlowControl.TabIndex = 9
        '
        'btnConnect
        '
        Me.btnConnect.Location = New System.Drawing.Point(24, 165)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(182, 23)
        Me.btnConnect.TabIndex = 10
        Me.btnConnect.Text = "Connect"
        Me.btnConnect.UseVisualStyleBackColor = True
        '
        'btnDisconnect
        '
        Me.btnDisconnect.Enabled = False
        Me.btnDisconnect.Location = New System.Drawing.Point(24, 204)
        Me.btnDisconnect.Name = "btnDisconnect"
        Me.btnDisconnect.Size = New System.Drawing.Size(182, 23)
        Me.btnDisconnect.TabIndex = 12
        Me.btnDisconnect.Text = "Disconnect"
        Me.btnDisconnect.UseVisualStyleBackColor = True
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(17, 31)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(81, 13)
        Me.Label27.TabIndex = 12
        Me.Label27.Text = "Phone Number:"
        '
        'txtPhoneNumber
        '
        Me.txtPhoneNumber.Location = New System.Drawing.Point(123, 24)
        Me.txtPhoneNumber.Name = "txtPhoneNumber"
        Me.txtPhoneNumber.Size = New System.Drawing.Size(193, 20)
        Me.txtPhoneNumber.TabIndex = 13
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(17, 57)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(53, 13)
        Me.Label26.TabIndex = 14
        Me.Label26.Text = "Message:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnSendClass2Msg)
        Me.GroupBox2.Controls.Add(Me.btnCheckPhone)
        Me.GroupBox2.Controls.Add(Me.txtStorage)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.btnSendMsg)
        Me.GroupBox2.Controls.Add(Me.txtMsg)
        Me.GroupBox2.Controls.Add(Me.Label26)
        Me.GroupBox2.Controls.Add(Me.txtPhoneNumber)
        Me.GroupBox2.Controls.Add(Me.Label27)
        Me.GroupBox2.Location = New System.Drawing.Point(235, 4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(395, 370)
        Me.GroupBox2.TabIndex = 11
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "My Phone"
        '
        'btnSendClass2Msg
        '
        Me.btnSendClass2Msg.Enabled = False
        Me.btnSendClass2Msg.Location = New System.Drawing.Point(123, 330)
        Me.btnSendClass2Msg.Name = "btnSendClass2Msg"
        Me.btnSendClass2Msg.Size = New System.Drawing.Size(182, 23)
        Me.btnSendClass2Msg.TabIndex = 32
        Me.btnSendClass2Msg.Text = "Send Class 2 Message"
        Me.btnSendClass2Msg.UseVisualStyleBackColor = True
        Me.btnSendClass2Msg.Visible = False
        '
        'btnCheckPhone
        '
        Me.btnCheckPhone.Enabled = False
        Me.btnCheckPhone.Location = New System.Drawing.Point(123, 253)
        Me.btnCheckPhone.Name = "btnCheckPhone"
        Me.btnCheckPhone.Size = New System.Drawing.Size(182, 23)
        Me.btnCheckPhone.TabIndex = 31
        Me.btnCheckPhone.Text = "Check Phone"
        Me.btnCheckPhone.UseVisualStyleBackColor = True
        '
        'txtStorage
        '
        Me.txtStorage.Location = New System.Drawing.Point(123, 218)
        Me.txtStorage.Name = "txtStorage"
        Me.txtStorage.ReadOnly = True
        Me.txtStorage.Size = New System.Drawing.Size(193, 20)
        Me.txtStorage.TabIndex = 30
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(17, 225)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(47, 13)
        Me.Label13.TabIndex = 29
        Me.Label13.Text = "Storage:"
        Me.Label13.Visible = False
        '
        'btnSendMsg
        '
        Me.btnSendMsg.Enabled = False
        Me.btnSendMsg.Location = New System.Drawing.Point(123, 292)
        Me.btnSendMsg.Name = "btnSendMsg"
        Me.btnSendMsg.Size = New System.Drawing.Size(182, 23)
        Me.btnSendMsg.TabIndex = 13
        Me.btnSendMsg.Text = "Send Message"
        Me.btnSendMsg.UseVisualStyleBackColor = True
        '
        'txtMsg
        '
        Me.txtMsg.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtMsg.Location = New System.Drawing.Point(123, 57)
        Me.txtMsg.Multiline = True
        Me.txtMsg.Name = "txtMsg"
        Me.txtMsg.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtMsg.Size = New System.Drawing.Size(259, 143)
        Me.txtMsg.TabIndex = 15
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 10000
        '
        'Timer2
        '
        Me.Timer2.Enabled = True
        Me.Timer2.Interval = 60000
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(642, 386)
        Me.Controls.Add(Me.btnDisconnect)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.btnConnect)
        Me.Controls.Add(Me.cboFlowControl)
        Me.Controls.Add(Me.cboStopBit)
        Me.Controls.Add(Me.cboDataBit)
        Me.Controls.Add(Me.cboBaudRate)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cboComPort)
        Me.Controls.Add(Me.Label1)
        Me.Name = "MainForm"
        Me.Text = "My Phone"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboComPort As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cboBaudRate As System.Windows.Forms.ComboBox
    Friend WithEvents cboDataBit As System.Windows.Forms.ComboBox
    Friend WithEvents cboStopBit As System.Windows.Forms.ComboBox
    Friend WithEvents cboFlowControl As System.Windows.Forms.ComboBox
    Friend WithEvents btnConnect As System.Windows.Forms.Button
    Friend WithEvents btnDisconnect As System.Windows.Forms.Button
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents txtPhoneNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtMsg As System.Windows.Forms.TextBox
    Friend WithEvents btnSendMsg As System.Windows.Forms.Button
    Friend WithEvents txtStorage As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents btnCheckPhone As System.Windows.Forms.Button
    Friend WithEvents btnSendClass2Msg As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Timer2 As System.Windows.Forms.Timer

End Class
