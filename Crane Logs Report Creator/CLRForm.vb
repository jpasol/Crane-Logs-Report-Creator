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

        AddHandler cmdExit1.Click, AddressOf ExitForm
        AddHandler cmdExit2.Click, AddressOf ExitForm
        AddHandler cmdPrev1.Click, AddressOf PrevForm
        AddHandler cmdNext1.Click, AddressOf NextForm

        DelaySum.DataSource = clsCLR.CraneLogsData.BerthingHourDelays

        ' Add any initialization after the InitializeComponent() call.

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
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        Dim strGC As String = "GC0" & txtGC.Text
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


    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        On Error Resume Next
        Dim tabSelected As TabPage = DirectCast(sender, TabControl).SelectedTab

        Select Case tabSelected.Text.ToString
            Case "General Information"
                RefreshInfo()
            Case "More Information"
                CraneSummary()
                DelaySummary()
        End Select
    End Sub

    Private Sub RefreshInfo()
        With clsCLR
            On Error Resume Next

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

    Private Sub CraneSummary()
        ProdSum.Rows.Clear()
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

                    ProdSum.Rows.Add({cranemove, freight, twenty, forty, ffive, unit, teu})

                    txtUnits.Text = CShort(txtUnits.Text) + unit
                    txtTEUs.Text = CDbl(txtTEUs.Text) + teu
                End With
            Next
        Next

    End Sub

    Public Sub DelaySummary()
        txtDelaySum.Text = 0
        For Each row As DataGridViewRow In DelaySum.Rows
            With row.Cells
                txtDelaySum.Text = CDbl(txtDelaySum.Text) + .Item(3).Value
            End With
        Next
    End Sub

    Private Sub cmbDelay_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDelay.SelectedIndexChanged
        On Error Resume Next
        Dim rowindex As Integer

        rowindex = clsCLR.CraneLogsData.BerthingHourDelays.GetRowIndexOfBerthDelay(cmbDelay.Text)
        mskDelaystart.Text = GetMilTime(clsCLR.CraneLogsData.BerthingHourDelays.Rows(rowindex)(1))
        mskDelayend.Text = GetMilTime(clsCLR.CraneLogsData.BerthingHourDelays.Rows(rowindex)(2))

        'removed 12282018 for reformatting
        'Dim desclist As New List(Of String)
        '    For Each row As DataGridViewRow In DelaySum.Rows
        '        desclist.Add(row.Cells.Item(0).Value)
        '    Next

        '    rowindex = desclist.IndexOf(cmbDelay.Text)

        '    With DelaySum.Rows(rowindex).Cells
        '        mskDelaystart.Text = GetMilTime(.Item(1).Value)
        '        mskDelayend.Text = GetMilTime(.Item(2).Value)
        '    End With
    End Sub

    Private Sub mskDelayend_KeyDown(sender As Object, e As KeyEventArgs) Handles mskDelayend.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim rowindex As Integer
            Dim delaystart As Date = GetDateTime(mskDelaystart.Text)
            Dim delayend As Date = GetDateTime(mskDelayend.Text)
            Dim delayhours As Double = GetSpanHours(delaystart, delayend)

            rowindex = clsCLR.CraneLogsData.BerthingHourDelays.GetRowIndexOfBerthDelay(cmbDelay.Text)
            If rowindex >= 0 Then 'if index is found
                With clsCLR.CraneLogsData.BerthingHourDelays.Rows(rowindex)
                    .Item("delaystart") = delaystart
                    .Item("delayend") = delayend
                    .Item("delayhours") = delayhours
                End With
            Else 'If Not found
                clsCLR.CraneLogsData.BerthingHourDelays.AddBerthingHourDelaysRow(cmbDelay.Text,
                                                                                 delaystart,
                                                                                 delayend,
                                                                                 delayhours)
            End If
            DelaySummary() 'sum delays
        End If
    End Sub

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

    Private Sub txtGC_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtGC.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 49 Or Asc(e.KeyChar) > 52 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        Dim craneName As String = $"GC0{txtGC.Text}"
        Dim craneTab As String = $"tab{craneName}"
        RemoveCrane(craneName, craneTab)
    End Sub

    Private Sub RemoveCrane(craneName As String, craneTab As String)
        Dim result As MsgBoxResult = MsgBox("Continue Deleting Crane?", vbYesNo)
        If result = vbYes Then
            Try
                Dim removeCrane As Crane = clsCLR.Crane.Find(Function(crane) crane.CraneName = craneName)
                Dim removeTab As TabPage = TabControl1.TabPages.Item(craneTab)
                clsCLR.Crane.Remove(removeCrane)
                TabControl1.TabPages.Remove(removeTab)
            Catch
                MsgBox("Error in Removing Crane: Crane to be removed cannot be accessed")
                Exit Sub
            End Try
        End If
    End Sub

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
End Class
