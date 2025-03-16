using System;
using System.Runtime.InteropServices;

namespace StormDotNet
{
	// Token: 0x0200002A RID: 42
	public sealed class LogDeviceCallback : LogDevice
	{
		// Token: 0x060000B2 RID: 178 RVA: 0x00003298 File Offset: 0x00001498
		public static LogDeviceCallback CreateAndAcquireInstance(LogDeviceCallback.LogCallback callback, object context)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			LogDeviceCallback.ManagedCallback managedCallback = new LogDeviceCallback.ManagedCallback(callback, context);
			IntPtr intPtr = GCHandle.ToIntPtr(GCHandle.Alloc(managedCallback));
			LogDeviceCallback.Native.LogCallback logCallback = new LogDeviceCallback.Native.LogCallback(LogDeviceCallback.NativeLogCallback);
			managedCallback.nativeCallbackHandle = GCHandle.Alloc(logCallback);
			managedCallback.nativeContext = intPtr;
			IntPtr intPtr2 = LogDeviceCallback.Native.CreateAndAcquireInstance(logCallback, intPtr);
			if (intPtr2 == IntPtr.Zero)
			{
				throw new Exception("Failed to create and acquire an instance of LogDeviceCallback.");
			}
			return new LogDeviceCallback(intPtr2, managedCallback);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003314 File Offset: 0x00001514
		private LogDeviceCallback(IntPtr handle, LogDeviceCallback.ManagedCallback callback)
			: base(handle)
		{
			this.callback = callback;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003324 File Offset: 0x00001524
		public override void ReleaseInstance()
		{
			LogDeviceCallback.Native.ReleaseInstance(base.Handle);
			this.callback.nativeCallbackHandle.Free();
			base.ReleaseInstance();
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003348 File Offset: 0x00001548
		[MonoPInvokeCallback(typeof(LogDeviceCallback.Native.LogCallback))]
		private static void NativeLogCallback(uint sourceId, uint moduleId, byte level, ulong threadId, ulong systemTime, uint updateId, IntPtr text, IntPtr pContext)
		{
			Log.LogEntry logEntry = new Log.LogEntry(new StringId(sourceId), new StringId(moduleId), (Log.Level)level, threadId, systemTime, updateId, Marshal.PtrToStringAnsi(text));
			LogDeviceCallback.ManagedCallback managedCallback = (LogDeviceCallback.ManagedCallback)GCHandle.FromIntPtr(pContext).Target;
			managedCallback.callback(logEntry, managedCallback.context);
		}

		// Token: 0x04000028 RID: 40
		private LogDeviceCallback.ManagedCallback callback;

		// Token: 0x0200002B RID: 43
		// (Invoke) Token: 0x060000B7 RID: 183
		public delegate void LogCallback(Log.LogEntry logEntry, object context);

		// Token: 0x0200002C RID: 44
		private static class Native
		{
			// Token: 0x060000BA RID: 186
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "LogDeviceCallback_CreateAndAcquireInstance")]
			public static extern IntPtr CreateAndAcquireInstance(LogDeviceCallback.Native.LogCallback callback, IntPtr pContext);

			// Token: 0x060000BB RID: 187
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "LogDeviceCallback_ReleaseInstance")]
			public static extern void ReleaseInstance(IntPtr logDevice);

			// Token: 0x0200002D RID: 45
			// (Invoke) Token: 0x060000BD RID: 189
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void LogCallback(uint sourceId, uint moduleId, byte level, ulong threadId, ulong systemTime, uint updateId, IntPtr text, IntPtr pContext);
		}

		// Token: 0x0200002E RID: 46
		private struct ManagedCallback
		{
			// Token: 0x060000C0 RID: 192 RVA: 0x0000339C File Offset: 0x0000159C
			public ManagedCallback(LogDeviceCallback.LogCallback callback, object context)
			{
				this.callback = callback;
				this.context = context;
				this.nativeCallbackHandle = GCHandle.Alloc(null);
				this.nativeContext = IntPtr.Zero;
			}

			// Token: 0x04000029 RID: 41
			public LogDeviceCallback.LogCallback callback;

			// Token: 0x0400002A RID: 42
			public object context;

			// Token: 0x0400002B RID: 43
			public GCHandle nativeCallbackHandle;

			// Token: 0x0400002C RID: 44
			public IntPtr nativeContext;
		}
	}
}
