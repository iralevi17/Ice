using System;
using System.Threading;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.IO;
using Amazon.S3.Model;
using Ice.WeatherForecast.General.Interfaces;

namespace Ice.WeatherForecast.Service
{
    public class AwsDataStorage : IDataStorage
    {
        private const string AccessKey = "";
        private const string SecretKey = "";
        private const string LocalStoragePath = @"C:\temp\";

        private readonly IBucketNameIterator _bucketNameIterator;

        public AwsDataStorage(IBucketNameIterator bucketNameGenerator)
        {
            _bucketNameIterator = bucketNameGenerator;
        }

        public async Task<string> DownloadTemperatureFile(DateTime bucketTimeStamp, CancellationToken cancellationToken)
        {
            using (var s3Client = new AmazonS3Client(AccessKey, SecretKey, RegionEndpoint.USEast1))
            {
                foreach (var bucketName in _bucketNameIterator.Next(bucketTimeStamp))
                {
                    var request = new GetObjectRequest
                    {
                        BucketName = $"noaa-gfs-bdp-pds/gfs.{bucketName.timeStamp}/00/atmos",
                        Key = $"gfs.t00z.pgrb2.0p25.f{bucketName.offset}"
                    };

                    var fileName = $"{LocalStoragePath}{request.Key}";

                    var s3FileInfo = new S3FileInfo(s3Client, request.BucketName, request.Key);

                    if (s3FileInfo.Exists)
                    {
                        using (var response = await s3Client.GetObjectAsync(request, cancellationToken))
                        {
                            await response.WriteResponseStreamToFileAsync(fileName, false, cancellationToken);
                        }

                        return fileName;
                    }
                }

                throw new Exception($"Temperature file is not found on AWS data storage according to timeStamp '{bucketTimeStamp}'");
            }
        }
    }
}