#!/bin/bash
PROJECTDIR=$(pwd)
CONTROLLERDIR=$PROJECTDIR"/Controller"
MODELDIR=$PROJECTDIR"/Model"
METADATADIR=$PROJECTDIR"/Meta-Data"
DLLDIR=$PROJECTDIR"/Controller"
RUNFILE=$CONTROLLERDIR"/RunProject.exe"
TEMPFILE=$METADATADIR"/temp.txt"

RED='\033[0;31m'
GRE='\033[0;32m'
BBLU='\033[1;34m'
NOCOLOR='\033[0m'

if [[ -e "$TEMPFILE" ]]; then
	echo -e "${BBLU}[WJRUNNABLE]${GRE} TEMPFILE EXISTS${NOCOLOR}"
else
	echo -e "${BBLU}[WJRUNNABLE]${GRE} CREATE TEMPFILE${NOCOLOR}"
	cd $METADATADIR
	touch temp.txt
fi
if [[ -e "$RUNFILE" ]]; then
	echo -e "${BBLU}[WJRUNNABLE]${GRE} START REMOVING OLD FILE${NOCOLOR}"
	rm $RUNFILE
	echo -e "${BBLU}[WJRUNNABLE]${GRE} END REMOVING OLD FILE${NOCOLOR}"
fi
echo -e "${BBLU}[WJRUNNABLE]${GRE} START BUILDING${NOCOLOR}"
mcs -r:$DLLDIR"/Newtonsoft.Json.dll" -r:$DLLDIR"/MySql.Data.dll" -r:System.Data.dll -r:System.dll -r:Mono.Security.dll -o $RUNFILE $CONTROLLERDIR"/TGServer-Manual.cs" $CONTROLLERDIR"/*.cs" $MODELDIR"/*.cs"
echo -e "${BBLU}[WJRUNNABLE]${GRE} END BUILDING${NOCOLOR}"
if [[ -e "$RUNFILE" ]]; then
	echo -e "${BBLU}[WJRUNNABLE]${GRE} START RUNNING${NOCOLOR}"
	echo ""
	mono $RUNFILE $PROJECTDIR
	echo ""
else
	echo -e "${BBLU}[WJRUNNABLE]${RED} END BUILDING - ERROR OCCURED${NOCOLOR}"
fi
