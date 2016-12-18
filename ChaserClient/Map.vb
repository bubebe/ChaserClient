''' <summary>
''' マップ
''' </summary>
Public Class Map
    Inherits PictureBox

    Private Const IMG_MINE As String = "images\people.png"
    Private Const IMG_ITEM As String = "images\item.png"
    Private Const IMG_WALL As String = "images\wall.png"
    Private Const IMG_VISITED As String = "images\empty.png"
    Private Const IMG_ENEMY As String = "images\enemy.png"
    Private Const IMG_UNVISIT As String = "images\gray.png"
    Private Const PIC_W As Integer = 16
    Private Const PIC_H As Integer = 16

    Private _mapCenter = Enumerable.Empty(Of Integer)()
    Private _curLoc = Enumerable.Empty(Of mapCell)()
    Private _canvas As Bitmap
    Private _imgbuff As Image

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="pt">親オブジェクト</param>
    ''' <param name="scale">記憶サイズ</param>
    Public Sub New(ByRef pt As Object, Optional scale As Integer = 500)
        MyBase.New()
        MyBase.Size = pt.size
        _canvas = New Bitmap(MyBase.Width, MyBase.Height)
        If Not scale Mod 2 Then scale += 1
        _mapCenter = {scale \ 2 + 1, scale \ 2 + 1}
        ReDim _curLoc(scale, scale)
    End Sub
    ''' <summary>
    ''' マップを更新
    ''' </summary>
    Public Sub mapRefresh(ByRef oChaser As Chaser)
        '自機周辺を描写
        addrDraw(oChaser)
        '自機を描写
        _imgbuff = Image.FromFile(IMG_MINE)
        picDraw(_imgbuff, oChaser.x, oChaser.y)
        'Imageオブジェクトのリソースを解放する
        _imgbuff.Dispose()
        Application.DoEvents()
    End Sub

    ''' <summary>
    ''' 記憶座標の描写
    ''' </summary>
    ''' <param name="cx">x軸補正</param>
    ''' <param name="cy">y軸補正</param>
    Private Sub addrDraw(ByRef oChaser As Chaser, Optional cx As Integer = 0, Optional cy As Integer = 0)

        Dim addr As mapCell() = Nothing

        Select Case True
            Case oChaser.lastAct = "gr" _
                    OrElse oChaser.lastAct Like Client.PUT & "*" _
                    OrElse oChaser.lastAct Like Client.WALK & "*"
                '自機周辺
                addr = oChaser.around(Me)
            Case oChaser.lastAct Like Client.LOOK & "*"
                '指定方向に中心が2ずれる
                Select Case True
                    Case oChaser.lastAct Like "*" & Client.U
                        cy -= 2
                    Case oChaser.lastAct Like "*" & Client.R
                        cx += 2
                    Case oChaser.lastAct Like "*" & Client.D
                        cy += 2
                    Case oChaser.lastAct Like "*" & Client.L
                        cx -= 2
                End Select
                addr = oChaser.around(Me, cx, cy)
            Case oChaser.lastAct Like Client.SEARCH & "*"
                addr = oChaser.search(Me, (MainWindow.cutRight(oChaser.lastAct, 1)))
        End Select

        If addr Is Nothing Then Exit Sub

        For i As Integer = 1 To addr.Length - 1
            Select Case addr(i).status
                Case "0"
                    If addr(i).visited = True Then
                        _imgbuff = Image.FromFile(IMG_VISITED)
                    Else
                        _imgbuff = Image.FromFile(IMG_UNVISIT)
                    End If

                Case "1"
                    _imgbuff = Image.FromFile(IMG_ENEMY)
                Case "2"
                    _imgbuff = Image.FromFile(IMG_WALL)
                Case "3"
                    _imgbuff = Image.FromFile(IMG_ITEM)
                Case Else
                    Continue For
            End Select

            If oChaser.lastAct Like Client.SEARCH & "*" Then
                'Console.WriteLine("search=" & oChaser.lastAct)
                Select Case True
                    Case oChaser.lastAct Like "*" & Client.U
                        cy -= 1
                    Case oChaser.lastAct Like "*" & Client.R
                        cx += 1
                    Case oChaser.lastAct Like "*" & Client.D
                        cy += 1
                    Case oChaser.lastAct Like "*" & Client.L
                        cx -= 1
                    Case Else
                        'Console.WriteLine("undefinedallow")
                End Select
                'Console.WriteLine("cy=" & cy)
                picDraw(_imgbuff, cx, cy)

            Else
                Dim xy As Integer() = {0, 0}
                Select Case i
                    Case 1
                        xy = {cx - 1, cy - 1}
                    Case 2
                        xy = {cx, cy - 1}
                    Case 3
                        xy = {cx + 1, cy - 1}
                    Case 4
                        xy = {cx - 1, cy}
                    Case 5
                        xy = {cx, cy}
                    Case 6
                        xy = {cx + 1, cy}
                    Case 7
                        xy = {cx - 1, cy + 1}
                    Case 8
                        xy = {cx, cy + 1}
                    Case 9
                        xy = {cx + 1, cy + 1}
                End Select
                picDraw(_imgbuff, xy(0) + oChaser.x, xy(1) + oChaser.y)
            End If

        Next
        Me.Refresh()
    End Sub

    Private Sub picDraw(ByRef img As Image, x As Integer, y As Integer)
        Dim g As Graphics = Graphics.FromImage(_canvas)
        x = x * PIC_W + _canvas.Width / 2 - PIC_W / 2
        y = y * PIC_H + _canvas.Height / 2 - PIC_H / 2
        g.DrawImage(img, x, y, PIC_W, PIC_H)
        'Graphicsオブジェクトのリソースを解放する
        g.Dispose()
        '書き込む
        Me.Image = _canvas
    End Sub
    ''' <summary>
    ''' 指定座標の周辺MAP情報を返す
    ''' </summary>
    ''' <returns></returns>
    Public Function getMemory(center As Integer()) As mapCell()

        Dim cx As Integer = center(0) + _mapCenter(0)
        Dim cy As Integer = center(1) + _mapCenter(1)
        Dim xy() As Integer = {0, 0}
        Dim res As mapCell() = Enumerable.Empty(Of mapCell)()
        ReDim res(9)
        For i As Integer = 1 To 9
            Select Case i
                Case 1
                    xy = {cx - 1, cy - 1}
                Case 2
                    xy = {cx, cy - 1}
                Case 3
                    xy = {cx + 1, cy - 1}
                Case 4
                    xy = {cx - 1, cy}
                Case 5
                    xy = {cx, cy}
                Case 6
                    xy = {cx + 1, cy}
                Case 7
                    xy = {cx - 1, cy + 1}
                Case 8
                    xy = {cx, cy + 1}
                Case 9
                    xy = {cx + 1, cy + 1}
                Case Else
            End Select
            res(i) = IIf(_curLoc(xy(0), xy(1)) Is Nothing, New mapCell(""), _curLoc(xy(0), xy(1)))
        Next
        Return res
    End Function
    ''' <summary>
    ''' SEARCH方向のMAP記憶を返す
    ''' </summary>
    ''' <returns></returns>
    Public Function getMemory(allow As String, center As Integer()) As mapCell()
        Dim cx As Integer = center(0) + _mapCenter(0)
        Dim cy As Integer = center(1) + _mapCenter(1)
        'Debug.Print("getMemS=" & _x & "," & _y & "=" & _curLoc(cx, cy).visited)
        Dim xy() As Integer = {0, 0}
        Dim res As mapCell() = Enumerable.Empty(Of mapCell)()
        ReDim res(9)
        For i As Integer = 1 To 9
            Select Case allow
                Case Client.U
                    xy(1) -= 1
                Case Client.R
                    xy(0) += 1
                Case Client.D
                    xy(1) += 1
                Case Client.L
                    xy(0) -= 1
                Case Else
            End Select
            res(i) = _curLoc(xy(0) + cx, xy(1) + cy)
        Next
        Return res
    End Function
    ''' <summary>
    ''' MAP情報の記憶
    ''' </summary>
    Public Sub setMemory(str As String, ByRef oChaser As Chaser)

        Dim data() As Char = str.ToCharArray
        Dim lastAct As String = oChaser.lastAct
        Dim cx As Integer = oChaser.x + _mapCenter(0)
        Dim cy As Integer = oChaser.y + _mapCenter(1)

        Select Case True
            Case lastAct = "gr" _
                        OrElse lastAct Like Client.PUT & "*" _
                        OrElse lastAct Like Client.WALK & "*"
                '中心そのまま

                '中心は踏破セルに変更
                If _curLoc(cx, cy) Is Nothing Then
                    _curLoc(cx, cy) = New mapCell(data(5), True)
                Else
                    _curLoc(cx, cy).visited = True
                End If
                'Debug.Print(_x & "," & _y & "=" & _curLoc(cx, cy).visited)


            Case lastAct Like Client.LOOK & "*"
                '指定方向に中心が2ずれる

                Select Case True
                    Case lastAct Like "*" & Client.U
                        cy -= 2
                    Case lastAct Like "*" & Client.R
                        cx += 2
                    Case lastAct Like "*" & Client.D
                        cy += 2
                    Case lastAct Like "*" & Client.L
                        cx -= 2
                End Select
        End Select

        For i As Integer = 1 To data.Length - 1
            Dim xy() As Integer = {0, 0}
            If lastAct Like Client.SEARCH & "*" Then
                Select Case True
                    Case lastAct Like "*" & Client.U
                        cy -= 1
                    Case lastAct Like "*" & Client.R
                        cx += 1
                    Case lastAct Like "*" & Client.D
                        cy += 1
                    Case lastAct Like "*" & Client.L
                        cx -= 1
                    Case Else
                End Select
                xy = {cx, cy}
            Else
                Select Case i
                    Case 1
                        xy = {cx - 1, cy - 1}
                    Case 2
                        xy = {cx, cy - 1}
                    Case 3
                        xy = {cx + 1, cy - 1}
                    Case 4
                        xy = {cx - 1, cy}
                    Case 5
                        xy = {cx, cy}
                    Case 6
                        xy = {cx + 1, cy}
                    Case 7
                        xy = {cx - 1, cy + 1}
                    Case 8
                        xy = {cx, cy + 1}
                    Case 9
                        xy = {cx + 1, cy + 1}
                    Case Else
                End Select
            End If
            If _curLoc(xy(0), xy(1)) Is Nothing Then
                _curLoc(xy(0), xy(1)) = New mapCell(data(i))
            Else
                _curLoc(xy(0), xy(1)).status = data(i)
            End If
        Next

        mapRefresh(oChaser)
    End Sub
End Class
