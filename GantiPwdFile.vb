Imports MySql.Data.MySqlClient
Public Class GantiPwdFile
    Sub kondisiawal()
        TextBox2.Enabled = False
        TextBox3.Enabled = False
        TextBox1.Enabled = True
        TextBox1.Text = ""
        TextBox3.Text = ""
        TextBox2.Text = ""
        TextBox1.PasswordChar = "*"
        TextBox2.PasswordChar = "*"
        TextBox3.PasswordChar = "*"
    End Sub

    Private Sub GantiPwdFile_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call kondisiawal()
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            Call koneksi()
            cmd = New MySqlCommand("Select * from tbl_admin where usernameadmin = '" & DashboardLKC.SS2.Text & "' and pwdadmin = '" & TextBox1.Text & "'", con)
            rd = cmd.ExecuteReader
            rd.Read()
            If rd.HasRows Then
                TextBox3.Enabled = True
                TextBox2.Enabled = True
                TextBox3.Focus()
            Else
                MessageBox.Show("Password Lama Salah!", "")
                TextBox1.Text = ""
                TextBox1.Focus()
            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox3.Text = "" Or TextBox2.Text = "" Then
            MessageBox.Show("Password Baru Harus Dilengkapi!", "")
        Else
            If TextBox2.Text <> TextBox3.Text Then
                MessageBox.Show("Password Baru dan Konfirmasi Harus Sama!", "")
            Else
                Call koneksi()
                Dim updatedata As String = "Update tbl_admin set pwdadmin = '" & TextBox3.Text & "' where usernameadmin = '" & DashboardLKC.SS2.Text & "'"
                cmd = New MySqlCommand(updatedata, con)
                cmd.ExecuteNonQuery()
                MessageBox.Show("Password Berhasil di Update", "")
                Call kondisiawal()
            End If
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

End Class