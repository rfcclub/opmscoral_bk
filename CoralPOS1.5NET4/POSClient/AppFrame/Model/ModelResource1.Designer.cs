﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3053
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppFrame.Model {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ModelResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ModelResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("AppFrame.Model.ModelResource", typeof(ModelResource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid {1} ,{1} must be between {3} and {5} characters.
        /// </summary>
        public static string contactPersonError {
            get {
                return ResourceManager.GetString("contactPersonError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid {1} ,{1} must be between {3} and {5} characters.
        /// </summary>
        public static string customerAddressError {
            get {
                return ResourceManager.GetString("customerAddressError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid {1} ,{1} must be between {3} and {5} characters.
        /// </summary>
        public static string customerNameError {
            get {
                return ResourceManager.GetString("customerNameError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Value must be a number.
        /// </summary>
        public static string numberError {
            get {
                return ResourceManager.GetString("numberError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid {1} ,{1} must be between {3} and {5} characters.
        /// </summary>
        public static string passwordError {
            get {
                return ResourceManager.GetString("passwordError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {1} must be number.
        /// </summary>
        public static string telephoneError {
            get {
                return ResourceManager.GetString("telephoneError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid {1} ,{1} must be between {3} and {5} characters.
        /// </summary>
        public static string usernameError {
            get {
                return ResourceManager.GetString("usernameError", resourceCulture);
            }
        }
    }
}
