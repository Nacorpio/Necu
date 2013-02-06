Public Class FrmInput

    ''' <summary>
    ''' Occurs whenever the input-window is closed.
    ''' </summary>
    ''' <remarks></remarks>

    Public Shadows Event Closed()

    Private _value1 As String, _value2 As String

    ''' <summary>
    ''' The 1st TextBox's Label Text.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public ReadOnly Property Value1 As String
        Get
            Return _value1
        End Get
    End Property

    ''' <summary>
    ''' The 2nd TextBox's Label Text.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public ReadOnly Property Value2 As String
        Get
            Return _value2
        End Get
    End Property

    ''' <summary>
    ''' Open the window for the user to enter an input.
    ''' </summary>
    ''' <param name="windowTitle">The title of the window.</param>
    ''' <param name="title">The Sub-Title of the text in the window.</param>
    ''' <param name="description">The description of the input to be entered.</param>
    ''' <param name="icon">The icon of the input-window.</param>
    ''' <param name="value1">The text of the number one value to be entered.</param>
    ''' <param name="value2">The text of the number two value to be entered.</param>
    ''' <remarks></remarks>

    Public Sub OpenInput(ByVal windowTitle As String, ByVal title As String, ByVal description As String, ByVal icon As Icon, Optional ByVal value1 As String = "Username:", Optional ByVal value2 As String = "Password:")
        Me.Show()
        Label1.Text = title
        Label2.Text = description
        Label3.Text = value1
        Label4.Text = value2
        Me.Text = windowTitle
        Me.Icon = icon
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Me_Closed() Handles Me.Closed
        _value1 = TextBox1.Text
        _value2 = TextBox2.Text
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        RaiseEvent Closed()
    End Sub
End Class