Public Class Player

    'player variables
    Public X As Integer, Y As Integer, CellSize As Integer, Col As Color = FormMain.plrCol
    Public JumpTime As Integer = 0, Grounded As Boolean = True, CanJump As Boolean = False
    Public vsp As Single = 0, grav As Single = 1, Xspeed As Integer = 4

    Sub New(ByVal XPos As Integer, ByVal YPos As Integer, ByVal Size As Integer, ByVal Gravity As Single, ByVal Xspd As Integer)

        'set player variables
        X = XPos
        Y = YPos
        CellSize = Size
        grav = Gravity
        Xspeed = Xspd

    End Sub

    Public Sub Dispose()
        'dispose player
        Me.Finalize()
    End Sub

    Protected Overrides Sub Finalize()

        'reset varibles
        X = Nothing
        Y = Nothing
        CellSize = Nothing
        CanJump = Nothing
        grav = Nothing
        Xspeed = Nothing
        MyBase.Finalize()

    End Sub

End Class
