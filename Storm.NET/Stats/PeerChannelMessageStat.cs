using System;
using System.IO;
using System.Runtime.Serialization;

namespace StormDotNet.Stats
{
	// Token: 0x020000F5 RID: 245
	public struct PeerChannelMessageStat
	{
		// Token: 0x060004A2 RID: 1186 RVA: 0x0000DAC8 File Offset: 0x0000BCC8
		internal PeerChannelMessageStat(BinaryReader reader)
		{
			if (4079607964U != reader.ReadUInt32())
			{
				throw new SerializationException("Invalid magic number for a PeerChannelMessageStat.");
			}
			this.headerSize = reader.ReadUInt32();
			this.dataSize = reader.ReadUInt32();
			this.count = reader.ReadUInt32();
			this.channelId = new StringId(reader.ReadUInt32());
			this.protocolEntryId = new StringId(reader.ReadUInt32());
			this.dataContainerId = new StringId(reader.ReadUInt32());
		}

		// Token: 0x040002DA RID: 730
		private uint headerSize;

		// Token: 0x040002DB RID: 731
		private uint dataSize;

		// Token: 0x040002DC RID: 732
		private uint count;

		// Token: 0x040002DD RID: 733
		private StringId channelId;

		// Token: 0x040002DE RID: 734
		private StringId protocolEntryId;

		// Token: 0x040002DF RID: 735
		private StringId dataContainerId;
	}
}
