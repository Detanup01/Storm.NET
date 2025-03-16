using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;

namespace StormDotNet.Stats
{
	// Token: 0x020000F3 RID: 243
	public struct ObjectStat
	{
		// Token: 0x060004A0 RID: 1184 RVA: 0x0000D990 File Offset: 0x0000BB90
		internal ObjectStat(BinaryReader reader)
		{
			if (872195132U != reader.ReadUInt32())
			{
				throw new SerializationException("Invalid magic number for a ObjectStat.");
			}
			this.objectGUID = new ObjectGUID(reader.ReadBytes((int)reader.ReadByte()));
			this.objectTypeId = new StringId(reader.ReadUInt32());
			this.headerSize = reader.ReadUInt32();
			this.dataSize = reader.ReadUInt32();
			this.count = reader.ReadUInt32();
			List<MementoStat> list = new List<MementoStat>();
			int i = 0;
			int num = reader.ReadInt32();
			while (i < num)
			{
				list.Add(new MementoStat(reader));
				i++;
			}
			this.mementos = list.AsReadOnly();
			this.messageStream = new ObjectMessageStreamStat(reader);
		}

		// Token: 0x040002CE RID: 718
		private ObjectGUID objectGUID;

		// Token: 0x040002CF RID: 719
		private StringId objectTypeId;

		// Token: 0x040002D0 RID: 720
		private uint headerSize;

		// Token: 0x040002D1 RID: 721
		private uint dataSize;

		// Token: 0x040002D2 RID: 722
		private uint count;

		// Token: 0x040002D3 RID: 723
		private ReadOnlyCollection<MementoStat> mementos;

		// Token: 0x040002D4 RID: 724
		private ObjectMessageStreamStat messageStream;
	}
}
