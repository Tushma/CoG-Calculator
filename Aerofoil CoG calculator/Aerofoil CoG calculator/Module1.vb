Imports System
Imports System.IO
Imports System.Math
Public Structure Vector
    Public i As Double
    Public j As Double
    Function DefineVector(ByRef Vector As Vector, ByVal i As Single, ByVal j As Single)
        Vector.i = i
        Vector.j = j
        Return Vector
    End Function
End Structure
Module Module1
    Function VectorLength(ByVal Vector As Vector)
        Return Math.Sqrt(Math.Pow(Vector.i, 2) + Math.Pow(Vector.j, 2))
    End Function
    Function VectorDotProduct(ByVal Vector1 As Vector, ByVal Vector2 As Vector)
        Return (Vector1.i * Vector2.i + Vector1.j * Vector2.j)
    End Function
    Function AngleBetweenTwoVectors(ByVal Vector1 As Vector, ByVal Vector2 As Vector)
        ''Returns the Sine of the angle
        Return Math.Sqrt(1 - Math.Pow((VectorDotProduct(Vector1, Vector2) / (VectorLength(Vector1) * VectorLength(Vector2))), 2))
    End Function
    Function AreaOfTriangle(ByVal Vector1 As Vector, ByVal Vector2 As Vector)
        Return (0.5 * VectorLength(Vector1) * VectorLength(Vector2) * AngleBetweenTwoVectors(Vector1, Vector2))
    End Function
    Function CentroidOfTriangleSection(ByRef x0 As Double, ByRef x1 As Double, ByRef y0 As Double, ByRef y1 As Double)
        Dim CentroidVector As New Vector
        CentroidVector.i = (0.5 + x0 + x1) / 3
        CentroidVector.j = (y0 + y1) / 3
        Return CentroidVector
    End Function
    Sub Main()
        Dim SumOfAreas, x0, x1, y0, y1 As Double
        Dim CurrentLine As String
        Dim SumOfProductOfAreaAndCentroids, Vector0, Vector1, CentroidVector As Vector
        SumOfProductOfAreaAndCentroids.i = 0
        SumOfProductOfAreaAndCentroids.j = 0
        SumOfAreas = 0
        Using AerofoilFile As StreamReader = New StreamReader("C:\Users\joshk\OneDrive\Documents\Year One\Aerospace Eng Design A\CoordShape.txt")
            CurrentLine = AerofoilFile.ReadLine()
            x1 = CurrentLine.Substring(0, 8)
            y1 = CurrentLine.Substring(9)
            Vector1.DefineVector(Vector1, ((x1 - 0.5) * 100 * Math.Pow(10, -3)), (y1 * 100 * Math.Pow(10, -3)))
            Do While Not AerofoilFile.EndOfStream
                CurrentLine = AerofoilFile.ReadLine()
                x0 = x1
                y0 = y1
                x1 = CurrentLine.Substring(0, 8)
                y1 = CurrentLine.Substring(9)
                Vector0.DefineVector(Vector0, Vector1.i, Vector1.j)
                Vector1.DefineVector(Vector1, ((x1 - 0.5) * 100 * Math.Pow(10, -3)), (y1 * 100 * Math.Pow(10, -3)))
                CentroidVector = CentroidOfTriangleSection(x0, x1, y0 + 0.030255, y1 + 0.030255)
                SumOfProductOfAreaAndCentroids.i += AreaOfTriangle(Vector0, Vector1) * CentroidVector.i
                SumOfProductOfAreaAndCentroids.j += AreaOfTriangle(Vector0, Vector1) * CentroidVector.j
                SumOfAreas += AreaOfTriangle(Vector0, Vector1)
            Loop
            Console.WriteLine("Centre of Gravity of this aerofoil is: (" & ((SumOfProductOfAreaAndCentroids.i / SumOfAreas) * 100) + 150 & "," & ((SumOfProductOfAreaAndCentroids.j / SumOfAreas) * 100) + 64 & ")")
            Console.WriteLine("The area of the cross section is: " & (SumOfAreas))
            Console.ReadKey()
        End Using
    End Sub
End Module
