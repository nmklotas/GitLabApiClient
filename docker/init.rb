#!/usr/bin/env ruby
# Turn off auto devops as it not help for integration testing, causes stuck pipelines.
Gitlab::CurrentSettings.current_application_settings.update!(auto_devops_enabled: false)

u = User.first

g = Group.create!(name: 'txxxestgrouxxxp', path: 'txxxestgrouxxxp')
g.add_developer(u)
p = Project.create!(namespace: g, creator: u, path: 'txxxestprojecxxxt', name: 'txxxestprojecxxxt')
p.repository.create_if_not_exists
p.add_maintainer(u)
content = %{# Test project

Hello world
}
p.repository.create_file(u, 'README.md', content, message: 'Add README.md', branch_name: 'master')
c = p.repository.create_dir(u, 'newdir', message: 'Create newdir', branch_name: 'master')

t = PersonalAccessToken.new({ user: u, name: 'gitlab-api-client', scopes: ['api']})
t.save!

puts t.token
