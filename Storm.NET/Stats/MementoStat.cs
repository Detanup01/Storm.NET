using System;
using System.IO;
using System.Runtime.Serialization;

namespace StormDotNet.Stats
{
	// Token: 0x020000F2 RID: 242
	public struct MementoStat
	{
		// Token: 0x0600049F RID: 1183 RVA: 0x0000D918 File Offset: 0x0000BB18
		internal MementoStat(BinaryReader reader)
		{
			if (795618716U != reader.ReadUInt32())
			{
				throw new SerializationException("Invalid magic number for a MementoStat.");
			}
			this.headerSize = reader.ReadUInt32();
			this.dataSize = reader.ReadUInt32();
			this.count = reader.ReadUInt32();
			this.protocolEntryId = new StringId(reader.ReadUInt32());
			this.dataContainerId = new StringId(reader.ReadUInt32());
			this.dirtyBits = reader.ReadString();
		}

		// Token: 0x040002C8 RID: 712
		private uint headerSize;

		// Token: 0x040002C9 RID: 713
		private uint dataSize;

		// Token: 0x040002CA RID: 714
		private uint count;

		// Token: 0x040002CB RID: 715
		private StringId protocolEntryId;

		// Token: 0x040002CC RID: 716
		private StringId dataContainerId;

		// Token: 0x040002CD RID: 717
		private string dirtyBits;
	}
}
