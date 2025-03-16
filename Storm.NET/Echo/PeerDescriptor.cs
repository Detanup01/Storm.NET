using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;

namespace StormDotNet.Echo
{
	// Token: 0x020000B0 RID: 176
	public sealed class PeerDescriptor
	{
		// Token: 0x0600035D RID: 861 RVA: 0x0000AF14 File Offset: 0x00009114
		internal static void Initialize()
		{
			List<GCHandle> list = PeerDescriptor.gcHandles;
			lock (list)
			{
				PeerDescriptor.Native.CreatePeerDescriptor createPeerDescriptor = new PeerDescriptor.Native.CreatePeerDescriptor(PeerDescriptor.CreatePeerDescriptor);
				GCHandle gchandle = GCHandle.Alloc(createPeerDescriptor, GCHandleType.Normal);
				PeerDescriptor.gcHandles.Add(gchandle);
				PeerDescriptor.Native.ReleasePeerDescriptor releasePeerDescriptor = new PeerDescriptor.Native.ReleasePeerDescriptor(PeerDescriptor.ReleasePeerDescriptor);
				gchandle = GCHandle.Alloc(releasePeerDescriptor, GCHandleType.Normal);
				PeerDescriptor.gcHandles.Add(gchandle);
				PeerDescriptor.Native.Initialize(createPeerDescriptor, releasePeerDescriptor);
			}
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000AF94 File Offset: 0x00009194
		internal static void Uninitialize()
		{
			List<GCHandle> list = PeerDescriptor.gcHandles;
			lock (list)
			{
				foreach (GCHandle gchandle in PeerDescriptor.gcHandles)
				{
					gchandle.Free();
				}
				PeerDescriptor.gcHandles.Clear();
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600035F RID: 863 RVA: 0x0000B018 File Offset: 0x00009218
		internal IntPtr Handle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000360 RID: 864 RVA: 0x0000B020 File Offset: 0x00009220
		public PlatformType PlatformType
		{
			get
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				return (PlatformType)PeerDescriptor.Native.GetPlatformType(this.handle);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000361 RID: 865 RVA: 0x0000B046 File Offset: 0x00009246
		public NATType NatType
		{
			get
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				return (NATType)PeerDescriptor.Native.GetNatType(this.handle);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000362 RID: 866 RVA: 0x0000B06C File Offset: 0x0000926C
		public GUID GUID
		{
			get
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				IntPtr zero = IntPtr.Zero;
				byte b = 0;
				PeerDescriptor.Native.GetGUID(this.handle, ref zero, ref b);
				if (b > 0)
				{
					return new GUID(zero, b);
				}
				return GUID.INVALID_GUID;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000363 RID: 867 RVA: 0x0000B0BA File Offset: 0x000092BA
		public ushort DedicatedRouterId
		{
			get
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				return PeerDescriptor.Native.GetDedicatedRouterId(this.handle);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000364 RID: 868 RVA: 0x0000B0E0 File Offset: 0x000092E0
		public ushort DedicatedRouterSubId
		{
			get
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				return PeerDescriptor.Native.GetDedicatedRouterSubId(this.handle);
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000365 RID: 869 RVA: 0x0000B106 File Offset: 0x00009306
		public string PunchGUID
		{
			get
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				return Marshal.PtrToStringAnsi(PeerDescriptor.Native.GetPunchGUID(this.handle));
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000366 RID: 870 RVA: 0x0000B131 File Offset: 0x00009331
		public string GatewayBehavior
		{
			get
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				return Marshal.PtrToStringAnsi(PeerDescriptor.Native.GetGatewayBehavior(this.handle));
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000367 RID: 871 RVA: 0x0000B15C File Offset: 0x0000935C
		public unsafe ReadOnlyCollection<IPEndPoint> AdvertisableAddresses
		{
			get
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				List<IPEndPoint> list = new List<IPEndPoint>();
				int advertisableAddressesCount = PeerDescriptor.Native.GetAdvertisableAddressesCount(this.handle);
				if (advertisableAddressesCount > 0)
				{
					sbyte[] array = new sbyte[advertisableAddressesCount];
					IntPtr[] array2 = new IntPtr[advertisableAddressesCount];
					ushort[] array3 = new ushort[advertisableAddressesCount];
					sbyte[] array4;
					sbyte* ptr;
					if ((array4 = array) == null || array4.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array4[0];
					}
					IntPtr[] array5;
					IntPtr* ptr2;
					if ((array5 = array2) == null || array5.Length == 0)
					{
						ptr2 = null;
					}
					else
					{
						ptr2 = &array5[0];
					}
					ushort[] array6;
					ushort* ptr3;
					if ((array6 = array3) == null || array6.Length == 0)
					{
						ptr3 = null;
					}
					else
					{
						ptr3 = &array6[0];
					}
					PeerDescriptor.Native.GetAdvertisableAddresses(this.handle, new IntPtr((void*)ptr), new IntPtr((void*)ptr2), new IntPtr((void*)ptr3));
					array6 = null;
					array5 = null;
					array4 = null;
					for (int i = 0; i < advertisableAddressesCount; i++)
					{
						list.Add(new IPEndPoint(array[i], array2[i], array3[i]));
					}
				}
				return list.AsReadOnly();
			}
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000B260 File Offset: 0x00009460
		public string SerializeAsStationUrlUserData()
		{
			IntPtr intPtr = Marshal.AllocHGlobal(1024);
			PeerDescriptor.Native.SerializeAsStationUrlUserData(this.Handle, intPtr);
			return Marshal.PtrToStringAnsi(intPtr);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000B28B File Offset: 0x0000948B
		public bool InitializeFromStationUrlUserData(string encodedAnsiString)
		{
			return PeerDescriptor.Native.InitializeFromStationUrlUserData(this.Handle, encodedAnsiString);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000B299 File Offset: 0x00009499
		public bool IsReadyForMatchMaking()
		{
			return PeerDescriptor.Native.IsReadyForMatchMaking(this.Handle);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000B2A6 File Offset: 0x000094A6
		private PeerDescriptor(IntPtr handle)
		{
			this.handle = handle;
			this.isManaged = false;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000B2BC File Offset: 0x000094BC
		public PeerDescriptor()
		{
			GCHandle gchandle = GCHandle.Alloc(this, GCHandleType.Weak);
			this.handle = PeerDescriptor.Native.Create(GCHandle.ToIntPtr(gchandle));
			this.gcHandle = gchandle;
			this.isManaged = true;
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000B2F8 File Offset: 0x000094F8
		public PeerDescriptor(PeerDescriptor peerDescriptor)
		{
			if (peerDescriptor.handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("peerDescriptor");
			}
			GCHandle gchandle = GCHandle.Alloc(this, GCHandleType.Weak);
			this.handle = PeerDescriptor.Native.Create(GCHandle.ToIntPtr(gchandle));
			PeerDescriptor.Native.Copy(this.handle, peerDescriptor.handle);
			this.gcHandle = gchandle;
			this.isManaged = true;
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000B360 File Offset: 0x00009560
		~PeerDescriptor()
		{
			this.Dispose(false);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000B390 File Offset: 0x00009590
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000B3A0 File Offset: 0x000095A0
		private void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (this.isManaged && this.handle != IntPtr.Zero)
				{
					PeerDescriptor.Native.Destroy(this.handle);
					this.gcHandle.Free();
				}
				this.disposed = true;
			}
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000B3F0 File Offset: 0x000095F0
		[MonoPInvokeCallback(typeof(PeerDescriptor.Native.CreatePeerDescriptor))]
		private static IntPtr CreatePeerDescriptor(IntPtr peerDescriptor)
		{
			if (peerDescriptor == IntPtr.Zero)
			{
				throw new ArgumentNullException("peerDescriptor");
			}
			PeerDescriptor peerDescriptor2 = new PeerDescriptor(peerDescriptor);
			GCHandle gchandle = GCHandle.Alloc(peerDescriptor2, GCHandleType.Normal);
			peerDescriptor2.gcHandle = gchandle;
			return GCHandle.ToIntPtr(gchandle);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000B430 File Offset: 0x00009630
		[MonoPInvokeCallback(typeof(PeerDescriptor.Native.ReleasePeerDescriptor))]
		private static void ReleasePeerDescriptor(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("handle");
			}
			GCHandle gchandle = GCHandle.FromIntPtr(handle);
			PeerDescriptor peerDescriptor = gchandle.Target as PeerDescriptor;
			if (peerDescriptor == null)
			{
				throw new ArgumentException("The handle does not point to a valid peer descriptor.", "handle");
			}
			peerDescriptor.Dispose();
			gchandle.Free();
		}

		// Token: 0x04000267 RID: 615
		private static readonly List<GCHandle> gcHandles = new List<GCHandle>();

		// Token: 0x04000268 RID: 616
		private IntPtr handle;

		// Token: 0x04000269 RID: 617
		private GCHandle gcHandle;

		// Token: 0x0400026A RID: 618
		private bool disposed;

		// Token: 0x0400026B RID: 619
		private bool isManaged;

		// Token: 0x020000B1 RID: 177
		private static class Native
		{
			// Token: 0x06000374 RID: 884
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "PeerDescriptor_Initialize")]
			public static extern void Initialize(PeerDescriptor.Native.CreatePeerDescriptor createPeerDescriptor, PeerDescriptor.Native.ReleasePeerDescriptor releasePeerDescriptor);

			// Token: 0x06000375 RID: 885
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "PeerDescriptor_Create")]
			public static extern IntPtr Create(IntPtr peerDescriptor);

			// Token: 0x06000376 RID: 886
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "PeerDescriptor_Copy")]
			public static extern void Copy(IntPtr peerDescriptorDest, IntPtr peerDescriptorSrc);

			// Token: 0x06000377 RID: 887
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "PeerDescriptor_Destroy")]
			public static extern void Destroy(IntPtr peerDescriptor);

			// Token: 0x06000378 RID: 888
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "PeerDescriptor_GetPlatformType")]
			public static extern byte GetPlatformType(IntPtr peerDescriptor);

			// Token: 0x06000379 RID: 889
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "PeerDescriptor_GetNatType")]
			public static extern byte GetNatType(IntPtr peerDescriptor);

			// Token: 0x0600037A RID: 890
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "PeerDescriptor_GetGatewayBehavior")]
			public static extern IntPtr GetGatewayBehavior(IntPtr peerDescriptor);

			// Token: 0x0600037B RID: 891
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "PeerDescriptor_GetPunchGUID")]
			public static extern IntPtr GetPunchGUID(IntPtr peerDescriptor);

			// Token: 0x0600037C RID: 892
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "PeerDescriptor_GetDedicatedRouterId")]
			public static extern ushort GetDedicatedRouterId(IntPtr peerDescriptor);

			// Token: 0x0600037D RID: 893
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "PeerDescriptor_GetDedicatedRouterSubId")]
			public static extern ushort GetDedicatedRouterSubId(IntPtr peerDescriptor);

			// Token: 0x0600037E RID: 894
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "PeerDescriptor_GetGUID")]
			public static extern void GetGUID(IntPtr peerDescriptor, ref IntPtr buffer, ref byte bufferLength);

			// Token: 0x0600037F RID: 895
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "PeerDescriptor_GetAdvertisableAddressesCount")]
			public static extern int GetAdvertisableAddressesCount(IntPtr peerDescriptor);

			// Token: 0x06000380 RID: 896
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "PeerDescriptor_GetAdvertisableAddresses")]
			public static extern void GetAdvertisableAddresses(IntPtr peerDescriptor, IntPtr addressFamilies, IntPtr addresses, IntPtr ports);

			// Token: 0x06000381 RID: 897
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "PeerDescriptor_SerializeAsStationUrlUserData")]
			public static extern int SerializeAsStationUrlUserData(IntPtr peerDescriptor, IntPtr url);

			// Token: 0x06000382 RID: 898
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "PeerDescriptor_InitializeFromStationUrlUserData")]
			public static extern bool InitializeFromStationUrlUserData(IntPtr peerDescriptor, string encodedAnsiString);

			// Token: 0x06000383 RID: 899
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "PeerDescriptor_IsReadyForMatchMaking")]
			public static extern bool IsReadyForMatchMaking(IntPtr peerDescriptor);

			// Token: 0x020000B2 RID: 178
			// (Invoke) Token: 0x06000385 RID: 901
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate IntPtr CreatePeerDescriptor(IntPtr peerDescriptor);

			// Token: 0x020000B3 RID: 179
			// (Invoke) Token: 0x06000389 RID: 905
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void ReleasePeerDescriptor(IntPtr handle);
		}
	}
}
