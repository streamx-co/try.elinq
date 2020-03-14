#! /bin/sh

function cleanup()
{
	[ ! -z "$TRY" ] && kill $TRY
	[ ! -z "$XXX" ] && kill $XXX
       
	sleep 1
}

trap 'cleanup' INT TERM

/root/.dotnet/tools/dotnet-try /xlinq/ --port 8080 &
TRY=$!
nginx -g "daemon off;" &
XXX=$!

wait -n

cleanup
