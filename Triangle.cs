using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triangle1
{
    class Triangle1
    {
        public double A;
        public double B;
        public double C;
        public double H;

        public Triangle1() { }

        public Triangle1(double a)
        {
            A = a;
            B = a;
            C = a;
        }

        public Triangle1(double A, double B, double C)
        {
            this.A = A;
            this.B = B;
            this.C = C;
        }

        public Triangle1(double a, double ha)
        {
            A = a;
            H = ha;
            B = a;
            C = Math.Sqrt(A * A + H * H); // Используем теорему Пифагора для вычисления гипотенузы
        }

        // Метод для вычисления площади треугольника (основание и высота)
        public double Area1()
        {
            return (A * H) / 2;
        }
        public double Perimeter() // Метод для вычисления периметра треугольника
        {
            return A + B + C;
        }
        public double Area()
        {
            double semiPerimeter = Perimeter() / 2; // Полупериметр
            return Math.Sqrt(semiPerimeter * (semiPerimeter - A) * (semiPerimeter - B) * (semiPerimeter - C));
        }


        // Метод для вычисления типа треугольника по сторонам
        public string GetTriangleType()
        {
            if (A == B && B == C) return "Võrdkülgne";
            if (A == B || B == C || A == C) return "Võrdhaarsed";
            if (Math.Pow(A, 2) + Math.Pow(B, 2) == Math.Pow(C, 2)) return "Ristkülikukujuline";
            if (Math.Pow(A, 2) + Math.Pow(B, 2) < Math.Pow(C, 2)) return "nüri";
            if (Math.Pow(A, 2) + Math.Pow(B, 2) > Math.Pow(C, 2)) return "Teravnurkne";
            return "Mitmekülgne";
        }

        // Метод для вычисления типа треугольника по основанию и высоте
        public string GetTriangleTypeFromBaseAndHeight()
        {
            // Вычисляем гипотенузу по Пифагору для прямоугольного треугольника
            double baseHalf = A / 2; // Половина основания
            double c = Math.Sqrt(baseHalf * baseHalf + H * H); // Гипотенуза

            if (Math.Abs(A - c) < 0.0001 && Math.Abs(H - c) < 0.0001) return "Võrdkülgne"; // Равносторонний
            if (Math.Abs(A - c) < 0.0001 || Math.Abs(H - c) < 0.0001 || Math.Abs(baseHalf - H) < 0.0001) return "Võrdhaarsed"; // Равнобедренный

            double sumOfSquares = Math.Pow(baseHalf, 2) + Math.Pow(H, 2);
            double squareOfHypotenuse = Math.Pow(c, 2);

            if (Math.Abs(sumOfSquares - squareOfHypotenuse) < 0.0001) return "Ristkülikukujuline"; // Прямоугольный
            if (sumOfSquares < squareOfHypotenuse) return "nüri"; // Тупоугольный
            return "Teravnurkne"; // Острый
        }

        // Свойства для доступа к сторонам
        public double GetSetA
        {
            get { return A; }
            set { A = value; }
        }

        public double GetSetB
        {
            get { return B; }
            set { B = value; }
        }

        public double GetSetC
        {
            get { return C; }
            set { C = value; }
        }

        // Свойство для проверки существования треугольника
        public bool ExistTriangle
        {
            get
            {
                return (A > 0 && B > 0 && C > 0 && (A + B > C) && (A + C > B) && (B + C > A));
            }
        }

        public bool ExistTriangle1
        {
            get
            {
                return A > 0 && H > 0;
            }
        }
    }
}
