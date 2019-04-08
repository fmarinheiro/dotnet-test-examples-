#!/bin/bash

# Create an app user so our program doesn't run as root.
groupadd -r -g $HGID app
useradd -r -g app -u $HUID -d $HOME -s /sbin/nologin -c "Docker image user" app

# Chown all the files to the app user.
chown -R app:app $APP_HOME ${HOME}

# Change to the app user.
if [ "$1" == "" ];
then
   CMD="su -s /bin/bash app"
else
   CMD="su -s /bin/bash -c \"$@\" app"
fi
eval $CMD