
FROM node:20.12.2 AS build
WORKDIR /app

COPY package*.json ./
RUN npm install

COPY . .
RUN npm run build --prod

FROM nginx:alpine
COPY --from=build /app/dist/angular-project /usr/share/nginx/html

COPY nginx.conf /etc/nginx/nginx.conf
