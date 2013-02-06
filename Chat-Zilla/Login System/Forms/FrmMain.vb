Imports System.Net
Imports System.Text.RegularExpressions

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

#Region "Program Functionalities"

    '/// Spotify

#End Region

    Public Sub VerifiedAccount(ByVal gamerTag As String)
        Dim membership As String = GetMembership(gamerTag)
        If membership <> "gold" Then
            StatusXboxLiveSignedIn.ForeColor = Color.Red
            StatusXboxLiveSignedIn.Text = "Signed In As """ & gamerTag & """"
        ElseIf membership = "gold" Then
            StatusXboxLiveSignedIn.ForeColor = Color.Green
            StatusXboxLiveSignedIn.Text = "Signed In As """ & gamerTag & """"
        End If
    End Sub

    Public Function IsVerifiable(ByVal userName As String) As Boolean
        Dim webClient As New WebClient
        Dim source As String = webClient.DownloadString("http://www.xboxleaders.com/api/profile.json?gamertag=" & userName)
        Dim name As String = Split(source, """Gamertag"": """)(1).Split(""",")(0)
        If name = userName Then
            Return True
        Else
            Return False
        End If
    End Function

    Dim WithEvents form As Form = FrmInput

    Public Function GetMembership(ByVal userName As String) As String
        Dim webClient As New WebClient
        Dim source As String = webClient.DownloadString("http://www.xboxleaders.com/api/profile.json?gamertag=" & userName)
        Dim membership As String = Split(source, """Tier"": """)(1).Split(""",")(0).Trim
        Return membership
    End Function

    Private Sub form_closed() Handles form.Closed
        If IsVerifiable(FrmInput.Value1) Then
            MsgBox("Verified account with the gamertag " & FrmInput.Value1 & "!", MsgBoxStyle.Information)
            If GetMembership(FrmInput.Value1) <> "gold" Then
                MsgBox("Your Xbox Live Account is not upgraded. Please upgrade to use all of the features in this program.", MsgBoxStyle.Information)
                VerifiedAccount(FrmInput.Value1)
            End If
            VerifiedAccount(FrmInput.Value1)
        Else
            MsgBox("Could not verify the account!", MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub MenuItem101_Click(sender As Object, e As EventArgs) Handles MenuItem101.Click
        FrmInput.OpenInput("Xbox Authentication", "Link Xbox Live Account", "Please enter your Gamertag and the Gamerscore of your account to link it.", Me.Icon, "Gamertag:", "Gamerscore:")
    End Sub

    Private Sub FrmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class