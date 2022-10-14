using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
	public interface ITracer
	{
		public void StartTrace();
		public void StopTrace();
		public TraceResult GetTraceResult();
	}
}
