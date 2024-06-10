
Imports System.Data
Imports System.Data.SqlClient

Partial Class _Default
    Inherits Page
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        BindGridView()
    End Sub
    Private Sub BindGridView()

        Dim connectionString As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
        Dim query As String = "SELECT * FROM Students"
        Dim dataTable As New DataTable()
        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(query, connection)
                Try
                    connection.Open()
                    Dim reader As SqlDataReader = command.ExecuteReader()
                    dataTable.Columns.Add("ID", GetType(Integer))
                    dataTable.Columns.Add("FullName", GetType(String))
                    dataTable.Columns.Add("Email", GetType(String))
                    dataTable.Columns.Add("Avatar", GetType(String))
                    If reader.HasRows Then
                        While reader.Read()
                            Dim id As Integer = reader.GetInt32(0)
                            Dim fullName As String = reader.GetString(1)
                            Dim email As String = reader.GetString(2)
                            Dim avatar As String = reader.GetString(3)
                            dataTable.Rows.Add(id, fullName, email, avatar)
                        End While
                    End If
                    reader.Close()
                Catch ex As Exception
                End Try
            End Using
        End Using
        studentsTable.DataSource = dataTable
        studentsTable.DataBind()
    End Sub
    Protected Sub studentsTable_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles studentsTable.RowEditing
        Dim studentID As String = studentsTable.DataKeys(e.NewEditIndex).Value.ToString()
        Dim editUrl As String = String.Format("EditStudent.aspx?studentID={0}", studentID)
        Response.Redirect(editUrl)
    End Sub

    Protected Sub studentsTable_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles studentsTable.RowDeleting
        Try
            Dim id As Integer = Convert.ToInt32(studentsTable.DataKeys(e.RowIndex).Value)
            Dim connectionString As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Using connection As New SqlConnection(connectionString)
                Dim command As New SqlCommand("DELETE FROM Students WHERE id = @id", connection)
                command.Parameters.AddWithValue("@id", id)
                connection.Open()
                command.ExecuteNonQuery()
            End Using
            BindGridView()
        Catch ex As Exception
        End Try
    End Sub
End Class