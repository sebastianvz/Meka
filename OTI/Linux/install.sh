#!/bin/bash
while getopts p:f: option
do
case "${option}"
in
f) FILE=${OPTARG};;
p) FPATH=${OPTARG};;
esac
done	
if [ -z "$FPATH" ]; then
	echo 'usage:  update -f [zip file] -p [install location]'
	exit 1
fi

if [ -z "$FILE" ]; then
	echo 'usage:  update -f [zip file] -p [install location]'
	exit 1
fi

unzip -fo $FILE -d $FPATH

if [ -f $HOME/.local/share/otiKiosk/otiKiosk.cfg ]
then
	echo "Config file was not changes."
else
	cp $FPATH/otiKiosk.cfg $HOME/.local/share/otiKiosk/
fi
