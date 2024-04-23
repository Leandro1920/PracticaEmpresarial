<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class loginForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BTN_CANCEL = New System.Windows.Forms.Button()
        Me.BTN_LOGIN = New System.Windows.Forms.Button()
        Me.TXT_PWD = New System.Windows.Forms.TextBox()
        Me.TXT_USERNAME = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(302, 113)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(195, 29)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "Inicio de sesión"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(254, 263)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 16)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "Clave"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(254, 212)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 16)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "Usuario"
        '
        'BTN_CANCEL
        '
        Me.BTN_CANCEL.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold)
        Me.BTN_CANCEL.ForeColor = System.Drawing.SystemColors.ControlText
        Me.BTN_CANCEL.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BTN_CANCEL.Location = New System.Drawing.Point(320, 366)
        Me.BTN_CANCEL.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_CANCEL.Name = "BTN_CANCEL"
        Me.BTN_CANCEL.Size = New System.Drawing.Size(151, 28)
        Me.BTN_CANCEL.TabIndex = 12
        Me.BTN_CANCEL.Text = "Cancelar [ESC]"
        Me.BTN_CANCEL.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BTN_CANCEL.UseVisualStyleBackColor = True
        '
        'BTN_LOGIN
        '
        Me.BTN_LOGIN.Enabled = False
        Me.BTN_LOGIN.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold)
        Me.BTN_LOGIN.ForeColor = System.Drawing.SystemColors.ControlText
        Me.BTN_LOGIN.Location = New System.Drawing.Point(320, 331)
        Me.BTN_LOGIN.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_LOGIN.Name = "BTN_LOGIN"
        Me.BTN_LOGIN.Size = New System.Drawing.Size(151, 28)
        Me.BTN_LOGIN.TabIndex = 11
        Me.BTN_LOGIN.Text = "Iniciar sesión"
        Me.BTN_LOGIN.UseVisualStyleBackColor = True
        '
        'TXT_PWD
        '
        Me.TXT_PWD.Location = New System.Drawing.Point(258, 287)
        Me.TXT_PWD.Margin = New System.Windows.Forms.Padding(4)
        Me.TXT_PWD.Name = "TXT_PWD"
        Me.TXT_PWD.Size = New System.Drawing.Size(288, 20)
        Me.TXT_PWD.TabIndex = 10
        '
        'TXT_USERNAME
        '
        Me.TXT_USERNAME.Location = New System.Drawing.Point(258, 237)
        Me.TXT_USERNAME.Margin = New System.Windows.Forms.Padding(4)
        Me.TXT_USERNAME.Name = "TXT_USERNAME"
        Me.TXT_USERNAME.Size = New System.Drawing.Size(288, 20)
        Me.TXT_USERNAME.TabIndex = 9
        '
        'loginForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.BTN_CANCEL)
        Me.Controls.Add(Me.BTN_LOGIN)
        Me.Controls.Add(Me.TXT_PWD)
        Me.Controls.Add(Me.TXT_USERNAME)
        Me.Name = "loginForm"
        Me.Text = "loginForm"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents BTN_CANCEL As Button
    Friend WithEvents BTN_LOGIN As Button
    Friend WithEvents TXT_PWD As TextBox
    Friend WithEvents TXT_USERNAME As TextBox
End Class
