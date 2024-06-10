
Imports System.Data
Imports System.Data.SqlClient

Partial Class _Default
    Inherits Page


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        BindGridView()
    End Sub
    Private Sub BindGridView()

        Dim connectionString As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
        Dim query As String = "SELECT * FROM students"
        Dim dataTable As New DataTable()
        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(query, connection)
                Try
                    connection.Open()
                    Dim reader As SqlDataReader = command.ExecuteReader()
                    dataTable.Columns.Add("ID", GetType(Integer))
                    dataTable.Columns.Add("FullName", GetType(String))
                    If reader.HasRows Then
                        While reader.Read()
                            Dim id As Integer = 1
                            Dim fullName As String = reader.GetString(1)
                            dataTable.Rows.Add(id, fullName)
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
        Dim newName As String = DirectCast(row.FindControl("txtName"), TextBox).Text

        ' Thực hiện cập nhật dữ liệu trong cơ sở dữ liệu ở đây
        ' Ví dụ:
        ' UPDATE Students SET fullName = @NewName WHERE id = @Id

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