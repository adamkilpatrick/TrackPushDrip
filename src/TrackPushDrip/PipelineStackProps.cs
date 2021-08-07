using Amazon.CDK;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrackPushDrip
{
    public class PipelineStackProps : StackProps
    {

        public string PipelineName { get; init; }

    }
}
