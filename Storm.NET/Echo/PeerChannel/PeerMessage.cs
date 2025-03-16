using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace StormDotNet.Echo.PeerChannel
{
	// Token: 0x020000C5 RID: 197
	public class PeerMessage : DataContainer, IDisposable
	{
		// Token: 0x060003E0 RID: 992 RVA: 0x0000C118 File Offset: 0x0000A318
		internal new static void Initialize()
		{
			List<GCHandle> list = PeerMessage.gcHandles;
			lock (list)
			{
				PeerMessage.Native.CreatePeerMessage createPeerMessage = new PeerMessage.Native.CreatePeerMessage(PeerMessage.CreatePeerMessage);
				GCHandle gchandle = GCHandle.Alloc(createPeerMessage, GCHandleType.Normal);
				PeerMessage.gcHandles.Add(gchandle);
				PeerMessage.Native.ReleasePeerMessage releasePeerMessage = new PeerMessage.Native.ReleasePeerMessage(PeerMessage.ReleasePeerMessage);
				gchandle = GCHandle.Alloc(releasePeerMessage, GCHandleType.Normal);
				PeerMessage.gcHandles.Add(gchandle);
				PeerMessage.Native.Initialize(createPeerMessage, releasePeerMessage);
			}
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000C198 File Offset: 0x0000A398
		internal new static void Uninitialize()
		{
			List<GCHandle> list = PeerMessage.gcHandles;
			lock (list)
			{
				foreach (GCHandle gchandle in PeerMessage.gcHandles)
				{
					gchandle.Free();
				}
				PeerMessage.gcHandles.Clear();
			}
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000C21C File Offset: 0x0000A41C
		protected PeerMessage()
			: base(PeerMessage.Native.CreateContainer())
		{
			this.isManaged = true;
			this.disposed = false;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000C237 File Offset: 0x0000A437
		protected PeerMessage(IntPtr handle)
			: base(handle)
		{
			this.isManaged = false;
			this.disposed = false;
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000C250 File Offset: 0x0000A450
		~PeerMessage()
		{
			this.Dispose(false);
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000C280 File Offset: 0x0000A480
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000C290 File Offset: 0x0000A490
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (this.isManaged && !this.gcHandle.IsAllocated)
				{
					throw new InvalidOperationException("The container has not been initialized by calling InitializeContainer.");
				}
				if (this.isManaged && base.Handle != IntPtr.Zero)
				{
					PeerMessage.Native.DestroyContainer(base.Handle);
					this.gcHandle.Free();
				}
				this.disposed = true;
			}
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000C2FE File Offset: 0x0000A4FE
		protected void InitializeContainer()
		{
			this.gcHandle = GCHandle.Alloc(this, GCHandleType.Normal);
			PeerMessage.Native.InitializeContainer(base.Handle, new StringId(base.GetType().FullName).UniqueID, GCHandle.ToIntPtr(this.gcHandle));
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000C338 File Offset: 0x0000A538
		[MonoPInvokeCallback(typeof(PeerMessage.Native.CreatePeerMessage))]
		private static IntPtr CreatePeerMessage(IntPtr peerMessage, uint netProtocolEntryId)
		{
			if (peerMessage == IntPtr.Zero)
			{
				throw new ArgumentNullException("peerMessage");
			}
			Type type;
			if (!DynamicTypeRegistry.TryGet(netProtocolEntryId, out type))
			{
				throw new ArgumentException("The peer message cannot be found for the specified protocol entry ID. Make sure the type was registered correctly in the DynamicTypeRegistry", "netProtocolEntryId");
			}
			ConstructorInfo constructor = type.GetConstructor(new Type[] { typeof(IntPtr) });
			if (constructor == null)
			{
				throw new MissingMethodException(string.Format("{0} does not implement a valid constructor.", type.FullName));
			}
			return GCHandle.ToIntPtr(GCHandle.Alloc(constructor.Invoke(new object[] { peerMessage }), GCHandleType.Normal));
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000C3D0 File Offset: 0x0000A5D0
		[MonoPInvokeCallback(typeof(PeerMessage.Native.ReleasePeerMessage))]
		private static void ReleasePeerMessage(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("handle");
			}
			GCHandle.FromIntPtr(handle).Free();
		}

		// Token: 0x04000294 RID: 660
		private static readonly List<GCHandle> gcHandles = new List<GCHandle>();

		// Token: 0x04000295 RID: 661
		private GCHandle gcHandle;

		// Token: 0x04000296 RID: 662
		private bool isManaged;

		// Token: 0x04000297 RID: 663
		private bool disposed;

		// Token: 0x020000C6 RID: 198
		private static class Native
		{
			// Token: 0x060003EB RID: 1003
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "PeerMessage_Initialize")]
			public static extern void Initialize(PeerMessage.Native.CreatePeerMessage createPeerMessage, PeerMessage.Native.ReleasePeerMessage releasePeerMessage);

			// Token: 0x060003EC RID: 1004
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "PeerMessage_CreateContainer")]
			public static extern IntPtr CreateContainer();

			// Token: 0x060003ED RID: 1005
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "PeerMessage_DestroyContainer")]
			public static extern void DestroyContainer(IntPtr message);

			// Token: 0x060003EE RID: 1006
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "PeerMessage_InitializeContainer")]
			public static extern void InitializeContainer(IntPtr message, uint netProtocolEntryId, IntPtr handle);

			// Token: 0x020000C7 RID: 199
			// (Invoke) Token: 0x060003F0 RID: 1008
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate IntPtr CreatePeerMessage(IntPtr peerMessage, uint netProtocolEntryId);

			// Token: 0x020000C8 RID: 200
			// (Invoke) Token: 0x060003F4 RID: 1012
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void ReleasePeerMessage(IntPtr handle);
		}
	}
}
