using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Prometric_Test
{

    class Program
    {
        static void Main(string[] args)
        {
            List<Shape> ourShapes = PopulateShapes();
            Console.WriteLine("\n#================================#");
            Console.WriteLine("    Shapes have been created    ");
            Console.WriteLine("#================================#\n");


            int i = 1;
            foreach (var s in ourShapes)
            {

                Console.WriteLine(i + ") " + $"{s.name}: Area = {s.Area()}; " + $"Perimeter = {s.Perimeter()};");
                i++;
            }

            Console.WriteLine("\n#================================#");
            Console.WriteLine("     Shapes ordered by Area     ");
            Console.WriteLine("#================================#\n");

            var orderedByArea = ourShapes.OrderBy(o => o.Area()).ToList();

            foreach (var s in orderedByArea)
            {
                Console.WriteLine(i + ") " + $"{s.name}: Area = {s.Area()}; " + $"Perimeter = {s.Perimeter()};");
                i++;
            }

            Console.WriteLine("\n#================================#");
            Console.WriteLine("     Shapes ordered by Perimeter   ");
            Console.WriteLine("#================================#\n");

            // Create a temp variable to store the array and soft the array based on its Perimeter using Lamba expression
            var orderedByPerimeter = ourShapes.OrderBy(p => p.Perimeter()).ToList();

            //Print ordered List
            foreach (var s in orderedByPerimeter)
            {
                Console.WriteLine(i + ") " + $"{s.name}: Area = {s.Area()}; " + $"Perimeter = {s.Perimeter()};");
                i++;
            }

            Console.WriteLine("\n#================================#");
            Console.WriteLine("    Shapes now in Json Format    ");
            Console.WriteLine("#================================#\n");


            var jsonData = JsonConvert.SerializeObject(ourShapes);

            String path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JSON.txt");

            File.WriteAllText(path, jsonData);

            Console.WriteLine("JSON file is being read and stored in Directory as JSON.txt \n\n" + File.ReadAllText("JSON.txt"));

            Console.WriteLine("\n#================================#");
            Console.WriteLine("    Number of Instaces of Shape    ");
            Console.WriteLine("#================================#\n");

            Console.WriteLine(Shape.Instance);
        }

        private static List<Shape> PopulateShapes()
        {
            var shapes = new List<Shape>();

            //Shapes Generated
            Circle circle = new Circle(5);
            Triangle equilateralTriangle = new Triangle(10, 10, 10);
            Triangle isoscelesTriangle = new Triangle(15, 15, 5);
            Triangle scaleneTriangle = new Triangle(10, 8, 6);
            Quadrilaterals square = new Quadrilaterals(10, 10);
            Quadrilaterals rectangle = new Quadrilaterals(10, 6);

            // Add shapes to list of type Shape
            shapes.Add(circle);
            shapes.Add(equilateralTriangle);
            shapes.Add(isoscelesTriangle);
            shapes.Add(scaleneTriangle);
            shapes.Add(square);
            shapes.Add(rectangle);

            return shapes;
        }
    }

    public abstract class Shape
    {
        public string name;
        public static int Instance;

        public abstract double Area();
        public abstract double Perimeter();

    }

    public class Circle : Shape
    {
        public const double PI = 3.14;
        private double radius;

        public Circle(double radius)
        {
            this.name = "Circle";
            this.radius = radius;

            Instance++;
        }

        public string Name
        {
            get { return this.name; }
        }

        public override double Area()
        {
            return radius * PI;
        }

        public override double Perimeter()
        {

            return 2 * radius * PI;
        }

    }

    public class Triangle : Shape
    {
        private double a;
        private double b;
        private double c;

        public Triangle(double a, double b, double c)
        {
            if (a == b && b == c)
            {
                this.name = "Equilateral";
            }

            if (a == b || b == c || a == c)
            {
                this.name = "Isosceles";
            }

            if (a != b && b != c && a != c)
            {
                this.name = "Scalene";
            }

            this.a = a;
            this.b = b;
            this.c = c;

            Instance++;
        }

        public string Name
        {
            get { return this.name; }
        }

        public override double Area()
        {
            return CalculateHeight();
        }

        public override double Perimeter()
        {

            return a + b + c;
        }

        private double CalculateHeight()
        {
            var s = (a + b + c) / 2;

            return Math.Sqrt(s * (s - a) * (s - b) * (s - c));
        }

    }

    public class Quadrilaterals : Shape
    {
        private double length;
        private double width;

        public Quadrilaterals(double length, double width)
        {
            if (length == width)
            {
                this.name = "Square";
            }
            else
            {
                this.name = "Rectangle";
            }

            Instance++;
        }

        public string Name
        {
            get { return this.name; }
        }

        public override double Area()
        {
            return width * length;
        }

        public override double Perimeter()
        {
            return (width + length) * 2;
        }
    }
}