﻿//-------------------------------------------------------------------------------
// <copyright>
//    Copyright (c) 2012 Nripendra Nath Newa (nripendra@uba-solutions.com).
//    Licensed under the MIT License (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//      http://www.opensource.org/licenses/mit-license.php
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
// </copyright>
// <author>Nripendra Nath Newa (nripendra@uba-solutions.com).</author>
//-------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Resty.Net
{
    public class ContentType
    {
        /// <summary>
        /// Gets and sets the actual string representation of the content type
        /// </summary>
        protected string ContentTypeString { get; set; }

        /// <summary>
        /// application/json
        /// </summary>
        public static ContentType ApplicationJson { get { return new ContentType("application/json"); } }

        /// <summary>
        /// application/octet-stream
        /// </summary>
        public static ContentType ApplicationOctetStream { get { return new ContentType("application/octet-stream"); } }

        /// <summary>
        /// application/x-json
        /// </summary>
        public static ContentType ApplicationXJson { get { return new ContentType("application/x-json"); } }

        /// <summary>
        /// application/xml
        /// </summary>
        public static ContentType ApplicationXml { get { return new ContentType("application/xml"); } }

        /// <summary>
        /// application/x-www-form-urlencoded
        /// </summary>
        public static ContentType ApplicationX_WWW_Form_UrlEncoded { get { return new ContentType("application/x-www-form-urlencoded"); } }

        /// <summary>
        /// multipart/form-data
        /// </summary>
        public static MultiPartFormData MultiPartFormData { get { return Resty.Net.MultiPartFormData.FromString(); } }

        /// <summary>
        /// text/json
        /// </summary>
        public static ContentType TextJson { get { return new ContentType("text/json"); } }

        /// <summary>
        /// text/xml
        /// </summary>
        public static ContentType TextXml { get { return new ContentType("text/xml"); } }

        protected ContentType(string contentType)
        {
            if (contentType == null)
                throw new ArgumentNullException();

            if (string.IsNullOrWhiteSpace(contentType))
                throw new ArgumentException();

            ContentTypeString = contentType;
        }

        /// <summary>
        /// Create a custom content type.
        /// </summary>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static ContentType FromString(string contentType)
        {
            return new ContentType(contentType);
        }

        /// <summary>
        /// Returns a System.String that represents the current ContentType.
        /// </summary>
        /// <returns>A System.String that represents the current ContentType.</returns>
        public override string ToString()
        {
            return ContentTypeString;
        }
    }
}
