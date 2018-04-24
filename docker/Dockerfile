FROM gitlab/gitlab-ce:10.1.4-ce.0

RUN gem install gitlab -v 4.2.0

ADD entrypoint.sh /entrypoint.sh
ADD test_setup.sh /test_setup.sh
ADD authentication_token.sh /authentication_token.sh
RUN chmod -v +x /entrypoint.sh
RUN chmod -v +x /test_setup.sh
RUN chmod -v +x /authentication_token.sh

RUN echo "export GITLAB_API_ENDPOINT=http://localhost/api/v4" >> /etc/profile
RUN echo "export export GITLAB_API_PRIVATE_TOKEN=ElijahBaley" >> /etc/profile

EXPOSE 443 80 22 5432
CMD ["/entrypoint.sh"]
