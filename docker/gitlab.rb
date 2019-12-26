#!/usr/bin/env ruby

prometheus['enable'] = false
prometheus_monitoring['enable'] = false
alertmanager['enable'] = false
grafana['enable'] = false
gitlab_rails['initial_root_password'] = 'password'
