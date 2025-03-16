using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using StormDotNet.Echo;

namespace StormDotNet
{
	// Token: 0x02000050 RID: 80
	public static class SessionController
	{
		// Token: 0x06000150 RID: 336 RVA: 0x000051A0 File Offset: 0x000033A0
		internal static void Initialize()
		{
			List<GCHandle> list = SessionController.gcHandles;
			lock (list)
			{
				SessionController.Native.SessionDiscoveredHandler sessionDiscoveredHandler = new SessionController.Native.SessionDiscoveredHandler(SessionController.SessionDiscoveredHandler);
				GCHandle gchandle = GCHandle.Alloc(sessionDiscoveredHandler, GCHandleType.Normal);
				SessionController.gcHandles.Add(gchandle);
				SessionController.Native.DeleteSessionHandler deleteSessionHandler = new SessionController.Native.DeleteSessionHandler(SessionController.DeleteSessionHandler);
				gchandle = GCHandle.Alloc(deleteSessionHandler, GCHandleType.Normal);
				SessionController.gcHandles.Add(gchandle);
				SessionController.Native.Initialize(sessionDiscoveredHandler, deleteSessionHandler);
			}
			SessionController.discoveredSessionsCount = 10;
			SessionController.discoveredSessionsList = Marshal.AllocHGlobal(SessionController.discoveredSessionsCount * IntPtr.Size);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000523C File Offset: 0x0000343C
		internal static void Uninitialize()
		{
			List<GCHandle> list = SessionController.gcHandles;
			lock (list)
			{
				foreach (GCHandle gchandle in SessionController.gcHandles)
				{
					gchandle.Free();
				}
				SessionController.gcHandles.Clear();
			}
			if (SessionController.discoveredSessionsList != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(SessionController.discoveredSessionsList);
			}
			SessionController.discoveredSessionsList = IntPtr.Zero;
			SessionController.discoveredSessionsCount = 0;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x000052EC File Offset: 0x000034EC
		public static void RegisterObserver(SessionController.IObserver observer)
		{
			if (observer != null)
			{
				SessionController.sessionsObservers.Add(observer);
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000052FC File Offset: 0x000034FC
		public static void UnregisterObserver(SessionController.IObserver observer)
		{
			if (observer != null)
			{
				SessionController.sessionsObservers.Remove(observer);
			}
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00005310 File Offset: 0x00003510
		public static SessionHandler CreateSessionHandler()
		{
			SessionHandler sessionHandler = new SessionHandler();
			SessionController.participatingSessions.Add(sessionHandler);
			return sessionHandler;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00005330 File Offset: 0x00003530
		[MonoPInvokeCallback(typeof(SessionController.Native.SessionDiscoveredHandler))]
		private static void SessionDiscoveredHandler(IntPtr sessionDescriptorHandle, IntPtr hostDescriptorHandle)
		{
			if (sessionDescriptorHandle == IntPtr.Zero)
			{
				throw new ArgumentNullException("sessionDescriptorHandle");
			}
			if (hostDescriptorHandle == IntPtr.Zero)
			{
				throw new ArgumentNullException("hostDescriptorHandle");
			}
			SessionDescriptor sessionDescriptor = GCHandle.FromIntPtr(sessionDescriptorHandle).Target as SessionDescriptor;
			PeerDescriptor peerDescriptor = GCHandle.FromIntPtr(hostDescriptorHandle).Target as PeerDescriptor;
			if (sessionDescriptor == null)
			{
				throw new ArgumentException("The sessionDescriptor is not of base type SessionDescriptor.", "sessionDescriptor");
			}
			if (peerDescriptor == null)
			{
				throw new ArgumentException("The hostDescriptor is not of base type PeerDescriptor.", "hostDescriptor");
			}
			SessionController.NotifySessionDiscovered(sessionDescriptor, peerDescriptor);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000053C0 File Offset: 0x000035C0
		[MonoPInvokeCallback(typeof(SessionController.Native.DeleteSessionHandler))]
		private static void DeleteSessionHandler(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("handle");
			}
			SessionHandler sessionHandler = GCHandle.FromIntPtr(handle).Target as SessionHandler;
			if (sessionHandler == null)
			{
				throw new ArgumentException("The sessionHandler is not of base type SessionHandler.", "sessionHandler");
			}
			SessionController.participatingSessions.Remove(sessionHandler);
			sessionHandler.Dispose();
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00005420 File Offset: 0x00003620
		internal static void NotifySessionCreateSuccess(SessionHandler sessionHandler)
		{
			foreach (SessionController.IObserver observer in SessionController.sessionsObservers)
			{
				observer.OnSessionCreateSuccess(sessionHandler);
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00005470 File Offset: 0x00003670
		internal static void NotifySessionCreateFail(SessionHandler sessionHandler)
		{
			foreach (SessionController.IObserver observer in SessionController.sessionsObservers)
			{
				observer.OnSessionCreateFail(sessionHandler);
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000054C0 File Offset: 0x000036C0
		internal static void NotifySessionJoinSuccess(SessionHandler sessionHandler)
		{
			foreach (SessionController.IObserver observer in SessionController.sessionsObservers)
			{
				observer.OnSessionJoinSuccess(sessionHandler);
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00005510 File Offset: 0x00003710
		internal static void NotifySessionJoinFail(SessionHandler sessionHandler)
		{
			foreach (SessionController.IObserver observer in SessionController.sessionsObservers)
			{
				observer.OnSessionJoinFail(sessionHandler);
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00005560 File Offset: 0x00003760
		internal static void NotifySessionLeft(SessionHandler sessionHandler)
		{
			foreach (SessionController.IObserver observer in SessionController.sessionsObservers)
			{
				observer.OnSessionLeft(sessionHandler);
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x000055B0 File Offset: 0x000037B0
		internal static void NotifySessionLeave(SessionHandler sessionHandler, string reason, StringId leaveReason)
		{
			foreach (SessionController.IObserver observer in SessionController.sessionsObservers)
			{
				observer.OnSessionLeave(sessionHandler, reason, leaveReason);
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00005604 File Offset: 0x00003804
		internal static void NotifySessionDissolved(SessionHandler sessionHandler)
		{
			foreach (SessionController.IObserver observer in SessionController.sessionsObservers)
			{
				observer.OnSessionDissolved(sessionHandler);
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00005654 File Offset: 0x00003854
		internal static void NotifySessionPeerConnected(SessionHandler sessionHandler, uint peerId)
		{
			foreach (SessionController.IObserver observer in SessionController.sessionsObservers)
			{
				observer.OnSessionPeerConnected(sessionHandler, peerId);
			}
		}

		// Token: 0x0600015F RID: 351 RVA: 0x000056A8 File Offset: 0x000038A8
		internal static void NotifySessionPeerGone(SessionHandler sessionHandler, uint peerId, string reason, StringId leaveReason)
		{
			foreach (SessionController.IObserver observer in SessionController.sessionsObservers)
			{
				observer.OnSessionPeerGone(sessionHandler, peerId, reason, leaveReason);
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x000056FC File Offset: 0x000038FC
		internal static void NotifySessionDescriptorUpdated(SessionHandler sessionHandler, SessionDescriptor sessionDescriptor)
		{
			SessionController.sessionsObservers.ForEach(delegate(SessionController.IObserver observer)
			{
				observer.OnSessionDescriptorUpdated(sessionHandler, sessionDescriptor);
			});
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00005734 File Offset: 0x00003934
		internal static void NotifySessionHostElected(SessionHandler sessionHandler, uint hostPeerId, uint previousHostPeerId, PeerDescriptor hostDescriptor, SessionDescriptor sessionDescriptor, uint hostMigrationId)
		{
			SessionController.sessionsObservers.ForEach(delegate(SessionController.IObserver observer)
			{
				observer.OnSessionHostElected(sessionHandler, hostPeerId, previousHostPeerId, hostDescriptor, sessionDescriptor, hostMigrationId);
			});
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000578C File Offset: 0x0000398C
		internal static void NotifySessionHostMigrationStarted(SessionHandler sessionHandler, string reason, StringId reasonId, GUID[] connectedPeerGUIDs, GUID[] discoveredPeerGUIDs)
		{
			SessionController.sessionsObservers.ForEach(delegate(SessionController.IObserver observer)
			{
				observer.OnSessionHostMigrationStarted(sessionHandler, reason, reasonId, connectedPeerGUIDs, discoveredPeerGUIDs);
			});
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000057DC File Offset: 0x000039DC
		internal static void NotifySessionHostMigrationEnded(SessionHandler sessionHandler, uint hostPeerId, uint previousHostPeerId, PeerDescriptor hostDescriptor)
		{
			SessionController.sessionsObservers.ForEach(delegate(SessionController.IObserver observer)
			{
				observer.OnSessionHostMigrationEnded(sessionHandler, hostPeerId, previousHostPeerId, hostDescriptor);
			});
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00005824 File Offset: 0x00003A24
		internal static void NotifySessionDiscovered(SessionDescriptor sessionDescriptor, PeerDescriptor hostDescriptor)
		{
			foreach (SessionController.IObserver observer in SessionController.sessionsObservers)
			{
				observer.OnSessionDiscovered(sessionDescriptor, hostDescriptor);
			}
		}

		// Token: 0x0400006E RID: 110
		public static readonly uint INVALID_SESSION_REF_ID = uint.MaxValue;

		// Token: 0x0400006F RID: 111
		private static readonly List<GCHandle> gcHandles = new List<GCHandle>();

		// Token: 0x04000070 RID: 112
		private static List<SessionController.IObserver> sessionsObservers = new List<SessionController.IObserver>();

		// Token: 0x04000071 RID: 113
		private static IntPtr discoveredSessionsList = IntPtr.Zero;

		// Token: 0x04000072 RID: 114
		private static int discoveredSessionsCount = 0;

		// Token: 0x04000073 RID: 115
		private static List<SessionHandler> participatingSessions = new List<SessionHandler>();

		// Token: 0x02000051 RID: 81
		public interface IObserver
		{
			// Token: 0x06000166 RID: 358
			void OnSessionCreateSuccess(SessionHandler sessionHandler);

			// Token: 0x06000167 RID: 359
			void OnSessionCreateFail(SessionHandler sessionHandler);

			// Token: 0x06000168 RID: 360
			void OnSessionJoinSuccess(SessionHandler sessionHandler);

			// Token: 0x06000169 RID: 361
			void OnSessionJoinFail(SessionHandler sessionHandler);

			// Token: 0x0600016A RID: 362
			void OnSessionLeft(SessionHandler sessionHandler);

			// Token: 0x0600016B RID: 363
			void OnSessionLeave(SessionHandler sessionHandler, string reason, StringId leaveReason);

			// Token: 0x0600016C RID: 364
			void OnSessionDissolved(SessionHandler sessionHandler);

			// Token: 0x0600016D RID: 365
			void OnSessionPeerConnected(SessionHandler sessionHandler, uint peerId);

			// Token: 0x0600016E RID: 366
			void OnSessionPeerGone(SessionHandler sessionHandler, uint peerId, string reason, StringId leaveReason);

			// Token: 0x0600016F RID: 367
			void OnSessionDescriptorUpdated(SessionHandler sessionHandler, SessionDescriptor sessionDescriptor);

			// Token: 0x06000170 RID: 368
			void OnSessionHostElected(SessionHandler sessionHandler, uint hostPeerId, uint previousHostPeerId, PeerDescriptor hostDescriptor, SessionDescriptor sessionDescriptor, uint hostMigrationId);

			// Token: 0x06000171 RID: 369
			void OnSessionHostMigrationStarted(SessionHandler sessionHandler, string reason, StringId reasonId, GUID[] connectedPeerGUIDs, GUID[] discoveredPeerGUIDs);

			// Token: 0x06000172 RID: 370
			void OnSessionHostMigrationEnded(SessionHandler sessionHandler, uint hostPeerId, uint previousHostPeerId, PeerDescriptor hostDescriptor);

			// Token: 0x06000173 RID: 371
			void OnSessionDiscovered(SessionDescriptor sessionDescriptor, PeerDescriptor hostDescriptor);
		}

		// Token: 0x02000052 RID: 82
		private static class Native
		{
			// Token: 0x06000174 RID: 372
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "SessionController_Initialize")]
			public static extern void Initialize(SessionController.Native.SessionDiscoveredHandler onSessionDiscovered, SessionController.Native.DeleteSessionHandler onDeleteSessionHandler);

			// Token: 0x02000053 RID: 83
			// (Invoke) Token: 0x06000176 RID: 374
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void SessionDiscoveredHandler(IntPtr sessionDescriptorHandle, IntPtr hostDescriptorHandle);

			// Token: 0x02000054 RID: 84
			// (Invoke) Token: 0x0600017A RID: 378
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void DeleteSessionHandler(IntPtr sessionHandler);
		}
	}
}
