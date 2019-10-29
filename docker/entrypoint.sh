#!/bin/bash

sh ./authentication_token.sh &
sh ./test_setup.sh &
/assets/wrapper
