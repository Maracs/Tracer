using Core;

namespace Test;


public class Tests
{

   static public int threadId1;
   static public int threadId2;
    
    internal class Foo
    {
        private Bar _bar;
        private ITracer _tracer;
        

        internal Foo(ITracer tracer)
        {
            _tracer = tracer;
            _bar = new Bar(_tracer);
        }

        public void MyMethod()
        {
            threadId1 = Thread.CurrentThread.ManagedThreadId;
            _tracer.StartTrace();
            Thread.Sleep(100);
            Task.Run(() => _bar.InnerMethod()).Wait();
            _tracer.StopTrace();
        }

        public void MethodWithNoNested()
        {
            _tracer.StartTrace();
            Thread.Sleep(105);
            _tracer.StopTrace();
        }
    }

    public class Bar
    {
        private ITracer _tracer;

        internal Bar(ITracer tracer)
        {
            _tracer = tracer;
        }

        public void InnerMethod()
        {

            threadId2 = Thread.CurrentThread.ManagedThreadId;
            
            _tracer.StartTrace();
            
            Thread.Sleep(200);
            PrivateMethod();
            _tracer.StopTrace();
        }

        private void PrivateMethod()
        {
            _tracer.StartTrace();
            Thread.Sleep(200);
            _tracer.StopTrace();
        }
    }

    private static int GetMillisecondsValue(string ms)
    {
        if (!int.TryParse(ms[..^2], out var value))
        {
            throw new Exception($"Invalid milliseconds value: {ms}");
        }

        return value;
    }
    
    [Test]
    public void SingleThreadTest()
    {
        var tracer = new Tracer();
        var boo = new Bar(tracer);
        boo.InnerMethod();
        var result = tracer.GetTraceResult();
        
        
        
        Assert.That(result.TraceInfo, Has.Count.EqualTo(1));
        Assert.That(result.TraceInfo[Thread.CurrentThread.ManagedThreadId].Methods, Has.Count.EqualTo(1));
        Assert.Multiple(() =>
        {
            Assert.That(result.TraceInfo[Thread.CurrentThread.ManagedThreadId].Methods[0].MethodName, Is.EqualTo("InnerMethod"));
            Assert.That(result.TraceInfo[Thread.CurrentThread.ManagedThreadId].Methods[0].ClassName, Is.EqualTo("Bar"));
            Assert.That(result.TraceInfo[Thread.CurrentThread.ManagedThreadId].Methods[0].TimeMs, Is.GreaterThanOrEqualTo(400));
        });
        Assert.That(result.TraceInfo[Thread.CurrentThread.ManagedThreadId].Methods[0].Methods, Has.Count.EqualTo(1));
        Assert.Multiple(() =>
        {
            Assert.That(result.TraceInfo[Thread.CurrentThread.ManagedThreadId].Methods[0].Methods[0].MethodName, Is.EqualTo("PrivateMethod"));
            Assert.That(result.TraceInfo[Thread.CurrentThread.ManagedThreadId].Methods[0].Methods[0].ClassName, Is.EqualTo("Bar"));
            Assert.That(result.TraceInfo[Thread.CurrentThread.ManagedThreadId].Methods[0].Methods[0].TimeMs, Is.GreaterThanOrEqualTo(200));
        });
        Assert.That(result.TraceInfo[Thread.CurrentThread.ManagedThreadId].Methods[0].Methods[0].Methods, Has.Count.EqualTo(0));
    }

    [Test]
    public void MultiThreadedTest()
    {
        var tracer = new Tracer();
        var foo = new Foo(tracer);
        foo.MethodWithNoNested();
        foo.MyMethod();
        var result = tracer.GetTraceResult();
        
        
        
        
        Assert.That(result.TraceInfo, Has.Count.EqualTo(2));
        Assert.That(result.TraceInfo[threadId2].Methods, Has.Count.EqualTo(1));
        Assert.Multiple(() =>
        {
            Assert.That(result.TraceInfo[threadId2].Methods[0].MethodName, Is.EqualTo("InnerMethod"));
            Assert.That(result.TraceInfo[threadId2].Methods[0].ClassName, Is.EqualTo("Bar"));
            Assert.That(result.TraceInfo[threadId2].Methods[0].TimeMs, Is.GreaterThanOrEqualTo(400));
        });
        Assert.That(result.TraceInfo[threadId2].Methods[0].Methods, Has.Count.EqualTo(1));
        Assert.Multiple(() =>
        {
            Assert.That(result.TraceInfo[threadId2].Methods[0].Methods[0].MethodName, Is.EqualTo("PrivateMethod"));
            Assert.That(result.TraceInfo[threadId2].Methods[0].Methods[0].ClassName, Is.EqualTo("Bar"));
            Assert.That(result.TraceInfo[threadId2].Methods[0].Methods[0].TimeMs, Is.GreaterThanOrEqualTo(200));
        });
        Assert.That(result.TraceInfo[threadId2].Methods[0].Methods[0].Methods, Has.Count.EqualTo(0));
        
        Assert.That(result.TraceInfo[threadId1].Methods, Has.Count.EqualTo(2));
        Assert.That(result.TraceInfo[threadId1].Methods[0].Methods, Has.Count.EqualTo(0));
        Assert.Multiple(() =>
        {
            Assert.That(result.TraceInfo[threadId1].Methods[0].MethodName, Is.EqualTo("MethodWithNoNested"));
            Assert.That(result.TraceInfo[threadId1].Methods[0].ClassName, Is.EqualTo("Foo"));
            Assert.That(result.TraceInfo[threadId1].Methods[0].TimeMs, Is.GreaterThanOrEqualTo(105));
        });
        
        Assert.That(result.TraceInfo[Thread.CurrentThread.ManagedThreadId].Methods[1].Methods, Has.Count.EqualTo(0));
        Assert.Multiple(() =>
        {
            Assert.That(result.TraceInfo[Thread.CurrentThread.ManagedThreadId].Methods[1].MethodName, Is.EqualTo("MyMethod"));
            Assert.That(result.TraceInfo[Thread.CurrentThread.ManagedThreadId].Methods[1].ClassName, Is.EqualTo("Foo"));
            Assert.That(result.TraceInfo[Thread.CurrentThread.ManagedThreadId].Methods[1].TimeMs, Is.GreaterThanOrEqualTo(300));
        });
    }
}