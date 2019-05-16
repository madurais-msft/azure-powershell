﻿using Microsoft.Azure.Commands.Blueprint.Common;
using Microsoft.WindowsAzure.Commands.Utilities.Common;
using System.Management.Automation;
using static Microsoft.Azure.Commands.Blueprint.Common.BlueprintConstants;

namespace Microsoft.Azure.Commands.Blueprint.Cmdlets
{
    [Cmdlet("Import", ResourceManager.Common.AzureRMConstants.AzureRMPrefix + "BlueprintWithArtifacts", DefaultParameterSetName = ParameterSetNames.ImportBlueprintParameterSet), OutputType(typeof(string))]
    public class ImportAzureRmBlueprint : BlueprintDefinitionCmdletBase
    {
        #region Parameters
        [Parameter(ParameterSetName = ParameterSetNames.ImportBlueprintParameterSet, Mandatory = true, ValueFromPipelineByPropertyName = true, HelpMessage = ParameterHelpMessages.BlueprintDefinitionName)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        [Parameter(ParameterSetName = ParameterSetNames.ImportBlueprintParameterSet, Mandatory = false, ValueFromPipelineByPropertyName = true, HelpMessage = ParameterHelpMessages.DefinitionSubscriptionId)]
        [ValidateNotNullOrEmpty]
        public string SubscriptionId { get; set; }

        [Parameter(ParameterSetName = ParameterSetNames.ImportBlueprintParameterSet, Mandatory = false, ValueFromPipelineByPropertyName = true, HelpMessage = ParameterHelpMessages.DefinitionManagementGroupId)]
        [ValidateNotNullOrEmpty]
        public string ManagementGroupId { get; set; }

        [Parameter(ParameterSetName = ParameterSetNames.ImportBlueprintParameterSet, Mandatory = true, ValueFromPipelineByPropertyName = true, HelpMessage = ParameterHelpMessages.ImportInputPath)]
        [ValidateNotNullOrEmpty]
        public string InputPath { get; set; }

        [Parameter(ParameterSetName = ParameterSetNames.ImportBlueprintParameterSet, Mandatory = false, HelpMessage = ParameterHelpMessages.ForceHelpMessage)]
        public SwitchParameter Force { get; set; }
        #endregion

        #region Cmdlet Overrides
        public override void ExecuteCmdlet()
        {
            string scope = this.IsParameterBound(c => c.ManagementGroupId) 
                ? Utils.GetScopeForManagementGroup(ManagementGroupId) 
                : Utils.GetScopeForSubscription(SubscriptionId ?? DefaultContext.Subscription.Id);
          
            ImportBlueprint(Name, scope, InputPath, Force);
            ImportArtifacts(Name, scope, InputPath);
        }
        #endregion
    }
}
