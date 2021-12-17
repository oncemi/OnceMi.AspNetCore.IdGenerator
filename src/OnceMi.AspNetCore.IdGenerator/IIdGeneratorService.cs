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
        long NewId();

        /// <summary>
        /// 生成指定数量的Id
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        long[] NewIds(int count);
    }
}
