using System;
using System.Globalization;

namespace StormDotNet
{
	// Token: 0x0200003B RID: 59
	public struct Vector3
	{
		// Token: 0x060000E2 RID: 226 RVA: 0x00003910 File Offset: 0x00001B10
		public Vector3(float x, float y, float z)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00003927 File Offset: 0x00001B27
		public static bool operator ==(Vector3 left, Vector3 right)
		{
			return left.X == right.X && left.Y == right.Y && left.Z == right.Z;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00003955 File Offset: 0x00001B55
		public override int GetHashCode()
		{
			return this.X.GetHashCode() + this.Y.GetHashCode() + this.Z.GetHashCode();
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000397A File Offset: 0x00001B7A
		public override bool Equals(object obj)
		{
			return obj is Vector3 && this == (Vector3)obj;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00003998 File Offset: 0x00001B98
		public override string ToString()
		{
			return string.Format("<{1}{0} {2}{0} {3}>", new object[]
			{
				NumberFormatInfo.GetInstance(CultureInfo.CurrentCulture).NumberGroupSeparator,
				this.X.ToString("G", CultureInfo.CurrentCulture),
				this.Y.ToString("G", CultureInfo.CurrentCulture),
				this.Z.ToString("G", CultureInfo.CurrentCulture)
			});
		}

		// Token: 0x0400004E RID: 78
		public float X;

		// Token: 0x0400004F RID: 79
		public float Y;

		// Token: 0x04000050 RID: 80
		public float Z;
	}
}
