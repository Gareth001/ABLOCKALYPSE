'THIS CODE WAS WRITTEN BY GARETH BOOTH. DANIEL VAN DRIEL DID NOT CODE ANYTHING AT ALL.

Public Class FormMain

    'declare class instances
    Public Block(10000) As Block, Blocks As Integer = 0
    Public Player As Player
    Dim Level As Level

    'declare varibales
    Dim tSec As Integer, tTicks As Integer, totalTicks As Integer, Time As Integer = 0, FPS As Integer
    Dim CurrentLevel As Integer, LevelUnlocked As Integer = 1, OpacityLoad As Boolean = True, Attempts As Integer = 0
    Dim Status As String = "Start"
    Public Trippy As Boolean = False, Center As Boolean = True, Debug As Boolean = False, PlayerImage As Boolean = False, PlrImg As Image, _
        plrCol As Color = Color.Red
    Dim MenuButtons(4) As String, sel As Integer = 0, lsel As Integer = 0, Buttons = 4, hlp As Integer, hacks As Boolean = False
    Dim Keys(2) As Boolean, Mouse As Boolean, MouseX As Integer, MouseY As Integer

    'declare graphic related variables
    Dim StringFormat As New StringFormat, Fnt As Font
    Dim G As Graphics, BBG As Graphics, BB As Bitmap
    Public WindowWidth As Integer = 6, WindowHeight As Integer = 28

    'scores
    Dim Scores As New List(Of String)
    Dim dir = Environment.GetEnvironmentVariable("UserProfile") & "\AppData\Roaming\Adobe\Color\", save = "Data.dat"

    Private Sub FormMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'set all keys to false
        Keys(0) = False 'W
        Keys(1) = False 'A
        Keys(2) = False 'D

        'set mouse to false
        Mouse = False

        'set the  curent level 
        CurrentLevel = 0 ': GameTimer.Interval = 16

        'button text
        MenuButtons(0) = "Start"
        MenuButtons(1) = "Custom Level"
        MenuButtons(2) = "Help Me!"
        MenuButtons(3) = "Scores"
        MenuButtons(4) = "Exit"

        'width and height of the form at the menu
        Me.Width = 840 + WindowWidth
        Me.Height = 540 + WindowHeight

        'reset graphics
        GraphicsReset()

        'set font
        StringFormat.Alignment = StringAlignment.Far
        Fnt = New Font("Comic Sans MS", 15)

        'Load data
        DataLoad()

        'center and bring focous
        CenterForm()
        Me.Show()
        Me.Focus()

    End Sub
    Private Sub GameStart(ByVal Lvl As Integer)

        'change form opacity if the setting is true
        If OpacityLoad = True Then
            G.Clear(Color.White)
            Me.Opacity = 1
        End If

        'reset FPS varables and string format
        FPS = 0 : tSec = 0 : tTicks = 0 : totalTicks = 0
        StringFormat.Alignment = StringAlignment.Far

        'change status and current level
        Status = "Game" : CurrentLevel = Lvl
        If Lvl >= 0 Then
            'if its not a custom level goto the builtin level
            LevelGoto(Lvl)
        ElseIf Lvl = -1 Then
            Try
                'load custom level
                Blocks = 0
                'create new instance of Level class using the custom bitmap and custom cellsize
                Level = New Level(FormLevelCreator.Bitmap, FormLevelCreator.CellSize)

                'recreate graphics for new form size
                GraphicsReset()

                'center form
                If Center = True Then
                    CenterForm()
                End If

                'before the level is loaded, Player is disposed. If no player was created, throw and exception
                If Player.CellSize = 0 Then
                    Throw New System.Exception
                End If

            Catch ex As Exception
                'goto menu if custom level was unsucsessful
                MenuGoto()

                'show error message based on what error occoured
                Dim str As String
                Select Case CStr(ex.GetType().ToString())
                    Case "System.IndexOutOfRangeException" : str = "There are too many blocks"
                    Case "System.NullReferenceException" : str = "There is no player"
                    Case "System.Exception" : str = "There is no player"
                    Case Else : str = ex.GetType.ToString & " IDIOT"
                End Select
                MsgBox(str, , "ABLOCKALYPSE")

                'exit sub if the level was unsucessful
                Exit Sub
            End Try
        End If

        'start timer, reset opacity
        GameTimer.Start()
        Me.Opacity = 100

    End Sub
    Private Sub LevelGoto(ByVal LevelNumber As Integer)

        'change opacity
        If OpacityLoad = True Then
            Me.Opacity = 1
        End If

        'clear graphics
        G.Clear(Color.White)

        'reset help opacity
        hlp = 1400

        'store the orginal levelNumber variale
        Dim n = LevelNumber

        'if -2, reset the current level. If -1, goto the next level 
        Select Case LevelNumber
            Case -2
                If FormLevelCreator.Loaded = True Then
                    Attempts += 1
                    GameStart(-1)
                    Exit Sub
                Else
                    LevelNumber = CurrentLevel
                    Attempts += 1
                End If
            Case -1
                If FormLevelCreator.Loaded = True Then
                    FormLevelCreator.Loaded = False
                    MenuGoto()
                Else
                    CurrentLevel += 1
                    LevelNumber = CurrentLevel
                End If
        End Select

        Try

            'create new instance of Level using a different overload
            Blocks = 0
            Level = New Level(LevelNumber)

            'recreate graphics for new form size
            GraphicsReset()

            'center if the level isnt just being reset
            If n <> -2 And Center = True Then
                CenterForm()
            End If

            'unlock new level
            If CurrentLevel > LevelUnlocked - 1 Then
                LevelUnlocked = CurrentLevel + 1
            End If

        Catch ex As Exception
            'goto menu if there is no next level (change to win screen)
            Win()
        End Try

        'reset opacity
        Me.Opacity = 100

    End Sub

    Private Sub PlayerMove()

        'jumping. If the player vsp is not larger than 10, stop jumping. 
        If Keys(0) = True And Player.vsp > 10 * -1 * Math.Sign(Player.grav) And Player.CanJump = True Then
            Player.JumpTime += 1
            Player.vsp -= (Player.JumpTime * 6) '* (Player.CellSize / 24)
        ElseIf Player.vsp <> 10 Or Keys(0) = False Then
            If Player.grav > 0 Then
                Player.CanJump = False
                Player.JumpTime = 0
            Else
                Player.CanJump = False
                Player.Grounded = False
            End If
        End If

        'controlling - and + gravity
        Dim col1, col2 As Boolean
        If Player.grav > 0 Then
            If Player.vsp >= 0 Then
                col1 = True
            Else
                col1 = False
            End If
        End If
        If Player.grav < 0 Then
            If Player.vsp <= 0 Then
                col2 = True
            Else
                col2 = False
            End If
        End If

        'vertical collisions, goto functions based on the direction of collision checking
        If col1 = True Then
            BlockCollision(True)
        ElseIf col2 = True Or col1 = False Then
            BlockCollision(False)
        End If

        'horizontal movement
        If Keys(1) = True And Keys(2) = False Then
            If PlaceFree(Player.X - Player.Xspeed, Player.Y + 1, "All") = -1 And _
                PlaceFree(Player.X - Player.Xspeed, Player.Y + Player.CellSize - 1, "All") = -1 Then
                Player.X -= Player.Xspeed
            Else
                BlockSpecialCollison("Left")
            End If
        ElseIf Keys(2) = True And Keys(1) = False Then
            If PlaceFree(Player.X + Player.CellSize + Player.Xspeed, Player.Y + 1, "All") = -1 And _
                PlaceFree(Player.X + Player.CellSize + Player.Xspeed, Player.Y + Player.CellSize - 1, "All") = -1 Then
                Player.X += Player.Xspeed
            Else
                BlockSpecialCollison("Right")
            End If
        End If

        'screen wrapping horizontally
        Dim canWrap As Integer
        If FormLevelCreator.Loaded = True Then
            canWrap = FormLevelCreator.Wrapped
        Else
            canWrap = Level.Stats(3)
        End If
        'wrap left
        If Player.X < 0 Then
            If canWrap = True Then
                Player.X = Me.Width - Player.CellSize - WindowWidth
            ElseIf canWrap = False Then
                Player.X = 0
            End If
            'wrap right
        ElseIf Player.X + Player.CellSize + WindowWidth > Me.Width Then
            If canWrap = True Then
                Player.X = 0
            ElseIf canWrap = False Then
                Player.X = Me.Width - Player.CellSize - WindowWidth
            End If
        End If
        'wrap down
        If Player.Y + Player.CellSize > Me.Height Then
            Player.Y = 0
        End If

    End Sub
    Private Function PlaceFree(ByVal X As Integer, ByVal Y As Integer, ByVal Type As String) As Integer

        'loop through each block
        For i = 0 To Blocks - 1

            'check if theres a block in the specified location
            If X > Block(i).X And X < Block(i).X + Block(i).CellSize And _
               Y >= Block(i).Y And Y <= Block(i).Y + Block(i).CellSize Then

                'return block number (from Block() array)
                Dim str As String = Block(i).Type
                If str = Type Or Type = "All" Then
                    If str <> "BlackClear" And str <> "WhiteClear" Then
                        'count these two types of blocks as invisible, they do not affect the player
                        Return i
                        Exit Function
                    End If
                End If

            End If

            'if the Y is larger than the specified Y, exit loop
            If Block(i).Y > Y Then
                Exit For
            End If

        Next

        'return -1 (nothing in positon)
        Return -1

    End Function
    Private Sub BlockCollision(ByVal Down As Boolean)

        Dim CanMove As Boolean
        If Down = True Then
            'checks if the block can move down
            If PlaceFree(Player.X + 1, Player.Y + Player.CellSize + Player.vsp, "All") = -1 And _
                PlaceFree(Player.X - 1 + Player.CellSize, Player.Y + Player.CellSize + Player.vsp, "All") = -1 Then
                CanMove = True
            Else
                CanMove = False
            End If
        Else
            'checks if the block can move up
            If PlaceFree(Player.X + 1, Player.Y + Player.vsp, "All") = -1 And _
            PlaceFree(Player.X - 1 + Player.CellSize, Player.Y + Player.vsp, "All") = -1 Then
                CanMove = True
            Else
                CanMove = False
            End If
        End If

        If CanMove = True Then
            'rise or fall
            Player.Y += Player.vsp
            If Player.grav > 0 Then
                If Player.vsp < Player.CellSize - 1 Then
                    Player.vsp += Player.grav
                Else
                    Player.vsp = Player.CellSize - 1
                End If
            Else
                If Player.vsp > (-Player.CellSize) + 1 Then
                    Player.vsp += Player.grav
                Else
                    Player.vsp = (-Player.CellSize) + 1
                End If
            End If

            Player.Grounded = False
        Else
            If Down = True Then
                'collide with ground
                Player.Y = Math.Ceiling(Player.Y / Player.CellSize) * Player.CellSize
                Player.vsp = 0
                Player.CanJump = True
                Player.Grounded = True
                BlockSpecialCollison("Down")
            Else
                'collide with ceiling
                Player.Y -= (Player.Y Mod Player.CellSize) ' - 1
                Player.vsp = 0
                Player.CanJump = False
                BlockSpecialCollison("Up")
            End If
        End If

    End Sub
    Private Sub BlockSpecialCollison(ByVal dir As String)

        'there are two blocks that the player can be colliding with when at ground level. check if any of those blocks are special, 
        'and do stuff based on that. It always checks the left block first. 
        Dim BlockLeft As Integer
        Dim BlockRight As Integer
        Dim Collision As String = ""

        'get the block id of the bottom left and bottom right blocks to the player
        Select Case dir
            Case "Down"
                BlockLeft = PlaceFree(Player.X + 1, Player.Y + Player.CellSize + 1, "All")
                BlockRight = PlaceFree(Player.X + Player.CellSize - 1, Player.Y + Player.CellSize + 1, "All")
            Case "Up"
                BlockLeft = PlaceFree(Player.X + 1, Player.Y - 1, "All")
                BlockRight = PlaceFree(Player.X + Player.CellSize - 1, Player.Y - 1, "All")
            Case "Left"
                BlockLeft = PlaceFree(Player.X - 1, Player.Y + 1, "All")
                BlockRight = PlaceFree(Player.X - 1, Player.Y + Player.CellSize - 1, "All")
            Case "Right"
                BlockLeft = PlaceFree(Player.X + Player.CellSize + 1, Player.Y + 1, "All")
                BlockRight = PlaceFree(Player.X + Player.CellSize + 1, Player.Y + Player.CellSize - 1, "All")
        End Select

        'if there is a non-default block, store the colour of the block in Collision
        If BlockLeft >= 0 Then
            If Block(BlockLeft).Type <> "Default" Then
                Collision = Block(BlockLeft).Type
            ElseIf BlockRight >= 0 Then
                If Block(BlockRight).Type <> "Default" Then
                    Collision = Block(BlockRight).Type
                End If
            End If
        ElseIf BlockRight >= 0 Then
            If Block(BlockRight).Type <> "Default" Then
                Collision = Block(BlockRight).Type
            End If
        End If

        'do something based on the block type 
        Select Case Collision
            Case "Green" 'EXIT
                'if its a custom level, goto menu
                If FormLevelCreator.Loaded = True Then
                    MenuGoto()
                    FormLevelCreator.Loaded = False
                    Exit Sub
                End If
                'else goto next level
                LevelGoto(-1)
                Exit Sub
            Case "Grey" 'SPIKE
                'reset level
                LevelGoto(-2)
                Exit Sub
            Case "Blue" 'BOUNCE
                If dir = "Down" Then
                    'if the block is below, launch the player
                    Player.vsp = -20
                ElseIf dir = "Up" Then
                    'else make the player stick to it
                    Player.vsp = -Math.Abs(Player.grav)
                End If
            Case "Pink" 'REVERSE GRAVITY
                If dir = "Down" Then
                    'go up
                    Player.grav = Math.Abs(Player.grav) * -1
                    Player.vsp = Player.grav
                ElseIf dir = "Up" Then
                    'go down
                    Player.grav = Math.Abs(Player.grav)
                    'Player.Y += Player.grav
                    Player.vsp = 1
                End If
            Case "LBlue" 'DESTROY ALL Destructable
                For i = 0 To Blocks - 1
                    If Block(i).Type = "Destructable" Then
                        Block(i).Col = Color.FromArgb(255, 254, 255)
                    End If
                Next
            Case "Yellow" 'OPPOSITE OF BLUE
                For i = 0 To Blocks - 1
                    If Block(i).Type = "WhiteClear" Then
                        Block(i).Col = Color.FromArgb(0, 1, 0)
                    End If
                Next
        End Select

    End Sub

    Private Sub TimeAdd()

        If tSec = TimeOfDay.Second Then
            'add one Frame if its not a second passed
            tTicks += 1
        Else
            'set FPS, add to the time survived
            tSec = TimeOfDay.Second : totalTicks += tTicks : FPS = tTicks : Time += 1 : tTicks = 0
        End If

    End Sub
    Private Function TimeGet(ByVal Tme As Integer) As String

        'set temp variables
        Dim sec As Integer = Tme, str As String = "", temp As Integer = 0

        'loop through days
        While sec >= 86400
            temp += 1
            sec -= 86400
        End While
        'display days if there are any
        If temp <> 0 Then
            str += temp & "d"
            temp = 0
        End If
        'loop through hours
        While sec >= 3600
            temp += 1
            sec -= 3600
        End While
        'display hours if there are any
        If temp <> 0 Then
            str += " " & temp & "h"
            temp = 0
        End If
        'loop through mintues
        While sec >= 60
            temp += 1
            sec -= 60
        End While
        'display minutes if there are any
        If temp <> 0 Then
            str += " " & temp & "m"
            temp = 0
        End If
        'display seconds if there are any
        If sec <> 0 Then
            str += " " & sec & "s"
        End If

        'return str
        Return str

    End Function
    Private Sub GameTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GameTimer.Tick

        'move player
        If Status = "Game" Then
            PlayerMove()
        End If

        'draw everything
        If Status = "Game" Then

            'update FPS
            TimeAdd()

            'clear
            G.Clear(Color.White)

            'draw each block
            For i = 0 To Blocks - 1
                G.FillRectangle(New SolidBrush(Block(i).Col), New Rectangle(Block(i).X, Block(i).Y, _
                                                                            Block(i).CellSize, Block(i).CellSize))
            Next

            'draw player
            If PlayerImage = True Then
                If Player.grav > 0 Then
                    'if not upside down, draw player image
                    G.DrawImage(PlrImg, New Rectangle(Player.X, Player.Y, Player.CellSize, Player.CellSize))
                ElseIf Player.grav < 0 Then
                    'if upside down, rotate image and draw that image
                    Dim img As Image : img = PlrImg.Clone() : img.RotateFlip(RotateFlipType.RotateNoneFlipY)
                    G.DrawImage(img, New Rectangle(Player.X, Player.Y, Player.CellSize, Player.CellSize))
                End If
            Else
                'draw player rectangle
                G.FillRectangle(New SolidBrush(Player.Col), New Rectangle(Player.X, Player.Y, Player.CellSize, Player.CellSize))
            End If

            'draw text
            Dim str As String
            If CurrentLevel + 1 = 0 Then
                str = "Custom Level" & vbCrLf
            Else
                str = "Level " & CurrentLevel + 1 & vbCrLf
            End If
            If Debug = True Then
                str += "vsp: " & Player.vsp & vbCrLf & _
                                 "JumpTime: " & Player.JumpTime & vbCrLf & _
                                 "Player: (" & Player.X & "," & Player.Y & ")" & vbCrLf & _
                                 "Attempts: " & Attempts & vbCrLf & _
                                 "grav: " & Player.grav & vbCrLf & _
                                 "Blocks: " & Blocks & vbCrLf & _
                                 "Time: " & TimeGet(Time) & vbCrLf & _
                                 "FPS: " & FPS & " | " & tTicks & vbCrLf & _
                                 "totalTks: " & totalTicks
            Else
                str += "Time: " & TimeGet(Time) & vbCrLf & _
                    "Attempts: " & Attempts & vbCrLf & _
                            "FPS: " & FPS
            End If
            G.DrawString(str, Fnt, Brushes.Aquamarine, Me.Width - 20, 0, StringFormat)

            'Draw Help Message at the center of the screen
            If Level.Stats(5) <> "" And hlp > 0 Then
                If hlp > 0 Then
                    hlp -= 10
                End If
                StringFormat.Alignment = StringAlignment.Center
                G.DrawString(Level.Stats(5), Fnt, New SolidBrush(Color.FromArgb(Math.Min(255, hlp), 127, 255, 212)), _
                             New Point(Me.Width / 2, Me.Height / 2), StringFormat)
                StringFormat.Alignment = StringAlignment.Far
            End If

            'do graphics thing
            G = Graphics.FromImage(BB) : BBG.DrawImage(BB, 0, 0, Me.Width, Me.Height)

        End If

    End Sub

    Private Sub FormMain_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        If e.KeyCode = Windows.Forms.Keys.W Or e.KeyCode = Windows.Forms.Keys.Up Then
            'if up is pressed at the menu, decrease sel. If at the game, set up key variable to true. If at level select, go up one tile
            If Status = "Menu" Then
                sel -= 1
                If sel < 0 Then : sel = Buttons
                End If
                MenuDraw()
            ElseIf Status = "Game" Then
                Keys(0) = True
            ElseIf Status = "Select" Then
                lsel -= 10
                If lsel = -10 Then
                    lsel = 50
                ElseIf lsel < 0 Then : lsel = 50 - Math.Abs(lsel)
                End If
                LevelSelectDraw()
            End If
        ElseIf e.KeyCode = Windows.Forms.Keys.S Or e.KeyCode = Windows.Forms.Keys.Down Then
            'if down is pressed at the menu, decrease sel. If at the game, set down key variable to true. If at level select, go down one tile
            If Status = "Menu" Then
                sel += 1
                If sel > Buttons Then : sel = 0
                End If
                MenuDraw()
            ElseIf Status = "Select" Then
                lsel += 10
                If lsel = 60 Then : lsel = 0
                ElseIf lsel = 50 Then : lsel = 50
                Else : lsel = lsel Mod 50
                End If
                LevelSelectDraw()
            End If
        ElseIf e.KeyCode = Windows.Forms.Keys.A Or e.KeyCode = Windows.Forms.Keys.Left Then
            'If at the game, set left key variable to true. If at level select, go left one tile
            Keys(1) = True
            If Status = "Select" Then
                lsel -= 1
                If lsel < 0 Then : lsel = 50
                End If
                LevelSelectDraw()
            End If
        ElseIf e.KeyCode = Windows.Forms.Keys.D Or e.KeyCode = Windows.Forms.Keys.Right Then
            'If at the game, set right key variable to true. If at level select, go right one tile
            Keys(2) = True
            If Status = "Select" Then
                lsel += 1
                If lsel > 50 Then : lsel = 0
                End If
                LevelSelectDraw()
            End If
        ElseIf e.KeyCode = Windows.Forms.Keys.Space Or e.KeyCode = Windows.Forms.Keys.Enter Then
            'space is the enter key. If at menu, do click event for menu. If at level select, goto level
            If Status = "Menu" Then : MenuClick(sel)
            ElseIf Status = "Select" Then
                If lsel < LevelUnlocked Then : LevelSelectClick(lsel) : End If
            End If
        End If

    End Sub
    Private Sub FormMain_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp

        'if at the load screen, dispose picturebox and goto menu
        If Status = "Start" Then
            If OpacityLoad = True Then
                Me.Opacity = 1
            End If
            LoadPic.Dispose()
            MenuGoto()
            Exit Sub
        End If

        'when the key is released, set the respective variable to false
        If e.KeyCode = Windows.Forms.Keys.W Or e.KeyCode = Windows.Forms.Keys.Up Then
            Keys(0) = False
        ElseIf e.KeyCode = Windows.Forms.Keys.A Or e.KeyCode = Windows.Forms.Keys.Left Then
            Keys(1) = False
        ElseIf e.KeyCode = Windows.Forms.Keys.D Or e.KeyCode = Windows.Forms.Keys.Right Then
            Keys(2) = False
        ElseIf e.KeyCode = Windows.Forms.Keys.H Then
            'reset help timer
            hlp = 1200
        ElseIf e.KeyCode = Windows.Forms.Keys.Escape Then
            'goto menu
            If Status <> "Menu" Then : MenuGoto()
            End If
        ElseIf e.KeyCode = Windows.Forms.Keys.C Then
            'cheat menu
            If Status = "Menu" Then : Cheat()
            End If
        End If

    End Sub
    Protected Overrides Sub OnResize(ByVal e As System.EventArgs)
        If Status <> "Game" Then
            MyBase.OnResize(e)
            Select Case Me.WindowState
                Case FormWindowState.Normal
                    Select Case Status
                        Case "Menu"
                            MenuGoto()
                        Case "Select"
                            LevelSelectGoto()
                        Case "Help"
                            G.Clear(Color.White)
                            G.DrawImage(My.Resources.HelpScreen, New Point(0, 0))
                        Case "Win"
                            MenuGoto()
                    End Select
            End Select
        End If

        While Me.Width = 160 And Me.Height = 27
            GameTimer.Stop()
            Application.DoEvents()
        End While
        If Status = "Game" Then
            GameTimer.Start()
        End If

    End Sub
    Private Sub FormMain_ResizeEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeEnd
        Select Case Status
            Case "Menu"
                Dim cnt As Boolean = Center
                Center = False
                MenuGoto()
                Center = cnt
            Case "Select"
                LevelSelectGoto()
            Case "Help"
                G.Clear(Color.White)
                G.DrawImage(My.Resources.HelpScreen, New Point(0, 0))
            Case "Win"
                MenuGoto()
        End Select
    End Sub

    Private Sub MenuGoto()

        'reset select, stop timer
        sel = 0
        GameTimer.Stop()

        'reset form height, center and change status
        Me.Width = 840 + WindowWidth
        Me.Height = 540 + WindowHeight
        If Center = True Then
            CenterForm()
        End If
        Status = "Menu"

        'recreate graphics for new form size
        GraphicsReset()

        'clear graphics, draw menu image
        G.Clear(Color.White)
        G.DrawImage(My.Resources.MenuScreen, New Point(0, 0))

        StringFormat.Alignment = StringAlignment.Near
        G.DrawString("Programmed by Gareth Booth. Designed by Daniel Van Driel.", Fnt, Brushes.Black, _
                     New Point(5, Me.Height - (Font.SizeInPoints * 7)), StringFormat)
        StringFormat.Alignment = StringAlignment.Far

        'draw the buttons and stuff
        MenuDraw()

    End Sub
    Private Sub MenuDraw()

        'variables for the buttons
        Dim Width = 200, Height = 50
        StringFormat.Alignment = StringAlignment.Center

        'draw each button
        For i = 0 To Buttons

            'button variables
            Dim Str As String
            Str = MenuButtons(i)
            Dim length = Str.Length

            'one brush for the text, one for a rectangle
            Dim Col As Brush
            Dim c As Brush
            If sel = i Then
                'random colour if the button is selected
                Col = Brushes.Black
                Randomize()
                c = New SolidBrush(Color.FromArgb(100, Rnd() * 255, Rnd() * 255, Rnd() * 255))
            Else
                'else black
                Col = Brushes.Black
                c = New SolidBrush(Color.FromArgb(200, 255, 255, 255))
            End If

            'if the mouse is over a button then MenuClick()
            If MouseX > (Me.Width / 2 - Width / 2) And MouseX < (((Me.Width / 2 - Width / 2)) + Width) Then
                If MouseY > ((90 + (1 + i) * 68)) And MouseY < ((90 + (1 + i) * 68) + Height) Then
                    If Mouse = True Then
                        Mouse = False
                        MenuClick(i)
                        Exit Sub
                    End If
                End If
            End If

            'draw buttons
            G.DrawImage(My.Resources.ButtonMenu, New Rectangle(New Point(Me.Width / 2 - Width / 2, 90 + (1 + i) * 68), New Size(Width, Height)))
            'draw transparent rectangle
            G.FillRectangle(c, New Rectangle(New Point(Me.Width / 2 - Width / 2, 90 + (1 + i) * 68), New Size(Width, Height)))
            'draw text
            G.DrawString(Str, Fnt, Col, New Point(Me.Width / 2, (90 + (1 + i) * 68) + 32 - 1.5 * Fnt.SizeInPoints), StringFormat)
        Next

        'reset opacity
        Me.Opacity = 100
        StringFormat.Alignment = StringAlignment.Far

    End Sub
    Private Sub MenuClick(ByVal Button As Integer)
        Select Case Button
            Case 0 'START
                'goto level select
                FormLevelCreator.Loaded = False
                LevelSelectGoto()
            Case 1 'CUSTOM LEVEL
                'dispose the player if one exists
                Try
                    Player.Dispose()
                Catch ex As Exception
                End Try
                'show level creator form, hide this form
                FormLevelCreator.Show()
                Me.Enabled = False
                Me.Hide()
                While Me.Enabled = False
                    Me.Focus()
                    FormLevelCreator.BringToFront()
                    Application.DoEvents()
                End While
                'if no level is loaded, goto menu else start custom level
                If FormLevelCreator.Loaded = False Then
                    MenuGoto()
                Else
                    GameStart(-1)
                End If
            Case 2 'HELP
                'show help menu
                Status = "Help"
                G.Clear(Color.White)
                G.DrawImage(My.Resources.HelpScreen, New Point(0, 0))
            Case 3
                If Scores.Count > 0 Then
                    Dim str As String = ""
                    For i = 0 To Scores.Count - 1
                        str += Scores.Item(i) & vbCrLf
                    Next
                    MsgBox(str)
                Else
                    MsgBox("No Scores", , "ABLOCKALYPSE")
                End If
            Case 4 'EXIT
                Application.Exit()
        End Select
    End Sub

    Private Sub LevelSelectGoto()

        'change opacity
        If OpacityLoad = True Then
            Me.Opacity = 1
        End If

        'change status, reset select
        Status = "Select"
        lsel = 0

        'draw level select screen
        G.Clear(Color.White)
        G.DrawImage(My.Resources.LevelScreen, New Point(0, 0))

        StringFormat.Alignment = StringAlignment.Center
        G.DrawString("Press Escape to go Back", Fnt, Brushes.Black, New Point(Me.Width / 2, Me.Height / 1.25), StringFormat)
        StringFormat.Alignment = StringAlignment.Far

        'draw buttons and stuff
        LevelSelectDraw()

        'reset opacity
        Me.Opacity = 100

    End Sub
    Private Sub LevelSelectDraw()

        'variables for the buttons
        Dim Width = 50, Height = 50
        StringFormat.Alignment = StringAlignment.Center

        'draw each button. 5 rows and 10 columns
        For i = 0 To 5
            For j = 0 To 9

                If i = 5 And j > 0 Then
                    Exit For
                End If
                'current level to draw
                Dim Str As String = CStr(i * 10 + j + 1)
                Dim length = Str.Length

                'if mouse is in area and its clicked, start level
                If MouseX > (j * Width + (j + 1) * 30) And MouseX < ((j * Width + (j + 1) * 30) + Width) Then
                    If MouseY > ((i + 1.4) * Height + i * 24) And MouseY < ((i + 1.4) * Height + i * 24 + Height) Then
                        If Mouse = True And (CInt(Str) - 1) < LevelUnlocked Then
                            Mouse = False
                            LevelSelectClick(CInt(Str) - 1)
                            Exit Sub
                        End If
                    End If
                End If

                'draw button
                G.DrawImage(My.Resources.ButtonSelect, New Rectangle(New Point(j * Width + (j + 1) * 30, _
                                                                             (i + 1.4) * Width + i * 24), New Size(Width, Height)))
                'text brush
                Dim B As SolidBrush
                If CInt(Str) <= LevelUnlocked Then
                    B = New SolidBrush(Color.Black)
                Else
                    B = New SolidBrush(Color.White)
                End If

                'draw rectangle with random colour ontop of button image
                If lsel = (i * 10 + j) Then
                    Dim c As Brush
                    Randomize()
                    c = New SolidBrush(Color.FromArgb(255, 255, 100 + Rnd() * 155, 100 + Rnd() * 155))

                    G.FillRectangle(c, New Rectangle(New Point(j * Width + (j + 1) * 30 + 1, _
                                            (i + 1.4) * Width + i * 24 + 1), New Size(Width - 2, Height - 2)))
                End If

                'draw level number
                G.DrawString(Str, Fnt, B, New Point((j * Height + (j + 1) * 30) + Height / 2, _
                                                                ((i + 1.4) * Height + i * 24) + Height / 4), StringFormat)

            Next

        Next

        StringFormat.Alignment = StringAlignment.Far

    End Sub
    Private Sub LevelSelectClick(ByVal level As Integer)
        'reset keys
        Keys(0) = False
        Keys(1) = False
        Keys(2) = False
        'start game
        GameStart(level)
    End Sub

    Private Sub Win()

        'reset everything
        GameTimer.Stop()
        Me.Width = 840 + WindowWidth
        Me.Height = 540 + WindowHeight
        Status = "Win"
        If Center = True Then : CenterForm() : End If

        'recreate graphics for new form size
        GraphicsReset()
        StringFormat.Alignment = StringAlignment.Center

        'clear graphics, draw image and text
        G.Clear(Color.White)
        G.DrawImage(My.Resources.MenuScreen, New Point(0, 0))
        G.DrawString("Daniel Van Driel", Fnt, Brushes.Black, New Point(Me.Width / 3 - 50, Me.Height - Me.Height / 4 - 30))
        G.DrawImage(My.Resources.Penguin, New Point(Me.Width / 3, Me.Height - Me.Height / 4))
        G.DrawImage(My.Resources.MountainDew, New Rectangle(New Point(Me.Width - Me.Width / 2.5, Me.Height - Me.Height / 4), _
                                                            New Size(My.Resources.Penguin.Size.Width + 20, My.Resources.Penguin.Size.Height + 20)))
        G.DrawString("Gareth Booth", Fnt, Brushes.Black, New Point(Me.Width - Me.Width / 3 - 80, Me.Height - Me.Height / 4 - 30))
        G.DrawString("Special thanks to Mr Chan", New Font(Fnt.SystemFontName, 10), Brushes.Black, _
                     New Point(10, 10))

        'draw gameover screen
        Dim str As String = ""
        If Attempts <> 1 Then
            str = "ttempts"
        Else
            str = "ttempt"
        End If
        G.DrawString("CONGRATULATIONS!!!" & vbCrLf & _
                                 "You have completed ABLOCKALYPSE!" & vbCrLf & _
                                 "It ONLY took you " & Attempts & " a" & str & "!" & vbCrLf & _
                                 "You ONLY spent/wasted" & TimeGet(Time) & " of your life!" & vbCrLf & _
                                 "Go on, share the accomplishment with your friends and family (if you still have any)" & vbCrLf & _
                                 "Here's what you get for finishing the game: type Trippy into the cheat menu." & vbCrLf & _
                                 "Remeber to give the level creator a go!" & vbCrLf & _
                                 "Press escape." _
                    , Fnt, Brushes.Black, New Point(Me.Width / 2, Me.Height / 3.5), StringFormat)

        StringFormat.Alignment = StringAlignment.Far

        'open name dialog. If they think their good and they enter no name, default name is used 
        Dim name As String = InputBox("Enter Name", "ABLOCKALYPSE", "Sample Text")
        If name = "" Then : name = "John Smith"
        End If

        'add score, reset time and attempts
        Scores.Add(name & ": " & TimeGet(Time) & " : " & CStr(Attempts) & " A" & str)
        Attempts = 0
        Time = 0

    End Sub

    Private Sub FormMain_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
        'set mouse to true
        Mouse = True
        If Status = "Select" Then
            'click event for level select is in LevelSelectDraw()
            LevelSelectDraw()
            Mouse = False
        ElseIf Status = "Menu" Then
            'click event for menu is in MenuDraw()
            MenuDraw()
            Mouse = False
        ElseIf Status = "Help" Then
            'click to exit help menu
            Mouse = False
            MenuGoto()
        End If
    End Sub
    Private Sub FormMain_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp
        'set mouse to false
        Mouse = False
    End Sub
    Private Sub FormMain_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        'record Mouse X and Y when the location changes
        MouseX = e.X
        MouseY = e.Y
    End Sub

    Private Sub Cheat()

        'open a input box and do actions based on the entered text
        Dim str As String
        str = InputBox(" ", " ")
        Select Case str
            Case "Trippy"
                Trippy = Not Trippy
            Case "Reset"
                LevelUnlocked = 1
                Time = 0
                Attempts = 0
                Fnt = New Font("Comic Sans MS", 15)
            Case "ResetScores"
                Scores.Clear() : LevelUnlocked = 1 : Time = 0 : Attempts = 0 : Fnt = New Font("Comic Sans MS", 15)
            Case "Opacity"
                OpacityLoad = Not OpacityLoad
            Case "Center"
                Center = Not Center
            Case "Debug"
                Debug = Not Debug
            Case "Potato"
                PlayerImage = True
                PlrImg = New Bitmap(My.Resources.Penguin)
            Case "MLG"
                PlayerImage = True
                PlrImg = New Bitmap(My.Resources.MountainDew)
            Case "Normal"
                PlayerImage = False
            Case "Colour"
                Dim Col As New ColorDialog
                If Col.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
                    plrCol = Col.Color
                End If
            Case "Font"
                Try
                    Dim FntD As New FontDialog
                    FntD.MaxSize = 20
                    If FntD.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
                        Fnt = New Font(CStr(FntD.Font.FontFamily.ToString), 15)
                        MenuGoto()
                    End If
                Catch ex As Exception
                    MsgBox("That font is not supported.")
                End Try
            Case "Data"
                MsgBox(My.Computer.FileSystem.ReadAllText(dir & save))
            Case "Unlock"
                If hacks = True Then
                    LevelUnlocked = 50
                End If
            Case "hax"
                hacks = Not hacks
            Case "Show"
                MsgBox("Reset, ResetScores, Opacity, Debug, Show, Center, Potato, MLG, Colour, Normal," & _
                       " Font, Data", , "ABLOCKALYPSE")
            Case Else
                If hacks = True Then
                    Try
                        Dim int As Integer = CInt(str)
                        GameStart(int)
                    Catch ex As Exception
                    End Try
                    'reset keys
                    Keys(0) = False
                    Keys(1) = False
                    Keys(2) = False
                End If
        End Select

    End Sub
    Private Sub LoadPic_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadPic.Click
        'clicking at the load screen will also go to the menu.
        If OpacityLoad = True Then
            Me.Opacity = 1
        End If
        LoadPic.Dispose()
        MenuGoto()
    End Sub
    Private Sub GraphicsReset()
        'reset graphics objects for new form width and height
        G = Me.CreateGraphics
        BBG = Me.CreateGraphics
        BB = New Bitmap(Me.Width, Me.Height)
    End Sub
    Private Sub CenterForm()

        'change form location
        Me.Location = New Point((Screen.PrimaryScreen.Bounds.Width / 2) - (Me.Width / 2), _
                                (Screen.PrimaryScreen.Bounds.Height / 2) - (Me.Height / 2))

    End Sub

    Private Sub DataSave()
        'save all data into a file. Loop through each score, and add it to the end of the file after the other stuff.
        Dim File = FreeFile() : FileOpen(File, dir + save, OpenMode.Output)
        'put the text here
        Dim str As String = ""
        For i = 0 To Scores.Count - 1
            str += CStr(Scores.Item(i)) & ","
        Next
        PrintLine(File, Attempts & "," & LevelUnlocked & "," & Time & "," & CStr(Fnt.Name) & "," & str)
        FileClose(File)
    End Sub
    Private Sub DataLoad()
        'if the file exists...
        If My.Computer.FileSystem.FileExists(dir & save) Then

            'get text
            Dim contents = My.Computer.FileSystem.ReadAllText(dir & save)

            'if their is nothing then save new data
            If contents = "" Then : DataSave()
            End If

            'set variables
            Dim i As Integer = 0, j As Integer = 0, k As Integer = 0
            Dim str = "", maxJ As Integer = 0

            'count the amount of commas, this is the amount of times to loop through.
            For l = 0 To contents.Length - 1
                If contents.Chars(l) = CChar(",") Then
                    maxJ += 1
                End If
            Next

            While k < contents.Length

                'if j is the number of data to read then exit
                If j = maxJ Then
                    Exit While
                End If

                While contents.Chars(i) <> CChar(",")
                    str += contents.Chars(i)
                    i += 1
                End While

                Select Case j
                    Case 0
                        'font type
                        Attempts = CInt(str)
                    Case 1
                        'font size
                        LevelUnlocked = CInt(str)
                    Case 2
                        Time = CInt(str)
                    Case 3
                        Try
                            Fnt = New Font(CStr(str), Fnt.SizeInPoints)
                        Catch ex As Exception
                        End Try
                    Case Else
                        Scores.Add(CStr(str))
                End Select

                k += (str.Length + 1)
                i = k
                j += 1
                str = ""

            End While

        Else

            'create new directory if none exists
            If Not My.Computer.FileSystem.DirectoryExists(dir) Then
                My.Computer.FileSystem.CreateDirectory(dir)
            End If
            'create file
            Dim filesys As System.IO.FileStream = System.IO.File.Create(dir & save)
            filesys.Dispose()
            'save data to the file
            DataSave()
            'load the new data
            DataLoad()

        End If
    End Sub
    Private Sub FormMain_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        DataSave()
    End Sub

End Class
