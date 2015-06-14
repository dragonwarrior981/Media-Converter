Imports System
Public Enum ProgressBarDisplayText
    Percentage
    CustomText
End Enum
Public Enum ProgressBarDisplayTextColor
    AliceBlue
    AntiqueWhite
    Aqua
    Aquamarine
    Azure
    Beige
    Bisque
    Black
    BlanchedAlmond
    Blue
    BlueViolet
    Brown
    BurlyWood
    CadetBlue
    Chartreuse
    Chocolate
    Coral
    CornflowerBlue
    Cornsilk
    Crimson
    Cyan
    DarkBlue
    DarkCyan
    DarkGoldenrod
    DarkGray
    DarkGreen
    DarkKhaki
    DarkMagenta
    DarkOliveGreen
    DarkOrange
    DarkOrchid
    DarkRed
    DarkSalmon
    DarkSeaGreen
    DarkSlateBlue
    DarkSlateGray
    DarkTurquoise
    DarkViolet
    DeepPink
    DeepSkyBlue
    DimGray
    DodgerBlue
    Firebrick
    FloralWhite
    ForestGreen
    Fuchsia
    Gainsboro
    GhostWhite
    Gold
    Goldenrod
    Gray
    Green
    GreenYellow
    Honeydew
    HotPink
    IndianRed
    Indigo
    Ivory
    Khaki
    Lavender
    LavenderBlush
    LawnGreen
    LemonChiffon
    LightBlue
    LightCoral
    LightCyan
    LightGoldenrodYellow
    LightGray
    LightGreen
    LightPink
    LightSalmon
    LightSeaGreen
    LightSkyBlue
    LightSlateGray
    LightSteelBlue
    LightYellow
    Lime
    LimeGreen
    Linen
    Magenta
    Maroon
    MediumAquamarine
    MediumBlue
    MediumOrchid
    MediumPurple
    MediumSeaGreen
    MediumSlateBlue
    MediumSpringGreen
    MediumTurquoise
    MediumVioletRed
    MidnightBlue
    MintCream
    MistyRose
    Moccasin
    NavajoWhite
    Navy
    OldLace
    Olive
    OliveDrab
    Orange
    OrangeRed
    Orchid
    PaleGoldenrod
    PaleGreen
    PaleTurquoise
    PaleVioletRed
    PapayaWhip
    PeachPuff
    Peru
    Pink
    Plum
    PowderBlue
    Purple
    Red
    RosyBrown
    RoyalBlue
    SaddleBrown
    Salmon
    SandyBrown
    SeaGreen
    SeaShell
    Sienna
    Silver
    SkyBlue
    SlateBlue
    SlateGray
    Snow
    SpringGreen
    SteelBlue
    Tan
    Teal
    Thistle
    Tomato
    Transparent
    Turquoise
    Violet
    Wheat
    White
    WhiteSmoke
    Yellow
    YellowGreen
End Enum
Public Class CustomProgressBar
    Inherits ProgressBar

    'Property to set to decide whether to print a % or Text
    Public Property DisplayStyle() As ProgressBarDisplayText
    'Property to hold the custom text
    Public Property CustomText() As String
    'Public Property _TextColor() As ProgressBarDisplayTextColor
    'Private systemColor As System.Drawing.Color = System.Drawing.Color.FromName(_TextColor)

    Public Sub New()
        InitializeComponent()
        ' Modify the ControlStyles flags
        SetStyle(ControlStyles.UserPaint Or ControlStyles.AllPaintingInWmPaint, True)
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        Dim rect As Rectangle = ClientRectangle
        Dim g As Graphics = e.Graphics

        ProgressBarRenderer.DrawHorizontalBar(g, rect)
        rect.Inflate(-3, -3)
        If Value > 0 Then
            ' As we doing this ourselves we need to draw the chunks on the progress bar
            Dim clip As New Rectangle(rect.X, rect.Y, CInt(Math.Truncate(Math.Round((CSng(Value) / Maximum) * rect.Width))), rect.Height)
            ProgressBarRenderer.DrawHorizontalChunks(g, clip)
        End If

        ' Set the Display text (Either a % amount or our custom text
        Dim text As String = If(DisplayStyle = ProgressBarDisplayText.Percentage, Value.ToString() & "% Completed", CustomText)


        Using f As New Font(FontFamily.GenericSerif, 10)

            Dim len As SizeF = g.MeasureString(text, f)
            ' Calculate the location of the text (the middle of progress bar)
            ' Point location = new Point(Convert.ToInt32((rect.Width / 2) - (len.Width / 2)), Convert.ToInt32((rect.Height / 2) - (len.Height / 2)));
            Dim location As New Point(Convert.ToInt32((Width / 2) - Convert.ToInt32(len.Width) \ 2), Convert.ToInt32((Height / 2) - Convert.ToInt32(len.Height) \ 2))
            ' The commented-out code will centre the text into the highlighted area only. This will centre the text regardless of the highlighted area.
            ' Draw the custom text
            g.DrawString(text, f, Brushes.Black, location)
        End Using
    End Sub
End Class
