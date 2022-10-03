using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
	internal interface ITracer
	{
		public void Start();
		public void Stop();
		public TraceResult Result();
	}
}
