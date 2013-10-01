// --------------------------------------------------------------------------------------------
// Version: MPL 1.1/GPL 2.0/LGPL 2.1
// 
// The contents of this file are subject to the Mozilla Public License Version
// 1.1 (the "License"); you may not use this file except in compliance with
// the License. You may obtain a copy of the License at
// http://www.mozilla.org/MPL/
// 
// Software distributed under the License is distributed on an "AS IS" basis,
// WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License
// for the specific language governing rights and limitations under the
// License.
// 
// <remarks>
// Generated by IDLImporter from file nsIDOMIccManager.idl
// 
// You should use these interfaces when you access the COM objects defined in the mentioned
// IDL/IDH file.
// </remarks>
// --------------------------------------------------------------------------------------------
namespace Gecko
{
	using System;
	using System.Runtime.InteropServices;
	using System.Runtime.InteropServices.ComTypes;
	using System.Runtime.CompilerServices;
	
	
	/// <summary>
    ///This Source Code Form is subject to the terms of the Mozilla Public
    /// License, v. 2.0. If a copy of the MPL was not distributed with this file,
    /// You can obtain one at http://mozilla.org/MPL/2.0/. </summary>
	[ComImport()]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("9d898c66-3485-4cd5-ab8d-92ef2988887b")]
	public interface nsIDOMMozIccManager : nsIDOMEventTarget
	{
		
		/// <summary>
        /// This method allows the registration of event listeners on the event target.
        /// If an EventListener is added to an EventTarget while it is processing an
        /// event, it will not be triggered by the current actions but may be
        /// triggered during a later stage of event flow, such as the bubbling phase.
        ///
        /// If multiple identical EventListeners are registered on the same
        /// EventTarget with the same parameters the duplicate instances are
        /// discarded. They do not cause the EventListener to be called twice
        /// and since they are discarded they do not need to be removed with the
        /// removeEventListener method.
        ///
        /// @param   type The event type for which the user is registering
        /// @param   listener The listener parameter takes an interface
        /// implemented by the user which contains the methods
        /// to be called when the event occurs.
        /// @param   useCapture If true, useCapture indicates that the user
        /// wishes to initiate capture. After initiating
        /// capture, all events of the specified type will be
        /// dispatched to the registered EventListener before
        /// being dispatched to any EventTargets beneath them
        /// in the tree. Events which are bubbling upward
        /// through the tree will not trigger an
        /// EventListener designated to use capture.
        /// @param   wantsUntrusted If false, the listener will not receive any
        /// untrusted events (see above), if true, the
        /// listener will receive events whether or not
        /// they're trusted
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new void AddEventListener([MarshalAs(UnmanagedType.LPStruct)] nsAStringBase type, [MarshalAs(UnmanagedType.Interface)] nsIDOMEventListener listener, [MarshalAs(UnmanagedType.U1)] bool useCapture, [MarshalAs(UnmanagedType.U1)] bool wantsUntrusted, int argc);
		
		/// <summary>
        /// addSystemEventListener() adds an event listener of aType to the system
        /// group.  Typically, core code should use system group for listening to
        /// content (i.e., non-chrome) element's events.  If core code uses
        /// nsIDOMEventTarget::AddEventListener for a content node, it means
        /// that the listener cannot listen the event when web content calls
        /// stopPropagation() of the event.
        ///
        /// @param aType            An event name you're going to handle.
        /// @param aListener        An event listener.
        /// @param aUseCapture      TRUE if you want to listen the event in capturing
        /// phase.  Otherwise, FALSE.
        /// @param aWantsUntrusted  TRUE if you want to handle untrusted events.
        /// Otherwise, FALSE.
        /// @return                 NS_OK if succeed.  Otherwise, NS_ERROR_*.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new void AddSystemEventListener([MarshalAs(UnmanagedType.LPStruct)] nsAStringBase type, [MarshalAs(UnmanagedType.Interface)] nsIDOMEventListener listener, [MarshalAs(UnmanagedType.U1)] bool aUseCapture, [MarshalAs(UnmanagedType.U1)] bool aWantsUntrusted, int argc);
		
		/// <summary>
        /// This method allows the removal of event listeners from the event
        /// target. If an EventListener is removed from an EventTarget while it
        /// is processing an event, it will not be triggered by the current actions.
        /// EventListeners can never be invoked after being removed.
        /// Calling removeEventListener with arguments which do not identify any
        /// currently registered EventListener on the EventTarget has no effect.
        ///
        /// @param   type Specifies the event type of the EventListener being
        /// removed.
        /// @param   listener The EventListener parameter indicates the
        /// EventListener to be removed.
        /// @param   useCapture Specifies whether the EventListener being
        /// removed was registered as a capturing listener or
        /// not. If a listener was registered twice, one with
        /// capture and one without, each must be removed
        /// separately. Removal of a capturing listener does
        /// not affect a non-capturing version of the same
        /// listener, and vice versa.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new void RemoveEventListener([MarshalAs(UnmanagedType.LPStruct)] nsAStringBase type, [MarshalAs(UnmanagedType.Interface)] nsIDOMEventListener listener, [MarshalAs(UnmanagedType.U1)] bool useCapture);
		
		/// <summary>
        /// removeSystemEventListener() should be used if you have used
        /// addSystemEventListener().
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new void RemoveSystemEventListener([MarshalAs(UnmanagedType.LPStruct)] nsAStringBase type, [MarshalAs(UnmanagedType.Interface)] nsIDOMEventListener listener, [MarshalAs(UnmanagedType.U1)] bool aUseCapture);
		
		/// <summary>
        /// This method allows the dispatch of events into the implementations
        /// event model. Events dispatched in this manner will have the same
        /// capturing and bubbling behavior as events dispatched directly by the
        /// implementation. The target of the event is the EventTarget on which
        /// dispatchEvent is called.
        ///
        /// @param   evt Specifies the event type, behavior, and contextual
        /// information to be used in processing the event.
        /// @return  Indicates whether any of the listeners which handled the
        /// event called preventDefault. If preventDefault was called
        /// the value is false, else the value is true.
        /// @throws  INVALID_STATE_ERR: Raised if the Event's type was
        /// not specified by initializing the event before
        /// dispatchEvent was called. Specification of the Event's
        /// type as null or an empty string will also trigger this
        /// exception.
        /// </summary>
		[return: MarshalAs(UnmanagedType.U1)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new bool DispatchEvent([MarshalAs(UnmanagedType.Interface)] nsIDOMEvent evt);
		
		/// <summary>
        /// Returns the nsPIDOMEventTarget object which should be used as the target
        /// of DOMEvents.
        /// Usually |this| is returned, but for example global object returns
        /// the outer object.
        /// </summary>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new nsIDOMEventTarget GetTargetForDOMEvent();
		
		/// <summary>
        /// Returns the nsPIDOMEventTarget object which should be used as the target
        /// of the event and when constructing event target chain.
        /// Usually |this| is returned, but for example global object returns
        /// the inner object.
        /// </summary>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new nsIDOMEventTarget GetTargetForEventTargetChain();
		
		/// <summary>
        /// Called before the capture phase of the event flow.
        /// This is used to create the event target chain and implementations
        /// should set the necessary members of nsEventChainPreVisitor.
        /// At least aVisitor.mCanHandle must be set,
        /// usually also aVisitor.mParentTarget if mCanHandle is PR_TRUE.
        /// First one tells that this object can handle the aVisitor.mEvent event and
        /// the latter one is the possible parent object for the event target chain.
        /// @see nsEventDispatcher.h for more documentation about aVisitor.
        ///
        /// @param aVisitor the visitor object which is used to create the
        /// event target chain for event dispatching.
        ///
        /// @note Only nsEventDispatcher should call this method.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new void PreHandleEvent(System.IntPtr aVisitor);
		
		/// <summary>
        /// If nsEventChainPreVisitor.mWantsWillHandleEvent is set PR_TRUE,
        /// called just before possible event handlers on this object will be called.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new void WillHandleEvent(System.IntPtr aVisitor);
		
		/// <summary>
        /// Called after the bubble phase of the system event group.
        /// The default handling of the event should happen here.
        /// @param aVisitor the visitor object which is used during post handling.
        ///
        /// @see nsEventDispatcher.h for documentation about aVisitor.
        /// @note Only nsEventDispatcher should call this method.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new void PostHandleEvent(System.IntPtr aVisitor);
		
		/// <summary>
        /// Dispatch an event.
        /// @param aEvent the event that is being dispatched.
        /// @param aDOMEvent the event that is being dispatched, use if you want to
        /// dispatch nsIDOMEvent, not only nsEvent.
        /// @param aPresContext the current presentation context, can be nullptr.
        /// @param aEventStatus the status returned from the function, can be nullptr.
        ///
        /// @note If both aEvent and aDOMEvent are used, aEvent must be the internal
        /// event of the aDOMEvent.
        ///
        /// If aDOMEvent is not nullptr (in which case aEvent can be nullptr) it is used
        /// for dispatching, otherwise aEvent is used.
        ///
        /// @deprecated This method is here just until all the callers outside Gecko
        /// have been converted to use nsIDOMEventTarget::dispatchEvent.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new void DispatchDOMEvent(System.IntPtr aEvent, [MarshalAs(UnmanagedType.Interface)] nsIDOMEvent aDOMEvent, System.IntPtr aPresContext, System.IntPtr aEventStatus);
		
		/// <summary>
        /// Get the event listener manager, the guy you talk to to register for events
        /// on this node.
        /// @param aMayCreate If PR_FALSE, returns a listener manager only if
        /// one already exists.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new System.IntPtr GetListenerManager([MarshalAs(UnmanagedType.U1)] bool aMayCreate);
		
		/// <summary>
        /// Get the script context in which the event handlers should be run.
        /// May return null.
        /// @note Caller *must* check the value of aRv.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new System.IntPtr GetContextForEventHandlers(ref int aRv);
		
		/// <summary>
        /// If the method above returns null, but a success code, this method
        /// is called.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new System.IntPtr GetJSContextForEventHandlers();
		
		/// <summary>
        /// Send the response back to ICC after an attempt to execute STK Proactive
        /// Command.
        ///
        /// @param command
        /// Command received from ICC. See MozStkCommand.
        /// @param response
        /// The response that will be sent to ICC.
        /// @see MozStkResponse for the detail of response.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void SendStkResponse(Gecko.JsVal command, Gecko.JsVal response);
		
		/// <summary>
        /// Send the "Menu Selection" Envelope command to ICC for menu selection.
        ///
        /// @param itemIdentifier
        /// The identifier of the item selected by user.
        /// @param helpRequested
        /// true if user requests to provide help information, false otherwise.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void SendStkMenuSelection(ushort itemIdentifier, [MarshalAs(UnmanagedType.U1)] bool helpRequested);
		
		/// <summary>
        /// Send "Event Download" Envelope command to ICC.
        /// ICC will not respond with any data for this command.
        ///
        /// @param event
        /// one of events below:
        /// - MozStkLocationEvent
        /// - MozStkCallEvent
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void SendStkEventDownload(Gecko.JsVal @event);
		
		/// <summary>
        /// The 'stkcommand' event is notified whenever STK Proactive Command is
        /// issued from ICC.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		Gecko.JsVal GetOnstkcommandAttribute(System.IntPtr jsContext);
		
		/// <summary>
        /// The 'stkcommand' event is notified whenever STK Proactive Command is
        /// issued from ICC.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void SetOnstkcommandAttribute(Gecko.JsVal aOnstkcommand, System.IntPtr jsContext);
		
		/// <summary>
        /// 'stksessionend' event is notified whenever STK Session is terminated by
        /// ICC.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		Gecko.JsVal GetOnstksessionendAttribute(System.IntPtr jsContext);
		
		/// <summary>
        /// 'stksessionend' event is notified whenever STK Session is terminated by
        /// ICC.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void SetOnstksessionendAttribute(Gecko.JsVal aOnstksessionend, System.IntPtr jsContext);
	}
	
	/// <summary>nsIDOMMozIccManagerConsts </summary>
	public class nsIDOMMozIccManagerConsts
	{
		
		// <summary>
        // STK Menu Presentation types.
        // </summary>
		public const ulong STK_MENU_TYPE_NOT_SPECIFIED = 0x00;
		
		// 
		public const ulong STK_MENU_TYPE_DATA_VALUES = 0x01;
		
		// 
		public const ulong STK_MENU_TYPE_NAVIGATION_OPTIONS = 0x03;
		
		// <summary>
        // Browser launch mode.
        // </summary>
		public const ulong STK_BROWSER_MODE_LAUNCH_IF_NOT_ALREADY_LAUNCHED = 0x00;
		
		// 
		public const ulong STK_BROWSER_MODE_USING_EXISTING_BROWSER = 0x02;
		
		// 
		public const ulong STK_BROWSER_MODE_USING_NEW_BROWSER = 0x03;
		
		// <summary>
        // STK Proactive commands.
        //
        // @see TS 11.14, clause 13.4
        // </summary>
		public const ulong STK_CMD_REFRESH = 0x01;
		
		// 
		public const ulong STK_CMD_POLL_INTERVAL = 0x03;
		
		// 
		public const ulong STK_CMD_POLL_OFF = 0x04;
		
		// 
		public const ulong STK_CMD_SET_UP_EVENT_LIST = 0x05;
		
		// 
		public const ulong STK_CMD_SET_UP_CALL = 0x10;
		
		// 
		public const ulong STK_CMD_SEND_SS = 0x11;
		
		// 
		public const ulong STK_CMD_SEND_USSD = 0x12;
		
		// 
		public const ulong STK_CMD_SEND_SMS = 0x13;
		
		// 
		public const ulong STK_CMD_SEND_DTMF = 0x14;
		
		// 
		public const ulong STK_CMD_LAUNCH_BROWSER = 0x15;
		
		// 
		public const ulong STK_CMD_PLAY_TONE = 0x20;
		
		// 
		public const ulong STK_CMD_DISPLAY_TEXT = 0x21;
		
		// 
		public const ulong STK_CMD_GET_INKEY = 0x22;
		
		// 
		public const ulong STK_CMD_GET_INPUT = 0x23;
		
		// 
		public const ulong STK_CMD_SELECT_ITEM = 0x24;
		
		// 
		public const ulong STK_CMD_SET_UP_MENU = 0x25;
		
		// 
		public const ulong STK_CMD_SET_UP_IDLE_MODE_TEXT = 0x28;
		
		// <summary>
        //Command performed successfully </summary>
		public const ulong STK_RESULT_OK = 0x00;
		
		// <summary>
        //Command performed with partial comprehension </summary>
		public const ulong STK_RESULT_PRFRMD_WITH_PARTIAL_COMPREHENSION = 0x01;
		
		// <summary>
        //Command performed, with missing information </summary>
		public const ulong STK_RESULT_PRFRMD_WITH_MISSING_INFO = 0x02;
		
		// <summary>
        //REFRESH performed with additional EFs read </summary>
		public const ulong STK_RESULT_PRFRMD_WITH_ADDITIONAL_EFS_READ = 0x03;
		
		// <summary>
        //Command performed successfully, limited service </summary>
		public const ulong STK_RESULT_PRFRMD_LIMITED_SERVICE = 0x06;
		
		// <summary>
        //Proactive UICC session terminated by the user </summary>
		public const ulong STK_RESULT_UICC_SESSION_TERM_BY_USER = 0x10;
		
		// <summary>
        //Backward move in the proactive UICC session requested by the user </summary>
		public const ulong STK_RESULT_BACKWARD_MOVE_BY_USER = 0x11;
		
		// <summary>
        //No response from user </summary>
		public const ulong STK_RESULT_NO_RESPONSE_FROM_USER = 0x12;
		
		// <summary>
        //Help information required by the user </summary>
		public const ulong STK_RESULT_HELP_INFO_REQUIRED = 0x13;
		
		// <summary>
        //USSD or SS transaction terminated by the user </summary>
		public const ulong STK_RESULT_USSD_SS_SESSION_TERM_BY_USER = 0x14;
		
		// <summary>
        //Terminal currently unable to process command </summary>
		public const ulong STK_RESULT_TERMINAL_CRNTLY_UNABLE_TO_PROCESS = 0x20;
		
		// <summary>
        //Network currently unable to process command </summary>
		public const ulong STK_RESULT_NETWORK_CRNTLY_UNABLE_TO_PROCESS = 0x21;
		
		// <summary>
        //User did not accept the proactive command </summary>
		public const ulong STK_RESULT_USER_NOT_ACCEPT = 0x22;
		
		// <summary>
        //User cleared down call before connection or network release </summary>
		public const ulong STK_RESULT_USER_CLEAR_DOWN_CALL = 0x23;
		
		// <summary>
        //Launch browser generic error code </summary>
		public const ulong STK_RESULT_LAUNCH_BROWSER_ERROR = 0x26;
		
		// <summary>
        //Command beyond terminal's capabilities </summary>
		public const ulong STK_RESULT_BEYOND_TERMINAL_CAPABILITY = 0x30;
		
		// <summary>
        //Command type not understood by terminal </summary>
		public const ulong STK_RESULT_CMD_TYPE_NOT_UNDERSTOOD = 0x31;
		
		// <summary>
        //Command data not understood by terminal </summary>
		public const ulong STK_RESULT_CMD_DATA_NOT_UNDERSTOOD = 0x32;
		
		// <summary>
        //Command number not known by terminal </summary>
		public const ulong STK_RESULT_CMD_NUM_NOT_KNOWN = 0x33;
		
		// <summary>
        //SS Return Error </summary>
		public const ulong STK_RESULT_SS_RETURN_ERROR = 0x34;
		
		// <summary>
        //SMS RP-ERROR </summary>
		public const ulong STK_RESULT_SMS_RP_ERROR = 0x35;
		
		// <summary>
        //Error, required values are missing </summary>
		public const ulong STK_RESULT_REQUIRED_VALUES_MISSING = 0x36;
		
		// <summary>
        //USSD Return Error </summary>
		public const ulong STK_RESULT_USSD_RETURN_ERROR = 0x37;
		
		// <summary>
        //MultipleCard commands error </summary>
		public const ulong STK_RESULT_MULTI_CARDS_CMD_ERROR = 0x38;
		
		// <summary>
        // Interaction with call control by USIM or MO short message control by
        // USIM, permanent problem
        // </summary>
		public const ulong STK_RESULT_USIM_CALL_CONTROL_PERMANENT = 0x39;
		
		// <summary>
        //Bearer Independent Protocol error </summary>
		public const ulong STK_RESULT_BIP_ERROR = 0x3a;
		
		// <summary>
        // STK Event List
        // </summary>
		public const ulong STK_EVENT_TYPE_MT_CALL = 0x00;
		
		// 
		public const ulong STK_EVENT_TYPE_CALL_CONNECTED = 0x01;
		
		// 
		public const ulong STK_EVENT_TYPE_CALL_DISCONNECTED = 0x02;
		
		// 
		public const ulong STK_EVENT_TYPE_LOCATION_STATUS = 0x03;
		
		// 
		public const ulong STK_EVENT_TYPE_USER_ACTIVITY = 0x04;
		
		// 
		public const ulong STK_EVENT_TYPE_IDLE_SCREEN_AVAILABLE = 0x05;
		
		// 
		public const ulong STK_EVENT_TYPE_CARD_READER_STATUS = 0x06;
		
		// 
		public const ulong STK_EVENT_TYPE_LANGUAGE_SELECTION = 0x07;
		
		// 
		public const ulong STK_EVENT_TYPE_BROWSER_TERMINATION = 0x08;
		
		// 
		public const ulong STK_EVENT_TYPE_DATA_AVAILABLE = 0x09;
		
		// 
		public const ulong STK_EVENT_TYPE_CHANNEL_STATUS = 0x0a;
		
		// 
		public const ulong STK_EVENT_TYPE_SINGLE_ACCESS_TECHNOLOGY_CHANGED = 0x0b;
		
		// 
		public const ulong STK_EVENT_TYPE_DISPLAY_PARAMETER_CHANGED = 0x0c;
		
		// 
		public const ulong STK_EVENT_TYPE_LOCAL_CONNECTION = 0x0d;
		
		// 
		public const ulong STK_EVENT_TYPE_NETWORK_SEARCH_MODE_CHANGED = 0x0e;
		
		// 
		public const ulong STK_EVENT_TYPE_BROWSING_STATUS = 0x0f;
		
		// 
		public const ulong STK_EVENT_TYPE_FRAMES_INFORMATION_CHANGED = 0x10;
		
		// <summary>
        // The service state of STK Location Status.
        // </summary>
		public const ulong STK_SERVICE_STATE_NORMAL = 0x00;
		
		// 
		public const ulong STK_SERVICE_STATE_LIMITED = 0x01;
		
		// 
		public const ulong STK_SERVICE_STATE_UNAVAILABLE = 0x02;
		
		// <summary>
        // Tone type.
        // </summary>
		public const ulong STK_TONE_TYPE_DIAL_TONE = 0x01;
		
		// 
		public const ulong STK_TONE_TYPE_CALLED_SUBSCRIBER_BUSY = 0x02;
		
		// 
		public const ulong STK_TONE_TYPE_CONGESTION = 0x03;
		
		// 
		public const ulong STK_TONE_TYPE_RADIO_PATH_ACK = 0x04;
		
		// 
		public const ulong STK_TONE_TYPE_RADIO_PATH_NOT_AVAILABLE = 0x05;
		
		// 
		public const ulong STK_TONE_TYPE_ERROR = 0x06;
		
		// 
		public const ulong STK_TONE_TYPE_CALL_WAITING_TONE = 0x07;
		
		// 
		public const ulong STK_TONE_TYPE_RINGING_TONE = 0x08;
		
		// 
		public const ulong STK_TONE_TYPE_GENERAL_BEEP = 0x10;
		
		// 
		public const ulong STK_TONE_TYPE_POSITIVE_ACK_TONE = 0x11;
		
		// 
		public const ulong STK_TONE_TYPE_NEGATIVE_ACK_TONE = 0x12;
		
		// <summary>
        // Time unit
        // </summary>
		public const ulong STK_TIME_UNIT_MINUTE = 0x00;
		
		// 
		public const ulong STK_TIME_UNIT_SECOND = 0x01;
		
		// 
		public const ulong STK_TIME_UNIT_TENTH_SECOND = 0x02;
	}
}
