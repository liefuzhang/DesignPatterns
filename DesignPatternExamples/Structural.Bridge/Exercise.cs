using System;

namespace Coding.Exercise {
    public abstract class Shape {
        public IRenderer renderer;
        public Shape(IRenderer renderer) {
            this.renderer = renderer;
        }
        public string Name { get; set; }

        public override string ToString() {
            return $"Drawing {Name} as {renderer.WhatToRenderAs}";
        }
    }

    public class Triangle : Shape {
        public Triangle(IRenderer renderer) : base(renderer) {
            Name = "Triangle";
        }
    }

    public class Square : Shape {
        public Square(IRenderer renderer): base(renderer) {
            Name = "Square";
        }
    }

    public interface IRenderer {
        string WhatToRenderAs { get; }
    }

    public class VectorRenderer : IRenderer {
        public string WhatToRenderAs => "lines";
    }

    public class RasterRenderer : IRenderer {
        public string WhatToRenderAs => "pixels";
    }

    public class exercise {
        public static void Main(string[] args) {
            Console.WriteLine(new Triangle(new RasterRenderer()).ToString());
        }
    }

    // imagine VectorTriangle and RasterTriangle are here too
}

