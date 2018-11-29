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
        Select Case tabSelected.Name.ToString
            Case "More Information"
                Dim dscfull(2) As Short
                Dim dscempty(2) As Short
                Dim loadfull(2) As Short
                Dim loadempty(2) As Short

                For Each gcrane In cranes
                    If Not IsNothing(gcrane) Then
                        For count As Integer = 0 To 2
                            dscfull(count) += gcrane.ContainerDsc.Rows(0).Cells(count).ToString
                            dscempty(count) += gcrane.ContainerDsc.Rows(0).Cells(count).ToString
                            loadfull(count) += gcrane.ContainerDsc.Rows(1).Cells(count).ToString
                            loadempty(count) += gcrane.ContainerDsc.Rows(1).Cells(count).ToString
                        Next
                    End If
                Next


        End Select
    End Sub
End Class
