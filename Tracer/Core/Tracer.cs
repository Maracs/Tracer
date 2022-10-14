
using System.Collections.Concurrent;

using System.Diagnostics;


namespace Core
{
	public class Tracer : ITracer
	{
		
        private class TrackedMethod
        {
            public Stopwatch Stopwatch;
            public MethodData MethodInformation;
        }


        private Dictionary<int, ThreadInformation> _trace = new();
		private ConcurrentDictionary<int, Stack<TrackedMethod>>_threads = new();
		
		
		 public void StartTrace()
        {
            var trackedMethod = new TrackedMethod {Stopwatch = new(), MethodInformation = new()};

            var stackFrame = new StackFrame(1);
            trackedMethod.MethodInformation.MethodName = stackFrame.GetMethod()?.Name;
            trackedMethod.MethodInformation.ClassName = stackFrame.GetMethod()?.ReflectedType?.Name;

            var threadId = Thread.CurrentThread.ManagedThreadId;
            if (!_threads.TryGetValue(threadId, out var trackedStack))
            {
                trackedStack = new Stack<TrackedMethod>();
                trackedStack.Push(trackedMethod);
                _threads.TryAdd(threadId, trackedStack);
            }
            else
            {
                if (trackedStack.Count != 0) 
                    trackedStack.Peek().MethodInformation.Methods.Add(trackedMethod.MethodInformation);
                _threads[threadId].Push(trackedMethod);
            }
            
            trackedMethod.Stopwatch.Start();
        }

        public void StopTrace()
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            var trackedMethod = _threads[threadId].Pop();
            
            trackedMethod.Stopwatch.Stop();
            trackedMethod.MethodInformation.TimeMs = trackedMethod.Stopwatch.ElapsedMilliseconds;
            
            if (!_trace.TryGetValue(threadId, out var threadInformation))
            {
                _trace[threadId] = new ThreadInformation() {
                        Id = threadId, 
                        TimeMs = 0, 
                        Methods = new List<MethodData>()
                };
            }

            if (_threads[threadId].Count == 0)
            {
                _trace[threadId].Methods.Add(trackedMethod.MethodInformation);
            }

           
            
            foreach (var method in _trace[threadId].Methods)
            {
                _trace[threadId].TimeMs = _trace[threadId].TimeMs+method.TimeMs;
            }
            
        }

        public TraceResult GetTraceResult()
        {
            TraceResult result = new(_trace);
            return result;
        }


		
	}
}
