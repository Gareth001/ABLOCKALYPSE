Public Class FormLevelCreator

    'Level creator variables
    Public Bitmap As Bitmap, Loaded As Boolean = False, Gravity As Single = 1, CellSize As Integer = 24, _
        Wrapped As Boolean = False, XSpeed As Integer = 4

    Private Sub BackToForm()

        'go back to form
        FormMain.Show()
        FormMain.Enabled = True
        FormMain.Focus()
        Me.Hide()

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        'go back to form with nothing loaded if cancel is pressed
        Loaded = False
        BackToForm()
    End Sub

    Private Sub btnChoose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChoose.Click

        Dim LoadLevel As New OpenFileDialog
        LoadLevel.Filter = "png|*.png"

        'if the player loads a file, set loaded to true, let the player load level and preview the image and its location
        If LoadLevel.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            btnLoad.Enabled = True
            lblFileName.Text = LoadLevel.FileName.ToString
            Loaded = True
            Bitmap = New Bitmap(Image.FromFile(LoadLevel.FileName.ToString))
            picPreview.Image = Bitmap
        End If

        LoadLevel.Dispose()

    End Sub

    Private Sub btnHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHelp.Click

        'show help
        MsgBox("Create a PNG file that contains the level layout." & vbCrLf & _
               "It can contain no more than 10002 non white pixels" & vbCrLf & _
               "Recommended size: 30*30" & vbCrLf & _
               "Remember that the player is the same size as the blocks" & vbCrLf & _
               "The colour of the pixels will decide what type of block it is." & vbCrLf & _
               "Special Blocks:" & vbCrLf & _
               "Player: R(255) G(0) B(0)" & vbCrLf & _
               "Exit: R(0) G(255) B(0)" & vbCrLf & _
               "Spikes: R(100) G(100) B(100)" & vbCrLf & _
               "Change gravity: R(255) G(0) B(255)" & vbCrLf & _
               "Turns all destrictuble blocks into Clear: R(0) G(255) B(255)" & vbCrLf & _
               "Turn all Clear blocks into destructible blocks: R(255) G(255) B(0)" & vbCrLf & _
               "Destructible: R(0) G(1) B(0)" & vbCrLf & _
                "Clear: R(255) G(254) B(255)" & vbCrLf & _
               "All other blocks will be considered to be normal solid blocks" & vbCrLf & _
               "Only one player will be created" & vbCrLf & _
               "If the level has an error, you will be sent to the menu" & vbCrLf & _
               "Press escape if you 'accidentally' forget to put in an exit" & vbCrLf & _
               "For best effect, ensure that the Xspeed is less than and a factor of the Cellsize" & vbCrLf & _
               "Creating a level that is bigger than the resolution of your screen is a bad idea."
               )

    End Sub

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        'go back to form with level loaded if load is pressed
        Loaded = True
        BackToForm()
    End Sub

    Private Sub NumericUpDown1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nupGravity.ValueChanged
        'update gravity
        Gravity = nupGravity.Value
    End Sub

    Private Sub nupCellSize_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nupCellSize.ValueChanged
        'update cellsize
        CellSize = nupCellSize.Value
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkWrap.CheckedChanged
        'update wrapped
        Wrapped = chkWrap.Checked
    End Sub

    Private Sub NumericUpDown1_ValueChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nupXSpd.ValueChanged
        'update xspeed
        XSpeed = nupXSpd.Value
    End Sub

    Private Sub FormLevelCreator_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'center form when its loaded
        Me.Location = New Point((Screen.PrimaryScreen.Bounds.Width / 2) - (Me.Width / 2), _
                        (Screen.PrimaryScreen.Bounds.Height / 2) - (Me.Height / 2))
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click

        'reset all variables
        btnLoad.Enabled = False
        lblFileName.Text = "No Level Loaded"
        Loaded = False
        Bitmap = Nothing
        picPreview.Image = Nothing
        Gravity = 1
        nupGravity.Value = Gravity
        CellSize = 24
        nupCellSize.Value = CellSize
        Wrapped = False
        chkWrap.Checked = Wrapped
        XSpeed = 4
        nupXSpd.Value = XSpeed

    End Sub

End Class