Imports MySql.Data.MySqlClient
Public Class DaftarHarga

    Private Sub DaftarHarga_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call koneksi()

        da = New MySqlDataAdapter("Select * from tbl_harga", con)
        ds = New DataSet
        da.Fill(ds, "tbl_harga")
        DataGridView1.DataSource = ds.Tables("tbl_harga")
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView1.ReadOnly = True
    End Sub
End Class