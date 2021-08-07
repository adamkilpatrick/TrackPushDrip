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
                Account = "PIPELINE_ACCOUNT",
                Region = "us-east-1",
            };

            new PipelineStack(app, "PipelineStack", new PipelineStackProps
            {
                PipelineName = "TrackPushDrip",
                GithubOwner = StringParameter.ValueForStringParameter(app, "GITHUB_OWNER"),
                Env = defaultEnv,
                GithubRepo = StringParameter.ValueForStringParameter(app, "GITHUB_REPO"),
                
            });

            app.Synth();
        }
    }
}
