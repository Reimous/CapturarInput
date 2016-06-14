Public Class Form1
    Private timer As Stopwatch
    Private sw As Stopwatch
    Private EnableInput, playing, recording As Boolean
    Private ListaInputs As List(Of Inputs)

    Public Declare Function GetAsyncKeyState Lib "user32.dll" (ByVal vKey As Int32) As UShort
    Private Declare Sub mouse_event Lib "user32" (ByVal dwFlags As Integer,
      ByVal dx As Integer, ByVal dy As Integer, ByVal cButtons As Integer,
      ByVal dwExtraInfo As Integer)

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        timer = New Stopwatch
        sw = New Stopwatch
        Timer1.Interval = 25
        timer.Start()
        Timer1.Start()
        EnableInput = True
        playing = False
        recording = False

        ListaInputs = New List(Of Inputs)

        Label1.Text = ""
    End Sub

    Private Function Cronometro(ByVal tiempo As Integer) As Boolean

        If sw.ElapsedMilliseconds > tiempo Then
            sw.Restart()
            Return True
        Else
            sw.Start()
            Return False
        End If
    End Function

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        'If grabando Then

        If recording Then
            For i = 0 To 255
                If GetAsyncKeyState(i) Then
                    If EnableInput = False Then
                        EnableInput = Cronometro(100)
                    Else
                        EnableInput = False
                        Label1.Text &= Chr(i) & "_" & timer.ElapsedMilliseconds & "_" & Cursor.Position.X & "_" & Cursor.Position.Y
                        Dim inp As New Inputs
                        inp.inp = i
                        inp.tiempo = timer.ElapsedMilliseconds
                        inp.posMouse = New Point(Cursor.Position.X, Cursor.Position.Y)
                        ListaInputs.Add(inp)
                    End If

                End If
            Next
        End If

        If GetAsyncKeyState(Convert.ToInt32(Keys.Escape)) Then

        End If

        If GetAsyncKeyState(Convert.ToInt32(Keys.A)) Then

        End If

        If GetAsyncKeyState(1) Then
        ElseIf GetAsyncKeyState(2) Then

        End If
        If GetAsyncKeyState(Keys.ShiftKey) Then
            recording = Not recording
        End If
        'End If
    End Sub

    Private Sub btn_play_Click(sender As Object, e As EventArgs) Handles btn_play.Click
        play()
    End Sub

    Private Sub play()
        Dim timerPlay As New Stopwatch
        timerPlay.Start()
        Dim count = 0
        playing = True
        While count < ListaInputs.Count
            If ListaInputs(count).tiempo < timerPlay.ElapsedMilliseconds Then
                If Not playing Then
                    Exit While
                End If
                If ListaInputs(count).inp = 1 Then
                    Cursor.Position = New Point(ListaInputs(count).posMouse.X, ListaInputs(count).posMouse.Y)
                    mouse_event(&H2, 0, 0, 0, 0)
                    mouse_event(&H4, 0, 0, 0, 0)
                ElseIf ListaInputs(count).inp = 2 Then
                Else
                    SendKeys.Send(Chr(ListaInputs(count).inp))
                End If

                count += 1
            End If

        End While
    End Sub

    Private Sub btn_grabar_Click(sender As Object, e As EventArgs) Handles btn_grabar.Click
        recording = True
    End Sub

    Private Sub btn_stop_Click(sender As Object, e As EventArgs) Handles btn_stop.Click
        playing = False
        recording = False
    End Sub
End Class

Public Class Inputs
    Public inp As Integer
    Public tiempo As Integer
    Public posMouse As Point
End Class

