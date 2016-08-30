USE SeleniumDemo
GO
CREATE USER [IIS APPPOOL\SeleniumDemoIntegrationTests]
GO
ALTER ROLE db_datareader ADD MEMBER [IIS APPPOOL\SeleniumDemoIntegrationTests]
ALTER ROLE db_datawriter ADD MEMBER [IIS APPPOOL\SeleniumDemoIntegrationTests]
GO
