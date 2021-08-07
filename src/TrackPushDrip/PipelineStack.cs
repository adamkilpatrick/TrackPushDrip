using Amazon.CDK;
using Amazon.CDK.AWS.CodePipeline;
using Amazon.CDK.AWS.CodePipeline.Actions;
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
                    Owner = props.GithubOwner,
                    Repo = props.GithubRepo,
                    Branch = "main"
                }),
                SynthAction = SimpleSynthAction.StandardNpmSynth(new StandardNpmSynthOptions
                {
                    SourceArtifact = sourceArtifact,
                    CloudAssemblyArtifact = cloudAssemblyArtifact,
                    BuildCommand = "npm run build"
                })
            });
        }
    }
}
