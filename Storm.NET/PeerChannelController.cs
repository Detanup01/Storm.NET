using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using StormDotNet.Echo.PeerChannel;

namespace StormDotNet
{
	// Token: 0x02000046 RID: 70
	public static class PeerChannelController
	{
		// Token: 0x06000110 RID: 272 RVA: 0x00004090 File Offset: 0x00002290
		internal static void Initialize()
		{
			List<GCHandle> list = PeerChannelController.gcHandles;
			lock (list)
			{
				PeerChannelController.Native.OnPeerBroadcastCallback onPeerBroadcastCallback = new PeerChannelController.Native.OnPeerBroadcastCallback(PeerChannelController.OnPeerBroadcast);
				GCHandle gchandle = GCHandle.Alloc(onPeerBroadcastCallback, GCHandleType.Normal);
				PeerChannelController.gcHandles.Add(gchandle);
				PeerChannelController.Native.OnPeerUnicastCallback onPeerUnicastCallback = new PeerChannelController.Native.OnPeerUnicastCallback(PeerChannelController.OnPeerUnicast);
				gchandle = GCHandle.Alloc(onPeerUnicastCallback, GCHandleType.Normal);
				PeerChannelController.gcHandles.Add(gchandle);
				PeerChannelController.Native.Initialize(onPeerBroadcastCallback, onPeerUnicastCallback);
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00004110 File Offset: 0x00002310
		internal static void Uninitialize()
		{
			List<GCHandle> list = PeerChannelController.gcHandles;
			lock (list)
			{
				foreach (GCHandle gchandle in PeerChannelController.gcHandles)
				{
					gchandle.Free();
				}
				PeerChannelController.gcHandles.Clear();
			}
			Dictionary<uint, PeerChannel> dictionary = PeerChannelController.peerChannels;
			lock (dictionary)
			{
				PeerChannelController.peerChannels.Clear();
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000041CC File Offset: 0x000023CC
		public static void AddPeerChannel(PeerChannel peerChannel)
		{
			PeerChannelController.AddPeerChannel(new StringId(uint.MaxValue), peerChannel);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000041DC File Offset: 0x000023DC
		public static void AddPeerChannel(StringId sessionType, PeerChannel peerChannel)
		{
			if (sessionType == null)
			{
				throw new ArgumentNullException("sessionType");
			}
			Dictionary<uint, PeerChannel> dictionary = PeerChannelController.peerChannels;
			lock (dictionary)
			{
				if (PeerChannelController.peerChannels.ContainsKey(peerChannel.ChannelId.UniqueID))
				{
					throw new InvalidOperationException(string.Format("A peer channel of the type {0} is already added.", peerChannel.GetType().Name));
				}
				peerChannel.SessionType = sessionType;
				if (!PeerChannelController.Native.AddPeerChannelType(sessionType.UniqueID, peerChannel.ChannelId.UniqueID))
				{
					EResult lastResult = EResult.GetLastResult();
					throw new Exception(string.Format("Failed to add the peer channel. Error: {0}", lastResult.GetErrorCodeName()));
				}
				PeerChannelController.peerChannels.Add(peerChannel.ChannelId.UniqueID, peerChannel);
				peerChannel.IsRegistered = true;
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000042B4 File Offset: 0x000024B4
		internal static void AddBroadcastEntry(StringId sessionType, PeerChannel peerChannel, uint netProtocolEntryId, Type netProtocolEntryType, StringId transportMode, bool sessionTimeSupport)
		{
			if (sessionType == null)
			{
				throw new ArgumentNullException("sessionType");
			}
			if (peerChannel == null)
			{
				throw new ArgumentNullException("peerChannel");
			}
			if (netProtocolEntryType == null)
			{
				throw new ArgumentNullException("netProtocolEntryType");
			}
			if (transportMode == null)
			{
				throw new ArgumentNullException("transportMode");
			}
			if (!netProtocolEntryType.IsSubclassOf(typeof(INetProperty)) && !netProtocolEntryType.IsSubclassOf(typeof(PeerMessage)))
			{
				throw new ArgumentException("The type of the message is not a subclass of INetProperty or PeerMessage.");
			}
			if (!transportMode.IsValid)
			{
				throw new ArgumentException("The transport mode is invalid.");
			}
			Dictionary<uint, PeerChannel> dictionary = PeerChannelController.peerChannels;
			lock (dictionary)
			{
				if (!PeerChannelController.peerChannels.ContainsKey(peerChannel.ChannelId.UniqueID))
				{
					throw new InvalidOperationException(string.Format("The peer channel is not registered.", peerChannel.GetType().Name));
				}
				if (netProtocolEntryType.IsSubclassOf(typeof(INetProperty)))
				{
					if (!PeerChannelController.Native.AddPeerChannelBroadcast(sessionType.UniqueID, peerChannel.ChannelId.UniqueID, netProtocolEntryId, transportMode.UniqueID, sessionTimeSupport))
					{
						EResult lastResult = EResult.GetLastResult();
						throw new Exception("Failed to add the broadcast entry to the peer channel. Error: " + lastResult.GetErrorCodeName());
					}
				}
				else if (!PeerChannelController.Native.AddPeerChannelBroadcastEntry(sessionType.UniqueID, peerChannel.ChannelId.UniqueID, netProtocolEntryId, transportMode.UniqueID, sessionTimeSupport))
				{
					EResult lastResult2 = EResult.GetLastResult();
					throw new Exception("Failed to add the broadcast entry to the peer channel. Error: " + lastResult2.GetErrorCodeName());
				}
				DynamicTypeRegistry.TryAdd(netProtocolEntryId, netProtocolEntryType);
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00004444 File Offset: 0x00002644
		internal static void AddUnicastEntry(StringId sessionType, PeerChannel peerChannel, uint netProtocolEntryId, Type netProtocolEntryType, StringId transportMode, bool sessionTimeSupport)
		{
			if (sessionType == null)
			{
				throw new ArgumentNullException("sessionType");
			}
			if (peerChannel == null)
			{
				throw new ArgumentNullException("peerChannel");
			}
			if (netProtocolEntryType == null)
			{
				throw new ArgumentNullException("netProtocolEntryType");
			}
			if (transportMode == null)
			{
				throw new ArgumentNullException("transportMode");
			}
			if (!netProtocolEntryType.IsSubclassOf(typeof(INetProperty)) && !netProtocolEntryType.IsSubclassOf(typeof(PeerMessage)))
			{
				throw new ArgumentException("The type of the message is not a subclass of INetProperty or PeerMessage.");
			}
			if (!transportMode.IsValid)
			{
				throw new ArgumentException("The transport mode is invalid.");
			}
			Dictionary<uint, PeerChannel> dictionary = PeerChannelController.peerChannels;
			lock (dictionary)
			{
				if (!PeerChannelController.peerChannels.ContainsKey(peerChannel.ChannelId.UniqueID))
				{
					throw new InvalidOperationException(string.Format("The peer channel is not registered.", peerChannel.GetType().Name));
				}
				if (netProtocolEntryType.IsSubclassOf(typeof(INetProperty)))
				{
					if (!PeerChannelController.Native.AddPeerChannelUnicast(sessionType.UniqueID, peerChannel.ChannelId.UniqueID, netProtocolEntryId, transportMode.UniqueID, sessionTimeSupport))
					{
						EResult lastResult = EResult.GetLastResult();
						throw new Exception(string.Format("Failed to add the broadcast entry to the peer channel. Error: {0}", lastResult.GetErrorCodeName()));
					}
				}
				else if (!PeerChannelController.Native.AddPeerChannelUnicastEntry(sessionType.UniqueID, peerChannel.ChannelId.UniqueID, netProtocolEntryId, transportMode.UniqueID, sessionTimeSupport))
				{
					EResult lastResult2 = EResult.GetLastResult();
					throw new Exception(string.Format("Failed to add the broadcast entry to the peer channel. Error: {0}", lastResult2.GetErrorCodeName()));
				}
				DynamicTypeRegistry.TryAdd(netProtocolEntryId, netProtocolEntryType);
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000045D4 File Offset: 0x000027D4
		internal static EResult SendPeerUnicast(ResultRelayer resultRelayer, uint sessionRefId, uint toPeerId, StringId peerChannelId, INetProperty message)
		{
			EResult lastResult;
			using (StormEngine.SetProfilePoint(null))
			{
				if (resultRelayer == null || resultRelayer.Handle == IntPtr.Zero)
				{
					throw new ArgumentNullException("resultRelayer");
				}
				if (peerChannelId == null)
				{
					throw new ArgumentNullException("peerChannelId");
				}
				if (message == null || message.Handle == IntPtr.Zero)
				{
					throw new ArgumentNullException("message");
				}
				PeerChannelController.Native.SendPeerUnicast(resultRelayer.Handle, sessionRefId, toPeerId, peerChannelId.UniqueID, message.Handle);
				lastResult = EResult.GetLastResult();
			}
			return lastResult;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00004684 File Offset: 0x00002884
		internal static EResult SendPeerMessageUnicast(ResultRelayer resultRelayer, uint sessionRefId, uint toPeerId, StringId peerChannelId, PeerMessage message)
		{
			EResult lastResult;
			using (StormEngine.SetProfilePoint(null))
			{
				if (resultRelayer == null || resultRelayer.Handle == IntPtr.Zero)
				{
					throw new ArgumentNullException("resultRelayer");
				}
				if (peerChannelId == null)
				{
					throw new ArgumentNullException("peerChannelId");
				}
				if (message == null || message.Handle == IntPtr.Zero)
				{
					throw new ArgumentNullException("message");
				}
				PeerChannelController.Native.SendPeerMessageUnicast(resultRelayer.Handle, sessionRefId, toPeerId, peerChannelId.UniqueID, message.Handle);
				lastResult = EResult.GetLastResult();
			}
			return lastResult;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0000472C File Offset: 0x0000292C
		internal static EResult SendPeerBroadcast(ResultRelayer resultRelayer, uint sessionRefId, StringId peerChannelId, INetProperty message)
		{
			EResult lastResult;
			using (StormEngine.SetProfilePoint(null))
			{
				if (resultRelayer == null || resultRelayer.Handle == IntPtr.Zero)
				{
					throw new ArgumentNullException("resultRelayer");
				}
				if (peerChannelId == null)
				{
					throw new ArgumentNullException("peerChannelId");
				}
				if (message == null)
				{
					throw new ArgumentNullException("message");
				}
				PeerChannelController.Native.SendPeerBroadcast(resultRelayer.Handle, sessionRefId, peerChannelId.UniqueID, message.Handle);
				lastResult = EResult.GetLastResult();
			}
			return lastResult;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000047C4 File Offset: 0x000029C4
		internal static EResult SendPeerMessageBroadcast(ResultRelayer resultRelayer, uint sessionRefId, StringId peerChannelId, PeerMessage message)
		{
			EResult lastResult;
			using (StormEngine.SetProfilePoint(null))
			{
				if (resultRelayer == null || resultRelayer.Handle == IntPtr.Zero)
				{
					throw new ArgumentNullException("resultRelayer");
				}
				if (peerChannelId == null)
				{
					throw new ArgumentNullException("peerChannelId");
				}
				if (message == null)
				{
					throw new ArgumentNullException("message");
				}
				PeerChannelController.Native.SendPeerMessageBroadcast(resultRelayer.Handle, sessionRefId, peerChannelId.UniqueID, message.Handle);
				lastResult = EResult.GetLastResult();
			}
			return lastResult;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00004858 File Offset: 0x00002A58
		[MonoPInvokeCallback(typeof(PeerChannelController.Native.OnPeerBroadcastCallback))]
		private static int OnPeerBroadcast(uint sessionRefId, uint senderPeerId, uint peerChannelId, IntPtr messageHandle, uint sessionTime)
		{
			int num;
			using (StormEngine.SetProfilePoint(null))
			{
				if (messageHandle == IntPtr.Zero)
				{
					throw new ArgumentNullException("messageHandle");
				}
				PeerChannel peerChannel = null;
				if (!PeerChannelController.peerChannels.TryGetValue(peerChannelId, out peerChannel))
				{
					throw new KeyNotFoundException(string.Format("{0} is not a registered peer channel.", new StringId(peerChannelId)));
				}
				GCHandle gchandle = GCHandle.FromIntPtr(messageHandle);
				INetProperty netProperty = gchandle.Target as INetProperty;
				if (netProperty != null)
				{
					num = (int)PeerChannel.OnPeerBroadcast(sessionRefId, senderPeerId, peerChannel, netProperty, sessionTime).GetErrorCode();
				}
				else
				{
					PeerMessage peerMessage = gchandle.Target as PeerMessage;
					if (peerMessage == null)
					{
						throw new ArgumentException("The message is not of base type INetProperty or PeerMessage.", "messageGCHandle");
					}
					num = (int)PeerChannel.OnPeerMessageBroadcast(sessionRefId, senderPeerId, peerChannel, peerMessage, sessionTime).GetErrorCode();
				}
			}
			return num;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00004930 File Offset: 0x00002B30
		[MonoPInvokeCallback(typeof(PeerChannelController.Native.OnPeerUnicastCallback))]
		private static int OnPeerUnicast(uint sessionRefId, uint senderPeerId, uint peerChannelId, IntPtr messageHandle, uint sessionTime)
		{
			int num;
			using (StormEngine.SetProfilePoint(null))
			{
				if (messageHandle == IntPtr.Zero)
				{
					throw new ArgumentNullException("messageHandle");
				}
				PeerChannel peerChannel = null;
				if (!PeerChannelController.peerChannels.TryGetValue(peerChannelId, out peerChannel))
				{
					throw new KeyNotFoundException(string.Format("{0} is not a registered peer channel.", new StringId(peerChannelId)));
				}
				GCHandle gchandle = GCHandle.FromIntPtr(messageHandle);
				INetProperty netProperty = gchandle.Target as INetProperty;
				if (netProperty != null)
				{
					num = (int)PeerChannel.OnPeerUnicast(sessionRefId, senderPeerId, peerChannel, netProperty, sessionTime).GetErrorCode();
				}
				else
				{
					PeerMessage peerMessage = gchandle.Target as PeerMessage;
					if (peerMessage == null)
					{
						throw new ArgumentException("The message is not of base type INetProperty or PeerMessage.", "messageGCHandle");
					}
					num = (int)PeerChannel.OnPeerMessageUnicast(sessionRefId, senderPeerId, peerChannel, peerMessage, sessionTime).GetErrorCode();
				}
			}
			return num;
		}

		// Token: 0x0400005D RID: 93
		private static readonly List<GCHandle> gcHandles = new List<GCHandle>();

		// Token: 0x0400005E RID: 94
		private static readonly Dictionary<uint, PeerChannel> peerChannels = new Dictionary<uint, PeerChannel>();

		// Token: 0x02000047 RID: 71
		private static class Native
		{
			// Token: 0x0600011D RID: 285
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "PeerChannelController_Initialize")]
			public static extern void Initialize(PeerChannelController.Native.OnPeerBroadcastCallback onPeerBroadcastCallback, PeerChannelController.Native.OnPeerUnicastCallback onPeerUnicastCallback);

			// Token: 0x0600011E RID: 286
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "PeerChannelController_AddPeerChannelType")]
			[return: MarshalAs(UnmanagedType.U1)]
			public static extern bool AddPeerChannelType(uint sessionType, uint netPeerChannelTypeName);

			// Token: 0x0600011F RID: 287
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "PeerChannelController_AddPeerChannelBroadcast")]
			[return: MarshalAs(UnmanagedType.U1)]
			public static extern bool AddPeerChannelBroadcast(uint sessionType, uint netPeerChannelTypeName, uint netProtocolEntryId, uint transportMode, [MarshalAs(UnmanagedType.U1)] bool sessionTimeSupport);

			// Token: 0x06000120 RID: 288
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "PeerChannelController_AddPeerChannelBroadcastEntry")]
			[return: MarshalAs(UnmanagedType.U1)]
			public static extern bool AddPeerChannelBroadcastEntry(uint sessionType, uint netPeerChannelTypeName, uint netProtocolEntryId, uint transportMode, [MarshalAs(UnmanagedType.U1)] bool sessionTimeSupport);

			// Token: 0x06000121 RID: 289
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "PeerChannelController_AddPeerChannelUnicast")]
			[return: MarshalAs(UnmanagedType.U1)]
			public static extern bool AddPeerChannelUnicast(uint sessionType, uint netPeerChannelTypeName, uint netProtocolEntryId, uint transportMode, [MarshalAs(UnmanagedType.U1)] bool sessionTimeSupport);

			// Token: 0x06000122 RID: 290
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "PeerChannelController_AddPeerChannelUnicastEntry")]
			[return: MarshalAs(UnmanagedType.U1)]
			public static extern bool AddPeerChannelUnicastEntry(uint sessionType, uint netPeerChannelTypeName, uint netProtocolEntryId, uint transportMode, [MarshalAs(UnmanagedType.U1)] bool sessionTimeSupport);

			// Token: 0x06000123 RID: 291
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "PeerChannelController_SendPeerUnicast")]
			[return: MarshalAs(UnmanagedType.U1)]
			public static extern bool SendPeerUnicast(IntPtr resultRelayer, uint sessionRefId, uint toPeerId, uint peerChannelId, IntPtr message);

			// Token: 0x06000124 RID: 292
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "PeerChannelController_SendPeerMessageUnicast")]
			[return: MarshalAs(UnmanagedType.U1)]
			public static extern bool SendPeerMessageUnicast(IntPtr resultRelayer, uint sessionRefId, uint toPeerId, uint peerChannelId, IntPtr message);

			// Token: 0x06000125 RID: 293
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "PeerChannelController_SendPeerBroadcast")]
			[return: MarshalAs(UnmanagedType.U1)]
			public static extern bool SendPeerBroadcast(IntPtr resultRelayer, uint sessionRefId, uint peerChannelId, IntPtr message);

			// Token: 0x06000126 RID: 294
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "PeerChannelController_SendPeerMessageBroadcast")]
			[return: MarshalAs(UnmanagedType.U1)]
			public static extern bool SendPeerMessageBroadcast(IntPtr resultRelayer, uint sessionRefId, uint peerChannelId, IntPtr message);

			// Token: 0x02000048 RID: 72
			// (Invoke) Token: 0x06000128 RID: 296
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate int OnPeerBroadcastCallback(uint sessionRefId, uint senderPeerId, uint peerChannelId, IntPtr netPropertyHandle, uint sessionTime);

			// Token: 0x02000049 RID: 73
			// (Invoke) Token: 0x0600012C RID: 300
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate int OnPeerUnicastCallback(uint sessionRefId, uint senderPeerId, uint peerChannelId, IntPtr netPropertyHandle, uint sessionTime);
		}
	}
}
