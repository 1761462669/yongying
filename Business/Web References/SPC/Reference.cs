﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.34209
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码是由 Microsoft.VSDesigner 4.0.30319.34209 版自动生成。
// 
#pragma warning disable 1591

namespace Business.SPC {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="SPCDataCollectionTestSoap", Namespace="http://tempuri.org/")]
    public partial class SPCDataCollectionTest : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback SPCDataCollectionOperationCompleted;
        
        private System.Threading.SendOrPostCallback SPCZSZHDataOperationCompleted;
        
        private System.Threading.SendOrPostCallback SPCZSGCDataOperationCompleted;
        
        private System.Threading.SendOrPostCallback SPCJBGCDataOperationCompleted;
        
        private System.Threading.SendOrPostCallback SPCLBCPDataOperationCompleted;
        
        private System.Threading.SendOrPostCallback SPCCPJYDataOperationCompleted;
        
        private System.Threading.SendOrPostCallback SPCDXFXDatOperationCompleted;
        
        private System.Threading.SendOrPostCallback SPCYSSJDataOperationCompleted;
        
        private System.Threading.SendOrPostCallback SPCSTDataOperationCompleted;
        
        private System.Threading.SendOrPostCallback SPCZSLXDataOperationCompleted;
        
        private System.Threading.SendOrPostCallback SPCJBWCDataOperationCompleted;
        
        private System.Threading.SendOrPostCallback InsertMonthDataOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public SPCDataCollectionTest() {
            this.Url = global::Business.Properties.Settings.Default.Business_SPC_SPCDataCollectionTest;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event SPCDataCollectionCompletedEventHandler SPCDataCollectionCompleted;
        
        /// <remarks/>
        public event SPCZSZHDataCompletedEventHandler SPCZSZHDataCompleted;
        
        /// <remarks/>
        public event SPCZSGCDataCompletedEventHandler SPCZSGCDataCompleted;
        
        /// <remarks/>
        public event SPCJBGCDataCompletedEventHandler SPCJBGCDataCompleted;
        
        /// <remarks/>
        public event SPCLBCPDataCompletedEventHandler SPCLBCPDataCompleted;
        
        /// <remarks/>
        public event SPCCPJYDataCompletedEventHandler SPCCPJYDataCompleted;
        
        /// <remarks/>
        public event SPCDXFXDatCompletedEventHandler SPCDXFXDatCompleted;
        
        /// <remarks/>
        public event SPCYSSJDataCompletedEventHandler SPCYSSJDataCompleted;
        
        /// <remarks/>
        public event SPCSTDataCompletedEventHandler SPCSTDataCompleted;
        
        /// <remarks/>
        public event SPCZSLXDataCompletedEventHandler SPCZSLXDataCompleted;
        
        /// <remarks/>
        public event SPCJBWCDataCompletedEventHandler SPCJBWCDataCompleted;
        
        /// <remarks/>
        public event InsertMonthDataCompletedEventHandler InsertMonthDataCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SPCDataCollection", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool SPCDataCollection(string wo, int isExist) {
            object[] results = this.Invoke("SPCDataCollection", new object[] {
                        wo,
                        isExist});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void SPCDataCollectionAsync(string wo, int isExist) {
            this.SPCDataCollectionAsync(wo, isExist, null);
        }
        
        /// <remarks/>
        public void SPCDataCollectionAsync(string wo, int isExist, object userState) {
            if ((this.SPCDataCollectionOperationCompleted == null)) {
                this.SPCDataCollectionOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSPCDataCollectionOperationCompleted);
            }
            this.InvokeAsync("SPCDataCollection", new object[] {
                        wo,
                        isExist}, this.SPCDataCollectionOperationCompleted, userState);
        }
        
        private void OnSPCDataCollectionOperationCompleted(object arg) {
            if ((this.SPCDataCollectionCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SPCDataCollectionCompleted(this, new SPCDataCollectionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SPCZSZHData", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool SPCZSZHData(string wo, int isExist) {
            object[] results = this.Invoke("SPCZSZHData", new object[] {
                        wo,
                        isExist});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void SPCZSZHDataAsync(string wo, int isExist) {
            this.SPCZSZHDataAsync(wo, isExist, null);
        }
        
        /// <remarks/>
        public void SPCZSZHDataAsync(string wo, int isExist, object userState) {
            if ((this.SPCZSZHDataOperationCompleted == null)) {
                this.SPCZSZHDataOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSPCZSZHDataOperationCompleted);
            }
            this.InvokeAsync("SPCZSZHData", new object[] {
                        wo,
                        isExist}, this.SPCZSZHDataOperationCompleted, userState);
        }
        
        private void OnSPCZSZHDataOperationCompleted(object arg) {
            if ((this.SPCZSZHDataCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SPCZSZHDataCompleted(this, new SPCZSZHDataCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SPCZSGCData", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool SPCZSGCData(string wo, int isExist) {
            object[] results = this.Invoke("SPCZSGCData", new object[] {
                        wo,
                        isExist});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void SPCZSGCDataAsync(string wo, int isExist) {
            this.SPCZSGCDataAsync(wo, isExist, null);
        }
        
        /// <remarks/>
        public void SPCZSGCDataAsync(string wo, int isExist, object userState) {
            if ((this.SPCZSGCDataOperationCompleted == null)) {
                this.SPCZSGCDataOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSPCZSGCDataOperationCompleted);
            }
            this.InvokeAsync("SPCZSGCData", new object[] {
                        wo,
                        isExist}, this.SPCZSGCDataOperationCompleted, userState);
        }
        
        private void OnSPCZSGCDataOperationCompleted(object arg) {
            if ((this.SPCZSGCDataCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SPCZSGCDataCompleted(this, new SPCZSGCDataCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SPCJBGCData", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool SPCJBGCData(string wo, int isExist) {
            object[] results = this.Invoke("SPCJBGCData", new object[] {
                        wo,
                        isExist});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void SPCJBGCDataAsync(string wo, int isExist) {
            this.SPCJBGCDataAsync(wo, isExist, null);
        }
        
        /// <remarks/>
        public void SPCJBGCDataAsync(string wo, int isExist, object userState) {
            if ((this.SPCJBGCDataOperationCompleted == null)) {
                this.SPCJBGCDataOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSPCJBGCDataOperationCompleted);
            }
            this.InvokeAsync("SPCJBGCData", new object[] {
                        wo,
                        isExist}, this.SPCJBGCDataOperationCompleted, userState);
        }
        
        private void OnSPCJBGCDataOperationCompleted(object arg) {
            if ((this.SPCJBGCDataCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SPCJBGCDataCompleted(this, new SPCJBGCDataCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SPCLBCPData", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool SPCLBCPData(string wo, int isExist) {
            object[] results = this.Invoke("SPCLBCPData", new object[] {
                        wo,
                        isExist});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void SPCLBCPDataAsync(string wo, int isExist) {
            this.SPCLBCPDataAsync(wo, isExist, null);
        }
        
        /// <remarks/>
        public void SPCLBCPDataAsync(string wo, int isExist, object userState) {
            if ((this.SPCLBCPDataOperationCompleted == null)) {
                this.SPCLBCPDataOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSPCLBCPDataOperationCompleted);
            }
            this.InvokeAsync("SPCLBCPData", new object[] {
                        wo,
                        isExist}, this.SPCLBCPDataOperationCompleted, userState);
        }
        
        private void OnSPCLBCPDataOperationCompleted(object arg) {
            if ((this.SPCLBCPDataCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SPCLBCPDataCompleted(this, new SPCLBCPDataCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SPCCPJYData", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool SPCCPJYData(string wo, int isExist) {
            object[] results = this.Invoke("SPCCPJYData", new object[] {
                        wo,
                        isExist});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void SPCCPJYDataAsync(string wo, int isExist) {
            this.SPCCPJYDataAsync(wo, isExist, null);
        }
        
        /// <remarks/>
        public void SPCCPJYDataAsync(string wo, int isExist, object userState) {
            if ((this.SPCCPJYDataOperationCompleted == null)) {
                this.SPCCPJYDataOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSPCCPJYDataOperationCompleted);
            }
            this.InvokeAsync("SPCCPJYData", new object[] {
                        wo,
                        isExist}, this.SPCCPJYDataOperationCompleted, userState);
        }
        
        private void OnSPCCPJYDataOperationCompleted(object arg) {
            if ((this.SPCCPJYDataCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SPCCPJYDataCompleted(this, new SPCCPJYDataCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SPCDXFXDat", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool SPCDXFXDat(string wo, int isExist) {
            object[] results = this.Invoke("SPCDXFXDat", new object[] {
                        wo,
                        isExist});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void SPCDXFXDatAsync(string wo, int isExist) {
            this.SPCDXFXDatAsync(wo, isExist, null);
        }
        
        /// <remarks/>
        public void SPCDXFXDatAsync(string wo, int isExist, object userState) {
            if ((this.SPCDXFXDatOperationCompleted == null)) {
                this.SPCDXFXDatOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSPCDXFXDatOperationCompleted);
            }
            this.InvokeAsync("SPCDXFXDat", new object[] {
                        wo,
                        isExist}, this.SPCDXFXDatOperationCompleted, userState);
        }
        
        private void OnSPCDXFXDatOperationCompleted(object arg) {
            if ((this.SPCDXFXDatCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SPCDXFXDatCompleted(this, new SPCDXFXDatCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SPCYSSJData", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool SPCYSSJData(string wo) {
            object[] results = this.Invoke("SPCYSSJData", new object[] {
                        wo});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void SPCYSSJDataAsync(string wo) {
            this.SPCYSSJDataAsync(wo, null);
        }
        
        /// <remarks/>
        public void SPCYSSJDataAsync(string wo, object userState) {
            if ((this.SPCYSSJDataOperationCompleted == null)) {
                this.SPCYSSJDataOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSPCYSSJDataOperationCompleted);
            }
            this.InvokeAsync("SPCYSSJData", new object[] {
                        wo}, this.SPCYSSJDataOperationCompleted, userState);
        }
        
        private void OnSPCYSSJDataOperationCompleted(object arg) {
            if ((this.SPCYSSJDataCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SPCYSSJDataCompleted(this, new SPCYSSJDataCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SPCSTData", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool SPCSTData(string wo, string state) {
            object[] results = this.Invoke("SPCSTData", new object[] {
                        wo,
                        state});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void SPCSTDataAsync(string wo, string state) {
            this.SPCSTDataAsync(wo, state, null);
        }
        
        /// <remarks/>
        public void SPCSTDataAsync(string wo, string state, object userState) {
            if ((this.SPCSTDataOperationCompleted == null)) {
                this.SPCSTDataOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSPCSTDataOperationCompleted);
            }
            this.InvokeAsync("SPCSTData", new object[] {
                        wo,
                        state}, this.SPCSTDataOperationCompleted, userState);
        }
        
        private void OnSPCSTDataOperationCompleted(object arg) {
            if ((this.SPCSTDataCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SPCSTDataCompleted(this, new SPCSTDataCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SPCZSLXData", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool SPCZSLXData(string wo) {
            object[] results = this.Invoke("SPCZSLXData", new object[] {
                        wo});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void SPCZSLXDataAsync(string wo) {
            this.SPCZSLXDataAsync(wo, null);
        }
        
        /// <remarks/>
        public void SPCZSLXDataAsync(string wo, object userState) {
            if ((this.SPCZSLXDataOperationCompleted == null)) {
                this.SPCZSLXDataOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSPCZSLXDataOperationCompleted);
            }
            this.InvokeAsync("SPCZSLXData", new object[] {
                        wo}, this.SPCZSLXDataOperationCompleted, userState);
        }
        
        private void OnSPCZSLXDataOperationCompleted(object arg) {
            if ((this.SPCZSLXDataCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SPCZSLXDataCompleted(this, new SPCZSLXDataCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SPCJBWCData", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool SPCJBWCData(string wo) {
            object[] results = this.Invoke("SPCJBWCData", new object[] {
                        wo});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void SPCJBWCDataAsync(string wo) {
            this.SPCJBWCDataAsync(wo, null);
        }
        
        /// <remarks/>
        public void SPCJBWCDataAsync(string wo, object userState) {
            if ((this.SPCJBWCDataOperationCompleted == null)) {
                this.SPCJBWCDataOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSPCJBWCDataOperationCompleted);
            }
            this.InvokeAsync("SPCJBWCData", new object[] {
                        wo}, this.SPCJBWCDataOperationCompleted, userState);
        }
        
        private void OnSPCJBWCDataOperationCompleted(object arg) {
            if ((this.SPCJBWCDataCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SPCJBWCDataCompleted(this, new SPCJBWCDataCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/InsertMonthData", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string InsertMonthData(string month) {
            object[] results = this.Invoke("InsertMonthData", new object[] {
                        month});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void InsertMonthDataAsync(string month) {
            this.InsertMonthDataAsync(month, null);
        }
        
        /// <remarks/>
        public void InsertMonthDataAsync(string month, object userState) {
            if ((this.InsertMonthDataOperationCompleted == null)) {
                this.InsertMonthDataOperationCompleted = new System.Threading.SendOrPostCallback(this.OnInsertMonthDataOperationCompleted);
            }
            this.InvokeAsync("InsertMonthData", new object[] {
                        month}, this.InsertMonthDataOperationCompleted, userState);
        }
        
        private void OnInsertMonthDataOperationCompleted(object arg) {
            if ((this.InsertMonthDataCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.InsertMonthDataCompleted(this, new InsertMonthDataCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")]
    public delegate void SPCDataCollectionCompletedEventHandler(object sender, SPCDataCollectionCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SPCDataCollectionCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SPCDataCollectionCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")]
    public delegate void SPCZSZHDataCompletedEventHandler(object sender, SPCZSZHDataCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SPCZSZHDataCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SPCZSZHDataCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")]
    public delegate void SPCZSGCDataCompletedEventHandler(object sender, SPCZSGCDataCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SPCZSGCDataCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SPCZSGCDataCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")]
    public delegate void SPCJBGCDataCompletedEventHandler(object sender, SPCJBGCDataCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SPCJBGCDataCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SPCJBGCDataCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")]
    public delegate void SPCLBCPDataCompletedEventHandler(object sender, SPCLBCPDataCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SPCLBCPDataCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SPCLBCPDataCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")]
    public delegate void SPCCPJYDataCompletedEventHandler(object sender, SPCCPJYDataCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SPCCPJYDataCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SPCCPJYDataCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")]
    public delegate void SPCDXFXDatCompletedEventHandler(object sender, SPCDXFXDatCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SPCDXFXDatCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SPCDXFXDatCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")]
    public delegate void SPCYSSJDataCompletedEventHandler(object sender, SPCYSSJDataCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SPCYSSJDataCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SPCYSSJDataCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")]
    public delegate void SPCSTDataCompletedEventHandler(object sender, SPCSTDataCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SPCSTDataCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SPCSTDataCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")]
    public delegate void SPCZSLXDataCompletedEventHandler(object sender, SPCZSLXDataCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SPCZSLXDataCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SPCZSLXDataCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")]
    public delegate void SPCJBWCDataCompletedEventHandler(object sender, SPCJBWCDataCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SPCJBWCDataCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SPCJBWCDataCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")]
    public delegate void InsertMonthDataCompletedEventHandler(object sender, InsertMonthDataCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class InsertMonthDataCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal InsertMonthDataCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591