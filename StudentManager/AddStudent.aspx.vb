
Imports System.Data.SqlClient

Partial Class AddStudent
    Inherits System.Web.UI.Page
    Protected Sub btnAddStudent_Click(sender As Object, e As EventArgs)
        Dim name As String = txtName.Text
        Dim email As String = txtEmail.Text
        Dim phoneNumber As String = txtPhoneNumber.Text

        Dim connectionString As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
        Dim query As String = "INSERT INTO students (fullName) VALUES (@Name)"

        Using connection As New SqlConnection(connectionString)
            Try
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@Name", name)
                    connection.Open()
                    command.ExecuteNonQuery()
                End Using
            Catch ex As Exception
            End Try
        End Using

        Response.Redirect("Default.aspx")
    End Sub

End Class
