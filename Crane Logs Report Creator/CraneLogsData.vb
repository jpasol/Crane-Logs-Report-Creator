Partial Class CraneLogsData
    Partial Public Class BerthingHourDelaysDataTable
        Public Function Totalhours() As Double
            Dim hours As Double = 0
            For Each row In Me.Rows
                hours += row("delayhours").ToString
            Next
            Return hours
        End Function

        Friend Function GetRowIndexOfBerthDelay(text As String) As Integer
            Try
                Dim berthDelayRow As DataRow = Me.AsEnumerable.Single(Function(row) row("berthdelay").ToString = text)
                Return Me.Rows.IndexOf(berthDelayRow)
            Catch ex As Exception
                Return -1 'not found
            End Try
        End Function

    End Class

    Partial Public Class MonthlyCummulativeVolumeDataTable

    End Class

    Partial Public Class CraneProductivityDataTable
        Private Sub CraneProductivityDataTable_ColumnChanging(sender As Object, e As DataColumnChangeEventArgs) Handles Me.ColumnChanging


        End Sub

    End Class
End Class
