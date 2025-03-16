using System;

namespace StormDotNet
{
	// Token: 0x0200000E RID: 14
	public sealed class DataBuffer : Data<byte[]>
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002566 File Offset: 0x00000766
		public override DataType DataType
		{
			get
			{
				return DataType.Buffer;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600003D RID: 61 RVA: 0x0000256A File Offset: 0x0000076A
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00002574 File Offset: 0x00000774
		public unsafe override byte[] Value
		{
			get
			{
				return this.value;
			}
			set
			{
				using (StormEngine.SetProfilePoint(null))
				{
					uint num = CRC32.Compute(value, value.Length);
					if (this.value.Length != value.Length || this.checksum != num)
					{
						this.value = value;
						this.checksum = num;
						try
						{
							byte[] array;
							byte* ptr;
							if ((array = this.value) == null || array.Length == 0)
							{
								ptr = null;
							}
							else
							{
								ptr = &array[0];
							}
							BaseData.Native.SetBuffer(base.DataContainer.Handle, base.Index, new IntPtr((void*)ptr), this.value.Length);
						}
						finally
						{
							byte[] array = null;
						}
					}
				}
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002624 File Offset: 0x00000824
		protected internal override string[] ExtraParams
		{
			get
			{
				return new string[0];
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000040 RID: 64 RVA: 0x0000262C File Offset: 0x0000082C
		protected internal override string DefaultValue
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002633 File Offset: 0x00000833
		public DataBuffer(DataContainer dataContainer)
			: base(dataContainer)
		{
			this.value = new byte[0];
			this.checksum = CRC32.Compute(this.value, this.value.Length);
			base.Register();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002667 File Offset: 0x00000867
		protected internal override void UpdateValue(byte[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.value = value;
		}

		// Token: 0x04000008 RID: 8
		private byte[] value;

		// Token: 0x04000009 RID: 9
		private uint checksum;
	}
}
