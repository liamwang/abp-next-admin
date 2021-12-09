﻿using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace LINGYUN.Abp.WorkflowCore.Persistence.Elasticsearch
{
    [DependsOn(typeof(AbpWorkflowCoreModule))]
    public class AbpWorkflowCorePersistenceElasticsearchModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddTransient<IPersistenceProvider, ElasticsearchPersistenceProvider>();
            context.Services.AddTransient<ElasticsearchPersistenceProvider>();

            PreConfigure<WorkflowOptions>(options =>
            {
                options.UsePersistence(provider => provider.GetRequiredService<ElasticsearchPersistenceProvider>());
            });
        }
    }
}