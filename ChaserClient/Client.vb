Imports System.Net.Sockets

Public Class Client

    Public Const WALK As String = "w"
    Public Const LOOK As String = "l"
    Public Const SEARCH As String = "s"
    Public Const PUT As String = "p"

    Public Const U As String = "u"
    Public Const R As String = "r"
    Public Const L As String = "l"
    Public Const D As String = "d"

    Private Property _tcp As TcpClient
    Private Property _ns As NetworkStream
    Private Property _enc As System.Text.Encoding = System.Text.Encoding.UTF8
    Private Property _sendBytes As Byte() = Nothing

    Private Property _teamname As String
    Private Property _hostip As String
    Private Property _hostport As Integer
    Private Property _playing As Boolean
    Private Property _start As Boolean

    Private Property _rtimeout As Integer
    Private Property _wtimeout As Integer



    Public Sub New(Optional ip As String = "127.0.0.1", Optional port As Integer = 2009, Optional teamname As String = "TMN")
        _tcp = New TcpClient()
        _hostip = ip
        _hostport = port
        _teamname = teamname
        _playing = True
        _rtimeout = 10000
        _wtimeout = 10000
    End Sub

    Public Sub GameEnd()
        _playing = False
        Me.DisConnect()
    End Sub
    Public Property playing() As Boolean
        Get
            Return _playing
        End Get
        Set(value As Boolean)
            _playing = value
        End Set
    End Property
    Public Function Connect() As Integer
        Try
            _tcp.Connect(_hostip, _hostport)
            _ns = _tcp.GetStream()
            Me.WriteTimeout = _wtimeout
            send(_teamname)
            Return 1
        Catch
            Throw
        End Try
    End Function
    Public Function DisConnect() As Integer
        _ns.Close()
        _tcp.Close()
        Return 1
    End Function


    Public Function send(ByRef str As String, Optional oChaser As Chaser = Nothing) As Integer

        If Not oChaser Is Nothing Then

            '行動ログ
            If str <> "#" Then oChaser.actAdd(str)
            '現在位置
            Select Case True
                Case str = Client.WALK & Client.U
                    oChaser.y -= 1
                Case str = Client.WALK & Client.R
                    oChaser.x += 1
                Case str = Client.WALK & Client.D
                    oChaser.y += 1
                Case str = Client.WALK & Client.L
                    oChaser.x -= 1
            End Select


        End If

        If _ns Is Nothing Then
            Return 0
            Exit Function
        End If

        '送信
        _sendBytes = _enc.GetBytes(str & vbCrLf)
        Try
            _ns.Write(_sendBytes, 0, _sendBytes.Length)
            Return 1
        Catch
            Throw
        End Try

    End Function

    ''' <summary>
    ''' 座標番号と方向の置換
    ''' </summary>
    ''' <param name="i"></param>
    ''' <returns></returns>
    Public Function direction(i As Integer) As String
        If i Mod 2 = 0 Then
            Select Case i
                Case 2
                    Return Client.U
                Case 4
                    Return Client.L
                Case 6
                    Return Client.R
                Case 8
                    Return Client.D
            End Select
        End If
        Return ""
    End Function
    ''' <summary>
    ''' xy座標と方向の置換
    ''' </summary>
    ''' <param name="i"></param>
    ''' <returns></returns>
    Public Function direction(i As Integer()) As String
        Select Case True
            Case i(0) = 0 And i(1) = -1
                Return Client.U
            Case i(0) = -1 And i(1) = 0
                Return Client.L
            Case i(0) = 1 And i(1) = 0
                Return Client.R
            Case i(0) = 0 And i(1) = 1
                Return Client.D
        End Select
        Return ""
    End Function

    ''' <summary>
    ''' サーバーから送られたデータを受信する
    ''' </summary>
    ''' <returns></returns>
    Public Function Response() As String

        Dim resMsg As String = receive()
        If resMsg = "" OrElse resMsg Like "0*" Then GameEnd()
        If _start = False AndAlso resMsg = "@" Then
            _start = True
            Me.ReadTimeout = _rtimeout
        End If

        Return resMsg
    End Function


    Private Function receive() As String
        'サーバーから送られたデータを受信する
        Dim ms As New System.IO.MemoryStream()
        Dim resBytes As Byte() = New Byte(255) {}
        Dim resSize As Integer = 0
        Do
            'データの一部を受信する
            Try
                resSize = _ns.Read(resBytes, 0, resBytes.Length)
            Catch ex As Exception
                Throw ex
                Console.WriteLine(ex.ToString)
            End Try
            'Readが0を返した時はサーバーが切断したと判断
            If resSize = 0 Then
                Console.WriteLine("サーバーが切断しました。")
                Exit Do
            End If
            '受信したデータを蓄積する
            ms.Write(resBytes, 0, resSize)
            'まだ読み取れるデータがあるか、データの最後が\nでない時は、
            ' 受信を続ける
        Loop While _ns.DataAvailable OrElse
            resBytes(resSize - 1) <> AscW(ControlChars.Lf)
        '受信したデータを文字列に変換
        Dim resMsg As String = _enc.GetString(ms.GetBuffer(), 0, CInt(ms.Length))
        ms.Close()
        '末尾の\nを削除
        resMsg = resMsg.TrimEnd(ControlChars.CrLf)
        resMsg = resMsg.TrimEnd(ControlChars.Cr)
        resMsg = resMsg.TrimEnd(ControlChars.Lf)
        resMsg = resMsg.TrimEnd(vbCrLf)
        Return resMsg
    End Function



    Public Property WriteTimeout As Integer
        Get
            Return _ns.WriteTimeout
        End Get
        Set(value As Integer)
            _ns.WriteTimeout = value
        End Set
    End Property
    Public Property ReadTimeout As Integer
        Get
            Return _ns.ReadTimeout
        End Get
        Set(value As Integer)
            _ns.ReadTimeout = value
        End Set
    End Property
    Public Property sendBytes As Byte()
        Get
            Return _sendBytes
        End Get
        Set(value As Byte())
            _sendBytes = value
        End Set
    End Property
    Public ReadOnly Property enc As System.Text.Encoding
        Get
            Return _enc
        End Get
    End Property

    Public Property tcp As TcpClient
        Get
            Return _tcp
        End Get
        Set(value As TcpClient)
            _tcp = value
        End Set
    End Property

    Public Property ns As NetworkStream
        Get
            Return _ns
        End Get
        Set(value As NetworkStream)
            _ns = value
        End Set
    End Property

    Public Property TeamName As String
        Get
            Return _teamname
        End Get
        Set(value As String)
            _teamname = value
        End Set
    End Property

    Public Property HostIP As String
        Get
            Return _hostip
        End Get
        Set(value As String)
            _hostip = value
        End Set
    End Property

    Public Property HostPort As Integer
        Get
            Return _hostport
        End Get
        Set(value As Integer)
            _hostport = value
        End Set
    End Property



End Class
