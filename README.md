# JobSearchApp

## Overview

JobSearchApp is a web application designed for IT job searching, catering to three user types: Recruiters, Candidates, and Admin. The application offers features such as searching, filtering, and paging for vacancies and candidates. Recruiters benefit from an integrated ChatGPT system for auto-generating vacancy descriptions, enhancing efficiency in creating structured and comprehensive job listings. Candidates can upload PDF resumes directly to their profiles, allowing recruiters to view them without local downloads. The built-in chat system facilitates communication between recruiters and candidates, keeping work-related conversations within the app. The application also includes pop-up messages to notify users of errors or incorrect actions.

## Features

- Search, filter, and paginate through job vacancies and candidate profiles.
- Recruiters can create vacancies with auto-generated descriptions via ChatGPT integration.
- Candidates can upload PDF resumes to their profiles.
- Built-in chat system for communication between recruiters and candidates.
- Error notifications and guidance for users.

## Usage

JobSearchApp is designed for individuals seeking employment and those looking to hire IT professionals.

## Dependencies

### Client-side

- React, Redux, Redux Toolkit, MUI, Bootstrap, Formik, React Router, React PDF Viewer, Microsoft SignalR, Axios, Redux Persist, React Toastify.

### Server-side

- .NET 7, ASP.NET Core, EntityFrameworkCore, Serilog, FluentValidation, AutoMapper, MassTransit, MassTransit.RabbitMQ, AspNetCore.Authentication.JwtBearer, Newtonsoft, SignalR, Ocelot.

### Other

- Docker, Docker Compose, PostgreSQL, RabbitMQ, Redis.

## Installation

1. Install Docker, NodeJs, NPM.
2. Navigate to the client folder in the terminal, run `npm install`, and then `npm run dev`.
3. In the repository folder, run `docker-compose up --build` to start the server-side.

