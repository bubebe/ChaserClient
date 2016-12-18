Public Class mapCell
    Private _visited As Boolean
    Private _status As String

    Public Sub New(status As String, Optional visited As Boolean = False)
        _visited = visited
        _status = status
    End Sub
    Public Property visited As Boolean
        Get
            Return _visited
        End Get
        Set(value As Boolean)
            _visited = value
        End Set
    End Property
    Public Property status As String
        Get
            Return _status
        End Get
        Set(value As String)
            _status = value
        End Set
    End Property


End Class