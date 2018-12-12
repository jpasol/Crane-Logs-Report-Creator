Imports Reports

Public Interface ICraneLogsReport
    ReadOnly Property Vessel As Vessel
    ReadOnly Property Crane() As List(Of Crane)
    ReadOnly Property CraneLogsData As CraneLogsData
    ReadOnly Property TotalMoves As Double
    ReadOnly Property FirstMove As Date
    ReadOnly Property LastMove As Date
    ReadOnly Property CraneDensity As Double
    ReadOnly Property TotalBerthHours As Double
    ReadOnly Property NetBerthHours As Double
    ReadOnly Property GrossBerthProdRate As Double
    ReadOnly Property NetBerthProdRate As Double
    ReadOnly Property GrossVesselWorkingTime As Double
    ReadOnly Property NetVesselWorkingTime As Double
    ReadOnly Property GrossVesselProdRate As Double
    ReadOnly Property NetVesselProdRate As Double
    ReadOnly Property TotalGrossWorkingHours As Double
    ReadOnly Property TotalNetWorkingHours As Double
    ReadOnly Property GrossCraneProductivity As Double
    ReadOnly Property NetCraneProductivity As Double

    Property LastPort
    Property NextPort

    Sub IntializeCrane(GantryName As String)

End Interface
