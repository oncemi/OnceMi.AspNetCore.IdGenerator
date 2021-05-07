# OnceMi.AspNetCore.IdGenerator
ASP.NET Core雪花算法分布式ID生成工具。Use https://gitee.com/yitter/idgenerator

# How to use
1、Install OnceMi.AspNetCore.IdGenerator。  
CLI中安装：  
```shell
dotnet add package OnceMi.AspNetCore.IdGenerator
```
Nuget中安装：  
在Nuget包管理器中搜索`OnceMi.AspNetCore.IdGenerator`并安装。  

2、Configuration  
You need to configure IdGenerator in your Startup.cs：

```csharp
services.AddIdGenerator(x =>
{
    x.AppId = 1;   //Between 1-1023
});
```

在使用过程中保证每个IdGenerator一个AppId，Appid范围为1-1023。  

3、Use  
```csharp
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IIdGeneratorService _idGenerator;

    public HomeController(ILogger<HomeController> logger, IIdGeneratorService idGenerator)
    {
        _logger = logger;
        _idGenerator = idGenerator;
    }

    public IActionResult Index()
    {
        List<long> ids = new List<long>();
        for (int i = 0; i < 5; i++)
        {
            ids.Add(_idGenerator.NewId());
        }
        ViewBag.Ids = ids;
        return View();
    }
}
```

### API Reference

##### NewId  
`long NewId();`

生成一个ID。  

##### NewIds  
`List<long> NewIds(int count);`

生成指定个数的ID。  
