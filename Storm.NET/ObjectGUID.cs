using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace StormDotNet
{
	// Token: 0x02000034 RID: 52
	public sealed class ObjectGUID
	{
		// Token: 0x060000C6 RID: 198 RVA: 0x00003408 File Offset: 0x00001608
		public ObjectGUID()
		{
			this.Reset();
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003418 File Offset: 0x00001618
		public ObjectGUID(byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (ObjectGUID.UseFixedSize)
			{
				if (buffer.Length != ObjectGUID.MAX_LENGTH)
				{
					throw new ArgumentException(string.Format("An ObjectGUID must have {0} bytes.", ObjectGUID.MAX_LENGTH), "buffer");
				}
			}
			else if (buffer.Length > ObjectGUID.MAX_LENGTH)
			{
				throw new ArgumentException(string.Format("An ObjectGUID cannot be more than {0} bytes.", ObjectGUID.MAX_LENGTH), "buffer");
			}
			this.buffer = (byte[])buffer.Clone();
			if (ObjectGUID.UseFixedSize)
			{
				this.value = BitConverter.ToUInt64(this.buffer, 0);
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000034BC File Offset: 0x000016BC
		public override bool Equals(object obj)
		{
			if (obj != null && obj is ObjectGUID)
			{
				ObjectGUID objectGUID = obj as ObjectGUID;
				if (ObjectGUID.UseFixedSize)
				{
					return this.value == objectGUID.value;
				}
				if (this.buffer.Length == objectGUID.buffer.Length)
				{
					return this.buffer.SequenceEqual(objectGUID.buffer);
				}
			}
			return false;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003518 File Offset: 0x00001718
		public override int GetHashCode()
		{
			uint num = 2166136261U;
			for (int i = 0; i < this.buffer.Length; i++)
			{
				num ^= (uint)this.buffer[i];
				num *= 16777619U;
			}
			return (int)num;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003554 File Offset: 0x00001754
		public override string ToString()
		{
			if (ObjectGUID.UseFixedSize)
			{
				return Endian.FromNetworkOrder(this.value).ToString("X16");
			}
			string text = string.Empty;
			for (int i = 0; i < this.buffer.Length; i++)
			{
				text += this.buffer[i].ToString("X2");
			}
			return text;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000035B8 File Offset: 0x000017B8
		public void Reset()
		{
			if (ObjectGUID.UseFixedSize)
			{
				this.value = ulong.MaxValue;
				this.buffer = BitConverter.GetBytes(this.value);
				return;
			}
			this.buffer = new byte[0];
		}

		// Token: 0x04000033 RID: 51
		private static readonly bool UseFixedSize = ObjectGUID.Native.UseFixedSizeGUID();

		// Token: 0x04000034 RID: 52
		public static readonly ObjectGUID INVALID_GUID = new ObjectGUID();

		// Token: 0x04000035 RID: 53
		public static readonly int MAX_LENGTH = (ObjectGUID.UseFixedSize ? 8 : 255);

		// Token: 0x04000036 RID: 54
		private ulong value;

		// Token: 0x04000037 RID: 55
		private byte[] buffer;

		// Token: 0x02000035 RID: 53
		private static class Native
		{
			// Token: 0x060000CD RID: 205
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "UseFixedSizeObjectGuid")]
			[return: MarshalAs(UnmanagedType.U1)]
			public static extern bool UseFixedSizeGUID();
		}
	}
}
