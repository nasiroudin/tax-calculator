#!/bin/bash

set -e

if [ "$1" = '/opt/mssql/bin/sqlservr' ]; then
  # If this is the container's first run, initialize the database
  if [ ! -f /tmp/app-initialized ]; then
    # Initialize the database asynchronously in a background process. This allows 
    # a) the SQL Server process to be the main process in the container, which allows graceful shutdown and other goodies, and 
    # b) us to only start the SQL Server process once, as opposed to starting, stopping, then starting it again.
    function initialize_database() {
      # Wait a bit for SQL Server to start. SQL Server's process doesn't provide a clever way to check if it's up or not, 
      # and it needs to be up before we can import the application database
      sleep 15s

      # Run the setup script to create the DB and the schema in the DB
      /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Password! -d master -i scripts/tax_db_init.sql
      
      # Seed the databases
      /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Password! -d master -i scripts/tax_db_seed.sql

      # Note that the container has been initialized so future starts won't wipe changes to the data
      touch /tmp/app-initialized
    }
    initialize_database &
  fi
fi

exec "$@"