Imports MySql.Data.MySqlClient
Public Class Pengiriman
    Dim tglmysql As String
    Sub kondisiawal()
        Label6.Text = ""
        Label7.Text = ""
        Label8.Text = ""
        Label9.Text = ""
        Label15.Text = Today
        Label13.Text = DashboardLKC.SS2.Text
        Label18.Text = ""
        Label16.Text = ""
        Label19.Text = ""
        TextBox2.Text = ""
        Label24.Text = ""
        TextBox3.Text = ""
        TextBox1.Text = ""
        Label34.Text = ""
        Label29.Text = ""
        Label35.Text = ""
        Label40.Text = ""
        Label38.Text = ""
        ComboBox1.Text = ""
        ComboBox2.Text = ""
        ComboBox3.Text = ""
        TextBox3.Enabled = False
        Label28.Text = ""
        Call kodepengirim()
        Call kodepenerima()
        Call otomatis()
        Call kolom()
        Label16.Text = "0"
        ComboBox3.Items.Clear()
        ComboBox3.Items.Add("BELUM DIKIRIM")
        ComboBox3.Items.Add("SEDANG DIKIRIM")
        ComboBox3.Items.Add("TERKIRIM")
    End Sub

    Private Sub FormTransaksiPenjualan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call kondisiawal()
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label14.Text = TimeOfDay
    End Sub

    Sub kodepenerima()
        Call koneksi()
        ComboBox2.Items.Clear()
        cmd = New MySqlCommand("select * from tbl_customer", con)
        rd = cmd.ExecuteReader
        Do While rd.Read
            ComboBox2.Items.Add(rd.Item(0))
        Loop
    End Sub

    Sub kodepengirim()
        Call koneksi()
        ComboBox1.Items.Clear()
        cmd = New MySqlCommand("select * from tbl_penerima", con)
        rd = cmd.ExecuteReader
        Do While rd.Read
            ComboBox1.Items.Add(rd.Item(0))
        Loop
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Call koneksi()
        cmd = New MySqlCommand("select * from tbl_penerima where kodepengirim = '" & ComboBox1.Text & "'", con)
        rd = cmd.ExecuteReader
        rd.Read()
        If rd.HasRows Then
            Label7.Text = rd!namausr
            Label8.Text = rd!alamatusr
            Label9.Text = rd!telpusr
        End If
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        Call koneksi()
        cmd = New MySqlCommand("select * from tbl_customer where kodepenerima = '" & ComboBox2.Text & "'", con)
        rd = cmd.ExecuteReader
        rd.Read()
        If rd.HasRows Then
            Label34.Text = rd!namacust
            Label29.Text = rd!alamatcust
            Label35.Text = rd!telpcust
        End If
    End Sub

    Sub otomatis()
        Call koneksi()
        cmd = New MySqlCommand("select * from tbl_pengiriman where noresi in (select max(noresi) from tbl_pengiriman)", con)
        Dim kodeurut As String
        Dim hitung As Long
        rd = cmd.ExecuteReader
        rd.Read()
        If Not rd.HasRows Then
            kodeurut = "L" + Format(Now, "yyMMdd") + "001"
        Else
            hitung = Microsoft.VisualBasic.Right(rd.GetString(0), 9) + 1
            kodeurut = "L" + Format(Now, "yyMMdd") + Microsoft.VisualBasic.Right("000" & hitung, 3)
        End If
        Label6.Text = kodeurut
    End Sub

    Sub kolom()
        DataGridView1.Columns.Clear()
        DataGridView1.Columns.Add("Kode", "Kode Kirim")
        DataGridView1.Columns.Add("Nama", "Nama Barang")
        DataGridView1.Columns.Add("Jenis", "Jenis Barang")
        DataGridView1.Columns.Add("Tujuan", "Kota Tujuan")
        DataGridView1.Columns.Add("Harga", "Harga Kirim")
        DataGridView1.Columns.Add("Berat", "Berat Barang (/Kg)")
        DataGridView1.Columns.Add("Total", "Sub Total")
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Chr(13) Then
            Call koneksi()
            cmd = New MySqlCommand("Select * from tbl_barang where kodebarang = '" & TextBox2.Text & "'", con)
            rd = cmd.ExecuteReader
            rd.Read()
            If Not rd.HasRows Then
                MessageBox.Show("Kode Kirim Barang Tidak Ditemukan! ", "")
                TextBox2.Text = ""
            Else
                TextBox2.Text = rd.Item("kodebarang")
                Label19.Text = rd.Item("namabarang")
                Label40.Text = rd.Item("jenisbarang")
                Label38.Text = rd.Item("kotatujuan")
                Label24.Text = rd.Item("hargakirim")
                TextBox3.Enabled = True
            End If
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If TextBox2.Text = "" Or Label19.Text = "" Or Label38.Text = "" Or Label24.Text = "" Or TextBox3.Text = "" Then
            MessageBox.Show("Masukkan Kode Kirim Barang Lalu Tekan Enter!", "")
        ElseIf Val(TextBox3.Text) < "150" Then
            MessageBox.Show("Berat Barang Minimal 150kg!", "")
        Else DataGridView1.Rows.Add(New String() {TextBox2.Text, Label19.Text, Label40.Text, Label38.Text, Label24.Text, TextBox3.Text, Val(Label24.Text) * Val(TextBox3.Text)})
            Call total()
            Call item()
        End If

        TextBox2.Text = ""
        TextBox3.Text = ""
    End Sub

    Sub total()
        Dim hitung As Integer = 0
        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            hitung = hitung + DataGridView1.Rows(i).Cells(6).Value
            Label16.Text = hitung
        Next
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            If Val(TextBox1.Text) < Val(Label16.Text) Then
                MsgBox("Nominal Bayar Kurang, Masukkan Nominal yang Sesuai! ")
            ElseIf Val(TextBox1.Text) = Val(Label16.Text) Then
                Label18.Text = "0"
            ElseIf Val(TextBox1.Text) > Val(Label16.Text) Then
                Label18.Text = Val(TextBox1.Text) - Val(Label16.Text)
                Button1.Focus()
            End If
        End If
    End Sub

    Sub item()
        Dim item As Integer = 0
        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            item = item + DataGridView1.Rows(i).Cells(5).Value
            Label28.Text = item
        Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Label18.Text = "" Or Label7.Text = "" Or Label16.Text = "" Or ComboBox3.Text = "" Then
            MessageBox.Show("Transaksi Tidak Dapat Dilanjutkan, Isi Semua Field!", "")
        Else
            tglmysql = Format(Today, "yy-MM-dd")
            Dim simpankirim As String = "insert into tbl_pengiriman values('" & Label6.Text & "', '" & tglmysql & "', '" & Label14.Text & "', '" & Label28.Text & "', '" & Label16.Text & "', '" & TextBox1.Text & "', '" & Label18.Text & "', '" & ComboBox1.Text & "', '" & ComboBox2.Text & "', '" & Label13.Text & "',  '" & ComboBox3.Text & "')"
            cmd = New MySqlCommand(simpankirim, con)
            rd.Close()
            cmd.ExecuteNonQuery()

            For Baris As Integer = 0 To DataGridView1.Rows.Count - 2
                Dim SimpanDetail As String = "Insert into tbl_detailkirim values('" & Label6.Text & "', '" & DataGridView1.Rows(Baris).Cells(0).Value & "', '" & DataGridView1.Rows(Baris).Cells(3).Value & "', '" & DataGridView1.Rows(Baris).Cells(4).Value & "', '" & DataGridView1.Rows(Baris).Cells(5).Value & "', '" & DataGridView1.Rows(Baris).Cells(6).Value & "')"
                cmd = New MySqlCommand(SimpanDetail, con)
                rd.Close()
                cmd.ExecuteNonQuery()

                cmd = New MySqlCommand("Select * from tbl_barang where kodebarang='" & DataGridView1.Rows(Baris).Cells(0).Value & "'", con)
                rd = cmd.ExecuteReader
                rd.Read()
            Next
            If MessageBox.Show("Cetak Bukti Transaksi?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                AxCrystalReport1.SelectionFormula = "totext({tbl_Pengiriman.NoResi})='" & Label6.Text & "'"
                AxCrystalReport1.ReportFileName = "notawal.rpt"
                AxCrystalReport1.WindowState = Crystal.WindowStateConstants.crptMaximized
                AxCrystalReport1.RetrieveDataFiles()
                AxCrystalReport1.Action = 1
                Call kondisiawal()
            Else
                Call kondisiawal()
                MessageBox.Show("Transaksi Berhasil Disimpan!", "")
            End If
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Call kondisiawal()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
    End Sub
End Class