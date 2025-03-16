using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;

namespace StormDotNet.Stats
{
	// Token: 0x020000F6 RID: 246
	public struct PeerChannelStat
	{
		// Token: 0x060004A3 RID: 1187 RVA: 0x0000DB44 File Offset: 0x0000BD44
		internal PeerChannelStat(BinaryReader reader)
		{
			if (113120060U != reader.ReadUInt32())
			{
				throw new SerializationException("Invalid magic number for a PeerChannelStat.");
			}
			this.headerSize = reader.ReadUInt32();
			this.dataSize = reader.ReadUInt32();
			this.count = reader.ReadUInt32();
			List<PeerChannelMessageStat> list = new List<PeerChannelMessageStat>();
			int i = 0;
			int num = reader.ReadInt32();
			while (i < num)
			{
				list.Add(new PeerChannelMessageStat(reader));
				i++;
			}
			this.messages = list.AsReadOnly();
		}

		// Token: 0x040002E0 RID: 736
		private uint headerSize;

		// Token: 0x040002E1 RID: 737
		private uint dataSize;

		// Token: 0x040002E2 RID: 738
		private uint count;

		// Token: 0x040002E3 RID: 739
		private ReadOnlyCollection<PeerChannelMessageStat> messages;
	}
}
