using System;
using System.Runtime.InteropServices;

namespace StormDotNet.Extensions
{
	// Token: 0x020000FC RID: 252
	internal static class MarshalEx
	{
		// Token: 0x060004AC RID: 1196 RVA: 0x0000DED4 File Offset: 0x0000C0D4
		public static void WriteBoolean(IntPtr ptr, bool value, int sizeOfBool)
		{
			if (ptr == IntPtr.Zero)
			{
				throw new ArgumentNullException("ptr");
			}
			byte b = ((!value) ? 0 : 1);
			switch (sizeOfBool)
			{
			case 1:
				Marshal.WriteByte(ptr, b);
				return;
			case 2:
				Marshal.WriteInt16(ptr, (short)b);
				return;
			case 3:
				break;
			case 4:
				Marshal.WriteInt32(ptr, (int)b);
				return;
			default:
				if (sizeOfBool != 8)
				{
					return;
				}
				Marshal.WriteInt64(ptr, (long)((ulong)b));
				break;
			}
		}
	}
}
