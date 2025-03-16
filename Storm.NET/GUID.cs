using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace StormDotNet
{
	// Token: 0x02000027 RID: 39
	public sealed class GUID
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00002FF1 File Offset: 0x000011F1
		public byte Length
		{
			get
			{
				return (byte)this.buffer.Length;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00002FFC File Offset: 0x000011FC
		internal byte[] Data
		{
			get
			{
				return this.buffer;
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003004 File Offset: 0x00001204
		public GUID()
		{
			this.buffer = new byte[0];
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003018 File Offset: 0x00001218
		public GUID(byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (buffer.Length > GUID.MAX_LENGTH)
			{
				throw new ArgumentException(string.Format("A GUID cannot be more than {0} bytes.", GUID.MAX_LENGTH), "buffer");
			}
			this.buffer = (byte[])buffer.Clone();
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003073 File Offset: 0x00001273
		internal unsafe GUID(IntPtr buffer, byte length)
			: this((byte*)buffer.ToPointer(), length)
		{
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003084 File Offset: 0x00001284
		internal unsafe GUID(byte* buffer, byte length)
		{
			if (length <= 0)
			{
				this.buffer = new byte[0];
				return;
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			this.buffer = new byte[(int)length];
			Marshal.Copy(new IntPtr((void*)buffer), this.buffer, 0, (int)length);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000030D8 File Offset: 0x000012D8
		public override bool Equals(object obj)
		{
			if (obj != null && obj is GUID)
			{
				GUID guid = obj as GUID;
				if (this.buffer.Length == guid.buffer.Length)
				{
					return this.buffer.SequenceEqual(guid.buffer);
				}
			}
			return false;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000311C File Offset: 0x0000131C
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

		// Token: 0x060000AC RID: 172 RVA: 0x00003158 File Offset: 0x00001358
		public override string ToString()
		{
			string text = string.Empty;
			for (int i = 0; i < this.buffer.Length; i++)
			{
				text += this.buffer[i].ToString("X2");
			}
			return text;
		}

		// Token: 0x04000023 RID: 35
		public static readonly GUID INVALID_GUID = new GUID();

		// Token: 0x04000024 RID: 36
		public static readonly int MAX_LENGTH = 255;

		// Token: 0x04000025 RID: 37
		private byte[] buffer;
	}
}
