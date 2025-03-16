using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;

namespace StormDotNet.Stats
{
	// Token: 0x020000F4 RID: 244
	public struct ObjectStreamStat
	{
		// Token: 0x060004A1 RID: 1185 RVA: 0x0000DA40 File Offset: 0x0000BC40
		internal ObjectStreamStat(BinaryReader reader)
		{
			if (2141048860U != reader.ReadUInt32())
			{
				throw new SerializationException("Invalid magic number for a ObjectStreamStat.");
			}
			this.headerSize = reader.ReadUInt32();
			this.dataSize = reader.ReadUInt32();
			this.skippedSize = reader.ReadUInt32();
			this.count = reader.ReadUInt32();
			List<ObjectStat> list = new List<ObjectStat>();
			int i = 0;
			int num = reader.ReadInt32();
			while (i < num)
			{
				list.Add(new ObjectStat(reader));
				i++;
			}
			this.objects = list.AsReadOnly();
		}

		// Token: 0x040002D5 RID: 725
		private uint headerSize;

		// Token: 0x040002D6 RID: 726
		private uint dataSize;

		// Token: 0x040002D7 RID: 727
		private uint skippedSize;

		// Token: 0x040002D8 RID: 728
		private uint count;

		// Token: 0x040002D9 RID: 729
		private ReadOnlyCollection<ObjectStat> objects;
	}
}
