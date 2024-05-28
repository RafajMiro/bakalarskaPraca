# FROM node:20.12.2

# WORKDIR /app
# COPY package*.json ./

# RUN npm install

# COPY . .

# RUN npm run build
# RUN npm install -g http-server

# EXPOSE 4252

# CMD ["http-server", "dist/angular-project", "-p", "4252"]

# FROM node:20.12.2 AS builder

# WORKDIR /app
# COPY package*.json ./
# RUN npm install

# COPY . .
# RUN npm run build

# FROM node:20.12.2

# RUN npm install -g http-server
# WORKDIR /app
# COPY --from=builder /app/dist/angular-project/browser /app/dist

# EXPOSE 4253
# CMD ["http-server", "/app/dist", "-p", "4253"]

# FROM node:20.12.2 AS builder

# WORKDIR /app
# COPY package*.json ./
# RUN npm install
# COPY . .
# RUN npm run build -- --output-path=dist/angular-project/browser

# FROM node:20.12.2
# RUN npm install -g http-server
# WORKDIR /app
# COPY --from=builder /app/dist/angular-project/browser /app/dist
# EXPOSE 4252
# CMD ["http-server", "/app/dist", "-p", "4252"]
# Stage 1: Build the Angular application
# Stage 1: Build the Angular application
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
