Imports System.Security.Cryptography
Imports Core.modules
Imports Core.utils

Public Class loginForm
    Dim user As New User
    Dim md5 As New Core.utils.MD5
    'Dim frmKeyReset As New keyReset
    Private Sub loginForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.BackColor = ColorTranslator.FromHtml(APPConfig.FormBG)
        'Me.Select()
        'BTN_LOGIN.BackColor = ColorTranslator.FromHtml(APPConfig.MPBlueColor)
        'BTN_CANCEL.BackColor = ColorTranslator.FromHtml(APPConfig.MPRedColor)
        'BTN_LOGIN.ForeColor = ColorTranslator.FromHtml(APPConfig.MPWhiteColor)
        'BTN_CANCEL.ForeColor = ColorTranslator.FromHtml(APPConfig.MPWhiteColor)
        TXT_USERNAME.Select()

        'TODO: Acceso Temporal para desarrollo'
        'acceso temporal para desarrollo
        'TXT_USERNAME.Text = "Segura"
        'TXT_PWD.Text = "1234"
        'TXT_USERNAME.Select()
    End Sub

    Private Sub loginForm_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown ' PONER TECLAS DE ACCESO RAPIDO
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub BTN_CANCEL_Click(sender As Object, e As EventArgs) Handles BTN_CANCEL.Click
        Me.Close()
    End Sub

    Private Sub loginForm_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Me.TopMost = True
    End Sub

    Private Sub loginForm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        Me.TopMost = True
    End Sub

    Private Sub BTN_LOGIN_Click(sender As Object, e As EventArgs) Handles BTN_LOGIN.Click
        logIn()
    End Sub
    Private Sub logIn()

        Dim passEncrypt As String = MD5.EncryptText(TXT_PWD.Text.Trim)
        Dim username As String = TXT_USERNAME.Text.Trim

        If User.verifyAccess(username, passEncrypt) Then
            If (User.loggedUser.Item("user_temp_pwd") <> "") Then
                'frmKeyReset.idUser = User.loggedUser.Item("id_user")
                'frmKeyReset.txtNewPassword.Text = ""
                'frmKeyReset.txtConfirmPassword.Text = ""
                'frmKeyReset.ShowDialog()
                TXT_PWD.Text = ""
            Else
                User.checkPermissions(User.loggedUser.Item("id_role"))
                'CType(Me.Parent, mainFrame).validatePermissions()

                MsgBox("Ha ingresado correctamente", MsgBoxStyle.Critical)
                Me.Close()
            End If
            User.errorCountSet(User.loggedUser.Item("user_username"))
        End If
    End Sub

    Private Sub TXT_PWD_TextChanged(sender As Object, e As EventArgs) Handles TXT_PWD.TextChanged
        If TXT_PWD.TextLength >= 4 Then
            BTN_LOGIN.Enabled = True
        Else
            BTN_LOGIN.Enabled = False
        End If

    End Sub

    Private Sub TXT_USERNAME_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXT_USERNAME.KeyPress
        'If globalVariables.regexUserName.IsMatch(e.KeyChar) Then
        '    e.Handled = False
        'Else
        '    e.Handled = True
        'End If
    End Sub

    Private Sub BTN_LOGIN_KeyUp(sender As Object, e As KeyEventArgs) Handles BTN_LOGIN.KeyUp

    End Sub

    Private Sub BTN_LOGIN_KeyDown(sender As Object, e As KeyEventArgs) Handles BTN_LOGIN.KeyDown
        If e.KeyCode = Keys.Enter Then
            BTN_LOGIN.PerformClick()
        End If
    End Sub

    'Private Sub BTN_LOGIN_Enter(sender As Object, e As EventArgs) Handles BTN_LOGIN.Enter
    '   logIn()
    'End Sub
End Class