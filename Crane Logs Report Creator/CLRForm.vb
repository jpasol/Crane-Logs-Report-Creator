Public Class CLRForm
    Public Sub New(Registry As String, ByRef N4Connection As ADODB.Connection, ByRef OPConnection As ADODB.Connection)

        ' This call is required by the designer.
        InitializeComponent()
        clsCLR = New CLRClass(Registry, N4Connection, OPConnection)
        clrRegistry = Registry
        connN4 = N4Connection
        connOP = OPConnection
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private clsCLR As CLRClass
    Private connN4 As ADODB.Connection
    Private connOP As ADODB.Connection
    Private clrRegistry As String
    Private cranes(4) As CraneCtl
    Private dtContrMoves As New DataTable
    Private dtHCGBMoves As New DataTable

    Private Sub mapDetails()
        With clsCLR
            mskShippingLine.Text = .Details(.clrInfo.shiplines)
            mskVessel.Text = .Details(.clrInfo.name)
            mskRegistry.Text = .Details(.clrInfo.registry)
            mskVoyage.Text = .Details(.clrInfo.voynum)
            mskATA.Text = .Details(.clrInfo.ata)
            mskATD.Text = .Details(.clrInfo.atd)
        End With
    End Sub

    Private Sub CLRForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        mapDetails()

        '''''''''''Populate Columns for Container Moves datatable''''''''''''''
        dtContrMoves.Columns.Add("Move Kind")
        dtContrMoves.Columns.Add("Container")
        dtContrMoves.Columns.Add("20")
        dtContrMoves.Columns.Add("40")
        dtContrMoves.Columns.Add("45")
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    End Sub

    Private Sub mskATA_LostFocus(sender As Object, e As EventArgs) Handles mskATA.LostFocus
        mskBerthHours.Text = clsCLR.CalculateInfo("Hours between two Dates", {mskATA.Text, mskATD.Text})
    End Sub

    Private Sub mskATD_LostFocus(sender As Object, e As EventArgs) Handles mskATD.LostFocus
        mskBerthHours.Text = clsCLR.CalculateInfo("Hours between two Dates", {mskATA.Text, mskATD.Text})
    End Sub

    Private Sub mskFirstmve_LostFocus(sender As Object, e As EventArgs) Handles mskFirstmve.LostFocus
        mskGVWT.Text = clsCLR.CalculateInfo("Hours between two Dates", {mskFirstmve.Text, mskLastmve.Text})
    End Sub

    Private Sub mskLastmve_LostFocus(sender As Object, e As EventArgs) Handles mskLastmve.LostFocus
        mskGVWT.Text = clsCLR.CalculateInfo("Hours between two Dates", {mskFirstmve.Text, mskLastmve.Text})
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim strGC As String = "GC0" & txtGC.Text
        Try
            TabControl1.SelectTab(strGC)
        Catch ex As Exception
            cranes(txtGC.Text) = New CraneCtl(strGC, clrRegistry, connN4)

            TabControl1.TabPages.Add(cranes(txtGC.Text).tabCraneLog.TabPages(strGC))
            TabControl1.SelectTab(strGC)
        End Try


    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Dim tabSelected As TabPage = DirectCast(sender, TabControl).SelectedTab
        Select Case tabSelected.Text.ToString
            Case "More Information"
                Dim Freight() As String = {"Full", "Empty"}
                Dim Volume() As String = {"Import", "Export"}

                dtContrMoves.Clear()
                dtHCGBMoves.Clear()

                ProdDsc.Rows.Clear()
                ProdLoad.Rows.Clear()
                VolumeTEU.Rows.Clear()


                For Each gcrane In cranes 'fill moves 
                    If Not IsNothing(gcrane) Then

                        For Each row As DataGridViewRow In gcrane.ContainerDsc.Rows
                            Dim tempRow As DataRow
                            tempRow = dtContrMoves.NewRow

                            tempRow.Item(0) = "Discharge"
                            tempRow.Item(1) = row.Cells(0).Value
                            tempRow.Item(2) = row.Cells(1).Value
                            tempRow.Item(3) = row.Cells(2).Value
                            tempRow.Item(4) = row.Cells(3).Value

                            dtContrMoves.Rows.Add(tempRow)
                        Next
                        For Each row As DataGridViewRow In gcrane.ContainerLoad.Rows
                            Dim tempRow As DataRow
                            tempRow = dtContrMoves.NewRow

                            tempRow.Item(0) = "Loading"
                            tempRow.Item(1) = row.Cells(0).Value
                            tempRow.Item(2) = row.Cells(1).Value
                            tempRow.Item(3) = row.Cells(2).Value
                            tempRow.Item(4) = row.Cells(3).Value

                            dtContrMoves.Rows.Add(tempRow)
                        Next

                    End If
                Next

                For Each frght In Freight 'Populate Productivity Data grid views
                    ProdDsc.Rows.Add({frght})
                    ProdLoad.Rows.Add({frght})
                    For Each vol In Volume
                        VolumeTEU.Rows.Add({vol & " " & frght})
                    Next
                Next

        End Select
    End Sub

End Class
