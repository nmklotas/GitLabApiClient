#!/bin/bash

export GITLAB_API_ENDPOINT=http://localhost/api/v4
export export GITLAB_API_PRIVATE_TOKEN=ElijahBaley
configured=false
while [ "$configured" = false ]
do
    gitlab-ctl status >/dev/null
    if [ $? -eq 0 ]; then
        gitlab create_project "txxxestprojecxxxt"
        project_created=$?
        gitlab create_group "txxxestgrouxxxp" "txxxestgrouxxxp"
        group_created=$?
        if [ "$project_created" -eq 0 ]  && [ "$group_created" -eq 0 ]; then
            echo echo "Success test configuration"
            configured=true
        else
            echo "Unable to configure retry in 30s"
            sleep 15
        fi
    else
        echo "Gitlab is not running retry in 30s"
        sleep 15
    fi
done
