
Imports System.Data
Imports System.Data.SqlClient

Partial Class _Default
    Inherits Page


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        GetDataFromDatabase()
    End Sub
    Private Sub GetDataFromDatabase()

        Dim connectionString As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
        Dim query As String = "SELECT * FROM students"
        Dim dataTable As New DataTable()
        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(query, connection)
                Try
                    connection.Open()
                    Dim reader As SqlDataReader = command.ExecuteReader()
                    If reader.HasRows Then
                        dataTable.Columns.Add("ID", GetType(Integer))
                        dataTable.Columns.Add("FullName", GetType(String))
                        While reader.Read()
                            Dim id As Integer = reader.GetInt32(0)
                            Dim fullName As String = reader.GetString(1)
                            dataTable.Rows.Add(id, fullName)
                        End While
                    End If
                    reader.Close()
                Catch ex As Exception
                End Try
            End Using
        End Using
        gridView1.DataSource = dataTable
        gridView1.DataBind()
    End Sub
End Class