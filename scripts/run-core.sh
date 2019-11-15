PREFIX=Services
cd ./../Backend/
RUN="dotnet run --no-restore"

Services=($PREFIX.Operations $PREFIX.Identity Api.Gateway $PREFIX.Push Prototype/Services.Test)

for service in ${Services[*]} 
do
    echo Starting Service $service
    cd $service
    $RUN
    cd ..    
done

for path in 