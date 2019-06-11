﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.Pipelines.CoveragePublisher.Model;

namespace Microsoft.Azure.Pipelines.CoveragePublisher.Publishers.AzurePipelines
{
    internal class PipelinesExecutionContext: IExecutionContext
    {
        private int buildId = -1;
        private long containerId = -1;
        private string accessToken = null;
        private Guid? projectId = null;

        public ILogger ConsoleLogger { get; private set; }

        public PipelinesExecutionContext()
        {
            ConsoleLogger = new PipelinesLogger();
        }

        public int BuildId
        {
            get
            {
                if (buildId == -1)
                {
                    int.TryParse(Environment.GetEnvironmentVariable(Constants.EnvironmentVariables.BuildId) ?? "", out buildId);
                }
                return buildId;
            }
        }

        public long ContainerId
        {
            get
            {
                if (containerId == -1)
                {
                    long.TryParse(Environment.GetEnvironmentVariable(Constants.EnvironmentVariables.BuildContainerId) ?? "", out containerId);
                }
                return containerId;
            }
        }

        public string AccessToken
        {

            get
            {
                if (accessToken == null)
                {
                    accessToken = Environment.GetEnvironmentVariable(Constants.EnvironmentVariables.AccessToken) ?? "";
                }

                if (string.IsNullOrEmpty(accessToken))
                {
                    throw new ArgumentNullException("AccessToken envrionment variable was null or empty.");
                }
                
                return accessToken;
            }
        }

        public Guid ProjectId
        {
            get
            {
                if (projectId == null)
                {
                    Guid.TryParse(Environment.GetEnvironmentVariable(Constants.EnvironmentVariables.ProjectId) ?? "", out Guid parsedGuid);
                    projectId = parsedGuid;
                }
                return (Guid)projectId;
            }
        }
    }
}