//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option or rebuild the Visual Studio project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Web.Application.StronglyTypedResourceProxyBuilder", "10.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class PricingCommitteeInput {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal PricingCommitteeInput() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Resources.PricingCommitteeInput", global::System.Reflection.Assembly.Load("App_GlobalResources"));
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Το e-mail του μέλους της Επιτροπής&lt;br /&gt;[έγκυρο e-mail, π.χ. myaccount@myisp.gr ].
        /// </summary>
        internal static string ContactEmail {
            get {
                return ResourceManager.GetString("ContactEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Το Ον/μο του μέλους της Επιτροπής&lt;br /&gt;[ελεύθερο κείμενο, μέχρι 100 χαρακτήρες].
        /// </summary>
        internal static string ContactName {
            get {
                return ResourceManager.GetString("ContactName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Το σταθερό τηλέφωνο του του μέλους της Επιτροπής&lt;br /&gt;[πρέπει να ξεκινάει από 2 και να αποτελείται από 10 αριθμητικούς χαρακτήρες, χωρίς κενό].
        /// </summary>
        internal static string ContactPhone {
            get {
                return ResourceManager.GetString("ContactPhone", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Η ιδιότητα του μέλους της Επιτροπής (π.χ. Επιτροπής Ελέγχου Κοστολόγησης, Επιτροπή Δειγματοληπτικού Ελέγχου και Ενστάσεων)&lt;br /&gt;[ελεύθερο κείμενο, μέχρι 100 χαρακτήρες].
        /// </summary>
        internal static string PricingCommitteeType {
            get {
                return ResourceManager.GetString("PricingCommitteeType", resourceCulture);
            }
        }
    }
}