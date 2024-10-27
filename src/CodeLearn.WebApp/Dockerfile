FROM node:22-alpine AS build

# PNPM configuration
ENV PNPM_HOME="/pnpm"
ENV PATH="$PNPM_HOME:$PATH"
RUN corepack enable

# Set the working directory in the container
WORKDIR /app

# Copy package.json and pnpm-lock.yaml to the working directory
COPY package.json ./
COPY pnpm-lock.yaml ./

# Install project dependencies
RUN pnpm install

# Copy the entire React app code to the container
COPY . .

EXPOSE 4444
CMD ["pnpm", "run", "dev"]