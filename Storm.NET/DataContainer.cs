using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace StormDotNet
{
	// Token: 0x0200000F RID: 15
	public class DataContainer
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00002680 File Offset: 0x00000880
		internal static void Initialize()
		{
			List<GCHandle> list = DataContainer.gcHandles;
			lock (list)
			{
				DataContainer.Native.GetDataFormat getDataFormat = new DataContainer.Native.GetDataFormat(DataContainer.GetDataFormat);
				GCHandle gchandle = GCHandle.Alloc(getDataFormat, GCHandleType.Normal);
				DataContainer.gcHandles.Add(gchandle);
				DataContainer.Native.UpdateInt8 updateInt = new DataContainer.Native.UpdateInt8(DataContainer.Update);
				gchandle = GCHandle.Alloc(updateInt, GCHandleType.Normal);
				DataContainer.gcHandles.Add(gchandle);
				DataContainer.Native.UpdateUInt8 updateUInt = new DataContainer.Native.UpdateUInt8(DataContainer.Update);
				gchandle = GCHandle.Alloc(updateUInt, GCHandleType.Normal);
				DataContainer.gcHandles.Add(gchandle);
				DataContainer.Native.UpdateInt16 updateInt2 = new DataContainer.Native.UpdateInt16(DataContainer.Update);
				gchandle = GCHandle.Alloc(updateInt2, GCHandleType.Normal);
				DataContainer.gcHandles.Add(gchandle);
				DataContainer.Native.UpdateUInt16 updateUInt2 = new DataContainer.Native.UpdateUInt16(DataContainer.Update);
				gchandle = GCHandle.Alloc(updateUInt2, GCHandleType.Normal);
				DataContainer.gcHandles.Add(gchandle);
				DataContainer.Native.UpdateInt32 updateInt3 = new DataContainer.Native.UpdateInt32(DataContainer.Update);
				gchandle = GCHandle.Alloc(updateInt3, GCHandleType.Normal);
				DataContainer.gcHandles.Add(gchandle);
				DataContainer.Native.UpdateUInt32 updateUInt3 = new DataContainer.Native.UpdateUInt32(DataContainer.Update);
				gchandle = GCHandle.Alloc(updateUInt3, GCHandleType.Normal);
				DataContainer.gcHandles.Add(gchandle);
				DataContainer.Native.UpdateInt64 updateInt4 = new DataContainer.Native.UpdateInt64(DataContainer.Update);
				gchandle = GCHandle.Alloc(updateInt4, GCHandleType.Normal);
				DataContainer.gcHandles.Add(gchandle);
				DataContainer.Native.UpdateUInt64 updateUInt4 = new DataContainer.Native.UpdateUInt64(DataContainer.Update);
				gchandle = GCHandle.Alloc(updateUInt4, GCHandleType.Normal);
				DataContainer.gcHandles.Add(gchandle);
				DataContainer.Native.UpdateFloat updateFloat = new DataContainer.Native.UpdateFloat(DataContainer.Update);
				gchandle = GCHandle.Alloc(updateFloat, GCHandleType.Normal);
				DataContainer.gcHandles.Add(gchandle);
				DataContainer.Native.UpdateBool updateBool = new DataContainer.Native.UpdateBool(DataContainer.Update);
				gchandle = GCHandle.Alloc(updateBool, GCHandleType.Normal);
				DataContainer.gcHandles.Add(gchandle);
				DataContainer.Native.UpdateVector2 updateVector = new DataContainer.Native.UpdateVector2(DataContainer.Update);
				gchandle = GCHandle.Alloc(updateVector, GCHandleType.Normal);
				DataContainer.gcHandles.Add(gchandle);
				DataContainer.Native.UpdateVector3 updateVector2 = new DataContainer.Native.UpdateVector3(DataContainer.Update);
				gchandle = GCHandle.Alloc(updateVector2, GCHandleType.Normal);
				DataContainer.gcHandles.Add(gchandle);
				DataContainer.Native.UpdateVector4 updateVector3 = new DataContainer.Native.UpdateVector4(DataContainer.Update);
				gchandle = GCHandle.Alloc(updateVector3, GCHandleType.Normal);
				DataContainer.gcHandles.Add(gchandle);
				DataContainer.Native.UpdateQuaternion updateQuaternion = new DataContainer.Native.UpdateQuaternion(DataContainer.Update);
				gchandle = GCHandle.Alloc(updateQuaternion, GCHandleType.Normal);
				DataContainer.gcHandles.Add(gchandle);
				DataContainer.Native.UpdateBuffer updateBuffer = new DataContainer.Native.UpdateBuffer(DataContainer.Update);
				gchandle = GCHandle.Alloc(updateBuffer, GCHandleType.Normal);
				DataContainer.gcHandles.Add(gchandle);
				DataContainer.Native.Initialize(getDataFormat, updateInt, updateUInt, updateInt2, updateUInt2, updateInt3, updateUInt3, updateInt4, updateUInt4, updateFloat, updateBool, updateVector, updateVector2, updateVector3, updateQuaternion, updateBuffer);
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002904 File Offset: 0x00000B04
		internal static void Uninitialize()
		{
			List<GCHandle> list = DataContainer.gcHandles;
			lock (list)
			{
				foreach (GCHandle gchandle in DataContainer.gcHandles)
				{
					gchandle.Free();
				}
				DataContainer.gcHandles.Clear();
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002988 File Offset: 0x00000B88
		internal IntPtr Handle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002990 File Offset: 0x00000B90
		protected internal DataContainer(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("handle");
			}
			this.handle = handle;
			this.attributes = new List<BaseData>();
			this.nextIndex = 0;
			this.regLock = new object();
			this.dataFormat = string.Empty;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000029EC File Offset: 0x00000BEC
		internal byte RegisterAttribute(BaseData attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			if (this.nextIndex == 255)
			{
				throw new InvalidOperationException("The attribute cannot be registered because the 255 attributes are already registered.");
			}
			object obj = this.regLock;
			lock (obj)
			{
				byte b = this.nextIndex;
				this.nextIndex = b + 1;
				attribute.index = b;
				this.attributes.Add(attribute);
				this.dataFormat += string.Format("{0}:{1}({2})={3}&", new object[]
				{
					attribute.index,
					attribute.DataType.ToString(),
					string.Join(":", attribute.ExtraParams),
					attribute.DefaultValue
				});
			}
			return attribute.index;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002AD8 File Offset: 0x00000CD8
		[MonoPInvokeCallback(typeof(DataContainer.Native.GetDataFormat))]
		private static int GetDataFormat(IntPtr dataContainerHandle, IntPtr dataFormat, int length)
		{
			DataContainer dataContainer = GCHandle.FromIntPtr(dataContainerHandle).Target as DataContainer;
			byte[] bytes = Encoding.ASCII.GetBytes(dataContainer.dataFormat);
			if (length <= bytes.Length)
			{
				return bytes.Length + 1;
			}
			Marshal.Copy(bytes, 0, dataFormat, bytes.Length);
			Marshal.WriteByte(dataFormat, bytes.Length, 0);
			return 0;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002B2C File Offset: 0x00000D2C
		[MonoPInvokeCallback(typeof(DataContainer.Native.UpdateInt8))]
		private static void Update(IntPtr dataContainerHandle, byte index, sbyte value)
		{
			((Data<sbyte>)(GCHandle.FromIntPtr(dataContainerHandle).Target as DataContainer).attributes[(int)index]).UpdateValue(value);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002B64 File Offset: 0x00000D64
		[MonoPInvokeCallback(typeof(DataContainer.Native.UpdateUInt8))]
		private static void Update(IntPtr dataContainerHandle, byte index, byte value)
		{
			((Data<byte>)(GCHandle.FromIntPtr(dataContainerHandle).Target as DataContainer).attributes[(int)index]).UpdateValue(value);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002B9C File Offset: 0x00000D9C
		[MonoPInvokeCallback(typeof(DataContainer.Native.UpdateInt16))]
		private static void Update(IntPtr dataContainerHandle, byte index, short value)
		{
			((Data<short>)(GCHandle.FromIntPtr(dataContainerHandle).Target as DataContainer).attributes[(int)index]).UpdateValue(value);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002BD4 File Offset: 0x00000DD4
		[MonoPInvokeCallback(typeof(DataContainer.Native.UpdateUInt16))]
		private static void Update(IntPtr dataContainerHandle, byte index, ushort value)
		{
			((Data<ushort>)(GCHandle.FromIntPtr(dataContainerHandle).Target as DataContainer).attributes[(int)index]).UpdateValue(value);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002C0C File Offset: 0x00000E0C
		[MonoPInvokeCallback(typeof(DataContainer.Native.UpdateInt32))]
		private static void Update(IntPtr dataContainerHandle, byte index, int value)
		{
			((Data<int>)(GCHandle.FromIntPtr(dataContainerHandle).Target as DataContainer).attributes[(int)index]).UpdateValue(value);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002C44 File Offset: 0x00000E44
		[MonoPInvokeCallback(typeof(DataContainer.Native.UpdateUInt32))]
		private static void Update(IntPtr dataContainerHandle, byte index, uint value)
		{
			((Data<uint>)(GCHandle.FromIntPtr(dataContainerHandle).Target as DataContainer).attributes[(int)index]).UpdateValue(value);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002C7C File Offset: 0x00000E7C
		[MonoPInvokeCallback(typeof(DataContainer.Native.UpdateInt64))]
		private static void Update(IntPtr dataContainerHandle, byte index, long value)
		{
			((Data<long>)(GCHandle.FromIntPtr(dataContainerHandle).Target as DataContainer).attributes[(int)index]).UpdateValue(value);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002CB4 File Offset: 0x00000EB4
		[MonoPInvokeCallback(typeof(DataContainer.Native.UpdateUInt64))]
		private static void Update(IntPtr dataContainerHandle, byte index, ulong value)
		{
			((Data<ulong>)(GCHandle.FromIntPtr(dataContainerHandle).Target as DataContainer).attributes[(int)index]).UpdateValue(value);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002CEC File Offset: 0x00000EEC
		[MonoPInvokeCallback(typeof(DataContainer.Native.UpdateFloat))]
		private static void Update(IntPtr dataContainerHandle, byte index, float value)
		{
			((Data<float>)(GCHandle.FromIntPtr(dataContainerHandle).Target as DataContainer).attributes[(int)index]).UpdateValue(value);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002D24 File Offset: 0x00000F24
		[MonoPInvokeCallback(typeof(DataContainer.Native.UpdateBool))]
		private static void Update(IntPtr dataContainerHandle, byte index, bool value)
		{
			((Data<bool>)(GCHandle.FromIntPtr(dataContainerHandle).Target as DataContainer).attributes[(int)index]).UpdateValue(value);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002D5C File Offset: 0x00000F5C
		[MonoPInvokeCallback(typeof(DataContainer.Native.UpdateVector2))]
		private static void Update(IntPtr dataContainerHandle, byte index, float x, float y)
		{
			((Data<Vector2>)(GCHandle.FromIntPtr(dataContainerHandle).Target as DataContainer).attributes[(int)index]).UpdateValue(new Vector2(x, y));
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002D98 File Offset: 0x00000F98
		[MonoPInvokeCallback(typeof(DataContainer.Native.UpdateVector3))]
		private static void Update(IntPtr dataContainerHandle, byte index, float x, float y, float z)
		{
			((Data<Vector3>)(GCHandle.FromIntPtr(dataContainerHandle).Target as DataContainer).attributes[(int)index]).UpdateValue(new Vector3(x, y, z));
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002DD8 File Offset: 0x00000FD8
		[MonoPInvokeCallback(typeof(DataContainer.Native.UpdateVector4))]
		private static void Update(IntPtr dataContainerHandle, byte index, float x, float y, float z, float w)
		{
			BaseData baseData = (GCHandle.FromIntPtr(dataContainerHandle).Target as DataContainer).attributes[(int)index];
			if (baseData.DataType == DataType.Vector4)
			{
				((Data<Vector4>)baseData).UpdateValue(new Vector4(x, y, z, w));
				return;
			}
			if (baseData.DataType == DataType.Quaternion)
			{
				((Data<Quaternion>)baseData).UpdateValue(new Quaternion(x, y, z, w));
				return;
			}
			throw new ArgumentException("The specified value is not a quaternion or a 4D vector.", "value");
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002E58 File Offset: 0x00001058
		[MonoPInvokeCallback(typeof(DataContainer.Native.UpdateBuffer))]
		private static void Update(IntPtr dataContainerHandle, byte index, IntPtr buffer, int length)
		{
			BaseData baseData = (GCHandle.FromIntPtr(dataContainerHandle).Target as DataContainer).attributes[(int)index];
			byte[] array = new byte[length];
			if (buffer != IntPtr.Zero)
			{
				Marshal.Copy(buffer, array, 0, length);
			}
			((Data<byte[]>)baseData).UpdateValue(array);
		}

		// Token: 0x0400000A RID: 10
		private static readonly List<GCHandle> gcHandles = new List<GCHandle>();

		// Token: 0x0400000B RID: 11
		private IntPtr handle;

		// Token: 0x0400000C RID: 12
		private List<BaseData> attributes;

		// Token: 0x0400000D RID: 13
		private byte nextIndex;

		// Token: 0x0400000E RID: 14
		private object regLock;

		// Token: 0x0400000F RID: 15
		private string dataFormat;

		// Token: 0x02000010 RID: 16
		private static class Native
		{
			// Token: 0x06000058 RID: 88
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "DataContainer_Initialize")]
			public static extern void Initialize(DataContainer.Native.GetDataFormat getDataFormat, DataContainer.Native.UpdateInt8 updateInt8, DataContainer.Native.UpdateUInt8 updateUInt8, DataContainer.Native.UpdateInt16 updateInt16, DataContainer.Native.UpdateUInt16 updateUInt16, DataContainer.Native.UpdateInt32 updateInt32, DataContainer.Native.UpdateUInt32 updateUInt32, DataContainer.Native.UpdateInt64 updateInt64, DataContainer.Native.UpdateUInt64 updateUInt64, DataContainer.Native.UpdateFloat updateFloat, DataContainer.Native.UpdateBool updateBool, DataContainer.Native.UpdateVector2 updateVector2, DataContainer.Native.UpdateVector3 updateVector3, DataContainer.Native.UpdateVector4 updateVector4, DataContainer.Native.UpdateQuaternion updateQuaternion, DataContainer.Native.UpdateBuffer updateBuffer);

			// Token: 0x02000011 RID: 17
			// (Invoke) Token: 0x0600005A RID: 90
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate int GetDataFormat(IntPtr dataContainerHandle, IntPtr dataFormat, int length);

			// Token: 0x02000012 RID: 18
			// (Invoke) Token: 0x0600005E RID: 94
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void UpdateInt8(IntPtr dataContainerHandle, byte index, sbyte value);

			// Token: 0x02000013 RID: 19
			// (Invoke) Token: 0x06000062 RID: 98
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void UpdateUInt8(IntPtr dataContainerHandle, byte index, byte value);

			// Token: 0x02000014 RID: 20
			// (Invoke) Token: 0x06000066 RID: 102
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void UpdateInt16(IntPtr dataContainerHandle, byte index, short value);

			// Token: 0x02000015 RID: 21
			// (Invoke) Token: 0x0600006A RID: 106
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void UpdateUInt16(IntPtr dataContainerHandle, byte index, ushort value);

			// Token: 0x02000016 RID: 22
			// (Invoke) Token: 0x0600006E RID: 110
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void UpdateInt32(IntPtr dataContainerHandle, byte index, int value);

			// Token: 0x02000017 RID: 23
			// (Invoke) Token: 0x06000072 RID: 114
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void UpdateUInt32(IntPtr dataContainerHandle, byte index, uint value);

			// Token: 0x02000018 RID: 24
			// (Invoke) Token: 0x06000076 RID: 118
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void UpdateInt64(IntPtr dataContainerHandle, byte index, long value);

			// Token: 0x02000019 RID: 25
			// (Invoke) Token: 0x0600007A RID: 122
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void UpdateUInt64(IntPtr dataContainerHandle, byte index, ulong value);

			// Token: 0x0200001A RID: 26
			// (Invoke) Token: 0x0600007E RID: 126
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void UpdateFloat(IntPtr dataContainerHandle, byte index, float value);

			// Token: 0x0200001B RID: 27
			// (Invoke) Token: 0x06000082 RID: 130
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void UpdateBool(IntPtr dataContainerHandle, byte index, [MarshalAs(UnmanagedType.U1)] bool value);

			// Token: 0x0200001C RID: 28
			// (Invoke) Token: 0x06000086 RID: 134
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void UpdateVector2(IntPtr dataContainerHandle, byte index, float x, float y);

			// Token: 0x0200001D RID: 29
			// (Invoke) Token: 0x0600008A RID: 138
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void UpdateVector3(IntPtr dataContainerHandle, byte index, float x, float y, float z);

			// Token: 0x0200001E RID: 30
			// (Invoke) Token: 0x0600008E RID: 142
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void UpdateVector4(IntPtr dataContainerHandle, byte index, float x, float y, float z, float w);

			// Token: 0x0200001F RID: 31
			// (Invoke) Token: 0x06000092 RID: 146
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void UpdateQuaternion(IntPtr dataContainerHandle, byte index, float x, float y, float z, float w);

			// Token: 0x02000020 RID: 32
			// (Invoke) Token: 0x06000096 RID: 150
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void UpdateBuffer(IntPtr dataContainerHandle, byte index, IntPtr buffer, int length);
		}
	}
}
