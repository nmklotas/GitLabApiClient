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
u2 = User.create!(username: 'txxxestusexxxr', password: 'txxxestusexxxr_password', name: 'Txxxest Usexxxr', email: 'txxxestusexxxr@example.com')
t = PersonalAccessToken.new({ user: u, name: 'gitlab-api-client', scopes: ['api']})
t.save!

attributes = {
    description: 'txxxestrunnexxxr',
    active: true,
    locked: false,
    run_untagged: true,
    tag_list: [ 'test' ],
    runner_type: :instance_type
}
r = Ci::Runner::create!(attributes)
attributes = {
    description: 'txxxestrunnexxxr_group',
    active: true,
    locked: false,
    run_untagged: true,
    tag_list: [ 'test' ],
    runner_type: :group_type,
    groups: [ g ]
}
r = Ci::Runner::create!(attributes)
attributes = {
    description: 'txxxestrunnexxxr_project',
    active: true,
    locked: false,
    run_untagged: true,
    tag_list: [ 'test' ],
    runner_type: :project_type,
    projects: [ p ]
}
r = Ci::Runner::create!(attributes)

puts t.token
puts Gitlab::CurrentSettings.current_application_settings.runners_registration_token
