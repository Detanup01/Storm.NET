using System;

namespace StormDotNet
{
	// Token: 0x02000023 RID: 35
	public static class Endian
	{
		// Token: 0x06000099 RID: 153 RVA: 0x00002EB7 File Offset: 0x000010B7
		public static ulong FromNetworkOrder(ulong value)
		{
			if (BitConverter.IsLittleEndian)
			{
				return Endian.Swap(value);
			}
			return value;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00002EC8 File Offset: 0x000010C8
		public static ulong Swap(ulong value)
		{
			return value = ((value >> 56) & 255UL) | ((value >> 40) & 65280UL) | ((value >> 24) & 16711680UL) | ((value >> 8) & (ulong)(-16777216)) | ((value << 8) & 1095216660480UL) | ((value << 24) & 280375465082880UL) | ((value << 40) & 71776119061217280UL) | ((value << 56) & 18374686479671623680UL);
		}
	}
}
