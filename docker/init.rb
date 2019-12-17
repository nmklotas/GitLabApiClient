#!/usr/bin/env ruby
u = User.first

g = Group.create!(name: 'txxxestgrouxxxp', path: 'txxxestgrouxxxp')
p = Project.create!(namespace: g, creator: u, path: 'txxxestprojecxxxt', name: 'txxxestprojecxxxt')
p.repository.create_if_not_exists
c = p.repository.create_dir(u, 'newdir', message: 'Create newdir', branch_name: 'master')

t = PersonalAccessToken.new({ user: u, name: 'gitlab-api-client', scopes: ['api']})
t.save!

puts t.token
