using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
	public class TraceResult
	{
		
		IReadOnlyDictionary<int, ThreadInformation> _traceInfo;

		public IReadOnlyDictionary<int, ThreadInformation> TraceInfo {
			get { return _traceInfo; }
		}
		
		public TraceResult( Dictionary<int, ThreadInformation> traceInfo )
		{
			_traceInfo = traceInfo;
		}

		
	}
}
