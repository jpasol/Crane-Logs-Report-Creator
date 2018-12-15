Imports Reports
Imports Reports.ReportFunctions

Public Class CraneCtl

    Public Sub New(ByRef Crane As Crane)

        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        With Crane
            Me.tabCrane.Name = "tab" & .CraneName
            Me.tabCrane.Text = .CraneName
            Me.lblGantry.Text = .CraneName
        End With
        crnCrane = Crane

        With ContainerDsc
            .AutoGenerateColumns = False
            .Columns(0).DataPropertyName = "move_kind"
        End With

    End Sub
    Private crnCrane As Crane
    Public Sub mapMoves() 'turned to public for procing only by addcrane button
        SummarizeMoves(crnCrane.Moves.Tables("Inbound"), ContainerDsc)
        SummarizeMoves(crnCrane.Moves.Tables("Outbound"), ContainerLoad)

        Call ContainerDsc_LostFocus(ContainerDsc, New EventArgs) 'replaced here from new for integrity of access modifier 
        Call ContainerLoad_LostFocus(ContainerDsc, New EventArgs)

    End Sub
    Private Sub SummarizeMoves(Moves As DataTable, ByRef SumMoves As DataGridView)
        Dim strFreight() As String = {"FCL", "MTY"}
        Dim cntSizes() As Short = {20, 40, 45}

        For Each freight In strFreight
            Dim category As String
            Dim boxes(3) As String

            If freight = "FCL" Then category = "Full"
            If freight = "MTY" Then category = "Empty"
            boxes(0) = category
            For count As Integer = 1 To 3
                Dim col = count
                boxes(col) = (From units In Moves
                              Where units("freight_kind") = freight And
                                   units("nominal_length") = "NOM" & cntSizes(col - 1)
                              Select units).Count
            Next
            SumMoves.Rows.Add(boxes)
        Next
    End Sub
    Private Sub ContainerMoves()
        On Error Resume Next
        With crnCrane.Moves.Tables("Container")
            .Clear() 'empty out datatable
            For Each row As DataGridViewRow In ContainerDsc.Rows 'ADD DSCH DATA GRID VIEW ROWS TO DATATABLE
                Dim temprow As DataRow = .NewRow
                temprow("ctrmve") = row.Cells(0).Value
                temprow("move_kind") = "Discharge"
                temprow("cntsze20") = row.Cells(1).Value
                temprow("cntsze40") = row.Cells(2).Value
                temprow("cntsze45") = row.Cells(3).Value

                .Rows.Add(temprow)
            Next
            For Each row As DataGridViewRow In ContainerLoad.Rows 'ADD LOADING DATA GRID VIEW ROWS TO DATATABLE
                Dim temprow As DataRow = .NewRow
                temprow("ctrmve") = row.Cells(0).Value
                temprow("move_kind") = "Loading"
                temprow("cntsze20") = row.Cells(1).Value
                temprow("cntsze40") = row.Cells(2).Value
                temprow("cntsze45") = row.Cells(3).Value

                .Rows.Add(temprow)
            Next

        End With
        Refresh_info()
    End Sub
    Private Sub GearboxMoves()
        On Error Resume Next
        With crnCrane.Moves.Tables("Gearbox")
            .Clear() 'empty out datatable
            For Each row As DataGridViewRow In GearboxDsc.Rows 'ADD DSCH DATA GRID VIEW ROWS TO DATATABLE
                If Len(row.Cells(0).Value) <> 0 Then
                    Dim temprow As DataRow = .NewRow
                    temprow("move_kind") = "Discharge"
                    temprow("baynum") = row.Cells(0).Value
                    temprow("gbxsze20") = row.Cells(1).Value
                    temprow("gbxsze40") = row.Cells(2).Value

                    .Rows.Add(temprow)
                End If
            Next
            For Each row As DataGridViewRow In GearboxLoad.Rows 'ADD LOADING DATA GRID VIEW ROWS TO DATATABLE
                If Len(row.Cells(0).Value) <> 0 Then
                    Dim temprow As DataRow = .NewRow
                    temprow("move_kind") = "Loading"
                    temprow("baynum") = row.Cells(0).Value
                    temprow("gbxsze20") = row.Cells(1).Value
                    temprow("gbxsze40") = row.Cells(2).Value

                    .Rows.Add(temprow)
                End If
            Next

        End With
        Refresh_info()
    End Sub
    Private Sub HatchMoves()
        On Error Resume Next
        With crnCrane.Moves.Tables("Hatchcover")
                .Clear() 'empty out datatable
                For Each row As DataGridViewRow In HatchDsc.Rows 'ADD DSCH DATA GRID VIEW ROWS TO DATATABLE
                    If Len(row.Cells(0).Value) <> 0 Then
                        Dim temprow As DataRow = .NewRow
                        temprow("move_kind") = "Discharge"
                        temprow("baynum") = row.Cells(0).Value
                        temprow("cvrsze20") = row.Cells(1).Value
                        temprow("cvrsze40") = row.Cells(2).Value

                        .Rows.Add(temprow)
                    End If
                Next
                For Each row As DataGridViewRow In HatchLoad.Rows 'ADD LOADING DATA GRID VIEW ROWS TO DATATABLE
                    If Len(row.Cells(0).Value) <> 0 Then
                        Dim temprow As DataRow = .NewRow
                        temprow("move_kind") = "Loading"
                        temprow("baynum") = row.Cells(0).Value
                        temprow("cvrsze20") = row.Cells(1).Value
                        temprow("cvrsze40") = row.Cells(2).Value

                        .Rows.Add(temprow)
                    End If
                Next

            End With

        Refresh_info()
    End Sub
    Private Sub Delays()
        crnCrane.Delays.Tables("Deductable").Clear()
        crnCrane.Delays.Tables("Break").Clear()
        crnCrane.Delays.Tables("Nondeductable").Clear()


        Try
            For Each row As DataGridViewRow In dgvDelays.Rows
                Dim table = row.Cells(0).Value
                Dim temprow As DataRow
                With crnCrane.Delays.Tables(table)
                    temprow = .NewRow

                    temprow(0) = row.Cells(1).Value
                    temprow(1) = row.Cells(2).Value
                    temprow(2) = row.Cells(3).Value
                    temprow(3) = row.Cells(4).Value

                    .Rows.Add(temprow)
                End With
            Next

        Catch ex As Exception

        End Try
        Refresh_info()
    End Sub

    Private Sub Refresh_info()
        With crnCrane
            txtMoves.Text = .Moves.TotalMoves
            txtGhours.Text = .GrossWorkingHours
            txtGprod.Text = .GrossProductivity
            txtNhours.Text = .NetWorkingHours
            txtNprod.Text = .NetProductivity

            With .Moves.Tables("Container")
                txtD20.Text = .AsEnumerable.Where(Function(row) row("move_kind") = "Discharge").Sum(Function(row) CInt(row("cntsze20").ToString))
                txtD40.Text = .AsEnumerable.Where(Function(row) row("move_kind") = "Discharge").Sum(Function(row) CInt(row("cntsze40").ToString))
                txtD45.Text = .AsEnumerable.Where(Function(row) row("move_kind") = "Discharge").Sum(Function(row) CInt(row("cntsze45").ToString))
                txtL20.Text = .AsEnumerable.Where(Function(row) row("move_kind") = "Loading").Sum(Function(row) CInt(row("cntsze20").ToString))
                txtL40.Text = .AsEnumerable.Where(Function(row) row("move_kind") = "Loading").Sum(Function(row) CInt(row("cntsze40").ToString))
                txtL45.Text = .AsEnumerable.Where(Function(row) row("move_kind") = "Loading").Sum(Function(row) CInt(row("cntsze45").ToString))
            End With
            With .Moves.Tables("Hatchcover")
                txtHD20.Text = .AsEnumerable.Where(Function(row) row("move_kind") = "Discharge").Sum(Function(row) CInt(row("cvrsze20").ToString))
                txtHD40.Text = .AsEnumerable.Where(Function(row) row("move_kind") = "Discharge").Sum(Function(row) CInt(row("cvrsze40").ToString))
                txtHL20.Text = .AsEnumerable.Where(Function(row) row("move_kind") = "Loading").Sum(Function(row) CInt(row("cvrsze20").ToString))
                txtHL40.Text = .AsEnumerable.Where(Function(row) row("move_kind") = "Loading").Sum(Function(row) CInt(row("cvrsze40").ToString))
            End With

            With .Moves.Tables("Gearbox")
                txtGD20.Text = .AsEnumerable.Where(Function(row) row("move_kind") = "Discharge").Sum(Function(row) CInt(row("gbxsze20").ToString))
                txtGD40.Text = .AsEnumerable.Where(Function(row) row("move_kind") = "Discharge").Sum(Function(row) CInt(row("gbxsze40").ToString))
                txtGL20.Text = .AsEnumerable.Where(Function(row) row("move_kind") = "Loading").Sum(Function(row) CInt(row("gbxsze20").ToString))
                txtGL40.Text = .AsEnumerable.Where(Function(row) row("move_kind") = "Loading").Sum(Function(row) CInt(row("gbxsze40").ToString))
            End With


        End With

        txtDDelay.Text = crnCrane.Delays.Deductable.Totalhours
        txtBreaks.Text = crnCrane.Delays.Break.Totalhours
        txtNDelays.Text = crnCrane.Delays.Nondeductable.Totalhours
    End Sub

    Private Sub mskFirst_LostFocus(sender As Object, e As EventArgs) Handles mskFirst.LostFocus
        crnCrane.FirstMove = ReportFunctions.GetDateTime(mskFirst.Text)

        Refresh_info()
    End Sub

    Private Sub mskLast_LostFocus(sender As Object, e As EventArgs) Handles mskLast.LostFocus
        crnCrane.LastMove = ReportFunctions.GetDateTime(mskLast.Text)

        Refresh_info()
    End Sub

    Private Sub ContainerDsc_LostFocus(sender As Object, e As EventArgs) Handles ContainerDsc.LostFocus
        ContainerMoves()
    End Sub

    Private Sub ContainerLoad_LostFocus(sender As Object, e As EventArgs) Handles ContainerLoad.LostFocus
        ContainerMoves()
    End Sub

    Private Sub GearboxDsc_LostFocus(sender As Object, e As EventArgs) Handles GearboxDsc.LostFocus
        GearboxMoves()
    End Sub

    Private Sub GearboxLoad_LostFocus(sender As Object, e As EventArgs) Handles GearboxLoad.LostFocus
        GearboxMoves()
    End Sub

    Private Sub HatchDsc_LostFocus(sender As Object, e As EventArgs) Handles HatchDsc.LostFocus
        HatchMoves()
    End Sub

    Private Sub HatchLoad_LostFocus(sender As Object, e As EventArgs) Handles HatchLoad.LostFocus
        HatchMoves()
    End Sub

    Private Sub dgvDelays_LostFocus(sender As Object, e As EventArgs) Handles dgvDelays.LostFocus
        Delays()
    End Sub

    Private Sub btnAddDelay_Click(sender As Object, e As EventArgs) Handles btnAddDelay.Click
        Dim delayTable As DataTable = crnCrane.Delays.Tables(cmbDelays.Text)
        Dim delayfrom As Date = GetDateTime(mskFrom.Text)
        Dim delayto As Date = GetDateTime(mskTo.Text)
        Dim span As TimeSpan = delayto.Subtract(delayfrom)

        delayTable.Rows.Add(mskDescription.Text, delayfrom, delayto, span.TotalHours)
        PopulateDataGridViews()
    End Sub

    Private Sub btnCtnAdd_Click(sender As Object, e As EventArgs) Handles btnCtnAdd.Click

        crnCrane.Moves.Container.AddContainerRow(cmdCntmove.Text, StrConv(cmbMoveknd.Text, vbProperCase), txtBox20.Text, txtBox40.Text, txtBox45.Text)
        PopulateDataGridViews()

    End Sub

    Private Sub btnGearAdd_Click(sender As Object, e As EventArgs) Handles btnGearAdd.Click

        crnCrane.Moves.Gearbox.AddGearboxRow(cmbGearmove.Text, txtGearbay.Text, txtGear20.Text, txtGear40.Text)

        PopulateDataGridViews()

    End Sub

    Private Sub btnHatchAdd_Click(sender As Object, e As EventArgs) Handles btnHatchAdd.Click

        crnCrane.Moves.Hatchcover.AddHatchcoverRow(cmbHatchmove.Text, txtHatchbay.Text, txtHatch20.Text, txtHatch40.Text)
        PopulateDataGridViews()

    End Sub

    Public Sub PopulateDataGridViews()
        'container

        ContainerDsc.Rows.Clear()
        ContainerLoad.Rows.Clear()

        For Each row As DataRow In crnCrane.Moves.Container.Rows 'reflection to container view
            Dim containerTable As DataGridView
            If row("move_kind") = "Discharge" Then containerTable = ContainerDsc
            If row("move_kind") = "Loading" Then containerTable = ContainerLoad

            With containerTable.Rows
                .Add(row("ctrmve"), row("cntsze20"), row("cntsze40"), row("cntsze45"))
            End With
        Next


        'gearbox

        GearboxDsc.Rows.Clear()
        GearboxLoad.Rows.Clear()

        For Each row As DataRow In crnCrane.Moves.Gearbox.Rows 'reflection to gearbox view
            Dim gearboxTable As DataGridView
            If row("move_kind") = "Discharge" Then gearboxTable = GearboxDsc
            If row("move_kind") = "Loading" Then gearboxTable = GearboxLoad

            With gearboxTable.Rows
                .Add(row("baynum"), row("gbxsze20"), row("gbxsze40"))
            End With
        Next


        'hatchcover

        HatchDsc.Rows.Clear()
        HatchLoad.Rows.Clear()

        For Each row As DataRow In crnCrane.Moves.Hatchcover.Rows 'reflection to hatchcover view
            Dim hatchTable As DataGridView
            If row("move_kind") = "Discharge" Then hatchTable = HatchDsc
            If row("move_kind") = "Loading" Then hatchTable = HatchLoad

            With hatchTable.Rows
                .Add(row("baynum"), row("cvrsze20"), row("cvrsze40"))
            End With
        Next

        'delays

        dgvDelays.Rows.Clear()

        For Each table As DataTable In crnCrane.Delays.Tables 'reflection to delay view
            For Each row As DataRow In table.Rows
                With dgvDelays.Rows
                    Dim tablenameList As Object() = {table.TableName}
                    .Add(tablenameList.Concat(row.ItemArray).ToArray) 'table name union to row array
                End With
            Next
        Next

        Refresh_info()

    End Sub

End Class
