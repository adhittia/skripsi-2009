Imports System.Runtime.InteropServices
Imports System.Text


Public Class StringUtils

    Public Const SWP_NOMOVE As Integer = 2
    Public Const SWP_NOSIZE As Integer = 1
    Public Const HWND_TOPMOST As Integer = -1
    Public Const HWND_NOTOPMOST As Integer = -2

    ' The CreateEllipticRgn function creates an elliptical region
    <DllImport("gdi32")> _
   Public Shared Function CreateEllipticRgn(ByVal X1 As Integer, _
           ByVal Y1 As Integer, _
           ByVal X2 As Integer, _
           ByVal Y2 As Integer) As Integer
    End Function

    ' The SetWindowRgn function sets the window region of a window. The window region determines the area within the window where the system permits drawing. The system does not display any portion of a window that lies outside of the window region 
    <DllImport("user32")> _
    Public Shared Function _
       SetWindowRgn(ByVal hWnd As Integer, _
        ByVal hRgn As Integer, _
        ByVal bRedraw As Integer) As Integer
    End Function


    <DllImport("user32")> _
    Public Shared Function SetWindowPos(ByVal hwnd As Integer, _
        ByVal hWndInsertAfter As Integer, _
        ByVal x As Integer, _
        ByVal y As Integer, _
        ByVal cy As Long, _
        ByVal cx As Integer, _
       ByVal wFlags As Integer) As Integer
    End Function

    Public Shared Function IsUnicode(ByVal value As String) As Boolean
        Dim ascii As Byte() = AsciiStringToByteArray(value)
        Dim unicode As Byte() = UnicodeStringToByteArray(value)
        Dim value1 As String = FromASCIIByteArray(ascii)
        Dim value2 As String = FromUnicodeByteArray(unicode)
        If value1 <> value2 Then
            Return True
        End If
        Return False
    End Function

    Public Shared Function FromASCIIByteArray(ByVal characters() As Byte) As String
        Dim encoding As New ASCIIEncoding
        Dim constructedString As String = encoding.GetString(characters)
        Return constructedString
    End Function

    Public Shared Function FromUnicodeByteArray(ByVal characters() As Byte) As String
        Dim encoding As New UnicodeEncoding
        Dim constructedString As String = encoding.GetString(characters)
        Return constructedString
    End Function

    Public Shared Function AsciiStringToByteArray(ByVal str As String) As Byte()
        Dim encoding As New System.Text.ASCIIEncoding()
        Return encoding.GetBytes(str)
    End Function 'StrToByteArray

    Public Shared Function UnicodeStringToByteArray(ByVal str As String) As Byte()
        Dim encoding As New System.Text.UnicodeEncoding()
        Return encoding.GetBytes(str)
    End Function 'StrToByteArray

End Class
