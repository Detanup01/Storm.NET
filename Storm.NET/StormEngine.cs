using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using StormDotNet.Echo;
using StormDotNet.Punch;

namespace StormDotNet
{
	// Token: 0x0200009B RID: 155
	public static class StormEngine
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000300 RID: 768 RVA: 0x00008A1C File Offset: 0x00006C1C
		public static string Version
		{
			get
			{
				if (StormEngine.version != null)
				{
					return StormEngine.version;
				}
				object obj = StormEngine.versionLock;
				lock (obj)
				{
					if (StormEngine.version == null)
					{
						StormEngine.version = Marshal.PtrToStringAnsi(StormEngine.Native.StormVersion());
					}
				}
				return StormEngine.version;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000301 RID: 769 RVA: 0x00008A80 File Offset: 0x00006C80
		public static ClientParams PunchClientParams
		{
			get
			{
				if (StormEngine.punchClientParams == null)
				{
					object obj = StormEngine.punchClientParamsLock;
					lock (obj)
					{
						if (StormEngine.punchClientParams == null)
						{
							if (!StormEngine.Native.IsPunchSupported())
							{
								throw new NotSupportedException("Punch is not supported by the Storm library.");
							}
							IntPtr intPtr = Marshal.AllocHGlobal(ClientParams.GetNativeDataSize());
							StormEngine.punchClientParams = new ClientParams(StormEngine.Native.GetPunchClientParams(intPtr), intPtr);
							Marshal.FreeHGlobal(intPtr);
						}
					}
				}
				return StormEngine.punchClientParams;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000302 RID: 770 RVA: 0x00008B08 File Offset: 0x00006D08
		public static SEngineStartUpParams EchoStartupParams
		{
			get
			{
				if (StormEngine.echoStartupParams == null)
				{
					object obj = StormEngine.echoStartupParamsLock;
					lock (obj)
					{
						if (StormEngine.echoStartupParams == null)
						{
							IntPtr intPtr = Marshal.AllocHGlobal(SEngineStartUpParams.GetNativeDataSize());
							StormEngine.echoStartupParams = new SEngineStartUpParams(StormEngine.Native.GetEchoStartupParams(intPtr), intPtr);
							Marshal.FreeHGlobal(intPtr);
						}
					}
				}
				return StormEngine.echoStartupParams;
			}
		}

		// Token: 0x17000035 RID: 53
		// (set) Token: 0x06000303 RID: 771 RVA: 0x00008B80 File Offset: 0x00006D80
		private static PeerDescriptor LocalPeerDescriptor
		{
			set
			{
				using (StormEngine.SetProfilePoint(null))
				{
					object obj = StormEngine.usingLocalpeerDescriptor;
					lock (obj)
					{
						if (StormEngine.localPeerDescriptor != null)
						{
							StormEngine.localPeerDescriptor.Dispose();
						}
						if (value == null)
						{
							StormEngine.localPeerDescriptor = null;
						}
						else
						{
							StormEngine.localPeerDescriptor = new PeerDescriptor(value);
						}
					}
				}
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000304 RID: 772 RVA: 0x00008C00 File Offset: 0x00006E00
		// (set) Token: 0x06000305 RID: 773 RVA: 0x00008C07 File Offset: 0x00006E07
		public static ConnectivityFacade ConnectivityFacade
		{
			get
			{
				return StormEngine.connectivityFacade;
			}
			set
			{
				if (StormEngine.engineInitialized)
				{
					throw new InvalidOperationException("Storm Engine must be uninitialized to change a controller.");
				}
				StormEngine.connectivityFacade = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000306 RID: 774 RVA: 0x00008C21 File Offset: 0x00006E21
		// (set) Token: 0x06000307 RID: 775 RVA: 0x00008C28 File Offset: 0x00006E28
		public static UbiServicesControllerBase UbiServicesController
		{
			get
			{
				return StormEngine.ubiservicesController;
			}
			set
			{
				if (StormEngine.engineInitialized)
				{
					throw new InvalidOperationException("Storm Engine must be uninitialized to change a controller.");
				}
				UbiServicesControllerBase ubiServicesControllerBase = StormEngine.ubiservicesController;
				if (ubiServicesControllerBase != null)
				{
					ubiServicesControllerBase.Dispose();
				}
				StormEngine.ubiservicesController = value;
				StormEngine.Native.SetUbiServicesController((StormEngine.ubiservicesController != null) ? StormEngine.ubiservicesController.Handle : IntPtr.Zero);
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00008C7C File Offset: 0x00006E7C
		public unsafe static bool Initialize(ApplicationDescriptor appDescriptor, Dictionary<string, string> commandLineArgs)
		{
			if (commandLineArgs.Count > 20)
			{
				throw new ArgumentException("Too many command line arguments");
			}
			if (!StormSdk.Initialized)
			{
				throw new InvalidOperationException("Storm SDK must be initialized first.");
			}
			if (StormEngine.engineInitialized)
			{
				throw new InvalidOperationException("Storm Engine cannot be initialized twice.");
			}
			StormEngine.Native.LocalPeerDescriptorUpdatedHandler localPeerDescriptorUpdatedHandler = new StormEngine.Native.LocalPeerDescriptorUpdatedHandler(StormEngine.LocalPeerDescriptorUpdatedHandler);
			GCHandle gchandle = GCHandle.Alloc(localPeerDescriptorUpdatedHandler, GCHandleType.Normal);
			StormEngine.gcHandles.Add(gchandle);
			List<IntPtr> list = new List<IntPtr>();
			List<IntPtr> list2 = new List<IntPtr>();
			foreach (KeyValuePair<string, string> keyValuePair in commandLineArgs)
			{
				list.Add(Marshal.StringToHGlobalAnsi(keyValuePair.Key));
				list2.Add(Marshal.StringToHGlobalAnsi(keyValuePair.Value));
			}
			IntPtr* ptr;
			IntPtr* ptr2;
			checked
			{
				ptr = stackalloc IntPtr[unchecked((UIntPtr)commandLineArgs.Count) * (UIntPtr)sizeof(IntPtr)];
				ptr2 = stackalloc IntPtr[unchecked((UIntPtr)commandLineArgs.Count) * (UIntPtr)sizeof(IntPtr)];
			}
			for (int i = 0; i < commandLineArgs.Count; i++)
			{
				ptr[(IntPtr)i * (IntPtr)sizeof(IntPtr) / (IntPtr)sizeof(IntPtr)] = list[i];
				ptr2[(IntPtr)i * (IntPtr)sizeof(IntPtr) / (IntPtr)sizeof(IntPtr)] = list2[i];
			}
			if (StormEngine.ConnectivityFacade == null)
			{
				StormEngine.ConnectivityFacade = new ConnectivityFacade();
			}
			if (StormEngine.UbiServicesController == null)
			{
				StormEngine.UbiServicesController = new UbiServicesControllerClientBase();
			}
			bool flag = StormEngine.Native.StormInitialize(localPeerDescriptorUpdatedHandler, appDescriptor.ApplicationId, appDescriptor.CompatibilityId, appDescriptor.PersistentDataPath, new IntPtr((void*)ptr), new IntPtr((void*)ptr2), commandLineArgs.Count);
			StormEngine.engineInitialized = flag;
			for (int j = 0; j < commandLineArgs.Count; j++)
			{
				Marshal.FreeHGlobal(list[j]);
				Marshal.FreeHGlobal(list2[j]);
			}
			return flag;
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00008E40 File Offset: 0x00007040
		public static bool Uninitialize()
		{
			if (!StormEngine.engineInitialized)
			{
				throw new InvalidOperationException("Storm Engine cannot be uninitialized twice.");
			}
			List<GCHandle> list = StormEngine.gcHandles;
			lock (list)
			{
				foreach (GCHandle gchandle in StormEngine.gcHandles)
				{
					gchandle.Free();
				}
				StormEngine.gcHandles.Clear();
			}
			StormEngine.punchClientParams = null;
			StormEngine.echoStartupParams = null;
			StormEngine.LocalPeerDescriptor = null;
			GC.Collect();
			GC.WaitForPendingFinalizers();
			bool flag2 = StormEngine.Native.StormUninitialize();
			if (flag2)
			{
				StormEngine.engineInitialized = false;
				UbiServicesControllerBase ubiServicesController = StormEngine.UbiServicesController;
				if (ubiServicesController != null)
				{
					ubiServicesController.Dispose();
				}
				StormEngine.UbiServicesController = null;
				StormEngine.ConnectivityFacade = null;
			}
			return flag2;
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00008F20 File Offset: 0x00007120
		public static void Update()
		{
			using (StormEngine.SetProfilePoint(null))
			{
				StormEngine.Native.StormUpdate();
			}
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00008F58 File Offset: 0x00007158
		public static EResult InitPunch()
		{
			EResult lastResult;
			using (StormEngine.SetProfilePoint(null))
			{
				if (!StormEngine.Native.IsPunchSupported())
				{
					throw new NotSupportedException("Punch is not supported by the Storm library.");
				}
				StormEngine.Native.InitPunch();
				lastResult = EResult.GetLastResult();
			}
			return lastResult;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00008FA8 File Offset: 0x000071A8
		public static EResult StartupPunch(ResultRelayer resultRelayer)
		{
			EResult lastResult;
			using (StormEngine.SetProfilePoint(null))
			{
				if (!StormEngine.Native.IsPunchSupported())
				{
					throw new NotSupportedException("Punch is not supported by the Storm library.");
				}
				StormEngine.Native.StartupPunch(resultRelayer.Handle);
				lastResult = EResult.GetLastResult();
			}
			return lastResult;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00009000 File Offset: 0x00007200
		public static EResult ShutdownPunch()
		{
			EResult lastResult;
			using (StormEngine.SetProfilePoint(null))
			{
				if (!StormEngine.Native.IsPunchSupported())
				{
					throw new NotSupportedException("Punch is not supported by the Storm library.");
				}
				StormEngine.Native.ShutdownPunch();
				lastResult = EResult.GetLastResult();
			}
			return lastResult;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00009050 File Offset: 0x00007250
		public static EResult TeardownPunch()
		{
			EResult lastResult;
			using (StormEngine.SetProfilePoint(null))
			{
				if (!StormEngine.Native.IsPunchSupported())
				{
					throw new NotSupportedException("Punch is not supported by the Storm library.");
				}
				StormEngine.Native.TeardownPunch();
				lastResult = EResult.GetLastResult();
			}
			return lastResult;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x000090A0 File Offset: 0x000072A0
		public static EResult InitEcho(ResultRelayer resultRelayer)
		{
			EResult lastResult;
			using (StormEngine.SetProfilePoint(null))
			{
				StormEngine.Native.InitEcho(resultRelayer.Handle);
				lastResult = EResult.GetLastResult();
			}
			return lastResult;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x000090E4 File Offset: 0x000072E4
		public static EResult StartupEcho(ResultRelayer resultRelayer)
		{
			EResult lastResult;
			using (StormEngine.SetProfilePoint(null))
			{
				StormEngine.Native.StartupEcho(resultRelayer.Handle);
				lastResult = EResult.GetLastResult();
			}
			return lastResult;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00009128 File Offset: 0x00007328
		public static EResult ShutdownEcho()
		{
			EResult lastResult;
			using (StormEngine.SetProfilePoint(null))
			{
				StormEngine.Native.ShutdownEcho();
				lastResult = EResult.GetLastResult();
			}
			return lastResult;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00009168 File Offset: 0x00007368
		public static EResult TeardownEcho()
		{
			EResult lastResult;
			using (StormEngine.SetProfilePoint(null))
			{
				StormEngine.Native.TeardownEcho();
				lastResult = EResult.GetLastResult();
			}
			return lastResult;
		}

		// Token: 0x06000313 RID: 787 RVA: 0x000091A8 File Offset: 0x000073A8
		[MethodImpl(256)]
		public static PerfProfileScope SetProfilePoint(string suffix = null)
		{
			return null;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x000091AB File Offset: 0x000073AB
		public static bool Succeeded(EResult result)
		{
			return result.GetErrorCode() > EResult.ECode.NotAssigned;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x000091B6 File Offset: 0x000073B6
		public static bool Failed(EResult result)
		{
			return result.GetErrorCode() < EResult.ECode.NotAssigned;
		}

		// Token: 0x06000316 RID: 790 RVA: 0x000091C1 File Offset: 0x000073C1
		public static bool SubscribeLocalPeerDescriptorUpdatedEvent(StormEngine.OnLocalPeerDescriptorUpdated handler)
		{
			if (handler == null)
			{
				return false;
			}
			bool flag = StormEngine.localPeerDescriptorDelegates.Contains(handler);
			if (!flag)
			{
				StormEngine.localPeerDescriptorDelegates.Add(handler);
			}
			return !flag;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x000091E4 File Offset: 0x000073E4
		public static bool UnsubscribeLocalPeerDescriptorUpdatedEvent(StormEngine.OnLocalPeerDescriptorUpdated handler)
		{
			if (handler == null)
			{
				return false;
			}
			bool flag = StormEngine.localPeerDescriptorDelegates.Contains(handler);
			if (flag)
			{
				StormEngine.localPeerDescriptorDelegates.Remove(handler);
			}
			return flag;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00009208 File Offset: 0x00007408
		[MonoPInvokeCallback(typeof(StormEngine.Native.LocalPeerDescriptorUpdatedHandler))]
		private static void LocalPeerDescriptorUpdatedHandler(IntPtr peerDescriptorHandle)
		{
			if (peerDescriptorHandle == IntPtr.Zero)
			{
				throw new ArgumentNullException("peerDescriptorHandle");
			}
			PeerDescriptor peerDescriptor = GCHandle.FromIntPtr(peerDescriptorHandle).Target as PeerDescriptor;
			StormEngine.LocalPeerDescriptor = peerDescriptor;
			foreach (StormEngine.OnLocalPeerDescriptorUpdated onLocalPeerDescriptorUpdated in StormEngine.localPeerDescriptorDelegates)
			{
				onLocalPeerDescriptorUpdated(peerDescriptor);
			}
		}

		// Token: 0x040000B0 RID: 176
		private static string version = null;

		// Token: 0x040000B1 RID: 177
		private static object versionLock = new object();

		// Token: 0x040000B2 RID: 178
		private static PlatformType platformType = (PlatformType)(-1);

		// Token: 0x040000B3 RID: 179
		private static object platformLock = new object();

		// Token: 0x040000B4 RID: 180
		private static bool engineInitialized = false;

		// Token: 0x040000B5 RID: 181
		private static volatile ClientParams punchClientParams = null;

		// Token: 0x040000B6 RID: 182
		private static object punchClientParamsLock = new object();

		// Token: 0x040000B7 RID: 183
		private static volatile SEngineStartUpParams echoStartupParams = null;

		// Token: 0x040000B8 RID: 184
		private static object echoStartupParamsLock = new object();

		// Token: 0x040000B9 RID: 185
		private static ConnectivityFacade connectivityFacade = null;

		// Token: 0x040000BA RID: 186
		private static UbiServicesControllerBase ubiservicesController = null;

		// Token: 0x040000BB RID: 187
		private static PeerDescriptor localPeerDescriptor = null;

		// Token: 0x040000BC RID: 188
		private static readonly List<GCHandle> gcHandles = new List<GCHandle>();

		// Token: 0x040000BD RID: 189
		private static object usingLocalpeerDescriptor = new object();

		// Token: 0x040000BE RID: 190
		private static List<StormEngine.OnLocalPeerDescriptorUpdated> localPeerDescriptorDelegates = new List<StormEngine.OnLocalPeerDescriptorUpdated>();

		// Token: 0x0200009C RID: 156
		private static class Native
		{
			// Token: 0x0600031A RID: 794
			[DllImport("Storm", CharSet = CharSet.Ansi)]
			public static extern IntPtr StormVersion();

			// Token: 0x0600031B RID: 795
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "StormEngine_SetUbiServicesController")]
			public static extern void SetUbiServicesController(IntPtr controller);

			// Token: 0x0600031C RID: 796
			[DllImport("Storm", CharSet = CharSet.Ansi)]
			[return: MarshalAs(UnmanagedType.U1)]
			public static extern bool StormInitialize(StormEngine.Native.LocalPeerDescriptorUpdatedHandler onLocalPeerDescriptorUpdated, [MarshalAs(UnmanagedType.LPStr)] string applicationId, [MarshalAs(UnmanagedType.LPStr)] string compatibilityId, [MarshalAs(UnmanagedType.LPStr)] string persistentDataPath, IntPtr argumentsKeys, IntPtr argumentsValues, int argumentsSize);

			// Token: 0x0600031D RID: 797
			[DllImport("Storm", CharSet = CharSet.Ansi)]
			[return: MarshalAs(UnmanagedType.U1)]
			public static extern bool StormUninitialize();

			// Token: 0x0600031E RID: 798
			[DllImport("Storm", CharSet = CharSet.Ansi)]
			[return: MarshalAs(UnmanagedType.U1)]
			public static extern bool StormUpdate();

			// Token: 0x0600031F RID: 799
			[DllImport("Storm", CharSet = CharSet.Ansi)]
			public static extern IntPtr GetPunchClientParams(IntPtr clientParams);

			// Token: 0x06000320 RID: 800
			[DllImport("Storm", CharSet = CharSet.Ansi)]
			[return: MarshalAs(UnmanagedType.U1)]
			public static extern bool InitPunch();

			// Token: 0x06000321 RID: 801
			[DllImport("Storm", CharSet = CharSet.Ansi)]
			[return: MarshalAs(UnmanagedType.U1)]
			public static extern bool StartupPunch(IntPtr resultRelayer);

			// Token: 0x06000322 RID: 802
			[DllImport("Storm", CharSet = CharSet.Ansi)]
			[return: MarshalAs(UnmanagedType.U1)]
			public static extern bool ShutdownPunch();

			// Token: 0x06000323 RID: 803
			[DllImport("Storm", CharSet = CharSet.Ansi)]
			[return: MarshalAs(UnmanagedType.U1)]
			public static extern bool TeardownPunch();

			// Token: 0x06000324 RID: 804
			[DllImport("Storm", CharSet = CharSet.Ansi)]
			[return: MarshalAs(UnmanagedType.U1)]
			public static extern bool IsPunchSupported();

			// Token: 0x06000325 RID: 805
			[DllImport("Storm", CharSet = CharSet.Ansi)]
			public static extern IntPtr GetEchoStartupParams(IntPtr startupParams);

			// Token: 0x06000326 RID: 806
			[DllImport("Storm", CharSet = CharSet.Ansi)]
			[return: MarshalAs(UnmanagedType.U1)]
			public static extern bool InitEcho(IntPtr resultRelayer);

			// Token: 0x06000327 RID: 807
			[DllImport("Storm", CharSet = CharSet.Ansi)]
			[return: MarshalAs(UnmanagedType.U1)]
			public static extern bool StartupEcho(IntPtr resultRelayer);

			// Token: 0x06000328 RID: 808
			[DllImport("Storm", CharSet = CharSet.Ansi)]
			[return: MarshalAs(UnmanagedType.U1)]
			public static extern bool ShutdownEcho();

			// Token: 0x06000329 RID: 809
			[DllImport("Storm", CharSet = CharSet.Ansi)]
			[return: MarshalAs(UnmanagedType.U1)]
			public static extern bool TeardownEcho();

			// Token: 0x0200009D RID: 157
			// (Invoke) Token: 0x0600032B RID: 811
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void LocalPeerDescriptorUpdatedHandler(IntPtr peerDescriptorHandle);
		}

		// Token: 0x0200009E RID: 158
		// (Invoke) Token: 0x0600032F RID: 815
		public delegate void OnLocalPeerDescriptorUpdated(PeerDescriptor newpeerDescriptor);
	}
}
