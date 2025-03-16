using System;
using System.Runtime.InteropServices;

namespace StormDotNet
{
	// Token: 0x0200003D RID: 61
	public class ConnectivityFacade
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00003B47 File Offset: 0x00001D47
		public NATType NatType
		{
			get
			{
				return (NATType)ConnectivityFacade.Native.GetNatType();
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00003B4E File Offset: 0x00001D4E
		public bool IsConnectedToRouter
		{
			get
			{
				return ConnectivityFacade.Native.IsConnectedToRouter();
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00003B55 File Offset: 0x00001D55
		public void SetUsingPunchDetectFromUbiServices(bool value)
		{
			ConnectivityFacade.Native.SetUsingPunchDetectFromUbiServices(value);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00003B5D File Offset: 0x00001D5D
		public void SetUsingPunchTraversalFromUbiServices(bool value)
		{
			ConnectivityFacade.Native.SetUsingPunchTraversalFromUbiServices(value);
		}

		// Token: 0x0200003E RID: 62
		private static class Native
		{
			// Token: 0x060000F1 RID: 241
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "ConnectionState_IsConnectedToRouter")]
			[return: MarshalAs(UnmanagedType.U1)]
			public static extern bool IsConnectedToRouter();

			// Token: 0x060000F2 RID: 242
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "ConnectionState_GetNatType")]
			public static extern int GetNatType();

			// Token: 0x060000F3 RID: 243
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "ConnectionState_SetUsingPunchDetectFromUbiServices")]
			[return: MarshalAs(UnmanagedType.U1)]
			public static extern void SetUsingPunchDetectFromUbiServices(bool value);

			// Token: 0x060000F4 RID: 244
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "ConnectionState_SetUsingPunchTraversalFromUbiServices")]
			[return: MarshalAs(UnmanagedType.U1)]
			public static extern void SetUsingPunchTraversalFromUbiServices(bool value);
		}
	}
}
