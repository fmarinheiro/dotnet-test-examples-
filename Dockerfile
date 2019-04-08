FROM microsoft/dotnet

ENV HOME=/home/app
ENV APP_HOME=/home/app/my-project
ENV HUID=1000
ENV HGID=1000

RUN mkdir -p $HOME
RUN mkdir $APP_HOME

WORKDIR $APP_HOME

ADD . $APP_HOME

RUN chmod o+x become.sh

ENTRYPOINT ["./become.sh"]