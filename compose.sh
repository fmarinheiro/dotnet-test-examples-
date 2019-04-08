#!/bin/bash

export HUID=$(id -u)
export HGID=$(id -g)

docker-compose up --build -d

