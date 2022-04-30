REM Change working directory to the project direct where IdentityDbContext resides.
cd NjordFinance.Web

REM Check for expected positional parameter and redirect as needed.
if "%1"=="" goto missingparameter

REM Define context migration event.
set migration=IdentityDbContext_%1

REM Add the migration. Reference the IdentityDbContext with fully-qualified namespace.
dotnet ef migrations add %migration% ^
	--context NjordFinance.Web.Data.IdentityDbContext

REM Update the database using the added migration.
dotnet ef database update IdentityDbContext_Initialize ^
	--context NjordFinance.Web.Data.IdentityDbContext ^
	--configuration Debug

REM Remove the migration.
dotnet ef migrations remove

goto scriptexit

REM Write error message to console describing missing parameter.
:missingparameter
echo Missing positional parameter 'event'

:scriptexit

REM Revert the working directory back to the parent.
cd ..\