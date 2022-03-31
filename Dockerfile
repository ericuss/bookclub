FROM golang:1.17-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM golang:1.17-alpine AS gobuild
WORKDIR /src/api/
COPY  ["./src/api/go.mod", "go.mod"]
COPY  ["./src/api/go.sum", "go.sum"]
RUN go mod tidy

COPY  ["./src/api/", "."]

RUN CGO_ENABLED=0 GOOS=linux go build -o main -o /app/main

# build environment
FROM node:16-alpine as nodebuild
WORKDIR /app
# needs to be refactored
COPY ./src/web/ .
# ENV BUILD_PATH=public
RUN yarn
RUN yarn build

FROM base AS final
COPY --from=gobuild /app .
COPY --from=nodebuild /app/build ./public
CMD ["./main"]