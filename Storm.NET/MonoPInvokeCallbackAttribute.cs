using System;

namespace StormDotNet
{
	// Token: 0x02000032 RID: 50
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public sealed class MonoPInvokeCallbackAttribute : Attribute
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x000033F9 File Offset: 0x000015F9
		public MonoPInvokeCallbackAttribute(Type callbackType)
		{
			this.callbackType = callbackType;
		}

		// Token: 0x0400002D RID: 45
		private Type callbackType;
	}
}
