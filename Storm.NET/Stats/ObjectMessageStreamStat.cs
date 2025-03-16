using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;

namespace StormDotNet.Stats
{
	// Token: 0x020000F1 RID: 241
	public struct ObjectMessageStreamStat
	{
		// Token: 0x0600049E RID: 1182 RVA: 0x0000D89C File Offset: 0x0000BA9C
		internal ObjectMessageStreamStat(BinaryReader reader)
		{
			if (4022212636U != reader.ReadUInt32())
			{
				throw new SerializationException("Invalid magic number for a ObjectMessageStreamStat.");
			}
			this.headerSize = reader.ReadUInt32();
			this.dataSize = reader.ReadUInt32();
			this.count = reader.ReadUInt32();
			List<ObjectMessageStat> list = new List<ObjectMessageStat>();
			int i = 0;
			int num = reader.ReadInt32();
			while (i < num)
			{
				list.Add(new ObjectMessageStat(reader));
				i++;
			}
			this.messages = list.AsReadOnly();
		}

		// Token: 0x040002C4 RID: 708
		private uint headerSize;

		// Token: 0x040002C5 RID: 709
		private uint dataSize;

		// Token: 0x040002C6 RID: 710
		private uint count;

		// Token: 0x040002C7 RID: 711
		private ReadOnlyCollection<ObjectMessageStat> messages;
	}
}
