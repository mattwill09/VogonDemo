using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Amazon.Lambda.S3Events;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using FileHelpers;
using FileProcessor.Models;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace FileProcessor
{
    public class Function
    {
        IAmazonS3 S3Client { get; set; }

        /// <summary>
        /// Default constructor. This constructor is used by Lambda to construct the instance. When invoked in a Lambda environment
        /// the AWS credentials will come from the IAM role associated with the function and the AWS region will be set to the
        /// region the Lambda function is executed in.
        /// </summary>
        public Function()
        {
            S3Client = new AmazonS3Client();
        }

        /// <summary>
        /// Constructs an instance with a preconfigured S3 client. This can be used for testing the outside of the Lambda environment.
        /// </summary>
        /// <param name="s3Client"></param>
        public Function(IAmazonS3 s3Client)
        {
            this.S3Client = s3Client;
        }
        
        /// <summary>
        /// This method is called for every Lambda invocation. This method takes in an S3 event object and can be used 
        /// to respond to S3 notifications.
        /// </summary>
        /// <param name="evnt"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<string> FunctionHandler(S3Event evnt, ILambdaContext context)
        {
            var s3Event = evnt.Records?[0].S3;
            if(s3Event == null)
            {
                return null;
            }

            context.Logger.LogLine($"Received event: {JsonConvert.SerializeObject(evnt)}");

            try
            {
                var response = await this.S3Client.GetObjectMetadataAsync(s3Event.Bucket.Name, s3Event.Object.Key);

                try
                {
                    var file = this.S3Client.GetObjectStreamAsync(s3Event.Bucket.Name, s3Event.Object.Key, null).Result;
                    var engine = new FileHelperEngine<FedACHRecord>();
                    var result = engine.ReadStream(new StreamReader(file));
                    context.Logger.LogLine($"Result count: {result.Length}");

                    var resultAsJson = JsonConvert.SerializeObject(result);
                    var request = new PutObjectRequest
                    {
                        BucketName = "dplrtstorage",
                        Key = "data.json",
                        ContentType = "text/json",
                        ContentBody = resultAsJson
                    };

                    await this.S3Client.PutObjectAsync(request);
                }
                catch (Exception e)
                {
                    context.Logger.LogLine($"Exception: {e}");
                }
                return response.Headers.ContentType;
            }
            catch(Exception e)
            {
                context.Logger.LogLine($"Error getting object {s3Event.Object.Key} from bucket {s3Event.Bucket.Name}. Make sure they exist and your bucket is in the same region as this function.");
                context.Logger.LogLine(e.Message);
                context.Logger.LogLine(e.StackTrace);
                throw;
            }
        }
    }
}
