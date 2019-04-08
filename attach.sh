#!/bin/bash

export HUID=$(id -u)
export HGID=$(id -g)

docker-compose run -e HUID=$(id -u) -e HGIU=$(id -g) -p 8000:5000 web