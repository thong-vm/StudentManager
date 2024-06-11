
Imports System.Activities.Statements
Imports System.Data.SqlClient
Imports System.IO

Partial Class EditStudent
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim id As String = Request.QueryString("studentID")

            If Not String.IsNullOrEmpty(id) Then
                LoadStudentData(id)
            Else
                Response.Redirect("Default.aspx")
            End If
        End If
    End Sub

    Private Sub LoadStudentData(id As String)
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString

        Using connection As New SqlConnection(connectionString)
            Dim command As New SqlCommand("SELECT fullName, email, avatar FROM Students WHERE id = @id", connection)
            command.Parameters.AddWithValue("@id", id)

            connection.Open()
            Using reader As SqlDataReader = command.ExecuteReader()
                If reader.Read() Then
                    txtName.Text = reader("fullName").ToString()
                    txtEmail.Text = reader("email").ToString()
                    imgAvatar.ImageUrl = reader("avatar").ToString()
                End If
            End Using
        End Using
    End Sub
    Protected Sub btnEditStudent_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim id As String = Request.QueryString("studentID")

        If Not String.IsNullOrEmpty(id) Then
            UpdateStudentData(id)

        Else
            Response.Redirect("Default.aspx")
        End If
    End Sub
    Private Sub UpdateStudentData(id As String)


        Dim avatarPath As String = imgAvatar.ImageUrl.ToString()

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

        Using connection As New SqlConnection(connectionString)
            Dim command As New SqlCommand("UPDATE Students SET fullName = @fullName, email = @email, avatar = @avatar WHERE id = @id", connection)
            command.Parameters.AddWithValue("@fullName", txtName.Text)
            command.Parameters.AddWithValue("@email", txtEmail.Text)
            command.Parameters.AddWithValue("@avatar", avatarPath)
            command.Parameters.AddWithValue("@id", id)
            connection.Open()
            command.ExecuteNonQuery()
        End Using

        Response.Redirect("Default.aspx")
    End Sub
End Class
