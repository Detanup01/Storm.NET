using System;

namespace StormDotNet
{
	// Token: 0x02000038 RID: 56
	public sealed class StringId
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x0000372A File Offset: 0x0000192A
		public uint UniqueID
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00003732 File Offset: 0x00001932
		public bool IsValid
		{
			get
			{
				return this.value != uint.MaxValue;
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00003740 File Offset: 0x00001940
		public StringId(string str)
		{
			this.value = CRC32.Compute(str);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00003754 File Offset: 0x00001954
		public StringId(uint crc)
		{
			this.value = crc;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00003764 File Offset: 0x00001964
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is StringId)
			{
				return this.value == (obj as StringId).value;
			}
			if (obj is sbyte || obj is short || obj is int || obj is long)
			{
				return (ulong)this.value == (ulong)Convert.ToInt64(obj);
			}
			if (obj is byte || obj is ushort || obj is uint || obj is ulong)
			{
				return (ulong)this.value == Convert.ToUInt64(obj);
			}
			return obj is string && this.value == CRC32.Compute((string)obj);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000372A File Offset: 0x0000192A
		public override int GetHashCode()
		{
			return (int)this.value;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000380F File Offset: 0x00001A0F
		public override string ToString()
		{
			return string.Format("0x{0:X8}", this.value);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00003826 File Offset: 0x00001A26
		public static bool operator ==(StringId a, StringId b)
		{
			return a == b || (a != null && b != null && a.value == b.value);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000372A File Offset: 0x0000192A
		public static implicit operator uint(StringId strId)
		{
			return strId.value;
		}

		// Token: 0x0400004A RID: 74
		private uint value;
	}
}
