﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Azure.Active.DirectoryB2C.WebApp.Common
{
    /// <summary>
    /// appsettings.json操作类
    /// </summary>
    public class Appsettings
    {
        static IConfiguration Configuration { get; set; }

        static string contentPath { get; set; }

        public Appsettings(string contentPath)
        {
            string Path = "appsettings.json";


            //如果你把配置文件 是 根据环境变量来分开了，可以这样写
            {
                //string Path = $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json";
            }


            //Configuration = new ConfigurationBuilder()
            //.Add(new JsonConfigurationSource { Path = Path, ReloadOnChange = true })//请注意要把当前appsetting.json 文件->右键->属性->复制到输出目录->始终复制
            //.Build();


            //var contentPath = env.ContentRootPath;
            Configuration = new ConfigurationBuilder()
               .SetBasePath(contentPath)
               .Add(new JsonConfigurationSource { Path = Path, Optional = false, ReloadOnChange = true })//这样的话，可以直接读目录里的json文件，而不是 bin 文件夹下的，所以不用修改复制属性
               .Build();


        }

        /// <summary>
        /// 封装要操作的字符
        /// </summary>
        /// <param name="sections"></param>
        /// <returns></returns>
        public static string app(params string[] sections)
        {
            try
            {
                var val = string.Empty;
                for (int i = 0; i < sections.Length; i++)
                {
                    val += sections[i] + ":";
                }

                return Configuration[val.TrimEnd(':')];
            }
            catch (Exception)
            {
                return "";
            }

        }
    }
}
