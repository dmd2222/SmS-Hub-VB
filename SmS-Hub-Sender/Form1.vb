Imports System.Net
Imports System.Text
Imports System.IO

Public Class Form1

    'Functions

    Function sendGET_HTTPS(url As String, getdata As String)


        If url.Substring(url.Length - 1, 1) = "?" Then
        Else
            url = url & "?" & getdata
        End If

        Dim WebBrowser1 As New WebBrowser


        WebBrowser1.Navigate(url)
        'laden
        Do Until WebBrowser1.ReadyState = WebBrowserReadyState.Complete
            My.Application.DoEvents()
        Loop

        Dim erg = WebBrowser1.Document.Body.InnerText

        Return erg
    End Function

    Function send_sms(number, text)
        Dim getdata = "action=SMS_SEND_API&deviceId=" & TextBox2.Text & "&number=" & number & "&message=" & text
        Dim response = sendGET_HTTPS(TextBox1.Text, getdata)
        Return response
    End Function

    'End Functions



    Private Sub ToolStripStatusLabel1_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripStatusLabel1.Click
        Process.Start("https://www.patreon.com/marcoschwald")
    End Sub

   

 

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click

        ListBox1.Items.Add(send_sms(TextBox4.Text, TextBox3.Text))
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        Dim openFileDialog1 As New OpenFileDialog()
        openFileDialog1.Filter = "CSV file (*.csv)| *.csv"
        openFileDialog1.ShowDialog()
        TextBox9.Text = openFileDialog1.FileName
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Dim liste As New List(Of String)

        Dim csvtext = My.Computer.FileSystem.ReadAllText(TextBox9.Text)
        csvtext = csvtext.Replace(" ", "")
        Dim csvarray = csvtext.Split(",")

        For Each item In csvarray
            send_sms(item, TextBox6.Text)
        Next

    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'Eisntellungen speichern
        Dim ini As New INIDatei
        ini.setPath("options.ini")
        ini.WertSchreiben("sekA", "url", TextBox1.Text)
        ini.WertSchreiben("sekA", "deviceId", TextBox2.Text)
        ini.WertSchreiben("sekA", "message", TextBox3.Text)
        ini.WertSchreiben("sekA", "number", TextBox4.Text)
        ini.WertSchreiben("sekA", "csvpath", TextBox9.Text)
    End Sub

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Einstellungen laden
        Dim ini As New INIDatei
        ini.setPath("options.ini")


        TextBox1.Text = ini.WertLesen("sekA", "url")
        TextBox2.Text = ini.WertLesen("sekA", "deviceId")
        TextBox3.Text = ini.WertLesen("sekA", "message")
        TextBox4.Text = ini.WertLesen("sekA", "number")
        TextBox9.Text = ini.WertLesen("sekA", "csvpath")

    End Sub

    Private Sub TextBox1_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox1.TextChanged
        TextBox8.Text = TextBox1.Text
    End Sub

    Private Sub TextBox2_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox2.TextChanged
        TextBox7.Text = TextBox2.Text
    End Sub

    Private Sub TextBox3_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox3.TextChanged
        TextBox6.Text = TextBox3.Text
    End Sub
End Class
