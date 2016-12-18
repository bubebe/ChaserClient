''' <summary>
''' 自機情報
''' </summary>
Public Class Chaser
    Private _x As Integer
    Private _y As Integer
    Private _lochis = Enumerable.Empty(Of String)()
    Private _acthis = Enumerable.Empty(Of String)()


    Public Sub New()
        _x = 0
        _y = 0
    End Sub

    ''' <summary>
    ''' 自機周辺情報を返す
    ''' </summary>
    ''' <param name="map"></param>
    ''' <param name="cx"></param>
    ''' <param name="cy"></param>
    ''' <returns></returns>
    Public Function around(map As Map, Optional cx As Integer = 0, Optional cy As Integer = 0) As mapCell()
        Return map.getMemory({_x + cx, _y + cy})
    End Function

    ''' <summary>
    ''' 自機SEARCH範囲情報を返す
    ''' </summary>
    ''' <param name="map"></param>
    ''' <param name="allow"></param>
    ''' <param name="cx"></param>
    ''' <param name="cy"></param>
    ''' <returns></returns>
    Public Function search(map As Map, allow As String, Optional cx As Integer = 0, Optional cy As Integer = 0) As mapCell()
        Return map.getMemory(allow, {_x + cx, _y + cy})
    End Function

    ''' <summary>
    ''' 現在位置
    ''' </summary>
    ''' <returns></returns>
    Public Property position As Integer()
        Set(value As Integer())
            _x = value(0)
            _y = value(1)
        End Set
        Get
            Return {_x, _y}
        End Get
    End Property
    Public Property x As Integer
        Set(value As Integer)
            _x = value
        End Set
        Get
            Return _x
        End Get
    End Property
    Public Property y As Integer
        Set(value As Integer)
            _y = value
        End Set
        Get
            Return _y
        End Get
    End Property
    ''' <summary>
    ''' 周辺情報履歴
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property locationHistory As String()
        Get
            Return _lochis
        End Get
    End Property
    Public ReadOnly Property locationHistory(i As Integer) As String
        Get
            If _lochis.Length > i And i >= 0 Then
                Return _lochis(i)
            Else
                Return -1
            End If
        End Get
    End Property
    Public Function locLength() As Integer
        Return _lochis.Length
    End Function
    Public Sub locationAdd(value As String)
        ReDim Preserve _lochis(_lochis.Length)
        _lochis(_lochis.Length - 1) = value
    End Sub
    ''' <summary>
    ''' 行動履歴
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property actHistory As String()
        Get
            Return _acthis
        End Get
    End Property
    Public ReadOnly Property actHistory(i As Integer) As String
        Get
            If _acthis.Length > i Then
                Return _acthis(i)
            Else
                Return -1
            End If
        End Get
    End Property
    Public ReadOnly Property lastAct() As String
        Get
            If _acthis.length > 0 Then
                Return _acthis(_acthis.length - 1)
            Else
                Return -1
            End If
        End Get
    End Property
    Public Sub actAdd(value As String)
        ReDim Preserve _acthis(_acthis.Length)
        _acthis(_acthis.Length - 1) = value
    End Sub

End Class

