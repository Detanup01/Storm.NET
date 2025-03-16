using System;
using System.Runtime.InteropServices;
using System.Threading;
using StormDotNet.Echo;
using StormDotNet.Echo.Object;
using StormDotNet.Echo.PeerChannel;

namespace StormDotNet
{
	// Token: 0x0200009F RID: 159
	public static class StormSdk
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000332 RID: 818 RVA: 0x00009313 File Offset: 0x00007513
		public static bool Initialized
		{
			get
			{
				return StormSdk.sdkInitialized;
			}
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000931C File Offset: 0x0000751C
		public static bool Initialize()
		{
			if (Interlocked.Increment(ref StormSdk.sdkInitCount) == 1)
			{
				StormSdk.Native.StormSdkInitialize();
				Log.Initialize();
				ResultRelayer.Initialize();
				SessionDescriptor.Initialize();
				PeerDescriptor.Initialize();
				NetObjectDescriptor.Initialize();
				SessionController.Initialize();
				SessionHandler.Initialize();
				VoiceChatController.Initialize();
				PeerChannelController.Initialize();
				ObjectController.Initialize();
				UbiServicesControllerBase.Initialize();
				DataContainer.Initialize();
				PeerMessage.Initialize();
				INetProperty.Initialize();
				NetObject.Initialize();
				NetObjectHandler.Initialize();
				NetObjectMemento.Initialize();
				NetObjectMessage.Initialize();
				StatsController.Initialize();
				LDNController.Initialize();
				StormSdk.sdkInitialized = true;
			}
			return StormSdk.sdkInitialized;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x000093B0 File Offset: 0x000075B0
		public static bool Uninitialize()
		{
			if (Interlocked.Decrement(ref StormSdk.sdkInitCount) == 0)
			{
				GC.Collect();
				GC.WaitForPendingFinalizers();
				LDNController.Uninitialize();
				StatsController.Uninitialize();
				NetObjectMessage.Uninitialize();
				NetObjectMemento.Uninitialize();
				NetObjectHandler.Uninitialize();
				NetObject.Uninitialize();
				INetProperty.Uninitialize();
				PeerMessage.Uninitialize();
				DataContainer.Uninitialize();
				UbiServicesControllerBase.Uninitialize();
				ObjectController.Uninitialize();
				PeerChannelController.Uninitialize();
				VoiceChatController.Uninitialize();
				SessionController.Uninitialize();
				SessionHandler.Uninitialize();
				NetObjectDescriptor.Uninitialize();
				PeerDescriptor.Uninitialize();
				SessionDescriptor.Uninitialize();
				ResultRelayer.Uninitialize();
				StormSdk.Native.StormSdkUninitialize();
				StormSdk.sdkInitialized = false;
			}
			return !StormSdk.sdkInitialized;
		}

		// Token: 0x040000BF RID: 191
		private static volatile int sdkInitCount;

		// Token: 0x040000C0 RID: 192
		private static volatile bool sdkInitialized;

		// Token: 0x020000A0 RID: 160
		private static class Native
		{
			// Token: 0x06000335 RID: 821
			[DllImport("Storm", CharSet = CharSet.Ansi)]
			public static extern void StormSdkInitialize();

			// Token: 0x06000336 RID: 822
			[DllImport("Storm", CharSet = CharSet.Ansi)]
			public static extern void StormSdkUninitialize();
		}
	}
}
