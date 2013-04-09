@echo off
cd solr-4.1.0\adventureWorksInstance
start cmd /c java -Dcom.sun.management.jmxremote -Dcom.sun.management.jmxremote.port=12345 -Dcom.sun.management.jmxremote.authenticate=false -Dcom.sun.management.jmxremote.ssl=false -jar start.jar
cd ..\..
timeout 5
start http://localhost:8983/solr/