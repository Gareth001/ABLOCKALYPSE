Public Class Level

    'this levels variables
    Public Stats(5) As Object
    Dim ThisLevel As Integer

    'there are 2 different construtors, one for the built in levels, one for the level editor. this is for the built in levels
    Sub New(ByVal LevelNumber As Integer)

        'create level from built in stats
        ThisLevel = LevelNumber
        AddStats(LevelNumber)
        CreateFromBitmap(Stats(0), Stats(1))

    End Sub

    'level editor level
    Sub New(ByVal Picture As Bitmap, ByVal CellSize As Integer)

        'create level from custom stuff
        ThisLevel = -1
        CreateFromBitmap(Picture, CellSize)

    End Sub

    Public Sub CreateFromBitmap(ByVal Picture As Bitmap, ByVal CellSize As Integer)

        'colour variable
        Dim col As Color = Nothing

        'loop through every pixel of the bitmap and do something depending on the colour of the pixel
        For j = 0 To Picture.Height - 1
            For i = 0 To Picture.Width - 1

                col = Picture.GetPixel(i, j)

                If col.R = 255 And col.G = 255 And col.B = 255 Then
                    'White
                ElseIf col.R = 255 And col.G = 0 And col.B = 0 Then
                    'Red
                    Dim g, X As Single
                    'try getting the gravity from the built in stats, if nothing then get them from the level creator form
                    Try
                        g = CSng(Stats(2))
                        X = CSng(Stats(4))
                        If g = 0 Or X = 0 Then
                            Throw New SystemException()
                        End If
                    Catch ex As Exception
                        g = FormLevelCreator.Gravity
                        X = FormLevelCreator.XSpeed
                    End Try
                    'create player
                    FormMain.Player = New Player(i * CellSize, j * CellSize, CellSize, g, X)
                Else
                    'Not Red or White
                    'create block
                    FormMain.Block(FormMain.Blocks) = New Block(i * CellSize, j * CellSize, CellSize, col)
                    FormMain.Blocks += 1
                End If
            Next
        Next

        'change form size
        FormMain.Width = (Picture.Width) * CellSize + FormMain.WindowWidth
        FormMain.Height = (Picture.Height) * CellSize + FormMain.WindowHeight

    End Sub

    Private Sub AddStats(ByVal LevelNumber As Integer)
        'every built in level will have 5 stats. 
        Select Case LevelNumber
            Case 0
                Stats(0) = My.Resources.L0 'Bitmap 
                Stats(1) = 24 'Cellsize
                Stats(2) = 1 'Gravity
                Stats(3) = False 'Screen Wrap
                Stats(4) = 4 'XSpeed
                Stats(5) = "Welcome to ABLOCKALYPSE! If you need help, " & vbCrLf & _
                    "go back to the menu (Press Esc) and click on Help Me." 'help message
            Case 1
                Stats(0) = My.Resources.L1
                Stats(1) = 32
                Stats(2) = 1
                Stats(3) = False
                Stats(4) = 8
                Stats(5) = "Press H to reopen this message"
            Case 2
                Stats(0) = My.Resources.L2
                Stats(1) = 30
                Stats(2) = 0.6
                Stats(3) = False
                Stats(4) = 5
                Stats(5) = "Watch out for those grey blocks!"
            Case 3
                Stats(0) = My.Resources.L3
                Stats(1) = 30
                Stats(2) = 1.1
                Stats(3) = True
                Stats(4) = 6
                Stats(5) = "Try going to the edge of the screen"
            Case 4
                Stats(0) = My.Resources.L4
                Stats(1) = 24
                Stats(2) = 1
                Stats(3) = True
                Stats(4) = 4
                Stats(5) = "To the left."
            Case 5
                Stats(0) = My.Resources.L5
                Stats(1) = 24
                Stats(2) = 0.75
                Stats(3) = True
                Stats(4) = 6
                Stats(5) = ""
            Case 6
                Stats(0) = My.Resources.L6
                Stats(1) = 28
                Stats(2) = 1.2
                Stats(3) = False
                Stats(4) = 7
                Stats(5) = ""
            Case 7
                Stats(0) = My.Resources.L7
                Stats(1) = 30
                Stats(2) = 1
                Stats(3) = True
                Stats(4) = 6
                Stats(5) = "Type MLG into the cheat menu."
            Case 8
                Stats(0) = My.Resources.L8
                Stats(1) = 24
                Stats(2) = 1
                Stats(3) = False
                Stats(4) = 4
                Stats(5) = "1898 lines of code!"
            Case 9
                Stats(0) = My.Resources.L9
                Stats(1) = 20
                Stats(2) = 0.66
                Stats(3) = False
                Stats(4) = 4
                Stats(5) = ""
            Case 10
                Stats(0) = My.Resources.L10
                Stats(1) = 24
                Stats(2) = 1
                Stats(3) = False
                Stats(4) = 4
                Stats(5) = "Some guy couldn't be bothered to add" & vbCrLf & " jumping when gravity is reversed."
            Case 11
                Stats(0) = My.Resources.L11
                Stats(1) = 20
                Stats(2) = 1
                Stats(3) = True
                Stats(4) = 10
                Stats(5) = "Let it go"
            Case 12
                Stats(0) = My.Resources.L12
                Stats(1) = 24
                Stats(2) = 1.2
                Stats(3) = True
                Stats(4) = 4
                Stats(5) = "¿ʇɐɥʍ"
            Case 13
                Stats(0) = My.Resources.L13
                Stats(1) = 60
                Stats(2) = 1
                Stats(3) = True
                Stats(4) = 10
                Stats(5) = ""
            Case 14
                Stats(0) = My.Resources.L14
                Stats(1) = 24
                Stats(2) = 1
                Stats(3) = False
                Stats(4) = 4
                Stats(5) = "Press C at the menu to open up" & vbCrLf & " a cheat/options menu. Try typing Colour!"
            Case 15
                Stats(0) = My.Resources.L15
                Stats(1) = 20
                Stats(2) = 1
                Stats(3) = False
                Stats(4) = 5
                Stats(5) = "2 blue 4 you"
            Case 16
                Stats(0) = My.Resources.L16
                Stats(1) = 32
                Stats(2) = 1
                Stats(3) = False
                Stats(4) = 8
                Stats(5) = "If you hate comic sans, type Font in the cheat menu (press c at the menu)."
            Case 17
                Stats(0) = My.Resources.L17
                Stats(1) = 32
                Stats(2) = 1.2
                Stats(3) = False
                Stats(4) = 8
                Stats(5) = "ᕙ( ͝° ͜ʖ ͡°)ᕗ"
            Case 18
                Stats(0) = My.Resources.L18
                Stats(1) = 25
                Stats(2) = 1.35
                Stats(3) = False
                Stats(4) = 5
                Stats(5) = "3.1415926535897932384626433832795028841971..."
            Case 19
                Stats(0) = My.Resources.L19
                Stats(1) = 24
                Stats(2) = 1
                Stats(3) = False
                Stats(4) = 4
                Stats(5) = "Thanks Ben and Jacob"
            Case 20
                Stats(0) = My.Resources.L20
                Stats(1) = 20
                Stats(2) = 0.9
                Stats(3) = False
                Stats(4) = 4
                Stats(5) = "gotta go fast"
            Case 21
                Stats(0) = My.Resources.L21
                Stats(1) = 18
                Stats(2) = 1.2
                Stats(3) = False
                Stats(4) = 6
                Stats(5) = "Don't worry about rage quitting, you're progress is saved."
            Case 22
                Stats(0) = My.Resources.L22
                Stats(1) = 25
                Stats(2) = 1.25
                Stats(3) = False
                Stats(4) = 5
                Stats(5) = ""
            Case 23
                Stats(0) = My.Resources.L23
                Stats(1) = 28
                Stats(2) = 1.3
                Stats(3) = False
                Stats(4) = 7
                Stats(5) = "Sample Text"
            Case 24
                Stats(0) = My.Resources.L24
                Stats(1) = 20
                Stats(2) = 1
                Stats(3) = False
                Stats(4) = 5
                Stats(5) = "¡ǝsɹǝʌᴉun uʍop ǝpᴉsdn uɐ uᴉ ʞɔnʇs ɯ,I 'dlǝɥ"
            Case 25
                Stats(0) = My.Resources.L25
                Stats(1) = 5
                Stats(2) = 1.4
                Stats(3) = False
                Stats(4) = 3
                Stats(5) = "f(x)=1/x"
            Case 26
                Stats(0) = My.Resources.L26
                Stats(1) = 15
                Stats(2) = 1
                Stats(3) = True
                Stats(4) = 5
                Stats(5) = "If you need a break, press Escape."
            Case 27
                Stats(0) = My.Resources.L27
                Stats(1) = 24
                Stats(2) = 1
                Stats(3) = False
                Stats(4) = 6
                Stats(5) = "Dont hate the developer, " & vbCrLf & "hate the game"
            Case 28
                Stats(0) = My.Resources.L28
                Stats(1) = 18
                Stats(2) = 1.4
                Stats(3) = False
                Stats(4) = 6
                Stats(5) = ""
            Case 29
                Stats(0) = My.Resources.L29
                Stats(1) = 40
                Stats(2) = 0.75 '0.925
                Stats(3) = False
                Stats(4) = 10
                Stats(5) = "Impossibru!"
            Case 30
                Stats(0) = My.Resources.L30
                Stats(1) = 15
                Stats(2) = 1
                Stats(3) = True
                Stats(4) = 5
                Stats(5) = ""
            Case 31
                Stats(0) = My.Resources.L31
                Stats(1) = 20
                Stats(2) = 1.35
                Stats(3) = False
                Stats(4) = 5
                Stats(5) = "Not even once."
            Case 32
                Stats(0) = My.Resources.L32
                Stats(1) = 24
                Stats(2) = 1
                Stats(3) = True
                Stats(4) = 6
                Stats(5) = "There's no turning back now..."
            Case 33
                Stats(0) = My.Resources.L33
                Stats(1) = 24
                Stats(2) = 1
                Stats(3) = False
                Stats(4) = 6
                Stats(5) = "2spooky"
            Case 34
                Stats(0) = My.Resources.L34
                Stats(1) = 12
                Stats(2) = 0.77
                Stats(3) = False
                Stats(4) = 4
                Stats(5) = "A real cliffhanger"
            Case 35
                Stats(0) = My.Resources.L35
                Stats(1) = 30
                Stats(2) = 0.8
                Stats(3) = True
                Stats(4) = 5
                Stats(5) = "This looks easy."
            Case 36
                Stats(0) = My.Resources.L36
                Stats(1) = 20
                Stats(2) = 1
                Stats(3) = True
                Stats(4) = 10
                Stats(5) = "Remember this level layout."
            Case 37
                Stats(0) = My.Resources.L37
                Stats(1) = 20
                Stats(2) = 1
                Stats(3) = True
                Stats(4) = 10
                Stats(5) = "I hope you remembered!"
            Case 38
                Stats(0) = My.Resources.L38
                Stats(1) = 20
                Stats(2) = 0.0085
                Stats(3) = True
                Stats(4) = 4
                Stats(5) = "At the cheat menu, type 'Show' to show a list of commands"
            Case 39
                Stats(0) = My.Resources.L39
                Stats(1) = 16
                Stats(2) = 1
                Stats(3) = False
                Stats(4) = 4
                Stats(5) = ""
            Case 40
                Stats(0) = My.Resources.L40
                Stats(1) = 20
                Stats(2) = 1
                Stats(3) = False
                Stats(4) = 10
                Stats(5) = "Epilepsy Warning"
            Case 41
                Stats(0) = My.Resources.L41
                Stats(1) = 20
                Stats(2) = 1.2
                Stats(3) = True
                Stats(4) = 4
                Stats(5) = "A-Maze-ing!"
            Case 42
                Stats(0) = My.Resources.L42
                Stats(1) = 16
                Stats(2) = 1
                Stats(3) = True
                Stats(4) = 4
                Stats(5) = "f(x)=sin(x)-1, g(x)=sin(x)+1"
            Case 43
                Stats(0) = My.Resources.L43
                Stats(1) = 18
                Stats(2) = 1
                Stats(3) = False
                Stats(4) = 6
                Stats(5) = "Almost there..."
            Case 44
                Stats(0) = My.Resources.L44
                Stats(1) = 12
                Stats(2) = 1.2
                Stats(3) = True
                Stats(4) = 4
                Stats(5) = ""
            Case 45
                Stats(0) = My.Resources.L45
                Stats(1) = 25
                Stats(2) = 0.1 '1
                Stats(3) = False
                Stats(4) = 5
                Stats(5) = "How's you're reaction time?"
            Case 46
                Stats(0) = My.Resources.L46
                Stats(1) = 15
                Stats(2) = 1
                Stats(3) = False
                Stats(4) = 5
                Stats(5) = "Having fun?"
            Case 47
                Stats(0) = My.Resources.L47
                Stats(1) = 12
                Stats(2) = 1
                Stats(3) = False
                Stats(4) = 4
                Stats(5) = "It's not what it looks like..."
            Case 48
                Stats(0) = My.Resources.L48
                Stats(1) = 12
                Stats(2) = 0.001
                Stats(3) = True
                Stats(4) = 4
                Stats(5) = ""
            Case 49
                Stats(0) = My.Resources.L49
                Stats(1) = 20
                Stats(2) = 1.2
                Stats(3) = True
                Stats(4) = 5
                Stats(5) = "The Penultimate Level."
            Case 50
                Stats(0) = My.Resources.L50
                Stats(1) = 32
                Stats(2) = 0.87
                Stats(3) = False
                Stats(4) = 4
                Stats(5) = "If you're stuck on a level, press" & vbCrLf & " ALT-F4 to skip it."
            Case Else
                'if this level doesnt exist, throw exception
                Throw New SystemException()
        End Select

    End Sub

End Class
