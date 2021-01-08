# FilamentCalculator

This is a smale WebApp to calc the cost of a printed part for 3D-Printers.

You can manage your filaments, manufacturer and more.

The calculator is wrtitten in ASP.NET CORE 3.1 and uses Docker for its deployment. 


Exampl Docker-Compose:

    version: '3.1'

    services:
    db:
        image: postgres
        environment:
        POSTGRES_PASSWORD: example

    web:
        image: devtown.filamentcalc
        build:
        context: .
        dockerfile: Dockerfile
        environment:
        PGHostname: db
        PGDB: filamentcalc
        PGUsername: postgres
        PGPassword: example
        SeedDemoData: J
        ports:
        - "9001:80"
        depends_on:
        - "db"