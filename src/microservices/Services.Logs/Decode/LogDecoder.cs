using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Services.Logs.Domain;

namespace Services.Logs.Decode
{
    public class LogDecoder : ILogDecoder
    {
        private readonly ILogger<LogDecoder> _logger;

        public LogDecoder(ILogger<LogDecoder> logger)
        {
            _logger = logger;
        }
        public ILog DecodeLog(byte[] data)
        {
            var dataList = data.ToList();
            try
            {
                int index = 0;

                var type = (LogType)Convert.ToByte(data[index]);
                index++;

                var nameLen = dataList.GetRange(index, 2).ToUInt16();
                index+=2;

                var name = dataList.GetRange(index, nameLen).GetString();
                index += nameLen;

                var categoryLen = dataList.GetRange(index, 2).ToUInt16();
                index += 2;

                var category = dataList.GetRange(index, categoryLen).GetString();
                index += categoryLen;

                var messageLen = dataList.GetRange(index, 2).ToUInt16();
                index += 2;

                var message = dataList.GetRange(index, messageLen).GetString();
                index += messageLen;

                var opIdBytes = dataList.GetRange(index, 16);
                index += 16; 

                var userIdBytes = dataList.GetRange(index, 16);

                var userId = new Guid(userIdBytes.ToArray());
                var operationId = new Guid(opIdBytes.ToArray());

                return new Log().Create(name, category, type, message, operationId, userId);
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Log decode failed with reason: {e.Message}");
                return null;
            }
        }
    }
}
