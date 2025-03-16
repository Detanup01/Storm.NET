using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using StormDotNet.Echo;

namespace StormDotNet
{
	// Token: 0x02000059 RID: 89
	public class SessionHandler
	{
		// Token: 0x06000185 RID: 389 RVA: 0x00005934 File Offset: 0x00003B34
		internal SessionHandler()
		{
			using (StormEngine.SetProfilePoint(null))
			{
				this.gcHandle = GCHandle.Alloc(this, GCHandleType.Normal);
				this.handle = SessionHandler.Native.CreateSessionHandler(GCHandle.ToIntPtr(this.gcHandle));
				if (this.handle == IntPtr.Zero)
				{
					this.gcHandle.Free();
					throw new ArgumentNullException("handle");
				}
				this.participatingPeersList = Marshal.AllocHGlobal(16 * IntPtr.Size);
				this.joiningPeersList = Marshal.AllocHGlobal(16 * IntPtr.Size);
				this.peerIdsList = Marshal.AllocHGlobal(64);
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00005A2C File Offset: 0x00003C2C
		~SessionHandler()
		{
			this.Dispose(false);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00005A5C File Offset: 0x00003C5C
		internal static void Initialize()
		{
			List<GCHandle> list = SessionHandler.gcHandles;
			lock (list)
			{
				SessionHandler.Native.CreationSucceededHandler creationSucceededHandler = new SessionHandler.Native.CreationSucceededHandler(SessionHandler.CreationSucceededHandler);
				GCHandle gchandle = GCHandle.Alloc(creationSucceededHandler, GCHandleType.Normal);
				SessionHandler.gcHandles.Add(gchandle);
				SessionHandler.Native.CreationFailedHandler creationFailedHandler = new SessionHandler.Native.CreationFailedHandler(SessionHandler.CreationFailedHandler);
				gchandle = GCHandle.Alloc(creationFailedHandler, GCHandleType.Normal);
				SessionHandler.gcHandles.Add(gchandle);
				SessionHandler.Native.JoinSucceededHandler joinSucceededHandler = new SessionHandler.Native.JoinSucceededHandler(SessionHandler.JoinSucceededHandler);
				gchandle = GCHandle.Alloc(joinSucceededHandler, GCHandleType.Normal);
				SessionHandler.gcHandles.Add(gchandle);
				SessionHandler.Native.JoinFailedHandler joinFailedHandler = new SessionHandler.Native.JoinFailedHandler(SessionHandler.JoinFailedHandler);
				gchandle = GCHandle.Alloc(joinFailedHandler, GCHandleType.Normal);
				SessionHandler.gcHandles.Add(gchandle);
				SessionHandler.Native.PeerConnectedHandler peerConnectedHandler = new SessionHandler.Native.PeerConnectedHandler(SessionHandler.PeerConnectedHandler);
				gchandle = GCHandle.Alloc(peerConnectedHandler, GCHandleType.Normal);
				SessionHandler.gcHandles.Add(gchandle);
				SessionHandler.Native.PeerGoneHandler peerGoneHandler = new SessionHandler.Native.PeerGoneHandler(SessionHandler.PeerGoneHandler);
				gchandle = GCHandle.Alloc(peerGoneHandler, GCHandleType.Normal);
				SessionHandler.gcHandles.Add(gchandle);
				SessionHandler.Native.SessionDescriptorUpdatedHandler sessionDescriptorUpdatedHandler = new SessionHandler.Native.SessionDescriptorUpdatedHandler(SessionHandler.SessionDescriptorUpdatedHandler);
				gchandle = GCHandle.Alloc(sessionDescriptorUpdatedHandler, GCHandleType.Normal);
				SessionHandler.gcHandles.Add(gchandle);
				SessionHandler.Native.HostElectedHandler hostElectedHandler = new SessionHandler.Native.HostElectedHandler(SessionHandler.HostElectedHandler);
				gchandle = GCHandle.Alloc(hostElectedHandler, GCHandleType.Normal);
				SessionHandler.gcHandles.Add(gchandle);
				SessionHandler.Native.HostMigrationStartedHandler hostMigrationStartedHandler = new SessionHandler.Native.HostMigrationStartedHandler(SessionHandler.HostMigrationStartedHandler);
				gchandle = GCHandle.Alloc(hostMigrationStartedHandler, GCHandleType.Normal);
				SessionHandler.gcHandles.Add(gchandle);
				SessionHandler.Native.HostMigrationEndedHandler hostMigrationEndedHandler = new SessionHandler.Native.HostMigrationEndedHandler(SessionHandler.HostMigrationEndedHandler);
				gchandle = GCHandle.Alloc(hostMigrationEndedHandler, GCHandleType.Normal);
				SessionHandler.gcHandles.Add(gchandle);
				SessionHandler.Native.SessionLeftHandler sessionLeftHandler = new SessionHandler.Native.SessionLeftHandler(SessionHandler.SessionLeftHandler);
				gchandle = GCHandle.Alloc(sessionLeftHandler, GCHandleType.Normal);
				SessionHandler.gcHandles.Add(gchandle);
				SessionHandler.Native.SessionLeaveHandler sessionLeaveHandler = new SessionHandler.Native.SessionLeaveHandler(SessionHandler.SessionLeaveHandler);
				gchandle = GCHandle.Alloc(sessionLeaveHandler, GCHandleType.Normal);
				SessionHandler.gcHandles.Add(gchandle);
				SessionHandler.Native.SessionDissolvedHandler sessionDissolvedHandler = new SessionHandler.Native.SessionDissolvedHandler(SessionHandler.SessionDissolvedHandler);
				gchandle = GCHandle.Alloc(sessionDissolvedHandler, GCHandleType.Normal);
				SessionHandler.gcHandles.Add(gchandle);
				SessionHandler.Native.Initialize(creationSucceededHandler, creationFailedHandler, joinSucceededHandler, joinFailedHandler, peerConnectedHandler, peerGoneHandler, sessionDescriptorUpdatedHandler, hostElectedHandler, hostMigrationStartedHandler, hostMigrationEndedHandler, sessionLeftHandler, sessionLeaveHandler, sessionDissolvedHandler);
			}
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00005C74 File Offset: 0x00003E74
		internal static void Uninitialize()
		{
			List<GCHandle> list = SessionHandler.gcHandles;
			lock (list)
			{
				foreach (GCHandle gchandle in SessionHandler.gcHandles)
				{
					gchandle.Free();
				}
				SessionHandler.gcHandles.Clear();
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00005CF8 File Offset: 0x00003EF8
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00005D08 File Offset: 0x00003F08
		private void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (this.joiningPeersList != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(this.joiningPeersList);
				}
				if (this.participatingPeersList != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(this.participatingPeersList);
				}
				if (this.peerIdsList != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(this.peerIdsList);
				}
				this.gcHandle.Free();
				this.disposed = true;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00005D88 File Offset: 0x00003F88
		public GUID SessionGUID
		{
			get
			{
				return this.sessionInfo.SessionGUID;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00005D95 File Offset: 0x00003F95
		public uint SessionRefId
		{
			get
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				return this.sessionInfo.SessionRefId;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00005DBB File Offset: 0x00003FBB
		public uint HostPeerId
		{
			get
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				return this.sessionInfo.HostPeerId;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00005DE1 File Offset: 0x00003FE1
		public uint LocalPeerId
		{
			get
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				return this.sessionInfo.LocalPeerId;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00005E08 File Offset: 0x00004008
		public SessionDescriptor SessionDescriptor
		{
			get
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				return GCHandle.FromIntPtr(SessionHandler.Native.GetSessionDescriptor(this.handle)).Target as SessionDescriptor;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00005E4C File Offset: 0x0000404C
		public PeerDescriptor HostPeerDescriptor
		{
			get
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				if (this.mustDestroyPeerDescriptor != 0)
				{
					SessionHandler.Native.DestroyPeerDescriptor(this.hostPeerDescriptor);
				}
				PeerDescriptor peerDescriptor = GCHandle.FromIntPtr(SessionHandler.Native.GetHostPeerDescriptor(this.handle, ref this.mustDestroyPeerDescriptor)).Target as PeerDescriptor;
				this.hostPeerDescriptor = peerDescriptor.Handle;
				return peerDescriptor;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00005EB8 File Offset: 0x000040B8
		public unsafe Dictionary<uint, PeerDescriptor> ParticipatingPeers
		{
			get
			{
				Dictionary<uint, PeerDescriptor> dictionary2;
				using (StormEngine.SetProfilePoint(null))
				{
					if (this.disposed)
					{
						throw new ObjectDisposedException(base.GetType().FullName);
					}
					List<uint> peerIds = this.PeerIds;
					uint num = 0U;
					int num2 = 16;
					byte* ptr = null;
					ptr = stackalloc byte[(UIntPtr)num2];
					while (!SessionHandler.Native.GetParticipatingPeers(this.handle, this.participatingPeersList, new IntPtr((void*)ptr), ref num2) && num < 10U)
					{
						num += 1U;
						num2 = (int)(1.5 * (double)num2);
						ptr = stackalloc byte[(UIntPtr)num2];
						Marshal.FreeHGlobal(this.participatingPeersList);
						this.participatingPeersList = Marshal.AllocHGlobal(num2 * IntPtr.Size);
					}
					Dictionary<uint, PeerDescriptor> dictionary = new Dictionary<uint, PeerDescriptor>();
					for (int i = 0; i < num2; i++)
					{
						PeerDescriptor peerDescriptor = GCHandle.FromIntPtr(Marshal.ReadIntPtr(this.participatingPeersList, i * IntPtr.Size)).Target as PeerDescriptor;
						dictionary.Add(peerIds[i], new PeerDescriptor(peerDescriptor));
					}
					dictionary2 = dictionary;
				}
				return dictionary2;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00005FCC File Offset: 0x000041CC
		public unsafe List<uint> PeerIds
		{
			get
			{
				List<uint> list2;
				using (StormEngine.SetProfilePoint(null))
				{
					if (this.disposed)
					{
						throw new ObjectDisposedException(base.GetType().FullName);
					}
					uint num = 0U;
					uint num2 = 16U;
					while (!SessionHandler.Native.GetPeerIds(this.handle, this.peerIdsList, ref num2) && num < 10U)
					{
						num += 1U;
						num2 = (uint)(1.5 * num2);
						Marshal.FreeHGlobal(this.peerIdsList);
						this.peerIdsList = Marshal.AllocHGlobal((int)(num2 * 4U));
					}
					List<uint> list = new List<uint>();
					uint* ptr = (uint*)this.peerIdsList.ToPointer();
					int num3 = 0;
					while ((long)num3 < (long)((ulong)num2))
					{
						list.Add(ptr[num3]);
						num3++;
					}
					list2 = list;
				}
				return list2;
			}
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000609C File Offset: 0x0000429C
		public EResult CreateSession(ResultRelayer resultRelayer, SessionDescriptor sessionDescriptor, uint sessionTime)
		{
			if (resultRelayer == null || resultRelayer.Handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("resultRelayer");
			}
			if (sessionDescriptor == null || sessionDescriptor.Handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("sessionDescriptor");
			}
			SessionHandler.Native.CreateSession(this.handle, resultRelayer.Handle, sessionTime, sessionDescriptor.Handle);
			return EResult.GetLastResult();
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00006108 File Offset: 0x00004308
		public EResult Join(ResultRelayer resultRelayer, SessionDescriptor sessionDescriptor, PeerDescriptor hostDescriptor)
		{
			if (resultRelayer == null || resultRelayer.Handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("resultRelayer");
			}
			if (sessionDescriptor == null || sessionDescriptor.Handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("sessionDescriptor");
			}
			if (hostDescriptor == null || hostDescriptor.Handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("hostDescriptor");
			}
			SessionHandler.Native.Join(this.handle, resultRelayer.Handle, sessionDescriptor.Handle, hostDescriptor.Handle);
			return EResult.GetLastResult();
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00006198 File Offset: 0x00004398
		public EResult Leave(ResultRelayer resultRelayer)
		{
			if (resultRelayer == null || resultRelayer.Handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("resultRelayer");
			}
			SessionHandler.Native.Leave(this.handle, resultRelayer.Handle);
			return EResult.GetLastResult();
		}

		// Token: 0x06000196 RID: 406 RVA: 0x000061D4 File Offset: 0x000043D4
		[MonoPInvokeCallback(typeof(SessionHandler.Native.CreationSucceededHandler))]
		private static void CreationSucceededHandler(IntPtr handle, IntPtr sessionInfo)
		{
			using (StormEngine.SetProfilePoint(null))
			{
				if (handle == IntPtr.Zero)
				{
					throw new ArgumentNullException("handle");
				}
				if (sessionInfo == IntPtr.Zero)
				{
					throw new ArgumentNullException("sessionInfo");
				}
				SessionHandler sessionHandler = GCHandle.FromIntPtr(handle).Target as SessionHandler;
				if (sessionHandler == null)
				{
					throw new ArgumentNullException("The sessionHandler is not of base type SessionDescriptor.", "sessionHandler");
				}
				sessionHandler.sessionInfo = new SessionInfo(sessionInfo);
				SessionController.NotifySessionCreateSuccess(sessionHandler);
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000626C File Offset: 0x0000446C
		[MonoPInvokeCallback(typeof(SessionHandler.Native.CreationFailedHandler))]
		private static void CreationFailedHandler(IntPtr handle, IntPtr sessionInfo)
		{
			using (StormEngine.SetProfilePoint(null))
			{
				if (handle == IntPtr.Zero)
				{
					throw new ArgumentNullException("handle");
				}
				if (sessionInfo == IntPtr.Zero)
				{
					throw new ArgumentNullException("sessionInfo");
				}
				SessionHandler sessionHandler = GCHandle.FromIntPtr(handle).Target as SessionHandler;
				if (sessionHandler == null)
				{
					throw new ArgumentException("The sessionHandler is not of base type SessionDescriptor.", "sessionHandler");
				}
				sessionHandler.sessionInfo = new SessionInfo(sessionInfo);
				SessionController.NotifySessionCreateFail(sessionHandler);
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00006304 File Offset: 0x00004504
		[MonoPInvokeCallback(typeof(SessionHandler.Native.JoinSucceededHandler))]
		private static void JoinSucceededHandler(IntPtr handle, IntPtr sessionInfo)
		{
			if (handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("handle");
			}
			if (sessionInfo == IntPtr.Zero)
			{
				throw new ArgumentNullException("sessionInfo");
			}
			SessionHandler sessionHandler = GCHandle.FromIntPtr(handle).Target as SessionHandler;
			if (sessionHandler == null)
			{
				throw new ArgumentException("The sessionHandler is not of base type SessionDescriptor.", "sessionHandler");
			}
			sessionHandler.sessionInfo = new SessionInfo(sessionInfo);
			SessionController.NotifySessionJoinSuccess(sessionHandler);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00006378 File Offset: 0x00004578
		[MonoPInvokeCallback(typeof(SessionHandler.Native.JoinFailedHandler))]
		private static void JoinFailedHandler(IntPtr handle, IntPtr sessionInfo)
		{
			using (StormEngine.SetProfilePoint(null))
			{
				if (handle == IntPtr.Zero)
				{
					throw new ArgumentNullException("handle");
				}
				if (sessionInfo == IntPtr.Zero)
				{
					throw new ArgumentNullException("sessionInfo");
				}
				SessionHandler sessionHandler = GCHandle.FromIntPtr(handle).Target as SessionHandler;
				if (sessionHandler == null)
				{
					throw new ArgumentException("The sessionHandler is not of base type SessionDescriptor.", "sessionHandler");
				}
				sessionHandler.sessionInfo = new SessionInfo(sessionInfo);
				SessionController.NotifySessionJoinFail(sessionHandler);
			}
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00006410 File Offset: 0x00004610
		[MonoPInvokeCallback(typeof(SessionHandler.Native.PeerConnectedHandler))]
		private static void PeerConnectedHandler(IntPtr handle, uint peerId)
		{
			if (handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("sessionHandler");
			}
			SessionHandler sessionHandler = GCHandle.FromIntPtr(handle).Target as SessionHandler;
			if (sessionHandler == null)
			{
				throw new ArgumentNullException("The handler is not of base type SessionHandler.", "handler");
			}
			SessionController.NotifySessionPeerConnected(sessionHandler, peerId);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00006464 File Offset: 0x00004664
		[MonoPInvokeCallback(typeof(SessionHandler.Native.PeerGoneHandler))]
		private static void PeerGoneHandler(IntPtr handle, uint peerId, IntPtr reasonPtr, ulong reasonLength, uint reasonCRC)
		{
			if (handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("sessionHandler");
			}
			SessionHandler sessionHandler = GCHandle.FromIntPtr(handle).Target as SessionHandler;
			if (sessionHandler == null)
			{
				throw new ArgumentException("The handler is not of base type SessionHandler.", "handler");
			}
			string text = Marshal.PtrToStringAnsi(reasonPtr, (int)reasonLength);
			StringId stringId = new StringId(reasonCRC);
			SessionController.NotifySessionPeerGone(sessionHandler, peerId, text, stringId);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x000064C8 File Offset: 0x000046C8
		[MonoPInvokeCallback(typeof(SessionHandler.Native.SessionDescriptorUpdatedHandler))]
		private static void SessionDescriptorUpdatedHandler(IntPtr handle, IntPtr sessionDescriptorHandle)
		{
			if (handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("sessionHandler");
			}
			if (sessionDescriptorHandle == IntPtr.Zero)
			{
				throw new ArgumentNullException("sessionDescriptorHandle");
			}
			SessionHandler sessionHandler = GCHandle.FromIntPtr(handle).Target as SessionHandler;
			SessionDescriptor sessionDescriptor = GCHandle.FromIntPtr(sessionDescriptorHandle).Target as SessionDescriptor;
			if (sessionHandler == null)
			{
				throw new ArgumentException("The handler is not of base type SessionHandler.", "handler");
			}
			if (sessionDescriptor == null)
			{
				throw new ArgumentException("The sessionDescriptor is not of base type SessionDescriptor.", "sessionDescriptor");
			}
			SessionController.NotifySessionDescriptorUpdated(sessionHandler, sessionDescriptor);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00006558 File Offset: 0x00004758
		[MonoPInvokeCallback(typeof(SessionHandler.Native.HostElectedHandler))]
		private static void HostElectedHandler(IntPtr handle, uint hostPeerId, uint previousHostPeerId, IntPtr hostDescriptorHandle, IntPtr sessionDescriptorHandle, uint hostMigrationId)
		{
			if (handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("sessionHandler");
			}
			if (hostDescriptorHandle == IntPtr.Zero)
			{
				throw new ArgumentNullException("hostDescriptorHandle");
			}
			if (sessionDescriptorHandle == IntPtr.Zero)
			{
				throw new ArgumentNullException("sessionDescriptorHandle");
			}
			SessionHandler sessionHandler = GCHandle.FromIntPtr(handle).Target as SessionHandler;
			PeerDescriptor peerDescriptor = GCHandle.FromIntPtr(hostDescriptorHandle).Target as PeerDescriptor;
			SessionDescriptor sessionDescriptor = GCHandle.FromIntPtr(sessionDescriptorHandle).Target as SessionDescriptor;
			if (sessionHandler == null)
			{
				throw new ArgumentException("The handler is not of base type SessionHandler.", "handler");
			}
			if (peerDescriptor == null)
			{
				throw new ArgumentException("The hostDescriptor is not of base type PeerDescriptor.", "hostDescriptor");
			}
			if (sessionDescriptor == null)
			{
				throw new ArgumentException("The sessionDescriptor is not of base type SessionDescriptor.", "sessionDescriptor");
			}
			SessionController.NotifySessionHostElected(sessionHandler, hostPeerId, previousHostPeerId, peerDescriptor, sessionDescriptor, hostMigrationId);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00006630 File Offset: 0x00004830
		[MonoPInvokeCallback(typeof(SessionHandler.Native.HostMigrationStartedHandler))]
		private unsafe static void HostMigrationStartedHandler(IntPtr handle, IntPtr reasonPtr, ulong reasonLength, uint reasonCRC, IntPtr connectedPeerGUIDBuffersPtr, IntPtr connectedPeerGUIDSizesPtr, ulong connectedPeerGUIDsCount, IntPtr discoveredPeerGUIDBuffersPtr, IntPtr discoveredPeerGUIDSizesPtr, ulong discoveredPeerGUIDsCount)
		{
			if (handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("sessionHandler");
			}
			SessionHandler sessionHandler = GCHandle.FromIntPtr(handle).Target as SessionHandler;
			if (sessionHandler == null)
			{
				throw new ArgumentException("The handler is not of base type SessionHandler.", "handler");
			}
			string text = Marshal.PtrToStringAnsi(reasonPtr, (int)reasonLength);
			StringId stringId = new StringId(reasonCRC);
			byte** ptr = (byte**)(void*)connectedPeerGUIDBuffersPtr;
			byte* ptr2 = (byte*)(void*)connectedPeerGUIDSizesPtr;
			GUID[] array = new GUID[connectedPeerGUIDsCount];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = new GUID(*(IntPtr*)(ptr + (IntPtr)num * (IntPtr)sizeof(byte*) / (IntPtr)sizeof(byte*)), ptr2[num]);
			}
			byte** ptr3 = (byte**)(void*)discoveredPeerGUIDBuffersPtr;
			byte* ptr4 = (byte*)(void*)discoveredPeerGUIDSizesPtr;
			GUID[] array2 = new GUID[discoveredPeerGUIDsCount];
			for (int num2 = 0; num2 != array2.Length; num2++)
			{
				array2[num2] = new GUID(*(IntPtr*)(ptr3 + (IntPtr)num2 * (IntPtr)sizeof(byte*) / (IntPtr)sizeof(byte*)), ptr4[num2]);
			}
			SessionController.NotifySessionHostMigrationStarted(sessionHandler, text, stringId, array, array2);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00006734 File Offset: 0x00004934
		[MonoPInvokeCallback(typeof(SessionHandler.Native.HostMigrationEndedHandler))]
		private static void HostMigrationEndedHandler(IntPtr handle, uint hostPeerId, uint previousHostPeerId, IntPtr hostDescriptorHandle)
		{
			if (handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("sessionHandler");
			}
			if (hostDescriptorHandle == IntPtr.Zero)
			{
				throw new ArgumentNullException("hostDescriptorHandle");
			}
			SessionHandler sessionHandler = GCHandle.FromIntPtr(handle).Target as SessionHandler;
			PeerDescriptor peerDescriptor = GCHandle.FromIntPtr(hostDescriptorHandle).Target as PeerDescriptor;
			if (sessionHandler == null)
			{
				throw new ArgumentException("The handler is not of base type SessionHandler.", "handler");
			}
			if (peerDescriptor == null)
			{
				throw new ArgumentException("The hostDescriptor is not of base type PeerDescriptor.", "hostDescriptor");
			}
			SessionController.NotifySessionHostMigrationEnded(sessionHandler, hostPeerId, previousHostPeerId, peerDescriptor);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x000067C8 File Offset: 0x000049C8
		[MonoPInvokeCallback(typeof(SessionHandler.Native.SessionLeftHandler))]
		private static void SessionLeftHandler(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("sessionHandler");
			}
			SessionHandler sessionHandler = GCHandle.FromIntPtr(handle).Target as SessionHandler;
			if (sessionHandler == null)
			{
				throw new ArgumentException("The handler is not of base type SessionHandler.", "handler");
			}
			SessionController.NotifySessionLeft(sessionHandler);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00006818 File Offset: 0x00004A18
		[MonoPInvokeCallback(typeof(SessionHandler.Native.SessionLeaveHandler))]
		private static void SessionLeaveHandler(IntPtr handle, IntPtr reasonPtr, ulong reasonLength, uint reasonCRC)
		{
			if (handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("sessionHandler");
			}
			SessionHandler sessionHandler = GCHandle.FromIntPtr(handle).Target as SessionHandler;
			if (sessionHandler == null)
			{
				throw new ArgumentException("The handler is not of base type SessionHandler.", "handler");
			}
			string text = Marshal.PtrToStringAnsi(reasonPtr, (int)reasonLength);
			StringId stringId = new StringId(reasonCRC);
			SessionController.NotifySessionLeave(sessionHandler, text, stringId);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000687C File Offset: 0x00004A7C
		[MonoPInvokeCallback(typeof(SessionHandler.Native.SessionDissolvedHandler))]
		private static void SessionDissolvedHandler(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("sessionHandler");
			}
			SessionHandler sessionHandler = GCHandle.FromIntPtr(handle).Target as SessionHandler;
			if (sessionHandler == null)
			{
				throw new ArgumentException("The handler is not of base type SessionHandler.", "handler");
			}
			SessionController.NotifySessionDissolved(sessionHandler);
		}

		// Token: 0x04000085 RID: 133
		private GCHandle gcHandle;

		// Token: 0x04000086 RID: 134
		private IntPtr handle = IntPtr.Zero;

		// Token: 0x04000087 RID: 135
		private bool disposed;

		// Token: 0x04000088 RID: 136
		private SessionInfo sessionInfo;

		// Token: 0x04000089 RID: 137
		private IntPtr participatingPeersList = IntPtr.Zero;

		// Token: 0x0400008A RID: 138
		private IntPtr joiningPeersList = IntPtr.Zero;

		// Token: 0x0400008B RID: 139
		private IntPtr hostPeerDescriptor = IntPtr.Zero;

		// Token: 0x0400008C RID: 140
		private List<IntPtr> joiningPeersToDestroy = new List<IntPtr>();

		// Token: 0x0400008D RID: 141
		private IntPtr peerIdsList = IntPtr.Zero;

		// Token: 0x0400008E RID: 142
		private byte mustDestroyPeerDescriptor;

		// Token: 0x0400008F RID: 143
		private static readonly List<GCHandle> gcHandles = new List<GCHandle>();

		// Token: 0x0200005A RID: 90
		private struct Native
		{
			// Token: 0x060001A4 RID: 420
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "SessionHandler_Initialize")]
			public static extern void Initialize(SessionHandler.Native.CreationSucceededHandler onCreationSucceeded, SessionHandler.Native.CreationFailedHandler onCreationFailed, SessionHandler.Native.JoinSucceededHandler onJoinSucceeded, SessionHandler.Native.JoinFailedHandler onJoinFailed, SessionHandler.Native.PeerConnectedHandler onPeerConnected, SessionHandler.Native.PeerGoneHandler onPeerGone, SessionHandler.Native.SessionDescriptorUpdatedHandler onSessionDescriptorUpdated, SessionHandler.Native.HostElectedHandler onHostElected, SessionHandler.Native.HostMigrationStartedHandler onHostMigrationStarted, SessionHandler.Native.HostMigrationEndedHandler onHostMigrationEnded, SessionHandler.Native.SessionLeftHandler onSessionLeft, SessionHandler.Native.SessionLeaveHandler onSessionLeave, SessionHandler.Native.SessionDissolvedHandler onSessionDissolved);

			// Token: 0x060001A5 RID: 421
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "SessionHandler_Allocate")]
			public static extern IntPtr CreateSessionHandler(IntPtr handle);

			// Token: 0x060001A6 RID: 422
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "SessionHandler_CreateSession")]
			[return: MarshalAs(UnmanagedType.U1)]
			public static extern bool CreateSession(IntPtr sessionHandler, IntPtr resultRelayer, uint sessionTime, IntPtr sessionDescriptor);

			// Token: 0x060001A7 RID: 423
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "SessionHandler_Join")]
			[return: MarshalAs(UnmanagedType.U1)]
			public static extern bool Join(IntPtr sessionHandler, IntPtr resultRelayer, IntPtr sessionDesciptror, IntPtr hostPeerDescriptor);

			// Token: 0x060001A8 RID: 424
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "SessionHandler_Leave")]
			[return: MarshalAs(UnmanagedType.U1)]
			public static extern bool Leave(IntPtr sessionHandler, IntPtr resultRelayer);

			// Token: 0x060001A9 RID: 425
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "SessionHandler_GetSessionDescriptor")]
			public static extern IntPtr GetSessionDescriptor(IntPtr sessionHandler);

			// Token: 0x060001AA RID: 426
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "SessionHandler_GetHostPeerDescriptor")]
			public static extern IntPtr GetHostPeerDescriptor(IntPtr sessionHandler, ref byte mustDestroyPeerDescriptor);

			// Token: 0x060001AB RID: 427
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "SessionHandler_GetParticipatingPeers")]
			[return: MarshalAs(UnmanagedType.U1)]
			public static extern bool GetParticipatingPeers(IntPtr sessionHandler, IntPtr participatingPeers, IntPtr mustDestroyDescriptors, ref int count);

			// Token: 0x060001AC RID: 428
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "SessionHandler_GetPeerIds")]
			[return: MarshalAs(UnmanagedType.U1)]
			public static extern bool GetPeerIds(IntPtr sessionHandler, IntPtr peerIds, ref uint count);

			// Token: 0x060001AD RID: 429
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "PeerDescriptor_Destroy")]
			public static extern void DestroyPeerDescriptor(IntPtr peerDescriptor);

			// Token: 0x0200005B RID: 91
			// (Invoke) Token: 0x060001AF RID: 431
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void CreationSucceededHandler(IntPtr handle, IntPtr sessionInfo);

			// Token: 0x0200005C RID: 92
			// (Invoke) Token: 0x060001B3 RID: 435
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void CreationFailedHandler(IntPtr handle, IntPtr sessionInfo);

			// Token: 0x0200005D RID: 93
			// (Invoke) Token: 0x060001B7 RID: 439
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void JoinSucceededHandler(IntPtr handle, IntPtr sessionInfo);

			// Token: 0x0200005E RID: 94
			// (Invoke) Token: 0x060001BB RID: 443
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void JoinFailedHandler(IntPtr handle, IntPtr sessionInfo);

			// Token: 0x0200005F RID: 95
			// (Invoke) Token: 0x060001BF RID: 447
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void PeerConnectedHandler(IntPtr handle, uint peerId);

			// Token: 0x02000060 RID: 96
			// (Invoke) Token: 0x060001C3 RID: 451
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void PeerGoneHandler(IntPtr handle, uint peerId, IntPtr reason, ulong reasonLength, uint reasonId);

			// Token: 0x02000061 RID: 97
			// (Invoke) Token: 0x060001C7 RID: 455
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void SessionDescriptorUpdatedHandler(IntPtr handle, IntPtr sessionDescriptor);

			// Token: 0x02000062 RID: 98
			// (Invoke) Token: 0x060001CB RID: 459
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void HostElectedHandler(IntPtr handle, uint hostPeerId, uint previousHostPeerId, IntPtr hostDescriptor, IntPtr sessionDescriptor, uint hostMigrationId);

			// Token: 0x02000063 RID: 99
			// (Invoke) Token: 0x060001CF RID: 463
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void HostMigrationStartedHandler(IntPtr handle, IntPtr reason, ulong reasonLength, uint reasonId, IntPtr connectedPeerGUIDs, IntPtr connectedPeerGUIDSizes, ulong connectedPeerGUIDsCount, IntPtr discoveredPeerGUIDs, IntPtr discoveredPeerGUIDSizes, ulong discoveredPeerGUIDsCount);

			// Token: 0x02000064 RID: 100
			// (Invoke) Token: 0x060001D3 RID: 467
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void HostMigrationEndedHandler(IntPtr handle, uint hostPeerId, uint previousHostPeerId, IntPtr hostDescriptor);

			// Token: 0x02000065 RID: 101
			// (Invoke) Token: 0x060001D7 RID: 471
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void SessionLeftHandler(IntPtr handle);

			// Token: 0x02000066 RID: 102
			// (Invoke) Token: 0x060001DB RID: 475
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void SessionLeaveHandler(IntPtr handle, IntPtr reason, ulong reasonLength, uint reasonId);

			// Token: 0x02000067 RID: 103
			// (Invoke) Token: 0x060001DF RID: 479
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void SessionDissolvedHandler(IntPtr handle);
		}
	}
}
