Public Class Block

    'block variables
    Public X As Integer, Y As Integer, CellSize As Integer, Col As Color

    Sub New(ByVal XPos As Integer, ByVal YPos As Integer, ByVal Size As Integer, ByVal Colour As Color)

        'set variables
        X = XPos
        Y = YPos
        CellSize = Size
        Col = Colour

        'make colourful if trippy is activated
        If FormMain.Trippy = True Then
            If Col.R = 0 And Col.G = 0 And Col.B = 0 Then
                Randomize()
                Col = Color.FromArgb(255, Rnd() * 255, Rnd() * 255, Rnd() * 255)
            End If
        End If

    End Sub

    Public Property Type As String
        Get
            'return colour of the block as a string
            Dim typ As String = ""
            If Col.R = 0 And Col.G = 0 And Col.B = 0 Then
                typ = "Default"
            ElseIf Col.R = 0 And Col.G = 255 And Col.B = 0 Then
                typ = "Green"
            ElseIf Col.R = 100 And Col.G = 100 And Col.B = 100 Then
                typ = "Grey"
            ElseIf Col.R = 0 And Col.G = 0 And Col.B = 255 Then
                typ = "Blue"
            ElseIf Col.R = 255 And Col.G = 0 And Col.B = 255 Then
                typ = "Pink"
            ElseIf Col.R = 0 And Col.G = 255 And Col.B = 255 Then
                typ = "LBlue"
            ElseIf Col.R = 0 And Col.G = 1 And Col.B = 0 Then
                typ = "Destructable"
            ElseIf Col.R = 1 And Col.G = 1 And Col.B = 0 Then
                typ = "BlackClear"
            ElseIf Col.R = 255 And Col.G = 254 And Col.B = 255 Then
                typ = "WhiteClear"
            ElseIf Col.R = 255 And Col.G = 255 And Col.B = 0 Then
                typ = "Yellow"
            Else
                typ = "Default"
            End If
            Return typ
        End Get
        Set(ByVal Type As String)
            Select Case Type
                Case "Default"
                    Col = Color.FromArgb(0, 0, 0)
                Case "Green"
                    Col = Color.FromArgb(0, 255, 0)
                Case "Grey"
                    Col = Color.FromArgb(100, 100, 100)
                Case "Blue"
                    Col = Color.FromArgb(0, 0, 255)
                Case "Pink"
                    Col = Color.FromArgb(255, 0, 255)
                Case "LBlue"
                    Col = Color.FromArgb(0, 255, 255)
                Case "Destructable"
                    Col = Color.FromArgb(0, 1, 0)
                Case "BlackClear"
                    Col = Color.FromArgb(1, 1, 0)
                Case "WhiteClear"
                    Col = Color.FromArgb(255, 254, 255)
                Case "Yellow"
                    Col = Color.FromArgb(255, 255, 0)
            End Select
        End Set
    End Property

End Class
