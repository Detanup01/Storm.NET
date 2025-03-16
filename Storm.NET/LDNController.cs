using System;
using System.Runtime.InteropServices;

namespace StormDotNet
{
	// Token: 0x0200003F RID: 63
	public static class LDNController
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00003B6D File Offset: 0x00001D6D
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x00003B74 File Offset: 0x00001D74
		private static ulong DiscoveredNetworksCount { get; set; }

		// Token: 0x060000F7 RID: 247 RVA: 0x00003B7C File Offset: 0x00001D7C
		internal static void Initialize()
		{
			LDNController.DiscoveredNetworksCount = 10UL;
			LDNController.discoveredNetworksList = Marshal.AllocHGlobal((int)LDNController.DiscoveredNetworksCount * IntPtr.Size);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00003B9C File Offset: 0x00001D9C
		internal static void Uninitialize()
		{
			if (LDNController.discoveredNetworksList != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(LDNController.discoveredNetworksList);
			}
			LDNController.discoveredNetworksList = IntPtr.Zero;
		}

		// Token: 0x04000055 RID: 85
		private static IntPtr discoveredNetworksList = IntPtr.Zero;
	}
}
