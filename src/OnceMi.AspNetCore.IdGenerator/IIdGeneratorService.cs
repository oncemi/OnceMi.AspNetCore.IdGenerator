using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnceMi.AspNetCore.IdGenerator
{
    public interface IIdGeneratorService
    {
        /// <summary>
        /// 生成一个新的Id
        /// </summary>
        /// <returns></returns>
        long CreateId();

        /// <summary>
        /// 尝试生成Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool TryCreateId(out long id);

        /// <summary>
        /// 生成指定个数的Id
        /// </summary>
        /// <returns></returns>
        IEnumerable<long> CreateIds(int count);
    }
}
