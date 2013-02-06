Imports System.Net
Imports System.Text.RegularExpressions

Public Class XboxProfile

    '    {
    '  "Data": {
    '    "Tier": "gold",
    '    "IsValid": 1,
    '    "IsCheater": 0,
    '    "IsOnline": 0,
    '    "OnlineStatus": "Last seen 16 hours ago playing Modern Warfare&#174; 3",
    '    "XBLLaunchTeam": 0,
    '    "NXELaunchTeam": 0,
    '    "KinectLaunchTeam": 0,
    '    "AvatarTile": "http:\/\/avatar.xboxlive.com\/avatar\/Nacorpio\/avatarpic-l.png",
    '    "AvatarSmall": "http:\/\/avatar.xboxlive.com\/avatar\/Nacorpio\/avatarpic-s.png",
    '    "AvatarLarge": "http:\/\/avatar.xboxlive.com\/avatar\/Nacorpio\/avatarpic-l.png",
    '    "AvatarBody": "http:\/\/avatar.xboxlive.com\/avatar\/Nacorpio\/avatar-body.png",
    '    "AvatarTileSSL": "https:\/\/avatar-ssl.xboxlive.com\/avatar\/Nacorpio\/avatarpic-l.png",
    '    "AvatarSmallSSL": "https:\/\/avatar-ssl.xboxlive.com\/avatar\/Nacorpio\/avatarpic-s.png",
    '    "AvatarLargeSSL": "https:\/\/avatar-ssl.xboxlive.com\/avatar\/Nacorpio\/avatarpic-l.png",
    '    "AvatarBodySSL": "https:\/\/avatar-ssl.xboxlive.com\/avatar\/Nacorpio\/avatar-body.png",
    '    "Gamertag": "Nacorpio",
    '    "GamerScore": 410,
    '    "Reputation": 12,
    '    "Name": null,
    '    "Motto": "",
    '    "Location": "",
    '    "Bio": null
    '  },
    '  "Stat": "ok",
    '  "In": 2.539,
    '  "Authed": "false",
    '  "AuthedAs": null
    '}

    Private _Friends As New List(Of XboxProfile)

    Private _Tier As String, _IsValid As Boolean, _IsCheater As Boolean, _IsOnline As Boolean, _
    _OnlineStatus As String, _AvatarSmall As String, _AvatarSmallSSL As String, _Gamertag As String, _
    _Gamerscore As Integer, _Reputation As Integer, _Name As String, _Motto As String, _Location As String, _
    _Biography As String

    Sub New(ByVal Gamertag As String)
        Dim webClient As New WebClient
        Dim src As String
        If Gamertag <> String.Empty Then
            src = webClient.DownloadString("http://www.xboxleaders.com/api/profile.json?gamertag=" & Gamertag)
            _Tier = ParseJson(src, "Tier").ToString.Trim
            _IsValid = CBool(ParseJson(src, "IsValid").ToString.Trim)
            '_IsCheater = CBool(ParseJson(src, "IsCheater").ToString.Trim)
            '_IsOnline = CBool(ParseJson(src, "IsOnline").ToString.Trim)
            _OnlineStatus = ParseJson(src, "OnlineStatus").ToString.Trim
            _AvatarSmall = ParseJson(src, "AvatarSmall").ToString.Trim
            _AvatarSmallSSL = ParseJson(src, "AvatarSmallSSL").ToString.Trim
            _Gamertag = ParseJson(src, "Gamertag").ToString.Trim
            '_Gamerscore = CInt(ParseJson(src, "GamerScore").ToString.Trim)
            '_Reputation = CInt(ParseJson(src, "Reputation").ToString.Trim)
            _Name = ParseJson(src, "Name").ToString.Replace("null", String.Empty).ToString.Trim
            _Motto = ParseJson(src, "Motto").ToString.Trim
            _Location = ParseJson(src, "Location").ToString.Trim
            _Biography = ParseJson(src, "Bio").ToString.Trim
            _Friends = GetFriends(Gamertag)
        Else
            Throw New Exception("Do not leave the gamertag empty.")
        End If
    End Sub

    Public Shared Function GetFriends(ByVal Gamertag As String) As List(Of XboxProfile)
        Dim result As New List(Of XboxProfile)
        Dim webClient As New WebClient
        Dim regex As New Regex("""Gamertag"": "".*?""")
        Dim src As String
        If Gamertag <> String.Empty Then
            src = webClient.DownloadString("http://www.xboxleaders.com/api/friends.json?gamertag=" & Gamertag)
            For Each m As Match In regex.Matches(src)
                result.Add(New XboxProfile(m.Value.Split("""Gamertag"": """)(1).Split("""")(0).Trim))
            Next
        End If
        Return result
    End Function

    ''' <summary>
    ''' Parse a JSON function using this function.
    ''' </summary>
    ''' <param name="src">The source to parse from.</param>
    ''' <param name="name">The name of the JSON function to parse.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Private Shared Function ParseJson(ByVal src As String, ByVal name As String) As Object
        Return Split(src, """" & name & """:")(1).Split(",")(0).Trim
    End Function

    ''' <summary>
    ''' Parse a JSON array using this function.
    ''' </summary>
    ''' <param name="src">The source to parse from.</param>
    ''' <param name="name">The name of the JSON array to parse.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Private Shared Function ParseArray(ByVal src As String, ByVal name As String) As String
        Return Split(src, """" & name & """: [")(1).Split("]")(0).Replace("null", String.Empty)
    End Function

    Public ReadOnly Property Friends As List(Of XboxProfile)
        Get
            Return _Friends
        End Get
    End Property

    Public ReadOnly Property Tier As String
        Get
            Return _Tier
        End Get
    End Property

    Public ReadOnly Property IsValid As Boolean
        Get
            Return _IsValid
        End Get
    End Property

    Public ReadOnly Property IsCheater As Boolean
        Get
            Return _IsCheater
        End Get
    End Property

    Public ReadOnly Property IsOnline As Boolean
        Get
            Return _IsOnline
        End Get
    End Property

    Public ReadOnly Property AvatarSmall As String
        Get
            Return _AvatarSmall
        End Get
    End Property

    Public ReadOnly Property AvatarSmallSSL As String
        Get
            Return _AvatarSmallSSL
        End Get
    End Property

    Public ReadOnly Property Gamertag As String
        Get
            Return _Gamertag
        End Get
    End Property

    Public ReadOnly Property GamerScore As Integer
        Get
            Return _Gamerscore
        End Get
    End Property

    Public ReadOnly Property Reputation As Integer
        Get
            Return _Reputation
        End Get
    End Property

    Public ReadOnly Property Name As String
        Get
            Return _Name
        End Get
    End Property

    Public ReadOnly Property Motto As String
        Get
            Return _Motto
        End Get
    End Property

    Public ReadOnly Property Location As String
        Get
            Return _Location
        End Get
    End Property

    Public ReadOnly Property Biography As String
        Get
            Return _Biography
        End Get
    End Property

End Class
