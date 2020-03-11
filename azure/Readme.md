# Azure Deployment Scripts for OutdoorShop

To deploy the DEV environment, ensure you have connected to Azure with `Az-Connect`
Then run `.\deploy-dev`
The script will ask for a devid, these are typically the developer's initials as each developer will have their own enviroment.

This will create the DEV environment in Azure and write a "User Secrets" file that contains the endpoints for CosmosDb and Storage.

To delete the DEV environment, run `.\delete-dev`