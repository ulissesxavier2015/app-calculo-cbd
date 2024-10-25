FROM node:20.11.0-slim
WORKDIR /src

COPY ./front/ ./
RUN npm install -g @angular/cli
RUN npm install -g http-server
RUN npm ci && npm run build

CMD cd dist/web-app/browser && http-server -p 5001
EXPOSE 5001