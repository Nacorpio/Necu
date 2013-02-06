Imports System.Net
Imports System.Text.RegularExpressions
Imports System.ComponentModel

Public Class FrmMain

    '/// Constant variables

    Public Const DEFAULT_NAME As String = "Guest"
    Public Const OWNER_NAME As String = "Admin"

    '/// The processes managed in the program.

    Private GameProcesses() As String = New String() {"Steam|Steam.exe", "Call of Duty: Modern Warfare 2|iw4mp.exe", "Call of Duty 4|iw3mp.exe", "Call of Duty: Black Ops - Multiplayer|BlackOpsMP.EXE", "Call of Duty: Black Ops - Singleplayer|BlackOpsSP.EXE", "BC2|BFBC2Game.exe", "Call of Duty: World at War|CoDWaWmp.exe", _
                                                      "Quake 3|quake3.exe", "Urban Terror|ioUrbanTerror.exe"}
    Private MusicProcesses() As String = New String() {"iTunes|iTunes.exe", "Spotify|spotify.exe"}
    Private OtherProcesses() As String = New String() {"Skype|Skype.exe", "Firefox|firefox.exe", "Bootcamp|Bootcamp.exe"}
    Private DisallowedProcesses() As String = New String() {"Wireshark|wireshark.exe", "Process Hacker|ProcessHacker.exe", ""}

    Private _XboxProfile As XboxProfile

#Region "Program Functionalities"

    '/// Spotify

#End Region

    Public Event Verified()

    Private Sub Account_Verified() Handles Me.Verified
        StatusXboxLiveSignedIn.ForeColor = Color.Blue
        StatusXboxLiveSignedIn.Text = "Retrieving friends..."
        For Each xProfile As XboxProfile In _XboxProfile.Friends
            Dim lItem As New ListViewItem(xProfile.Gamertag)
            lItem.SubItems.Add(xProfile.Reputation)
            lItem.SubItems.Add(xProfile.GamerScore)
            listMyFriends.Items.Add(lItem)
        Next
        StatusXboxLiveSignedIn.ForeColor = Color.Green
        StatusXboxLiveSignedIn.Text = "Signed In As """ & _XboxProfile.Gamertag & """"
    End Sub

    Public Sub VerifiedAccount(ByVal gamerTag As String)
        StatusXboxLiveSignedIn.ForeColor = Color.Blue
        StatusXboxLiveSignedIn.Text = "Verifying account..."
        _XboxProfile = New XboxProfile(gamerTag)
        RaiseEvent Verified()
    End Sub

    Dim WithEvents form As Form = FrmInput

    Public Function GetMembership(ByVal userName As String) As String
        Dim webClient As New WebClient
        Dim source As String = webClient.DownloadString("http://www.xboxleaders.com/api/profile.json?gamertag=" & userName)
        Dim membership As String = Split(source, """Tier"": """)(1).Split(""",")(0).Trim
        Return membership
    End Function

    Private WithEvents bgw As New BackgroundWorker

    Private Sub bgw_DoWork() Handles bgw.DoWork
        MsgBox("Verified account with the gamertag " & FrmInput.Value1 & "!", MsgBoxStyle.Information)
        VerifiedAccount("Nacorpio")
    End Sub

    Private Sub form_closed() Handles form.Closed
        bgw.RunWorkerAsync()
    End Sub

    Private Sub MenuItem101_Click(sender As Object, e As EventArgs) Handles MenuItem101.Click
        FrmInput.OpenInput("Xbox Authentication", "Link Xbox Live Account", "Please enter your Gamertag and the Gamerscore of your account to link it.", Me.Icon, "Gamertag:", "Gamerscore:")
    End Sub

    Private Sub FrmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Control.CheckForIllegalCrossThreadCalls = False
    End Sub
End Class