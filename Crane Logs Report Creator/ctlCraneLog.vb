Imports Reports

Public Class CraneCtl

    Public Sub New(Crane As String, Registry As String, Connection As ADODB.Connection)

        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

        crnCrane = New Crane(Crane, Registry, Connection)
        Me.tabCrane.Name = Crane
        Me.tabCrane.Text = Crane
        Me.lblGantry.Text = Crane
        mapMoves()
        Call ContainerDsc_LostFocus(ContainerDsc, New EventArgs)
        Call ContainerLoad_LostFocus(ContainerDsc, New EventArgs)

    End Sub
    Public crnCrane As Crane
    Private Sub mapMoves()
        SummarizeMoves(crnCrane.Moves.Tables("Inbound"), ContainerDsc)
        SummarizeMoves(crnCrane.Moves.Tables("Outbound"), ContainerLoad)


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
    Private Sub ContainerMoves(MoveKind As String)
        Try
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

                If MoveKind = "Discharge" Then
                    'ADD DSCH SUM
                    txtD20.Text = .AsEnumerable.Where(Function(row) row("move_kind") = MoveKind).Sum(Function(row) CInt(row("cntsze20").ToString))
                    txtD40.Text = .AsEnumerable.Where(Function(row) row("move_kind") = MoveKind).Sum(Function(row) CInt(row("cntsze40").ToString))
                    txtD45.Text = .AsEnumerable.Where(Function(row) row("move_kind") = MoveKind).Sum(Function(row) CInt(row("cntsze45").ToString))
                ElseIf MoveKind = "Loading" Then
                    txtL20.Text = .AsEnumerable.Where(Function(row) row("move_kind") = MoveKind).Sum(Function(row) CInt(row("cntsze20").ToString))
                    txtL40.Text = .AsEnumerable.Where(Function(row) row("move_kind") = MoveKind).Sum(Function(row) CInt(row("cntsze40").ToString))
                    txtL45.Text = .AsEnumerable.Where(Function(row) row("move_kind") = MoveKind).Sum(Function(row) CInt(row("cntsze45").ToString))
                End If

                With crnCrane
                    txtMoves.Text = .Moves.TotalMoves
                    txtGhours.Text = .GrossWorkingHours
                    txtGprod.Text = .GrossProductivity
                    txtNhours.Text = .NetWorkingHours
                    txtNprod.Text = .NetProductivity
                End With
            End With
        Catch
        End Try
    End Sub
    Private Sub GearboxMoves(MoveKind As String)
        Try
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

                If MoveKind = "Discharge" Then
                    'ADD Discharge SUM
                    txtGD20.Text = .AsEnumerable.Where(Function(row) row("move_kind") = MoveKind).Sum(Function(row) CInt(row("gbxsze20").ToString))
                    txtGD40.Text = .AsEnumerable.Where(Function(row) row("move_kind") = MoveKind).Sum(Function(row) CInt(row("gbxsze40").ToString))
                ElseIf MoveKind = "Loading" Then
                    'ADD LOAD SUM
                    txtGL20.Text = .AsEnumerable.Where(Function(row) row("move_kind") = MoveKind).Sum(Function(row) CInt(row("gbxsze20").ToString))
                    txtGL40.Text = .AsEnumerable.Where(Function(row) row("move_kind") = MoveKind).Sum(Function(row) CInt(row("gbxsze40").ToString))
                End If

                With crnCrane
                    txtMoves.Text = .Moves.TotalMoves
                    txtGhours.Text = .GrossWorkingHours
                    txtGprod.Text = .GrossProductivity
                    txtNhours.Text = .NetWorkingHours
                    txtNprod.Text = .NetProductivity
                End With
            End With
        Catch
        End Try
    End Sub
    Private Sub HatchMoves(MoveKind As String)
        Try
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

                If MoveKind = "Discharge" Then
                    'ADD Discharge SUM
                    txtHD20.Text = .AsEnumerable.Where(Function(row) row("move_kind") = MoveKind).Sum(Function(row) CInt(row("cvrsze20").ToString))
                    txtHD40.Text = .AsEnumerable.Where(Function(row) row("move_kind") = MoveKind).Sum(Function(row) CInt(row("cvrsze40").ToString))
                ElseIf MoveKind = "Loading" Then
                    'ADD LOAD SUM
                    txtHL20.Text = .AsEnumerable.Where(Function(row) row("move_kind") = MoveKind).Sum(Function(row) CInt(row("cvrsze20").ToString))
                    txtHL40.Text = .AsEnumerable.Where(Function(row) row("move_kind") = MoveKind).Sum(Function(row) CInt(row("cvrsze40").ToString))
                End If

                With crnCrane
                    txtMoves.Text = .Moves.TotalMoves
                    txtGhours.Text = .GrossWorkingHours
                    txtGprod.Text = .GrossProductivity
                    txtNhours.Text = .NetWorkingHours
                    txtNprod.Text = .NetProductivity
                End With
            End With
        Catch
        End Try
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
    End Sub


    Private Sub mskFirst_LostFocus(sender As Object, e As EventArgs) Handles mskFirst.LostFocus
        crnCrane.FirstMove = ReportFunctions.getDateTime(mskFirst.Text)

        With crnCrane
            txtMoves.Text = .Moves.TotalMoves
            txtGhours.Text = .GrossWorkingHours
            txtGprod.Text = .GrossProductivity
            txtNhours.Text = .NetWorkingHours
            txtNprod.Text = .NetProductivity
        End With
    End Sub

    Private Sub mskLast_LostFocus(sender As Object, e As EventArgs) Handles mskLast.LostFocus
        crnCrane.LastMove = ReportFunctions.getDateTime(mskLast.Text)

        With crnCrane
            txtMoves.Text = .Moves.TotalMoves
            txtGhours.Text = .GrossWorkingHours
            txtGprod.Text = .GrossProductivity
            txtNhours.Text = .NetWorkingHours
            txtNprod.Text = .NetProductivity
        End With
    End Sub

    Private Sub ContainerDsc_LostFocus(sender As Object, e As EventArgs) Handles ContainerDsc.LostFocus
        ContainerMoves("Discharge")
    End Sub
    Private Sub ContainerLoad_LostFocus(sender As Object, e As EventArgs) Handles ContainerLoad.LostFocus
        ContainerMoves("Loading")
    End Sub
    Private Sub GearboxDsc_LostFocus(sender As Object, e As EventArgs) Handles GearboxDsc.LostFocus
        GearboxMoves("Discharge")
    End Sub
    Private Sub GearboxLoad_LostFocus(sender As Object, e As EventArgs) Handles GearboxLoad.LostFocus
        GearboxMoves("Loading")
    End Sub

    Private Sub HatchDsc_LostFocus(sender As Object, e As EventArgs) Handles HatchDsc.LostFocus
        HatchMoves("Discharge")
    End Sub

    Private Sub HatchLoad_LostFocus(sender As Object, e As EventArgs) Handles HatchLoad.LostFocus
        HatchMoves("Loading")
    End Sub

    Private Sub btnAddDelay_Click(sender As Object, e As EventArgs) Handles btnAddDelay.Click
        Dim delaykind As String = cmbDelays.Text
        Dim description As String = mskDescription.Text
        Dim delayfrom As Date = ReportFunctions.getDateTime(mskFrom.Text)
        Dim delayto As Date = ReportFunctions.getDateTime(mskTo.Text)
        Dim span As TimeSpan = delayto.Subtract(delayfrom)


        dgvDelays.Rows.Add(delaykind, description, delayfrom, delayto, span.TotalHours)
        dgvDelays_LostFocus(dgvDelays, New EventArgs)
    End Sub

    Private Sub dgvDelays_LostFocus(sender As Object, e As EventArgs) Handles dgvDelays.LostFocus
        Delays()
        txtDDelay.Text = crnCrane.Delays.Deductable.Totalhours
        txtBreaks.Text = crnCrane.Delays.Break.Totalhours
        txtNDelays.Text = crnCrane.Delays.Nondeductable.Totalhours
    End Sub

    Private Sub dgvDelays_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDelays.CellContentClick

    End Sub
End Class
