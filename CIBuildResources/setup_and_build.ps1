## Run this script from the top level checked out directory 

$website_dir = "C:\inetpub\SeleniumDemoIntegrationTests"

# remove old test resultfiles from the workspace so they dont fill the disk
If (Test-Path *.trx){
	rm *.trx
}


#Remove old test results from the TestResults directory as well
If (Test-Path TestResults){
	rm -r TestResults
}

#remove the contents of the IIS directory to make sure things dont get confused between builds
If (Test-Path $website_dir){
	rm -r "$website_dir\*"
}

#Drop the database            
sqlcmd -i "C:\ProgramData\Jenkins\jobs\Jenkins example Selenium Project\workspace\CIBuildResources\delete_database.sql"

#Put DB.config files in the right places so the project will build
cp "CIBuildResources\DB.config" "SeleniumDemo"
cp "CIBuildResources\DB.config" "SeleniumDemo.IntegrationTests\bin\Debug"
cp "packages\EntityFramework.6.1.1\tools\migrate.exe" "SeleniumDemo\bin"

# Build the solution
C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe "SeleniumDemo.sln"

# Migrate (i.e. create) the database (run from bin folder)
cd SeleniumDemo\bin
.\migrate.exe "SeleniumDemo.dll" /startupConfigurationFile="..\web.config"
cd ..\..\

# Update the database to allow the IIS Application Pool account to perform actions
sqlcmd -i "C:\ProgramData\Jenkins\jobs\Jenkins example Selenium Project\workspace\CIBuildResources\database_permissions.sql"

#Copy the files for the website into the correct location for iis
cp -r "SeleniumDemo\*" $website_dir

#At this point navigating to localhost:portnumber should give you a working website and the test project should be compiled