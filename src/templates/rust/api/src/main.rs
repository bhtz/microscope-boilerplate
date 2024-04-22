use actix_web::{get, App, HttpResponse, HttpServer, Responder};
use crate::configurations::open_api_configuration::add_openapi_configuration;
use crate::controllers::weather_controller;
mod configurations;
mod controllers;

#[get("/version")]
async fn version() -> impl Responder {
    let version = option_env!("CARGO_PKG_VERSION").unwrap_or("1.0.0");
    HttpResponse::Ok().body(version)
}

#[actix_web::main]
async fn main() -> std::io::Result<()> {
    let port_env = option_env!("APPLICATION_PORT").unwrap_or("8080");
    let port: u16 = port_env.parse().expect("Port must be a valid number");

    println!("server running at port: http://0.0.0.0:{}", port);

    HttpServer::new(|| {
        App::new()
            .service(add_openapi_configuration())
            .service(version)
            .configure(weather_controller::new)
    })
    .bind(("0.0.0.0", port))?
    .run()
    .await
}
