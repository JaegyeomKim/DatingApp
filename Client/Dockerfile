FROM node:18-alpine

COPY package.json package-lock.json /App_Container/
COPY src /App_Container/

WORKDIR /App_Container

RUN npm install
RUN npm install -g @angular/cli

CMD ["ng", "build", "--configuration=production"]
