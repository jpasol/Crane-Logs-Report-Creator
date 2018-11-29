Imports Reports

Public Class CraneCtl

    Public Sub New(Crane As String, Registry As String, Connection As ADODB.Connection)

        ' This call is required by the designer.
        InitializeComponent()
        crnCrane = New Crane(Crane, Registry, Connection)
        Me.tabCrane.Name = Crane
        Me.tabCrane.Text = Crane
        Me.lblGantry.Text = Crane
        mapMoves()
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private crnCrane As Crane
    Private dscMoves As DataTable
    Private loadMoves As New DataTable
    Private Sub mapMoves()
        SummarizeMoves(crnCrane.Moves.Tables("Inbound"), ContainerDsc)
        SummarizeMoves(crnCrane.Moves.Tables("Outbound"), ContainerLoad)

    End Sub
    Private Sub SummarizeMoves(Moves As DataTable, ByRef SumMoves As DataGridView)
        Dim strFreight() As String = {"FCL", "MTY"}
        Dim cntSizes() As Short = {20, 40, 45}

        For Each freight In strFreight
            Dim rowMoves As DataRow
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

    Private Sub ContainerLoad_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles ContainerLoad.CellValueChanged
        Dim Total(3) As Short

        For count As Integer = 1 To 3
            For Each row As DataGridViewRow In ContainerLoad.Rows
                Total(count) += row.Cells(count).Value
            Next
        Next
        txtL20.Text = Total(1)
        txtL40.Text = Total(2)
        txtL45.Text = Total(3)
    End Sub

    Private Sub ContainerDsc_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles ContainerDsc.CellValueChanged
        Dim Total(3) As Short

        For count As Integer = 1 To 3
            For Each row As DataGridViewRow In ContainerDsc.Rows
                Total(count) += row.Cells(count).Value
            Next
        Next
        txtD20.Text = Total(1)
        txtD40.Text = Total(2)
        txtD45.Text = Total(3)
    End Sub

    Private Sub ContainerLoad_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles ContainerLoad.RowsRemoved
        Dim Total(3) As Short

        For count As Integer = 1 To 3
            For Each row As DataGridViewRow In ContainerLoad.Rows
                Total(count) += row.Cells(count).Value
            Next
        Next
        txtL20.Text = Total(1)
        txtL40.Text = Total(2)
        txtL45.Text = Total(3)
    End Sub

    Private Sub ContainerDsc_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles ContainerDsc.RowsRemoved
        Dim Total(3) As Short

        For count As Integer = 1 To 3
            For Each row As DataGridViewRow In ContainerDsc.Rows
                Total(count) += row.Cells(count).Value
            Next
        Next
        txtD20.Text = Total(1)
        txtD40.Text = Total(2)
        txtD45.Text = Total(3)
    End Sub
End Class
