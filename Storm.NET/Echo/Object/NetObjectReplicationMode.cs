using System;

namespace StormDotNet.Echo.Object
{
	// Token: 0x020000EB RID: 235
	public struct NetObjectReplicationMode
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x0000D707 File Offset: 0x0000B907
		public StringId Mode
		{
			get
			{
				return this.mode;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x0000D70F File Offset: 0x0000B90F
		public uint SessionPeerId
		{
			get
			{
				return this.sessionPeerId;
			}
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0000D717 File Offset: 0x0000B917
		public NetObjectReplicationMode SetDefault()
		{
			this.mode = NetObjectReplicationMode.EverySessionPeerMode;
			this.sessionPeerId = uint.MaxValue;
			return this;
		}

		// Token: 0x040002A6 RID: 678
		public static readonly StringId ManualMode = new StringId("ObjectReplicationMode.ManualMode");

		// Token: 0x040002A7 RID: 679
		public static readonly StringId ExcludeSessionPeerMode = new StringId("ObjectReplicationMode.ExcludeSessionPeerMode");

		// Token: 0x040002A8 RID: 680
		public static readonly StringId UniqueSessionPeerMode = new StringId("ObjectReplicationMode.UniqueSessionPeerMode");

		// Token: 0x040002A9 RID: 681
		public static readonly StringId EverySessionPeerMode = new StringId("ObjectReplicationMode.EverySessionPeerMode");

		// Token: 0x040002AA RID: 682
		private StringId mode;

		// Token: 0x040002AB RID: 683
		private uint sessionPeerId;
	}
}
