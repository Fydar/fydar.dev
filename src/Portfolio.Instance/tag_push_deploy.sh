
# Login

$(aws ecr get-login --no-include-email --region us-east-1)

# Build

docker build -t portfolioinstance .

# Tag

docker tag portfolioinstance:latest 222779217717.dkr.ecr.us-east-1.amazonaws.com/portfolioinstance:latest

# Deploy

docker push 222779217717.dkr.ecr.us-east-1.amazonaws.com/portfolioinstance:latest
