using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace StormDotNet
{
	// Token: 0x0200004A RID: 74
	public sealed class ResultRelayer : IDisposable
	{
		// Token: 0x0600012F RID: 303 RVA: 0x00004A20 File Offset: 0x00002C20
		internal static void Initialize()
		{
			List<GCHandle> list = ResultRelayer.gcHandles;
			lock (list)
			{
				ResultRelayer.Native.DestructionCallback destructionCallback = new ResultRelayer.Native.DestructionCallback(ResultRelayer.NativeDestructionCallback);
				GCHandle gchandle = GCHandle.Alloc(destructionCallback, GCHandleType.Normal);
				ResultRelayer.gcHandles.Add(gchandle);
				ResultRelayer.nativeCompletionCallback = new ResultRelayer.Native.CompletionCallback(ResultRelayer.NativeCompletionCallback);
				gchandle = GCHandle.Alloc(ResultRelayer.nativeCompletionCallback, GCHandleType.Normal);
				ResultRelayer.gcHandles.Add(gchandle);
				ResultRelayer.Native.SetDestructionCallback(destructionCallback);
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00004AA8 File Offset: 0x00002CA8
		internal static void Uninitialize()
		{
			List<GCHandle> list = ResultRelayer.gcHandles;
			lock (list)
			{
				foreach (GCHandle gchandle in ResultRelayer.gcHandles)
				{
					gchandle.Free();
				}
				ResultRelayer.gcHandles.Clear();
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00004B2C File Offset: 0x00002D2C
		internal IntPtr Handle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00004B34 File Offset: 0x00002D34
		internal ResultRelayer(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("handle");
			}
			this.handle = handle;
			this.callbacks = new Dictionary<IntPtr, ResultRelayer.ManagedCallback>();
			this.isResultAvailable = null;
			this.result = null;
			this.disposed = false;
			Dictionary<IntPtr, ResultRelayer> dictionary = ResultRelayer.resultRelayers;
			lock (dictionary)
			{
				if (ResultRelayer.resultRelayers.ContainsKey(handle))
				{
					throw new ArgumentException(string.Format("The ResultRelayer {0:X16} is already registered.", handle.ToInt64()));
				}
				ResultRelayer.resultRelayers.Add(handle, this);
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00004BEC File Offset: 0x00002DEC
		~ResultRelayer()
		{
			this.Dispose(false);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00004C1C File Offset: 0x00002E1C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00004C2C File Offset: 0x00002E2C
		private void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					foreach (KeyValuePair<IntPtr, ResultRelayer.ManagedCallback> keyValuePair in this.callbacks)
					{
						ResultRelayer.ManagedCallback value = keyValuePair.Value;
						GCHandle.FromIntPtr(keyValuePair.Key).Free();
					}
					this.callbacks.Clear();
				}
				this.handle = IntPtr.Zero;
				this.disposed = true;
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00004CBC File Offset: 0x00002EBC
		public EResult GetResult()
		{
			EResult eresult;
			using (StormEngine.SetProfilePoint(null))
			{
				if (this.disposed && (this.isResultAvailable ?? false))
				{
					eresult = this.result;
				}
				else
				{
					if (this.result == null || this.result.GetErrorCode() != (EResult.ECode)ResultRelayer.Native.GetResult(this.handle))
					{
						this.GetResultFromNative();
					}
					eresult = this.result;
				}
			}
			return eresult;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00004D48 File Offset: 0x00002F48
		private void GetResultFromNative()
		{
			using (StormEngine.SetProfilePoint(null))
			{
				int num = 0;
				uint num2 = 0U;
				if (ResultRelayer.feedStringBuffer == IntPtr.Zero)
				{
					ResultRelayer.feedStringBuffer = Marshal.AllocHGlobal(1024);
					ResultRelayer.feedStringLength = 1024;
				}
				if (ResultRelayer.debugFileBuffer == IntPtr.Zero)
				{
					ResultRelayer.debugFileBuffer = Marshal.AllocHGlobal(512);
					ResultRelayer.debugFileLength = 512;
				}
				int num3 = ResultRelayer.feedStringLength;
				int num4 = ResultRelayer.debugFileLength;
				if (!ResultRelayer.Native.GetResult(this.handle, ref num, ResultRelayer.feedStringBuffer, ref num3, ResultRelayer.debugFileBuffer, ref num4, ref num2))
				{
					Marshal.FreeHGlobal(ResultRelayer.feedStringBuffer);
					Marshal.FreeHGlobal(ResultRelayer.debugFileBuffer);
					num3 = (ResultRelayer.feedStringLength = num3 + 1);
					num4 = (ResultRelayer.debugFileLength = num4 + 1);
					ResultRelayer.feedStringBuffer = Marshal.AllocHGlobal(ResultRelayer.feedStringLength);
					ResultRelayer.debugFileBuffer = Marshal.AllocHGlobal(ResultRelayer.debugFileLength);
					ResultRelayer.Native.GetResult(this.handle, ref num, ResultRelayer.feedStringBuffer, ref num3, ResultRelayer.debugFileBuffer, ref num4, ref num2);
				}
				this.result = new EResult((EResult.ECode)num, Marshal.PtrToStringAnsi(ResultRelayer.feedStringBuffer, num3), Marshal.PtrToStringAnsi(ResultRelayer.debugFileBuffer, num4), (int)num2);
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00004E94 File Offset: 0x00003094
		public void RegisterCompletionCallback(ResultRelayer.CompletionCallback completionCallback, object context)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("this", "Cannot register a completion callback to a disposed ResultRelayer.");
			}
			ResultRelayer.ManagedCallback managedCallback = new ResultRelayer.ManagedCallback(completionCallback, this, context);
			IntPtr intPtr = GCHandle.ToIntPtr(GCHandle.Alloc(managedCallback));
			Dictionary<IntPtr, ResultRelayer.ManagedCallback> dictionary = this.callbacks;
			lock (dictionary)
			{
				this.callbacks[intPtr] = managedCallback;
			}
			ResultRelayer.Native.RegisterCompletionCallback(this.handle, ResultRelayer.nativeCompletionCallback, intPtr);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00004F20 File Offset: 0x00003120
		public void UnregisterCompletionCallbacks()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("this", "Cannot unregister a completion callback to a disposed ResultRelayer.");
			}
			ResultRelayer.Native.UnregisterCompletionCallbacks(this.handle);
			Dictionary<IntPtr, ResultRelayer.ManagedCallback> dictionary = this.callbacks;
			lock (dictionary)
			{
				foreach (IntPtr intPtr in this.callbacks.Keys)
				{
					GCHandle.FromIntPtr(intPtr).Free();
				}
				this.callbacks.Clear();
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00004FD4 File Offset: 0x000031D4
		[MonoPInvokeCallback(typeof(ResultRelayer.Native.CompletionCallback))]
		private static void NativeCompletionCallback(IntPtr pResultRelayer, IntPtr pContext)
		{
			using (StormEngine.SetProfilePoint(null))
			{
				GCHandle gchandle = GCHandle.FromIntPtr(pContext);
				ResultRelayer.ManagedCallback managedCallback = (ResultRelayer.ManagedCallback)gchandle.Target;
				ResultRelayer resultRelayer = managedCallback.resultRelayer;
				managedCallback.callback(resultRelayer, managedCallback.context);
				Dictionary<IntPtr, ResultRelayer.ManagedCallback> dictionary = resultRelayer.callbacks;
				lock (dictionary)
				{
					resultRelayer.callbacks.Remove(pContext);
				}
				gchandle.Free();
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00005074 File Offset: 0x00003274
		[MonoPInvokeCallback(typeof(ResultRelayer.Native.DestructionCallback))]
		private static void NativeDestructionCallback(IntPtr pResultRelayer)
		{
			using (StormEngine.SetProfilePoint(null))
			{
				ResultRelayer resultRelayer = null;
				Dictionary<IntPtr, ResultRelayer> dictionary = ResultRelayer.resultRelayers;
				lock (dictionary)
				{
					if (ResultRelayer.resultRelayers.TryGetValue(pResultRelayer, out resultRelayer))
					{
						ResultRelayer.resultRelayers.Remove(pResultRelayer);
					}
				}
				if (resultRelayer != null)
				{
					resultRelayer.isResultAvailable = new bool?(ResultRelayer.Native.IsResultAvailable(resultRelayer.handle));
					if ((resultRelayer.isResultAvailable ?? false) && (resultRelayer.result == null || resultRelayer.result.GetErrorCode() != (EResult.ECode)ResultRelayer.Native.GetResult(resultRelayer.handle)))
					{
						resultRelayer.GetResultFromNative();
					}
					resultRelayer.Dispose();
				}
			}
		}

		// Token: 0x0400005F RID: 95
		private static readonly Dictionary<IntPtr, ResultRelayer> resultRelayers = new Dictionary<IntPtr, ResultRelayer>();

		// Token: 0x04000060 RID: 96
		private static ResultRelayer.Native.CompletionCallback nativeCompletionCallback = null;

		// Token: 0x04000061 RID: 97
		private static readonly List<GCHandle> gcHandles = new List<GCHandle>();

		// Token: 0x04000062 RID: 98
		[ThreadStatic]
		private static IntPtr feedStringBuffer = IntPtr.Zero;

		// Token: 0x04000063 RID: 99
		[ThreadStatic]
		private static int feedStringLength = 0;

		// Token: 0x04000064 RID: 100
		[ThreadStatic]
		private static IntPtr debugFileBuffer = IntPtr.Zero;

		// Token: 0x04000065 RID: 101
		[ThreadStatic]
		private static int debugFileLength = 0;

		// Token: 0x04000066 RID: 102
		private IntPtr handle;

		// Token: 0x04000067 RID: 103
		private Dictionary<IntPtr, ResultRelayer.ManagedCallback> callbacks;

		// Token: 0x04000068 RID: 104
		private bool? isResultAvailable;

		// Token: 0x04000069 RID: 105
		private EResult result;

		// Token: 0x0400006A RID: 106
		private bool disposed;

		// Token: 0x0200004B RID: 75
		// (Invoke) Token: 0x0600013E RID: 318
		public delegate void CompletionCallback(ResultRelayer resultRelayer, object context);

		// Token: 0x0200004C RID: 76
		private static class Native
		{
			// Token: 0x06000141 RID: 321
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "ResultRelayer_SetDestructionCallback")]
			public static extern void SetDestructionCallback(ResultRelayer.Native.DestructionCallback destructionCallback);

			// Token: 0x06000142 RID: 322
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "ResultRelayer_RegisterCompletionCallback")]
			public static extern void RegisterCompletionCallback(IntPtr pResultRelayer, ResultRelayer.Native.CompletionCallback completionCallback, IntPtr pContext);

			// Token: 0x06000143 RID: 323
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "ResultRelayer_UnregisterCompletionCallbacks")]
			public static extern void UnregisterCompletionCallbacks(IntPtr pResultRelayer);

			// Token: 0x06000144 RID: 324
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "ResultRelayer_IsResultAvailable")]
			[return: MarshalAs(UnmanagedType.U1)]
			public static extern bool IsResultAvailable(IntPtr pResultRelayer);

			// Token: 0x06000145 RID: 325
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "ResultRelayer_GetResult")]
			public static extern int GetResult(IntPtr pResultRelayer);

			// Token: 0x06000146 RID: 326
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "ResultRelayer_GetFullResult")]
			[return: MarshalAs(UnmanagedType.U1)]
			public static extern bool GetResult(IntPtr pResultRelayer, ref int errorCode, IntPtr feedString, ref int feedStringLength, IntPtr debugFile, ref int debugFileLength, ref uint line);

			// Token: 0x0200004D RID: 77
			// (Invoke) Token: 0x06000148 RID: 328
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void CompletionCallback(IntPtr pResultRelayer, IntPtr pContext);

			// Token: 0x0200004E RID: 78
			// (Invoke) Token: 0x0600014C RID: 332
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void DestructionCallback(IntPtr pResultRelayer);
		}

		// Token: 0x0200004F RID: 79
		private struct ManagedCallback
		{
			// Token: 0x0600014F RID: 335 RVA: 0x00005188 File Offset: 0x00003388
			public ManagedCallback(ResultRelayer.CompletionCallback completionCallback, ResultRelayer resultRelayer, object context)
			{
				this.callback = completionCallback;
				this.resultRelayer = resultRelayer;
				this.context = context;
			}

			// Token: 0x0400006B RID: 107
			public ResultRelayer.CompletionCallback callback;

			// Token: 0x0400006C RID: 108
			public ResultRelayer resultRelayer;

			// Token: 0x0400006D RID: 109
			public object context;
		}
	}
}
