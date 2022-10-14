using System.Reflection;
using Core;
using Serialization.Abstraction;
using static System.Threading.Thread;


namespace Example
{
    public class Foo
    {
        private readonly Bar _bar;
        private readonly Tracer _tracer;

        internal Foo(Tracer tracer)
        {
            _tracer = tracer;
            _bar = new Bar(_tracer);
        }

        public void MyMethod()
        {
            _tracer.StartTrace();
            Sleep(100);
            _bar.InnerMethod();
            PrivateMethod();
            _tracer.StopTrace();
        }

        private void PrivateMethod()
        {
            _tracer.StartTrace();
            Sleep(105);
            _tracer.StopTrace();
        }
    }

    public class Bar
    {
        private readonly Tracer _tracer;

        internal Bar(Tracer tracer)
        {
            _tracer = tracer;
        }

        public void InnerMethod()
        {
            _tracer.StartTrace();
            Sleep(200);
            _tracer.StopTrace();
        }
    }

    public static class Program
    {
        private static void Main()
        {
            var tracer = new Tracer();
            var foo = new Foo(tracer);
            var task = Task.Run(() => foo.MyMethod());
            foo.MyMethod();
            foo.MyMethod();
            task.Wait();
            var result = tracer.GetTraceResult();
          
            var files = Directory.EnumerateFiles(".\\Tracer.Serialization", "*.dll");
            foreach (var file in files)
            {
                var serializerAssembly = Assembly.LoadFrom(file);
                var types = serializerAssembly.GetTypes();
                foreach (var type in types)
                {
                    if (type.GetInterface(nameof(ITraceResultSerializer)) == null) 
                        continue;
                    var serializer = (ITraceResultSerializer?)Activator.CreateInstance(type);
                    if (serializer == null)
                    {
                        throw new Exception($"Serializer {type.ToString()} not created");
                    }
                    using var fileStream = new FileStream($"result{type.ToString()}.txt", FileMode.Create);
                    serializer.Serialize(result, fileStream);
                }
            }
        }
    }
}
