Imports System.ComponentModel
Imports System.Net
Imports System.Windows.Forms
Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        BackgroundWorker1.WorkerReportsProgress = True
        ProgressBar1.Style = ProgressBarStyle.Marquee
        ProgressBar1.MarqueeAnimationSpeed = 100
        BackgroundWorker1.RunWorkerAsync()
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        BackgroundWorker1.ReportProgress(25, "Installing VC2010")
        Dim wc As New WebClient
        Dim Temp As String = System.IO.Path.GetTempPath
        ' Download VC2010
        Try
            wc.DownloadFile("https://download.microsoft.com/download/1/6/5/165255E7-1014-4D0A-B094-B6A430A6BFFC/vcredist_x86.exe", Temp + "\vcredist_x86.exe")
            Dim Pr2010 = Process.Start(Temp + "\vcredist_x86.exe")
            Pr2010.WaitForExit()
        Catch ex As Exception
            MessageBox.Show("Admin privileges required to install VC2010. Please run the application as an administrator.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        ' Download VC2013
        BackgroundWorker1.ReportProgress(50, "Installing VC2013")
        wc.DownloadFile("https://download.microsoft.com/download/2/E/6/2E61CFA4-993B-4DD4-91DA-3737CD5CD6E3/vcredist_x86.exe", Temp + "\vcredist_2013_x86.exe")
        Dim Pr2013 = Process.Start(Temp + "\vcredist_2013_x86.exe")
        Pr2013.WaitForExit()
        ' Download VC2015
        BackgroundWorker1.ReportProgress(75, "Installing VC2015")
        wc.DownloadFile("https://download.visualstudio.microsoft.com/download/pr/eaab1f82-787d-4fd7-8c73-f782341a0c63/5365A927487945ECB040E143EA770ADBB296074ECE4021B1D14213BDE538C490/VC_redist.x86.exe", Temp + "\vc_redist.x86.exe")
        Dim Pr2015 = Process.Start(Temp + "\vc_redist.x86.exe")
        Pr2015.WaitForExit()
        BackgroundWorker1.ReportProgress(100, "Done")
    End Sub

    Private Sub BackgroundWorker1_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged
        Label1.Text = e.UserState.ToString()
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        Form2.Show()
        Timer1.Interval = 1000
        Timer1.Start()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ' Stop the timer
        Timer1.Stop()

        ' Code to execute after the delay
        Form2.Close()
        Me.Close()
    End Sub
End Class
