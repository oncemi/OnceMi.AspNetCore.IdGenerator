using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using Yitter.IdGenerator;

namespace OnceMi.AspNetCore.IdGenerator
{
    public class IdGeneratorService : IIdGeneratorService
    {
        private readonly IIdGenerator _generator;
        private readonly IdGeneratorOption _option;
        private readonly ILogger<IdGeneratorService> _logger;

        public IdGeneratorService(IOptions<IdGeneratorOption> option, ILogger<IdGeneratorService> logger)
        {
            _option = option.Value ?? throw new ArgumentNullException(nameof(IdGeneratorOption));
            _logger = logger ?? throw new ArgumentNullException(nameof(ILogger<IdGeneratorService>));
            //create
            if (_option.GeneratorOptions != null)
            {
                _option.GeneratorOptions.WorkerId = _option.AppId;
            }
            else
            {
                _option.GeneratorOptions = new IdGeneratorOptions()
                {
                    Method = 1,
                    WorkerId = _option.AppId,
                    WorkerIdBitLength = 10,
                    SeqBitLength = 6,
                    TopOverCostCount = 2000,
                    BaseTime = DateTime.Parse("2021-01-01 00:00:00")
                };
            }
            if (_option.GeneratorOptions.WorkerId > Math.Pow(2, _option.GeneratorOptions.WorkerIdBitLength) - 1)
            {
                throw new Exception($"WorkerId must lesss than {Math.Pow(2, _option.GeneratorOptions.WorkerIdBitLength) - 1}");
            }
            if (DateTime.Now < _option.GeneratorOptions.BaseTime)
            {
                throw new Exception("The system time is incorrect.");
            }
            _generator = new DefaultIdGenerator(_option.GeneratorOptions);
        }

        /// <summary>
        /// 生成新的Id
        /// </summary>
        /// <returns></returns>
        public long NewId()
        {
            if (_generator == null)
            {
                throw new Exception("Object is not init.");
            }
            long id = _generator.NewLong();
            return id;
        }

        /// <summary>
        /// 生成指定个数的Id
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public long[] NewIds(int count)
        {
            if (_generator == null)
            {
                throw new Exception("Object is not init.");
            }
            long[] result = new long[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = this.NewId();
            }
            return result;
        }
    }
}
