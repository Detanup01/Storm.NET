using System;
using System.Globalization;

namespace StormDotNet
{
	// Token: 0x0200003A RID: 58
	public struct Vector2
	{
		// Token: 0x060000DD RID: 221 RVA: 0x00003858 File Offset: 0x00001A58
		public Vector2(float x, float y)
		{
			this.X = x;
			this.Y = y;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003868 File Offset: 0x00001A68
		public static bool operator ==(Vector2 left, Vector2 right)
		{
			return left.X == right.X && left.Y == right.Y;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00003888 File Offset: 0x00001A88
		public override int GetHashCode()
		{
			return this.X.GetHashCode() + this.Y.GetHashCode();
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000038A1 File Offset: 0x00001AA1
		public override bool Equals(object obj)
		{
			return obj is Vector2 && this == (Vector2)obj;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000038C0 File Offset: 0x00001AC0
		public override string ToString()
		{
			return string.Format("<{1}{0} {2}>", NumberFormatInfo.GetInstance(CultureInfo.CurrentCulture).NumberGroupSeparator, this.X.ToString("G", CultureInfo.CurrentCulture), this.Y.ToString("G", CultureInfo.CurrentCulture));
		}

		// Token: 0x0400004C RID: 76
		public float X;

		// Token: 0x0400004D RID: 77
		public float Y;
	}
}
