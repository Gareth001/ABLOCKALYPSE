<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormLevelCreator
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormLevelCreator))
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnChoose = New System.Windows.Forms.Button()
        Me.lblFileName = New System.Windows.Forms.Label()
        Me.btnLoad = New System.Windows.Forms.Button()
        Me.btnHelp = New System.Windows.Forms.Button()
        Me.lblGravity = New System.Windows.Forms.Label()
        Me.nupGravity = New System.Windows.Forms.NumericUpDown()
        Me.lblCellSize = New System.Windows.Forms.Label()
        Me.nupCellSize = New System.Windows.Forms.NumericUpDown()
        Me.chkWrap = New System.Windows.Forms.CheckBox()
        Me.picPreview = New System.Windows.Forms.PictureBox()
        Me.lblXsp = New System.Windows.Forms.Label()
        Me.nupXSpd = New System.Windows.Forms.NumericUpDown()
        Me.btnReset = New System.Windows.Forms.Button()
        CType(Me.nupGravity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nupCellSize, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picPreview, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nupXSpd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(12, 12)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(71, 23)
        Me.btnCancel.TabIndex = 0
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnChoose
        '
        Me.btnChoose.Location = New System.Drawing.Point(89, 12)
        Me.btnChoose.Name = "btnChoose"
        Me.btnChoose.Size = New System.Drawing.Size(75, 23)
        Me.btnChoose.TabIndex = 1
        Me.btnChoose.Text = "Choose File"
        Me.btnChoose.UseVisualStyleBackColor = True
        '
        'lblFileName
        '
        Me.lblFileName.Location = New System.Drawing.Point(9, 38)
        Me.lblFileName.Name = "lblFileName"
        Me.lblFileName.Size = New System.Drawing.Size(414, 27)
        Me.lblFileName.TabIndex = 2
        Me.lblFileName.Text = "No Level Loaded"
        '
        'btnLoad
        '
        Me.btnLoad.Enabled = False
        Me.btnLoad.Location = New System.Drawing.Point(170, 12)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(75, 23)
        Me.btnLoad.TabIndex = 2
        Me.btnLoad.Text = "Play Level"
        Me.btnLoad.UseVisualStyleBackColor = True
        '
        'btnHelp
        '
        Me.btnHelp.Location = New System.Drawing.Point(332, 12)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(75, 23)
        Me.btnHelp.TabIndex = 4
        Me.btnHelp.Text = "HELP ME!"
        Me.btnHelp.UseVisualStyleBackColor = True
        '
        'lblGravity
        '
        Me.lblGravity.AutoSize = True
        Me.lblGravity.Location = New System.Drawing.Point(12, 70)
        Me.lblGravity.Name = "lblGravity"
        Me.lblGravity.Size = New System.Drawing.Size(46, 13)
        Me.lblGravity.TabIndex = 6
        Me.lblGravity.Text = "Gravity: "
        '
        'nupGravity
        '
        Me.nupGravity.DecimalPlaces = 3
        Me.nupGravity.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.nupGravity.Location = New System.Drawing.Point(53, 68)
        Me.nupGravity.Minimum = New Decimal(New Integer() {1, 0, 0, 196608})
        Me.nupGravity.Name = "nupGravity"
        Me.nupGravity.Size = New System.Drawing.Size(51, 20)
        Me.nupGravity.TabIndex = 5
        Me.nupGravity.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'lblCellSize
        '
        Me.lblCellSize.AutoSize = True
        Me.lblCellSize.Location = New System.Drawing.Point(110, 70)
        Me.lblCellSize.Name = "lblCellSize"
        Me.lblCellSize.Size = New System.Drawing.Size(77, 13)
        Me.lblCellSize.TabIndex = 8
        Me.lblCellSize.Text = "Size of Blocks:"
        '
        'nupCellSize
        '
        Me.nupCellSize.Increment = New Decimal(New Integer() {4, 0, 0, 0})
        Me.nupCellSize.Location = New System.Drawing.Point(184, 68)
        Me.nupCellSize.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nupCellSize.Name = "nupCellSize"
        Me.nupCellSize.Size = New System.Drawing.Size(51, 20)
        Me.nupCellSize.TabIndex = 6
        Me.nupCellSize.Value = New Decimal(New Integer() {24, 0, 0, 0})
        '
        'chkWrap
        '
        Me.chkWrap.AutoSize = True
        Me.chkWrap.Location = New System.Drawing.Point(343, 68)
        Me.chkWrap.Name = "chkWrap"
        Me.chkWrap.Size = New System.Drawing.Size(89, 17)
        Me.chkWrap.TabIndex = 8
        Me.chkWrap.Text = "Wrap Screen"
        Me.chkWrap.UseVisualStyleBackColor = True
        '
        'picPreview
        '
        Me.picPreview.Location = New System.Drawing.Point(12, 99)
        Me.picPreview.Name = "picPreview"
        Me.picPreview.Size = New System.Drawing.Size(399, 213)
        Me.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picPreview.TabIndex = 4
        Me.picPreview.TabStop = False
        '
        'lblXsp
        '
        Me.lblXsp.AutoSize = True
        Me.lblXsp.Location = New System.Drawing.Point(241, 70)
        Me.lblXsp.Name = "lblXsp"
        Me.lblXsp.Size = New System.Drawing.Size(48, 13)
        Me.lblXsp.TabIndex = 12
        Me.lblXsp.Text = "XSpeed:"
        '
        'nupXSpd
        '
        Me.nupXSpd.Increment = New Decimal(New Integer() {4, 0, 0, 0})
        Me.nupXSpd.Location = New System.Drawing.Point(286, 67)
        Me.nupXSpd.Name = "nupXSpd"
        Me.nupXSpd.Size = New System.Drawing.Size(51, 20)
        Me.nupXSpd.TabIndex = 7
        Me.nupXSpd.Value = New Decimal(New Integer() {4, 0, 0, 0})
        '
        'btnReset
        '
        Me.btnReset.Location = New System.Drawing.Point(251, 12)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(75, 23)
        Me.btnReset.TabIndex = 3
        Me.btnReset.Text = "Reset"
        Me.btnReset.UseVisualStyleBackColor = True
        '
        'FormLevelCreator
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(436, 332)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnReset)
        Me.Controls.Add(Me.nupXSpd)
        Me.Controls.Add(Me.lblXsp)
        Me.Controls.Add(Me.chkWrap)
        Me.Controls.Add(Me.nupCellSize)
        Me.Controls.Add(Me.lblCellSize)
        Me.Controls.Add(Me.nupGravity)
        Me.Controls.Add(Me.lblGravity)
        Me.Controls.Add(Me.btnHelp)
        Me.Controls.Add(Me.picPreview)
        Me.Controls.Add(Me.btnLoad)
        Me.Controls.Add(Me.lblFileName)
        Me.Controls.Add(Me.btnChoose)
        Me.Controls.Add(Me.btnCancel)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FormLevelCreator"
        Me.Text = "ABLOCKALYPSE Level Creator"
        CType(Me.nupGravity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nupCellSize, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picPreview, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nupXSpd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnChoose As System.Windows.Forms.Button
    Friend WithEvents lblFileName As System.Windows.Forms.Label
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents picPreview As System.Windows.Forms.PictureBox
    Friend WithEvents btnHelp As System.Windows.Forms.Button
    Friend WithEvents lblGravity As System.Windows.Forms.Label
    Friend WithEvents nupGravity As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblCellSize As System.Windows.Forms.Label
    Friend WithEvents nupCellSize As System.Windows.Forms.NumericUpDown
    Friend WithEvents chkWrap As System.Windows.Forms.CheckBox
    Friend WithEvents lblXsp As System.Windows.Forms.Label
    Friend WithEvents nupXSpd As System.Windows.Forms.NumericUpDown
    Friend WithEvents btnReset As System.Windows.Forms.Button
End Class
