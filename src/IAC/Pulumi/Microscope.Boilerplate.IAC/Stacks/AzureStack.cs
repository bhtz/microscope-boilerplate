using Pulumi;
using Pulumi.AzureNative.Resources;
using Pulumi.AzureNative.Storage;
using Pulumi.AzureNative.Web;
using Pulumi.AzureNative.Web.Inputs;
using AzureNative = Pulumi.AzureNative;
using SkuArgs = Pulumi.AzureNative.Storage.Inputs.SkuArgs;

namespace Microscope.Boilerplate.IAC.Stacks;

public class AzureStack : Stack
{
    [Output]
    public Output<string> AppServiceUrl { get; set; }
    
    public AzureStack()
    {
        var resourceGroup = new ResourceGroup("mcsp", new ResourceGroupArgs()
        {
            Location = "westeurope"
        });

        var storageAccount = new StorageAccount("sa", new StorageAccountArgs
        {
            ResourceGroupName = resourceGroup.Name,
            Sku = new SkuArgs
            {
                Name = SkuName.Standard_LRS
            },
            Kind = Kind.StorageV2
        });

        var storageAccountKeys = ListStorageAccountKeys.Invoke(new ListStorageAccountKeysInvokeArgs
        {
            ResourceGroupName = resourceGroup.Name,
            AccountName = storageAccount.Name
        });

        var primaryStorageKey = storageAccountKeys.Apply(accountKeys =>
        {
            var firstKey = accountKeys.Keys[0].Value;
            return Output.CreateSecret(firstKey);
        });

        var appServicePlan = new AzureNative.Web.AppServicePlan("plan", new()
        {
            ResourceGroupName = resourceGroup.Name,
            Location = resourceGroup.Location,
            Kind = "Linux",
            Name = "plan",
            Reserved = true,
            Sku = new AzureNative.Web.Inputs.SkuDescriptionArgs
            {
                Tier = "Free",
                Size = "F1",
                Name = "F1",
            }
        });

        var api = new WebApp("API", new WebAppArgs
        {
            ResourceGroupName = resourceGroup.Name,
            ServerFarmId = appServicePlan.Id
        });

        var bff = new WebApp("BFF", new WebAppArgs
        {
            ResourceGroupName = resourceGroup.Name,
            ServerFarmId = appServicePlan.Id
        });

        var keycloak = new WebApp("keycloak", new WebAppArgs
        {
            ResourceGroupName = resourceGroup.Name,
            ServerFarmId = appServicePlan.Id,
            Reserved = true,
            SiteConfig = new SiteConfigArgs
            {
                LinuxFxVersion = "DOCKER|registry.hub.docker.com/jboss/keycloak:latest",
                AppSettings = 
                {
                    new NameValuePairArgs
                    {
                        Name = "WEBSITES_ENABLE_APP_SERVICE_STORAGE",
                        Value = "false",
                    },
                    
                    new NameValuePairArgs
                    {
                        Name = "KEYCLOAK_USER",
                        Value = "admin",
                    },
                
                    new NameValuePairArgs
                    {
                        Name = "KEYCLOAK_PASSWORD",
                        Value = "microscope",
                    },
                }
            },
        });
        
        // var postgresServer = new AzureNative.DBforPostgreSQL.Server("postgresServer", new AzureNative.DBforPostgreSQL.ServerArgs
        // {
        //     ResourceGroupName = resourceGroup.Name,
        //     ServerName = "mcsppg",
        //     Version = "13",
        //     AdministratorLogin = "microscope",
        //     AdministratorLoginPassword = "microscope",
        //     CreateMode = "Create",
        //     Backup = new AzureNative.DBforPostgreSQL.Inputs.BackupArgs
        //     {
        //         BackupRetentionDays = 7,
        //         GeoRedundantBackup = "Disabled",
        //     },
        //     Sku = new AzureNative.DBforPostgreSQL.Inputs.SkuArgs
        //     {
        //         Name = "Standard_B1ms", // This is the Burstable tier
        //         Tier = "Burstable",
        //     },
        // });

        this.AppServiceUrl = bff.DefaultHostName;
    }
}