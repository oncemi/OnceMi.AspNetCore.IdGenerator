using IdGen;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnceMi.AspNetCore.IdGenerator
{
    public class IdGeneratorOption
    {
        private int _appId = 1;

        public int AppId
        {
            get
            {
                return _appId;
            }
            set
            {
                if (value < 0 || value > 1023)
                {
                    throw new Exception("App id must be between 0 and 1023.");
                }
                _appId = value;
            }
        }

        public IdGeneratorOptions GeneratorOptions { get; set; }
    }
}
