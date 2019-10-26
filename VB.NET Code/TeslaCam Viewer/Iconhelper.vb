Imports System.Runtime.InteropServices

Public Enum IconSizes As Integer
    Large32x32 = 0
    Small16x16 = 1
End Enum

Public Class Iconhelper
    Private Const SHGFI_ICON As Integer = &H100
    Private Const SHGFI_USEFILEATTRIBUTES As Integer = &H10

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)>
    Private Structure SHFILEINFOW
        Public hIcon As IntPtr
        Public iIcon As Integer
        Public dwAttributes As Integer
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=260)> Public szDisplayName As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=80)> Public szTypeName As String
    End Structure

    <DllImport("shell32.dll", EntryPoint:="SHGetFileInfoW")>
    Private Shared Function SHGetFileInfoW(<MarshalAs(UnmanagedType.LPTStr)> ByVal pszPath As String, ByVal dwFileAttributes As Integer, ByRef psfi As SHFILEINFOW, ByVal cbFileInfo As Integer, ByVal uFlags As Integer) As Integer
    End Function

    <DllImport("user32.dll", EntryPoint:="DestroyIcon")> Private Shared Function DestroyIcon(ByVal hIcon As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    ''' <summary>Gets a Bitmap image of the specified file or folder icon.</summary>
    ''' <param name="FileOrFolderPath">The full path to the folder or file to get the icon image from, or just a file extension (.ext) to get the registered icon image of the file type.</param>
    ''' <param name="IconSize">The size of the icon to retrieve.</param>
    Public Shared Function GetIconImage(ByVal FileOrFolderPath As String, ByVal IconSize As IconSizes) As Bitmap
        Dim bm As Bitmap = Nothing
        Dim fi As New SHFILEINFOW
        Dim flags As Integer = If(FileOrFolderPath.StartsWith("."), (IconSize Or SHGFI_USEFILEATTRIBUTES), IconSize)
        If SHGetFileInfoW(FileOrFolderPath, 0, fi, Marshal.SizeOf(fi), SHGFI_ICON Or flags) <> 0 Then
            bm = Icon.FromHandle(fi.hIcon).ToBitmap
        End If
        DestroyIcon(fi.hIcon).ToString()
        Return bm
    End Function
End Class
