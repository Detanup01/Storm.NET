using System;
using System.Runtime.InteropServices;

namespace StormDotNet
{
	// Token: 0x02000029 RID: 41
	public class LogDevice
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00003244 File Offset: 0x00001444
		internal IntPtr Handle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x0000324C File Offset: 0x0000144C
		protected internal LogDevice(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("handle");
			}
			this.handle = handle;
			this.gcHandle = GCHandle.Alloc(this);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000327F File Offset: 0x0000147F
		public virtual void ReleaseInstance()
		{
			this.handle = IntPtr.Zero;
			this.gcHandle.Free();
		}

		// Token: 0x04000026 RID: 38
		private IntPtr handle;

		// Token: 0x04000027 RID: 39
		private GCHandle gcHandle;
	}
}
