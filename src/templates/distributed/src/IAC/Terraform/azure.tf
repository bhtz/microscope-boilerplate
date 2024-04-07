provider "azurerm" {
  skip_provider_registration = "true"
  features {}
}

# CORE BUILDING BLOCKS RESOURCES
resource "azurerm_resource_group" "microscope_rg" {
  name     = "microscope-rg"
  location = "westeurope" # Change to your desired region
}

resource "azurerm_storage_account" "storage" {
  name                     = "mcspsa"
  resource_group_name      = azurerm_resource_group.microscope_rg.name
  location                 = azurerm_resource_group.microscope_rg.location
  account_tier             = "Standard"
  account_replication_type = "LRS"
}

resource "azurerm_app_service_plan" "app_service_plan" {
  name                = "mcsp-plan"
  location            = azurerm_resource_group.microscope_rg.location
  resource_group_name = azurerm_resource_group.microscope_rg.name
  kind                = "Linux"
  reserved            = true
  sku {
    tier = "Basic"
    size = "B1"
  }
}

resource "azurerm_app_service" "mcsp_iam" {
  name                = "mcspiam"
  location            = azurerm_resource_group.microscope_rg.location
  resource_group_name = azurerm_resource_group.microscope_rg.name
  app_service_plan_id = azurerm_app_service_plan.app_service_plan.id
  app_settings = {
    "WEBSITES_ENABLE_APP_SERVICE_STORAGE" = "false"
  }
  
  site_config {
    always_on = true
    linux_fx_version = "DOCKER|keycloak/keycloak:22.0.0"
  }
}

resource "azurerm_postgresql_flexible_server" "postgres" {
  name                   = "mcsp-bdd"
  location               = azurerm_resource_group.microscope_rg.location
  resource_group_name    = azurerm_resource_group.microscope_rg.name
  administrator_login    = "microscope"
  administrator_password = "microscope"
  sku_name               = "GP_Standard_D2s_v3" # Replace with the desired SKU GP_Standard_D2s_v3
  storage_mb             = 32768     # Replace with the desired storage size
  version                = "13"      # PostgreSQL version
}

resource "azurerm_servicebus_namespace" "servicebus" {
  name                = "mcsp-servicebus"
  location            = azurerm_resource_group.microscope_rg.location
  resource_group_name = azurerm_resource_group.microscope_rg.name
  sku = "Basic"
}

# SOLUTION RESOURCES

resource "azurerm_app_service" "todo_app_service" {
  name                = "mcsp-todoservice"
  location            = azurerm_resource_group.microscope_rg.location
  resource_group_name = azurerm_resource_group.microscope_rg.name
  app_service_plan_id = azurerm_app_service_plan.app_service_plan.id
  site_config {
    linux_fx_version = "DOTNET|7.0"
  }
}

resource "azurerm_app_service" "bff_app_service" {
  name                = "mcsp-bff"
  location            = azurerm_resource_group.microscope_rg.location
  resource_group_name = azurerm_resource_group.microscope_rg.name
  app_service_plan_id = azurerm_app_service_plan.app_service_plan.id
  site_config {
    linux_fx_version = "DOTNET|7.0"
  }
}
