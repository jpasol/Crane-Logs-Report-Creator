<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CraneCtl
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.tabCraneLog = New System.Windows.Forms.TabControl()
        Me.tabCrane = New System.Windows.Forms.TabPage()
        Me.tabMoves = New System.Windows.Forms.TabControl()
        Me.tabCntrs = New System.Windows.Forms.TabPage()
        Me.ContainerLoadMTY = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn13 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn14 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn15 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ContainerDscMTY = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn17 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn18 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn23 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn24 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.ContainerLoadFCL = New System.Windows.Forms.DataGridView()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn11 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtL20 = New System.Windows.Forms.TextBox()
        Me.txtL40 = New System.Windows.Forms.TextBox()
        Me.txtL45 = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtD20 = New System.Windows.Forms.TextBox()
        Me.txtD40 = New System.Windows.Forms.TextBox()
        Me.txtD45 = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ContainerDscFCL = New System.Windows.Forms.DataGridView()
        Me.ctrCategory = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cntsze20 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cntsze40 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cntsze45 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tabGearbox = New System.Windows.Forms.TabPage()
        Me.GearboxLoad = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtGL20 = New System.Windows.Forms.TextBox()
        Me.txtGL40 = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtGD20 = New System.Windows.Forms.TextBox()
        Me.txtGD40 = New System.Windows.Forms.TextBox()
        Me.GearboxDsc = New System.Windows.Forms.DataGridView()
        Me.baynum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.gbxsze20 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.gbxsze40 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.tabHatchcover = New System.Windows.Forms.TabPage()
        Me.HatchLoad = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtHL20 = New System.Windows.Forms.TextBox()
        Me.txtHL40 = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtHD20 = New System.Windows.Forms.TextBox()
        Me.txtHD40 = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.HatchDsc = New System.Windows.Forms.DataGridView()
        Me.hc_baynum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.hcsze20 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.hcsze40 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.tabDelays = New System.Windows.Forms.TabPage()
        Me.txtDDelay = New System.Windows.Forms.TextBox()
        Me.txtBreaks = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.btnAddDelay = New System.Windows.Forms.Button()
        Me.mskTo = New System.Windows.Forms.MaskedTextBox()
        Me.mskFrom = New System.Windows.Forms.MaskedTextBox()
        Me.mskDescription = New System.Windows.Forms.MaskedTextBox()
        Me.cmbDelays = New System.Windows.Forms.ComboBox()
        Me.dgvDelays = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtNDelays = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.mskLast = New System.Windows.Forms.MaskedTextBox()
        Me.mskFirst = New System.Windows.Forms.MaskedTextBox()
        Me.txtNprod = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtNhours = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtGprod = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtGhours = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtMoves = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblGantry = New System.Windows.Forms.Label()
        Me.delaykind = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn19 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn20 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn21 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn22 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tabCraneLog.SuspendLayout()
        Me.tabCrane.SuspendLayout()
        Me.tabMoves.SuspendLayout()
        Me.tabCntrs.SuspendLayout()
        CType(Me.ContainerLoadMTY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ContainerDscMTY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ContainerLoadFCL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ContainerDscFCL, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabGearbox.SuspendLayout()
        CType(Me.GearboxLoad, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GearboxDsc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabHatchcover.SuspendLayout()
        CType(Me.HatchLoad, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.HatchDsc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabDelays.SuspendLayout()
        CType(Me.dgvDelays, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'tabCraneLog
        '
        Me.tabCraneLog.Controls.Add(Me.tabCrane)
        Me.tabCraneLog.Font = New System.Drawing.Font("Calibri", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabCraneLog.Location = New System.Drawing.Point(3, 3)
        Me.tabCraneLog.Name = "tabCraneLog"
        Me.tabCraneLog.SelectedIndex = 0
        Me.tabCraneLog.Size = New System.Drawing.Size(1336, 738)
        Me.tabCraneLog.TabIndex = 0
        '
        'tabCrane
        '
        Me.tabCrane.Controls.Add(Me.tabMoves)
        Me.tabCrane.Controls.Add(Me.GroupBox2)
        Me.tabCrane.Controls.Add(Me.lblGantry)
        Me.tabCrane.Location = New System.Drawing.Point(4, 33)
        Me.tabCrane.Name = "tabCrane"
        Me.tabCrane.Padding = New System.Windows.Forms.Padding(3)
        Me.tabCrane.Size = New System.Drawing.Size(1328, 701)
        Me.tabCrane.TabIndex = 1
        Me.tabCrane.Text = "Crane A"
        Me.tabCrane.UseVisualStyleBackColor = True
        '
        'tabMoves
        '
        Me.tabMoves.Controls.Add(Me.tabCntrs)
        Me.tabMoves.Controls.Add(Me.tabGearbox)
        Me.tabMoves.Controls.Add(Me.tabHatchcover)
        Me.tabMoves.Controls.Add(Me.tabDelays)
        Me.tabMoves.Location = New System.Drawing.Point(6, 282)
        Me.tabMoves.Name = "tabMoves"
        Me.tabMoves.SelectedIndex = 0
        Me.tabMoves.Size = New System.Drawing.Size(1316, 413)
        Me.tabMoves.TabIndex = 3
        '
        'tabCntrs
        '
        Me.tabCntrs.Controls.Add(Me.ContainerLoadMTY)
        Me.tabCntrs.Controls.Add(Me.ContainerDscMTY)
        Me.tabCntrs.Controls.Add(Me.Label26)
        Me.tabCntrs.Controls.Add(Me.Label27)
        Me.tabCntrs.Controls.Add(Me.Label24)
        Me.tabCntrs.Controls.Add(Me.Label25)
        Me.tabCntrs.Controls.Add(Me.ContainerLoadFCL)
        Me.tabCntrs.Controls.Add(Me.Label5)
        Me.tabCntrs.Controls.Add(Me.txtL20)
        Me.tabCntrs.Controls.Add(Me.txtL40)
        Me.tabCntrs.Controls.Add(Me.txtL45)
        Me.tabCntrs.Controls.Add(Me.Label4)
        Me.tabCntrs.Controls.Add(Me.txtD20)
        Me.tabCntrs.Controls.Add(Me.txtD40)
        Me.tabCntrs.Controls.Add(Me.txtD45)
        Me.tabCntrs.Controls.Add(Me.Label3)
        Me.tabCntrs.Controls.Add(Me.ContainerDscFCL)
        Me.tabCntrs.Controls.Add(Me.Label2)
        Me.tabCntrs.Location = New System.Drawing.Point(4, 33)
        Me.tabCntrs.Name = "tabCntrs"
        Me.tabCntrs.Padding = New System.Windows.Forms.Padding(60)
        Me.tabCntrs.Size = New System.Drawing.Size(1308, 376)
        Me.tabCntrs.TabIndex = 0
        Me.tabCntrs.Text = "Containers"
        Me.tabCntrs.UseVisualStyleBackColor = True
        '
        'ContainerLoadMTY
        '
        Me.ContainerLoadMTY.AllowUserToAddRows = False
        Me.ContainerLoadMTY.AllowUserToDeleteRows = False
        Me.ContainerLoadMTY.AllowUserToResizeRows = False
        Me.ContainerLoadMTY.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader
        Me.ContainerLoadMTY.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
        Me.ContainerLoadMTY.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.ContainerLoadMTY.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.DataGridViewTextBoxColumn13, Me.DataGridViewTextBoxColumn14, Me.DataGridViewTextBoxColumn15})
        Me.ContainerLoadMTY.Location = New System.Drawing.Point(975, 82)
        Me.ContainerLoadMTY.Name = "ContainerLoadMTY"
        Me.ContainerLoadMTY.RowHeadersVisible = False
        Me.ContainerLoadMTY.Size = New System.Drawing.Size(321, 252)
        Me.ContainerLoadMTY.TabIndex = 44
        '
        'Column1
        '
        Me.Column1.DataPropertyName = "container"
        Me.Column1.HeaderText = "Container"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 118
        '
        'DataGridViewTextBoxColumn13
        '
        Me.DataGridViewTextBoxColumn13.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn13.DataPropertyName = "cntsze20"
        Me.DataGridViewTextBoxColumn13.FillWeight = 51.21306!
        Me.DataGridViewTextBoxColumn13.HeaderText = "20"
        Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        '
        'DataGridViewTextBoxColumn14
        '
        Me.DataGridViewTextBoxColumn14.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn14.DataPropertyName = "cntsze40"
        Me.DataGridViewTextBoxColumn14.FillWeight = 51.21306!
        Me.DataGridViewTextBoxColumn14.HeaderText = "40"
        Me.DataGridViewTextBoxColumn14.Name = "DataGridViewTextBoxColumn14"
        '
        'DataGridViewTextBoxColumn15
        '
        Me.DataGridViewTextBoxColumn15.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn15.DataPropertyName = "cntsze45"
        Me.DataGridViewTextBoxColumn15.FillWeight = 51.21306!
        Me.DataGridViewTextBoxColumn15.HeaderText = "45"
        Me.DataGridViewTextBoxColumn15.Name = "DataGridViewTextBoxColumn15"
        '
        'ContainerDscMTY
        '
        Me.ContainerDscMTY.AllowUserToAddRows = False
        Me.ContainerDscMTY.AllowUserToDeleteRows = False
        Me.ContainerDscMTY.AllowUserToResizeRows = False
        Me.ContainerDscMTY.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader
        Me.ContainerDscMTY.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
        Me.ContainerDscMTY.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.ContainerDscMTY.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn17, Me.DataGridViewTextBoxColumn18, Me.DataGridViewTextBoxColumn23, Me.DataGridViewTextBoxColumn24})
        Me.ContainerDscMTY.Location = New System.Drawing.Point(332, 82)
        Me.ContainerDscMTY.Name = "ContainerDscMTY"
        Me.ContainerDscMTY.RowHeadersVisible = False
        Me.ContainerDscMTY.Size = New System.Drawing.Size(321, 252)
        Me.ContainerDscMTY.TabIndex = 43
        '
        'DataGridViewTextBoxColumn17
        '
        Me.DataGridViewTextBoxColumn17.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn17.DataPropertyName = "container"
        Me.DataGridViewTextBoxColumn17.FillWeight = 105.0768!
        Me.DataGridViewTextBoxColumn17.HeaderText = "Container"
        Me.DataGridViewTextBoxColumn17.Name = "DataGridViewTextBoxColumn17"
        Me.DataGridViewTextBoxColumn17.ReadOnly = True
        '
        'DataGridViewTextBoxColumn18
        '
        Me.DataGridViewTextBoxColumn18.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn18.DataPropertyName = "cntsze20"
        Me.DataGridViewTextBoxColumn18.FillWeight = 68.98444!
        Me.DataGridViewTextBoxColumn18.HeaderText = "20"
        Me.DataGridViewTextBoxColumn18.Name = "DataGridViewTextBoxColumn18"
        '
        'DataGridViewTextBoxColumn23
        '
        Me.DataGridViewTextBoxColumn23.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn23.DataPropertyName = "cntsze40"
        Me.DataGridViewTextBoxColumn23.FillWeight = 68.98444!
        Me.DataGridViewTextBoxColumn23.HeaderText = "40"
        Me.DataGridViewTextBoxColumn23.Name = "DataGridViewTextBoxColumn23"
        '
        'DataGridViewTextBoxColumn24
        '
        Me.DataGridViewTextBoxColumn24.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn24.DataPropertyName = "cntsze45"
        Me.DataGridViewTextBoxColumn24.FillWeight = 68.98444!
        Me.DataGridViewTextBoxColumn24.HeaderText = "45"
        Me.DataGridViewTextBoxColumn24.Name = "DataGridViewTextBoxColumn24"
        '
        'Label26
        '
        Me.Label26.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label26.Location = New System.Drawing.Point(975, 41)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(321, 38)
        Me.Label26.TabIndex = 42
        Me.Label26.Text = "EMPTY"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label27
        '
        Me.Label27.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label27.Location = New System.Drawing.Point(655, 41)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(321, 38)
        Me.Label27.TabIndex = 41
        Me.Label27.Text = "FULL"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label24
        '
        Me.Label24.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label24.Location = New System.Drawing.Point(332, 41)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(321, 38)
        Me.Label24.TabIndex = 40
        Me.Label24.Text = "EMPTY"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label25
        '
        Me.Label25.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label25.Location = New System.Drawing.Point(12, 41)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(321, 38)
        Me.Label25.TabIndex = 39
        Me.Label25.Text = "FULL"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ContainerLoadFCL
        '
        Me.ContainerLoadFCL.AllowUserToAddRows = False
        Me.ContainerLoadFCL.AllowUserToDeleteRows = False
        Me.ContainerLoadFCL.AllowUserToResizeRows = False
        Me.ContainerLoadFCL.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader
        Me.ContainerLoadFCL.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
        Me.ContainerLoadFCL.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.ContainerLoadFCL.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column3, Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn4, Me.DataGridViewTextBoxColumn11})
        Me.ContainerLoadFCL.Location = New System.Drawing.Point(655, 82)
        Me.ContainerLoadFCL.Name = "ContainerLoadFCL"
        Me.ContainerLoadFCL.RowHeadersVisible = False
        Me.ContainerLoadFCL.Size = New System.Drawing.Size(321, 252)
        Me.ContainerLoadFCL.TabIndex = 38
        '
        'Column3
        '
        Me.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Column3.DataPropertyName = "container"
        Me.Column3.FillWeight = 105.0768!
        Me.Column3.HeaderText = "Container"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "cntsze20"
        Me.DataGridViewTextBoxColumn3.FillWeight = 51.21306!
        Me.DataGridViewTextBoxColumn3.HeaderText = "20"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "cntsze40"
        Me.DataGridViewTextBoxColumn4.FillWeight = 51.21306!
        Me.DataGridViewTextBoxColumn4.HeaderText = "40"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn11.DataPropertyName = "cntsze45"
        Me.DataGridViewTextBoxColumn11.FillWeight = 51.21306!
        Me.DataGridViewTextBoxColumn11.HeaderText = "45"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        '
        'Label5
        '
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.Location = New System.Drawing.Point(655, 337)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(330, 32)
        Me.Label5.TabIndex = 27
        Me.Label5.Text = "TOTAL BOXES"
        '
        'txtL20
        '
        Me.txtL20.Location = New System.Drawing.Point(991, 337)
        Me.txtL20.Name = "txtL20"
        Me.txtL20.ReadOnly = True
        Me.txtL20.Size = New System.Drawing.Size(100, 32)
        Me.txtL20.TabIndex = 26
        '
        'txtL40
        '
        Me.txtL40.Location = New System.Drawing.Point(1097, 337)
        Me.txtL40.Name = "txtL40"
        Me.txtL40.ReadOnly = True
        Me.txtL40.Size = New System.Drawing.Size(100, 32)
        Me.txtL40.TabIndex = 25
        '
        'txtL45
        '
        Me.txtL45.Location = New System.Drawing.Point(1203, 337)
        Me.txtL45.Name = "txtL45"
        Me.txtL45.ReadOnly = True
        Me.txtL45.Size = New System.Drawing.Size(93, 32)
        Me.txtL45.TabIndex = 24
        '
        'Label4
        '
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Location = New System.Drawing.Point(12, 337)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(330, 32)
        Me.Label4.TabIndex = 23
        Me.Label4.Text = "TOTAL BOXES"
        '
        'txtD20
        '
        Me.txtD20.Location = New System.Drawing.Point(348, 337)
        Me.txtD20.Name = "txtD20"
        Me.txtD20.ReadOnly = True
        Me.txtD20.Size = New System.Drawing.Size(100, 32)
        Me.txtD20.TabIndex = 22
        '
        'txtD40
        '
        Me.txtD40.Location = New System.Drawing.Point(454, 337)
        Me.txtD40.Name = "txtD40"
        Me.txtD40.ReadOnly = True
        Me.txtD40.Size = New System.Drawing.Size(100, 32)
        Me.txtD40.TabIndex = 21
        '
        'txtD45
        '
        Me.txtD45.Location = New System.Drawing.Point(560, 337)
        Me.txtD45.Name = "txtD45"
        Me.txtD45.ReadOnly = True
        Me.txtD45.Size = New System.Drawing.Size(93, 32)
        Me.txtD45.TabIndex = 20
        '
        'Label3
        '
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Location = New System.Drawing.Point(655, 3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(641, 38)
        Me.Label3.TabIndex = 19
        Me.Label3.Text = "LOADING"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ContainerDscFCL
        '
        Me.ContainerDscFCL.AllowUserToAddRows = False
        Me.ContainerDscFCL.AllowUserToDeleteRows = False
        Me.ContainerDscFCL.AllowUserToResizeRows = False
        Me.ContainerDscFCL.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader
        Me.ContainerDscFCL.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
        Me.ContainerDscFCL.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.ContainerDscFCL.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ctrCategory, Me.cntsze20, Me.cntsze40, Me.cntsze45})
        Me.ContainerDscFCL.Location = New System.Drawing.Point(12, 82)
        Me.ContainerDscFCL.Name = "ContainerDscFCL"
        Me.ContainerDscFCL.RowHeadersVisible = False
        Me.ContainerDscFCL.Size = New System.Drawing.Size(321, 252)
        Me.ContainerDscFCL.TabIndex = 18
        '
        'ctrCategory
        '
        Me.ctrCategory.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.ctrCategory.DataPropertyName = "container"
        Me.ctrCategory.FillWeight = 187.9699!
        Me.ctrCategory.HeaderText = "Container"
        Me.ctrCategory.Name = "ctrCategory"
        Me.ctrCategory.ReadOnly = True
        '
        'cntsze20
        '
        Me.cntsze20.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.cntsze20.DataPropertyName = "cntsze20"
        Me.cntsze20.FillWeight = 68.98444!
        Me.cntsze20.HeaderText = "20"
        Me.cntsze20.Name = "cntsze20"
        '
        'cntsze40
        '
        Me.cntsze40.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.cntsze40.DataPropertyName = "cntsze40"
        Me.cntsze40.FillWeight = 68.98444!
        Me.cntsze40.HeaderText = "40"
        Me.cntsze40.Name = "cntsze40"
        '
        'cntsze45
        '
        Me.cntsze45.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.cntsze45.DataPropertyName = "cntsze45"
        Me.cntsze45.FillWeight = 68.98444!
        Me.cntsze45.HeaderText = "45"
        Me.cntsze45.Name = "cntsze45"
        '
        'Label2
        '
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Location = New System.Drawing.Point(12, 3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(641, 38)
        Me.Label2.TabIndex = 16
        Me.Label2.Text = "DISCHARGE"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tabGearbox
        '
        Me.tabGearbox.Controls.Add(Me.GearboxLoad)
        Me.tabGearbox.Controls.Add(Me.Label15)
        Me.tabGearbox.Controls.Add(Me.txtGL20)
        Me.tabGearbox.Controls.Add(Me.txtGL40)
        Me.tabGearbox.Controls.Add(Me.Label16)
        Me.tabGearbox.Controls.Add(Me.txtGD20)
        Me.tabGearbox.Controls.Add(Me.txtGD40)
        Me.tabGearbox.Controls.Add(Me.GearboxDsc)
        Me.tabGearbox.Controls.Add(Me.Label13)
        Me.tabGearbox.Controls.Add(Me.Label14)
        Me.tabGearbox.Location = New System.Drawing.Point(4, 33)
        Me.tabGearbox.Name = "tabGearbox"
        Me.tabGearbox.Padding = New System.Windows.Forms.Padding(3)
        Me.tabGearbox.Size = New System.Drawing.Size(1308, 376)
        Me.tabGearbox.TabIndex = 1
        Me.tabGearbox.Text = "Gearbox"
        Me.tabGearbox.UseVisualStyleBackColor = True
        '
        'GearboxLoad
        '
        Me.GearboxLoad.AllowUserToResizeRows = False
        Me.GearboxLoad.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GearboxLoad.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn5, Me.DataGridViewTextBoxColumn6, Me.DataGridViewTextBoxColumn7})
        Me.GearboxLoad.Location = New System.Drawing.Point(655, 44)
        Me.GearboxLoad.Name = "GearboxLoad"
        Me.GearboxLoad.Size = New System.Drawing.Size(641, 288)
        Me.GearboxLoad.TabIndex = 36
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "baynum"
        Me.DataGridViewTextBoxColumn5.HeaderText = "Bay No."
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "gbxsze20"
        Me.DataGridViewTextBoxColumn6.HeaderText = "20"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "gbxsze40"
        Me.DataGridViewTextBoxColumn7.HeaderText = "40"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        '
        'Label15
        '
        Me.Label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label15.Location = New System.Drawing.Point(655, 338)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(436, 32)
        Me.Label15.TabIndex = 35
        Me.Label15.Text = "TOTAL MOVES"
        '
        'txtGL20
        '
        Me.txtGL20.Location = New System.Drawing.Point(1097, 338)
        Me.txtGL20.Name = "txtGL20"
        Me.txtGL20.ReadOnly = True
        Me.txtGL20.Size = New System.Drawing.Size(100, 32)
        Me.txtGL20.TabIndex = 33
        '
        'txtGL40
        '
        Me.txtGL40.Location = New System.Drawing.Point(1203, 338)
        Me.txtGL40.Name = "txtGL40"
        Me.txtGL40.ReadOnly = True
        Me.txtGL40.Size = New System.Drawing.Size(93, 32)
        Me.txtGL40.TabIndex = 32
        '
        'Label16
        '
        Me.Label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label16.Location = New System.Drawing.Point(12, 338)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(436, 32)
        Me.Label16.TabIndex = 31
        Me.Label16.Text = "TOTAL MOVES"
        '
        'txtGD20
        '
        Me.txtGD20.Location = New System.Drawing.Point(454, 338)
        Me.txtGD20.Name = "txtGD20"
        Me.txtGD20.ReadOnly = True
        Me.txtGD20.Size = New System.Drawing.Size(100, 32)
        Me.txtGD20.TabIndex = 29
        '
        'txtGD40
        '
        Me.txtGD40.Location = New System.Drawing.Point(560, 338)
        Me.txtGD40.Name = "txtGD40"
        Me.txtGD40.ReadOnly = True
        Me.txtGD40.Size = New System.Drawing.Size(93, 32)
        Me.txtGD40.TabIndex = 28
        '
        'GearboxDsc
        '
        Me.GearboxDsc.AllowUserToResizeRows = False
        Me.GearboxDsc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GearboxDsc.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.baynum, Me.gbxsze20, Me.gbxsze40})
        Me.GearboxDsc.Location = New System.Drawing.Point(12, 44)
        Me.GearboxDsc.Name = "GearboxDsc"
        Me.GearboxDsc.Size = New System.Drawing.Size(641, 288)
        Me.GearboxDsc.TabIndex = 23
        '
        'baynum
        '
        Me.baynum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.baynum.DataPropertyName = "baynum"
        Me.baynum.HeaderText = "Bay No."
        Me.baynum.Name = "baynum"
        '
        'gbxsze20
        '
        Me.gbxsze20.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.gbxsze20.DataPropertyName = "gbxsze20"
        Me.gbxsze20.HeaderText = "20"
        Me.gbxsze20.Name = "gbxsze20"
        '
        'gbxsze40
        '
        Me.gbxsze40.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.gbxsze40.DataPropertyName = "gbxsze40"
        Me.gbxsze40.HeaderText = "40"
        Me.gbxsze40.Name = "gbxsze40"
        '
        'Label13
        '
        Me.Label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label13.Location = New System.Drawing.Point(655, 3)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(641, 38)
        Me.Label13.TabIndex = 21
        Me.Label13.Text = "LOADING"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label14
        '
        Me.Label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label14.Location = New System.Drawing.Point(12, 3)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(641, 38)
        Me.Label14.TabIndex = 20
        Me.Label14.Text = "DISCHARGE"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tabHatchcover
        '
        Me.tabHatchcover.Controls.Add(Me.HatchLoad)
        Me.tabHatchcover.Controls.Add(Me.Label17)
        Me.tabHatchcover.Controls.Add(Me.txtHL20)
        Me.tabHatchcover.Controls.Add(Me.txtHL40)
        Me.tabHatchcover.Controls.Add(Me.Label18)
        Me.tabHatchcover.Controls.Add(Me.txtHD20)
        Me.tabHatchcover.Controls.Add(Me.txtHD40)
        Me.tabHatchcover.Controls.Add(Me.Label19)
        Me.tabHatchcover.Controls.Add(Me.HatchDsc)
        Me.tabHatchcover.Controls.Add(Me.Label20)
        Me.tabHatchcover.Location = New System.Drawing.Point(4, 33)
        Me.tabHatchcover.Name = "tabHatchcover"
        Me.tabHatchcover.Padding = New System.Windows.Forms.Padding(3)
        Me.tabHatchcover.Size = New System.Drawing.Size(1308, 376)
        Me.tabHatchcover.TabIndex = 2
        Me.tabHatchcover.Text = "Hatch Cover"
        Me.tabHatchcover.UseVisualStyleBackColor = True
        '
        'HatchLoad
        '
        Me.HatchLoad.AllowUserToResizeRows = False
        Me.HatchLoad.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.HatchLoad.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn8, Me.DataGridViewTextBoxColumn9, Me.DataGridViewTextBoxColumn10})
        Me.HatchLoad.Location = New System.Drawing.Point(655, 43)
        Me.HatchLoad.Name = "HatchLoad"
        Me.HatchLoad.Size = New System.Drawing.Size(641, 289)
        Me.HatchLoad.TabIndex = 40
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn8.DataPropertyName = "baynum"
        Me.DataGridViewTextBoxColumn8.HeaderText = "Bay No."
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn9.DataPropertyName = "cvrsze20"
        Me.DataGridViewTextBoxColumn9.HeaderText = "20"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn10.DataPropertyName = "cvrsze40"
        Me.DataGridViewTextBoxColumn10.HeaderText = "40"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        '
        'Label17
        '
        Me.Label17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label17.Location = New System.Drawing.Point(655, 338)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(436, 32)
        Me.Label17.TabIndex = 39
        Me.Label17.Text = "TOTAL MOVES"
        '
        'txtHL20
        '
        Me.txtHL20.Location = New System.Drawing.Point(1097, 338)
        Me.txtHL20.Name = "txtHL20"
        Me.txtHL20.ReadOnly = True
        Me.txtHL20.Size = New System.Drawing.Size(100, 32)
        Me.txtHL20.TabIndex = 37
        '
        'txtHL40
        '
        Me.txtHL40.Location = New System.Drawing.Point(1203, 338)
        Me.txtHL40.Name = "txtHL40"
        Me.txtHL40.ReadOnly = True
        Me.txtHL40.Size = New System.Drawing.Size(93, 32)
        Me.txtHL40.TabIndex = 36
        '
        'Label18
        '
        Me.Label18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label18.Location = New System.Drawing.Point(12, 338)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(436, 32)
        Me.Label18.TabIndex = 35
        Me.Label18.Text = "TOTAL MOVES"
        '
        'txtHD20
        '
        Me.txtHD20.Location = New System.Drawing.Point(454, 338)
        Me.txtHD20.Name = "txtHD20"
        Me.txtHD20.ReadOnly = True
        Me.txtHD20.Size = New System.Drawing.Size(100, 32)
        Me.txtHD20.TabIndex = 33
        '
        'txtHD40
        '
        Me.txtHD40.Location = New System.Drawing.Point(560, 338)
        Me.txtHD40.Name = "txtHD40"
        Me.txtHD40.ReadOnly = True
        Me.txtHD40.Size = New System.Drawing.Size(93, 32)
        Me.txtHD40.TabIndex = 32
        '
        'Label19
        '
        Me.Label19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label19.Location = New System.Drawing.Point(655, 3)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(641, 38)
        Me.Label19.TabIndex = 31
        Me.Label19.Text = "CLOSING"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'HatchDsc
        '
        Me.HatchDsc.AllowUserToResizeRows = False
        Me.HatchDsc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.HatchDsc.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.hc_baynum, Me.hcsze20, Me.hcsze40})
        Me.HatchDsc.Location = New System.Drawing.Point(12, 44)
        Me.HatchDsc.Name = "HatchDsc"
        Me.HatchDsc.Size = New System.Drawing.Size(641, 288)
        Me.HatchDsc.TabIndex = 30
        '
        'hc_baynum
        '
        Me.hc_baynum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.hc_baynum.DataPropertyName = "baynum"
        Me.hc_baynum.HeaderText = "Bay No."
        Me.hc_baynum.Name = "hc_baynum"
        '
        'hcsze20
        '
        Me.hcsze20.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.hcsze20.DataPropertyName = "cvrsze20"
        Me.hcsze20.HeaderText = "20"
        Me.hcsze20.Name = "hcsze20"
        '
        'hcsze40
        '
        Me.hcsze40.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.hcsze40.DataPropertyName = "cvrsze40"
        Me.hcsze40.HeaderText = "40"
        Me.hcsze40.Name = "hcsze40"
        '
        'Label20
        '
        Me.Label20.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label20.Location = New System.Drawing.Point(12, 3)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(641, 38)
        Me.Label20.TabIndex = 28
        Me.Label20.Text = "OPENING"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tabDelays
        '
        Me.tabDelays.Controls.Add(Me.txtDDelay)
        Me.tabDelays.Controls.Add(Me.txtBreaks)
        Me.tabDelays.Controls.Add(Me.Label23)
        Me.tabDelays.Controls.Add(Me.Label22)
        Me.tabDelays.Controls.Add(Me.Label21)
        Me.tabDelays.Controls.Add(Me.btnAddDelay)
        Me.tabDelays.Controls.Add(Me.mskTo)
        Me.tabDelays.Controls.Add(Me.mskFrom)
        Me.tabDelays.Controls.Add(Me.mskDescription)
        Me.tabDelays.Controls.Add(Me.cmbDelays)
        Me.tabDelays.Controls.Add(Me.dgvDelays)
        Me.tabDelays.Controls.Add(Me.Label1)
        Me.tabDelays.Controls.Add(Me.txtNDelays)
        Me.tabDelays.Location = New System.Drawing.Point(4, 33)
        Me.tabDelays.Name = "tabDelays"
        Me.tabDelays.Padding = New System.Windows.Forms.Padding(3)
        Me.tabDelays.Size = New System.Drawing.Size(1308, 376)
        Me.tabDelays.TabIndex = 3
        Me.tabDelays.Text = "Delays"
        Me.tabDelays.UseVisualStyleBackColor = True
        '
        'txtDDelay
        '
        Me.txtDDelay.Location = New System.Drawing.Point(990, 320)
        Me.txtDDelay.Name = "txtDDelay"
        Me.txtDDelay.ReadOnly = True
        Me.txtDDelay.Size = New System.Drawing.Size(100, 32)
        Me.txtDDelay.TabIndex = 33
        '
        'txtBreaks
        '
        Me.txtBreaks.Location = New System.Drawing.Point(1096, 320)
        Me.txtBreaks.Name = "txtBreaks"
        Me.txtBreaks.ReadOnly = True
        Me.txtBreaks.Size = New System.Drawing.Size(100, 32)
        Me.txtBreaks.TabIndex = 32
        '
        'Label23
        '
        Me.Label23.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label23.Location = New System.Drawing.Point(661, 28)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(234, 38)
        Me.Label23.TabIndex = 31
        Me.Label23.Text = "From"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label22
        '
        Me.Label22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label22.Location = New System.Drawing.Point(223, 28)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(434, 38)
        Me.Label22.TabIndex = 30
        Me.Label22.Text = "Description"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label21
        '
        Me.Label21.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label21.Location = New System.Drawing.Point(6, 28)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(211, 38)
        Me.Label21.TabIndex = 29
        Me.Label21.Text = "Delay Kind"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnAddDelay
        '
        Me.btnAddDelay.Location = New System.Drawing.Point(1140, 69)
        Me.btnAddDelay.Name = "btnAddDelay"
        Me.btnAddDelay.Size = New System.Drawing.Size(162, 32)
        Me.btnAddDelay.TabIndex = 14
        Me.btnAddDelay.Text = "Add"
        Me.btnAddDelay.UseVisualStyleBackColor = True
        '
        'mskTo
        '
        Me.mskTo.Location = New System.Drawing.Point(901, 69)
        Me.mskTo.Mask = "0000\H 00/00/0000"
        Me.mskTo.Name = "mskTo"
        Me.mskTo.Size = New System.Drawing.Size(232, 32)
        Me.mskTo.TabIndex = 13
        Me.mskTo.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals
        '
        'mskFrom
        '
        Me.mskFrom.Location = New System.Drawing.Point(663, 69)
        Me.mskFrom.Mask = "0000\H 00/00/0000"
        Me.mskFrom.Name = "mskFrom"
        Me.mskFrom.Size = New System.Drawing.Size(232, 32)
        Me.mskFrom.TabIndex = 12
        Me.mskFrom.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals
        '
        'mskDescription
        '
        Me.mskDescription.Location = New System.Drawing.Point(223, 69)
        Me.mskDescription.Name = "mskDescription"
        Me.mskDescription.Size = New System.Drawing.Size(434, 32)
        Me.mskDescription.TabIndex = 11
        '
        'cmbDelays
        '
        Me.cmbDelays.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbDelays.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDelays.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbDelays.FormattingEnabled = True
        Me.cmbDelays.Items.AddRange(New Object() {"Deductable", "Break", "Nondeductable"})
        Me.cmbDelays.Location = New System.Drawing.Point(6, 69)
        Me.cmbDelays.Name = "cmbDelays"
        Me.cmbDelays.Size = New System.Drawing.Size(211, 32)
        Me.cmbDelays.TabIndex = 10
        '
        'dgvDelays
        '
        Me.dgvDelays.AllowUserToAddRows = False
        Me.dgvDelays.AllowUserToResizeRows = False
        Me.dgvDelays.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDelays.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.delaykind, Me.DataGridViewTextBoxColumn19, Me.DataGridViewTextBoxColumn20, Me.DataGridViewTextBoxColumn21, Me.DataGridViewTextBoxColumn22})
        Me.dgvDelays.Location = New System.Drawing.Point(6, 107)
        Me.dgvDelays.Name = "dgvDelays"
        Me.dgvDelays.Size = New System.Drawing.Size(1296, 207)
        Me.dgvDelays.TabIndex = 9
        '
        'Label1
        '
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Location = New System.Drawing.Point(586, 320)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(398, 32)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Total Deductable Delays"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtNDelays
        '
        Me.txtNDelays.Location = New System.Drawing.Point(1202, 320)
        Me.txtNDelays.Name = "txtNDelays"
        Me.txtNDelays.ReadOnly = True
        Me.txtNDelays.Size = New System.Drawing.Size(100, 32)
        Me.txtNDelays.TabIndex = 7
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.mskLast)
        Me.GroupBox2.Controls.Add(Me.mskFirst)
        Me.GroupBox2.Controls.Add(Me.txtNprod)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.txtNhours)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.txtGprod)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.txtGhours)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.txtMoves)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Location = New System.Drawing.Point(6, 44)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1316, 232)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Summary"
        '
        'mskLast
        '
        Me.mskLast.Location = New System.Drawing.Point(89, 159)
        Me.mskLast.Mask = "0000\H 00/00/0000"
        Me.mskLast.Name = "mskLast"
        Me.mskLast.Size = New System.Drawing.Size(255, 32)
        Me.mskLast.TabIndex = 16
        Me.mskLast.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals
        '
        'mskFirst
        '
        Me.mskFirst.Location = New System.Drawing.Point(89, 89)
        Me.mskFirst.Mask = "0000\H 00/00/0000"
        Me.mskFirst.Name = "mskFirst"
        Me.mskFirst.Size = New System.Drawing.Size(255, 32)
        Me.mskFirst.TabIndex = 15
        Me.mskFirst.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals
        '
        'txtNprod
        '
        Me.txtNprod.Font = New System.Drawing.Font("Calibri", 28.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNprod.Location = New System.Drawing.Point(1062, 124)
        Me.txtNprod.Multiline = True
        Me.txtNprod.Name = "txtNprod"
        Me.txtNprod.Size = New System.Drawing.Size(166, 67)
        Me.txtNprod.TabIndex = 14
        Me.txtNprod.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label12
        '
        Me.Label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label12.Location = New System.Drawing.Point(1062, 54)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(166, 67)
        Me.Label12.TabIndex = 13
        Me.Label12.Text = "Net Productivity Rate"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtNhours
        '
        Me.txtNhours.Font = New System.Drawing.Font("Calibri", 28.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNhours.Location = New System.Drawing.Point(890, 124)
        Me.txtNhours.Multiline = True
        Me.txtNhours.Name = "txtNhours"
        Me.txtNhours.Size = New System.Drawing.Size(166, 67)
        Me.txtNhours.TabIndex = 12
        Me.txtNhours.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label11
        '
        Me.Label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label11.Location = New System.Drawing.Point(890, 54)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(166, 67)
        Me.Label11.TabIndex = 11
        Me.Label11.Text = "Net Working Hours"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtGprod
        '
        Me.txtGprod.Font = New System.Drawing.Font("Calibri", 28.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGprod.Location = New System.Drawing.Point(718, 124)
        Me.txtGprod.Multiline = True
        Me.txtGprod.Name = "txtGprod"
        Me.txtGprod.Size = New System.Drawing.Size(166, 67)
        Me.txtGprod.TabIndex = 10
        Me.txtGprod.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label10
        '
        Me.Label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label10.Location = New System.Drawing.Point(718, 54)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(166, 67)
        Me.Label10.TabIndex = 9
        Me.Label10.Text = "Gross Productivity"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtGhours
        '
        Me.txtGhours.Font = New System.Drawing.Font("Calibri", 28.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGhours.Location = New System.Drawing.Point(546, 124)
        Me.txtGhours.Multiline = True
        Me.txtGhours.Name = "txtGhours"
        Me.txtGhours.Size = New System.Drawing.Size(166, 67)
        Me.txtGhours.TabIndex = 8
        Me.txtGhours.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label9
        '
        Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label9.Location = New System.Drawing.Point(546, 54)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(166, 67)
        Me.Label9.TabIndex = 7
        Me.Label9.Text = "Gross Working Hours"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtMoves
        '
        Me.txtMoves.Font = New System.Drawing.Font("Calibri", 28.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMoves.Location = New System.Drawing.Point(374, 124)
        Me.txtMoves.Multiline = True
        Me.txtMoves.Name = "txtMoves"
        Me.txtMoves.Size = New System.Drawing.Size(166, 67)
        Me.txtMoves.TabIndex = 6
        Me.txtMoves.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label6
        '
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Location = New System.Drawing.Point(374, 54)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(166, 67)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "TOTAL MOVES"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.Location = New System.Drawing.Point(89, 124)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(255, 32)
        Me.Label8.TabIndex = 2
        Me.Label8.Text = "Last Move"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Location = New System.Drawing.Point(89, 54)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(255, 32)
        Me.Label7.TabIndex = 1
        Me.Label7.Text = "First Move"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblGantry
        '
        Me.lblGantry.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblGantry.Location = New System.Drawing.Point(6, 3)
        Me.lblGantry.Name = "lblGantry"
        Me.lblGantry.Size = New System.Drawing.Size(1316, 38)
        Me.lblGantry.TabIndex = 0
        Me.lblGantry.Text = "Label1"
        Me.lblGantry.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'delaykind
        '
        Me.delaykind.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.delaykind.HeaderText = "Delay Kind"
        Me.delaykind.Name = "delaykind"
        Me.delaykind.Width = 125
        '
        'DataGridViewTextBoxColumn19
        '
        Me.DataGridViewTextBoxColumn19.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.DataGridViewTextBoxColumn19.FillWeight = 203.0457!
        Me.DataGridViewTextBoxColumn19.HeaderText = "Description"
        Me.DataGridViewTextBoxColumn19.Name = "DataGridViewTextBoxColumn19"
        Me.DataGridViewTextBoxColumn19.Width = 131
        '
        'DataGridViewTextBoxColumn20
        '
        Me.DataGridViewTextBoxColumn20.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        DataGridViewCellStyle1.Format = "0000\H 00/00/0000"
        DataGridViewCellStyle1.NullValue = Nothing
        Me.DataGridViewTextBoxColumn20.DefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridViewTextBoxColumn20.FillWeight = 65.65144!
        Me.DataGridViewTextBoxColumn20.HeaderText = "From"
        Me.DataGridViewTextBoxColumn20.Name = "DataGridViewTextBoxColumn20"
        Me.DataGridViewTextBoxColumn20.Width = 78
        '
        'DataGridViewTextBoxColumn21
        '
        Me.DataGridViewTextBoxColumn21.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        DataGridViewCellStyle2.Format = "0000\H 00/00/0000"
        Me.DataGridViewTextBoxColumn21.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridViewTextBoxColumn21.FillWeight = 65.65144!
        Me.DataGridViewTextBoxColumn21.HeaderText = "To"
        Me.DataGridViewTextBoxColumn21.Name = "DataGridViewTextBoxColumn21"
        Me.DataGridViewTextBoxColumn21.Width = 54
        '
        'DataGridViewTextBoxColumn22
        '
        Me.DataGridViewTextBoxColumn22.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle3.NullValue = Nothing
        Me.DataGridViewTextBoxColumn22.DefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridViewTextBoxColumn22.FillWeight = 65.65144!
        Me.DataGridViewTextBoxColumn22.HeaderText = "Total"
        Me.DataGridViewTextBoxColumn22.Name = "DataGridViewTextBoxColumn22"
        '
        'CraneCtl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.tabCraneLog)
        Me.Name = "CraneCtl"
        Me.Size = New System.Drawing.Size(1342, 744)
        Me.tabCraneLog.ResumeLayout(False)
        Me.tabCrane.ResumeLayout(False)
        Me.tabMoves.ResumeLayout(False)
        Me.tabCntrs.ResumeLayout(False)
        Me.tabCntrs.PerformLayout()
        CType(Me.ContainerLoadMTY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ContainerDscMTY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ContainerLoadFCL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ContainerDscFCL, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabGearbox.ResumeLayout(False)
        Me.tabGearbox.PerformLayout()
        CType(Me.GearboxLoad, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GearboxDsc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabHatchcover.ResumeLayout(False)
        Me.tabHatchcover.PerformLayout()
        CType(Me.HatchLoad, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.HatchDsc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabDelays.ResumeLayout(False)
        Me.tabDelays.PerformLayout()
        CType(Me.dgvDelays, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tabCraneLog As TabControl
    Friend WithEvents tabCrane As TabPage
    Friend WithEvents tabMoves As TabControl
    Friend WithEvents tabCntrs As TabPage
    Friend WithEvents Label5 As Label
    Friend WithEvents txtL20 As TextBox
    Friend WithEvents txtL40 As TextBox
    Friend WithEvents txtL45 As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtD20 As TextBox
    Friend WithEvents txtD40 As TextBox
    Friend WithEvents txtD45 As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents ContainerDscFCL As DataGridView
    Friend WithEvents Label2 As Label
    Friend WithEvents tabGearbox As TabPage
    Friend WithEvents Label15 As Label
    Friend WithEvents txtGL20 As TextBox
    Friend WithEvents txtGL40 As TextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents txtGD20 As TextBox
    Friend WithEvents txtGD40 As TextBox
    Friend WithEvents GearboxDsc As DataGridView
    Friend WithEvents Label13 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents tabHatchcover As TabPage
    Friend WithEvents Label17 As Label
    Friend WithEvents txtHL20 As TextBox
    Friend WithEvents txtHL40 As TextBox
    Friend WithEvents Label18 As Label
    Friend WithEvents txtHD20 As TextBox
    Friend WithEvents txtHD40 As TextBox
    Friend WithEvents Label19 As Label
    Friend WithEvents HatchDsc As DataGridView
    Friend WithEvents Label20 As Label
    Friend WithEvents tabDelays As TabPage
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents txtNprod As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents txtNhours As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents txtGprod As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents txtGhours As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents txtMoves As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents lblGantry As Label
    Friend WithEvents GearboxLoad As DataGridView
    Friend WithEvents HatchLoad As DataGridView
    Friend WithEvents mskLast As MaskedTextBox
    Friend WithEvents mskFirst As MaskedTextBox
    Friend WithEvents dgvDelays As DataGridView
    Friend WithEvents Label1 As Label
    Friend WithEvents txtNDelays As TextBox
    Friend WithEvents cmbDelays As ComboBox
    Friend WithEvents mskDescription As MaskedTextBox
    Friend WithEvents mskFrom As MaskedTextBox
    Friend WithEvents btnAddDelay As Button
    Friend WithEvents mskTo As MaskedTextBox
    Friend WithEvents Label23 As Label
    Friend WithEvents Label22 As Label
    Friend WithEvents Label21 As Label
    Friend WithEvents txtDDelay As TextBox
    Friend WithEvents txtBreaks As TextBox
    Friend WithEvents DataGridViewTextBoxColumn5 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As DataGridViewTextBoxColumn
    Friend WithEvents baynum As DataGridViewTextBoxColumn
    Friend WithEvents gbxsze20 As DataGridViewTextBoxColumn
    Friend WithEvents gbxsze40 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As DataGridViewTextBoxColumn
    Friend WithEvents hc_baynum As DataGridViewTextBoxColumn
    Friend WithEvents hcsze20 As DataGridViewTextBoxColumn
    Friend WithEvents hcsze40 As DataGridViewTextBoxColumn
    Friend WithEvents ContainerLoadFCL As DataGridView
    Friend WithEvents ContainerLoadMTY As DataGridView
    Friend WithEvents ContainerDscMTY As DataGridView
    Friend WithEvents Label26 As Label
    Friend WithEvents Label27 As Label
    Friend WithEvents Label24 As Label
    Friend WithEvents Label25 As Label
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn13 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn14 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn15 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn17 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn18 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn23 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn24 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As DataGridViewTextBoxColumn
    Friend WithEvents ctrCategory As DataGridViewTextBoxColumn
    Friend WithEvents cntsze20 As DataGridViewTextBoxColumn
    Friend WithEvents cntsze40 As DataGridViewTextBoxColumn
    Friend WithEvents cntsze45 As DataGridViewTextBoxColumn
    Friend WithEvents delaykind As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn19 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn20 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn21 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn22 As DataGridViewTextBoxColumn
End Class
