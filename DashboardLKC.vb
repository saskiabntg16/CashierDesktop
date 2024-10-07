Public Class DashboardLKC
    Sub terkunci()
        LoginToolStripMenuItem.Enabled = True
        GantiPwdToolStripMenuItem1.Enabled = False
        KeluarToolStripMenuItem.Enabled = False
        MASTERDATAToolStripMenuItem.Enabled = False
        TRANSAKSIToolStripMenuItem.Enabled = False
        CETAKToolStripMenuItem.Enabled = False
        SS2.Text = ""
        SS4.Text = ""
    End Sub

    Private Sub DashboardLKC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call terkunci()
        SS6.Text = Today
    End Sub

    Private Sub MasterDataAdminToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterDataAdminToolStripMenuItem.Click
        MasterAdmin.ShowDialog()
    End Sub

    Private Sub MasterDataCustomerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterDataCustomerToolStripMenuItem.Click
        MasterPengirim.ShowDialog()
    End Sub

    Private Sub MasterDataBarangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterDataBarangToolStripMenuItem.Click
        MasterKirimBarang.ShowDialog()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        SS8.Text = TimeOfDay
    End Sub

    Private Sub PengirimanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PengirimanToolStripMenuItem.Click
        Pengiriman.ShowDialog()
    End Sub

    Private Sub StatusPengirimanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StatusPengirimanToolStripMenuItem.Click
        StatusPengiriman.ShowDialog()
    End Sub

    Private Sub DaftarHargaToolStripMenuItem_Click(sender As Object, e As EventArgs)
        DaftarHarga.ShowDialog()
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles LoginToolStripMenuItem.Click
        Login.ShowDialog()
    End Sub

    Private Sub ExitToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem1.Click
        Me.Close()
    End Sub

    Private Sub GantiPwdToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles GantiPwdToolStripMenuItem1.Click
        GantiPwdFile.ShowDialog()
    End Sub

    Private Sub KeluarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KeluarToolStripMenuItem.Click
        Call terkunci()
    End Sub

    Private Sub MasterPenerimaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterPenerimaToolStripMenuItem.Click
        MasterPenerima.ShowDialog()
    End Sub

    Private Sub DaftarHargaToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles DaftarHargaToolStripMenuItem.Click
        DaftarHarga.ShowDialog()
    End Sub

    Private Sub InvoiceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InvoiceToolStripMenuItem.Click
        Laporan.ShowDialog()
    End Sub
End Class
