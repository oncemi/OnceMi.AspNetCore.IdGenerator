using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnceMi.AspNetCore.IdGenerator
{
    public class IdGeneratorService : IIdGeneratorService
    {
        private readonly IdGen.IdGenerator _generator;
        private readonly IdGeneratorOption _option;
        private readonly ILogger<IdGeneratorService> _logger;

        public IdGeneratorService(IOptions<IdGeneratorOption> option, ILogger<IdGeneratorService> logger)
        {
            _option = option.Value ?? throw new ArgumentNullException(nameof(IdGeneratorOption));
            _logger = logger ?? throw new ArgumentNullException(nameof(ILogger<IdGeneratorService>));
            //create
            if (_option.GeneratorOptions != null)
            {
                _generator = new IdGen.IdGenerator(_option.AppId, _option.GeneratorOptions);
            }
            else
            {
                _generator = new IdGen.IdGenerator(_option.AppId);
            }
        }

        public long NewId()
        {
            if (_generator == null)
            {
                throw new Exception("Object is not init.");
            }
            while (true)
            {
                try
                {
                    long id = _generator.CreateId();
                    return id;
                }
                catch (Exception ex)
                {
                    //一个时间周期中，生成的ID超过了序列限制
                    if (!string.IsNullOrEmpty(ex.Message) 
                        && ex.Message.Contains("sequence overflow", StringComparison.OrdinalIgnoreCase))
                    {
                        _logger.LogWarning(ex.Message, ex);
                        continue;
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }

        public List<long> NewIds(int count)
        {
            if (_generator == null)
            {
                throw new Exception("Object is not init.");
            }
            var data = _generator.Take(count);
            if (data.Count() != count)
            {
                throw new Exception($"Create {count} ids failed..");
            }
            return data.ToList();
        }
    }
}
