using System;
using System.Runtime.InteropServices;

namespace StormDotNet
{
	// Token: 0x02000044 RID: 68
	public static class OperationRegister
	{
		// Token: 0x0600010C RID: 268 RVA: 0x00004046 File Offset: 0x00002246
		public static ResultRelayer AddPendingOperation(string description, uint delay)
		{
			if (description == null)
			{
				throw new ArgumentNullException("description");
			}
			return new ResultRelayer(OperationRegister.Native.AddPendingOperation(description, delay));
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00004062 File Offset: 0x00002262
		public static void RemovePendingOperation(ResultRelayer resultRelayer)
		{
			if (resultRelayer == null || resultRelayer.Handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("resultRelayer");
			}
			OperationRegister.Native.RemovePendingOperation(resultRelayer.Handle);
		}

		// Token: 0x02000045 RID: 69
		private static class Native
		{
			// Token: 0x0600010E RID: 270
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "OperationRegister_AddPendingOperation")]
			public static extern IntPtr AddPendingOperation([MarshalAs(UnmanagedType.LPStr)] string description, uint delay);

			// Token: 0x0600010F RID: 271
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "OperationRegister_RemovePendingOperation")]
			public static extern void RemovePendingOperation(IntPtr pResultRelayer);
		}
	}
}
