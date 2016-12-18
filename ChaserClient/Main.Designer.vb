<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainWindow
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainWindow))
        Me.txtMsg = New System.Windows.Forms.TextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnConnect = New System.Windows.Forms.Button()
        Me.txtTMN = New System.Windows.Forms.TextBox()
        Me.lblTMN = New System.Windows.Forms.Label()
        Me.lblSvPort = New System.Windows.Forms.Label()
        Me.txtSvIP = New System.Windows.Forms.TextBox()
        Me.lblSv = New System.Windows.Forms.Label()
        Me.txtSvPort = New System.Windows.Forms.TextBox()
        Me.pnlMapOuter = New System.Windows.Forms.Panel()
        Me.Panel2.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtMsg
        '
        Me.txtMsg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtMsg.Location = New System.Drawing.Point(0, 0)
        Me.txtMsg.Multiline = True
        Me.txtMsg.Name = "txtMsg"
        Me.txtMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMsg.Size = New System.Drawing.Size(293, 502)
        Me.txtMsg.TabIndex = 2
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Firebrick
        Me.Panel2.Controls.Add(Me.txtMsg)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(293, 502)
        Me.Panel2.TabIndex = 7
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Left
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Panel2)
        Me.SplitContainer1.Size = New System.Drawing.Size(293, 656)
        Me.SplitContainer1.SplitterDistance = 150
        Me.SplitContainer1.TabIndex = 11
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnConnect)
        Me.Panel1.Controls.Add(Me.txtTMN)
        Me.Panel1.Controls.Add(Me.lblTMN)
        Me.Panel1.Controls.Add(Me.lblSvPort)
        Me.Panel1.Controls.Add(Me.txtSvIP)
        Me.Panel1.Controls.Add(Me.lblSv)
        Me.Panel1.Controls.Add(Me.txtSvPort)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(293, 150)
        Me.Panel1.TabIndex = 5
        '
        'btnConnect
        '
        Me.btnConnect.Location = New System.Drawing.Point(169, 96)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(75, 23)
        Me.btnConnect.TabIndex = 0
        Me.btnConnect.Text = "Connect"
        Me.btnConnect.UseVisualStyleBackColor = True
        '
        'txtTMN
        '
        Me.txtTMN.Location = New System.Drawing.Point(84, 57)
        Me.txtTMN.Name = "txtTMN"
        Me.txtTMN.Size = New System.Drawing.Size(100, 19)
        Me.txtTMN.TabIndex = 6
        Me.txtTMN.Text = "TMN"
        '
        'lblTMN
        '
        Me.lblTMN.AutoSize = True
        Me.lblTMN.Location = New System.Drawing.Point(15, 60)
        Me.lblTMN.Name = "lblTMN"
        Me.lblTMN.Size = New System.Drawing.Size(62, 12)
        Me.lblTMN.TabIndex = 6
        Me.lblTMN.Text = "TeamName"
        '
        'lblSvPort
        '
        Me.lblSvPort.AutoSize = True
        Me.lblSvPort.Location = New System.Drawing.Point(15, 35)
        Me.lblSvPort.Name = "lblSvPort"
        Me.lblSvPort.Size = New System.Drawing.Size(54, 12)
        Me.lblSvPort.TabIndex = 5
        Me.lblSvPort.Text = "Host Port"
        '
        'txtSvIP
        '
        Me.txtSvIP.Location = New System.Drawing.Point(84, 7)
        Me.txtSvIP.Name = "txtSvIP"
        Me.txtSvIP.Size = New System.Drawing.Size(100, 19)
        Me.txtSvIP.TabIndex = 3
        Me.txtSvIP.Text = "127.0.0.1"
        '
        'lblSv
        '
        Me.lblSv.AutoSize = True
        Me.lblSv.Location = New System.Drawing.Point(15, 10)
        Me.lblSv.Name = "lblSv"
        Me.lblSv.Size = New System.Drawing.Size(43, 12)
        Me.lblSv.TabIndex = 4
        Me.lblSv.Text = "Host IP"
        '
        'txtSvPort
        '
        Me.txtSvPort.Location = New System.Drawing.Point(84, 32)
        Me.txtSvPort.Name = "txtSvPort"
        Me.txtSvPort.Size = New System.Drawing.Size(100, 19)
        Me.txtSvPort.TabIndex = 1
        Me.txtSvPort.Text = "2009"
        '
        'pnlMapOuter
        '
        Me.pnlMapOuter.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.pnlMapOuter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMapOuter.Location = New System.Drawing.Point(293, 0)
        Me.pnlMapOuter.Name = "pnlMapOuter"
        Me.pnlMapOuter.Size = New System.Drawing.Size(656, 656)
        Me.pnlMapOuter.TabIndex = 12
        '
        'MainWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(949, 656)
        Me.Controls.Add(Me.pnlMapOuter)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(965, 695)
        Me.Name = "MainWindow"
        Me.Text = "Chaser"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtMsg As System.Windows.Forms.TextBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents Panel1 As Panel
    Friend WithEvents btnConnect As Button
    Friend WithEvents txtTMN As TextBox
    Friend WithEvents lblTMN As Label
    Friend WithEvents lblSvPort As Label
    Friend WithEvents txtSvIP As TextBox
    Friend WithEvents lblSv As Label
    Friend WithEvents txtSvPort As TextBox
    Friend WithEvents pnlMapOuter As Panel
End Class
