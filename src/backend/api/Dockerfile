FROM golang:1.17-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM golang:1.17-alpine AS build
WORKDIR /src
COPY  ["./api/", "/src/api/"]
WORKDIR /src/api/

RUN CGO_ENABLED=0 GOOS=linux go build -o main -o /app/main

FROM base AS final
COPY --from=build /app .
CMD ["./main"]
