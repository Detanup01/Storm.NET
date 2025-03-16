using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;

namespace StormDotNet.Stats
{
	// Token: 0x020000F8 RID: 248
	public struct FrameStat
	{
		// Token: 0x060004A5 RID: 1189 RVA: 0x0000DCD8 File Offset: 0x0000BED8
		internal FrameStat(BinaryReader reader)
		{
			if (4197360668U != reader.ReadUInt32())
			{
				throw new SerializationException("Invalid magic number for a FrameStat.");
			}
			this.id = reader.ReadUInt32();
			this.size = reader.ReadUInt32();
			this.compressionRatio = reader.ReadSingle();
			this.averageLossRatio = reader.ReadSingle();
			this.bitRate = reader.ReadUInt32();
			this.upstream = reader.ReadUInt32();
			this.packetSentCount = reader.ReadUInt32();
			this.packetLostCount = reader.ReadUInt32();
			this.interval = reader.ReadUInt32();
			this.time = reader.ReadUInt64();
			this.count = reader.ReadUInt32();
			List<PacketStat> list = new List<PacketStat>();
			int i = 0;
			int num = reader.ReadInt32();
			while (i < num)
			{
				list.Add(new PacketStat(reader));
				i++;
			}
			this.packets = list.AsReadOnly();
		}

		// Token: 0x040002F8 RID: 760
		private uint id;

		// Token: 0x040002F9 RID: 761
		private uint size;

		// Token: 0x040002FA RID: 762
		private float compressionRatio;

		// Token: 0x040002FB RID: 763
		private float averageLossRatio;

		// Token: 0x040002FC RID: 764
		private uint bitRate;

		// Token: 0x040002FD RID: 765
		private uint upstream;

		// Token: 0x040002FE RID: 766
		private uint packetSentCount;

		// Token: 0x040002FF RID: 767
		private uint packetLostCount;

		// Token: 0x04000300 RID: 768
		private uint interval;

		// Token: 0x04000301 RID: 769
		private ulong time;

		// Token: 0x04000302 RID: 770
		private uint count;

		// Token: 0x04000303 RID: 771
		private ReadOnlyCollection<PacketStat> packets;
	}
}
