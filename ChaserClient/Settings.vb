''' <summary>
''' 設定
''' </summary>
Public Class Settings
    Private Const setFileName As String = "settings.config"
    Private _appSettings As New settingItems

    Public Property Ip() As String
        Get
            Return _appSettings.Ip
        End Get
        Set(ByVal Value As String)
            _appSettings.Ip = Value
        End Set
    End Property

    Public Property Port() As String
        Get
            Return _appSettings.Port
        End Get
        Set(ByVal Value As String)
            _appSettings.Port = Value
        End Set
    End Property
    Public Property TeamName() As String
        Get
            Return _appSettings.TeamName
        End Get
        Set(ByVal value As String)
            _appSettings.TeamName = value
        End Set
    End Property
    Public Sub New()
        _appSettings.Ip = "127.0.0.1"
        _appSettings.Port = "2009"
        _appSettings.TeamName = "TMN"
    End Sub
    ''' <summary>
    ''' 設定ファイルに保存します。
    ''' </summary>
    Public Sub writeSettings()
        Dim sw As IO.StreamWriter = Nothing
        Try
            '＜XMLファイルに書き込む＞
            'XmlSerializerオブジェクトを作成
            '書き込むオブジェクトの型を指定する
            Dim serializer1 As New System.Xml.Serialization.XmlSerializer(GetType(settingItems))
            'ファイルを開く（UTF-8 BOM無し）
            sw = New System.IO.StreamWriter(
            setFileName, False, New System.Text.UTF8Encoding(False))
            'シリアル化し、XMLファイルに保存する
            serializer1.Serialize(sw, _appSettings)
        Catch
            Throw
        Finally
            '閉じる
            sw.Close()
        End Try

    End Sub
    ''' <summary>
    ''' 設定ファイルを読み込みます。
    ''' </summary>
    Public Sub readSettings()
        Dim sr As IO.StreamReader = Nothing
        Try
            If Not System.IO.File.Exists(setFileName) Then
                writeSettings()
            End If
            '＜XMLファイルから読み込む＞
            'XmlSerializerオブジェクトの作成
            Dim serializer2 As New System.Xml.Serialization.XmlSerializer(GetType(settingItems))
            'ファイルを開く
            sr = New System.IO.StreamReader(
                setFileName, New System.Text.UTF8Encoding(False))
            'XMLファイルから読み込み逆シリアル化する
            _appSettings = CType(serializer2.Deserialize(sr), settingItems)
        Catch
            Throw
        Finally
            sr.Close()
        End Try

    End Sub

End Class
Public Class settingItems
    Private _ip As String
    Private _port As String
    Private _teamname As String
    Public Property Ip() As String
        Get
            Return _ip
        End Get
        Set(ByVal Value As String)
            _ip = Value
        End Set
    End Property

    Public Property Port() As String
        Get
            Return _port
        End Get
        Set(ByVal Value As String)
            _port = Value
        End Set
    End Property
    Public Property TeamName() As String
        Get
            Return _teamname
        End Get
        Set(ByVal value As String)
            _teamname = value
        End Set
    End Property
    Public Sub New()
    End Sub
End Class