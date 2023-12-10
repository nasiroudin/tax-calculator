FROM mcr.microsoft.com/mssql/server:2022-latest

USER root

ENV ACCEPT_EULA=Y
ENV MSSQL_SA_PASSWORD=Password!

COPY ./entrypoint.sh /

RUN chmod +x entrypoint.sh

ENTRYPOINT [ "/bin/bash", "entrypoint.sh" ]
CMD [ "/opt/mssql/bin/sqlservr"]
