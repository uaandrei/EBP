﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18010
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DomainModel.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DomainModel.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to Bid sum must be greater than 1..
        /// </summary>
        public static string BidSumMustBeGreaterThan1Message {
            get {
                return ResourceManager.GetString("BidSumMustBeGreaterThan1Message", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Bid must be greater than product starting price..
        /// </summary>
        public static string BidSumMustBeGreaterThanProductStartingPrice {
            get {
                return ResourceManager.GetString("BidSumMustBeGreaterThanProductStartingPrice", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Bid sum must be smaller than 999999..
        /// </summary>
        public static string BidSumMustBeSmallerThan999999Message {
            get {
                return ResourceManager.GetString("BidSumMustBeSmallerThan999999Message", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The product bid must be greater than the last bid..
        /// </summary>
        public static string NewProductBidMustBeGreaterThanLastOneMessage {
            get {
                return ResourceManager.GetString("NewProductBidMustBeGreaterThanLastOneMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Product bidding end date can&apos;t be set in the past..
        /// </summary>
        public static string ProductEndDateCantBeSetInPastTimeMessage {
            get {
                return ResourceManager.GetString("ProductEndDateCantBeSetInPastTimeMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Product bidding end date must be greater than start date..
        /// </summary>
        public static string ProductEndDateMustBeGreaterThanStartDateMessage {
            get {
                return ResourceManager.GetString("ProductEndDateMustBeGreaterThanStartDateMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Product bidding start date can&apos;t be set in the past..
        /// </summary>
        public static string ProductStartDateCantBeSetInPastTimeMessage {
            get {
                return ResourceManager.GetString("ProductStartDateCantBeSetInPastTimeMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Product starting price must be greater than 0..
        /// </summary>
        public static string ProductStartingPriceMustBeGreaterThanZeroMessage {
            get {
                return ResourceManager.GetString("ProductStartingPriceMustBeGreaterThanZeroMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Product starting price must be lower than 1000000..
        /// </summary>
        public static string ProductStartingPriceMustBeLowerThan1000000Message {
            get {
                return ResourceManager.GetString("ProductStartingPriceMustBeLowerThan1000000Message", resourceCulture);
            }
        }
    }
}
