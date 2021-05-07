using System;
using System.Collections.Generic;
using System.Text;
using Yitter.IdGenerator;

namespace OnceMi.AspNetCore.IdGenerator
{
    public class IdGeneratorOption
    {
        public ushort AppId { get; set; }

        public IdGeneratorOptions GeneratorOptions { get; set; }
    }
}
