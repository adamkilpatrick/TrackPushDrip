using Amazon.CDK;
using Amazon.CDK.AWS.SSM;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TrackPushDrip
{
    sealed class Program
    {
        public static void Main(string[] args)
        {
            var app = new App();
            var defaultEnv = new Amazon.CDK.Environment
            {
                Account = System.Environment.GetEnvironmentVariable("PIPELINE_ACCOUNT"),
                Region = "us-east-1",
            };

            new PipelineStack(app, "PipelineStack", new PipelineStackProps
            {
                PipelineName = "TrackPushDrip",
                Env = defaultEnv,
            });

            app.Synth();
        }
    }
}
