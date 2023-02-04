#! /bin/sh

function cleanup()
{
	[ ! -z "$TRY" ] && kill $TRY
	[ ! -z "$XXX" ] && kill $XXX
       
	sleep 1
}

trap 'cleanup' INT TERM

dotnet dev-certs https
DOTNET_TRY_CLI_TELEMETRY_OPTOUT=1 /root/.dotnet/tools/dotnet-try --verbose --port 8080 /elinq/ &
TRY=$!
nginx -g "daemon off;" &
XXX=$!

wait -n

cleanup
