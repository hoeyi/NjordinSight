#!/bin/bash
/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P $MSSQL_SA_PASSWORD -i init-ozym-db.sql -v OZYM_APP_PASSWORD="$OZYM_APP_PASSWORD" -C