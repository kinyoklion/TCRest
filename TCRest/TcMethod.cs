using System;

namespace TCRest
{
	public class TcMethod
	{
		public string MethodId { private set; get; }

		public TcMethod(string methodId)
		{
				MethodId = methodId;
		}
	}
}

