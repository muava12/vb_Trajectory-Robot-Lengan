'Imports Microsoft.VisualBasic.PowerPacks
Public Class Form1

    Dim L1, L2, L3, X1, X2, Y1, Y2, teta1, teta2, teta1start, teta2start, teta1end, teta2end,
        runtime, endtime
    Const pi = 3.14159265358979
    Const Xawal = 270
    Const Yawal = 270

    Private Sub Init_Lengan()
        L1 = Val(TextBox_L1.Text)
        L2 = Val(TextBox_L2.Text)
        If (L1 + L2) > 6 Then
            'Me.Enabled = False  'kalo di vb6 "Form1.Enabled=False"
            MessageBox.Show("Maaf, panjang  L1+L2 maksimal 6.00 !!")
            L1 = 0
            L2 = 0

        End If
    End Sub


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim i As Integer
        i = 0
        ComboBox1.SelectedIndex = 1
        Timer1.Enabled = False
        Timer1.Interval = 100

        Label14.Parent = Panel1
        Label14.BackColor = Color.Transparent
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedIndex = 0 Then
            'MessageBox.Show("Maaf, untuk saat ini hanya bisa 2 lengan :D")
            'ComboBox1.SelectedIndex = 1
            TextBox_L2.Enabled = False
            Label_L2.Enabled = False
            TextBox_L3.Enabled = False
            Label_L3.Enabled = False
            TextBox_Teta2.Enabled = False
            Label_Teta2.Enabled = False
            TextBox_L2.Text = "0.00"
            TextBox_Teta2.Text = "0.00"
        ElseIf ComboBox1.SelectedIndex = 1 Then
            TextBox_L2.Enabled = True
            Label_L2.Enabled = True
            TextBox_L3.Enabled = False
            Label_L3.Enabled = False
            TextBox_L3.Text = "0.00"
            TextBox_Teta2.Enabled = True
            Label_Teta2.Enabled = True
        ElseIf ComboBox1.SelectedIndex = 2 Then
            MessageBox.Show("Maaf, untuk saat ini hanya bisa 1 dan 2 lengan :D")
            ComboBox1.SelectedIndex = 1
            TextBox_L1.Text = "2.00"
            TextBox_L2.Enabled = True
            Label_L2.Enabled = True
            TextBox_L2.Text = "2.00"
            TextBox_L3.Enabled = False
            Label_L3.Enabled = False
            TextBox_L3.Text = "0.00"
        End If
    End Sub

    Private Sub Button_Reset_Click(sender As Object, e As EventArgs) Handles Button_Reset.Click
        Init_Lengan()
        teta1 = 0 : teta2 = 0
        teta1start = 0 : teta2start = 0
        teta1end = 0 : teta2end = 0
        TextBox_L1.Text = "3.00"
        TextBox_L2.Text = "3.00"
        TextBox_Teta1.Text = "0.00"
        TextBox_Teta2.Text = "0.00"
        TextBox_X.Text = "6.00"
        Label_XRun.Text = TextBox_X.Text
        Label_Teta1Run.Text = "0.00"  'kalo label di vb6 "Text" jadi "caption"
        Label_Teta2Run.Text = "0.00"

        Kinematika()
        Gambar()
    End Sub

    Private Sub Button_Kin_Click(sender As Object, e As EventArgs) Handles Button_Kin.Click
        Button_Kin.BackColor = Color.RoyalBlue
        Init_Lengan()
        teta1start = Val(Label_Teta1Run.Text)
        teta2start = Val(Label_Teta2Run.Text)
        teta1end = Val(TextBox_Teta1.Text) * -1
        teta2end = Val(TextBox_Teta2.Text) * -1
        teta1 = teta1start
        teta2 = teta2start
        endtime = Val(TextBox_Time.Text)
        runtime = 0
        Timer1.Enabled = True
    End Sub

    Private Sub Kinematika()
        X1 = L1 * Math.Cos(teta1 * pi / 180) 'kalo di vb6 langsung Cos Sin Tan, Ga pake embel2 Math.
        Y1 = L1 * Math.Sin(teta1 * pi / 180)
        X2 = X1 + (L2 * Math.Cos((teta1 + teta2) * pi / 180))
        Y2 = Y1 + (L2 * Math.Sin((teta1 + teta2) * pi / 180))
    End Sub

    Private Sub Button_InvKin_Click(sender As Object, e As EventArgs) Handles Button_InvKin.Click
        Button_InvKin.BackColor = Color.RoyalBlue
        Init_Lengan()
        X2 = Val(TextBox_X.Text)
        Y2 = Val(TextBox_Y.Text)
        If (Math.Sqrt(X2 ^ 2 + Y2 ^ 2) > (L1 + L2)) Or (Math.Sqrt(X2 ^ 2 + Y2 ^ 2) < Math.Abs(L1 - L2)) Then
            MessageBox.Show("Maaf, target berada diluar workspace !!")
        Else
            Inverse_Kinematika()
            TextBox_Teta1.Text = Format(teta1, "0.00")
            TextBox_Teta2.Text = Format(teta2, "0.00")
            Button_Kin.PerformClick()
        End If
    End Sub

    Private Sub Inverse_Kinematika()
        Dim A
        A = (X2 ^ 2 + Y2 ^ 2 - L1 ^ 2 - L2 ^ 2) / (2 * L1 * L2)
        If A = 0 Then A = 1.0E-20
        If X2 = 0 Then X2 = 1.0E-20
        teta2 = Math.Atan(Math.Sqrt(1 - A ^ 2) / A)
        If Math.Sqrt(X2 ^ 2 + Y2 ^ 2) < Math.Sqrt(L1 ^ 2 + L2 ^ 2) Then teta2 = pi + teta2
        teta1 = Math.Atan(Y2 / X2) - Math.Atan((L2 * Math.Sin(teta2)) / (L1 + (L2 * Math.Cos(teta2))))
        If X2 < 0 Then teta1 = pi + teta1
        teta2 = (teta2 * 180) / pi
        teta1 = (teta1 * 180) / pi
    End Sub

    Private Sub Gambar()
        X1 = Math.Round(X1 * 45) : Y1 = Math.Round(Y1 * 45) : X2 = Math.Round(X2 * 45) : Y2 = Math.Round(Y2 * 45)
        Dim jarak1, jarak2, diameter1, diameter2 As Integer
        Dim pulpen1 As New Pen(Drawing.Color.Red, 2)
        'Dim pulpen2 As New Pen(Drawing.Color.Brown, 2)
        Dim pulpen3 As New Pen(Drawing.Color.YellowGreen, 10)
        Dim grafik As Graphics = Panel1.CreateGraphics
        'grafik.Clear(Color.PaleTurquoise)  'kao di vb6, ini jadi "Panel1.Cls" 
        Panel1.Refresh()
        jarak1 = 45 * Math.Abs(L1 - L2)
        jarak2 = 45 * (L1 + L2)

        diameter1 = jarak1 * 2
        diameter2 = jarak2 * 2

        grafik.DrawEllipse(pulpen1, (Xawal - jarak1), (Yawal - jarak1), diameter1, diameter1)
        grafik.DrawEllipse(pulpen1, (Xawal - jarak2), (Yawal - jarak2), diameter2, diameter2)

        Dim pointA1 As New Point(Xawal, Yawal)
        Dim pointA2 As New Point(Xawal + X1, Xawal + Y1)
        Dim pointB1 As New Point(Xawal + X1, Xawal + Y1)
        Dim pointB2 As New Point(Xawal + X2, Xawal + Y2)
        grafik.DrawLine(pulpen3, pointA1, pointA2) 'lengan 1
        grafik.DrawLine(pulpen3, pointB1, pointB2) 'lengan 2

        Dim lebar As Integer = 20
        Dim tinggi As Integer = 20
        Dim rect0 As New Rectangle(Xawal - 10, Yawal - 10, lebar, tinggi)
        Dim rect1 As New Rectangle((Xawal - 10) + X1, (Yawal - 10) + Y1, lebar, tinggi)
        Dim rect2 As New Rectangle((Xawal - 10) + X2, (Yawal - 10) + Y2, lebar, tinggi)
        grafik.FillEllipse(Brushes.DarkCyan, rect0) 'Servo Lengan 1
        Label16.Visible = True
        grafik.FillEllipse(Brushes.DarkCyan, rect1) 'Servo Lengan 2
        grafik.FillEllipse(Brushes.Brown, rect2) 'End of Effector

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Kinematika()
        Gambar()
        Label_Runtime.Text = Format(runtime, "0.00")
        Label_Teta1Run.Text = Format(teta1, "0.00")
        Label_Teta2Run.Text = Format(teta2, "0.00")
        Label_XRun.Text = Format(X2 / 45, "0.00")
        Label_YRun.Text = Format((Y2 / 45) * -1, "0.00") 'pertanyaan: kenapa harus di kalikan "-1" baru di hasilkan posisi yang benar, padahal di contoh program gak usah????

        '------program progress bar-----
        ProgressBar1.Maximum = endtime
        ProgressBar1.Minimum = 0
        ProgressBar1.Value = runtime

        If Val(Label_Runtime.Text) >= endtime Then
            TextBox_X.Text = Label_XRun.Text
            TextBox_Y.Text = Label_YRun.Text
            runtime = 0
            Button_Kin.BackColor = Color.Empty
            Button_InvKin.BackColor = Color.Empty
            Timer1.Enabled = False
        End If
        runtime = runtime + 0.1
        teta1 = teta1start + (((teta1end - teta1start) * runtime) / endtime)
        teta2 = teta2start + (((teta2end - teta2start) * runtime) / endtime)
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'MsgBox("Dibuat oleh MUAFA ROSYAD pada tanggal 13-15 Desember 2013" & vbCrLf & "Surabaya", , Title:="Tentang Aplikasi ini")
        Form_About.ShowDialog()

    End Sub

    Private Sub Button_Keterangan_Click(sender As Object, e As EventArgs) Handles Button_Keterangan.Click
        If GroupBox_Keterangan.Visible = False Then
            Timer_Keterangan_Tampil.Enabled = True
        ElseIf GroupBox_Keterangan.Visible = True Then
            Timer_Keterangan_Tutup.Enabled = True
            GroupBox_Keterangan.Visible = False
        End If
    End Sub

    Private Sub Timer_Keterangan_Tampil_Tick(sender As Object, e As EventArgs) Handles Timer_Keterangan_Tampil.Tick
        If Me.Width < 967 Then
            Me.Width += 10
        Else
            Timer_Keterangan_Tampil.Enabled = False
            GroupBox_Keterangan.Visible = True
        End If
    End Sub

    Private Sub Timer_Keterangan_Tutup_Tick(sender As Object, e As EventArgs) Handles Timer_Keterangan_Tutup.Tick
        If Me.Width > 777 And Me.Width <= 970 Then
            Me.Width -= 10
        Else
            Timer_Keterangan_Tutup.Enabled = False
            GroupBox_Keterangan.Visible = False
        End If
    End Sub
End Class
