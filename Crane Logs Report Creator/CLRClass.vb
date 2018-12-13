Imports ADODB
Imports Crane_Logs_Report_Creator
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Windows.Forms
Imports Reports
Imports Reports.ReportFunctions
Public Class CLRClass
    Implements IReportswSave
    Implements ICraneLogsReport

    Public Sub New(Registry As String, ByRef N4connection As ADODB.Connection, ByRef OPConnection As ADODB.Connection, Username As String)
        CLRVessel = New Vessel(Registry, N4connection)
        Crane = New List(Of Crane)
        CraneLogsData = New CraneLogsData
        ReportFunctions = New ReportFunctions(OPConnection, N4connection) 'so you dont need to explicitly include the connection as parameter
        Me.N4Connection = N4connection
        Me.OPConnection = OPConnection
        Me.UserName = Username

    End Sub

    Private Property UserName As String
    Private Property DateNow As Date
    Private Property ReportFunctions As ReportFunctions
    Public ReadOnly Property N4Connection As Connection Implements IReportswSave.N4Connection
    Public ReadOnly Property OPConnection As Connection Implements IReportswSave.OPConnection
    Public ReadOnly Property CLRVessel As Vessel Implements ICraneLogsReport.Vessel
    Public ReadOnly Property Crane As List(Of Crane) Implements ICraneLogsReport.Crane
    Public ReadOnly Property CraneLogsData As CraneLogsData Implements ICraneLogsReport.CraneLogsData
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
                Return GetSpanHours(.ATA, .ATD)
            End With
        End Get
    End Property

    Public ReadOnly Property NetBerthHours As Double Implements ICraneLogsReport.NetBerthHours
        Get
            Return TotalBerthHours - CraneLogsData.BerthingHourDelays.Totalhours
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
            Return GetSpanHours(FirstMove, LastMove)
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
        Dim refkeyCLR As Integer
        Dim refkeyCrane As Integer
        Dim insertcommand As New ADODB.Command
        insertcommand.ActiveConnection = OPConnection
        DateNow = Date.Now 'get date   

#Region "Save Crane Logs Report"
        insertcommand.CommandText = $"
INSERT INTO [opreports].[dbo].[reports_clr]
           ([registry]
           ,[registry_refley]
           ,[vslname]
           ,[owner]
           ,[owner_refkey]
           ,[last_port]
           ,[last_port_refkey]
           ,[next_port]
           ,[next_port_refkey]
           ,[ata]
           ,[atd]
           ,[first_move]
           ,[last_move]
           ,[moves]
           ,[created]
           ,[userid])
     VALUES
           ('{ReportFunctions.GetRefkey(KeyType.Registry, CLRVessel.Registry)}'
           ,'{CLRVessel.Registry}'
           ,'{CLRVessel.Name}'
           ,'{CLRVessel.Owner}'
           ,{ReportFunctions.GetRefkey(KeyType.Shipline, CLRVessel.Owner)}
           ,'{Me.LastPort}'
           ,{ReportFunctions.GetRefkey(KeyType.Port, Me.LastPort)}
           ,'{Me.NextPort}'
           ,{ReportFunctions.GetRefkey(KeyType.Port, Me.NextPort)}
           ,'{CLRVessel.ATA}'
           ,'{CLRVessel.ATD}'
           ,'{Me.FirstMove}'
           ,'{Me.LastMove}'
           ,{Me.TotalMoves}
           ,'{DateNow}'
           ,'{UserName}'
           )

      Select Scope_Identity() as NewID
"
        refkeyCLR = insertcommand.Execute.Fields(0).Value 'Jumper to catch value of insert command after execution
#End Region

#Region "Save Berthing Hour Delays"
        For Each bhdrow As DataRow In CraneLogsData.BerthingHourDelays.Rows
            insertcommand.CommandText = $"
INSERT INTO [opreports].[dbo].[clr_berthdelays]
           ([clr_refkey]
           ,[registry]
           ,[berthdelay]
           ,[berthdelay_refkey]
           ,[delaystart]
           ,[delayend])
     VALUES
           ({refkeyCLR}
           ,{CLRVessel.Registry}
           ,'{bhdrow("berthdelay").ToString}'
           ,{ReportFunctions.GetRefkey(KeyType.BerthDelay, bhdrow("berthdelay").ToString)}
           ,'{bhdrow("delaystart").ToString}'
           ,'{bhdrow("delayend").ToString}')
"
            insertcommand.Execute()
        Next
#End Region

        For Each crn As Crane In Crane
            'save crane then get generated refkey
            insertcommand.CommandText = $"
INSERT INTO [opreports].[dbo].[crane]
           ([qc_shortname]
           ,[qc_refkey]
           ,[registry]
           ,[clr_refkey]
           ,[first_move]
           ,[lastmove]
           ,[moves])
     VALUES
           ('{crn.CraneName}'
           ,{ReportFunctions.GetRefkey(KeyType.QuayCrane, crn.CraneName)}
           ,'{CLRVessel.Registry}'
           ,{refkeyCLR}
           ,'{crn.FirstMove}'
           ,'{crn.LastMove}'
           ,{crn.Moves.TotalMoves}

    Select Scope_Identity() as NewID
"
            refkeyCrane = insertcommand.Execute.Fields(0).Value

            'use refkey to save the crane's container, gearbox, hatchcover moves, and, delays
            'save crane container moves

            For Each mve As DataRow In crn.Moves.Container.Rows
                insertcommand.CommandText = $"
INSERT INTO [opreports].[dbo].[crane_containers]
           ([crane_refkey]
           ,[qc_shortname]
           ,[move_kind]
           ,[move_kind_refkey]
           ,[ctrtyp]
           ,[ctrtyp_refkey]
           ,[freigh]
           ,[freight_refkey]
           ,[20]
           ,[40]
           ,[45])
     VALUES
           ({refkeyCrane}
           ,'{crn.CraneName}'
           ,'{mve("move_kind").ToString}'
           ,{ReportFunctions.GetRefkey(KeyType.Move_kind, mve("move_kind").ToString)}
           ,'{mve("ctrtyp").ToString}'
           ,{ReportFunctions.GetRefkey(KeyType.Move_kind, mve("ctrtyp").ToString)}
           ,'{mve("freight").ToString}'
           ,{ReportFunctions.GetRefkey(KeyType.Move_kind, mve("freight").ToString)}
           ,{mve("cntsze20").ToString}
           ,{mve("cntsze40").ToString}
           ,{mve("cntsze45").ToString}
"
                insertcommand.Execute()
            Next

            'save gearbox moves
            For Each mve As DataRow In crn.Moves.Gearbox.Rows
                insertcommand.CommandText = $"
INSERT INTO [opreports].[dbo].[crane_gearboxes]
           ([qc_refkey]
           ,[move_kind]
           ,[baynum]
           ,[20]
           ,[40])
     VALUES
           ({refkeyCrane}
           ,{ReportFunctions.GetRefkey(KeyType.Move_kind, mve("move_kind").ToString)}
           ,{mve("baynum").ToString}
           ,{mve("cntsze20").ToString}
           ,{mve("cntsze40").ToString}
"
                insertcommand.Execute()
            Next

            'save hatchcover moves
            For Each mve As DataRow In crn.Moves.Hatchcover.Rows
                insertcommand.CommandText = $"
INSERT INTO [opreports].[dbo].[crane_hatchcovers]
           ([qc_refkey]
           ,[move_kind]
           ,[baynum]
           ,[20]
           ,[40])
     VALUES
           ({refkeyCrane}
           ,{ReportFunctions.GetRefkey(KeyType.Move_kind, mve("move_kind").ToString)}
           ,{mve("baynum").ToString}
           ,{mve("cntsze20").ToString}
           ,{mve("cntsze40").ToString}
"
                insertcommand.Execute()
            Next

            'save delays
            'deductable
            For Each mve As DataRow In crn.Delays.Deductable.Rows
                insertcommand.CommandText = $"
INSERT INTO [opreports].[dbo].[crane_delays]
           ([qc_refkey]
           ,[delay_kind]
           ,[description]
           ,[delaystart]
           ,[delayend])
     VALUES
           ({refkeyCrane}
           ,{ReportFunctions.GetRefkey(KeyType.Delaykind, "Deductable")}
           ,'{mve("description").ToString}'
           ,'{mve("delaystart").ToString}'
           ,'{mve("delayend").ToString}'
"
                insertcommand.Execute()
            Next

            'break
            For Each mve As DataRow In crn.Delays.Break.Rows
                insertcommand.CommandText = $"
INSERT INTO [opreports].[dbo].[crane_delays]
           ([qc_refkey]
           ,[delay_kind]
           ,[description]
           ,[delaystart]
           ,[delayend])
     VALUES
           ({refkeyCrane}
           ,{ReportFunctions.GetRefkey(KeyType.Delaykind, "Breaktime")}
           ,'{mve("description").ToString}'
           ,'{mve("delaystart").ToString}'
           ,'{mve("delayend").ToString}'
"
                insertcommand.Execute()
            Next

            'nondeductable
            For Each mve As DataRow In crn.Delays.Nondeductable.Rows
                insertcommand.CommandText = $"
INSERT INTO [opreports].[dbo].[crane_delays]
           ([qc_refkey]
           ,[delay_kind]
           ,[description]
           ,[delaystart]
           ,[delayend])
     VALUES
           ({refkeyCrane}
           ,{ReportFunctions.GetRefkey(KeyType.Delaykind, "Nondeductable")}
           ,'{mve("description").ToString}'
           ,'{mve("delaystart").ToString}'
           ,'{mve("delayend").ToString}'
"
                insertcommand.Execute()
            Next
        Next
    End Sub

    Public Sub RetrieveData() Implements IReportswSave.RetrieveData 'different implementation; used only if clr is existing
        Dim cranelogRetriever As New ADODB.Command
        cranelogRetriever.ActiveConnection = OPConnection

        'GET Refkey
        cranelogRetriever.CommandText = $"
SELECT [refkey]
FROM [opreports].[dbo].[ref_registry]
Where registry = {CLRVessel.Registry}"
        Dim cranelogRefkey As Integer = cranelogRetriever.Execute.Fields("refkey").Value.ToString 'tostring just to be safe

        'get bhd using cranelogRefkey
        cranelogRetriever.CommandText = $"
"
    End Sub

    Public Sub IntializeCrane(GantryName As String) Implements ICraneLogsReport.IntializeCrane
        Dim number As Integer = GantryName.Substring(GantryName.Length - 1, 1)
        Crane.Add(New Crane(GantryName, CLRVessel.Registry, N4Connection))
    End Sub

    Public Function Exists() As Boolean Implements IReportswSave.Exists ' no need register parameter since this can only be used when clr class is instantiated
        Dim craneLogsFinder As New ADODB.Command
        craneLogsFinder.ActiveConnection = OPConnection
        craneLogsFinder.CommandText = $"Select refkey from reports_clr where registry = '{CLRVessel.Registry}'" 'shortcut to registry since they will only point to the same thing  

        Dim craneLogs As New ADODB.Recordset
        craneLogs = craneLogsFinder.Execute()

        With craneLogs
            Return Not (.BOF And .EOF)
        End With

    End Function

End Class
