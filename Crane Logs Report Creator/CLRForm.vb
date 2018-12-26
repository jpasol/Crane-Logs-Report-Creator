Imports Reports.ReportFunctions
Imports Reports
Public Class CLRForm
    Public Sub New(Registry As String, ByRef N4Connection As ADODB.Connection, ByRef OPConnection As ADODB.Connection, Username As String)

        ' This call is required by the designer.
        InitializeComponent()
        clsCLR = New CLRClass(Registry, N4Connection, OPConnection, Username)
        connN4 = N4Connection
        connOP = OPConnection
        Me.username = Username
        ' Add any initialization after the InitializeComponent() call.

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

        'ADD BERTHING HOUR DELAYS TO DELAYSUM
        DelaySum.Rows.Add({"VFM"})
        DelaySum.Rows.Add({"GOB"})
        DelaySum.Rows.Add({"POB"})


        If clsCLR.Exists() Then
            clsCLR.RetrieveData()

            For Each crn As Crane In clsCLR.Crane
                Dim CraneControl As New CraneCtl(crn)
                CraneControl.PopulateDelays()
                TabControl1.TabPages.Add(CraneControl.tabCraneLog.TabPages($"tab{crn.CraneName}"))
            Next
        End If

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
                ctlCrane.Refresh()
            End With
        End Try


    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        On Error Resume Next
        Dim tabSelected As TabPage = DirectCast(sender, TabControl).SelectedTab

        Select Case tabSelected.Text.ToString
            Case "General Information"
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

            Case "More Information"
                CraneSummary()
                DelaySummary()
        End Select
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
        Dim rowindex As Integer
        Dim desclist As New List(Of String)
        For Each row As DataGridViewRow In DelaySum.Rows
            desclist.Add(row.Cells.Item(0).Value)
        Next

        rowindex = desclist.IndexOf(cmbDelay.Text)

        With DelaySum.Rows(rowindex).Cells
            mskDelaystart.Text = GetMilTime(.Item(1).Value)
            mskDelayend.Text = GetMilTime(.Item(2).Value)
        End With
    End Sub

    Private Sub mskDelayend_KeyDown(sender As Object, e As KeyEventArgs) Handles mskDelayend.KeyDown
If e.KeyCode = Keys.Enter Then
            Dim rowindex As Integer
            Dim desclist As New List(Of String)
            Dim delaystart As Date
            Dim delayend As Date

            For Each row As DataGridViewRow In DelaySum.Rows
                desclist.Add(row.Cells.Item(0).Value)
            Next

            rowindex = desclist.IndexOf(cmbDelay.Text)

            With DelaySum.Rows(rowindex).Cells
                delaystart = GetDateTime(mskDelaystart.Text)
                delayend = GetDateTime(mskDelayend.Text)
                .Item(1).Value = delaystart
                .Item(2).Value = delayend
                .Item(3).Value = GetSpanHours(delaystart, delayend)
            End With


            With clsCLR.CraneLogsData.BerthingHourDelays 'clear then add all
                .Clear()
                For Each row As DataGridViewRow In DelaySum.Rows
                    If CDbl(row.Cells(3).Value) > 0 Then 'only add delays that has total value > 0
                        .AddBerthingHourDelaysRow(berthdelay:=row.Cells(0).Value,
                                                 delaystart:=row.Cells(1).Value,
                                                 delayend:=row.Cells(2).Value,
                                                 delayhours:=row.Cells(3).Value)
                    End If
                Next
            End With
            DelaySummary()
        End If
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        'set remaning details to respective properties
        With clsCLR
            .LastPort = mskLastPort.Text
            .NextPort = mskNextPort.Text
        End With

        Dim result As MsgBoxResult
        If clsCLR.Exists Then
            result = MsgBox("Update Crane Log Report?", vbYesNo)
            If result = vbYes Then
                If clsCLR.CancelExistingCraneLogsReport() Then 'cancelled succesfully
                    clsCLR.Save()
                End If
            End If
            Else
            result = MsgBox("Continue Saving?", vbYesNo)
            If result = vbYes Then
                clsCLR.Save()
            End If
        End If



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

End Class
