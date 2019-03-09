Imports Reports.ReportFunctions
Imports Reports
Imports System.Runtime.InteropServices
Public Class CLRForm
    Public Const KEY_ALT As Integer = &H1
    Public Const _HOTKEY As Integer = &H312

    Public Declare Function GetActiveWindow Lib "user32" Alias "GetActiveWindow" () As IntPtr

    <DllImport("User32.dll")>
    Public Shared Function RegisterHotKey(ByVal hwnd As IntPtr,
                        ByVal id As Integer, ByVal fsModifiers As Integer,
                        ByVal vk As Integer) As Integer
    End Function


    <DllImport("User32.dll")>
    Public Shared Function UnregisterHotKey(ByVal hwnd As IntPtr,
                        ByVal id As Integer) As Integer
    End Function

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = _HOTKEY And GetActiveWindow = Me.Handle Then
            Dim id As IntPtr = m.WParam
            Select Case (id.ToString)
                Case "1"
                    ExitForm()
                Case "2"
                    PrevForm()
                Case "3"
                    NextForm()
            End Select
        End If
        MyBase.WndProc(m)
    End Sub 'System wide hotkey event handling

    Public Sub New(Registry As String, ByRef N4Connection As ADODB.Connection, ByRef OPConnection As ADODB.Connection, Username As String)

        ' This call is required by the designer.
        InitializeComponent()
        clsCLR = New CLRClass(Registry, N4Connection, OPConnection, Username)
        connN4 = N4Connection
        connOP = OPConnection
        Me.username = Username

        AddHandler cmdNext1.Click, AddressOf NextForm
        AddHandler AddCrane.Click, AddressOf AddCraneNumber
        AddHandler AddCrane1.Click, AddressOf AddCraneNumber
        AddHandler DeleteCrane.Click, AddressOf RemoveCrane

        AddHandler mskVFMStart.TextChanged, AddressOf SumDelays
        AddHandler mskGOBStart.TextChanged, AddressOf SumDelays
        AddHandler mskPOBStart.TextChanged, AddressOf SumDelays
        AddHandler mskVFMEnd.TextChanged, AddressOf SumDelays
        AddHandler mskGOBEnd.TextChanged, AddressOf SumDelays
        AddHandler mskPOBEnd.TextChanged, AddressOf SumDelays



        summaryTable = New SummaryData.SummaryTableDataTable
        ifSummaryView = New DataView
        imSummaryView = New DataView
        ofSummaryView = New DataView
        omSummaryView = New DataView
        ifSummary.AutoGenerateColumns = False
        imSummary.AutoGenerateColumns = False
        ofSummary.AutoGenerateColumns = False
        omSummary.AutoGenerateColumns = False

        SetSummaryTables()

        SumDeductable.DataSource = clsCLR.CraneLogsData.DelaySummary
        SumNonDeductable.DataSource = clsCLR.CraneLogsData.DelaySummaryND


        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub SetToolTipForMaskedTextbox(controlCollection As ControlCollection)
        For Each ctl As Control In controlCollection
            If ctl.HasChildren Then
                SetToolTipForMaskedTextbox(ctl.Controls)
            End If
            If ctl.GetType() Is GetType(MaskedTextBox) Then
                Dim maskedBox As MaskedTextBox = DirectCast(ctl, MaskedTextBox)
                If maskedBox.Mask.Contains("00/00/0000") Then
                    DateFormat.SetToolTip(maskedBox, "hhmmH MM/DD/YYYY")
                End If
            End If
        Next
    End Sub

    Private Sub SetSummaryTables()
        Dim summaryViews As DataView() = {ifSummaryView, imSummaryView, ofSummaryView, omSummaryView}
        Dim summaryDataGrids As DataGridView() = {ifSummary, imSummary, ofSummary, omSummary}

        For Each views As DataView In summaryViews 'set table of dataview
            views.Table = summaryTable
        Next

        For index As Integer = 0 To 3 'set datasource
            summaryDataGrids(index).DataSource = summaryViews(index)
        Next

        For index As Integer = 0 To 3 'set datasource
            summaryDataGrids(index).Columns.Item(0).DataPropertyName = "20"
            summaryDataGrids(index).Columns.Item(1).DataPropertyName = "40"
            summaryDataGrids(index).Columns.Item(2).DataPropertyName = "45"
        Next

        ifSummaryView.RowFilter = "category = 'Discharge' and freightkind = 'FCL'"
        imSummaryView.RowFilter = "category = 'Discharge' And freightkind = 'MTY'"
        ofSummaryView.RowFilter = "category = 'Loading' and freightkind = 'FCL'"
        omSummaryView.RowFilter = "category = 'Loading' and freightkind = 'MTY'"


    End Sub

    Private Sub NextForm()
        If Me.TabControl1.SelectedIndex < Me.TabControl1.TabCount - 1 Then
            Me.TabControl1.SelectedIndex += 1
        End If
    End Sub

    Private Sub PrevForm()
        If Me.TabControl1.SelectedIndex > 0 Then
            Me.TabControl1.SelectedIndex -= 1
        End If
    End Sub

    Private clsCLR As CLRClass
    Private connN4 As ADODB.Connection
    Private connOP As ADODB.Connection
    Private ctlCrane As CraneCtl
    Private username As String

    Private Sub MapDetails()
        With clsCLR.CLRVessel
            mskShippingLine.Text = .Owner
            mskVessel.Text = .Name
            mskRegistry.Text = .Registry
            mskVoyage.Text = .InboundVoyage & " - " & .OutboundVoyage
            mskATA.Text = GetMilTime(.ATA)
            mskATD.Text = GetMilTime(.ATD)

        End With
    End Sub

    Private Sub CLRForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        MapDetails()

        For Each crn As Crane In clsCLR.Crane
            Dim CraneControl As New CraneCtl(crn)
            CraneControl.PopulateDelays()
            TabControl1.TabPages.Add(CraneControl.tabCraneLog.TabPages($"tab{crn.CraneName}"))
        Next


        RegisterHotKey(Me.Handle, 1, 0, Keys.F3)
        RegisterHotKey(Me.Handle, 2, 0, Keys.F10)
        RegisterHotKey(Me.Handle, 3, 0, Keys.F11)


        RefreshInfo()
        InitializeDelays()

    End Sub

    Private Sub InitializeDelays()
        mskVFMStart.Text = mskATA.Text
        mskPOBStart.Text = mskLastmve.Text
        mskPOBEnd.Text = mskATD.Text
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        On Error Resume Next
        Dim tabSelected As TabPage = DirectCast(sender, TabControl).SelectedTab

        Select Case tabSelected.TabIndex
            Case 0 'page 1
                RefreshInfo()
            Case 1 'page2
                CraneSummary()
        End Select
    End Sub

    Private Sub RefreshInfo()
        With clsCLR

            mskFirstmve.Text = GetMilTime(.FirstMove)
            mskLastmve.Text = GetMilTime(.LastMove)
            mskMoves.Text = .TotalMoves
            mskDensity.Text = Format(.CraneDensity, "0.00")
            mskBerthHours.Text = Format(.TotalBerthHours, "0.00")
            mskNetBerth.Text = Format(.NetBerthHours, "0.00")
            mskGrossBerthProd.Text = Format(.GrossBerthProdRate, "0.00")
            mskNetBerthProd.Text = Format(.NetBerthProdRate, "0.00")
            mskGrossWorkingTime.Text = Format(.GrossVesselWorkingTime, "0.00")
            mskNetWorkingTime.Text = Format(.NetVesselWorkingTime, "0.00")
            mskGrossVesselProd.Text = Format(.GrossVesselProdRate, "0.00")
            mskNetVesselProd.Text = Format(.NetVesselProdRate, "0.00")
            mskGrossHours.Text = Format(.TotalGrossWorkingHours, "0.00")
            mskNetHours.Text = Format(.TotalNetWorkingHours, "0.00")
            mskGrossCraneProd.Text = Format(.GrossCraneProductivity, "0.00")
            mskNetCraneProd.Text = Format(.NetCraneProductivity, "0.00")
        End With
    End Sub
    Private summaryTable As SummaryData.SummaryTableDataTable
    Private ifSummaryView As DataView
    Private imSummaryView As DataView
    Private ofSummaryView As DataView
    Private omSummaryView As DataView
    Private Sub CraneSummary()
        summaryTable.Clear()

        txtUnits.Text = 0
        txtTEUs.Text = 0


        Dim bound() As String = {"Discharge", "Loading"}
        Dim freightkind() As String = {"FCL", "MTY"}


        For Each cranemove In bound

            For Each freight In freightkind
                Dim twenty As Short
                Dim forty As Short
                Dim ffive As Short
                Dim unit As Short
                Dim teu As Single

                With clsCLR.Crane.AsEnumerable
                    twenty = .Sum(Function(crn) crn.Moves.Container.TotalMoves(20, freight, cranemove))
                    forty = .Sum(Function(crn) crn.Moves.Container.TotalMoves(40, freight, cranemove))
                    ffive = .Sum(Function(crn) crn.Moves.Container.TotalMoves(45, freight, cranemove))
                    unit = twenty + forty + ffive
                    teu = (twenty) + (forty * 2) + (ffive * 2.25)

                    summaryTable.Rows.Add({cranemove, freight, twenty, forty, ffive, unit, teu})

                    txtUnits.Text = CShort(txtUnits.Text) + unit
                    txtTEUs.Text = CDbl(txtTEUs.Text) + teu
                End With
            Next
        Next

        ifSummary.Refresh()
        imSummary.Refresh()
        ofSummary.Refresh()
        omSummary.Refresh()

        clsCLR.SumDeductableDelays()
        clsCLR.SumNonDeductableDelays()

        SumDeductable.Refresh()
        SumNonDeductable.Refresh()
    End Sub

    Public Function DelaySummary(FromControl As MaskedTextBox, EndControl As MaskedTextBox) As Double
        Dim fromDate As Date = GetDateTime(FromControl.Text)
        Dim toDate As Date = GetDateTime(EndControl.Text)

        Return GetSpanHours(fromDate, toDate)
    End Function

    'Private Sub cmbDelay_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    On Error Resume Next
    '    Dim rowindex As Integer

    '    rowindex = clsCLR.CraneLogsData.BerthingHourDelays.GetRowIndexOfBerthDelay(cmbDelay.Text)
    '    mskVFMStart.Text = GetMilTime(clsCLR.CraneLogsData.BerthingHourDelays.Rows(rowindex)(1))
    '    mskVFMEnd.Text = GetMilTime(clsCLR.CraneLogsData.BerthingHourDelays.Rows(rowindex)(2))

    '    'removed 12282018 for reformatting
    '    'Dim desclist As New List(Of String)
    '    '    For Each row As DataGridViewRow In DelaySum.Rows
    '    '        desclist.Add(row.Cells.Item(0).Value)
    '    '    Next

    '    '    rowindex = desclist.IndexOf(cmbDelay.Text)

    '    '    With DelaySum.Rows(rowindex).Cells
    '    '        mskDelaystart.Text = GetMilTime(.Item(1).Value)
    '    '        mskDelayend.Text = GetMilTime(.Item(2).Value)
    '    '    End With
    'End Sub

    'Private Sub mskDelayend_KeyDown(sender As Object, e As KeyEventArgs) Handles mskVFMEnd.KeyDown
    '    If e.KeyCode = Keys.Enter Then
    '        Dim rowindex As Integer
    '        Dim delaystart As Date = GetDateTime(mskVFMStart.Text)
    '        Dim delayend As Date = GetDateTime(mskVFMEnd.Text)
    '        Dim delayhours As Double = GetSpanHours(delaystart, delayend)

    '        rowindex = clsCLR.CraneLogsData.BerthingHourDelays.GetRowIndexOfBerthDelay(cmbDelay.Text)
    '        If rowindex >= 0 Then 'if index is found
    '            With clsCLR.CraneLogsData.BerthingHourDelays.Rows(rowindex)
    '                .Item("delaystart") = delaystart
    '                .Item("delayend") = delayend
    '                .Item("delayhours") = delayhours
    '            End With
    '        Else 'If Not found
    '            clsCLR.CraneLogsData.BerthingHourDelays.AddBerthingHourDelaysRow(cmbDelay.Text,
    '                                                                             delaystart,
    '                                                                             delayend,
    '                                                                             delayhours)
    '        End If
    '        DelaySummary() 'sum delays
    '    End If
    'End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        'set remaning details to respective properties
        With clsCLR
            .LastPort = mskLastPort.Text
            .NextPort = mskNextPort.Text

        End With

        Dim result As MsgBoxResult

        Dim existsResult As Boolean = clsCLR.Exists
        connOP.Open()
        connOP.BeginTrans()

        Try
            If existsResult = True Then
                result = MsgBox("Update Crane Log Report?", vbYesNo)
                If result = vbYes Then
                    clsCLR.CancelExistingCraneLogsReport() 'cancelled succesfully
                    clsCLR.Save()
                    connOP.CommitTrans()
                    MsgBox("Saved Successfully!")
                Else
                    connOP.RollbackTrans()
                End If
            Else
                result = MsgBox("Continue Saving?", vbYesNo)
                If result = vbYes Then
                    clsCLR.Save()
                    connOP.CommitTrans()
                    MsgBox("Saved Successfully!")
                Else
                    connOP.RollbackTrans()
                End If
            End If
        Catch ex As Exception
            MsgBox("Rolling Back Changes")
            connOP.RollbackTrans()
        End Try

        connOP.Close()


    End Sub

    'Private Sub txtGC_KeyPress(sender As Object, e As KeyPressEventArgs)
    '    If Asc(e.KeyChar) <> 8 Then
    '        If Asc(e.KeyChar) < 49 Or Asc(e.KeyChar) > 52 Then
    '            e.Handled = True
    '        End If
    '    End If
    'End Sub

    'Private Sub cmdDelete_Click(sender As Object, e As EventArgs)
    '    Dim craneName As String = $"GC0{1}"
    '    Dim craneTab As String = $"tab{craneName}"
    '    RemoveCrane(craneName, craneTab)
    'End Sub

    'Private Sub RemoveCrane(craneName As String, craneTab As String)
    '    Dim result As MsgBoxResult = MsgBox("Continue Deleting Crane?", vbYesNo)
    '    If result = vbYes Then
    '        Try
    '            Dim removeCrane As Crane = clsCLR.Crane.Find(Function(crane) crane.CraneName = craneName)
    '            Dim removeTab As TabPage = TabControl1.TabPages.Item(craneTab)
    '            clsCLR.Crane.Remove(removeCrane)
    '            TabControl1.TabPages.Remove(removeTab)
    '        Catch
    '            MsgBox("Error in Removing Crane: Crane to be removed cannot be accessed")
    '            Exit Sub
    '        End Try
    '    End If
    'End Sub

    Private Sub CLRForm_HandleDestroyed(sender As Object, e As EventArgs) Handles Me.HandleDestroyed
        UnregisterHotKey(Me.Handle, 1)
        UnregisterHotKey(Me.Handle, 2)
        UnregisterHotKey(Me.Handle, 3)

    End Sub

    Private Sub ExitForm()
        Me.Dispose()
    End Sub

    Private Sub TabControl1_DrawItem(sender As Object, e As DrawItemEventArgs) Handles TabControl1.DrawItem
        e.Graphics.FillRectangle(New SolidBrush(Color.FromArgb(255 / (e.Index + 1), 255, 102, 0)), e.Bounds)

        Dim paddedBounds As Rectangle = e.Bounds
        paddedBounds.Inflate(-2, -2)
        e.Graphics.DrawString(TabControl1.TabPages(e.Index).Text, TabControl1.TabPages(e.Index).Font, New SolidBrush(Color.Black), paddedBounds)
    End Sub

    Private Sub AddCraneNumber()
        Dim gcNumber As Integer = InputBox("What Crane Number? 1 - 4")
        If {1, 2, 3, 4}.Contains(gcNumber) Then
            Dim strGC As String = $"GC0{gcNumber}"

            Dim tempcrane As Reports.Crane
            Try
                TabControl1.SelectTab("tab" & strGC)
            Catch ex As Exception
                With clsCLR
                    clsCLR.IntializeCrane(strGC)

                    tempcrane = (.Crane.AsEnumerable.Where(Function(crn) crn.CraneName = strGC).Select(Function(crn) crn))(0)
                    ctlCrane = New CraneCtl(tempcrane)
                    TabControl1.TabPages.Add(ctlCrane.tabCraneLog.TabPages("tab" & strGC))
                    TabControl1.SelectTab("tab" & strGC)

                    ctlCrane.Refresh_info()
                End With
            End Try
        Else
            MsgBox($"GC0{gcNumber} Cannot be accessed/created")
        End If
    End Sub

    Private Sub RemoveCrane(sender As Object, e As EventArgs)
        Dim cursorLocation As Point = tabcontrolCursorLocation
        Dim craneTab As TabPage = GetTabPageByLocation(TabControl1, cursorLocation)

        TabControl1.TabPages.Remove(craneTab)
    End Sub

    Private Sub TabControl1_MouseClick(sender As Object, e As MouseEventArgs) Handles TabControl1.MouseClick
        If e.Button = MouseButtons.Right Then
            tabcontrolCursorLocation = e.Location
            Dim tabPage As TabPage = GetTabPageByLocation(TabControl1, e.Location)

            Select Case TabControl1.TabPages.IndexOf(tabPage)
                Case 0, 1
                    CLRMenu.Show(TabControl1, e.Location)
                Case Else
                    CraneTabMenu.Show(TabControl1, e.Location)
            End Select
        End If
    End Sub
    Private tabcontrolCursorLocation As Point 'used to catch cursor location when using gettabpagebylocation
    Private Function GetTabPageByLocation(tabControl1 As TabControl, location As Point) As TabPage

        For Each tabpage As TabPage In tabControl1.TabPages
            Dim r As Rectangle = tabControl1.GetTabRect(tabControl1.TabPages.IndexOf(tabpage))
            If r.Contains(location) Then
                Return tabpage
            End If
        Next
    End Function

    Private Sub mskVFMEnd_KeyDown(sender As Object, e As KeyEventArgs) Handles mskVFMEnd.KeyDown
        If e.KeyCode = Keys.Enter Then
            mskGOBStart.Text = mskVFMStart.Text
            Dim endGOB As Date = GetDateTime(mskGOBStart.Text).AddMinutes(5)
            mskGOBEnd.Text = GetMilTime(endGOB)
        End If
    End Sub

    Private Sub SumDelays(sender As Object, e As EventArgs)
        Dim maskedBox As MaskedTextBox = DirectCast(sender, MaskedTextBox)
        Try
            Select Case True
                Case maskedBox.Name.Contains("VFM")
                    Dim fromDate As Date = GetDateTime(mskVFMStart.Text)
                    Dim toDate As Date = GetDateTime(mskVFMEnd.Text)
                    mskVFM.Text = GetSpanHours(fromDate, toDate)

                Case maskedBox.Name.Contains("GOB")
                    Dim fromDate As Date = GetDateTime(mskGOBStart.Text)
                    Dim toDate As Date = GetDateTime(mskGOBEnd.Text)
                    mskGOB.Text = GetSpanHours(fromDate, toDate)
                Case maskedBox.Name.Contains("POB")
                    Dim fromDate As Date = GetDateTime(mskPOBStart.Text)
                    Dim toDate As Date = GetDateTime(mskPOBEnd.Text)
                    mskPOB.Text = GetSpanHours(fromDate, toDate)

            End Select
            txtDelaySum.Text = CDbl(mskVFM.Text) + CDbl(mskGOB.Text) + CDbl(mskPOB.Text)
        Catch
        End Try
    End Sub


End Class
