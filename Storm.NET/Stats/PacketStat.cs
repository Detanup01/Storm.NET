using System;
using System.IO;
using System.Runtime.Serialization;

namespace StormDotNet.Stats
{
	// Token: 0x020000F7 RID: 247
	public struct PacketStat
	{
		// Token: 0x060004A4 RID: 1188 RVA: 0x0000DBC0 File Offset: 0x0000BDC0
		internal PacketStat(BinaryReader reader)
		{
			if (1217577916U != reader.ReadUInt32())
			{
				throw new SerializationException("Invalid magic number for a PacketStat.");
			}
			this.id = reader.ReadUInt32();
			this.packetType = (PacketType)reader.ReadByte();
			this.peerId = reader.ReadUInt32();
			this.udpDataSize = reader.ReadUInt32();
			this.udpHeaderSize = reader.ReadUInt32();
			this.mpeDataSize = reader.ReadUInt32();
			this.mpeHeaderSize = reader.ReadUInt32();
			this.overhead = reader.ReadUInt32();
			this.routed = reader.ReadBoolean();
			this.routedIPAddressSrc = reader.ReadString();
			this.routedIPAddressDst = reader.ReadString();
			this.routedPortSrc = reader.ReadUInt16();
			this.routedPortDst = reader.ReadUInt16();
			this.count = reader.ReadUInt32();
			this.ipAddress = reader.ReadString();
			this.sent = reader.ReadBoolean();
			this.peerChannelStat = new PeerChannelStat(reader);
			this.objectStreamStat = new ObjectStreamStat(reader);
			this.voiceChatStat = new VoiceChatStat(reader);
			this.canceled = reader.ReadBoolean();
		}

		// Token: 0x040002E4 RID: 740
		private uint id;

		// Token: 0x040002E5 RID: 741
		private PacketType packetType;

		// Token: 0x040002E6 RID: 742
		private uint peerId;

		// Token: 0x040002E7 RID: 743
		private uint udpDataSize;

		// Token: 0x040002E8 RID: 744
		private uint udpHeaderSize;

		// Token: 0x040002E9 RID: 745
		private uint mpeDataSize;

		// Token: 0x040002EA RID: 746
		private uint mpeHeaderSize;

		// Token: 0x040002EB RID: 747
		private uint overhead;

		// Token: 0x040002EC RID: 748
		private bool routed;

		// Token: 0x040002ED RID: 749
		private string routedIPAddressSrc;

		// Token: 0x040002EE RID: 750
		private string routedIPAddressDst;

		// Token: 0x040002EF RID: 751
		private ushort routedPortSrc;

		// Token: 0x040002F0 RID: 752
		private ushort routedPortDst;

		// Token: 0x040002F1 RID: 753
		private uint count;

		// Token: 0x040002F2 RID: 754
		private string ipAddress;

		// Token: 0x040002F3 RID: 755
		private bool sent;

		// Token: 0x040002F4 RID: 756
		private PeerChannelStat peerChannelStat;

		// Token: 0x040002F5 RID: 757
		private ObjectStreamStat objectStreamStat;

		// Token: 0x040002F6 RID: 758
		private VoiceChatStat voiceChatStat;

		// Token: 0x040002F7 RID: 759
		private bool canceled;
	}
}
