﻿Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO

Partial Class AddStudent
    Inherits System.Web.UI.Page

    Protected Sub btnAddStudent_Click(sender As Object, e As EventArgs)
        Dim uploadController = New ImageUploading()
        Dim fullName As String = txtName.Text
        Dim email As String = txtEmail.Text
        Dim avatarPath As String = uploadController.GetUrl(fileAvatar)
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
        Dim query As String = "INSERT INTO Students (fullName, email, avatar) VALUES (@fullName, @email, @avatar)"

        Using connection As New SqlConnection(connectionString)
            Try
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@fullName", fullName)
                    command.Parameters.AddWithValue("@email", email)
                    command.Parameters.AddWithValue("@avatar", avatarPath)
                    connection.Open()
                    command.ExecuteNonQuery()
                End Using
            Catch ex As Exception
            End Try
        End Using

        Response.Redirect("Default.aspx")
    End Sub
End Class
