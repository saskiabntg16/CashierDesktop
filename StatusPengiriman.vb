Imports MySql.Data.MySqlClient
Public Class StatusPengiriman
    Sub kondisiawal()
        TextBox1.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox7.Text = ""
        TextBox8.Text = ""
        TextBox2.Text = ""
        TextBox1.Enabled = True
        ComboBox1.Enabled = True

        TextBox1.Enabled = False
        TextBox3.Enabled = False
        TextBox4.Enabled = False
        TextBox5.Enabled = False
        TextBox7.Enabled = False
        TextBox8.Enabled = False
        TextBox2.Enabled = False
        ComboBox1.Enabled = False

        ComboBox1.Items.Clear()
        ComboBox1.Text = ""

        Button2.Enabled = True

        Button2.Text = "Update"
        Button4.Text = "Tutup"

        Call koneksi()
        da = New MySqlDataAdapter("Select noresi, tanggal, beratkirim, totalkirim, kodepengirim, kodepenerima, usernameadmin, statuskirim from tbl_pengiriman", con)
        ds = New DataSet
        da.Fill(ds, "tbl_pengiriman")
        DataGridView1.DataSource = ds.Tables("tbl_pengiriman")
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView1.ReadOnly = True
    End Sub
    Private Sub StatusPengiriman_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call kondisiawal()
    End Sub

    Private Sub TextBox9_TextChanged(sender As Object, e As EventArgs) Handles TextBox9.TextChanged
        da = New MySqlDataAdapter("select * from tbl_pengiriman where kodepenerima Like '%" & Me.TextBox9.Text & "%' or kodepengirim Like '%" & Me.TextBox9.Text & "%'", con)
        ds = New DataSet
        da.Fill(ds, "tbl_pengiriman")
        DataGridView1.DataSource = (ds.Tables("tbl_pengiriman"))
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Button2.Text = "Update" Then
            Button2.Text = "Simpan"
            Button4.Text = "Batal"
            ComboBox1.Items.Add("BELUM DIKIRIM")
            ComboBox1.Items.Add("SEDANG DIKIRIM")
            ComboBox1.Items.Add("TERKIRIM")
            TextBox1.Enabled = True
            ComboBox1.Enabled = True
        Else
            If TextBox1.Text = "" Or ComboBox1.Text = "" Or TextBox4.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox5.Text = "" Or TextBox8.Text = "" Then
                MessageBox.Show("Silahkan Isi Semua Field ", "")
            Else
                Call koneksi()
                Dim updatedata As String = "Update tbl_pengiriman set kodepengirim = '" & TextBox4.Text & "', kodepenerima = '" & TextBox2.Text & "', beratkirim = '" & TextBox3.Text & "', usernameadmin = '" & TextBox5.Text & "', totalkirim = '" & TextBox8.Text & "', statuskirim = '" & ComboBox1.Text & "' where noresi = '" & TextBox1.Text & "'"
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
            cmd = New MySqlCommand("Select * from tbl_pengiriman where noresi = '" & TextBox1.Text & "'", con)
            rd = cmd.ExecuteReader
            rd.Read()
            If Not rd.HasRows Then
                MessageBox.Show("Nomor Resi Tidak Ditemukan", "")
            Else
                TextBox1.Text = rd.Item("noresi")
                TextBox7.Text = rd.Item("tanggal")
                TextBox3.Text = rd.Item("beratkirim")
                TextBox8.Text = rd.Item("totalkirim")
                TextBox4.Text = rd.Item("kodepengirim")
                TextBox2.Text = rd.Item("kodepenerima")
                TextBox5.Text = rd.Item("usernameadmin")
                ComboBox1.Text = rd.Item("statuskirim")
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

End Class