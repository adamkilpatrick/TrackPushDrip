using Amazon.CDK;
using Amazon.CDK.AWS.CodeBuild;
using Amazon.CDK.AWS.CodePipeline;
using Amazon.CDK.AWS.CodePipeline.Actions;
using Amazon.CDK.AWS.SSM;
using Amazon.CDK.Pipelines;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrackPushDrip
{
    public class PipelineStack : Stack
    {
        internal PipelineStack(Construct scope, string id, PipelineStackProps props) : base(scope, id, props)
        {
            var sourceArtifact = new Artifact_();
            var cloudAssemblyArtifact = new Artifact_();

            var pipeline = new CdkPipeline(this, "Pipeline", new CdkPipelineProps
            {
                PipelineName = props.PipelineName,
                CloudAssemblyArtifact = cloudAssemblyArtifact,
                SourceAction = new GitHubSourceAction(new GitHubSourceActionProps
                {
                    ActionName = "GitHub",
                    Output = sourceArtifact,
                    OauthToken = SecretValue.SecretsManager("GITHUB_TOKEN"),
                    Trigger = GitHubTrigger.POLL,
                    Owner = StringParameter.ValueForStringParameter(this, "GITHUB_OWNER"),
                    Repo = StringParameter.ValueForStringParameter(this, "GITHUB_REPO"),
                    Branch = "main"
                }),
                SynthAction = SimpleSynthAction.StandardNpmSynth(new StandardNpmSynthOptions
                {
                    SourceArtifact = sourceArtifact,
                    CloudAssemblyArtifact = cloudAssemblyArtifact,
                    EnvironmentVariables = new Dictionary<string, IBuildEnvironmentVariable> { 
                        { "PIPELINE_ACCOUNT", 
                            new BuildEnvironmentVariable() { Value = StringParameter.ValueForStringParameter(this, "PIPELINE_ACCOUNT")} 
                        } 
                    },
                    BuildCommand = "npm run build"
                })
            });
        }
    }
}
