using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace StormDotNet.Echo.Object
{
	// Token: 0x020000D0 RID: 208
	public sealed class NetObjectDescriptor : IDisposable
	{
		// Token: 0x06000412 RID: 1042 RVA: 0x0000C694 File Offset: 0x0000A894
		internal static void Initialize()
		{
			List<GCHandle> list = NetObjectDescriptor.gcHandles;
			lock (list)
			{
				NetObjectDescriptor.Native.CreateObjectDescriptor createObjectDescriptor = new NetObjectDescriptor.Native.CreateObjectDescriptor(NetObjectDescriptor.CreateObjectDescriptor);
				GCHandle gchandle = GCHandle.Alloc(createObjectDescriptor, GCHandleType.Normal);
				NetObjectDescriptor.gcHandles.Add(gchandle);
				NetObjectDescriptor.Native.ReleaseObjectDescriptor releaseObjectDescriptor = new NetObjectDescriptor.Native.ReleaseObjectDescriptor(NetObjectDescriptor.ReleaseObjectDescriptor);
				gchandle = GCHandle.Alloc(releaseObjectDescriptor, GCHandleType.Normal);
				NetObjectDescriptor.gcHandles.Add(gchandle);
				NetObjectDescriptor.Native.Initialize(createObjectDescriptor, releaseObjectDescriptor);
			}
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000C714 File Offset: 0x0000A914
		internal static void Uninitialize()
		{
			List<GCHandle> list = NetObjectDescriptor.gcHandles;
			lock (list)
			{
				foreach (GCHandle gchandle in NetObjectDescriptor.gcHandles)
				{
					gchandle.Free();
				}
				NetObjectDescriptor.gcHandles.Clear();
			}
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000C798 File Offset: 0x0000A998
		private NetObjectDescriptor(IntPtr handle)
		{
			this.handle = handle;
			this.isManaged = false;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000C7B0 File Offset: 0x0000A9B0
		~NetObjectDescriptor()
		{
			this.Dispose(false);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000C7E0 File Offset: 0x0000A9E0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000C7F0 File Offset: 0x0000A9F0
		private void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (this.isManaged && this.handle != IntPtr.Zero)
				{
					NetObjectDescriptor.Native.Destroy(this.handle);
					this.gcHandle.Free();
				}
				this.disposed = true;
			}
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000C840 File Offset: 0x0000AA40
		[MonoPInvokeCallback(typeof(NetObjectDescriptor.Native.CreateObjectDescriptor))]
		private static IntPtr CreateObjectDescriptor(IntPtr objectDescriptor)
		{
			if (objectDescriptor == IntPtr.Zero)
			{
				throw new ArgumentNullException("objectDescriptor");
			}
			NetObjectDescriptor netObjectDescriptor = new NetObjectDescriptor(objectDescriptor);
			GCHandle gchandle = GCHandle.Alloc(netObjectDescriptor, GCHandleType.Normal);
			netObjectDescriptor.gcHandle = gchandle;
			return GCHandle.ToIntPtr(gchandle);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000C880 File Offset: 0x0000AA80
		[MonoPInvokeCallback(typeof(NetObjectDescriptor.Native.ReleaseObjectDescriptor))]
		private static void ReleaseObjectDescriptor(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("handle");
			}
			GCHandle gchandle = GCHandle.FromIntPtr(handle);
			NetObjectDescriptor netObjectDescriptor = gchandle.Target as NetObjectDescriptor;
			if (netObjectDescriptor == null)
			{
				throw new ArgumentException("The handle does not point to a valid object descriptor.", "handle");
			}
			netObjectDescriptor.Dispose();
			gchandle.Free();
		}

		// Token: 0x0400029A RID: 666
		private static readonly List<GCHandle> gcHandles = new List<GCHandle>();

		// Token: 0x0400029B RID: 667
		private IntPtr handle;

		// Token: 0x0400029C RID: 668
		private GCHandle gcHandle;

		// Token: 0x0400029D RID: 669
		private bool isManaged;

		// Token: 0x0400029E RID: 670
		private bool disposed;

		// Token: 0x020000D1 RID: 209
		private static class Native
		{
			// Token: 0x0600041B RID: 1051
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "ObjectDescriptor_Initialize")]
			public static extern void Initialize(NetObjectDescriptor.Native.CreateObjectDescriptor createObjectDescriptor, NetObjectDescriptor.Native.ReleaseObjectDescriptor releaseObjectDescriptor);

			// Token: 0x0600041C RID: 1052
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "ObjectDescriptor_Destroy")]
			public static extern void Destroy(IntPtr objectDescriptor);

			// Token: 0x020000D2 RID: 210
			// (Invoke) Token: 0x0600041E RID: 1054
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate IntPtr CreateObjectDescriptor(IntPtr objectDescriptor);

			// Token: 0x020000D3 RID: 211
			// (Invoke) Token: 0x06000422 RID: 1058
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void ReleaseObjectDescriptor(IntPtr handle);
		}
	}
}
