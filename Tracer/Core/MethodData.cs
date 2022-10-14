using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
	public class MethodData
	{
		
		public List<MethodData> Methods { get; set; } = new List<MethodData>();

		

		public long TimeMs { get; set; }

		public string MethodName { get; set; }

		public string ClassName { get; set; }

		

		public override bool Equals( object? md )
		{
			if ( md == null ) return false;
			var md1 = (MethodData)md;
			return MethodName == md1.MethodName &&
				   ClassName == md1.ClassName;				   
		}
	}
}
