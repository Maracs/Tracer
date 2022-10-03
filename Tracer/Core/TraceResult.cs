using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
	public class TraceResult
	{
		//IReadOnlyList<MethodData> _traceInfo;
		IReadOnlyDictionary<int, List<MethodData>> _traceInfo;

		public IReadOnlyDictionary<int, List<MethodData>> TraceInfo {
			get { return _traceInfo; }
		}
		
		public TraceResult( Dictionary<int, List<MethodData>> traceInfo )
		{
			_traceInfo = traceInfo;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			foreach(int key in _traceInfo.Keys){
				sb.AppendLine( key.ToString() );
				foreach ( MethodData value in _traceInfo[ key ] )
				{
					sb.Append( value.ClassName + " " );
					sb.Append( value.MethodName + " " );
					sb.Append( value.TimeMs + " " );
				}

			}
			sb.AppendLine( "\n" );
			return sb.ToString();
		}
	}
}
