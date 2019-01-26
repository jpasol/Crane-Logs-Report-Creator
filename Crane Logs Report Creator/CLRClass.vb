Imports ADODB
Imports Crane_Logs_Report_Creator
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Windows.Forms
Imports Reports
Imports Reports.ReportFunctions
Public Class CLRClass
    Implements IReportswSave
    Implements ICraneLogsReport

    Public Sub New(Registry As String, ByRef N4connection As ADODB.Connection, ByRef OPConnection As ADODB.Connection, Optional Username As String = "")
        CLRVessel = New Vessel(Registry, N4connection, WithoutUnits:=True)
        Crane = New List(Of Crane)
        CraneLogsData = New CraneLogsData
        ReportFunctions = New ReportFunctions(OPConnection, N4connection) 'so you dont need to explicitly include the connection as parameter
        Me.N4Connection = N4connection
        Me.OPConnection = OPConnection
        Me.UserName = Username

        If Exists() Then
            RetrieveData()
        Else
            GenerateBerthDelays()
            GenerateCranes()
        End If
    End Sub

    Private Sub GenerateCranes()
        For craneNum As Integer = 1 To 4
            Dim craneName As String = $"GC0{craneNum}"
            Dim tempCrane As New Crane(craneName, CLRVessel.Registry, N4Connection, False)
            If tempCrane.Moves.TotalMoves > 0 Then
                tempCrane.FirstMove = GetFirstMove(tempCrane)
                tempCrane.LastMove = GetLastMove(tempCrane)
                Crane.Add(tempCrane)
            End If
        Next
    End Sub

    Private Function GetLastMove(tempCrane As Crane) As Date
        Dim tempDataTable As New DataTable
        tempDataTable.Merge(tempCrane.Moves.Inbound)
        tempDataTable.Merge(tempCrane.Moves.Outbound)

        Return CDate(tempDataTable.AsEnumerable.OrderByDescending(Function(row) CDate(row("time_move"))).Select(Function(row) row("time_move").ToString).First)

    End Function

    Private Function GetFirstMove(tempCrane As Crane) As Date
        Dim tempDataTable As New DataTable
        tempDataTable.Merge(tempCrane.Moves.Inbound)
        tempDataTable.Merge(tempCrane.Moves.Outbound)

        Return CDate(tempDataTable.AsEnumerable.OrderBy(Function(row) CDate(row("time_move"))).Select(Function(row) row("time_move").ToString).First)
    End Function

    Private Sub GenerateBerthDelays()
        CreateVesselFormalities()
        CreateGOB()
        CreatePOB()
    End Sub

    Private Sub CreatePOB()
        Dim pobStart As Date = CLRVessel.LaborOffBoard
        Dim pobEnd As Date = CLRVessel.ATD

        CraneLogsData.BerthingHourDelays.AddBerthingHourDelaysRow("POB", pobStart, pobEnd, GetSpanHours(pobStart, pobEnd))

    End Sub

    Private Sub CreateGOB()
        Dim gobStart As Date = CLRVessel.LaborOnBoard
        Dim gobEnd As Date = DateAdd(DateInterval.Minute, 5, gobStart)

        CraneLogsData.BerthingHourDelays.AddBerthingHourDelaysRow("GOB", gobStart, gobEnd, GetSpanHours(gobStart, gobEnd))
    End Sub

    Private Sub CreateVesselFormalities()
        Dim vfmStart As Date = CLRVessel.ATA
        Dim vfmEnd As Date = CLRVessel.LaborOnBoard

        CraneLogsData.BerthingHourDelays.AddBerthingHourDelaysRow("VFM", vfmStart, vfmEnd, GetSpanHours(vfmStart, vfmEnd))
    End Sub

    Private Property UserName As String
    Private Property DateNow As Date
    Private Property ReportFunctions As ReportFunctions
    Private Property Refkey As Integer
    Public ReadOnly Property N4Connection As Connection Implements IReportswSave.N4Connection
    Public ReadOnly Property OPConnection As Connection Implements IReportswSave.OPConnection
    Public ReadOnly Property CLRVessel As Vessel Implements ICraneLogsReport.Vessel
    Public ReadOnly Property Crane As List(Of Crane) Implements ICraneLogsReport.Crane
    Public ReadOnly Property CraneLogsData As CraneLogsData Implements ICraneLogsReport.CraneLogsData
    Public Property LastPort As Object Implements ICraneLogsReport.LastPort
    Public Property NextPort As Object Implements ICraneLogsReport.NextPort

    Public ReadOnly Property Registry As String Implements ICraneLogsReport.Registry
        Get
            Return CLRVessel.Registry
        End Get
    End Property

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
        DateNow = Date.Now 'get date   

        Try
            refkeyCLR = SaveCraneLogsReport()
            SaveBerthDelays(refkeyCLR)
            SaveCranes(refkeyCLR)

        Catch ex As Exception
            MsgBox("Save Unsuccessful, Rolling Back Changes" & vbNewLine &
                   "Error Message: " & ex.Message)
            Throw ex
        End Try

    End Sub

    Private Function SaveCraneLogsReport() As Integer
        Dim insertcommand As New ADODB.Command
        insertcommand.ActiveConnection = OPConnection
        insertcommand.CommandText = $"
INSERT INTO [opreports].[dbo].[reports_clr]
           ([registry]
           ,[vslname]
           ,[owner]
           ,[last_port]
           ,[next_port]
           ,[ata]
           ,[atd]
           ,[first_move]
           ,[last_move]
           ,[moves]
           ,[created]
           ,[userid]
           ,[status])
    OUTPUT INSERTED.refkey  
     VALUES
           ('{Registry}'
           ,'{CLRVessel.Name}'
           ,'{CLRVessel.Owner}'
           ,'{LastPort}'
           ,'{NextPort}'
           ,'{CLRVessel.ATA}'
           ,'{CLRVessel.ATD}'
           ,'{FirstMove}'
           ,'{LastMove}'
           ,{TotalMoves}
           ,'{DateNow}'
           ,'{UserName}'
           ,NULL)
        "

        Return insertcommand.Execute.Fields("refkey").Value

    End Function

    Private Sub SaveCranes(refkeyCLR As Integer)
        Dim insertcommand As New ADODB.Command
        insertcommand.ActiveConnection = OPConnection

        For Each crn As Crane In Crane
            Dim refkeyCrane As Integer
            'save crane then get generated refkey
            insertcommand.CommandText = $"
INSERT INTO [opreports].[dbo].[crane]
           ([che_qc]
           ,[registry]
           ,[clr_refkey]
           ,[first_move]
           ,[last_move]
           ,[moves])
    OUTPUT INSERTED.refkey
     VALUES
           ('{crn.CraneName}'
           ,'{CLRVessel.Registry}'
           ,{refkeyCLR}
           ,'{crn.FirstMove}'
           ,'{crn.LastMove}'
           ,{crn.Moves.TotalMoves}
           )
"
            refkeyCrane = insertcommand.Execute.Fields(0).Value

            'use refkey to save the crane's container, gearbox, hatchcover moves, and, delays

            SaveContainerMoves(crn, refkeyCrane)
            SaveGearboxMoves(crn, refkeyCrane)
            SaveHatchcoverMoves(crn, refkeyCrane)
            SaveDelays(crn, refkeyCrane)

        Next
    End Sub

    Private Sub SaveDelays(crn As Crane, refkeyCrane As Integer)
        SaveDeductableDelays(crn, refkeyCrane)
        SaveBreaktimeDelays(crn, refkeyCrane)
        SaveNondeductableDelays(crn, refkeyCrane)
    End Sub

    Private Sub SaveNondeductableDelays(crn As Crane, refkeyCrane As Integer)
        Dim insertcommand As New ADODB.Command
        insertcommand.ActiveConnection = OPConnection
        For Each delay As DataRow In crn.Delays.Nondeductable.Rows
            If GetSpanHours(delay("delaystart"), delay("delayend")) > 0 Then
                insertcommand.CommandText = $"
INSERT INTO [opreports].[dbo].[crane_delays]
           ([crane_refkey]
           ,[che_qc]
           ,[delay_kind]
           ,[description]
           ,[delaystart]
           ,[delayend])
     VALUES
           ({refkeyCrane}
           ,'{crn.CraneName}'
           ,'NONDE'
           ,'{delay("description").ToString}'
           ,'{delay("delaystart").ToString}'
           ,'{delay("delayend").ToString}'
           )
"
                insertcommand.Execute()
            End If

        Next
    End Sub

    Friend Sub CancelExistingCraneLogsReport()
        Try
            CancelCraneLogReport(Refkey)
            'CancelBerthDelays(Refkey)
            'CancelCrane(Refkey)
        Catch ex As Exception
            MsgBox("Cancellation Unsuccessful" & vbNewLine &
                    "Error Message: " & ex.Message)
            Throw ex
        End Try

    End Sub

    Private Sub CancelCrane(refkey As Integer)
        Dim craneRefkey As Integer = GetCraneRefkey(refkey)

        Dim cancelCLR As New ADODB.Command
        cancelCLR.ActiveConnection = OPConnection
        cancelCLR.CommandText = $"
UPDATE [opreports].[dbo].[clr_berthdelays]
   SET [status] = 'VOID'
 WHERE [clr_refkey] = {refkey}
"
        cancelCLR.Execute()

        CancelCraneContainers(craneRefkey)
        CancelCraneGearboxes(craneRefkey)
        CancelCraneHatchcovers(craneRefkey)
        CancelCraneDelays(craneRefkey)

    End Sub

    Private Sub CancelCraneDelays(craneRefkey As Integer)
        Dim cancelCraneDelays As New ADODB.Command
        cancelCraneDelays.ActiveConnection = OPConnection
        cancelCraneDelays.CommandText = $"
UPDATE [opreports].[dbo].[crane_containers]
   SET [status] = 'VOID'
 WHERE [crane_refkey] = {craneRefkey}
"
        cancelCraneDelays.Execute()
    End Sub

    Private Sub CancelCraneHatchcovers(craneRefkey As Integer)
        Dim cancelCraneHatchcovers As New ADODB.Command
        cancelCraneHatchcovers.ActiveConnection = OPConnection
        cancelCraneHatchcovers.CommandText = $"
UPDATE [opreports].[dbo].[crane_containers]
   SET [status] = 'VOID'
 WHERE [crane_refkey] = {craneRefkey}
"
        cancelCraneHatchcovers.Execute()
    End Sub

    Private Sub CancelCraneGearboxes(craneRefkey As Integer)
        Dim cancelCraneGearboxes As New ADODB.Command
        cancelCraneGearboxes.ActiveConnection = OPConnection
        cancelCraneGearboxes.CommandText = $"
UPDATE [opreports].[dbo].[crane_containers]
   SET [status] = 'VOID'
 WHERE [crane_refkey] = {craneRefkey}
"
        cancelCraneGearboxes.Execute()
    End Sub

    Private Sub CancelCraneContainers(craneRefkey As Integer)
        Dim cancelCraneContainers As New ADODB.Command
        cancelCraneContainers.ActiveConnection = OPConnection
        cancelCraneContainers.CommandText = $"
UPDATE [opreports].[dbo].[crane_containers]
   SET [status] = 'VOID'
 WHERE [crane_refkey] = {craneRefkey}
"
        cancelCraneContainers.Execute()

    End Sub

    Private Function GetCraneRefkey(refkey As Integer) As Integer
        Dim craneRefkey As New ADODB.Command
        craneRefkey.ActiveConnection = OPConnection
        craneRefkey.CommandText = $"
SELECT TOP 1 [refkey]
  FROM [opreports].[dbo].[crane]
	WHERE [clr_refkey] = {refkey}
"
        Return craneRefkey.Execute.Fields("refkey").Value
    End Function

    Private Sub CancelBerthDelays(refkey As Integer)
        Dim cancelCLR As New ADODB.Command
        cancelCLR.ActiveConnection = OPConnection
        cancelCLR.CommandText = $"
UPDATE [opreports].[dbo].[clr_berthdelays]
   SET [status] = 'VOID'
 WHERE [clr_refkey] = {refkey}
"
        cancelCLR.Execute()
    End Sub

    Private Sub CancelCraneLogReport(refkey As Integer)
        Dim cancelCLR As New ADODB.Command
        cancelCLR.ActiveConnection = OPConnection
        cancelCLR.CommandText = $"
UPDATE [opreports].[dbo].[reports_clr]
   SET [status] = 'VOID'
 WHERE [refkey] = {refkey}
"
        cancelCLR.Execute()
    End Sub

    Private Sub SaveBreaktimeDelays(crn As Crane, refkeyCrane As Integer)
        Dim insertcommand As New ADODB.Command
        insertcommand.ActiveConnection = OPConnection
        For Each delay As DataRow In crn.Delays.Break.Rows
            If GetSpanHours(delay("delaystart"), delay("delayend")) > 0 Then
                insertcommand.CommandText = $"
INSERT INTO [opreports].[dbo].[crane_delays]
           ([crane_refkey]
           ,[che_qc]
           ,[delay_kind]
           ,[description]
           ,[delaystart]
           ,[delayend])
     VALUES
           ({refkeyCrane}
           ,'{crn.CraneName}'
           ,'BREAK'
           ,'{delay("description").ToString}'
           ,'{delay("delaystart").ToString}'
           ,'{delay("delayend").ToString}'
           )
"
                insertcommand.Execute()
            End If
        Next
    End Sub

    Private Sub SaveDeductableDelays(crn As Crane, refkeyCrane As Integer)
        Dim insertcommand As New ADODB.Command
        insertcommand.ActiveConnection = OPConnection
        For Each delay As DataRow In crn.Delays.Deductable.Rows
            If GetSpanHours(delay("delaystart"), delay("delayend")) > 0 Then
                insertcommand.CommandText = $"
INSERT INTO [opreports].[dbo].[crane_delays]
           ([crane_refkey]
           ,[che_qc]
           ,[delay_kind]
           ,[description]
           ,[delaystart]
           ,[delayend])
     VALUES
           ({refkeyCrane}
           ,'{crn.CraneName}'
           ,'DEDUC'
           ,'{delay("description").ToString}'
           ,'{delay("delaystart").ToString}'
           ,'{delay("delayend").ToString}'
           )
"
                insertcommand.Execute()
            End If
        Next
    End Sub

    Private Sub SaveHatchcoverMoves(crn As Crane, refkeyCrane As Integer)
        Dim insertcommand As New ADODB.Command
        insertcommand.ActiveConnection = OPConnection
        For Each mve As DataRow In crn.Moves.Hatchcover.Rows
            If (mve("cvrsze20") + mve("cvrsze40")) > 0 Then
                insertcommand.CommandText = $"
INSERT INTO [opreports].[dbo].[crane_hatchcovers]
           ([crane_refkey]
           ,[che_qc]
           ,[actual_ib]
           ,[actual_ob]
           ,[baynum]
           ,[20]
           ,[40])
     VALUES
           ({refkeyCrane}
           ,'{crn.CraneName}'
           ,'{mve("actual_ib").ToString}'
           ,'{mve("actual_ob").ToString}'
           ,{mve("baynum").ToString}
           ,{mve("cvrsze20").ToString}
           ,{mve("cvrsze40").ToString})
"
                insertcommand.Execute()
            End If
        Next
    End Sub

    Private Sub SaveGearboxMoves(crn As Crane, refkeyCrane As Integer)
        Dim insertcommand As New ADODB.Command
        insertcommand.ActiveConnection = OPConnection
        For Each mve As DataRow In crn.Moves.Gearbox.Rows
            If (mve("gbxsze20") + mve("gbxsze40")) > 0 Then
                insertcommand.CommandText = $"
INSERT INTO [opreports].[dbo].[crane_gearboxes]
           ([crane_refkey]
           ,[che_qc]
           ,[actual_ib]
           ,[actual_ob]
           ,[baynum]
           ,[20]
           ,[40])
     VALUES
           ({refkeyCrane}
           ,'{crn.CraneName}'
           ,'{mve("actual_ib").ToString}'
           ,'{mve("actual_ob").ToString}'
           ,{mve("baynum").ToString}
           ,{mve("gbxsze20").ToString}
           ,{mve("gbxsze40").ToString})
"
                insertcommand.Execute()
            End If
        Next
    End Sub

    Private Sub SaveContainerMoves(crn As Crane, refkeyCrane As Integer)
        Dim insertcommand As New ADODB.Command
        insertcommand.ActiveConnection = OPConnection
        For Each mve As DataRow In crn.Moves.Container.Rows
            If (mve("cntsze20") + mve("cntsze40") + mve("cntsze45")) > 0 Then
                insertcommand.CommandText = $"
INSERT INTO [opreports].[dbo].[crane_containers]
           ([crane_refkey]
           ,[che_qc]
           ,[move_kind]
           ,[actual_ib]
           ,[actual_ob]
           ,[freight_kind]
           ,[20]
           ,[40]
           ,[45])
     VALUES
           ({refkeyCrane}
           ,'{crn.CraneName}'
           ,'{mve("move_kind").ToString}'
           ,'{mve("actual_ib").ToString}'
           ,'{mve("actual_ob").ToString}'
           ,'{mve("freight_kind").ToString}'
           ,{mve("cntsze20").ToString}
           ,{mve("cntsze40").ToString}
           ,{mve("cntsze45").ToString}
           )
"

                insertcommand.Execute()
            End If
        Next
    End Sub

    Private Sub SaveBerthDelays(refkeyCLR As Integer)
        Dim insertcommand As New ADODB.Command
        insertcommand.ActiveConnection = OPConnection
        For Each bhdrow As DataRow In CraneLogsData.BerthingHourDelays.Rows
            insertcommand.CommandText = $"
INSERT INTO [opreports].[dbo].[clr_berthdelays]
           ([registry]
           ,[clr_refkey]
           ,[berthdelay]
           ,[delaystart]
           ,[delayend])

     VALUES
           ('{Registry}'
           ,{refkeyCLR}       
           ,'{bhdrow("berthdelay").ToString}'
           ,'{bhdrow("delaystart").ToString}'
           ,'{bhdrow("delayend").ToString}')
"
            insertcommand.Execute()
        Next
    End Sub

    Public Sub RetrieveData() Implements IReportswSave.RetrieveData 'different implementation; used only if clr is existing
        OPConnection.Open()
        Refkey = GetCraneLogsReportRefkey()

        GetBerthDelays(Refkey)
        GetCranes(Refkey)
        OPConnection.Close()
    End Sub

    Private Function GetCraneLogsReportRefkey() As Integer
        Dim cranelogRetriever As New ADODB.Command
        cranelogRetriever.ActiveConnection = OPConnection
        'GET Refkey
        cranelogRetriever.CommandText = $"
SELECT top 1 [refkey]
FROM [opreports].[dbo].[reports_clr]
Where registry = '{CLRVessel.Registry}'
order by refkey desc
"
        Return cranelogRetriever.Execute.Fields("refkey").Value 'tostring just to be safe

    End Function
    Private Sub GetBerthDelays(Refkey As Integer)
        Dim berthdelayRetriever As New ADODB.Command
        berthdelayRetriever.ActiveConnection = OPConnection
        berthdelayRetriever.CommandText = $"SELECT [berthdelay]
      ,[delaystart]
      ,[delayend]
  FROM [opreports].[dbo].[clr_berthdelays]
	
	WHERE [clr_refkey] = {Refkey}
"
        Dim dataAdapter As New OleDb.OleDbDataAdapter
        dataAdapter.Fill(CraneLogsData.BerthingHourDelays, berthdelayRetriever.Execute) 'shortcut to fill instead of copying the returned recordset of execute

        ComputeBerthDelayHours()

    End Sub

    Private Sub ComputeBerthDelayHours()
        For Each row As DataRow In CraneLogsData.BerthingHourDelays.Rows
            Dim delaystart As DateTime = CDate(row("delaystart").ToString)
            Dim delayend As DateTime = CDate(row("delayend"))

            row("delayhours") = GetSpanHours(delaystart, delayend)
        Next
    End Sub

    Private Sub GetCranes(Refkey As Integer)
        Dim craneRetriever As New ADODB.Command With {
            .ActiveConnection = OPConnection,
            .CommandText = $"
SELECT  [refkey]
      ,[che_qc]
      ,[first_move]
      ,[last_move]
      ,[moves]
  FROM [opreports].[dbo].[crane]
	
	WHERE [clr_refkey] = {Refkey}
"
        }
        Dim cranes As New ADODB.Recordset
        cranes = craneRetriever.Execute
        With cranes
            Try
                .MoveFirst()
            Catch
            Finally
                While Not (.EOF Or .BOF)
                    Dim temporaryCrane As New Crane(.Fields("che_qc").Value, CLRVessel.Registry, N4Connection, True)
                    temporaryCrane.Moves.Container.Clear() 'removes preloaded data

                    temporaryCrane.FirstMove = .Fields("first_move").Value
                    temporaryCrane.LastMove = .Fields("last_move").Value

                    Dim craneRefkey = .Fields("refkey").Value
                    Dim informationFiller As New OleDb.OleDbDataAdapter
                    With informationFiller
                        .Fill(temporaryCrane.Moves.Container, GetContainerMoves(craneRefkey))
                        .Fill(temporaryCrane.Moves.Gearbox, GetGearboxMoves(craneRefkey))
                        .Fill(temporaryCrane.Moves.Hatchcover, GetHatchcoverMoves(craneRefkey))


                        PopulateDelays(temporaryCrane, GetCraneDelays(craneRefkey))
                    End With
                    Crane.Add(temporaryCrane)
                    .MoveNext()
                End While
            End Try
        End With
    End Sub

    Private Sub PopulateDelays(ByRef temporaryCrane As Crane, recordset As Recordset)
        With recordset
            Try
                .MoveFirst()
            Catch
            Finally
                While Not (.EOF Or .BOF)
                    Dim tableName As String = ReportFunctions.ConvertDelayKindtoTableName(.Fields("delay_kind").Value)
                    Dim description As String = .Fields("description").Value
                    Dim delayFrom As Date = .Fields("delaystart").Value
                    Dim delayTo As Date = .Fields("delayend").Value
                    Dim span As TimeSpan = delayTo.Subtract(delayFrom)

                    temporaryCrane.Delays.Tables.Item(tableName).Rows.Add({description,
                                                                          delayFrom,
                                                                          delayTo,
                                                                          span.TotalHours})
                    .MoveNext()
                End While
            End Try
        End With
    End Sub

    Private Function GetCraneDelays(craneRefkey As Object) As ADODB.Recordset
        Dim craneDelays As New ADODB.Command
        craneDelays.ActiveConnection = OPConnection
        craneDelays.CommandText = $"
SELECT [delay_kind]
      ,[description]
      ,[delaystart]
      ,[delayend]
  FROM [opreports].[dbo].[crane_delays]
    WHERE [crane_refkey] = {craneRefkey}
"
        Return craneDelays.Execute

    End Function

    Private Function GetHatchcoverMoves(craneRefkey As Object) As Object
        Dim hatchCoverMoves As New ADODB.Command
        With hatchCoverMoves
            .ActiveConnection = OPConnection
            .CommandText = $"
SELECT [actual_ib]
      ,[actual_ob]
      ,[baynum]
      ,[20]  as 'cvrsze20'
      ,[40]  as 'cvrsze40'
  FROM [opreports].[dbo].[crane_hatchcovers]
    WHERE crane_refkey = {craneRefkey}
"
            Return hatchCoverMoves.Execute
        End With
    End Function

    Private Function GetGearboxMoves(craneRefkey As Object) As ADODB.Recordset
        Dim gearboxMoves As New ADODB.Command
        With gearboxMoves
            .ActiveConnection = OPConnection
            .CommandText = $"
SELECT [actual_ib]
      ,[actual_ob]
      ,[baynum]
      ,[20]  as 'gbxsze20'
      ,[40]  as 'gbxsze40'
  FROM [opreports].[dbo].[crane_gearboxes]
    WHERE crane_refkey = {craneRefkey}
"
            Return gearboxMoves.Execute
        End With
    End Function

    Private Function GetContainerMoves(craneRefkey As Object) As ADODB.Recordset
        Dim containerMoves As New ADODB.Command
        containerMoves.ActiveConnection = OPConnection
        containerMoves.CommandText = $"
SELECT [move_kind]
      ,[actual_ib]
      ,[actual_ob]
      ,[freight_kind]
      ,[category]
      ,[20] as 'cntsze20'
      ,[40] as 'cntsze40'
      ,[45] as 'cntsze45'
  FROM [opreports].[dbo].[crane_containers]
	WHERE [crane_refkey] = {craneRefkey}
"
        Return containerMoves.Execute
    End Function

    Public Sub IntializeCrane(GantryName As String) Implements ICraneLogsReport.IntializeCrane
        Dim number As Integer = GantryName.Substring(GantryName.Length - 1, 1)
        Crane.Add(New Crane(GantryName, CLRVessel.Registry, N4Connection, False))
    End Sub

    Public Function Exists() As Boolean Implements IReportswSave.Exists ' no need register parameter since this can only be used when clr class is instantiated
        Dim craneLogsFinder As New ADODB.Command

        OPConnection.Open()
        craneLogsFinder.ActiveConnection = OPConnection
        craneLogsFinder.CommandText = $"Select refkey from reports_clr where registry = '{CLRVessel.Registry}' and (status <> 'VOID' or status IS NULL)" 'shortcut to registry since they will only point to the same thing  

        Dim craneLogs As New ADODB.Recordset
        craneLogs = craneLogsFinder.Execute()

        With craneLogs
            Dim result As Boolean = Not (.BOF And .EOF)
            OPConnection.Close()
            Return result
        End With
    End Function

End Class
