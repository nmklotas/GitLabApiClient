#!/bin/bash

#update-permissions
updated=false
while [ "$updated" = false ]
do
    gitlab-psql -d gitlabhq_production -c "UPDATE users SET authentication_token = 'ElijahBaley' WHERE username = 'root';" >/dev/null
    if [ $? -eq 0 ]; then
        updated=true
        echo "Success updated root authentication_token"
    else
        echo "Unable to update root authentication_token retry in 30s"
        sleep 15
    fi
done