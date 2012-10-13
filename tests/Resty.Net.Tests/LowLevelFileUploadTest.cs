﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using System.IO;
using System.Collections.Specialized;
using System.Net;
using System.Reflection;

namespace Resty.Net.Tests
{
    public class LowLevelFileUploadTest
    {
        Uri _MyUri;
        NancyHostHelper _nancyHostHelper;

        public LowLevelFileUploadTest()
        {
            _nancyHostHelper = new NancyHostHelper();
            _MyUri = _nancyHostHelper.Start();
        }

        ~LowLevelFileUploadTest()
        {
            _nancyHostHelper.Stop();
        }

        [Fact]
        public void Simple()
        {
            string boundary = Guid.NewGuid().ToString();
            RestRequest request = new RestRequest(HttpMethod.POST, new RestUri(_MyUri, "/File"));
            request.ContentType = ContentType.Create(string.Format("multipart/form-data; boundary={0}", boundary));
            var location = AppDomain.CurrentDomain.BaseDirectory;

            string[] files = new string[] 
            { 
                Path.Combine(location, "TextFile1.txt"),
                Path.Combine(location, "TextFile2.txt"),
            };

            IDictionary<string, string> formData = new Dictionary<string, string>
            {
                {"Id", "1"},
                {"Email", "abc@abc.com"},
            };

            StringBuilder sb = new StringBuilder();

            foreach (var file in files)
            {
                sb.AppendFormat("--{0}", boundary);
                sb.AppendFormat("\r\n");
                sb.AppendFormat("Content-Disposition: form-data; name=\"media\"; filename=\"" + Path.GetFileName(file) + "\"");
                sb.AppendFormat("\r\n");
                sb.AppendFormat("Content-Type:  application/octet-stream");
                sb.AppendFormat("\r\n");
                sb.AppendFormat("\r\n");
                using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
                {
                    byte[] contents = new byte[fs.Length];
                    fs.Read(contents, 0, contents.Length);
                    sb.Append(Encoding.Default.GetString(contents));
                }
                sb.AppendFormat("\r\n");
            }

            foreach (var keyvaluepair in formData)
            {
                sb.AppendFormat("--{0}", boundary);
                sb.AppendFormat("\r\n");
                sb.AppendFormat("Content-Disposition: form-data; name=\"" + keyvaluepair.Key + "\"");
                sb.AppendFormat("\r\n");
                sb.AppendFormat("\r\n");
                sb.AppendFormat(keyvaluepair.Value);
                sb.AppendFormat("\r\n");
            }

            sb.AppendFormat("--{0}--", boundary);
            byte[] fulldata = Encoding.Default.GetBytes(sb.ToString());

            var body = new RestRawRequestBody(fulldata);
            request.Body = body;

            var response = request.GetResponse<List<string>>();
            var data = response.Data;

            Assert.Equal(2, data.Count);
            Assert.Equal("Hello world from text file 1!", data[0]);
            Assert.Equal("Hello world from text file 2!", data[1]);
        }

    }
}
