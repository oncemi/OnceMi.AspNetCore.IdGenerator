using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections;
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
                    WorkerIdBitLength = 6,
                    SeqBitLength = 6,
                    TopOverCostCount = 2000,
                    BaseTime = new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                };
            }
            if (_option.GeneratorOptions.WorkerId > Math.Pow(2, _option.GeneratorOptions.WorkerIdBitLength) - 1)
            {
                throw new Exception($"App Id must be between 0-{Math.Pow(2, _option.GeneratorOptions.WorkerIdBitLength) - 1}.");
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
        public long CreateId()
        {
            if (_generator == null)
            {
                throw new Exception("Object is not init.");
            }
            long id = _generator.NewLong();
            return id;
        }

        /// <summary>
        /// 尝试生成Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool TryCreateId(out long id)
        {
            id = 0;
            try
            {
                id = CreateId();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<long> CreateIds(int count)
        {
            return Stream().Take(count);
        }

        private IEnumerable<long> Stream()
        {
            while (true)
            {
                yield return CreateId();
            }
        }
    }
}
