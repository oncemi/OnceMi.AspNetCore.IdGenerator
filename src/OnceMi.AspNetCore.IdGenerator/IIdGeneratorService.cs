using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnceMi.AspNetCore.IdGenerator
{
    public interface IIdGeneratorService
    {
        long NewId();

        List<long> NewIds(int count);
    }
}
