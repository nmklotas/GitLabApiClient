#!/usr/bin/env ruby
u = User.first

g = Group.create!(name: 'txxxestgrouxxxp', path: 'txxxestgrouxxxp')
g.add_developer(u)
p = Project.create!(namespace: g, creator: u, path: 'txxxestprojecxxxt', name: 'txxxestprojecxxxt')
p.repository.create_if_not_exists
p.add_maintainer(u)
c = p.repository.create_dir(u, 'newdir', message: 'Create newdir', branch_name: 'master')

t = PersonalAccessToken.new({ user: u, name: 'gitlab-api-client', scopes: ['api']})
t.save!

puts t.token
