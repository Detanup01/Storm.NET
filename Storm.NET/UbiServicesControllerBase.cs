using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace StormDotNet
{
	// Token: 0x0200006F RID: 111
	public class UbiServicesControllerBase : IDisposable
	{
		// Token: 0x060001FE RID: 510 RVA: 0x00006E50 File Offset: 0x00005050
		internal static void Initialize()
		{
			List<GCHandle> list = UbiServicesControllerBase.gcHandles;
			lock (list)
			{
				UbiServicesControllerBase.Native.EnableFct enableFct = new UbiServicesControllerBase.Native.EnableFct(UbiServicesControllerBase.Enable);
				GCHandle gchandle = GCHandle.Alloc(enableFct, GCHandleType.Normal);
				UbiServicesControllerBase.gcHandles.Add(gchandle);
				UbiServicesControllerBase.Native.DisableFct disableFct = new UbiServicesControllerBase.Native.DisableFct(UbiServicesControllerBase.Disable);
				gchandle = GCHandle.Alloc(disableFct, GCHandleType.Normal);
				UbiServicesControllerBase.gcHandles.Add(gchandle);
				UbiServicesControllerBase.Native.UpdateFct updateFct = new UbiServicesControllerBase.Native.UpdateFct(UbiServicesControllerBase.Update);
				gchandle = GCHandle.Alloc(updateFct, GCHandleType.Normal);
				UbiServicesControllerBase.gcHandles.Add(gchandle);
				UbiServicesControllerBase.Native.ReloginFct reloginFct = new UbiServicesControllerBase.Native.ReloginFct(UbiServicesControllerBase.Relogin);
				gchandle = GCHandle.Alloc(reloginFct, GCHandleType.Normal);
				UbiServicesControllerBase.gcHandles.Add(gchandle);
				UbiServicesControllerBase.Native.GetStormUrlFct getStormUrlFct = new UbiServicesControllerBase.Native.GetStormUrlFct(UbiServicesControllerBase.GetStormUrl);
				gchandle = GCHandle.Alloc(getStormUrlFct, GCHandleType.Normal);
				UbiServicesControllerBase.gcHandles.Add(gchandle);
				UbiServicesControllerBase.Native.GetApplicationIdFct getApplicationIdFct = new UbiServicesControllerBase.Native.GetApplicationIdFct(UbiServicesControllerBase.GetApplicationId);
				gchandle = GCHandle.Alloc(getApplicationIdFct, GCHandleType.Normal);
				UbiServicesControllerBase.gcHandles.Add(gchandle);
				UbiServicesControllerBase.Native.GetCustomFeatureSwitchFct getCustomFeatureSwitchFct = new UbiServicesControllerBase.Native.GetCustomFeatureSwitchFct(UbiServicesControllerBase.GetCustomFeatureSwitch);
				gchandle = GCHandle.Alloc(getCustomFeatureSwitchFct, GCHandleType.Normal);
				UbiServicesControllerBase.gcHandles.Add(gchandle);
				UbiServicesControllerBase.Native.PushCustomEventFct pushCustomEventFct = new UbiServicesControllerBase.Native.PushCustomEventFct(UbiServicesControllerBase.PushCustomEvent);
				gchandle = GCHandle.Alloc(pushCustomEventFct, GCHandleType.Normal);
				UbiServicesControllerBase.gcHandles.Add(gchandle);
				UbiServicesControllerBase.Native.PushContextStartEventFct pushContextStartEventFct = new UbiServicesControllerBase.Native.PushContextStartEventFct(UbiServicesControllerBase.PushContextStartEvent);
				gchandle = GCHandle.Alloc(pushContextStartEventFct, GCHandleType.Normal);
				UbiServicesControllerBase.gcHandles.Add(gchandle);
				UbiServicesControllerBase.Native.PushContextStopEventFct pushContextStopEventFct = new UbiServicesControllerBase.Native.PushContextStopEventFct(UbiServicesControllerBase.PushContextStopEvent);
				gchandle = GCHandle.Alloc(pushContextStopEventFct, GCHandleType.Normal);
				UbiServicesControllerBase.gcHandles.Add(gchandle);
				UbiServicesControllerBase.Native.IsJsonValidFct isJsonValidFct = new UbiServicesControllerBase.Native.IsJsonValidFct(UbiServicesControllerBase.IsJsonValid);
				gchandle = GCHandle.Alloc(isJsonValidFct, GCHandleType.Normal);
				UbiServicesControllerBase.gcHandles.Add(gchandle);
				UbiServicesControllerBase.Native.SendHttpGetRequestFct sendHttpGetRequestFct = new UbiServicesControllerBase.Native.SendHttpGetRequestFct(UbiServicesControllerBase.SendHttpGetRequest);
				gchandle = GCHandle.Alloc(sendHttpGetRequestFct, GCHandleType.Normal);
				UbiServicesControllerBase.gcHandles.Add(gchandle);
				UbiServicesControllerBase.Native.SendHttpPostRequestFct sendHttpPostRequestFct = new UbiServicesControllerBase.Native.SendHttpPostRequestFct(UbiServicesControllerBase.SendHttpPostRequest);
				gchandle = GCHandle.Alloc(sendHttpPostRequestFct, GCHandleType.Normal);
				UbiServicesControllerBase.gcHandles.Add(gchandle);
				UbiServicesControllerBase.Native.GetValueInJsonFct getValueInJsonFct = new UbiServicesControllerBase.Native.GetValueInJsonFct(UbiServicesControllerBase.GetValueInJson);
				gchandle = GCHandle.Alloc(getValueInJsonFct, GCHandleType.Normal);
				UbiServicesControllerBase.gcHandles.Add(gchandle);
				UbiServicesControllerBase.Native.GetTitleSpaceIdFct getTitleSpaceIdFct = new UbiServicesControllerBase.Native.GetTitleSpaceIdFct(UbiServicesControllerBase.GetTitleSpaceId);
				gchandle = GCHandle.Alloc(getTitleSpaceIdFct, GCHandleType.Normal);
				UbiServicesControllerBase.gcHandles.Add(gchandle);
				UbiServicesControllerBase.Native.GetUrlPrefixForConnectedEnvironmentFct getUrlPrefixForConnectedEnvironmentFct = new UbiServicesControllerBase.Native.GetUrlPrefixForConnectedEnvironmentFct(UbiServicesControllerBase.GetUrlPrefixForConnectedEnvironment);
				gchandle = GCHandle.Alloc(getUrlPrefixForConnectedEnvironmentFct, GCHandleType.Normal);
				UbiServicesControllerBase.gcHandles.Add(gchandle);
				UbiServicesControllerBase.Native.GetNativeFacadeFct getNativeFacadeFct = new UbiServicesControllerBase.Native.GetNativeFacadeFct(UbiServicesControllerBase.GetNativeFacade);
				gchandle = GCHandle.Alloc(getNativeFacadeFct, GCHandleType.Normal);
				UbiServicesControllerBase.gcHandles.Add(gchandle);
				UbiServicesControllerBase.Native.GetSessionTicketFct getSessionTicketFct = new UbiServicesControllerBase.Native.GetSessionTicketFct(UbiServicesControllerBase.GetSessionTicket);
				gchandle = GCHandle.Alloc(getSessionTicketFct, GCHandleType.Normal);
				UbiServicesControllerBase.gcHandles.Add(gchandle);
				UbiServicesControllerBase.Native.GetSessionIdFct getSessionIdFct = new UbiServicesControllerBase.Native.GetSessionIdFct(UbiServicesControllerBase.GetSessionId);
				gchandle = GCHandle.Alloc(getSessionIdFct, GCHandleType.Normal);
				UbiServicesControllerBase.gcHandles.Add(gchandle);
				UbiServicesControllerBase.Native.IsLoggedInFct isLoggedInFct = new UbiServicesControllerBase.Native.IsLoggedInFct(UbiServicesControllerBase.IsLoggedIn);
				gchandle = GCHandle.Alloc(isLoggedInFct, GCHandleType.Normal);
				UbiServicesControllerBase.gcHandles.Add(gchandle);
				UbiServicesControllerBase.Native.GetUpstreamHttpDataCounterFct getUpstreamHttpDataCounterFct = new UbiServicesControllerBase.Native.GetUpstreamHttpDataCounterFct(UbiServicesControllerBase.GetUpstreamHttpDataCounter);
				gchandle = GCHandle.Alloc(getUpstreamHttpDataCounterFct, GCHandleType.Normal);
				UbiServicesControllerBase.gcHandles.Add(gchandle);
				UbiServicesControllerBase.Native.GetUpstreamStreamHttpHttpDataCounterFct getUpstreamStreamHttpHttpDataCounterFct = new UbiServicesControllerBase.Native.GetUpstreamStreamHttpHttpDataCounterFct(UbiServicesControllerBase.GetUpstreamStreamHttpHttpDataCounter);
				gchandle = GCHandle.Alloc(getUpstreamStreamHttpHttpDataCounterFct, GCHandleType.Normal);
				UbiServicesControllerBase.gcHandles.Add(gchandle);
				UbiServicesControllerBase.Native.Initialize(enableFct, disableFct, updateFct, reloginFct, getStormUrlFct, getApplicationIdFct, getCustomFeatureSwitchFct, pushCustomEventFct, pushContextStartEventFct, pushContextStopEventFct, isJsonValidFct, sendHttpGetRequestFct, sendHttpPostRequestFct, getValueInJsonFct, getTitleSpaceIdFct, getUrlPrefixForConnectedEnvironmentFct, getNativeFacadeFct, getSessionTicketFct, getSessionIdFct, isLoggedInFct, getUpstreamHttpDataCounterFct, getUpstreamStreamHttpHttpDataCounterFct);
			}
		}

		// Token: 0x060001FF RID: 511 RVA: 0x000071AC File Offset: 0x000053AC
		internal static void Uninitialize()
		{
			List<GCHandle> list = UbiServicesControllerBase.gcHandles;
			lock (list)
			{
				foreach (GCHandle gchandle in UbiServicesControllerBase.gcHandles)
				{
					gchandle.Free();
				}
				UbiServicesControllerBase.gcHandles.Clear();
			}
			UbiServicesControllerBase.Native.Uninitialize();
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000200 RID: 512 RVA: 0x00007238 File Offset: 0x00005438
		// (remove) Token: 0x06000201 RID: 513 RVA: 0x00007270 File Offset: 0x00005470
		public event EventHandler<UbiServicesControllerBase.FacadeResetEventArgs> FacadeReset;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000202 RID: 514 RVA: 0x000072A8 File Offset: 0x000054A8
		// (remove) Token: 0x06000203 RID: 515 RVA: 0x000072E0 File Offset: 0x000054E0
		public event EventHandler<UbiServicesControllerBase.ServerConnectedEventArgs> ServerConnected;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000204 RID: 516 RVA: 0x00007318 File Offset: 0x00005518
		// (remove) Token: 0x06000205 RID: 517 RVA: 0x00007350 File Offset: 0x00005550
		public event EventHandler<UbiServicesControllerBase.ServerDisconnectedEventArgs> ServerDisconnected;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000206 RID: 518 RVA: 0x00007388 File Offset: 0x00005588
		// (remove) Token: 0x06000207 RID: 519 RVA: 0x000073C0 File Offset: 0x000055C0
		public event EventHandler<UbiServicesControllerBase.S2STicketSuccessEventArgs> S2STicketSuccess;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000208 RID: 520 RVA: 0x000073F8 File Offset: 0x000055F8
		// (remove) Token: 0x06000209 RID: 521 RVA: 0x00007430 File Offset: 0x00005630
		public event EventHandler<UbiServicesControllerBase.S2STicketFailureEventArgs> S2STicketFailure;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600020A RID: 522 RVA: 0x00007468 File Offset: 0x00005668
		// (remove) Token: 0x0600020B RID: 523 RVA: 0x000074A0 File Offset: 0x000056A0
		public event EventHandler<UbiServicesControllerBase.S2SConfigUrlSuccessEventArgs> S2SConfigUrlSuccess;

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600020C RID: 524 RVA: 0x000074D5 File Offset: 0x000056D5
		public virtual string ApplicationId
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600020D RID: 525 RVA: 0x000074D5 File Offset: 0x000056D5
		public virtual string TitleSpaceId
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600020E RID: 526 RVA: 0x000074D5 File Offset: 0x000056D5
		public virtual string UrlPrefixForConnectedEnvironment
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600020F RID: 527 RVA: 0x000074D5 File Offset: 0x000056D5
		public virtual IntPtr NativeFacade
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000210 RID: 528 RVA: 0x000074D5 File Offset: 0x000056D5
		public virtual object Facade
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000211 RID: 529 RVA: 0x000074DC File Offset: 0x000056DC
		// (set) Token: 0x06000212 RID: 530 RVA: 0x000074E4 File Offset: 0x000056E4
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			protected set
			{
				this.enabled = value;
				UbiServicesControllerBase.Native.SetEnabled(this.Handle, this.enabled ? 1 : 0);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000213 RID: 531 RVA: 0x000074D5 File Offset: 0x000056D5
		public virtual string SessionTicket
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000214 RID: 532 RVA: 0x000074D5 File Offset: 0x000056D5
		public virtual string SessionId
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000215 RID: 533 RVA: 0x000074D5 File Offset: 0x000056D5
		public virtual bool IsLogged
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000216 RID: 534 RVA: 0x000074D5 File Offset: 0x000056D5
		public virtual uint UpstreamHttpDataCounter
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000217 RID: 535 RVA: 0x000074D5 File Offset: 0x000056D5
		public virtual uint UpstreamStreamHttpHttpDataCounter
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00007504 File Offset: 0x00005704
		internal IntPtr Handle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000750C File Offset: 0x0000570C
		public UbiServicesControllerBase()
		{
			this.gcHandle = GCHandle.Alloc(this, GCHandleType.Normal);
			this.handle = UbiServicesControllerBase.Native.Create(GCHandle.ToIntPtr(this.gcHandle));
			this.FacadeReset += this.ForwardFacadeReset;
			this.ServerConnected += this.ForwardServerConnected;
			this.ServerDisconnected += this.ForwardServerDisconnected;
			this.S2STicketSuccess += this.ForwardS2STicketSuccess;
			this.S2STicketFailure += this.ForwardS2STicketFailure;
			this.S2SConfigUrlSuccess += this.ForwardS2SConfigUrlSuccess;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x000075B0 File Offset: 0x000057B0
		~UbiServicesControllerBase()
		{
			this.Dispose(false);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x000075E0 File Offset: 0x000057E0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x000075EF File Offset: 0x000057EF
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				UbiServicesControllerBase.Native.Destroy(this.handle);
				this.handle = IntPtr.Zero;
				this.gcHandle.Free();
				this.disposed = true;
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x000074D5 File Offset: 0x000056D5
		public virtual EResult Enable()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600021E RID: 542 RVA: 0x000074D5 File Offset: 0x000056D5
		public virtual EResult Disable()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00002240 File Offset: 0x00000440
		public virtual void Update()
		{
		}

		// Token: 0x06000220 RID: 544 RVA: 0x000074D5 File Offset: 0x000056D5
		public virtual bool Relogin()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000221 RID: 545 RVA: 0x000074D5 File Offset: 0x000056D5
		public virtual string GetStormUrl(string urlKeyName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000222 RID: 546 RVA: 0x000074D5 File Offset: 0x000056D5
		public virtual bool GetCustomFeatureSwitch(string switchName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000223 RID: 547 RVA: 0x000074D5 File Offset: 0x000056D5
		public virtual bool PushCustomEvent(string eventName, string eventJsonToWrite)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000224 RID: 548 RVA: 0x000074D5 File Offset: 0x000056D5
		public virtual bool PushContextStartEvent(string eventType, string eventName, string eventJsonToWrite)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000225 RID: 549 RVA: 0x000074D5 File Offset: 0x000056D5
		public virtual bool PushContextStopEvent(string eventType, string eventName, string eventJsonToWrite)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000226 RID: 550 RVA: 0x000074D5 File Offset: 0x000056D5
		public virtual bool IsJsonValid(string json)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000227 RID: 551 RVA: 0x000074D5 File Offset: 0x000056D5
		public virtual string GetValueInJson(string json, string key)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000228 RID: 552 RVA: 0x000074D5 File Offset: 0x000056D5
		public virtual void SendHttpGetRequest(string url, Dictionary<string, string> httpHeaders, UbiServicesControllerBase.HttpRequestResponse whenDone, bool useDefaultRetryConfig = true, uint retryCount = 0U)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000229 RID: 553 RVA: 0x000074D5 File Offset: 0x000056D5
		public virtual void SendHttpPostRequest(string url, Dictionary<string, string> httpHeaders, string body, UbiServicesControllerBase.HttpRequestResponse whenDone, bool useDefaultRetryConfig = true, uint retryCount = 0U)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600022A RID: 554 RVA: 0x000074D5 File Offset: 0x000056D5
		public virtual void ResetFacade(object newFacade)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00007623 File Offset: 0x00005823
		protected void OnFacadeReset(object sender, UbiServicesControllerBase.FacadeResetEventArgs e)
		{
			EventHandler<UbiServicesControllerBase.FacadeResetEventArgs> facadeReset = this.FacadeReset;
			if (facadeReset == null)
			{
				return;
			}
			facadeReset(sender, e);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00007637 File Offset: 0x00005837
		protected void OnServerConnected(object sender, UbiServicesControllerBase.ServerConnectedEventArgs e)
		{
			EventHandler<UbiServicesControllerBase.ServerConnectedEventArgs> serverConnected = this.ServerConnected;
			if (serverConnected == null)
			{
				return;
			}
			serverConnected(sender, e);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000764B File Offset: 0x0000584B
		protected void OnServerDisconnected(object sender, UbiServicesControllerBase.ServerDisconnectedEventArgs e)
		{
			EventHandler<UbiServicesControllerBase.ServerDisconnectedEventArgs> serverDisconnected = this.ServerDisconnected;
			if (serverDisconnected == null)
			{
				return;
			}
			serverDisconnected(sender, e);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000765F File Offset: 0x0000585F
		private void ForwardFacadeReset(object sender, UbiServicesControllerBase.FacadeResetEventArgs e)
		{
			UbiServicesControllerBase.Native.OnFacadeReset(((UbiServicesControllerBase)sender).Handle);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00007671 File Offset: 0x00005871
		private void ForwardServerConnected(object sender, UbiServicesControllerBase.ServerConnectedEventArgs e)
		{
			UbiServicesControllerBase.Native.OnServerConnected(((UbiServicesControllerBase)sender).Handle, (int)e.Result.GetErrorCode(), e.Result.GetErrorFeedString());
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00007699 File Offset: 0x00005899
		private void ForwardServerDisconnected(object sender, UbiServicesControllerBase.ServerDisconnectedEventArgs e)
		{
			UbiServicesControllerBase.Native.OnServerDisconnected(((UbiServicesControllerBase)sender).Handle, (int)e.Result.GetErrorCode(), e.Result.GetErrorFeedString());
		}

		// Token: 0x06000231 RID: 561 RVA: 0x000076C1 File Offset: 0x000058C1
		private void ForwardS2STicketSuccess(object sender, UbiServicesControllerBase.S2STicketSuccessEventArgs e)
		{
			UbiServicesControllerBase.Native.OnS2STicketSuccess(((UbiServicesControllerBase)sender).Handle, e.S2STicket, (int)e.Result.GetErrorCode(), e.Result.GetErrorFeedString());
		}

		// Token: 0x06000232 RID: 562 RVA: 0x000076EF File Offset: 0x000058EF
		private void ForwardS2STicketFailure(object sender, UbiServicesControllerBase.S2STicketFailureEventArgs e)
		{
			UbiServicesControllerBase.Native.OnS2STicketFailure(((UbiServicesControllerBase)sender).Handle, e.StatusCode, (int)e.Result.GetErrorCode(), e.Result.GetErrorFeedString());
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000771D File Offset: 0x0000591D
		private void ForwardS2SConfigUrlSuccess(object sender, UbiServicesControllerBase.S2SConfigUrlSuccessEventArgs e)
		{
			UbiServicesControllerBase.Native.OnS2SConfigUrlSuccess(((UbiServicesControllerBase)sender).Handle, e.S2SConfigUrl, (int)e.Result.GetErrorCode(), e.Result.GetErrorFeedString());
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000774C File Offset: 0x0000594C
		private static UbiServicesControllerBase GetFromHandle(IntPtr controllerHandle)
		{
			if (controllerHandle == IntPtr.Zero)
			{
				throw new ArgumentNullException("controllerHandle");
			}
			UbiServicesControllerBase ubiServicesControllerBase = GCHandle.FromIntPtr(controllerHandle).Target as UbiServicesControllerBase;
			if (ubiServicesControllerBase == null)
			{
				throw new ArgumentException("The controller is not of a base type UbiServicesControllerBase.");
			}
			return ubiServicesControllerBase;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00007794 File Offset: 0x00005994
		[MonoPInvokeCallback(typeof(UbiServicesControllerBase.Native.EnableFct))]
		private static void Enable(IntPtr controllerHandle, ref int errorCode, IntPtr errorMsg, uint errorMsgLen)
		{
			using (StormEngine.SetProfilePoint(null))
			{
				if (errorMsg == IntPtr.Zero)
				{
					throw new ArgumentNullException("errorMsg");
				}
				EResult eresult = UbiServicesControllerBase.GetFromHandle(controllerHandle).Enable();
				byte[] bytes = Encoding.UTF8.GetBytes(eresult.GetErrorFeedString());
				int num = Math.Min(bytes.Length, (int)(errorMsgLen - 1U));
				Marshal.Copy(bytes, 0, errorMsg, num);
				Marshal.WriteByte(errorMsg, num, 0);
				errorCode = (int)eresult.GetErrorCode();
			}
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000781C File Offset: 0x00005A1C
		[MonoPInvokeCallback(typeof(UbiServicesControllerBase.Native.DisableFct))]
		private static void Disable(IntPtr controllerHandle, ref int errorCode, IntPtr errorMsg, uint errorMsgLen)
		{
			using (StormEngine.SetProfilePoint(null))
			{
				if (errorMsg == IntPtr.Zero)
				{
					throw new ArgumentNullException("errorMsg");
				}
				EResult eresult = UbiServicesControllerBase.GetFromHandle(controllerHandle).Disable();
				byte[] bytes = Encoding.UTF8.GetBytes(eresult.GetErrorFeedString());
				int num = Math.Min(bytes.Length, (int)(errorMsgLen - 1U));
				Marshal.Copy(bytes, 0, errorMsg, num);
				Marshal.WriteByte(errorMsg, num, 0);
				errorCode = (int)eresult.GetErrorCode();
			}
		}

		// Token: 0x06000237 RID: 567 RVA: 0x000078A4 File Offset: 0x00005AA4
		[MonoPInvokeCallback(typeof(UbiServicesControllerBase.Native.UpdateFct))]
		private static void Update(IntPtr controllerHandle)
		{
			UbiServicesControllerBase.GetFromHandle(controllerHandle).Update();
		}

		// Token: 0x06000238 RID: 568 RVA: 0x000078B4 File Offset: 0x00005AB4
		[MonoPInvokeCallback(typeof(UbiServicesControllerBase.Native.ReloginFct))]
		private static bool Relogin(IntPtr controllerHandle)
		{
			bool flag;
			using (StormEngine.SetProfilePoint(null))
			{
				flag = UbiServicesControllerBase.GetFromHandle(controllerHandle).Relogin();
			}
			return flag;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x000078F4 File Offset: 0x00005AF4
		[MonoPInvokeCallback(typeof(UbiServicesControllerBase.Native.GetStormUrlFct))]
		private static bool GetStormUrl(IntPtr controllerHandle, string urlKeyName, IntPtr stormUrl, ref uint stormUrlLen)
		{
			bool flag2;
			using (StormEngine.SetProfilePoint(null))
			{
				if (urlKeyName == null)
				{
					throw new ArgumentNullException("urlKeyName");
				}
				if (stormUrl == IntPtr.Zero)
				{
					throw new ArgumentNullException("stormUrl");
				}
				UbiServicesControllerBase fromHandle = UbiServicesControllerBase.GetFromHandle(controllerHandle);
				byte[] bytes = Encoding.UTF8.GetBytes(fromHandle.GetStormUrl(urlKeyName));
				bool flag = false;
				if ((ulong)stormUrlLen >= (ulong)((long)bytes.Length))
				{
					Marshal.Copy(bytes, 0, stormUrl, bytes.Length);
					flag = true;
				}
				stormUrlLen = (uint)bytes.Length;
				flag2 = flag;
			}
			return flag2;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00007988 File Offset: 0x00005B88
		[MonoPInvokeCallback(typeof(UbiServicesControllerBase.Native.GetApplicationIdFct))]
		private static bool GetApplicationId(IntPtr controllerHandle, IntPtr applicationId, ref uint applicationIdLen)
		{
			bool flag2;
			using (StormEngine.SetProfilePoint(null))
			{
				if (applicationId == IntPtr.Zero)
				{
					throw new ArgumentNullException("applicationId");
				}
				UbiServicesControllerBase fromHandle = UbiServicesControllerBase.GetFromHandle(controllerHandle);
				byte[] bytes = Encoding.UTF8.GetBytes(fromHandle.ApplicationId);
				bool flag = false;
				if ((ulong)applicationIdLen >= (ulong)((long)bytes.Length))
				{
					Marshal.Copy(bytes, 0, applicationId, bytes.Length);
					flag = true;
				}
				applicationIdLen = (uint)bytes.Length;
				flag2 = flag;
			}
			return flag2;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00007A0C File Offset: 0x00005C0C
		[MonoPInvokeCallback(typeof(UbiServicesControllerBase.Native.GetCustomFeatureSwitchFct))]
		private static bool GetCustomFeatureSwitch(IntPtr controllerHandle, string switchName)
		{
			bool customFeatureSwitch;
			using (StormEngine.SetProfilePoint(null))
			{
				if (switchName == null)
				{
					throw new ArgumentNullException("switchName");
				}
				customFeatureSwitch = UbiServicesControllerBase.GetFromHandle(controllerHandle).GetCustomFeatureSwitch(switchName);
			}
			return customFeatureSwitch;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00007A58 File Offset: 0x00005C58
		[MonoPInvokeCallback(typeof(UbiServicesControllerBase.Native.PushCustomEventFct))]
		private static bool PushCustomEvent(IntPtr controllerHandle, string eventName, string eventJsonToWrite)
		{
			bool flag;
			using (StormEngine.SetProfilePoint(null))
			{
				if (eventName == null)
				{
					throw new ArgumentNullException("eventName");
				}
				if (eventJsonToWrite == null)
				{
					throw new ArgumentNullException("eventJsonToWrite");
				}
				flag = UbiServicesControllerBase.GetFromHandle(controllerHandle).PushCustomEvent(eventName, eventJsonToWrite);
			}
			return flag;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00007AB4 File Offset: 0x00005CB4
		[MonoPInvokeCallback(typeof(UbiServicesControllerBase.Native.PushContextStartEventFct))]
		private static bool PushContextStartEvent(IntPtr controllerHandle, string eventType, string eventName, string eventJsonToWrite)
		{
			bool flag;
			using (StormEngine.SetProfilePoint(null))
			{
				if (eventName == null)
				{
					throw new ArgumentNullException("eventName");
				}
				if (eventJsonToWrite == null)
				{
					throw new ArgumentNullException("eventJsonToWrite");
				}
				flag = UbiServicesControllerBase.GetFromHandle(controllerHandle).PushContextStartEvent(eventType, eventName, eventJsonToWrite);
			}
			return flag;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00007B10 File Offset: 0x00005D10
		[MonoPInvokeCallback(typeof(UbiServicesControllerBase.Native.PushContextStopEventFct))]
		private static bool PushContextStopEvent(IntPtr controllerHandle, string eventType, string eventName, string eventJsonToWrite)
		{
			bool flag;
			using (StormEngine.SetProfilePoint(null))
			{
				if (eventName == null)
				{
					throw new ArgumentNullException("eventName");
				}
				if (eventJsonToWrite == null)
				{
					throw new ArgumentNullException("eventJsonToWrite");
				}
				flag = UbiServicesControllerBase.GetFromHandle(controllerHandle).PushContextStopEvent(eventType, eventName, eventJsonToWrite);
			}
			return flag;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00007B6C File Offset: 0x00005D6C
		[MonoPInvokeCallback(typeof(UbiServicesControllerBase.Native.IsJsonValidFct))]
		private static bool IsJsonValid(IntPtr controllerHandle, string json)
		{
			bool flag;
			using (StormEngine.SetProfilePoint(null))
			{
				if (json == null)
				{
					throw new ArgumentNullException("json");
				}
				flag = UbiServicesControllerBase.GetFromHandle(controllerHandle).IsJsonValid(json);
			}
			return flag;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00007BB8 File Offset: 0x00005DB8
		[MonoPInvokeCallback(typeof(UbiServicesControllerBase.Native.SendHttpGetRequestFct))]
		private static void SendHttpGetRequest(IntPtr controllerHandle, string url, uint headersCount, IntPtr headersKeys, IntPtr headersValues, ulong whenDone, byte useDefaultRetryConfig, uint retryCount)
		{
			using (StormEngine.SetProfilePoint(null))
			{
				if (url == null)
				{
					throw new ArgumentNullException("url");
				}
				if (headersKeys == IntPtr.Zero)
				{
					throw new ArgumentNullException("headersKeys");
				}
				if (headersValues == IntPtr.Zero)
				{
					throw new ArgumentNullException("headersValues");
				}
				UbiServicesControllerBase controller = UbiServicesControllerBase.GetFromHandle(controllerHandle);
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				for (uint num = 0U; num < headersCount; num += 1U)
				{
					IntPtr intPtr = Marshal.ReadIntPtr(headersKeys, (int)((ulong)num * (ulong)((long)IntPtr.Size)));
					IntPtr intPtr2 = Marshal.ReadIntPtr(headersValues, (int)((ulong)num * (ulong)((long)IntPtr.Size)));
					dictionary.Add(Marshal.PtrToStringAnsi(intPtr), Marshal.PtrToStringAnsi(intPtr2));
				}
				UbiServicesControllerBase.HttpRequestResponse httpRequestResponse = delegate(bool hasSucceeded, uint statusCode, string responseBody)
				{
					UbiServicesControllerBase.Native.OnHttpRequestCompleted(controller.Handle, whenDone, hasSucceeded ? 1 : 0, statusCode, responseBody);
				};
				controller.SendHttpGetRequest(url, dictionary, httpRequestResponse, useDefaultRetryConfig > 0, retryCount);
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00007CC4 File Offset: 0x00005EC4
		[MonoPInvokeCallback(typeof(UbiServicesControllerBase.Native.SendHttpPostRequestFct))]
		private static void SendHttpPostRequest(IntPtr controllerHandle, string url, uint headersCount, IntPtr headersKeys, IntPtr headersValues, string body, ulong whenDone, byte useDefaultRetryConfig, uint retryCount)
		{
			using (StormEngine.SetProfilePoint(null))
			{
				if (url == null)
				{
					throw new ArgumentNullException("url");
				}
				if (headersKeys == IntPtr.Zero)
				{
					throw new ArgumentNullException("headersKeys");
				}
				if (headersValues == IntPtr.Zero)
				{
					throw new ArgumentNullException("headersValues");
				}
				if (body == null)
				{
					throw new ArgumentNullException("body");
				}
				UbiServicesControllerBase controller = UbiServicesControllerBase.GetFromHandle(controllerHandle);
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				for (uint num = 0U; num < headersCount; num += 1U)
				{
					IntPtr intPtr = Marshal.ReadIntPtr(headersKeys, (int)((ulong)num * (ulong)((long)IntPtr.Size)));
					IntPtr intPtr2 = Marshal.ReadIntPtr(headersValues, (int)((ulong)num * (ulong)((long)IntPtr.Size)));
					dictionary.Add(Marshal.PtrToStringAnsi(intPtr), Marshal.PtrToStringAnsi(intPtr2));
				}
				UbiServicesControllerBase.HttpRequestResponse httpRequestResponse = delegate(bool hasSucceeded, uint statusCode, string responseBody)
				{
					UbiServicesControllerBase.Native.OnHttpRequestCompleted(controller.Handle, whenDone, hasSucceeded ? 1 : 0, statusCode, responseBody);
				};
				controller.SendHttpPostRequest(url, dictionary, body, httpRequestResponse, useDefaultRetryConfig > 0, retryCount);
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00007DE0 File Offset: 0x00005FE0
		[MonoPInvokeCallback(typeof(UbiServicesControllerBase.Native.GetValueInJsonFct))]
		private static bool GetValueInJson(IntPtr controllerHandle, string json, string key, IntPtr jsonValue, ref uint jsonValueLen)
		{
			bool flag2;
			using (StormEngine.SetProfilePoint(null))
			{
				if (json == null)
				{
					throw new ArgumentNullException("json");
				}
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (jsonValue == IntPtr.Zero)
				{
					throw new ArgumentNullException("jsonValue");
				}
				UbiServicesControllerBase fromHandle = UbiServicesControllerBase.GetFromHandle(controllerHandle);
				byte[] bytes = Encoding.UTF8.GetBytes(fromHandle.GetValueInJson(json, key));
				bool flag = false;
				if ((ulong)jsonValueLen >= (ulong)((long)bytes.Length))
				{
					Marshal.Copy(bytes, 0, jsonValue, bytes.Length);
					flag = true;
				}
				jsonValueLen = (uint)bytes.Length;
				flag2 = flag;
			}
			return flag2;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00007E84 File Offset: 0x00006084
		[MonoPInvokeCallback(typeof(UbiServicesControllerBase.Native.GetTitleSpaceIdFct))]
		private static bool GetTitleSpaceId(IntPtr controllerHandle, IntPtr titleSpaceId, ref uint titleSpaceIdLen)
		{
			bool flag2;
			using (StormEngine.SetProfilePoint(null))
			{
				if (titleSpaceId == IntPtr.Zero)
				{
					throw new ArgumentNullException("titleSpaceId");
				}
				UbiServicesControllerBase fromHandle = UbiServicesControllerBase.GetFromHandle(controllerHandle);
				byte[] bytes = Encoding.UTF8.GetBytes(fromHandle.TitleSpaceId);
				bool flag = false;
				if ((ulong)titleSpaceIdLen >= (ulong)((long)bytes.Length))
				{
					Marshal.Copy(bytes, 0, titleSpaceId, bytes.Length);
					flag = true;
				}
				titleSpaceIdLen = (uint)bytes.Length;
				flag2 = flag;
			}
			return flag2;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00007F08 File Offset: 0x00006108
		[MonoPInvokeCallback(typeof(UbiServicesControllerBase.Native.GetUrlPrefixForConnectedEnvironmentFct))]
		private static bool GetUrlPrefixForConnectedEnvironment(IntPtr controllerHandle, IntPtr urlPrefix, ref uint urlPrefixLen)
		{
			bool flag2;
			using (StormEngine.SetProfilePoint(null))
			{
				if (urlPrefix == IntPtr.Zero)
				{
					throw new ArgumentNullException("urlPrefix");
				}
				UbiServicesControllerBase fromHandle = UbiServicesControllerBase.GetFromHandle(controllerHandle);
				byte[] bytes = Encoding.UTF8.GetBytes(fromHandle.UrlPrefixForConnectedEnvironment);
				bool flag = false;
				if ((ulong)urlPrefixLen >= (ulong)((long)bytes.Length))
				{
					Marshal.Copy(bytes, 0, urlPrefix, bytes.Length);
					flag = true;
				}
				urlPrefixLen = (uint)bytes.Length;
				flag2 = flag;
			}
			return flag2;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00007F8C File Offset: 0x0000618C
		[MonoPInvokeCallback(typeof(UbiServicesControllerBase.Native.GetNativeFacadeFct))]
		private static IntPtr GetNativeFacade(IntPtr controllerHandle)
		{
			IntPtr nativeFacade;
			using (StormEngine.SetProfilePoint(null))
			{
				nativeFacade = UbiServicesControllerBase.GetFromHandle(controllerHandle).NativeFacade;
			}
			return nativeFacade;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00007FCC File Offset: 0x000061CC
		[MonoPInvokeCallback(typeof(UbiServicesControllerBase.Native.GetSessionTicketFct))]
		private static bool GetSessionTicket(IntPtr controllerHandle, IntPtr sessionTicket, ref uint sessionTicketLen)
		{
			bool flag2;
			using (StormEngine.SetProfilePoint(null))
			{
				if (sessionTicket == IntPtr.Zero)
				{
					throw new ArgumentNullException("sessionTicket");
				}
				UbiServicesControllerBase fromHandle = UbiServicesControllerBase.GetFromHandle(controllerHandle);
				byte[] bytes = Encoding.UTF8.GetBytes(fromHandle.SessionTicket);
				bool flag = false;
				if ((ulong)sessionTicketLen >= (ulong)((long)bytes.Length))
				{
					Marshal.Copy(bytes, 0, sessionTicket, bytes.Length);
					flag = true;
				}
				sessionTicketLen = (uint)bytes.Length;
				flag2 = flag;
			}
			return flag2;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00008050 File Offset: 0x00006250
		[MonoPInvokeCallback(typeof(UbiServicesControllerBase.Native.GetSessionIdFct))]
		private static bool GetSessionId(IntPtr controllerHandle, IntPtr sessionId, ref uint sessionIdLen)
		{
			bool flag2;
			using (StormEngine.SetProfilePoint(null))
			{
				if (sessionId == IntPtr.Zero)
				{
					throw new ArgumentNullException("sessionId");
				}
				UbiServicesControllerBase fromHandle = UbiServicesControllerBase.GetFromHandle(controllerHandle);
				byte[] bytes = Encoding.UTF8.GetBytes(fromHandle.SessionId);
				bool flag = false;
				if ((ulong)sessionIdLen >= (ulong)((long)bytes.Length))
				{
					Marshal.Copy(bytes, 0, sessionId, bytes.Length);
					flag = true;
				}
				sessionIdLen = (uint)bytes.Length;
				flag2 = flag;
			}
			return flag2;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x000080D4 File Offset: 0x000062D4
		[MonoPInvokeCallback(typeof(UbiServicesControllerBase.Native.IsLoggedInFct))]
		private static bool IsLoggedIn(IntPtr controllerHandle)
		{
			bool isLogged;
			using (StormEngine.SetProfilePoint(null))
			{
				isLogged = UbiServicesControllerBase.GetFromHandle(controllerHandle).IsLogged;
			}
			return isLogged;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00008114 File Offset: 0x00006314
		[MonoPInvokeCallback(typeof(UbiServicesControllerBase.Native.GetUpstreamHttpDataCounterFct))]
		private static uint GetUpstreamHttpDataCounter(IntPtr controllerHandle)
		{
			uint upstreamHttpDataCounter;
			using (StormEngine.SetProfilePoint(null))
			{
				upstreamHttpDataCounter = UbiServicesControllerBase.GetFromHandle(controllerHandle).UpstreamHttpDataCounter;
			}
			return upstreamHttpDataCounter;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00008154 File Offset: 0x00006354
		[MonoPInvokeCallback(typeof(UbiServicesControllerBase.Native.GetUpstreamStreamHttpHttpDataCounterFct))]
		private static uint GetUpstreamStreamHttpHttpDataCounter(IntPtr controllerHandle)
		{
			uint upstreamStreamHttpHttpDataCounter;
			using (StormEngine.SetProfilePoint(null))
			{
				upstreamStreamHttpHttpDataCounter = UbiServicesControllerBase.GetFromHandle(controllerHandle).UpstreamStreamHttpHttpDataCounter;
			}
			return upstreamStreamHttpHttpDataCounter;
		}

		// Token: 0x04000092 RID: 146
		private static readonly List<GCHandle> gcHandles = new List<GCHandle>();

		// Token: 0x04000093 RID: 147
		private GCHandle gcHandle;

		// Token: 0x04000094 RID: 148
		private IntPtr handle;

		// Token: 0x04000095 RID: 149
		private bool enabled;

		// Token: 0x04000096 RID: 150
		protected bool disposed;

		// Token: 0x02000070 RID: 112
		public sealed class FacadeResetEventArgs : EventArgs
		{
		}

		// Token: 0x02000071 RID: 113
		public sealed class ServerConnectedEventArgs : ResultEventArgs
		{
			// Token: 0x0600024D RID: 589 RVA: 0x000081A8 File Offset: 0x000063A8
			public ServerConnectedEventArgs(EResult result)
				: base(result)
			{
			}
		}

		// Token: 0x02000072 RID: 114
		public sealed class ServerDisconnectedEventArgs : ResultEventArgs
		{
			// Token: 0x0600024E RID: 590 RVA: 0x000081A8 File Offset: 0x000063A8
			public ServerDisconnectedEventArgs(EResult result)
				: base(result)
			{
			}
		}

		// Token: 0x02000073 RID: 115
		public sealed class S2STicketSuccessEventArgs : ResultEventArgs
		{
			// Token: 0x1700002E RID: 46
			// (get) Token: 0x0600024F RID: 591 RVA: 0x000081B1 File Offset: 0x000063B1
			public string S2STicket { get; }
		}

		// Token: 0x02000074 RID: 116
		public sealed class S2STicketFailureEventArgs : ResultEventArgs
		{
			// Token: 0x1700002F RID: 47
			// (get) Token: 0x06000250 RID: 592 RVA: 0x000081B9 File Offset: 0x000063B9
			public uint StatusCode { get; }
		}

		// Token: 0x02000075 RID: 117
		public sealed class S2SConfigUrlSuccessEventArgs : ResultEventArgs
		{
			// Token: 0x17000030 RID: 48
			// (get) Token: 0x06000251 RID: 593 RVA: 0x000081C1 File Offset: 0x000063C1
			public string S2SConfigUrl { get; }
		}

		// Token: 0x02000076 RID: 118
		private static class Native
		{
			// Token: 0x06000252 RID: 594
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "UbiServicesController_Initialize")]
			public static extern void Initialize(UbiServicesControllerBase.Native.EnableFct enableFct, UbiServicesControllerBase.Native.DisableFct disableFct, UbiServicesControllerBase.Native.UpdateFct updateFct, UbiServicesControllerBase.Native.ReloginFct reloginFct, UbiServicesControllerBase.Native.GetStormUrlFct getStormUrlFct, UbiServicesControllerBase.Native.GetApplicationIdFct getApplicationIdFct, UbiServicesControllerBase.Native.GetCustomFeatureSwitchFct getCustomFeatureSwitchFct, UbiServicesControllerBase.Native.PushCustomEventFct pushCustomEventFct, UbiServicesControllerBase.Native.PushContextStartEventFct pushContextStartEventFct, UbiServicesControllerBase.Native.PushContextStopEventFct pushContextStopEventFct, UbiServicesControllerBase.Native.IsJsonValidFct isJsonValidFct, UbiServicesControllerBase.Native.SendHttpGetRequestFct sendHttpGetRequestFct, UbiServicesControllerBase.Native.SendHttpPostRequestFct sendHttpPostRequestFct, UbiServicesControllerBase.Native.GetValueInJsonFct getValueInJsonFct, UbiServicesControllerBase.Native.GetTitleSpaceIdFct getTitleSpaceIdFct, UbiServicesControllerBase.Native.GetUrlPrefixForConnectedEnvironmentFct getUrlPrefixForConnectedEnvironmentFct, UbiServicesControllerBase.Native.GetNativeFacadeFct getNativeFacadeFct, UbiServicesControllerBase.Native.GetSessionTicketFct getSessionTicketFct, UbiServicesControllerBase.Native.GetSessionIdFct getSessionIdFct, UbiServicesControllerBase.Native.IsLoggedInFct isLoggedInFct, UbiServicesControllerBase.Native.GetUpstreamHttpDataCounterFct getUpstreamHttpDataCounterFct, UbiServicesControllerBase.Native.GetUpstreamStreamHttpHttpDataCounterFct getUpstreamStreamHttpHttpDataCounterFct);

			// Token: 0x06000253 RID: 595
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "UbiServicesController_Uninitialize")]
			public static extern void Uninitialize();

			// Token: 0x06000254 RID: 596
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "UbiServicesController_Create")]
			public static extern IntPtr Create(IntPtr controllerHandle);

			// Token: 0x06000255 RID: 597
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "UbiServicesController_Destroy")]
			public static extern void Destroy(IntPtr nativeHandle);

			// Token: 0x06000256 RID: 598
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "UbiServicesController_SetEnabled")]
			public static extern void SetEnabled(IntPtr nativeHandle, byte enabled);

			// Token: 0x06000257 RID: 599
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "UbiServicesController_OnHttpRequestCompleted")]
			public static extern void OnHttpRequestCompleted(IntPtr nativeHandle, ulong whenDone, byte hasSucceeded, uint statusCode, [MarshalAs(UnmanagedType.LPStr)] string responseBody);

			// Token: 0x06000258 RID: 600
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "UbiServicesController_OnFacadeReset")]
			public static extern void OnFacadeReset(IntPtr nativeHandle);

			// Token: 0x06000259 RID: 601
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "UbiServicesController_OnServerConnected")]
			public static extern void OnServerConnected(IntPtr nativeHandle, int errorCode, [MarshalAs(UnmanagedType.LPStr)] string errorMessage);

			// Token: 0x0600025A RID: 602
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "UbiServicesController_OnServerDisconnected")]
			public static extern void OnServerDisconnected(IntPtr nativeHandle, int errorCode, [MarshalAs(UnmanagedType.LPStr)] string errorMessage);

			// Token: 0x0600025B RID: 603
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "UbiServicesController_OnS2STicketSuccess")]
			public static extern void OnS2STicketSuccess(IntPtr nativeHandle, [MarshalAs(UnmanagedType.LPStr)] string s2sTicket, int errorCode, [MarshalAs(UnmanagedType.LPStr)] string errorMessage);

			// Token: 0x0600025C RID: 604
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "UbiServicesController_OnS2STicketFailure")]
			public static extern void OnS2STicketFailure(IntPtr nativeHandle, uint statusCode, int errorCode, [MarshalAs(UnmanagedType.LPStr)] string errorMessage);

			// Token: 0x0600025D RID: 605
			[DllImport("Storm", CharSet = CharSet.Ansi, EntryPoint = "UbiServicesController_OnS2SConfigUrlSuccess")]
			public static extern void OnS2SConfigUrlSuccess(IntPtr nativeHandle, [MarshalAs(UnmanagedType.LPStr)] string s2sConfigUrl, int errorCode, [MarshalAs(UnmanagedType.LPStr)] string errorMessage);

			// Token: 0x02000077 RID: 119
			// (Invoke) Token: 0x0600025F RID: 607
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void EnableFct(IntPtr controllerHandle, ref int errorCode, IntPtr errorMsg, uint errorMsgLen);

			// Token: 0x02000078 RID: 120
			// (Invoke) Token: 0x06000263 RID: 611
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void DisableFct(IntPtr controllerHandle, ref int errorCode, IntPtr errorMsg, uint errorMsgLen);

			// Token: 0x02000079 RID: 121
			// (Invoke) Token: 0x06000267 RID: 615
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void UpdateFct(IntPtr controllerHandle);

			// Token: 0x0200007A RID: 122
			// (Invoke) Token: 0x0600026B RID: 619
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			[return: MarshalAs(UnmanagedType.U1)]
			public delegate bool ReloginFct(IntPtr controllerHandle);

			// Token: 0x0200007B RID: 123
			// (Invoke) Token: 0x0600026F RID: 623
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			[return: MarshalAs(UnmanagedType.U1)]
			public delegate bool GetStormUrlFct(IntPtr controllerHandle, [MarshalAs(UnmanagedType.LPStr)] string urlKeyName, IntPtr stormUrl, ref uint stormUrlLen);

			// Token: 0x0200007C RID: 124
			// (Invoke) Token: 0x06000273 RID: 627
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			[return: MarshalAs(UnmanagedType.U1)]
			public delegate bool GetApplicationIdFct(IntPtr controllerHandle, IntPtr applicationId, ref uint applicationIdLen);

			// Token: 0x0200007D RID: 125
			// (Invoke) Token: 0x06000277 RID: 631
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			[return: MarshalAs(UnmanagedType.U1)]
			public delegate bool GetCustomFeatureSwitchFct(IntPtr controllerHandle, [MarshalAs(UnmanagedType.LPStr)] string switchName);

			// Token: 0x0200007E RID: 126
			// (Invoke) Token: 0x0600027B RID: 635
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			[return: MarshalAs(UnmanagedType.U1)]
			public delegate bool PushCustomEventFct(IntPtr controllerHandle, [MarshalAs(UnmanagedType.LPStr)] string eventName, [MarshalAs(UnmanagedType.LPStr)] string eventJsonToWrite);

			// Token: 0x0200007F RID: 127
			// (Invoke) Token: 0x0600027F RID: 639
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			[return: MarshalAs(UnmanagedType.U1)]
			public delegate bool PushContextStartEventFct(IntPtr controllerHandle, [MarshalAs(UnmanagedType.LPStr)] string eventType, [MarshalAs(UnmanagedType.LPStr)] string eventName, [MarshalAs(UnmanagedType.LPStr)] string eventJsonToWrite);

			// Token: 0x02000080 RID: 128
			// (Invoke) Token: 0x06000283 RID: 643
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			[return: MarshalAs(UnmanagedType.U1)]
			public delegate bool PushContextStopEventFct(IntPtr controllerHandle, [MarshalAs(UnmanagedType.LPStr)] string eventType, [MarshalAs(UnmanagedType.LPStr)] string eventName, [MarshalAs(UnmanagedType.LPStr)] string eventJsonToWrite);

			// Token: 0x02000081 RID: 129
			// (Invoke) Token: 0x06000287 RID: 647
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			[return: MarshalAs(UnmanagedType.U1)]
			public delegate bool IsJsonValidFct(IntPtr controllerHandle, [MarshalAs(UnmanagedType.LPStr)] string json);

			// Token: 0x02000082 RID: 130
			// (Invoke) Token: 0x0600028B RID: 651
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void SendHttpGetRequestFct(IntPtr controllerHandle, [MarshalAs(UnmanagedType.LPStr)] string url, uint headersCount, IntPtr headersKeys, IntPtr headersValue, ulong whenDone, byte useDefaultRetryConfig, uint retryCount);

			// Token: 0x02000083 RID: 131
			// (Invoke) Token: 0x0600028F RID: 655
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void SendHttpPostRequestFct(IntPtr controllerHandle, [MarshalAs(UnmanagedType.LPStr)] string url, uint headersCount, IntPtr headersKeys, IntPtr headersValue, [MarshalAs(UnmanagedType.LPStr)] string body, ulong whenDone, byte useDefaultRetryConfig, uint retryCount);

			// Token: 0x02000084 RID: 132
			// (Invoke) Token: 0x06000293 RID: 659
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			[return: MarshalAs(UnmanagedType.U1)]
			public delegate bool GetValueInJsonFct(IntPtr controllerHandle, [MarshalAs(UnmanagedType.LPStr)] string json, [MarshalAs(UnmanagedType.LPStr)] string key, IntPtr value, ref uint valueLen);

			// Token: 0x02000085 RID: 133
			// (Invoke) Token: 0x06000297 RID: 663
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			[return: MarshalAs(UnmanagedType.U1)]
			public delegate bool GetTitleSpaceIdFct(IntPtr controllerHandle, IntPtr titleSpaceId, ref uint titleSpaceIdLen);

			// Token: 0x02000086 RID: 134
			// (Invoke) Token: 0x0600029B RID: 667
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			[return: MarshalAs(UnmanagedType.U1)]
			public delegate bool GetUrlPrefixForConnectedEnvironmentFct(IntPtr controllerHandle, IntPtr urlPrefix, ref uint urlPrefixLen);

			// Token: 0x02000087 RID: 135
			// (Invoke) Token: 0x0600029F RID: 671
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate IntPtr GetNativeFacadeFct(IntPtr controllerHandle);

			// Token: 0x02000088 RID: 136
			// (Invoke) Token: 0x060002A3 RID: 675
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			[return: MarshalAs(UnmanagedType.U1)]
			public delegate bool GetSessionTicketFct(IntPtr controllerHandle, IntPtr sessionTicket, ref uint sessionTicketLen);

			// Token: 0x02000089 RID: 137
			// (Invoke) Token: 0x060002A7 RID: 679
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			[return: MarshalAs(UnmanagedType.U1)]
			public delegate bool GetSessionIdFct(IntPtr controllerHandle, IntPtr sessionId, ref uint sessionIdLen);

			// Token: 0x0200008A RID: 138
			// (Invoke) Token: 0x060002AB RID: 683
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			[return: MarshalAs(UnmanagedType.U1)]
			public delegate bool IsLoggedInFct(IntPtr controllerHandle);

			// Token: 0x0200008B RID: 139
			// (Invoke) Token: 0x060002AF RID: 687
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate uint GetUpstreamHttpDataCounterFct(IntPtr controllerHandle);

			// Token: 0x0200008C RID: 140
			// (Invoke) Token: 0x060002B3 RID: 691
			[MonoUnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate uint GetUpstreamStreamHttpHttpDataCounterFct(IntPtr controllerHandle);
		}

		// Token: 0x0200008D RID: 141
		// (Invoke) Token: 0x060002B7 RID: 695
		public delegate void HttpRequestResponse(bool hasSucceeded, uint statusCode, string responseBody);
	}
}
