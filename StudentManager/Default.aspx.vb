
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
        studentsTable.EditIndex = e.NewEditIndex
        BindGridView()
    End Sub

    Protected Sub studentsTable_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles studentsTable.RowCancelingEdit
        studentsTable.EditIndex = -1
        BindGridView()
    End Sub

    Protected Sub studentsTable_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles studentsTable.RowUpdating
        Dim row = studentsTable.Rows(e.RowIndex)
        Dim id As Integer = Convert.ToInt32(studentsTable.DataKeys(e.RowIndex).Value)
        Dim fullName As String = DirectCast(row.FindControl("txtFullName"), TextBox).Text
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
        Dim query As String = "UPDATE Students SET fullName = @fullName WHERE id = @id"

        Using connection As New SqlConnection(connectionString)
            Try
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@id", id)
                    command.Parameters.AddWithValue("@fullName", fullName)
                    connection.Open()
                    command.ExecuteNonQuery()
                End Using
            Catch ex As Exception
            End Try
        End Using

        studentsTable.EditIndex = -1
        BindGridView()
    End Sub

    Protected Sub studentsTable_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles studentsTable.RowDeleting
        Dim id As Integer = Convert.ToInt32(studentsTable.DataKeys(e.RowIndex).Value)

        ' Thực hiện xóa dữ liệu trong cơ sở dữ liệu ở đây
        ' Ví dụ:
        ' DELETE FROM Students WHERE id = @Id

        BindGridView()
    End Sub
End Class