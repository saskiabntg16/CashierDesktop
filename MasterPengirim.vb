Imports MySql.Data.MySqlClient
Public Class MasterPengirim
    Sub kondisiawal()
        TextBox1.Text = ""
        TextBox4.Text = ""
        TextBox3.Text = ""
        TextBox5.Text = ""
        TextBox1.Enabled = False
        TextBox4.Enabled = False
        TextBox3.Enabled = False
        TextBox5.Enabled = False

        Button1.Enabled = True
        Button2.Enabled = True
        Button3.Enabled = True

        Button1.Text = "Input"
        Button2.Text = "Edit"
        Button3.Text = "Hapus"
        Button4.Text = "Tutup"
        Call koneksi()
        da = New MySqlDataAdapter("Select * from tbl_penerima", con)
        ds = New DataSet
        da.Fill(ds, "tbl_penerima")
        DataGridView2.DataSource = ds.Tables("tbl_penerima")
        DataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView2.ReadOnly = True
    End Sub

    Sub isi()
        TextBox1.Enabled = True
        TextBox4.Enabled = True
        TextBox3.Enabled = True
        TextBox5.Enabled = True
    End Sub

    Private Sub MasterPengirim_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call kondisiawal()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Button1.Text = "Input" Then
            Button1.Text = "Simpan"
            Button2.Enabled = False
            Button3.Enabled = False
            Button4.Text = "Batal"
            Call isi()
            TextBox1.Enabled = False
            TextBox4.Focus()
            Call otomatis()
        Else
            If TextBox1.Text = "" Or TextBox4.Text = "" Or TextBox3.Text = "" Or TextBox5.Text = "" Then
                MessageBox.Show("Silahkan Isi Semua Field ", "")
            Else
                Call koneksi()
                Dim inputdata As String = "insert into tbl_penerima values ('" & TextBox1.Text & "','" & TextBox4.Text & "','" & TextBox3.Text & "','" & TextBox5.Text & "')"
                cmd = New MySqlCommand(inputdata, con)
                cmd.ExecuteNonQuery()
                MessageBox.Show("Input Data Berhasil", "")
                Call kondisiawal()
            End If
        End If

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        da = New MySqlDataAdapter("select * from tbl_penerima where namausr like '%" & Me.TextBox2.Text & "%'", con)
        ds = New DataSet
        da.Fill(ds, "tbl_penerima")
        DataGridView2.DataSource = (ds.Tables("tbl_penerima"))
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Button2.Text = "Edit" Then
            Button2.Text = "Simpan"
            Button1.Enabled = False
            Button3.Enabled = False
            Button4.Text = "Batal"
            Call isi()
        Else
            If TextBox1.Text = "" Or TextBox4.Text = "" Or TextBox3.Text = "" Or TextBox5.Text = "" Then
                MessageBox.Show("Silahkan Isi Semua Field ", "")
            Else
                Call koneksi()
                Dim updatedata As String = "Update tbl_penerima set namausr = '" & TextBox4.Text & "', alamatusr = '" & TextBox3.Text & "', telpusr = '" & TextBox5.Text & "' where kodepengirim = '" & TextBox1.Text & "'"
                cmd = New MySqlCommand(updatedata, con)
                cmd.ExecuteNonQuery()
                MessageBox.Show("Update Data Berhasil", "")
                Call kondisiawal()
            End If
        End If

    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            Call koneksi()
            cmd = New MySqlCommand("Select * from tbl_penerima where kodepengirim = '" & TextBox1.Text & "'", con)
            rd = cmd.ExecuteReader
            rd.Read()
            If Not rd.HasRows Then
                MessageBox.Show("Kode penerima Tidak Ditemukan", "")
            Else
                TextBox1.Text = rd.Item("kodepengirim")
                TextBox4.Text = rd.Item("namausr")
                TextBox3.Text = rd.Item("alamatusr")
                TextBox5.Text = rd.Item("telpusr")
            End If
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If Button4.Text = "Tutup" Then
            Me.Close()
        Else
            Call kondisiawal()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Button3.Text = "Hapus" Then
            Button3.Text = "Delete"
            Button1.Enabled = False
            Button2.Enabled = False
            Button4.Text = "Batal"
            Call isi()
        Else
            If TextBox1.Text = "" Or TextBox4.Text = "" Or TextBox3.Text = "" Or TextBox5.Text = "" Then
                MessageBox.Show("Silahkan Isi Semua Field ", "")
            Else
                Call koneksi()
                Dim hapusdata As String = "Delete from tbl_penerima where kodepengirim = '" & TextBox1.Text & "'"
                cmd = New MySqlCommand(hapusdata, con)
                cmd.ExecuteNonQuery()
                MsgBox("Hapus Data Berhasil")
                Call kondisiawal()
            End If
        End If
    End Sub

    Sub otomatis()
        Call koneksi()
        cmd = New MySqlCommand("select * from tbl_penerima where kodepengirim in (select max(kodepengirim) from tbl_penerima)", con)
        Dim kodeurut As String
        Dim hitung As Long
        rd = cmd.ExecuteReader
        rd.Read()
        If Not rd.HasRows Then
            kodeurut = "USR" + "001"
        Else
            hitung = Microsoft.VisualBasic.Right(rd.GetString(0), 3) + 1
            kodeurut = "USR" + Microsoft.VisualBasic.Right("000" & hitung, 3)
        End If
        TextBox1.Text = kodeurut
    End Sub

End Class