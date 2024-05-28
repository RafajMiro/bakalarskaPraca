FROM node:20.12.2 AS builder

WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build -- --output-path=dist/angular-project/browser

FROM node:20.12.2

RUN npm install -g http-server
WORKDIR /app
COPY --from=builder /app/dist/angular-project/browser /app/dist
EXPOSE 4200
CMD ["http-server", "/app/dist/browser", "-p", "4200"]
