﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CoordinadoraService.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("https://ws.coordinadora.com/ags/1.5/server.php")]
        public string CoordinadoraService_com_coordinadora_sandbox_despachos_RpcServerSoapManagerService {
            get {
                return ((string)(this["CoordinadoraService_com_coordinadora_sandbox_despachos_RpcServerSoapManagerServic" +
                    "e"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://guias.coordinadora.com/ws/guias/1.6/server.php")]
        public string CoordinadoraService_com_coordinadora_sandbox_guias_JRpcServerSoapManagerService {
            get {
                return ((string)(this["CoordinadoraService_com_coordinadora_sandbox_guias_JRpcServerSoapManagerService"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:/Kiosko/temp/guide/")]
        public string GUIDE_PATH {
            get {
                return ((string)(this["GUIDE_PATH"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:/Kiosko/temp/invoice/")]
        public string INVOICE_PATH {
            get {
                return ((string)(this["INVOICE_PATH"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:/Kiosko/config/")]
        public string PAYMENT_CONFIGURATION_PATH {
            get {
                return ((string)(this["PAYMENT_CONFIGURATION_PATH"]));
            }
        }
    }
}