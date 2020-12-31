using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace OnceMi.AspNetCore.IdGenerator
{
    public static class IdGeneratorExtension
    {
        /// <summary>
        /// 配置默认配置
        /// </summary>
        public static IServiceCollection AddIdGenerator(this IServiceCollection services, Action<IdGeneratorOption> configure)
        {
            services.Configure(configure);
            services.TryAddSingleton<IIdGeneratorService, IdGeneratorService>();
            return services;
        }
    }
}
