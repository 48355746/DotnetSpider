﻿using DotnetSpider.Broker.Controllers;
using DotnetSpider.Broker.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DotnetSpider.Broker.Test
{
	public class BaseTest
	{
		private readonly IServiceCollection _service = new ServiceCollection();

		public IServiceProvider Services;

		protected virtual void Init(BrokerOptions options)
		{
			_service.AddSingleton(options);
			switch (options.StorageType)
			{
				case StorageType.MySql:
					{
						_service.AddScoped<INodeService, Services.MySql.NodeService>();
						_service.AddScoped<IRunningService, Services.MySql.RunningService>();
						_service.AddScoped<IBlockService, Services.MySql.BlockService>();
						_service.AddScoped<IRunningHistoryService, Services.MySql.RunningHistoryService>();
						_service.AddSingleton<IRequestQueueService, Services.MySql.RequestQueueService>();
						break;
					}
				case StorageType.SqlServer:
					{
						_service.AddScoped<INodeService, Services.NodeService>();
						_service.AddScoped<IRunningService, Services.RunningService>();
						_service.AddScoped<IBlockService, Services.BlockService>();
						_service.AddScoped<IRunningHistoryService, Services.RunningHistoryService>();
						_service.AddScoped<IRunningHistoryService, Services.RunningHistoryService>();
						_service.AddSingleton<IRequestQueueService, Services.RequestQueueService>();
						break;
					}
			}
			_service.AddLogging();
			Services = _service.BuildServiceProvider();
		}
	}
}
