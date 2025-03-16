using System;
using System.IO;
using System.Runtime.Serialization;

namespace StormDotNet.Stats
{
	// Token: 0x020000F0 RID: 240
	public struct ObjectMessageStat
	{
		// Token: 0x0600049D RID: 1181 RVA: 0x0000D830 File Offset: 0x0000BA30
		internal ObjectMessageStat(BinaryReader reader)
		{
			if (3275841692U != reader.ReadUInt32())
			{
				throw new SerializationException("Invalid magic number for a ObjectMessageStat.");
			}
			this.headerSize = reader.ReadUInt32();
			this.dataSize = reader.ReadUInt32();
			this.count = reader.ReadUInt32();
			this.protocolEntryId = new StringId(reader.ReadUInt32());
			this.dataContainerId = new StringId(reader.ReadUInt32());
		}

		// Token: 0x040002BF RID: 703
		private uint headerSize;

		// Token: 0x040002C0 RID: 704
		private uint dataSize;

		// Token: 0x040002C1 RID: 705
		private uint count;

		// Token: 0x040002C2 RID: 706
		private StringId protocolEntryId;

		// Token: 0x040002C3 RID: 707
		private StringId dataContainerId;
	}
}
