

Public Class MainWindow
    Private player As System.Media.SoundPlayer = Nothing
    Private Const ITEM_GET As String = "sound\get.wav"
    Private Const KILL_ENEMY As String = "sound\kill.wav"
    'Private Const FIND_ENEMY As String = "sound\kei_voice_054.wav"



    Private Property appSettings As New Settings
    Private Property oClient As Client
    Private Property oChaser As Chaser
    Private Property oMap As Map


    Dim h(,) As Integer = New Integer(,) {{0, 0}, {-1, -1}, {0, -1}, {1, -1}, {-1, 0}, {0, 0}, {1, 0}, {-1, 1}, {0, 1}, {1, 1}}
    Private Function vah2int(str As String) As Integer
        Select Case str
            Case Client.U
                Return 2
            Case Client.L
                Return 4
            Case Client.R
                Return 6
            Case Client.D
                Return 8
            Case Else
                Return 0
        End Select
    End Function


    '   割り当てられている数値 状態
    '0 なにもなし
    '1 相手プレイヤがいる
    '2 ブロックがある
    '3 アイテムがある

    Private Sub method()
        Dim loc As String = oChaser.locationHistory(oChaser.locLength - 1)
        Dim data() As Char = loc.ToCharArray
        Dim mem As mapCell() = oChaser.around(oMap)

        Select Case True
            Case oChaser.lastAct Like Client.LOOK & "*"

            Case oChaser.lastAct Like Client.SEARCH & "*"
            Case oChaser.lastAct Like Client.PUT & "*"
            Case oChaser.lastAct Like Client.WALK & "*"
                If oChaser.locationHistory(oChaser.locLength - 2) <> -1 AndAlso
                    loc <> oChaser.locationHistory(oChaser.locLength - 2) Then

                End If
            Case Else


        End Select


        ''ターンエンド時と変化があるかチェック
        'If loc <> oChaser.locationHistory(oChaser.locLength - 2) And
        '    (oChaser.lastAct Like LOOK & "*" OrElse oChaser.lastAct Like SEARCH & "*") Then

        '    '一致しない方向をLOOKorSEARCH?
        '    '前回LOOKorSEARCHの場合単純比較不可

        'End If


        '周囲に敵がいる
        If loc Like "1*1*" Then
            For i = 1 To 9 Step 1
                If data(i) = "1" Then
                    '見敵必殺！
                    If oClient.direction(i) <> "" Then
                        PlaySound(KILL_ENEMY)
                        send(Client.PUT & oClient.direction(i))
                    Else
                        '斜め
                        '隣に石を置く？
                        Dim cross As String = mem(2).status & mem(4).status & mem(6).status & mem(8).status
                        Dim count As Integer = CountChar(cross, "0") + CountChar(cross, "3")
                        '敵の隣のセル
                        Dim enm1 As mapCell() = oChaser.around(oMap, h(i, 0), 0)
                        Dim enm2 As mapCell() = oChaser.around(oMap, 0, h(i, 1))
                        '→置いても自殺しない？ 上下左右に0か3が2個以上
                        If count = 2 Then
                            '→空きが2個
                            '→積みパターンの可能性あり後ろに空きがあるかチェック

                            If (enm1(5).status = 2 And enm2(5).status = 2) Then
                                '→敵の両隣は壁→下がる→ウォーク
                                methodWalk()
                                Exit Sub
                            End If

                            '対角セルの隣を取得
                            Dim tmp1 As mapCell() = oChaser.around(oMap, -1 * h(i, 0), 0)
                            Dim tmp2 As mapCell() = oChaser.around(oMap, 0, -1 * h(i, 1))
                            If tmp1(5).status = 0 OrElse tmp1(5).status = 3 OrElse tmp2(5).status = 0 OrElse tmp2(5).status = 3 Then
                                'PlaySound(FIND_ENEMY)
                                '→→ある→敵の隣に置く
                                If enm1(5).status <> 2 Then
                                    send(Client.PUT & oClient.direction({h(i, 0), 0}))
                                ElseIf enm2(5).status <> 2 Then
                                    send(Client.PUT & oClient.direction({0, h(i, 1)}))
                                End If
                            Else
                                '→→ない→ウォーク→制限しないと敵に突っ込む
                                Dim ban As String() = {oClient.direction({h(i, 0), 0}), oClient.direction({0, h(i, 1)})}
                                methodWalk(ban)
                            End If

                        ElseIf count > 2 Then
                            '→3個以上→置く
                            If enm1(5).status <> 2 Then
                                send(Client.PUT & oClient.direction({h(i, 0), 0}))
                            ElseIf enm2(5).status <> 2 Then
                                send(Client.PUT & oClient.direction({0, h(i, 1)}))
                            End If
                        Else
                            '隣に置けない
                            '下がる→ウォーク
                            methodWalk()
                            Exit Sub
                        End If
                    End If

                    Exit Sub
                End If
            Next
        ElseIf loc Like "1*3*" Then
            For i = 1 To 9 Step 1
                'Console.WriteLine(i)
                If data(i) = "3" AndAlso oClient.direction(i) <> "" Then

                    Dim itm As mapCell() = oChaser.around(oMap, h(i, 0), h(i, 1))
                    Dim cross As String = itm(2).status & itm(4).status & itm(6).status & itm(8).status
                    Dim count As Integer = CountChar(cross, "0") + CountChar(cross, "3")

                    If count > 1 Then
                        '即死はしないから行っちゃえ

                        PlaySound(ITEM_GET)
                        send(Client.WALK & oClient.direction(i))
                    ElseIf cross.Length < 4 Then
                        'アイテム周囲で見えないセルがあるのでLOOKしてみる
                        send(Client.LOOK & oClient.direction(i))
                    Else
                        '即死するので他へ移動
                        Dim ban As String() = {oClient.direction(i)}
                        methodWalk(ban)
                    End If
                    Exit Sub
                End If
            Next
            '斜めチェック
            For i = 0 To 9
                If data(i) = "3" AndAlso oClient.direction(i) = "" Then

                    'アイテム周辺
                    Dim itm As mapCell() = oChaser.around(oMap, h(i, 0), h(i, 1))
                    Dim count As Integer = safeCount(itm)
                    '取れないのでスルー
                    If count < 2 Then Continue For

                    'アイテムの隣のセル
                    Dim itm1 As mapCell() = oChaser.around(oMap, h(i, 0), 0)
                    Dim itm2 As mapCell() = oChaser.around(oMap, 0, h(i, 1))
                    If itm1(5).status = 0 AndAlso itm2(5).status = 0 Then
                        'どっちかに移動
                        Dim ban As String() = {oClient.direction({-1 * h(i, 0), 0}), oClient.direction({0, -1 * h(i, 0)})}
                        methodWalk(ban)

                        '空いてるほうに移動
                    ElseIf itm1(5).status = 0 Then
                        send(Client.WALK & oClient.direction({h(i, 0), 0}))
                    ElseIf itm2(5).status = 0 Then
                        send(Client.WALK & oClient.direction({0, h(i, 1)}))
                    Else
                        Continue For
                    End If

                    Exit Sub
                End If
            Next

        End If

        methodWalk()

    End Sub

    Private Function safeCount(ByRef itm As mapCell()) As Integer
        Dim cross As String = itm(2).status & itm(4).status & itm(6).status & itm(8).status
        Return CountChar(cross, "0") + CountChar(cross, "3")
    End Function


    ''' <summary>
    ''' 移動
    ''' </summary>
    ''' <param name="ban">移動禁止方向</param>
    Private Sub methodWalk(Optional ban As String() = Nothing)

        Dim mem As mapCell() = oChaser.around(oMap)
        Dim banList As String = Join(ban, "")


        '未踏破セルを抽出
        '縦横
        Dim vah As Integer() = Enumerable.Empty(Of Integer)()
        '斜め
        Dim slant As Integer() = Enumerable.Empty(Of Integer)()
        For i As Integer = 1 To 9
            '中心はスルー
            If i = 5 Then Continue For
            'なしはスルー
            If mem(i) Is Nothing Then Continue For
            '除外リスト
            If banList <> "" AndAlso banList Like "*" & oClient.direction(i) & "*" Then Continue For
            '未踏破、通路なら候補に追加
            If mem(i).visited = False AndAlso mem(i).status = "0" Then
                If i Mod 2 = 0 Then
                    ReDim Preserve vah(vah.Length)
                    vah(vah.Length - 1) = i
                Else
                    ReDim Preserve slant(slant.Length)
                    slant(slant.Length - 1) = i
                End If
            End If
        Next

        If vah.Length > 0 AndAlso DoWalk(vah) Then
            Exit Sub
        ElseIf slant.Length > 0 AndAlso DoWalk(slant, True) Then
            Exit Sub
        End If

        '近くの未踏破セルに向かう
        '記憶全検索？→道が連続しているか判定
        '次のターン以降で目的地までの距離が変わってループしそう？
        'ルート指定モード？










        Dim rnd As Integer() = New Integer() {1, 2, 3, 4, 5, 6, 7, 8, 9}
        DoWalk(rnd)

    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="rnd"></param>
    ''' <param name="s">斜め</param>
    ''' <returns></returns>
    Private Function DoWalk(rnd As Integer(), Optional s As Boolean = False) As Boolean

        Dim n As Integer = rnd.Length
        Dim rng As New System.Random()
        While n > 1
            n -= 1
            Dim k As Integer = rng.Next(n + 1)
            Dim tmp As Integer = rnd(k)
            rnd(k) = rnd(n)
            rnd(n) = tmp
        End While

        '保険用
        Dim mem As mapCell() = oChaser.around(oMap)

        For i As Integer = 0 To rnd.Length - 1
            If s Then
                Dim itm1 As mapCell() = oChaser.around(oMap, h(rnd(i), 0), 0)
                Dim itm2 As mapCell() = oChaser.around(oMap, 0, h(rnd(i), 1))
                If itm1(5).status = 0 Then
                    send(Client.WALK & oClient.direction({h(rnd(i), 0), 0}))
                    Return True
                ElseIf itm2(5).status = 0 Then
                    send(Client.WALK & oClient.direction({0, h(rnd(i), 1)}))
                    Return True
                End If
            Else
                If oClient.direction(rnd(i)) <> "" AndAlso mem(rnd(i)).status = 0 Then
                    send(Client.WALK & oClient.direction(rnd(i)))
                    Return True
                End If

            End If
        Next
        Return False
    End Function

    Private Sub init() Handles MyBase.Load
        AddHandler btnConnect.Click, AddressOf gameStart
#If DEBUG Then
        MyBase.Text &= " [DEBUG]"
        txtTMN.Text = "DEBUG"
#End If

        appSettings.readSettings()
        txtSvIP.Text = appSettings.Ip
        txtSvPort.Text = appSettings.Port

    End Sub
    Private Sub FORM_CLOSED() Handles MyBase.Closed
        oMap = Nothing
        appSettings.Ip = txtSvIP.Text
        appSettings.Port = txtSvPort.Text
        appSettings.TeamName = txtTMN.Text
        appSettings.writeSettings()
    End Sub


    ''' <summary>
    ''' 
    ''' </summary>
    Private Sub gameStart()
        btnConnect.Enabled = False

        oChaser = New Chaser
        oMap = New Map(pnlMapOuter)
        pnlMapOuter.Controls.Clear()
        pnlMapOuter.Controls.Add(oMap)
        oMap.mapRefresh(oChaser)

        'サーバーに接続
        oClient = New Client()
        oClient.HostIP = txtSvIP.Text
        oClient.HostPort = CInt(txtSvPort.Text)
        oClient.TeamName = txtTMN.Text
#If DEBUG Then
        oClient.HostIP = "127.0.0.1"
        'oClient.HostIP = "192.168.1.198"
#End If
        Try
            oClient.Connect()
        Catch ex As Exception
            msgShow(ex.ToString)
            btnConnect.Enabled = True
            Exit Sub
        End Try

        Dim str As String = String.Format("サーバー({0}:{1})と接続しました({2}:{3})。",
       DirectCast(oClient.tcp.Client.RemoteEndPoint, System.Net.IPEndPoint).Address,
       DirectCast(oClient.tcp.Client.RemoteEndPoint, System.Net.IPEndPoint).Port,
       DirectCast(oClient.tcp.Client.LocalEndPoint, System.Net.IPEndPoint).Address,
       DirectCast(oClient.tcp.Client.LocalEndPoint, System.Net.IPEndPoint).Port)
        msgShow(str, True)

        Dim turn As Integer = 0
        Dim state As Boolean = False
        Dim strRes As String = ""
        Do While oClient.playing = True
            turn += 1
            strRes = oClient.Response
            str = String.Format("{0}:{1}:{2}", turn, strRes, strRes.Length)
            msgShow(str)
            If oClient.playing = False Then Exit Do

            If strRes = "@" Then
                getReady()
            Else
                oChaser.locationAdd(strRes)
                oMap.setMemory(strRes, oChaser)

                If state Then
                    turnEnd()
                Else
                    method()
                End If
                state = IIf(state, False, True)
            End If
            ' Application.DoEvents()
        Loop
        If strRes Like "0*" Then msgShow("終了コード受信")
        oChaser = Nothing
        'oClient.DisConnect()
        msgShow("終了します。")
        btnConnect.Enabled = True

    End Sub

    Private Sub msgShow(ByRef str As String, Optional init As Boolean = False)
        If init = True Then txtMsg.Text = ""
        txtMsg.Text = str & vbCrLf & txtMsg.Text
        txtMsg.Refresh()
    End Sub

    ''' <summary>
    ''' サーバーへ送信する
    ''' </summary>
    ''' <param name="str"></param>
    ''' <param name="save"></param>
    Private Sub send(ByRef str As String, Optional save As Boolean = True)

        msgShow("send=" & str)
        Try
            If save Then
                oClient.send(str, oChaser)
            Else
                oClient.send(str)
            End If
        Catch ex As Exception
            msgShow(ex.ToString)
        End Try
    End Sub

    Private Sub getReady()
        send("gr")
    End Sub

    Private Sub turnEnd()
        send("#")
    End Sub

    Public Shared Function cutRight(ByVal stTarget As String, ByVal iLength As Integer) As String
        If iLength <= stTarget.Length Then
            Return stTarget.Substring(stTarget.Length - iLength)
        End If

        Return stTarget
    End Function
    ' 文字の出現回数をカウント
    Public Shared Function CountChar(ByVal s As String, ByVal c As Char) As Integer
        Return s.Length - s.Replace(c.ToString(), "").Length
    End Function



    'WAVEファイルを再生する
    Private Sub PlaySound(ByVal waveFile As String)
        '再生されているときは止める
        If Not (player Is Nothing) Then
            StopSound()
        End If

        '読み込む
        player = New System.Media.SoundPlayer(waveFile)
        '非同期再生する
        player.Play()

        '次のようにすると、ループ再生される
        'player.PlayLooping()

        '次のようにすると、最後まで再生し終えるまで待機する
        'player.PlaySync()
    End Sub

    '再生されている音を止める
    Private Sub StopSound()
        If Not (player Is Nothing) Then
            player.Stop()
            player.Dispose()
            player = Nothing
        End If
    End Sub
End Class


