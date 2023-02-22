﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VaniAPIsHandler.ImageResize {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ImageResize.ImagesResizeSoap")]
    public interface ImagesResizeSoap {
        
        // CODEGEN: Generating message contract since element name url from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GenerateAvatarManual", ReplyAction="*")]
        VaniAPIsHandler.ImageResize.GenerateAvatarManualResponse GenerateAvatarManual(VaniAPIsHandler.ImageResize.GenerateAvatarManualRequest request);
        
        // CODEGEN: Generating message contract since element name url from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GenerateAvatarThumnail", ReplyAction="*")]
        VaniAPIsHandler.ImageResize.GenerateAvatarThumnailResponse GenerateAvatarThumnail(VaniAPIsHandler.ImageResize.GenerateAvatarThumnailRequest request);
        
        // CODEGEN: Generating message contract since element name url from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GenerateMobileThumnail", ReplyAction="*")]
        VaniAPIsHandler.ImageResize.GenerateMobileThumnailResponse GenerateMobileThumnail(VaniAPIsHandler.ImageResize.GenerateMobileThumnailRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GenerateAvatarManualRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GenerateAvatarManual", Namespace="http://tempuri.org/", Order=0)]
        public VaniAPIsHandler.ImageResize.GenerateAvatarManualRequestBody Body;
        
        public GenerateAvatarManualRequest() {
        }
        
        public GenerateAvatarManualRequest(VaniAPIsHandler.ImageResize.GenerateAvatarManualRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GenerateAvatarManualRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string url;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=1)]
        public int width;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public int height;
        
        public GenerateAvatarManualRequestBody() {
        }
        
        public GenerateAvatarManualRequestBody(string url, int width, int height) {
            this.url = url;
            this.width = width;
            this.height = height;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GenerateAvatarManualResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GenerateAvatarManualResponse", Namespace="http://tempuri.org/", Order=0)]
        public VaniAPIsHandler.ImageResize.GenerateAvatarManualResponseBody Body;
        
        public GenerateAvatarManualResponse() {
        }
        
        public GenerateAvatarManualResponse(VaniAPIsHandler.ImageResize.GenerateAvatarManualResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class GenerateAvatarManualResponseBody {
        
        public GenerateAvatarManualResponseBody() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GenerateAvatarThumnailRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GenerateAvatarThumnail", Namespace="http://tempuri.org/", Order=0)]
        public VaniAPIsHandler.ImageResize.GenerateAvatarThumnailRequestBody Body;
        
        public GenerateAvatarThumnailRequest() {
        }
        
        public GenerateAvatarThumnailRequest(VaniAPIsHandler.ImageResize.GenerateAvatarThumnailRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GenerateAvatarThumnailRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string url;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=1)]
        public int width;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public int height;
        
        public GenerateAvatarThumnailRequestBody() {
        }
        
        public GenerateAvatarThumnailRequestBody(string url, int width, int height) {
            this.url = url;
            this.width = width;
            this.height = height;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GenerateAvatarThumnailResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GenerateAvatarThumnailResponse", Namespace="http://tempuri.org/", Order=0)]
        public VaniAPIsHandler.ImageResize.GenerateAvatarThumnailResponseBody Body;
        
        public GenerateAvatarThumnailResponse() {
        }
        
        public GenerateAvatarThumnailResponse(VaniAPIsHandler.ImageResize.GenerateAvatarThumnailResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class GenerateAvatarThumnailResponseBody {
        
        public GenerateAvatarThumnailResponseBody() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GenerateMobileThumnailRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GenerateMobileThumnail", Namespace="http://tempuri.org/", Order=0)]
        public VaniAPIsHandler.ImageResize.GenerateMobileThumnailRequestBody Body;
        
        public GenerateMobileThumnailRequest() {
        }
        
        public GenerateMobileThumnailRequest(VaniAPIsHandler.ImageResize.GenerateMobileThumnailRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GenerateMobileThumnailRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string url;
        
        public GenerateMobileThumnailRequestBody() {
        }
        
        public GenerateMobileThumnailRequestBody(string url) {
            this.url = url;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GenerateMobileThumnailResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GenerateMobileThumnailResponse", Namespace="http://tempuri.org/", Order=0)]
        public VaniAPIsHandler.ImageResize.GenerateMobileThumnailResponseBody Body;
        
        public GenerateMobileThumnailResponse() {
        }
        
        public GenerateMobileThumnailResponse(VaniAPIsHandler.ImageResize.GenerateMobileThumnailResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class GenerateMobileThumnailResponseBody {
        
        public GenerateMobileThumnailResponseBody() {
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ImagesResizeSoapChannel : VaniAPIsHandler.ImageResize.ImagesResizeSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ImagesResizeSoapClient : System.ServiceModel.ClientBase<VaniAPIsHandler.ImageResize.ImagesResizeSoap>, VaniAPIsHandler.ImageResize.ImagesResizeSoap {
        
        public ImagesResizeSoapClient() {
        }
        
        public ImagesResizeSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ImagesResizeSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ImagesResizeSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ImagesResizeSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        VaniAPIsHandler.ImageResize.GenerateAvatarManualResponse VaniAPIsHandler.ImageResize.ImagesResizeSoap.GenerateAvatarManual(VaniAPIsHandler.ImageResize.GenerateAvatarManualRequest request) {
            return base.Channel.GenerateAvatarManual(request);
        }
        
        public void GenerateAvatarManual(string url, int width, int height) {
            VaniAPIsHandler.ImageResize.GenerateAvatarManualRequest inValue = new VaniAPIsHandler.ImageResize.GenerateAvatarManualRequest();
            inValue.Body = new VaniAPIsHandler.ImageResize.GenerateAvatarManualRequestBody();
            inValue.Body.url = url;
            inValue.Body.width = width;
            inValue.Body.height = height;
            VaniAPIsHandler.ImageResize.GenerateAvatarManualResponse retVal = ((VaniAPIsHandler.ImageResize.ImagesResizeSoap)(this)).GenerateAvatarManual(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        VaniAPIsHandler.ImageResize.GenerateAvatarThumnailResponse VaniAPIsHandler.ImageResize.ImagesResizeSoap.GenerateAvatarThumnail(VaniAPIsHandler.ImageResize.GenerateAvatarThumnailRequest request) {
            return base.Channel.GenerateAvatarThumnail(request);
        }
        
        public void GenerateAvatarThumnail(string url, int width, int height) {
            VaniAPIsHandler.ImageResize.GenerateAvatarThumnailRequest inValue = new VaniAPIsHandler.ImageResize.GenerateAvatarThumnailRequest();
            inValue.Body = new VaniAPIsHandler.ImageResize.GenerateAvatarThumnailRequestBody();
            inValue.Body.url = url;
            inValue.Body.width = width;
            inValue.Body.height = height;
            VaniAPIsHandler.ImageResize.GenerateAvatarThumnailResponse retVal = ((VaniAPIsHandler.ImageResize.ImagesResizeSoap)(this)).GenerateAvatarThumnail(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        VaniAPIsHandler.ImageResize.GenerateMobileThumnailResponse VaniAPIsHandler.ImageResize.ImagesResizeSoap.GenerateMobileThumnail(VaniAPIsHandler.ImageResize.GenerateMobileThumnailRequest request) {
            return base.Channel.GenerateMobileThumnail(request);
        }
        
        public void GenerateMobileThumnail(string url) {
            VaniAPIsHandler.ImageResize.GenerateMobileThumnailRequest inValue = new VaniAPIsHandler.ImageResize.GenerateMobileThumnailRequest();
            inValue.Body = new VaniAPIsHandler.ImageResize.GenerateMobileThumnailRequestBody();
            inValue.Body.url = url;
            VaniAPIsHandler.ImageResize.GenerateMobileThumnailResponse retVal = ((VaniAPIsHandler.ImageResize.ImagesResizeSoap)(this)).GenerateMobileThumnail(inValue);
        }
    }
}