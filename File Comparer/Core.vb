'This module's imports and settings.
Option Compare Binary
Option Explicit On
Option Infer Off
Option Strict On

Imports System
Imports System.Collections.Generic
Imports System.Environment
Imports System.IO
Imports System.Linq

'This module contains this program's core procedures.
Public Module CoreModule
   'This procedure is executed when this program is started.
   Public Sub Main()
      Try
         Dim Arguments As New List(Of String)(GetCommandLineArgs())
         Dim Path1 As String = Nothing
         Dim Path2 As String = Nothing

         Arguments.RemoveAt(0)

         If Arguments.Count < 2 Then
            With My.Application.Info
               Console.WriteLine($"{ .Title} v{ .Version} - by: { .CompanyName}, ***2023***{NewLine}")
               Console.WriteLine($"Usage: ""{ .AssemblyName}"" PATH1 PATH2")
            End With
         Else
            Path1 = RemoveQuotes(Arguments.First().Trim()).Trim()
            Path2 = RemoveQuotes(Arguments.Last().Trim()).Trim()
            CompareFiles(Path1, Path2)
         End If
      Catch ExceptionO As Exception
         DisplayException(ExceptionO)
      End Try
   End Sub

   'This procedure compares the files in the specified directories with each other.
   Private Sub CompareFiles(Path1 As String, Path2 As String)
      Try
         For Each FileO As FileInfo In My.Computer.FileSystem.GetDirectoryInfo(Path1).GetFiles("*.*")
            If Not File.ReadAllBytes(FileO.FullName).SequenceEqual(File.ReadAllBytes(Path.Combine(Path2, FileO.Name))) Then
               Console.WriteLine(FileO)
            End If
         Next FileO
      Catch ExceptionO As Exception
         DisplayException(ExceptionO)
      End Try
   End Sub

   'This procedure displays any exceptions that occur.
   Private Sub DisplayException(ExceptionO As Exception)
      Try
         Console.WriteLine($"ERROR: {ExceptionO.Message}")
      Catch
         [Exit](0)
      End Try
   End Sub

   'This procedure returns the specified text with any leading and trailing quote removed.
   Private Function RemoveQuotes(Text As String) As String
      Try
         Dim Result As String = Text

         If Result.StartsWith(""""c) Then Result = Result.Remove(0, 1)
         If Result.EndsWith(""""c) Then Result = Result.Remove(Result.Length - 1, 1)

         Return Result
      Catch ExceptionO As Exception
         DisplayException(ExceptionO)
      End Try

      Return Nothing
   End Function
End Module
