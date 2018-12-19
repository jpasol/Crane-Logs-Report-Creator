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

        ContainerDsc.AutoGenerateColumns = False
        ContainerLoad.AutoGenerateColumns = False
        GearboxDsc.AutoGenerateColumns = False
        GearboxLoad.AutoGenerateColumns = False
        HatchDsc.AutoGenerateColumns = False
        HatchLoad.AutoGenerateColumns = False

        With crnCrane
            dischargeContainers.Table = .Moves.Container
            loadContainers.Table = .Moves.Container
            dischargeGearboxes.Table = .Moves.Gearbox
            loadGearboxes.Table = .Moves.Gearbox
            openingHatchcovers.Table = .Moves.Hatchcover
            closingHatchcovers.Table = .Moves.Hatchcover
            'pending for delays
        End With

        ContainerDsc.DataSource = dischargeContainers
        ContainerLoad.DataSource = loadContainers
        GearboxDsc.DataSource = dischargeGearboxes
        GearboxLoad.DataSource = loadGearboxes
        HatchDsc.DataSource = openingHatchcovers
        HatchLoad.DataSource = closingHatchcovers

    End Sub
    Private dischargeContainers As New DataView
    Private loadContainers As New DataView
    Private dischargeGearboxes As New DataView
    Private loadGearboxes As New DataView
    Private openingHatchcovers As New DataView
    Private closingHatchcovers As New DataView
    Private delaysBinding As New BindingSource
    ' pending for delays
    Private crnCrane As Crane

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
        ContainerDsc.Refresh()
        ContainerLoad.Refresh()
    End Sub

    Private Sub btnGearAdd_Click(sender As Object, e As EventArgs) Handles btnGearAdd.Click

        crnCrane.Moves.Gearbox.AddGearboxRow(cmbGearmove.Text, txtGearbay.Text, txtGear20.Text, txtGear40.Text)
        GearboxDsc.Refresh()
        GearboxLoad.Refresh()

    End Sub

    Private Sub btnHatchAdd_Click(sender As Object, e As EventArgs) Handles btnHatchAdd.Click

        crnCrane.Moves.Hatchcover.AddHatchcoverRow(cmbHatchmove.Text, txtHatchbay.Text, txtHatch20.Text, txtHatch40.Text)
        HatchDsc.Refresh()
        HatchLoad.Refresh()


    End Sub

    Public Sub PopulateDataGridViews()
        ''container

        'ContainerDsc.Rows.Clear()
        'ContainerLoad.Rows.Clear()

        'For Each row As DataRow In crnCrane.Moves.Container.Rows 'reflection to container view
        '    Dim containerTable As DataGridView
        '    If row("move_kind") = "Discharge" Then containerTable = ContainerDsc
        '    If row("move_kind") = "Loading" Then containerTable = ContainerLoad

        '    With containerTable.Rows
        '        .Add(row("ctrmve"), row("cntsze20"), row("cntsze40"), row("cntsze45"))
        '    End With
        'Next


        ''gearbox

        'GearboxDsc.Rows.Clear()
        'GearboxLoad.Rows.Clear()

        'For Each row As DataRow In crnCrane.Moves.Gearbox.Rows 'reflection to gearbox view
        '    Dim gearboxTable As DataGridView
        '    If row("move_kind") = "Discharge" Then gearboxTable = GearboxDsc
        '    If row("move_kind") = "Loading" Then gearboxTable = GearboxLoad

        '    With gearboxTable.Rows
        '        .Add(row("baynum"), row("gbxsze20"), row("gbxsze40"))
        '    End With
        'Next


        ''hatchcover

        'HatchDsc.Rows.Clear()
        'HatchLoad.Rows.Clear()

        'For Each row As DataRow In crnCrane.Moves.Hatchcover.Rows 'reflection to hatchcover view
        '    Dim hatchTable As DataGridView
        '    If row("move_kind") = "Discharge" Then hatchTable = HatchDsc
        '    If row("move_kind") = "Loading" Then hatchTable = HatchLoad

        '    With hatchTable.Rows
        '        .Add(row("baynum"), row("cvrsze20"), row("cvrsze40"))
        '    End With
        'Next

        ''delays

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
