Imports ADODB
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Windows.Forms
Imports Reports
Public Class CLRClass
    Implements IReportswSave

    Public Sub New(Registry As String, ByRef N4connection As ADODB.Connection, ByRef OPConnection As ADODB.Connection)
        CLRVessel = New Vessel(Registry, N4connection)
        RetrieveData()
    End Sub
    Private strRegistry As String
    Private clrDetails(System.Enum.GetNames(GetType(clrInfo)).Count) As String
    Public ReadOnly Property N4Connection As Connection Implements IReportswSave.N4Connection

    Public ReadOnly Property OPConnection As Connection Implements IReportswSave.OPConnection
        Get
            Throw New NotImplementedException()
        End Get
    End Property
    Public ReadOnly Property CLRVessel As Vessel
    Public Property Details(index) As String
        Get
            Return clrDetails(index)
        End Get
        Set(value As String)
            clrDetails(index) = value
        End Set
    End Property
    Public Enum clrInfo
        shiplines
        name
        registry
        lstport
        nxtport
        voynum
        ata
        atd
        berthhours
        frstmove
        lstmove
        gvmt
        netberth
        grsberthrate
        netberthrate
        networktime
        grsprodrate
        netprodrate
        moves
        cranedensity
        grsworkhrs
        networkhrs
        grscraneprod
        netcraneprod
    End Enum


    Public Function CalculateInfo(strFunction As String, Inputs() As String) As Object Implements IReportswSave.CalculateInfo
        Select Case strFunction
            Case "Hours between two Dates"
                Dim ata As Date = getDateTime(Inputs(0))
                Dim atd As Date = getDateTime(Inputs(1))
                Dim span As TimeSpan = atd.Subtract(ata)

                Return span.TotalHours

        End Select
    End Function

    Public Sub Format(ByRef crReport As ReportClass) Implements IReportswSave.Format
        Throw New NotImplementedException()
    End Sub

    Public Sub Preview(ByRef crReport As ReportClass, crViewer As CrystalReportViewer) Implements IReportswSave.Preview
        Throw New NotImplementedException()
    End Sub

    Public Sub Save() Implements IReportswSave.Save
        Throw New NotImplementedException()
    End Sub

    Public Sub RetrieveData() Implements IReportswSave.RetrieveData
        With CLRVessel
            clrDetails(clrInfo.shiplines) = .Owner
            clrDetails(clrInfo.name) = .Name
            clrDetails(clrInfo.registry) = .Registry
            clrDetails(clrInfo.voynum) = .InboundVoyage & " - " & .OutboundVoyage
            clrDetails(clrInfo.ata) = getMilTime(.ATA)
            clrDetails(clrInfo.atd) = getMilTime(.ATD)
        End With
    End Sub
    Public Function getMilTime(strLDate As String) As String
        Dim dteDate As DateTime

        dteDate = Convert.ToDateTime(strLDate)
        getMilTime = dteDate.ToString("HHmm\H MM/dd/yyyy")
    End Function

    Public Function getDateTime(strMDate As String) As Date
        Return Date.ParseExact(strMDate, "HHmm\H MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture)
    End Function
End Class
