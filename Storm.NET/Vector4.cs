using System;
using System.Globalization;

namespace StormDotNet
{
	// Token: 0x0200003C RID: 60
	public struct Vector4
	{
		// Token: 0x060000E7 RID: 231 RVA: 0x00003A0F File Offset: 0x00001C0F
		public Vector4(float x, float y, float z, float w)
		{
			this.W = w;
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00003A2E File Offset: 0x00001C2E
		public static bool operator ==(Vector4 left, Vector4 right)
		{
			return left.X == right.X && left.Y == right.Y && left.Z == right.Z && left.W == right.W;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00003A6A File Offset: 0x00001C6A
		public override int GetHashCode()
		{
			return this.X.GetHashCode() + this.Y.GetHashCode() + this.Z.GetHashCode() + this.W.GetHashCode();
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00003A9B File Offset: 0x00001C9B
		public override bool Equals(object obj)
		{
			return obj is Vector4 && this == (Vector4)obj;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00003AB8 File Offset: 0x00001CB8
		public override string ToString()
		{
			return string.Format("<{1}{0} {2}{0} {3}{0}, {4}>", new object[]
			{
				NumberFormatInfo.GetInstance(CultureInfo.CurrentCulture).NumberGroupSeparator,
				this.X.ToString("G", CultureInfo.CurrentCulture),
				this.Y.ToString("G", CultureInfo.CurrentCulture),
				this.Z.ToString("G", CultureInfo.CurrentCulture),
				this.W.ToString("G", CultureInfo.CurrentCulture)
			});
		}

		// Token: 0x04000051 RID: 81
		public float X;

		// Token: 0x04000052 RID: 82
		public float Y;

		// Token: 0x04000053 RID: 83
		public float Z;

		// Token: 0x04000054 RID: 84
		public float W;
	}
}
