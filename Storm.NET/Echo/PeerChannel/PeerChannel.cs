using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace StormDotNet.Echo.PeerChannel
{
	// Token: 0x020000C2 RID: 194
	public abstract class PeerChannel
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060003CD RID: 973 RVA: 0x0000BAAC File Offset: 0x00009CAC
		public StringId ChannelId
		{
			get
			{
				if (this.channelId == null)
				{
					object obj = this.channelLock;
					lock (obj)
					{
						if (this.channelId == null)
						{
							this.channelId = new StringId(base.GetType().FullName);
						}
					}
				}
				return this.channelId;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060003CE RID: 974 RVA: 0x0000BB28 File Offset: 0x00009D28
		// (set) Token: 0x060003CF RID: 975 RVA: 0x0000BB30 File Offset: 0x00009D30
		internal bool IsRegistered
		{
			get
			{
				return this.isRegistered;
			}
			set
			{
				if (this.isRegistered)
				{
					throw new InvalidOperationException("The peer channel is already registered.");
				}
				if (value)
				{
					Dictionary<uint, PeerChannel.BroadcastEntry> dictionary = this.broadcastEntries;
					lock (dictionary)
					{
						foreach (PeerChannel.BroadcastEntry broadcastEntry in this.broadcastEntries.Values)
						{
							PeerChannelController.AddBroadcastEntry(this.sessionType, this, broadcastEntry.netProtocolEntryId, broadcastEntry.netProtocolEntryType, broadcastEntry.transportMode, broadcastEntry.sessionTimeSupport);
						}
					}
					Dictionary<uint, PeerChannel.UnicastEntry> dictionary2 = this.unicastEntries;
					lock (dictionary2)
					{
						foreach (PeerChannel.UnicastEntry unicastEntry in this.unicastEntries.Values)
						{
							PeerChannelController.AddUnicastEntry(this.sessionType, this, unicastEntry.netProtocolEntryId, unicastEntry.netProtocolEntryType, unicastEntry.transportMode, unicastEntry.sessionTimeSupport);
						}
					}
				}
				this.isRegistered = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (set) Token: 0x060003D0 RID: 976 RVA: 0x0000BC8C File Offset: 0x00009E8C
		internal StringId SessionType
		{
			set
			{
				this.sessionType = value;
			}
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000BC98 File Offset: 0x00009E98
		protected PeerChannel()
		{
			this.channelId = null;
			this.channelLock = new object();
			this.sessionType = new StringId(uint.MaxValue);
			this.isRegistered = false;
			this.broadcastEntries = new Dictionary<uint, PeerChannel.BroadcastEntry>();
			this.unicastEntries = new Dictionary<uint, PeerChannel.UnicastEntry>();
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000BCE8 File Offset: 0x00009EE8
		public void AddBroadcastEntry(Type netProtocolEntryType, StringId transportMode, bool sessionTimeSupport)
		{
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
				throw new ArgumentException("The type of the peer broadcast entry must be a subclass of INetProperty or PeerMessage.");
			}
			if (!transportMode.IsValid)
			{
				throw new ArgumentException("The transport mode is invalid.");
			}
			StringId stringId = new StringId(netProtocolEntryType.FullName);
			Dictionary<uint, PeerChannel.BroadcastEntry> dictionary = this.broadcastEntries;
			lock (dictionary)
			{
				if (this.broadcastEntries.ContainsKey(stringId.UniqueID))
				{
					throw new InvalidOperationException("");
				}
				this.broadcastEntries.Add(stringId.UniqueID, new PeerChannel.BroadcastEntry
				{
					netProtocolEntryId = stringId,
					netProtocolEntryType = netProtocolEntryType,
					transportMode = transportMode,
					sessionTimeSupport = sessionTimeSupport
				});
				if (this.IsRegistered)
				{
					PeerChannelController.AddBroadcastEntry(this.sessionType, this, stringId, netProtocolEntryType, transportMode, sessionTimeSupport);
				}
			}
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000BE10 File Offset: 0x0000A010
		public void AddUnicastEntry(Type netProtocolEntryType, StringId transportMode, bool sessionTimeSupport)
		{
			if (netProtocolEntryType == null)
			{
				throw new ArgumentNullException("netProtocolEntryType");
			}
			if (transportMode == null)
			{
				throw new ArgumentNullException("transportMode");
			}
			if (!transportMode.IsValid)
			{
				throw new ArgumentException("The transport mode is invalid.");
			}
			if (!netProtocolEntryType.IsSubclassOf(typeof(INetProperty)) && !netProtocolEntryType.IsSubclassOf(typeof(PeerMessage)))
			{
				throw new ArgumentException("The type of the peer unicast entry must be a subclass of INetProperty or PeerMessage.");
			}
			StringId stringId = new StringId(netProtocolEntryType.FullName);
			Dictionary<uint, PeerChannel.UnicastEntry> dictionary = this.unicastEntries;
			lock (dictionary)
			{
				if (this.unicastEntries.ContainsKey(stringId.UniqueID))
				{
					throw new InvalidOperationException("");
				}
				this.unicastEntries.Add(stringId.UniqueID, new PeerChannel.UnicastEntry
				{
					netProtocolEntryId = stringId,
					netProtocolEntryType = netProtocolEntryType,
					transportMode = transportMode,
					sessionTimeSupport = sessionTimeSupport
				});
				if (this.IsRegistered)
				{
					PeerChannelController.AddUnicastEntry(this.sessionType, this, stringId, netProtocolEntryType, transportMode, sessionTimeSupport);
				}
			}
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000BF38 File Offset: 0x0000A138
		public EResult SendPeerUnicast(ResultRelayer resultRelayer, uint sessionRefId, uint toPeerId, INetProperty message)
		{
			if (resultRelayer == null || resultRelayer.Handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("resultRelayer");
			}
			if (message == null || message.Handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("message");
			}
			if (!this.isRegistered)
			{
				throw new InvalidOperationException("The peer channel is not registered in the controller.");
			}
			return PeerChannelController.SendPeerUnicast(resultRelayer, sessionRefId, toPeerId, this.ChannelId, message);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000BFB0 File Offset: 0x0000A1B0
		public EResult SendPeerMessageUnicast(ResultRelayer resultRelayer, uint sessionRefId, uint toPeerId, PeerMessage message)
		{
			if (resultRelayer == null || resultRelayer.Handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("resultRelayer");
			}
			if (message == null || message.Handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("message");
			}
			if (!this.isRegistered)
			{
				throw new InvalidOperationException("The peer channel is not registered in the controller.");
			}
			return PeerChannelController.SendPeerMessageUnicast(resultRelayer, sessionRefId, toPeerId, this.ChannelId, message);
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000C024 File Offset: 0x0000A224
		public EResult SendPeerBroadcast(ResultRelayer resultRelayer, uint sessionRefId, INetProperty message)
		{
			if (resultRelayer == null || resultRelayer.Handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("resultRelayer");
			}
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			if (!this.isRegistered)
			{
				throw new InvalidOperationException("The peer channel is not registered in the controller.");
			}
			return PeerChannelController.SendPeerBroadcast(resultRelayer, sessionRefId, this.ChannelId, message);
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000C088 File Offset: 0x0000A288
		public EResult SendPeerMessageBroadcast(ResultRelayer resultRelayer, uint sessionRefId, PeerMessage message)
		{
			if (resultRelayer == null || resultRelayer.Handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("resultRelayer");
			}
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			if (!this.isRegistered)
			{
				throw new InvalidOperationException("The peer channel is not registered in the controller.");
			}
			return PeerChannelController.SendPeerMessageBroadcast(resultRelayer, sessionRefId, this.ChannelId, message);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00002242 File Offset: 0x00000442
		protected virtual EResult OnPeerBroadcast(uint sessionRefId, uint senderPeerId, INetProperty netProperty, uint sessionTime)
		{
			return EResult.Create(EResult.ECode.OK, "");
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00002242 File Offset: 0x00000442
		protected virtual EResult OnPeerMessageBroadcast(uint sessionRefId, uint senderPeerId, PeerMessage peerMessage, uint sessionTime)
		{
			return EResult.Create(EResult.ECode.OK, "");
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00002242 File Offset: 0x00000442
		protected virtual EResult OnPeerUnicast(uint sessionRefId, uint senderPeerId, INetProperty netProperty, uint sessionTime)
		{
			return EResult.Create(EResult.ECode.OK, "");
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00002242 File Offset: 0x00000442
		protected virtual EResult OnPeerMessageUnicast(uint sessionRefId, uint senderPeerId, PeerMessage peerMessage, uint sessionTime)
		{
			return EResult.Create(EResult.ECode.OK, "");
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000C0E4 File Offset: 0x0000A2E4
		[MethodImpl(256)]
		internal static EResult OnPeerBroadcast(uint sessionRefId, uint senderPeerId, PeerChannel peerChannel, INetProperty netProperty, uint sessionTime)
		{
			return peerChannel.OnPeerBroadcast(sessionRefId, senderPeerId, netProperty, sessionTime);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000C0F1 File Offset: 0x0000A2F1
		[MethodImpl(256)]
		internal static EResult OnPeerMessageBroadcast(uint sessionRefId, uint senderPeerId, PeerChannel peerChannel, PeerMessage peerMessage, uint sessionTime)
		{
			return peerChannel.OnPeerMessageBroadcast(sessionRefId, senderPeerId, peerMessage, sessionTime);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000C0FE File Offset: 0x0000A2FE
		[MethodImpl(256)]
		internal static EResult OnPeerUnicast(uint sessionRefId, uint senderPeerId, PeerChannel peerChannel, INetProperty netProperty, uint sessionTime)
		{
			return peerChannel.OnPeerUnicast(sessionRefId, senderPeerId, netProperty, sessionTime);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000C10B File Offset: 0x0000A30B
		[MethodImpl(256)]
		internal static EResult OnPeerMessageUnicast(uint sessionRefId, uint senderPeerId, PeerChannel peerChannel, PeerMessage peerMessage, uint sessionTime)
		{
			return peerChannel.OnPeerMessageUnicast(sessionRefId, senderPeerId, peerMessage, sessionTime);
		}

		// Token: 0x04000286 RID: 646
		private volatile StringId channelId;

		// Token: 0x04000287 RID: 647
		private object channelLock;

		// Token: 0x04000288 RID: 648
		private StringId sessionType;

		// Token: 0x04000289 RID: 649
		private bool isRegistered;

		// Token: 0x0400028A RID: 650
		private Dictionary<uint, PeerChannel.BroadcastEntry> broadcastEntries;

		// Token: 0x0400028B RID: 651
		private Dictionary<uint, PeerChannel.UnicastEntry> unicastEntries;

		// Token: 0x020000C3 RID: 195
		private struct BroadcastEntry
		{
			// Token: 0x0400028C RID: 652
			public StringId netProtocolEntryId;

			// Token: 0x0400028D RID: 653
			public Type netProtocolEntryType;

			// Token: 0x0400028E RID: 654
			public StringId transportMode;

			// Token: 0x0400028F RID: 655
			public bool sessionTimeSupport;
		}

		// Token: 0x020000C4 RID: 196
		private struct UnicastEntry
		{
			// Token: 0x04000290 RID: 656
			public StringId netProtocolEntryId;

			// Token: 0x04000291 RID: 657
			public Type netProtocolEntryType;

			// Token: 0x04000292 RID: 658
			public StringId transportMode;

			// Token: 0x04000293 RID: 659
			public bool sessionTimeSupport;
		}
	}
}
