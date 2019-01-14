Imports Reports
Imports Reports.ReportFunctions

Public Class CraneCtl

    Public Sub New(ByRef Crane As Crane)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        AddHandlersToTextboxes()
        AddHandlerstoDatagridViews()
        AddHandlersToInputs()

        crnCrane = Crane
        With Crane
            Me.tabCrane.Name = "tab" & .CraneName
            Me.tabCrane.Text = .CraneName
            Me.lblGantry.Text = .CraneName
        End With


        ContainerDsc.AutoGenerateColumns = False
        ContainerLoad.AutoGenerateColumns = False
        GearboxDsc.AutoGenerateColumns = False
        GearboxLoad.AutoGenerateColumns = False
        HatchDsc.AutoGenerateColumns = False
        HatchLoad.AutoGenerateColumns = False

        With crnCrane
            dischargeContainers.Table = .Moves.Container
            dischargeContainers.RowFilter = "actual_ib is not null and actual_ib <> ''"
            loadContainers.Table = .Moves.Container
            loadContainers.RowFilter = "actual_ob is not null and actual_ob <> ''"

            dischargeGearboxes.Table = .Moves.Gearbox
            dischargeGearboxes.RowFilter = "actual_ib is not null and actual_ib <> ''"
            loadGearboxes.Table = .Moves.Gearbox
            loadGearboxes.RowFilter = "actual_ob is not null and actual_ob <> ''"

            openingHatchcovers.Table = .Moves.Hatchcover
            openingHatchcovers.RowFilter = "actual_ib is not null and actual_ib <> ''"
            closingHatchcovers.Table = .Moves.Hatchcover
            closingHatchcovers.RowFilter = "actual_ob is not null and actual_ob <> ''"
            'pending for delays
        End With

        ContainerDsc.DataSource = dischargeContainers
        ContainerLoad.DataSource = loadContainers
        GearboxDsc.DataSource = dischargeGearboxes
        GearboxLoad.DataSource = loadGearboxes
        HatchDsc.DataSource = openingHatchcovers
        HatchLoad.DataSource = closingHatchcovers

        mskFirst.Text = GetMilTime(crnCrane.FirstMove)
        mskLast.Text = GetMilTime(crnCrane.LastMove)


    End Sub

    Private Sub AddHandlersToInputs()
        'Reflect First and Last Move
        AddHandler mskFirst.LostFocus, AddressOf FirstLastMove_LostFocus
        AddHandler mskLast.LostFocus, AddressOf FirstLastMove_LostFocus
    End Sub

    Private Sub AddHandlerstoDatagridViews()
        'add handlers when losing focus from all datagridviews
        AddHandler ContainerDsc.LostFocus, AddressOf Refresh_info
        AddHandler ContainerLoad.LostFocus, AddressOf Refresh_info
        AddHandler GearboxDsc.LostFocus, AddressOf Refresh_info
        AddHandler GearboxLoad.LostFocus, AddressOf Refresh_info
        AddHandler HatchDsc.LostFocus, AddressOf Refresh_info
        AddHandler HatchLoad.LostFocus, AddressOf Refresh_info
        AddHandler dgvDelays.LostFocus, AddressOf Refresh_info

        'Add ConfirmDelete Handler
        AddHandler ContainerDsc.UserDeletingRow, AddressOf ConfirmDelete
        AddHandler ContainerLoad.UserDeletingRow, AddressOf ConfirmDelete
        AddHandler GearboxDsc.UserDeletingRow, AddressOf ConfirmDelete
        AddHandler GearboxLoad.UserDeletingRow, AddressOf ConfirmDelete
        AddHandler HatchDsc.UserDeletingRow, AddressOf ConfirmDelete
        AddHandler HatchLoad.UserDeletingRow, AddressOf ConfirmDelete
        AddHandler dgvDelays.UserDeletingRow, AddressOf ConfirmDelete

        'Add DeletedSuccesfully Prompt Handler
        AddHandler ContainerDsc.UserDeletedRow, AddressOf DeletedPrompt
        AddHandler ContainerLoad.UserDeletedRow, AddressOf DeletedPrompt
        AddHandler GearboxDsc.UserDeletedRow, AddressOf DeletedPrompt
        AddHandler GearboxLoad.UserDeletedRow, AddressOf DeletedPrompt
        AddHandler HatchDsc.UserDeletedRow, AddressOf DeletedPrompt
        AddHandler HatchLoad.UserDeletedRow, AddressOf DeletedPrompt
        AddHandler dgvDelays.UserDeletedRow, AddressOf DeletedPrompt
    End Sub

    Private Sub AddHandlersToTextboxes()
        'ADD HANDLERS TO RESTRICT TEXTBOXES TO NUMERIC INPUT
        AddHandler txtBox20.KeyPress, AddressOf Common_Numeric
        AddHandler txtBox40.KeyPress, AddressOf Common_Numeric
        AddHandler txtBox45.KeyPress, AddressOf Common_Numeric
        AddHandler txtGear20.KeyPress, AddressOf Common_Numeric
        AddHandler txtGear40.KeyPress, AddressOf Common_Numeric
        AddHandler txtHatch20.KeyPress, AddressOf Common_Numeric
        AddHandler txtHatch40.KeyPress, AddressOf Common_Numeric
    End Sub

    Private Sub SetComboBoxSelectionTo0()
        cmbBound.SelectedIndex = 0
        cmbDelays.SelectedIndex = 0
        cmbFreight.SelectedIndex = 0
        cmbGearmove.SelectedIndex = 0
        cmbHatchmove.SelectedIndex = 0
        cmbMovekind.SelectedIndex = 0
    End Sub

    Private dischargeContainers As New DataView
    Private loadContainers As New DataView
    Private dischargeGearboxes As New DataView
    Private loadGearboxes As New DataView
    Private openingHatchcovers As New DataView
    Private closingHatchcovers As New DataView
    ' pending for delays
    Private crnCrane As Crane

    Private Sub Refresh_info()
        With crnCrane
            txtMoves.Text = .Moves.TotalMoves
            txtGhours.Text = Format(.GrossWorkingHours, "0.00")
            txtGprod.Text = Format(.GrossProductivity, "0.00")
            txtNhours.Text = Format(.NetWorkingHours, "0.00")
            txtNprod.Text = Format(.NetProductivity, "0.00")

            With .Moves.Container
                txtD20.Text = .AsEnumerable.Where(Function(row) ParseDBNulltoString(row("actual_ib")) <> "").Sum(Function(row) CInt(row("cntsze20").ToString))
                txtD40.Text = .AsEnumerable.Where(Function(row) ParseDBNulltoString(row("actual_ib")) <> "").Sum(Function(row) CInt(row("cntsze40").ToString))
                txtD45.Text = .AsEnumerable.Where(Function(row) ParseDBNulltoString(row("actual_ib")) <> "").Sum(Function(row) CInt(row("cntsze45").ToString))
                txtL20.Text = .AsEnumerable.Where(Function(row) ParseDBNulltoString(row("actual_ob")) <> "").Sum(Function(row) CInt(row("cntsze20").ToString))
                txtL40.Text = .AsEnumerable.Where(Function(row) ParseDBNulltoString(row("actual_ob")) <> "").Sum(Function(row) CInt(row("cntsze40").ToString))
                txtL45.Text = .AsEnumerable.Where(Function(row) ParseDBNulltoString(row("actual_ob")) <> "").Sum(Function(row) CInt(row("cntsze45").ToString))
            End With
            With .Moves.Hatchcover
                txtHD20.Text = .AsEnumerable.Where(Function(row) ParseDBNulltoString(row("actual_ib")) <> "").Sum(Function(row) CInt(row("cvrsze20").ToString))
                txtHD40.Text = .AsEnumerable.Where(Function(row) ParseDBNulltoString(row("actual_ib")) <> "").Sum(Function(row) CInt(row("cvrsze40").ToString))
                txtHL20.Text = .AsEnumerable.Where(Function(row) ParseDBNulltoString(row("actual_ob")) <> "").Sum(Function(row) CInt(row("cvrsze20").ToString))
                txtHL40.Text = .AsEnumerable.Where(Function(row) ParseDBNulltoString(row("actual_ob")) <> "").Sum(Function(row) CInt(row("cvrsze40").ToString))
            End With

            With .Moves.Gearbox
                txtGD20.Text = .AsEnumerable.Where(Function(row) ParseDBNulltoString(row("actual_ib")) <> "").Sum(Function(row) CInt(row("gbxsze20").ToString))
                txtGD40.Text = .AsEnumerable.Where(Function(row) ParseDBNulltoString(row("actual_ib")) <> "").Sum(Function(row) CInt(row("gbxsze40").ToString))
                txtGL20.Text = .AsEnumerable.Where(Function(row) ParseDBNulltoString(row("actual_ob")) <> "").Sum(Function(row) CInt(row("gbxsze20").ToString))
                txtGL40.Text = .AsEnumerable.Where(Function(row) ParseDBNulltoString(row("actual_ob")) <> "").Sum(Function(row) CInt(row("gbxsze40").ToString))
            End With


        End With

        txtDDelay.Text = Format(crnCrane.Delays.Deductable.Totalhours, "0.00")
        txtBreaks.Text = Format(crnCrane.Delays.Break.Totalhours, "0.00")
        txtNDelays.Text = Format(crnCrane.Delays.Nondeductable.Totalhours, "0.00")
    End Sub

    Private Sub FirstLastMove_LostFocus(sender As Object, e As EventArgs) 'handler for mskFirst and mskLast
        If IsValidDate({mskFirst, mskLast}) Then
            crnCrane.FirstMove = GetDateTime(mskFirst.Text)
            crnCrane.LastMove = GetDateTime(mskLast.Text)
            Refresh_info()
        End If
    End Sub

    Private Sub btnAddDelay_Click(sender As Object, e As EventArgs) Handles btnAddDelay.Click
        If IsInputValid({cmbDelays, mskDescription}) And IsValidDate({mskFrom, mskTo}) Then
            Dim delayTable As DataTable = crnCrane.Delays.Tables(cmbDelays.Text)
            Dim delayfrom As Date = GetDateTime(mskFrom.Text)
            Dim delayto As Date = GetDateTime(mskTo.Text)
            Dim span As TimeSpan = delayto.Subtract(delayfrom)

            delayTable.Rows.Add(mskDescription.Text, delayfrom, delayto, span.TotalHours)
            PopulateDelays()
            EmptyOutControls({cmbDelays, mskDescription, mskFrom, mskTo})
        End If
    End Sub

    Private Function IsValidDate(p() As MaskedTextBox) As Boolean
        For Each msk As Control In p
            If msk.Text.Contains("_") Then
                msk.ForeColor = Color.Red
                Return False
            Else
                msk.ForeColor = Color.Empty
            End If

        Next

        If GetDateTime(mskFrom.Text) > GetDateTime(mskTo.Text) Then
            mskFrom.ForeColor = Color.Red
            mskTo.ForeColor = Color.Red
            Return False
        Else
            mskFrom.ForeColor = Color.Empty
            mskTo.ForeColor = Color.Empty
        End If
        Return True
    End Function

    Private Sub btnCtnAdd_Click(sender As Object, e As EventArgs) Handles btnCtnAdd.Click
        If IsInputValid({cmbBound, cmbMovekind, cmbFreight, txtBox20, txtBox40, txtBox45}) Then
            Dim actualOB As String = GetBound(cmbBound.Text)(0)
            Dim actualIB As String = GetBound(cmbBound.Text)(1)

            Dim movekind As String = TranslateMoveKind(cmbMovekind.Text)
            Dim category As String = TranslateCategory(cmbMovekind.Text)
            Dim Freight As String = GetFreight(cmbFreight.Text)

            Dim count20 As Long = CLng(0 & txtBox20.Text)
            Dim count40 As Long = CLng(0 & txtBox40.Text)
            Dim count45 As Long = CLng(0 & txtBox45.Text)

            crnCrane.Moves.Container.AddContainerRow(movekind,
                                                     actualOB,
                                                     actualIB,
                                                     Freight,
                                                     category,
                                                     count20,
                                                     count40,
                                                     count45)
            Refresh_info()
            ContainerDsc.Refresh()
            ContainerLoad.Refresh()
            EmptyOutControls({cmbBound, cmbMovekind, cmbFreight, txtBox20, txtBox40, txtBox45})
        End If
    End Sub

    Private Sub EmptyOutControls(p() As Control)
        For Each ctl As Control In p
            ctl.ResetText()
        Next
    End Sub

    Private Function GetFreight(text As String) As String
        Select Case text
            Case "FULL"
                Return "FCL"
            Case "EMPTY"
                Return "MTY"
            Case Else
                Return Nothing
        End Select
    End Function


    Private Function TranslateCategory(text As String) As String
        Select Case text
            Case "TRANSHIPMENT"
                Return "TRSHP"
            Case Else
                Return Nothing
        End Select
    End Function

    Private Function TranslateMoveKind(text As String) As String
        Select Case text
            Case "DISCHARGE"
                Return "DSCH"
            Case "LOADING"
                Return "LOAD"
            Case "SVD"
                Return "SHFT"
            Case "SOB"
                Return "SHOB"
            Case Else
                Return Nothing
        End Select
    End Function

    Private Function GetBound(text As String) As String()
        Select Case text
            Case "DISCHARGE", "OPENING"
                Return {Nothing, crnCrane.Registry}
            Case "LOADING", "CLOSING"
                Return {crnCrane.Registry, Nothing}
            Case Else
                Return {Nothing, Nothing}
        End Select
    End Function

    Private Sub btnGearAdd_Click(sender As Object, e As EventArgs) Handles btnGearAdd.Click
        If IsInputValid({cmbGearmove, txtGearbay, txtGear20, txtGear40}) Then
            Dim actualOB As String = GetBound(cmbGearmove.Text)(0)
            Dim actualIB As String = GetBound(cmbGearmove.Text)(1)

            Dim count20 As Long = CLng(0 & txtGear20.Text)
            Dim count40 As Long = CLng(0 & txtGear40.Text)

            crnCrane.Moves.Gearbox.AddGearboxRow(actualOB,
                                                 actualIB,
                                                 txtGearbay.Text,
                                                 count20,
                                                 count40)
            Refresh_info()
            GearboxDsc.Refresh()
            GearboxLoad.Refresh()
            EmptyOutControls({cmbGearmove, txtGearbay, txtGear20, txtGear40})
        End If
    End Sub

    Private Function IsInputValid(p() As Control) As Boolean
        For Each txtbox As Control In p
            If txtbox.Text = "" Then
                If txtbox.GetType Is GetType(ComboBox) Then DirectCast(txtbox, ComboBox).DroppedDown = True
                txtbox.Focus()
                txtbox.BackColor = Color.Red
                Return False
            Else
                txtbox.BackColor = Color.Empty
            End If
        Next
        Return True
    End Function

    Private Sub btnHatchAdd_Click(sender As Object, e As EventArgs) Handles btnHatchAdd.Click
        If IsInputValid({cmbHatchmove, txtHatchbay, txtHatch20, txtHatch40}) Then
            Dim actualOB As String = GetBound(cmbHatchmove.Text)(0)
            Dim actualIB As String = GetBound(cmbHatchmove.Text)(1)

            Dim count20 As Long = CLng(0 & txtHatch20.Text)
            Dim count40 As Long = CLng(0 & txtHatch40.Text)

            crnCrane.Moves.Hatchcover.AddHatchcoverRow(actualOB,
                                                       actualIB,
                                                       txtHatchbay.Text,
                                                       count20,
                                                       count40)
            Refresh_info()
            HatchDsc.Refresh()
            HatchLoad.Refresh()
            EmptyOutControls({cmbHatchmove, txtHatchbay, txtHatch20, txtHatch40})
        End If
    End Sub

    Public Sub PopulateDelays()

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

    Private Sub Common_Numeric(sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) 'Handler for Numeric Fields
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub Refresh_LostFocus(sender As Object, ByVal e As EventArgs)
        Refresh_info()
    End Sub

    Private Sub dgvDelays_LostFocus(sender As Object, e As EventArgs) Handles dgvDelays.LostFocus

        crnCrane.Delays.Clear()

        For Each row As DataGridViewRow In dgvDelays.Rows
            Dim tempDatatable As DataTable = crnCrane.Delays.Tables(row.Cells(0).Value)
            With tempDatatable
                tempDatatable.Rows.Add(row.Cells(1).Value,
                                        row.Cells(2).Value,
                                        row.Cells(3).Value,
                                        row.Cells(4).Value)
            End With
        Next

    End Sub

    Private Sub CraneCtl_ControlAdded(sender As Object, e As ControlEventArgs) Handles Me.ControlAdded
        SetComboBoxSelectionTo0()
    End Sub

    Private Sub ConfirmDelete(sender As Object, e As DataGridViewRowCancelEventArgs)
        Dim result = MsgBox("Delete Row?", vbYesNo)
        If result = vbYes Then
            e.Cancel = False

        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub DeletedPrompt(sender As Object, e As DataGridViewRowEventArgs)
        crnCrane.Moves.AcceptChanges()
        crnCrane.Delays.AcceptChanges()
        MsgBox("Succesfully Deleted")
    End Sub

End Class
