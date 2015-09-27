﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using GOG.Interfaces;

using System.Net.Http;

namespace GOG.SharedControllers
{
    public class NetworkController :
        IFileRequestController,
        IStringNetworkController
    {
        private HttpClient client;
        private IUriController uriController;

        public NetworkController(IUriController uriController)
        {
            client = new HttpClient();
            this.uriController = uriController;
        }

        public async Task RequestFile(
            string fromUri,
            string toPath,
            IStreamWritableController streamWriteableController,
            IFileController fileController = null,
            IProgress<double> progress = null)
        {
            using (var response = await client.GetAsync(fromUri,
                HttpCompletionOption.ResponseHeadersRead))
            {
                if (!response.IsSuccessStatusCode) return;
                var totalBytes = response.Content.Headers.ContentLength;
                var requestUri = response.RequestMessage.RequestUri;
                var filename = requestUri.Segments[requestUri.Segments.Length - 1];

                var fullPath = Path.Combine(toPath, filename);

                int bufferSize = 1024 * 1024; // 1M
                byte[] buffer = new byte[bufferSize];
                int bytesRead = 0;
                long totalBytesRead = 0;

                if (fileController != null &&
                    fileController.ExistsFile(fullPath) &&
                    fileController.GetSize(fullPath) == totalBytes)
                {
                    // file already exists and has same length - assume it's downloaded
                    if (progress != null) progress.Report(1);
                    return;
                }

                using (Stream writeableStream = streamWriteableController.OpenWritable(fullPath))
                using (Stream responseStream = await response.Content.ReadAsStreamAsync())
                {
                    while ((bytesRead = await responseStream.ReadAsync(buffer, 0, bufferSize)) > 0)
                    {
                        totalBytesRead += bytesRead;
                        await writeableStream.WriteAsync(buffer, 0, bytesRead);
                        if (progress != null)
                        {
                            var percent = totalBytesRead / (double)totalBytes;
                            progress.Report(percent);
                        }
                    }
                }
            }

        }

        public async Task<string> GetString(
            string baseUri,
            IDictionary<string, string> parameters = null)
        {
            string uri = uriController.CombineUri(baseUri, parameters);

            using (var response = await client.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead))
            {
                if (response == null) return null;

                using (Stream stream = await response.Content.ReadAsStreamAsync())
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    return await reader.ReadToEndAsync();
            }
        }

        public async Task<string> PostString(
            string baseUri,
            IDictionary<string, string> parameters = null,
            string data = null)
        {
            string uri = uriController.CombineUri(baseUri, parameters);

            var content = new StringContent(data, Encoding.UTF8, "application/x-www-form-urlencoded");

                using (var response = await client.PostAsync(uri, content))
            {
                if (response == null) return null;

                using (Stream stream = await response.Content.ReadAsStreamAsync())
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    return await reader.ReadToEndAsync();
            }
        }
    }
}