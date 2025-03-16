using System;
using System.Runtime.InteropServices;

namespace StormDotNet
{
	// Token: 0x02000039 RID: 57
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	public sealed class MonoUnmanagedFunctionPointerAttribute : Attribute
	{
		// Token: 0x060000DC RID: 220 RVA: 0x00003844 File Offset: 0x00001A44
		public MonoUnmanagedFunctionPointerAttribute(CallingConvention callingConvention)
		{
			this.attribute = new UnmanagedFunctionPointerAttribute(callingConvention);
		}

		// Token: 0x0400004B RID: 75
		private UnmanagedFunctionPointerAttribute attribute;
	}
}
