using System;
using System.IO;
using System.Runtime.Serialization;

namespace StormDotNet.Stats
{
	// Token: 0x020000EF RID: 239
	public struct VoiceChatStat
	{
		// Token: 0x0600049C RID: 1180 RVA: 0x0000D7F2 File Offset: 0x0000B9F2
		internal VoiceChatStat(BinaryReader reader)
		{
			if (2265637180U != reader.ReadUInt32())
			{
				throw new SerializationException("Invalid magic number for a VoiceChatStat.");
			}
			this.headerSize = reader.ReadUInt32();
			this.dataSize = reader.ReadUInt32();
			this.count = reader.ReadUInt32();
		}

		// Token: 0x040002BC RID: 700
		private uint headerSize;

		// Token: 0x040002BD RID: 701
		private uint dataSize;

		// Token: 0x040002BE RID: 702
		private uint count;
	}
}
