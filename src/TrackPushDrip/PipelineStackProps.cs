using Amazon.CDK;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrackPushDrip
{
    public class PipelineStackProps : StackProps
    {

        public string PipelineName { get; init; }
        public string GithubOwner { get; init; }
        public string GithubRepo { get; init; }

    }
}
