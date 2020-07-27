# FilamentCalculator

This is a smale WebApp to calc the cost of a printed part with a 3D-Printer.  

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