Imports System
Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Drawing2D
Public Class ColorProgressBar
    Inherits System.Windows.Forms.Control

    ' set default values
    Private _Value As Integer = 0
    Private _Minimum As Integer = 0
    Private _Maximum As Integer = 100
    Private _Step As Integer = 10
    Private _FillStyle As FillStyles = FillStyles.Solid
    Private _BarColor As Color = Color.FromArgb(255, 128, 128)
    Private _BorderColor As Color = Color.Black
    Public Enum FillStyles
        Solid
        Dashed
    End Enum
    Public Sub New()
        InitializeComponent()
        MyBase.Size = New Size(150, 15)
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.ResizeRedraw Or ControlStyles.DoubleBuffer, True)
    End Sub
    <Description("ColorProgressBar color"), Category("ColorProgressBar")>
    Public Property BarColor() As Color
        Get
            Return _BarColor
        End Get
        Set(ByVal value As Color)
            _BarColor = value
            Me.Invalidate()
        End Set
    End Property
    <Description("ColorProgressBar fill style"), Category("ColorProgressBar")>
    Public Property FillStyle() As FillStyles
        Get
            Return _FillStyle
        End Get
        Set(ByVal value As FillStyles)
            _FillStyle = value
            Me.Invalidate()
        End Set
    End Property
    <Description("The current value for the ColorProgressBar, " & "in the range specified by the Minimum and Maximum properties."), Category("ColorProgressBar"), RefreshProperties(RefreshProperties.All)>
    Public Property Value() As Integer
        ' the rest of the Properties windows must be updated when this peroperty is changed.
        Get
            Return _Value
        End Get
        Set(ByVal value As Integer)
            If value < _Minimum Then
                Throw New ArgumentException("'" & value & "' is not a valid value for 'Value'." & ControlChars.Lf & "'Value' must be between 'Minimum' and 'Maximum'.")
            End If

            If value > _Maximum Then
                Throw New ArgumentException("'" & value & "' is not a valid value for 'Value'." & ControlChars.Lf & "'Value' must be between 'Minimum' and 'Maximum'.")
            End If

            _Value = value
            Me.Invalidate()
        End Set
    End Property
    <Description("The lower bound of the range this ColorProgressbar is working with."), Category("ColorProgressBar"), RefreshProperties(RefreshProperties.All)>
    Public Property Minimum() As Integer
        Get
            Return _Minimum
        End Get
        Set(ByVal value As Integer)
            _Minimum = value

            If _Minimum > _Maximum Then
                _Maximum = _Minimum
            End If
            If _Minimum > _Value Then
                _Value = _Minimum
            End If

            Me.Invalidate()
        End Set
    End Property
    <Description("The uppper bound of the range this ColorProgressbar is working with."), Category("ColorProgressBar"), RefreshProperties(RefreshProperties.All)>
    Public Property Maximum() As Integer
        Get
            Return _Maximum
        End Get
        Set(ByVal value As Integer)
            _Maximum = value

            If _Maximum < _Value Then
                _Value = _Maximum
            End If
            If _Maximum < _Minimum Then
                _Minimum = _Maximum
            End If

            Me.Invalidate()
        End Set
    End Property
    <Description("The amount to jump the current value of the control by when the Step() method is called."), Category("ColorProgressBar")>
    Public Property [Step]() As Integer
        Get
            Return _Step
        End Get
        Set(ByVal value As Integer)
            _Step = value
            Me.Invalidate()
        End Set
    End Property
    <Description("The border color of ColorProgressBar"), Category("ColorProgressBar")>
    Public Property BorderColor() As Color
        Get
            Return _BorderColor
        End Get
        Set(ByVal value As Color)
            _BorderColor = value
            Me.Invalidate()
        End Set
    End Property

    ' Call the PerformStep() method to increase the value displayed by the amount set in the Step property
    Public Sub PerformStep()
        If _Value < _Maximum Then
            _Value += _Step
        Else
            _Value = _Maximum
        End If

        Me.Invalidate()
    End Sub

    ' Call the PerformStepBack() method to decrease the value displayed by the amount set in the Step property
    Public Sub PerformStepBack()
        If _Value > _Minimum Then
            _Value -= _Step
        Else
            _Value = _Minimum
        End If

        Me.Invalidate()
    End Sub

    ' Call the Increment() method to increase the value displayed by an integer you specify
    Public Sub Increment(ByVal value As Integer)
        If _Value < _Maximum Then
            _Value += value
        Else
            _Value = _Maximum
        End If

        Me.Invalidate()
    End Sub

    ' Call the Decrement() method to decrease the value displayed by an integer you specify
    Public Sub Decrement(ByVal value As Integer)
        If _Value > _Minimum Then
            _Value -= value
        Else
            _Value = _Minimum
        End If

        Me.Invalidate()
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        ' Calculate matching colors
        Dim darkColor As Color = ControlPaint.Dark(_BarColor)
        Dim bgColor As Color = ControlPaint.Dark(_BarColor)

        ' Fill background
        Dim bgBrush As New SolidBrush(bgColor)
        e.Graphics.FillRectangle(bgBrush, Me.ClientRectangle)
        bgBrush.Dispose()

        ' Check for value
        If _Maximum = _Minimum OrElse _Value = 0 Then
            ' Draw border only and exit;
            drawBorder(e.Graphics)
            Return
        End If

        ' The following is the width of the bar. This will vary with each value.
        Dim fillWidth As Integer = (Me.Width * _Value) / (_Maximum - _Minimum)

        ' GDI+ doesn't like rectangles 0px wide or high
        If fillWidth = 0 Then
            ' Draw border only and exti;
            drawBorder(e.Graphics)
            Return
        End If

        ' Rectangles for upper and lower half of bar
        Dim topRect As New Rectangle(0, 0, fillWidth, Me.Height \ 2)
        Dim buttomRect As New Rectangle(0, Me.Height \ 2, fillWidth, Me.Height \ 2)

        ' The gradient brush
        Dim brush As LinearGradientBrush

        ' Paint upper half
        brush = New LinearGradientBrush(New Point(0, 0), New Point(0, Me.Height \ 2), darkColor, _BarColor)
        e.Graphics.FillRectangle(brush, topRect)
        brush.Dispose()

        ' Paint lower half
        ' (this.Height/2 - 1 because there would be a dark line in the middle of the bar)
        brush = New LinearGradientBrush(New Point(0, Me.Height \ 2 - 1), New Point(0, Me.Height), _BarColor, darkColor)
        e.Graphics.FillRectangle(brush, buttomRect)
        brush.Dispose()

        ' Calculate separator's setting
        Dim sepWidth As Integer = CInt(Math.Truncate(Me.Height * 0.67))
        Dim sepCount As Integer = CInt(fillWidth \ sepWidth)
        Dim sepColor As Color = ControlPaint.LightLight(_BarColor)

        ' Paint separators
        Select Case _FillStyle
            Case FillStyles.Dashed
                ' Draw each separator line
                For i As Integer = 1 To sepCount
                    e.Graphics.DrawLine(New Pen(sepColor, 1), sepWidth * i, 0, sepWidth * i, Me.Height)
                Next i

            Case FillStyles.Solid
                ' Draw nothing
            Case Else
        End Select

        ' Draw border and exit
        drawBorder(e.Graphics)
    End Sub

    ' Draw border
    Protected Sub drawBorder(ByVal g As Graphics)
        Dim borderRect As New Rectangle(0, 0, ClientRectangle.Width - 1, ClientRectangle.Height - 1)
        g.DrawRectangle(New Pen(_BorderColor, 1), borderRect)
    End Sub
End Class
