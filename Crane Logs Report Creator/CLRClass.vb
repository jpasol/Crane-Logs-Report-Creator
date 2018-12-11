Imports ADODB
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Windows.Forms
Imports Reports
Imports Reports.ReportFunctions
Public Class CLRClass
    Implements IReportswSave
    Implements ICraneLogsReport

    Public Sub New(Registry As String, ByRef N4connection As ADODB.Connection, ByRef OPConnection As ADODB.Connection, Username As String)
        CLRVessel = New Vessel(Registry, N4connection)
        Me.N4Connection = N4connection
        Me.OPConnection = OPConnection
        Me.Crane = New List(Of Crane)
        Me.username = Username
    End Sub
    Private clrData As New CraneLogsData
    Private username As String
    Private datenow As Date
    Public ReadOnly Property N4Connection As Connection Implements IReportswSave.N4Connection
    Public ReadOnly Property OPConnection As Connection Implements IReportswSave.OPConnection
    Public ReadOnly Property CLRVessel As Vessel Implements ICraneLogsReport.Vessel
    Public ReadOnly Property Crane As List(Of Crane) Implements ICraneLogsReport.Crane
    Public Property LastPort As Object Implements ICraneLogsReport.LastPort
    Public Property NextPort As Object Implements ICraneLogsReport.NextPort

    Public ReadOnly Property TotalMoves As Double Implements ICraneLogsReport.TotalMoves
        Get
            For Each crn As Crane In Crane
                If Crane IsNot Nothing Then TotalMoves += crn.Moves.TotalMoves
            Next
            Return TotalMoves
        End Get
    End Property

    Public ReadOnly Property FirstMove As Date Implements ICraneLogsReport.FirstMove
        Get
            Return Crane.AsEnumerable.Where(Function(crn) crn IsNot Nothing).Min(Function(mve) mve.FirstMove)
        End Get
    End Property

    Public ReadOnly Property LastMove As Date Implements ICraneLogsReport.LastMove
        Get
            Return Crane.AsEnumerable.Where(Function(crn) crn IsNot Nothing).Max(Function(mve) mve.LastMove)
        End Get
    End Property

    Public ReadOnly Property CraneDensity As Double Implements ICraneLogsReport.CraneDensity
        Get
            Return TotalMoves / Crane.AsEnumerable.Where(Function(crn) crn IsNot Nothing).Max(Function(mve) mve.Moves.TotalMoves)
        End Get
    End Property

    Public ReadOnly Property TotalBerthHours As Double Implements ICraneLogsReport.TotalBerthHours
        Get
            With CLRVessel
                Return getSpanHours(.ATA, .ATD)
            End With
        End Get
    End Property

    Public ReadOnly Property NetBerthHours As Double Implements ICraneLogsReport.NetBerthHours
        Get
            Return TotalBerthHours - clrData.BerthingHourDelays.Totalhours
        End Get
    End Property

    Public ReadOnly Property GrossBerthProdRate As Double Implements ICraneLogsReport.GrossBerthProdRate
        Get
            Return TotalMoves / TotalBerthHours
        End Get
    End Property

    Public ReadOnly Property NetBerthProdRate As Double Implements ICraneLogsReport.NetBerthProdRate
        Get
            Return TotalMoves / NetBerthHours
        End Get
    End Property

    Public ReadOnly Property GrossVesselWorkingTime As Double Implements ICraneLogsReport.GrossVesselWorkingTime
        Get
            Return getSpanHours(FirstMove, LastMove)
        End Get
    End Property

    Public ReadOnly Property NetVesselWorkingTime As Double Implements ICraneLogsReport.NetVesselWorkingTime
        Get
            NetVesselWorkingTime = GrossVesselWorkingTime
            For Each crn As Crane In Crane
                If Crane IsNot Nothing Then NetVesselWorkingTime -= (crn.Delays.Deductable.Totalhours + crn.Delays.Break.Totalhours)
            Next
            Return NetVesselWorkingTime
        End Get
    End Property

    Public ReadOnly Property GrossVesselProdRate As Double Implements ICraneLogsReport.GrossVesselProdRate
        Get
            Return TotalMoves / GrossVesselWorkingTime
        End Get
    End Property

    Public ReadOnly Property NetVesselProdRate As Double Implements ICraneLogsReport.NetVesselProdRate
        Get
            Return TotalMoves / NetVesselWorkingTime
        End Get
    End Property

    Public ReadOnly Property TotalGrossWorkingHours As Double Implements ICraneLogsReport.TotalGrossWorkingHours
        Get
            For Each crn As Crane In Crane
                If Crane IsNot Nothing Then TotalGrossWorkingHours += crn.GrossWorkingHours
            Next
            Return TotalGrossWorkingHours
        End Get
    End Property

    Public ReadOnly Property TotalNetWorkingHours As Double Implements ICraneLogsReport.TotalNetWorkingHours
        Get
            For Each crn As Crane In Crane
                If Crane IsNot Nothing Then TotalNetWorkingHours += crn.NetWorkingHours
            Next
            Return TotalGrossWorkingHours
        End Get
    End Property

    Public ReadOnly Property GrossCraneProductivity As Double Implements ICraneLogsReport.GrossCraneProductivity
        Get
            Return TotalMoves / TotalGrossWorkingHours
        End Get
    End Property

    Public ReadOnly Property NetCraneProductivity As Double Implements ICraneLogsReport.NetCraneProductivity
        Get
            Return TotalMoves / TotalNetWorkingHours
        End Get
    End Property

    Public Function CalculateInfo(strFunction As String, Inputs() As String) As Object Implements IReportswSave.CalculateInfo
        Throw New NotImplementedException()
    End Function

    Public Sub Format(ByRef crReport As ReportClass) Implements IReportswSave.Format
        Throw New NotImplementedException()
    End Sub

    Public Sub Preview(ByRef crReport As ReportClass, crViewer As CrystalReportViewer) Implements IReportswSave.Preview
        Throw New NotImplementedException()
    End Sub

    Public Sub Save() Implements IReportswSave.Save
        Dim clrRefkey As Integer
#Region "Save Crane Logs Report"
        Dim cmdInsert As New ADODB.Command
        Dim sqlInsert As String
        datenow = Date.Now 'get date    
        sqlInsert = $"
INSERT INTO [opreports].[dbo].[reports_clr]
           ([registry]
           ,[vslname]
           ,[owner_refkey]
           ,[last_port_refkey]
           ,[next_port_refkey]
           ,[ata]
           ,[atd]
           ,[first_move]
           ,[last_move]
           ,[moves]
           ,[created]
           ,[userid])
     VALUES
           ('{CLRVessel.Registry}'
           ,'{CLRVessel.Name}'
           ,{Shipline_Refkey(CLRVessel.Owner)}
           ,{Port_Refkey(Me.LastPort)}
           ,{Port_Refkey(Me.NextPort)}
           ,'{CLRVessel.ATA}'
           ,'{CLRVessel.ATD}'
           ,'{Me.FirstMove}'
           ,'{Me.LastMove}'
           ,{Me.TotalMoves}
           ,'{datenow}'
           ,'{username}'
           )

      Select Scope_Identity() as NewID
"
        cmdInsert.ActiveConnection = OPConnection
        cmdInsert.CommandText = sqlInsert
        clrRefkey = cmdInsert.Execute.Fields(0).Value 'Jumper to catch value of insert command after execution
#End Region
#Region "Save Berthing Hour Delays"

#End Region


        For Each crn As Crane In Crane

        Next
    End Sub

    Public Sub RetrieveData() Implements IReportswSave.RetrieveData

    End Sub

    Public Sub IntializeCrane(GantryName As String) Implements ICraneLogsReport.IntializeCrane
        Dim number As Integer = GantryName.Substring(GantryName.Length - 1, 1)
        Crane.Add(New Crane(GantryName, CLRVessel.Registry, N4Connection))
    End Sub

    Public Function Exists(Registry As String) As Boolean Implements IReportswSave.Exists
        Throw New NotImplementedException()
    End Function

    Private Function Shipline_Refkey(Shipline As String) As Integer
        Dim slRefkey As Integer
        Dim rsRefkey As New ADODB.Recordset
        Dim cmdRefkey As New ADODB.Command
        Dim sqlRefkey As String = $"select refkey from ref_shipline where shipline = '{Shipline}'" ' $ for string interpolation, to lessen string concatenation when building sql statements etc.
        Dim sqlInsertkey As String = $"insert into ref_shipline(shipline) values('{Shipline}') select scope_identity() as newid "
        rsRefkey.Open(sqlRefkey, OPConnection, Options:=CommandTypeEnum.adCmdText)

        Try
            slRefkey = rsRefkey.Fields("refkey").Value
        Catch 'insert shipline then return refkey
            cmdRefkey.ActiveConnection = OPConnection
            cmdRefkey.CommandText = sqlInsertkey

            slRefkey = cmdRefkey.Execute.Fields(0).Value
        End Try

        Return slRefkey
    End Function

    Private Function Port_Refkey(Port As String) As Integer
        Dim portRefkey As Integer
        Dim rsRefkey As New ADODB.Recordset
        Dim cmdRefkey As New ADODB.Command
        Dim sqlRefkey As String = $"select refkey from ref_shipline where shipline = '{Port}'" ' $ for string interpolation, to lessen string concatenation when building sql statements etc.
        Dim sqlInsertkey As String = $"insert into ref_shipline(shipline) values('{Port}') select scope_identity() as newid "
        rsRefkey.Open(sqlRefkey, OPConnection, Options:=CommandTypeEnum.adCmdText)

        Try
            portRefkey = rsRefkey.Fields("refkey").Value
        Catch 'insert port then return refkey
            cmdRefkey.ActiveConnection = OPConnection
            cmdRefkey.CommandText = sqlInsertkey

            portRefkey = cmdRefkey.Execute.Fields(0).Value
        End Try

        Return portRefkey
    End Function

    Private Function BHD_Refkey(BHD As String) As Integer

    End Function

End Class
