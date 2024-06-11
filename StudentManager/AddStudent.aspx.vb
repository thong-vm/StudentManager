Imports System.Data.SqlClient
Imports System.IO

Partial Class AddStudent
    Inherits System.Web.UI.Page

    Protected Sub btnAddStudent_Click(sender As Object, e As EventArgs)
        Dim fullName As String = txtName.Text
        Dim email As String = txtEmail.Text
        Dim avatarPath As String = ""

        If fileAvatar.HasFile Then
            Try
                Dim fileName As String = Path.GetFileName(fileAvatar.PostedFile.FileName)
                Dim fileExtension As String = Path.GetExtension(fileName)
                Dim newFileName As String = Guid.NewGuid().ToString() & fileExtension
                avatarPath = "/Uploads/Avatars/" & newFileName

                Dim uploadPath As String = Server.MapPath("~/Uploads/Avatars/")
                If Not Directory.Exists(uploadPath) Then
                    Directory.CreateDirectory(uploadPath)
                End If

                fileAvatar.PostedFile.SaveAs(Path.Combine(uploadPath, newFileName))
            Catch ex As Exception
            End Try
        End If

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
