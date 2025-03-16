using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace StormDotNet.Echo
{
	// Token: 0x020000B6 RID: 182
	public sealed class SessionDescriptor : IDisposable
	{
		// Token: 0x0600038D RID: 909 RVA: 0x0000B4EC File Offset: 0x000096EC
		internal static void Initialize()
		{
			List<GCHandle> list = SessionDescriptor.gcHandles;
			lock (list)
			{
				SessionDescriptor.Native.CreateSessionDescriptor createSessionDescriptor = new SessionDescriptor.Native.CreateSessionDescriptor(SessionDescriptor.CreateSessionDescriptor);
				GCHandle gchandle = GCHandle.Alloc(createSessionDescriptor, GCHandleType.Normal);
				SessionDescriptor.gcHandles.Add(gchandle);
				SessionDescriptor.Native.ReleaseSessionDescriptor releaseSessionDescriptor = new SessionDescriptor.Native.ReleaseSessionDescriptor(SessionDescriptor.ReleaseSessionDescriptor);
				gchandle = GCHandle.Alloc(releaseSessionDescriptor, GCHandleType.Normal);
				SessionDescriptor.gcHandles.Add(gchandle);
				SessionDescriptor.Native.Initialize(createSessionDescriptor, releaseSessionDescriptor);
			}
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000B56C File Offset: 0x0000976C
		internal static void Uninitialize()
		{
			List<GCHandle> list = SessionDescriptor.gcHandles;
			lock (list)
			{
				foreach (GCHandle gchandle in SessionDescriptor.gcHandles)
				{
					gchandle.Free();
				}
				SessionDescriptor.gcHandles.Clear();
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0000B5F0 File Offset: 0x000097F0
		internal IntPtr Handle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x1700004F RID: 79
		// (set) Token: 0x06000390 RID: 912 RVA: 0x0000B5F8 File Offset: 0x000097F8
		public string SessionName
		{
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				if (!this.isManaged)
				{
					throw new InvalidOperationException();
				}
				if (value.Length > 128)
				{
					throw new ArgumentException(string.Format("The session name cannot be longer than {0} characters.", 128));
				}
				SessionDescriptor.Native.SetSessionName(this.handle, value);
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000391 RID: 913 RVA: 0x0000B65F File Offset: 0x0000985F
		// (set) Token: 0x06000392 RID: 914 RVA: 0x0000B68C File Offset: 0x0000988C
		public StringId SessionTypeId
		{
			get
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				return new StringId(SessionDescriptor.Native.GetSessionType(this.Handle));
			}
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				if (!value.IsValid)
				{
					throw new ArgumentOutOfRangeException("value", "The session type is not a valid StringId.");
				}
				SessionDescriptor.Native.SetSessionType(this.Handle, value.UniqueID);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000393 RID: 915 RVA: 0x0000B6DC File Offset: 0x000098DC
		// (set) Token: 0x06000394 RID: 916 RVA: 0x0000B72C File Offset: 0x0000992C
		public unsafe GUID SessionGUID
		{
			get
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				IntPtr zero = IntPtr.Zero;
				byte b = 0;
				SessionDescriptor.Native.GetSessionGUID(this.handle, ref zero, ref b);
				if (b > 0)
				{
					return new GUID(zero, b);
				}
				return GUID.INVALID_GUID;
			}
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				if (!this.isManaged)
				{
					throw new InvalidOperationException();
				}
				byte[] array;
				byte* ptr;
				if ((array = value.Data) == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				SessionDescriptor.Native.SetSessionGUID(this.handle, new IntPtr((void*)ptr), value.Length);
				array = null;
			}
		}

		// Token: 0x17000052 RID: 82
		// (set) Token: 0x06000395 RID: 917 RVA: 0x0000B796 File Offset: 0x00009996
		public bool AdvertiseOnLAN
		{
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				SessionDescriptor.Native.SetAdvertiseOnLAN(this.handle, value);
			}
		}

		// Token: 0x17000053 RID: 83
		// (set) Token: 0x06000396 RID: 918 RVA: 0x0000B7BD File Offset: 0x000099BD
		public bool AdvertiseOnline
		{
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				SessionDescriptor.Native.SetAdvertiseOnline(this.handle, value);
			}
		}

		// Token: 0x17000054 RID: 84
		// (set) Token: 0x06000397 RID: 919 RVA: 0x0000B7E4 File Offset: 0x000099E4
		public bool EnableHostMigration
		{
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				SessionDescriptor.Native.SetEnableHostMigration(this.handle, value);
			}
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000B80B File Offset: 0x00009A0B
		public SessionDescriptor(SessionType sessionType = SessionType.Game)
			: this(sessionType.ToTypeId())
		{
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000B81C File Offset: 0x00009A1C
		public SessionDescriptor(StringId sessionType)
		{
			if (!sessionType.IsValid)
			{
				throw new ArgumentException("The session type cannot be invalid.", "sessionType");
			}
			GCHandle gchandle = GCHandle.Alloc(this, GCHandleType.Weak);
			this.handle = SessionDescriptor.Native.Create(GCHandle.ToIntPtr(gchandle));
			this.gcHandle = gchandle;
			this.isManaged = true;
			this.SessionTypeId = sessionType;
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000B875 File Offset: 0x00009A75
		private SessionDescriptor(IntPtr handle)
		{
			this.handle = handle;
			this.isManaged = false;
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000B88C File Offset: 0x00009A8C
		~SessionDescriptor()
		{
			this.Dispose(false);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000B8BC File Offset: 0x00009ABC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000B8CC File Offset: 0x00009ACC
		private void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (this.isManaged && this.handle != IntPtr.Zero)
				{
					SessionDescriptor.Native.Destroy(this.handle);
					this.gcHandle.Free();
				}
				this.disposed = true;
			}
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000B91C File Offset: 0x00009B1C
		[MonoPInvokeCallback(typeof(SessionDescriptor.Native.CreateSessionDescriptor))]
		private static IntPtr CreateSessionDescriptor(IntPtr sessionDescriptor)
		{
			if (sessionDescriptor == IntPtr.Zero)
			{
				throw new ArgumentNullException("sessionDescriptor");
			}
			SessionDescriptor sessionDescriptor2 = new SessionDescriptor(sessionDescriptor);
			GCHandle gchandle = GCHandle.Alloc(sessionDescriptor2, GCHandleType.Normal);
			sessionDescriptor2.gcHandle = gchandle;
			return GCHandle.ToIntPtr(gchandle);
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000B95C File Offset: 0x00009B5C
		[MonoPInvokeCallback(typeof(SessionDescriptor.Native.ReleaseSessionDescriptor))]
		private static void ReleaseSessionDescriptor(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("handle");
			}
			GCHandle gchandle = GCHandle.FromIntPtr(handle);
			SessionDescriptor sessionDescriptor = gchandle.Target as SessionDescriptor;
			if (sessionDescriptor == null)
			{
				throw new ArgumentException("The handle does not point to a valid session descriptor.", "handle");
			}
			sessionDescriptor.Dispose();
			gchandle.Free();
		}

		// Token: 0x04000272 RID: 626
		private static readonly List<GCHandle> gcHandles = new List<GCHandle>();

		// Token: 0x04000273 RID: 627
		private IntPtr handle;

		// Token: 0x04000274 RID: 628
		private GCHandle gcHandle;

		// Token: 0x04000275 RID: 629
		private bool isManaged;

		// Token: 0x04000276 RID: 630
		private bool disposed;

		// Token: 0x020000B7 RID: 183
		private static class Native
		{
			// Token: 0x060003A1 RID: 929
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "SessionDescriptor_Initialize")]
			public static extern void Initialize(SessionDescriptor.Native.CreateSessionDescriptor createSessionDescriptor, SessionDescriptor.Native.ReleaseSessionDescriptor releaseSessionDescriptor);

			// Token: 0x060003A2 RID: 930
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "SessionDescriptor_Create")]
			public static extern IntPtr Create(IntPtr handle);

			// Token: 0x060003A3 RID: 931
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "SessionDescriptor_Destroy")]
			public static extern void Destroy(IntPtr sessionDescriptor);

			// Token: 0x060003A4 RID: 932
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "SessionDescriptor_SetSessionName")]
			public static extern void SetSessionName(IntPtr sessionDescriptor, [MarshalAs(UnmanagedType.LPStr)] string name);

			// Token: 0x060003A5 RID: 933
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "SessionDescriptor_GetSessionType")]
			public static extern uint GetSessionType(IntPtr sessionDescriptor);

			// Token: 0x060003A6 RID: 934
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "SessionDescriptor_SetSessionType")]
			public static extern uint SetSessionType(IntPtr sessionDescriptor, uint sessionType);

			// Token: 0x060003A7 RID: 935
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "SessionDescriptor_GetSessionGUID")]
			public static extern void GetSessionGUID(IntPtr sessionDescriptor, ref IntPtr buffer, ref byte bufferLength);

			// Token: 0x060003A8 RID: 936
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "SessionDescriptor_SetSessionGUID")]
			public static extern void SetSessionGUID(IntPtr sessionDescriptor, IntPtr buffer, byte bufferLength);

			// Token: 0x060003A9 RID: 937
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "SessionDescriptor_SetAdvertiseOnLAN")]
			public static extern void SetAdvertiseOnLAN(IntPtr sessionDescriptor, [MarshalAs(UnmanagedType.U1)] bool advertise);

			// Token: 0x060003AA RID: 938
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "SessionDescriptor_SetAdvertiseOnline")]
			public static extern void SetAdvertiseOnline(IntPtr sessionDescriptor, [MarshalAs(UnmanagedType.U1)] bool advertise);

			// Token: 0x060003AB RID: 939
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "SessionDescriptor_SetEnableHostMigration")]
			public static extern void SetEnableHostMigration(IntPtr sessionDescriptor, [MarshalAs(UnmanagedType.U1)] bool enableHostMigration);

			// Token: 0x020000B8 RID: 184
			// (Invoke) Token: 0x060003AD RID: 941
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate IntPtr CreateSessionDescriptor(IntPtr sessionDescriptor);

			// Token: 0x020000B9 RID: 185
			// (Invoke) Token: 0x060003B1 RID: 945
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void ReleaseSessionDescriptor(IntPtr handle);
		}
	}
}
