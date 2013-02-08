Imports System.Text.RegularExpressions
Imports System.Net

Public Class ChatZilla

    '/// User Variables
    Private _CurrentUser As String, _CurrentProfile As String, _CurrentAuthentication As String

    '/// Program Engine Properties/Variables
    Private _Version As String, _NewVersion As String

    '/// Friends from different networks.
    Private _XboxFriends As New List(Of String), _SkypeFriends As New List(Of String), _
    _SteamFriends As New List(Of String)

    Public Enum SignInType As Integer
        XboxLive = 0
        WindowsLive = 1
        Skype = 2
        Steam = 3
    End Enum

End Class
