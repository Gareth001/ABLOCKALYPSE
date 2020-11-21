<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormMain))
        Me.GameTimer = New System.Windows.Forms.Timer(Me.components)
        Me.LoadPic = New System.Windows.Forms.PictureBox()
        CType(Me.LoadPic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GameTimer
        '
        Me.GameTimer.Interval = 17
        '
        'LoadPic
        '
        Me.LoadPic.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LoadPic.Image = Global.ABLOCKALYPSE.My.Resources.Resources.LoadScreen
        Me.LoadPic.Location = New System.Drawing.Point(0, 0)
        Me.LoadPic.Name = "LoadPic"
        Me.LoadPic.Size = New System.Drawing.Size(401, 340)
        Me.LoadPic.TabIndex = 0
        Me.LoadPic.TabStop = False
        '
        'FormMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(401, 340)
        Me.Controls.Add(Me.LoadPic)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FormMain"
        Me.Text = "ABLOCKALYPSE ☣"
        CType(Me.LoadPic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GameTimer As System.Windows.Forms.Timer
    Friend WithEvents LoadPic As System.Windows.Forms.PictureBox

End Class
