using System;
using System.Runtime.InteropServices;

namespace StormDotNet
{
	// Token: 0x0200002F RID: 47
	public static class LogDeviceContainer
	{
		// Token: 0x060000C1 RID: 193 RVA: 0x000033C3 File Offset: 0x000015C3
		public static void AddLogDevice(LogDevice logDevice)
		{
			if (logDevice == null)
			{
				throw new ArgumentNullException("logDevice");
			}
			LogDeviceContainer.Native.AddLogDevice(logDevice.Handle);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000033DE File Offset: 0x000015DE
		public static void RemoveLogDevice(LogDevice logDevice)
		{
			if (logDevice == null)
			{
				throw new ArgumentNullException("logDevice");
			}
			LogDeviceContainer.Native.RemoveLogDevice(logDevice.Handle);
		}

		// Token: 0x02000030 RID: 48
		private class Native
		{
			// Token: 0x060000C3 RID: 195
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "LogDeviceContainer_AddLogDevice")]
			public static extern void AddLogDevice(IntPtr logDevice);

			// Token: 0x060000C4 RID: 196
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "LogDeviceContainer_RemoveLogDevice")]
			public static extern void RemoveLogDevice(IntPtr logDevice);
		}
	}
}
