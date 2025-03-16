using System;
using System.Runtime.InteropServices;

namespace StormDotNet.Punch
{
	// Token: 0x020000AB RID: 171
	public sealed class ClientParams
	{
		// Token: 0x0600034D RID: 845 RVA: 0x0000AD00 File Offset: 0x00008F00
		internal static int GetNativeDataSize()
		{
			return Marshal.SizeOf(typeof(ClientParams.Native));
		}

		// Token: 0x1700003D RID: 61
		// (set) Token: 0x0600034E RID: 846 RVA: 0x0000AD11 File Offset: 0x00008F11
		public unsafe ushort TraversalLocalPort
		{
			set
			{
				*this.clientParams.traversalLocalPort = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (set) Token: 0x0600034F RID: 847 RVA: 0x0000AD20 File Offset: 0x00008F20
		public string UpnpPortDescriptionPrefix
		{
			set
			{
				ClientParams.Native.SetStormString(this.clientParams.upnpPortDescriptionPrefix, value);
			}
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000AD34 File Offset: 0x00008F34
		internal ClientParams(IntPtr handle, IntPtr clientParams)
		{
			if (handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("handle");
			}
			if (clientParams == IntPtr.Zero)
			{
				throw new ArgumentNullException("startupParams");
			}
			this.handle = handle;
			this.clientParams = (ClientParams.Native)Marshal.PtrToStructure(clientParams, typeof(ClientParams.Native));
		}

		// Token: 0x04000208 RID: 520
		private IntPtr handle;

		// Token: 0x04000209 RID: 521
		private ClientParams.Native clientParams;

		// Token: 0x020000AC RID: 172
		[StructLayout(0, Pack = 1)]
		private struct Native
		{
			// Token: 0x06000351 RID: 849
			[DllImport("Storm", CharSet = CharSet.Ansi)]
			public static extern void SetStormString(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)] string str);

			// Token: 0x0400020A RID: 522
			public unsafe ushort* traversalLocalPort;

			// Token: 0x0400020B RID: 523
			public unsafe uint* traversalEndpointConnectionTimeout;

			// Token: 0x0400020C RID: 524
			public unsafe uint* traversalServerKeepAlivePeriod;

			// Token: 0x0400020D RID: 525
			public unsafe uint* traversalProbeKeepAlivePeriod;

			// Token: 0x0400020E RID: 526
			public IntPtr upnpPortDescriptionPrefix;
		}
	}
}
