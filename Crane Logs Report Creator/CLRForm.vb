Imports Reports.ReportFunctions
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

    Private Sub mapDetails()
        With clsCLR.CLRVessel
            mskShippingLine.Text = .Owner
            mskVessel.Text = .Name
            mskRegistry.Text = .Registry
            mskVoyage.Text = .InboundVoyage & " - " & .OutboundVoyage
            mskATA.Text = getMilTime(.ATA)
            mskATD.Text = getMilTime(.ATD)
        End With
    End Sub

    Private Sub CLRForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        mapDetails()

        'ADD BERTHING HOUR DELAYS TO DELAYSUM
        DelaySum.Rows.Add({"Vessel Formalities"})
        DelaySum.Rows.Add({"GOB Unlashing and GC positioning"})
        DelaySum.Rows.Add({"Waiting for Tug Boat / POB"})
    End Sub

    Private Sub mskATA_LostFocus(sender As Object, e As EventArgs) Handles mskATA.LostFocus
    End Sub

    Private Sub mskATD_LostFocus(sender As Object, e As EventArgs) Handles mskATD.LostFocus
    End Sub

    Private Sub mskFirstmve_LostFocus(sender As Object, e As EventArgs) Handles mskFirstmve.LostFocus
    End Sub

    Private Sub mskLastmve_LostFocus(sender As Object, e As EventArgs) Handles mskLastmve.LostFocus
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
            End With
        End Try


    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        On Error Resume Next
        Dim tabSelected As TabPage = DirectCast(sender, TabControl).SelectedTab

        Select Case tabSelected.Text.ToString
            Case "General Information"
                With clsCLR
                    mskFirstmve.Text = getMilTime(.FirstMove)
                    mskLastmve.Text = getMilTime(.LastMove)
                    mskMoves.Text = .TotalMoves
                    mskDensity.Text = .CraneDensity
                    mskBerthHours.Text = .TotalBerthHours
                    mskNetBerth.Text = .NetBerthHours
                    mskGrossBerthProd.Text = .GrossBerthProdRate
                    mskNetBerthProd.Text = .NetBerthProdRate
                    mskGrossWorkingTime.Text = .GrossVesselWorkingTime
                    mskNetWorkingTime.Text = .NetVesselWorkingTime
                    mskGrossVesselProd.Text = .GrossVesselProdRate
                    mskNetVesselProd.Text = .NetVesselProdRate
                    mskGrossHours.Text = .TotalGrossWorkingHours
                    mskNetHours.Text = .TotalNetWorkingHours
                    mskGrossCraneProd.Text = .GrossCraneProductivity
                    mskNetCraneProd.Text = .NetCraneProductivity
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


        Dim freight() As String = {"Discharge", "Loading"}
        Dim freightkind() As String = {"Full", "Empty"}


        For Each frght In freight

            For Each frtknd In freightkind
                Dim twenty As Short
                Dim forty As Short
                Dim ffive As Short
                Dim unit As Short
                Dim teu As Single

                With clsCLR.Crane.AsEnumerable
                    twenty = .Sum(Function(crn) crn.Moves.Container.Total20(frght, frtknd))
                    forty = .Sum(Function(crn) crn.Moves.Container.Total40(frght, frtknd))
                    ffive = .Sum(Function(crn) crn.Moves.Container.Total45(frght, frtknd))
                    unit = twenty + forty + ffive
                    teu = (twenty) + (forty * 2) + (ffive * 2.25)

                    ProdSum.Rows.Add({frght, frtknd, twenty, forty, ffive, unit, teu})

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
            mskDelaystart.Text = getMilTime(.Item(1).Value)
            mskDelayend.Text = getMilTime(.Item(2).Value)
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
                delaystart = getDateTime(mskDelaystart.Text)
                delayend = getDateTime(mskDelayend.Text)
                .Item(1).Value = delaystart
                .Item(2).Value = delayend
                .Item(3).Value = getSpanHours(delaystart, delayend)
            End With


            With clsCLR.CraneLogsData.BerthingHourDelays 'clear then add all
                .Clear()
                For Each row As DataGridViewRow In DelaySum.Rows
                    If CDbl(row.Cells(3).Value) > 0 Then 'only add delays that has total value > 0
                        .AddBerthingHourDelaysRow(berthdelay:=row.Cells(0).Value,
                                                 delaystart:=row.Cells(1).Value,
                                                 delayend:=row.Cells(2).Value)
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

        clsCLR.Save()


    End Sub

End Class
