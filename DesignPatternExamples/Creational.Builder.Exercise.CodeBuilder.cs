using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coding.Exercise {
    public class ClassObj {
        public string Name;
        public int IndentSize = 2;
        public List<Tuple<string, string>> Attrs = new List<Tuple<string, string>>();
        
        public override string ToString() {
            var sb = new StringBuilder();
            sb.AppendLine($"public class {Name}");
            sb.AppendLine("{");
            foreach (var attr in Attrs) {
                var i = new string(' ', IndentSize);
                sb.AppendLine($"{i}public {attr.Item2} {attr.Item1};");
            }
            sb.AppendLine("}");
            return sb.ToString();
        }
    }

    public class CodeBuilder {
        private ClassObj classObj = new ClassObj();
        private readonly string className;

        public CodeBuilder(string name) {
            className = name;
            classObj.Name = name;
        }

        public CodeBuilder AddField(string name, string type) {
            classObj.Attrs.Add(new Tuple<string, string>(name, type));
            return this;
        }

        public override string ToString() {
            return classObj.ToString();
        }

        public void Clear() {
            classObj = new ClassObj {Name = className};
        }
    }

    public class Demo {
        public static void Main(string[] args) {
            var cb = new CodeBuilder("Person").AddField("Name", "string").AddField("Age", "int");
            Console.WriteLine(cb);
        }
    }
}