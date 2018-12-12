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
        Me.Crane = New List(Of Crane)
        Me.CraneLogsData = New CraneLogsData
        Me.N4Connection = N4connection
        Me.OPConnection = OPConnection
        Me.username = Username

    End Sub
    Enum KeyType
        Port
        Shipline
        BerthDelay
        QuayCrane
        Move_kind
        Ctrtyp
        Freight
        Delaykind
    End Enum

    Private username As String
    Private datenow As Date
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
                Return getSpanHours(.ATA, .ATD)
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
        Dim refkeyCLR As Integer
        Dim refkeyCrane As Integer
        Dim insertcommand As New ADODB.Command
        insertcommand.ActiveConnection = OPConnection
        datenow = Date.Now 'get date   

#Region "Save Crane Logs Report"
        insertcommand.CommandText = $"
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
           ,{GetRefkey(KeyType.Shipline, CLRVessel.Owner)}
           ,{GetRefkey(KeyType.Port, Me.LastPort)}
           ,{GetRefkey(KeyType.Port, Me.NextPort)}
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
        refkeyCLR = insertcommand.Execute.Fields(0).Value 'Jumper to catch value of insert command after execution
#End Region

#Region "Save Berthing Hour Delays"
        For Each bhdrow As DataRow In CraneLogsData.BerthingHourDelays.Rows
            insertcommand.CommandText = $"
INSERT INTO [opreports].[dbo].[clr_berthdelays]
           ([clr_refkey]
           ,[berthdelay_refkey]
           ,[delaystart]
           ,[delayend])
     VALUES
           ({refkeyCLR}
           ,{GetRefkey(KeyType.BerthDelay, bhdrow("berthdelay").ToString)}
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
           ([qc_refkey]
           ,[clr_refkey]
           ,[first_move]
           ,[lastmove]
           ,[moves])
     VALUES
           ({GetRefkey(KeyType.QuayCrane, crn.CraneName)}
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
           ([qc_refkey]
           ,[move_kind]
           ,[ctrtyp]
           ,[freight]
           ,[20]
           ,[40]
           ,[45])
     VALUES
           ({refkeyCrane}
           ,{GetRefkey(KeyType.Move_kind, mve("move_kind").ToString)}
           ,{GetRefkey(KeyType.Move_kind, mve("ctrtyp").ToString)}
           ,{GetRefkey(KeyType.Move_kind, mve("freight").ToString)}
           ,{mve("cntsze20").ToString}
           ,{mve("cntsze40").ToString}
           ,{mve("cntsze45").ToString}
"
                insertcommand.Execute()
            Next

            'save gearbox moves
            For Each mve As DataRow In crn.Moves.Container.Rows
                insertcommand.CommandText = $"
INSERT INTO [opreports].[dbo].[crane_gearboxes]
           ([qc_refkey]
           ,[move_kind]
           ,[baynum]
           ,[20]
           ,[40])
     VALUES
           ({refkeyCrane}
           ,{GetRefkey(KeyType.Move_kind, mve("move_kind").ToString)}
           ,{mve("baynum").ToString}
           ,{mve("cntsze20").ToString}
           ,{mve("cntsze40").ToString}
"
                insertcommand.Execute()
            Next

            'save hatchcover moves
            For Each mve As DataRow In crn.Moves.Container.Rows
                insertcommand.CommandText = $"
INSERT INTO [opreports].[dbo].[crane_hatchcovers]
           ([qc_refkey]
           ,[move_kind]
           ,[baynum]
           ,[20]
           ,[40])
     VALUES
           ({refkeyCrane}
           ,{GetRefkey(KeyType.Move_kind, mve("move_kind").ToString)}
           ,{mve("baynum").ToString}
           ,{mve("cntsze20").ToString}
           ,{mve("cntsze40").ToString}
"
                insertcommand.Execute()
            Next

            'save delays
            For Each mve As DataRow In crn.Moves.Container.Rows
                insertcommand.CommandText = $"
INSERT INTO [opreports].[dbo].[crane_delays]
           ([qc_refkey]
           ,[delay_kind]
           ,[description]
           ,[delaystart]
           ,[delayend])
     VALUES
           ({refkeyCrane}
           ,{GetRefkey(KeyType.Delaykind, mve("delaykind"))}
           ,'{mve("description").ToString}'
           ,'{mve("delaystart").ToString}'
           ,'{mve("delayend").ToString}'
"
                insertcommand.Execute()
            Next
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

    Private Function GetRefkey(keyName As KeyType, keyValue As String) As Integer
        Dim refkeyCommand As New ADODB.Command
        Dim insertRefkey As String
        Dim selectRefkey As String
        Dim refkey As Integer
        Dim database As String
        Dim field As String

        Select Case keyName
            Case KeyType.Port
                database = "ref_ports"
                field = "port"
            Case KeyType.Shipline
                database = "ref_shiplines"
                field = "shipline"
            Case KeyType.BerthDelay
                database = "ref_berthdelays"
                field = "berthdelay"
            Case KeyType.QuayCrane
                database = "ref_quaycranes"
                field = "qc_shortname"
            Case 
        End Select

        refkeyCommand.ActiveConnection = OPConnection

        insertRefkey = $"insert into {database}({field}) values('{keyValue}') select scope_identity() as newid "
        selectRefkey = $"select refkey from {database} where {field} = '{keyValue}'"
        ' $ for string interpolation, to lessen string concatenation when building sql statements etc.
        Try
            refkeyCommand.CommandText = selectRefkey
            refkey = refkeyCommand.Execute.Fields("refkey").Value.ToString
        Catch 'insert shipline then return refkey
            refkeyCommand.CommandText = insertRefkey
            refkey = refkeyCommand.Execute.Fields("newid").Value
        End Try

        Return refkey
    End Function

End Class
