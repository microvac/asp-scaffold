using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Controllers
{
    public enum TSFlag
    {
        /// <summary>
        /// Default
        /// </summary>
        None,
        /// <summary>
        /// Type for a class to become JSonData
        /// </summary>
        JSonData,
        /// <summary>
        /// Type for a class to become a server proxy, look at the <see cref="HttpGet"/>, <see cref="HttpPost"/> and <see cref="HttpDelete"/> attribute
        /// to determine the type of ajax call for each method.
        /// </summary>
        ServerProxy,
        /// <summary>
        /// Do not process that member
        /// </summary>
        Ignore,
    }

    public class ToTSAttribute : Attribute
    {
        public ToTSAttribute(TSFlag type = TSFlag.None)
        {
            Flags = type;
        }
        /// <summary>
        /// For tagging class, whether they are rewrote as data class or ServerProxy
        /// </summary>
        public TSFlag Flags { get; set; }

        /// <summary>
        /// For Controller(Proxy) method, specify the return type of the JSON data (as opposed to <c>JsonResult</c>).
        /// </summary>
        public string ReturnType { get; set; }
    }
}